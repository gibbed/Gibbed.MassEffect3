/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gibbed.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Gibbed.MassEffect3.AudioExtract
{
    public class BlockStream : Stream
    {
        private readonly Stream _BaseStream;
        private readonly List<Block> _Blocks;
        private Block _CurrentBlock;
        private long _CurrentOffset;

        public BlockStream(Stream baseStream)
        {
            if (baseStream == null)
            {
                throw new ArgumentNullException("baseStream");
            }

            this._BaseStream = baseStream;
            this._Blocks = new List<Block>();
            this._CurrentOffset = 0;
        }

        public void AddBlock(
            uint uncompressedOffset,
            uint uncompressedSize,
            uint compressedOffset,
            uint compressedSize)
        {
            this._Blocks.Add(
                new Block(
                    uncompressedOffset,
                    uncompressedSize,
                    compressedOffset,
                    compressedSize));
        }

        private bool LoadBlock(long offset)
        {
            if (this._CurrentBlock == null ||
                this._CurrentBlock.IsValidOffset(offset) == false)
            {
                Block block = this._Blocks.SingleOrDefault(
                    candidate => candidate.IsValidOffset(offset));

                if (block == null)
                {
                    this._CurrentBlock = null;
                    return false;
                }

                this._CurrentBlock = block;
            }

            return this._CurrentBlock.Load(this._BaseStream);
        }

        public void SaveUncompressed(Stream output)
        {
            var data = new byte[1024];

            uint totalSize = this._Blocks.Max(
                candidate =>
                candidate.UncompressedOffset +
                candidate.UncompressedSize);

            output.SetLength(totalSize);

            foreach (Block block in this._Blocks)
            {
                output.Seek(block.UncompressedOffset, SeekOrigin.Begin);
                this.Seek(block.UncompressedOffset, SeekOrigin.Begin);

                var total = (int)block.UncompressedSize;
                while (total > 0)
                {
                    int read = this.Read(data, 0, Math.Min(total, data.Length));
                    output.Write(data, 0, read);
                    total -= read;
                }
            }

            output.Flush();
        }

        #region Stream
        public override bool CanRead
        {
            get { return this._BaseStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this._BaseStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            this._BaseStream.Flush();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get { return this._CurrentOffset; }

            set { this.Seek(value, SeekOrigin.Begin); }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalRead = 0;

            while (totalRead < count)
            {
                if (this.LoadBlock(this._CurrentOffset) == false)
                {
                    throw new InvalidOperationException();
                }

                int read = this._CurrentBlock.Read(
                    this._BaseStream,
                    this._CurrentOffset,
                    buffer,
                    offset,
                    count);

                totalRead += read;
                this._CurrentOffset += read;
                offset += read;
                count -= read;
            }

            return totalRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.End)
            {
                throw new NotSupportedException();
            }

            if (origin == SeekOrigin.Current)
            {
                if (offset == 0)
                {
                    return this._CurrentOffset;
                }

                offset = this._CurrentOffset + offset;
            }

            if (this.LoadBlock(offset) == false)
            {
                throw new InvalidOperationException();
            }

            this._CurrentOffset = offset;
            return this._CurrentOffset;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
        #endregion

        private class Block
        {
            private struct Segment
            {
                public uint CompressedSize;
                public uint UncompressedSize;
                public uint Offset;
            }

            public uint UncompressedOffset { get; private set; }
            public uint UncompressedSize { get; private set; }
            private uint CompressedOffset { get; set; }
            private uint CompressedSize { get; set; }
            private uint _SegmentSize;

            private bool _IsLoaded;
            private readonly List<Segment> _Segments;
            private int _CurrentSegmentIndex;
            private byte[] _CurrentSegmentData;

            public Block(
                uint uncompressedOffset,
                uint uncompressedSize,
                uint compressedOffset,
                uint compressedSize)
            {
                this.UncompressedOffset = uncompressedOffset;
                this.UncompressedSize = uncompressedSize;
                this.CompressedOffset = compressedOffset;
                this.CompressedSize = compressedSize;

                this._IsLoaded = false;
                this._Segments = new List<Segment>();
                this._CurrentSegmentIndex = -1;
            }

            public bool IsValidOffset(long offset)
            {
                return
                    offset >= this.UncompressedOffset &&
                    offset < this.UncompressedOffset + this.UncompressedSize;
            }

            public bool Load(Stream input)
            {
                if (this._IsLoaded == true)
                {
                    return true;
                }

                input.Seek(this.CompressedOffset, SeekOrigin.Begin);

                if (input.ReadValueU32() != 0x9E2A83C1)
                {
                    throw new FormatException("bad block magic");
                }

                this._SegmentSize = input.ReadValueU32();

                /*uint compressedSize = */
                input.ReadValueU32();
                uint uncompressedSize = input.ReadValueU32();

                if (uncompressedSize != this.UncompressedSize)
                {
                    throw new InvalidOperationException();
                }

                uint count = ((uncompressedSize + this._SegmentSize) - 1) / this._SegmentSize;
                uint segmentOffset = (4 * 4) + (count * 8);

                for (uint i = 0; i < count; i++)
                {
                    // ReSharper disable UseObjectOrCollectionInitializer
                    var segment = new Segment();
                    // ReSharper restore UseObjectOrCollectionInitializer
                    segment.CompressedSize = input.ReadValueU32();
                    segment.UncompressedSize = input.ReadValueU32();
                    segment.Offset = segmentOffset;
                    this._Segments.Add(segment);
                    segmentOffset += segment.CompressedSize;
                }

                this._IsLoaded = true;
                return true;
            }

            public int Read(Stream input, long baseOffset, byte[] buffer, int offset, int count)
            {
                var relativeOffset = (int)(baseOffset - this.UncompressedOffset);
                int segmentIndex = relativeOffset / (int)this._SegmentSize;

                int totalRead = 0;

                while (relativeOffset < this.UncompressedSize)
                {
                    if (segmentIndex != this._CurrentSegmentIndex)
                    {
                        this._CurrentSegmentIndex = segmentIndex;
                        Segment segment = this._Segments[segmentIndex];

                        var compressedData = new byte[segment.CompressedSize];
                        this._CurrentSegmentData = new byte[segment.UncompressedSize];

                        input.Seek(this.CompressedOffset + segment.Offset, SeekOrigin.Begin);
                        input.Read(compressedData, 0, compressedData.Length);

                        using (var temp = new MemoryStream(compressedData))
                        {
                            var zlib = new InflaterInputStream(temp);
                            if (zlib.Read(this._CurrentSegmentData, 0, this._CurrentSegmentData.Length) !=
                                this._CurrentSegmentData.Length)
                            {
                                throw new InvalidOperationException("decompression error");
                            }
                        }
                    }

                    int segmentOffset = relativeOffset % (int)this._SegmentSize;
                    int left = Math.Min(
                        count - totalRead,
                        this._CurrentSegmentData.Length - segmentOffset);

                    Array.ConstrainedCopy(
                        this._CurrentSegmentData,
                        segmentOffset,
                        buffer,
                        offset,
                        left);

                    totalRead += left;

                    if (totalRead >= count)
                    {
                        break;
                    }

                    offset += left;
                    relativeOffset += left;
                    segmentIndex++;
                }

                return totalRead;
            }
        }
    }
}
