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
using System.Globalization;
using System.IO;
using System.Linq;
using Gibbed.IO;
using Gibbed.MassEffect3.FileFormats;
using NDesk.Options;
using SFXArchive = Gibbed.MassEffect3.FileFormats.SFXArchive;

namespace Gibbed.MassEffect3.UnpackSFXArchive
{
    internal class Program
    {
        private static string GetExecutableName()
        {
            return Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        }

        public static void Main(string[] args)
        {
            bool showHelp = false;
            bool extractUnknowns = true;
            bool overwriteFiles = false;
            bool verbose = false;

            var options = new OptionSet()
            {
                {
                    "o|overwrite",
                    "overwrite existing files",
                    v => overwriteFiles = v != null
                },
                {
                    "nu|no-unknowns",
                    "don't extract unknown files",
                    v => extractUnknowns = v == null
                },
                {
                    "v|verbose",
                    "be verbose",
                    v => verbose = v != null
                },
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

            if (extras.Count < 1 || extras.Count > 2 || showHelp == true)
            {
                Console.WriteLine("Usage: {0} [OPTIONS]+ input_sfar [output_dir]", GetExecutableName());
                Console.WriteLine();
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            var inputPath = extras[0];
            var outputPath = extras.Count > 1 ? extras[1] : Path.ChangeExtension(inputPath, null);

            var manager = ProjectData.Manager.Load();
            if (manager.ActiveProject == null)
            {
                Console.WriteLine("Warning: no active project loaded.");
            }
            var hashes = manager.LoadLists(
                "*.filelist",
                FileNameHash.Compute,
                s => s.Replace("\\", "/"));

            using (var input = File.OpenRead(inputPath))
            {
                var sfx = new SFXArchiveFile();
                sfx.Deserialize(input);

                long current = 0;
                long total = sfx.Entries.Count;
                var padding = total.ToString(CultureInfo.InvariantCulture).Length;

                if (sfx.CompressionScheme != SFXArchive.CompressionScheme.None &&
                    sfx.CompressionScheme != SFXArchive.CompressionScheme.Lzma &&
                    sfx.CompressionScheme != SFXArchive.CompressionScheme.Lzx)
                {
                    Console.WriteLine("Unsupported compression scheme!");
                    return;
                }

                var inputBlock = new byte[sfx.MaximumBlockSize];
                var outputBlock = new byte[sfx.MaximumBlockSize];

                var hashesFromFile = new Dictionary<FileNameHash, string>();

                // todo: figure out what the file name is
                var fileNameListNameHash = new FileNameHash(
                    new byte[] { 0xB5, 0x50, 0x19, 0xCB, 0xF9, 0xD3, 0xDA, 0x65, 0xD5, 0x5B, 0x32, 0x1C, 0x00, 0x19, 0x69, 0x7C, });
                var fileNameListEntry = sfx.Entries
                    .FirstOrDefault(e =>
                        e.NameHash == fileNameListNameHash);
                if (fileNameListEntry != null)
                {
                    using (var temp = new MemoryStream())
                    {
                        DecompressEntry(
                            sfx, fileNameListEntry,
                            input, inputBlock,
                            temp, outputBlock);
                        temp.Position = 0;

                        var reader = new StreamReader(temp);
                        while (reader.EndOfStream == false)
                        {
                            var line = reader.ReadLine();
                            hashesFromFile.Add(FileNameHash.Compute(line), line);
                        }
                    }
                }

                foreach (var entry in sfx.Entries)
                {
                    current++;

                    var entryName = hashes[entry.NameHash];
                    
                    if (entryName == null)
                    {
                        if (hashesFromFile.ContainsKey(entry.NameHash) == true)
                        {
                            entryName = hashesFromFile[entry.NameHash];
                        }
                    }

                    if (entryName == null)
                    {
                        if (extractUnknowns == false)
                        {
                            continue;
                        }

                        entryName = entry.NameHash.ToString();
                        entryName = Path.Combine("__UNKNOWN", entryName);
                    }
                    else
                    {
                        entryName = entryName.Replace("/", "\\");
                        if (entryName.StartsWith("\\") == true)
                        {
                            entryName = entryName.Substring(1);
                        }
                    }

                    var entryPath = Path.Combine(outputPath, entryName);
                    if (overwriteFiles == false &&
                        File.Exists(entryPath) == true)
                    {
                        continue;
                    }

                    if (verbose == true)
                    {
                        Console.WriteLine("[{0}/{1}] {2}",
                            current.ToString(CultureInfo.InvariantCulture).PadLeft(padding), total, entryName);
                    }

                    input.Seek(entry.Offset, SeekOrigin.Begin);

                    Directory.CreateDirectory(Path.GetDirectoryName(entryPath));
                    using (var output = File.Create(entryPath))
                    {
                        DecompressEntry(
                            sfx, entry,
                            input, inputBlock,
                            output, outputBlock);
                    }
                }
            }
        }

        private static void DecompressEntry(
            SFXArchiveFile sfx,
            SFXArchive.Entry entry,
            Stream input,
            byte[] inputBlock,
            Stream output,
            byte[] outputBlock)
        {
            var left = entry.UncompressedSize;
            input.Seek(entry.Offset, SeekOrigin.Begin);

            if (entry.BlockSizeIndex == -1)
            {
                output.WriteFromStream(input, entry.UncompressedSize);
            }
            else
            {
                var blockSizeIndex = entry.BlockSizeIndex;
                while (left > 0)
                {
                    var compressedBlockSize = sfx.BlockSizes[blockSizeIndex];
                    if (compressedBlockSize == 0)
                    {
                        compressedBlockSize = sfx.MaximumBlockSize;
                    }

                    if (sfx.CompressionScheme == SFXArchive.CompressionScheme.None)
                    {
                        output.WriteFromStream(input, compressedBlockSize);
                        left -= compressedBlockSize;
                    }
                    else if (sfx.CompressionScheme == SFXArchive.CompressionScheme.Lzma)
                    {
                        if (compressedBlockSize == sfx.MaximumBlockSize ||
                            compressedBlockSize == left)
                        {
                            output.WriteFromStream(input, compressedBlockSize);
                            left -= compressedBlockSize;
                        }
                        else
                        {
                            var uncompressedBlockSize = (uint)Math.Min(
                                left, sfx.MaximumBlockSize);

                            if (compressedBlockSize < 5)
                            {
                                throw new InvalidOperationException();
                            }

                            var properties = input.ReadBytes(5);
                            compressedBlockSize -= 5;

                            if (input.Read(inputBlock, 0, (int)compressedBlockSize)
                                != compressedBlockSize)
                            {
                                throw new EndOfStreamException();
                            }

                            uint actualUncompressedBlockSize = uncompressedBlockSize;
                            uint actualCompressedBlockSize = compressedBlockSize;

                            var error = LZMA.Decompress(
                                outputBlock,
                                ref actualUncompressedBlockSize,
                                inputBlock,
                                ref actualCompressedBlockSize,
                                properties,
                                (uint)properties.Length);

                            if (error != LZMA.ErrorCode.Ok ||
                                uncompressedBlockSize != actualUncompressedBlockSize ||
                                compressedBlockSize != actualCompressedBlockSize)
                            {
                                throw new InvalidOperationException();
                            }

                            output.Write(outputBlock, 0, (int)actualUncompressedBlockSize);
                            left -= uncompressedBlockSize;
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    blockSizeIndex++;
                }
            }
        }
    }
}
