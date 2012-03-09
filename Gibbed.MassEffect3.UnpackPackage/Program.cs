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
using System.Text;
using Gibbed.IO;
using NDesk.Options;

namespace Gibbed.MassEffect3.UnpackPackage
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
            string classFilter = null;

            var options = new OptionSet()
            {
                {
                    "c|class=",
                    "only unpack exports that are instances of specified class, ie Core.GFxUI.GFxMovieInfo",
                    v => classFilter = v
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
            var outputPath = extras.Count >= 2 ? extras[1] : Path.ChangeExtension(inputPath, null);

            using (var input = File.OpenRead(inputPath))
            {
                var magic = input.ReadValueU32(Endian.Little);
                if (magic != 0x9E2A83C1 &&
                    magic.Swap() != 0x9E2A83C1)
                {
                    throw new FormatException("not a package");
                }
                var endian = magic == 0x9E2A83C1 ?
                    Endian.Little : Endian.Big;
                var encoding = endian == Endian.Little ?
                    Encoding.Unicode : Encoding.BigEndianUnicode;

                var versionLo = input.ReadValueU16(endian);
                var versionHi = input.ReadValueU16(endian);

                if (versionLo != 684 &&
                    versionHi != 194)
                {
                    throw new FormatException("unsupported version");
                }

                input.Seek(4, SeekOrigin.Current);

                var folderNameLength = input.ReadValueS32(endian);
                var folderNameByteLength =
                    folderNameLength >= 0 ? folderNameLength : (-folderNameLength * 2);
                input.Seek(folderNameByteLength, SeekOrigin.Current);

                /*var packageFlagsOffset = input.Position;*/
                var packageFlags = input.ReadValueU32(endian);

                if ((packageFlags & 8) != 0)
                {
                    input.Seek(4, SeekOrigin.Current);
                }

                var nameCount = input.ReadValueU32(endian);
                var namesOffset = input.ReadValueU32(endian);
                var exportCount = input.ReadValueU32(endian);
                var exportInfosOffset = input.ReadValueU32(endian);
                var importCount = input.ReadValueU32(endian);
                var importInfosOffset = input.ReadValueU32(endian);

                Stream data;
                if ((packageFlags & 0x02000000) == 0)
                {
                    data = input;
                }
                else
                {
                    input.Seek(36, SeekOrigin.Current);

                    var generationsCount = input.ReadValueU32(endian);
                    input.Seek(generationsCount * 12, SeekOrigin.Current);

                    input.Seek(20, SeekOrigin.Current);

                    var blockCount = input.ReadValueU32(endian);
                    
                    var blockStream = new BlockStream(input);
                    for (int i = 0; i < blockCount; i++)
                    {
                        var uncompressedOffset = input.ReadValueU32(endian);
                        var uncompressedSize = input.ReadValueU32(endian);
                        var compressedOffset = input.ReadValueU32(endian);
                        var compressedSize = input.ReadValueU32(endian);
                        blockStream.AddBlock(
                            uncompressedOffset, uncompressedSize,
                            compressedOffset, compressedSize);
                    }

                    data = blockStream;
                }

                var names = new string[nameCount];
                
                var exportInfos = new ExportInfo[exportCount];
                for (uint i = 0; i < exportCount; i++)
                {
                    exportInfos[i] = new ExportInfo();
                }

                var importInfos = new ImportInfo[importCount];
                for (uint i = 0; i < importCount; i++)
                {
                    importInfos[i] = new ImportInfo();
                }

                data.Seek(namesOffset, SeekOrigin.Begin);
                for (uint i = 0; i < nameCount; i++)
                {
                    var nameLength = data.ReadValueS32(endian);

                    if (nameLength >= 0)
                    {
                        names[i] = data.ReadString(nameLength, true, Encoding.UTF8);
                    }
                    else
                    {
                        names[i] = data.ReadString(-nameLength * 2, true, encoding);
                    }
                }

                data.Seek(importInfosOffset, SeekOrigin.Begin);
                for (uint i = 0; i < importCount; i++)
                {
                    var importInfo = importInfos[i];

                    var packageNameIndex = data.ReadValueS32(endian);
                    importInfo.PackageName = names[packageNameIndex];

                    data.Seek(12, SeekOrigin.Current);

                    var outerIndex = data.ReadValueS32(endian);
                    importInfo.Outer = GetResource(exportInfos, importInfos, outerIndex);

                    var objectNameIndex = data.ReadValueS32(endian);
                    importInfo.ObjectName = names[objectNameIndex];

                    data.Seek(4, SeekOrigin.Current);
                }

                data.Seek(exportInfosOffset, SeekOrigin.Begin);
                for (uint i = 0; i < exportCount; i++)
                {
                    var exportInfo = exportInfos[i];

                    exportInfo.PackageName = Path.GetFileNameWithoutExtension(inputPath);

                    var classIndex = data.ReadValueS32(endian);
                    exportInfo.Class = GetResource(exportInfos, importInfos, classIndex);

                    data.Seek(4, SeekOrigin.Current);

                    var outerIndex = data.ReadValueS32(endian);
                    exportInfo.Outer = GetResource(exportInfos, importInfos, outerIndex);

                    var objectNameIndex = data.ReadValueS32(endian);
                    exportInfo.ObjectName = names[objectNameIndex];

                    data.Seek(16, SeekOrigin.Current);

                    exportInfo.DataSize = data.ReadValueU32(endian);
                    exportInfo.DataOffset = data.ReadValueU32(endian);

                    data.Seek(4, SeekOrigin.Current);
                    var count = data.ReadValueU32(endian);
                    data.Seek(count * 4, SeekOrigin.Current);
                    data.Seek(20, SeekOrigin.Current);
                }

                for (int i = 0; i < exportInfos.Length; i++)
                {
                    var exportInfo = exportInfos[i];

                    if (classFilter != null)
                    {
                        if (exportInfo.Class == null)
                        {
                            continue;
                        }

                        if (exportInfo.Class.FullName.ToLowerInvariant() !=
                            classFilter)
                        {
                            continue;
                        }
                    }

                    if (exportInfo.Class == null)
                    {
                        Console.WriteLine("{0}", exportInfo.FullName);
                    }
                    else
                    {
                        Console.WriteLine("({0}) {1}",
                            exportInfo.Class.FullName,
                            exportInfo.FullName);
                    }

                    var fullPath = exportInfo.FullPath;
                    fullPath = fullPath.Replace(":", "_");

                    var exportPath = Path.Combine(outputPath, fullPath + " [export#" + i.ToString(CultureInfo.InvariantCulture) + "].bin");
                    if (File.Exists(exportPath) == true)
                    {
                        throw new InvalidOperationException();
                    }

                    Directory.CreateDirectory(Path.GetDirectoryName(exportPath));

                    using (var output = File.Create(exportPath))
                    {
                        data.Seek(exportInfo.DataOffset, SeekOrigin.Begin);
                        output.WriteFromStream(data, exportInfo.DataSize);
                    }
                }
            }
        }

        private static IResource GetResource(
            ExportInfo[] exportInfos, ImportInfo[] importInfos, int index)
        {
            if (index == 0)
            {
                return null;
            }
            if (index < 0 && -index <= importInfos.Length)
            {
                return importInfos[-index - 1];
            }

            if (index > 0 && index <= exportInfos.Length)
            {
                return exportInfos[index - 1];
            }

            throw new InvalidOperationException();
        }
    }
}
