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
using NDesk.Options;

namespace Gibbed.MassEffect3.PackageDecompress
{
    internal class Program
    {
        private static string GetExecutableName()
        {
            return Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static void Main(string[] args)
        {
            var showHelp = false;

            var options = new OptionSet()
            {
                {
                    "h|help",
                    "show this message and exit", 
                    v => showHelp = v != null
                },
            };

            List<string> extras;

            try
            {
                extras = options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("{0}: ", GetExecutableName());
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `{0} --help' for more information.", GetExecutableName());
                return;
            }

            if (extras.Count < 1 || extras.Count > 2 ||
                showHelp == true)
            {
                Console.WriteLine("Usage: {0} [OPTIONS]+ -j input_pcc [output_pcc]", GetExecutableName());
                Console.WriteLine();
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            var inputPath = extras[0];
            var outputPath = extras.Count >= 2 ? extras[1] : inputPath;
            var tempPath = Path.ChangeExtension(inputPath, "decompressing");

            if (File.Exists(tempPath) == true)
            {
                throw new InvalidOperationException("temporary path already exists?");
            }

            using (var input = File.OpenRead(inputPath))
            {
                var magic = input.ReadValueU32(Endian.Little);
                if (magic != 0x9E2A83C1 &&
                    magic.Swap() != 0x9E2A83C1)
                {
                    throw new FormatException("not a package");
                }
                var endian = magic == 0x9E2A83C1 ? Endian.Little : Endian.Big;

                var versionLo = input.ReadValueU16(endian);
                var versionHi = input.ReadValueU16(endian);

                if (versionLo != 684 &&
                    versionHi != 194)
                {
                    throw new FormatException("unsupported version");
                }

                long headerSize = 8;

                input.Seek(4, SeekOrigin.Current);
                headerSize += 4;

                var folderNameLength = input.ReadValueS32(endian);
                headerSize += 4;

                var folderNameByteLength =
                    folderNameLength >= 0 ? folderNameLength : (-folderNameLength * 2);
                input.Seek(folderNameByteLength, SeekOrigin.Current);
                headerSize += folderNameByteLength;

                var packageFlagsOffset = input.Position;
                var packageFlags = input.ReadValueU32(endian);
                headerSize += 4;

                if ((packageFlags & 0x02000000u) == 0)
                {
                    throw new FormatException("package is not flagged as compressed");
                }

                if ((packageFlags & 8) != 0)
                {
                    input.Seek(4, SeekOrigin.Current);
                    headerSize += 4;
                }

                input.Seek(60, SeekOrigin.Current);
                headerSize += 60;

                var generationsCount = input.ReadValueU32(endian);
                headerSize += 4;

                input.Seek(generationsCount * 12, SeekOrigin.Current);
                headerSize += generationsCount * 12;

                input.Seek(20, SeekOrigin.Current);
                headerSize += 20;

                var blockCount = input.ReadValueU32(endian);

                var blockInfos = new CompressedBlockInfo[blockCount];
                for (uint i = 0; i < blockCount; i++)
                {
                    blockInfos[i].UncompressedOffset = input.ReadValueU32(endian);
                    blockInfos[i].UncompressedSize = input.ReadValueU32(endian);
                    blockInfos[i].CompressedOffset = input.ReadValueU32(endian);
                    blockInfos[i].CompressedSize = input.ReadValueU32(endian);
                }

                var outputHeaderSize = headerSize + 4 + 8;
                var afterBlockTableOffset = input.Position;

                if (outputHeaderSize != blockInfos.First().UncompressedOffset)
                {
                    throw new FormatException();
                }

                using (var output = File.Create(tempPath))
                {
                    input.Seek(0, SeekOrigin.Begin);
                    output.Seek(0, SeekOrigin.Begin);

                    output.WriteFromStream(input, headerSize);
                    output.WriteValueU32(0, endian); // block count
                    input.Seek(afterBlockTableOffset, SeekOrigin.Begin);
                    output.WriteFromStream(input, 8);

                    output.Seek(packageFlagsOffset, SeekOrigin.Begin);
                    output.WriteValueU32(packageFlags & ~0x02000000u, endian);

                    foreach (var blockInfo in blockInfos)
                    {
                        input.Seek(blockInfo.CompressedOffset, SeekOrigin.Begin);
                        var blockMagic = input.ReadValueU32(endian);
                        if (blockMagic != 0x9E2A83C1)
                        {
                            throw new FormatException("bad compressed block magic");
                        }

                        var blockSegmentSize = input.ReadValueU32(endian);
                        /*var blockCompressedSize =*/ input.ReadValueU32(endian);
                        var blockUncompressedSize = input.ReadValueU32(endian);
                        if (blockUncompressedSize != blockInfo.UncompressedSize)
                        {
                            throw new FormatException("uncompressed size mismatch");
                        }

                        uint segmentCount = ((blockUncompressedSize + blockSegmentSize) - 1) / blockSegmentSize;

                        var segmentInfos = new CompressedSegmentInfo[segmentCount];
                        for (uint i = 0; i < segmentCount; i++)
                        {
                            segmentInfos[i].CompressedSize = input.ReadValueU32(endian);
                            segmentInfos[i].UncompressedSize = input.ReadValueU32(endian);
                        }

                        if (segmentInfos.Sum(si => si.UncompressedSize) != blockInfo.UncompressedSize)
                        {
                            throw new FormatException("uncompressed size mismatch");
                        }

                        output.Seek(blockInfo.UncompressedOffset, SeekOrigin.Begin);
                        foreach (var segmentInfo in segmentInfos)
                        {
                            using (var temp = input.ReadToMemoryStream(segmentInfo.CompressedSize))
                            {
                                var zlib = new InflaterInputStream(temp);
                                output.WriteFromStream(zlib, segmentInfo.UncompressedSize);
                            }
                        }
                    }
                }
            }

            File.Delete(outputPath);
            File.Move(tempPath, outputPath);
        }
    }
}
