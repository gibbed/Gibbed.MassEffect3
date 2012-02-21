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

namespace Gibbed.MassEffect3.UnpackPackage
{
    public class BlockStream : Stream
    {
        private Stream BaseStream;
        private List<Block> Blocks;
        private Block CurrentBlock;
        private long CurrentOffset;

        public BlockStream(Stream baseStream)
        {
            if (baseStream == null)
            {
                throw new ArgumentNullException("baseStream");
            }

            this.BaseStream = baseStream;
            this.Blocks = new List<Block>();
            this.CurrentOffset = 0;
        }

        public void AddBlock(
            uint uncompressedOffset,
            uint uncompressedSize,
            uint compressedOffset,
            uint compressedSize)
        {
            this.Blocks.Add(
                new Block(
                    uncompressedOffset,
                    uncompressedSize,
                    compressedOffset,
                    compressedSize));
        }

        private bool LoadBlock(long offset)
        {
            if (this.CurrentBlock == null ||
                this.CurrentBlock.IsValidOffset(offset) == false)
            {
                Block block = this.Blocks.SingleOrDefault(
                    candidate => candidate.IsValidOffset(offset));

                if (block == null)
                {
                    this.CurrentBlock = null;
                    return false;
                }

                this.CurrentBlock = block;
            }

            return this.CurrentBlock.Load(this.BaseStream);
        }

        public void SaveUncompressed(Stream output)
        {
            byte[] data = new byte[1024];

            uint totalSize = this.Blocks.Max(
                candidate =>
                    candidate.UncompressedOffset +
                    candidate.UncompressedSize);

            output.SetLength(totalSize);

            foreach (Block block in this.Blocks)
            {
                output.Seek(block.UncompressedOffset, SeekOrigin.Begin);
                this.Seek(block.UncompressedOffset, SeekOrigin.Begin);

                int total = (int)block.UncompressedSize;
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
            get { return this.BaseStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.BaseStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            this.BaseStream.Flush();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                return this.CurrentOffset;
            }

            set
            {
                this.Seek(value, SeekOrigin.Begin);
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalRead = 0;

            while (totalRead < count)
            {
                if (this.LoadBlock(this.CurrentOffset) == false)
                {
                    throw new InvalidOperationException();
                }

                int read = this.CurrentBlock.Read(
                    this.BaseStream,
                    this.CurrentOffset,
                    buffer,
                    offset,
                    count);

                totalRead += read;
                this.CurrentOffset += read;
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
                    return this.CurrentOffset;
                }

                offset = this.CurrentOffset + offset;
            }

            if (this.LoadBlock(offset) == false)
            {
                throw new InvalidOperationException();
            }

            this.CurrentOffset = offset;
            return this.CurrentOffset;
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
            public uint CompressedOffset { get; private set; }
            public uint CompressedSize { get; private set; }
            private uint SegmentSize;

            private bool Loaded;
            private List<Segment> Segments;
            private int CurrentSegmentIndex;
            private byte[] CurrentSegmentData;

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

                this.Loaded = false;
                this.Segments = new List<Segment>();
                this.CurrentSegmentIndex = -1;
            }

            public bool IsValidOffset(long offset)
            {
                return
                    offset >= this.UncompressedOffset &&
                    offset < this.UncompressedOffset + this.UncompressedSize;
            }

            public bool Load(Stream input)
            {
                if (this.Loaded == true)
                {
                    return true;
                }

                input.Seek(this.CompressedOffset, SeekOrigin.Begin);

                if (input.ReadValueU32() != 0x9E2A83C1)
                {
                    throw new FormatException("bad block magic");
                }

                this.SegmentSize = input.ReadValueU32();

                uint compressedSize = input.ReadValueU32();
                uint uncompressedSize = input.ReadValueU32();

                if (uncompressedSize != this.UncompressedSize)
                {
                    throw new InvalidOperationException();
                }

                uint count = ((uncompressedSize + this.SegmentSize) - 1) / this.SegmentSize;
                uint segmentOffset = (4 * 4) + (count * 8);

                for (uint i = 0; i < count; i++)
                {
                    Segment segment = new Segment();
                    segment.CompressedSize = input.ReadValueU32();
                    segment.UncompressedSize = input.ReadValueU32();
                    segment.Offset = segmentOffset;
                    this.Segments.Add(segment);
                    segmentOffset += segment.CompressedSize;
                }

                this.Loaded = true;
                return true;
            }

            public int Read(Stream input, long baseOffset, byte[] buffer, int offset, int count)
            {
                int relativeOffset = (int)(baseOffset - this.UncompressedOffset);
                int segmentIndex = relativeOffset / (int)this.SegmentSize;

                int totalRead = 0;

                while (relativeOffset < this.UncompressedSize)
                {
                    if (segmentIndex != this.CurrentSegmentIndex)
                    {
                        this.CurrentSegmentIndex = segmentIndex;
                        Segment segment = this.Segments[segmentIndex];

                        byte[] compressedData = new byte[segment.CompressedSize];
                        this.CurrentSegmentData = new byte[segment.UncompressedSize];

                        input.Seek(this.CompressedOffset + segment.Offset, SeekOrigin.Begin);
                        input.Read(compressedData, 0, compressedData.Length);

                        using (var temp = new MemoryStream(compressedData))
                        {
                            var zlib = new InflaterInputStream(temp);
                            if (zlib.Read(this.CurrentSegmentData, 0, this.CurrentSegmentData.Length) !=
                                this.CurrentSegmentData.Length)
                            {
                                throw new InvalidOperationException("decompression error");
                            }
                        }
                    }

                    int segmentOffset = relativeOffset % (int)this.SegmentSize;
                    int left = Math.Min(
                        count - totalRead,
                        this.CurrentSegmentData.Length - segmentOffset);

                    Array.ConstrainedCopy(
                        this.CurrentSegmentData,
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
