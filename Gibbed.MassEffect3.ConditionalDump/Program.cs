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
using System.Text;
using Gibbed.IO;
using NDesk.Options;

namespace Gibbed.MassEffect3.ConditionalDump
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
                Console.WriteLine("Usage: {0} [OPTIONS]+ input_cnd [output_file]", GetExecutableName());
                Console.WriteLine();
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            var inputPath = extras[0];
            var outputPath = extras.Count > 1 ? extras[1] : null;

            var cnd = new ConditionalsFile();
            using (var input = File.OpenRead(inputPath))
            {
                cnd.Deserialize(input);
            }

            if (outputPath == null)
            {
                DumpConditionals(Console.Out, cnd);
            }
            else
            {
                using (var output = File.Create(outputPath))
                {
                    var writer = new StreamWriter(output);
                    DumpConditionals(writer, cnd);
                    writer.Flush();
                }
            }
        }

        private static void DumpConditionals(TextWriter writer, ConditionalsFile cnd)
        {
            foreach (var id in cnd.Ids.OrderBy(id => id))
            {
                writer.WriteLine("[conditional_{0}]", id);

                var buffer = cnd.GetConditional(id);
                writer.WriteLine("{0}", DumpConditionalBool(new ByteBufferReader(buffer, 0, cnd.Endian)));

                writer.WriteLine();
            }
        }

        private static string DumpConditionalGeneric(ByteBufferReader reader)
        {
            var flags = reader[0].ReadValueU8();
            var flagType = (FlagType)((flags & 0x0F) >> 0);

            switch (flagType)
            {
                case FlagType.Bool:
                {
                    return DumpConditionalBool(reader);
                }

                case FlagType.Int:
                {
                    return DumpConditionalInt(reader);
                }

                default:
                {
                    return DumpConditionalFloat(reader);
                }
            }
        }

        private static string DumpConditionalBool(ByteBufferReader reader)
        {
            var flags = reader[0].ReadValueU8();

            var flagType = (FlagType)((flags & 0x0F) >> 0);
            var opType = (OpType)((flags & 0xF0) >> 4);

            switch (flagType)
            {
                case FlagType.Bool:
                {
                    switch (opType)
                    {
                        case OpType.StaticBool:
                        {
                            return reader[1].ReadValueB8() == true ? "true" : "false";
                        }

                        case OpType.Argument:
                        {
                            var value = reader[1].ReadValueS32();
                            if (value == -1)
                            {
                                return "(arg != 0)";
                            }

                            var functionLength = reader[5].ReadValueU16();
                            var tagLength = reader[7].ReadValueU16();

                            var function = reader[9].ReadString(functionLength, true, Encoding.ASCII);

                            var sb = new StringBuilder();
                            sb.Append("GetLocalVariable");
                            sb.Append("(");
                            sb.AppendFormat("\"{0}\"", function);
                            sb.Append(", ");
                            sb.AppendFormat("{0}", value);

                            if (tagLength > 0)
                            {
                                var tag = reader[9 + functionLength].ReadString(tagLength, true, Encoding.ASCII);
                                sb.Append(", ");
                                sb.AppendFormat("\"{0}\"", tag);
                            }

                            sb.Append(")");

                            return sb.ToString();
                        }

                        case OpType.Expression:
                        {
                            return "(" + DumpConditionalBoolExpression(reader) + ")";
                        }

                        case OpType.Table:
                        {
                            return "plot.bools[" + reader[1].ReadValueS32().ToString(CultureInfo.InvariantCulture) + "]";
                        }

                        default:
                        {
                            throw new NotImplementedException();
                        }
                    }
                }

                case FlagType.Int:
                {
                    return DumpConditionalInt(reader) + " != 0";
                }

                case FlagType.Float:
                {
                    return DumpConditionalFloat(reader) + " != 0";
                }

                default:
                {
                    throw new NotImplementedException();
                }
            }
        }

        private static string DumpConditionalBoolExpression(ByteBufferReader reader)
        {
            var op = reader[1].ReadValueU8();
            switch (op)
            {
                case 4:
                {
                    var count = reader[2].ReadValueU16();
                    var sb = new StringBuilder();

                    for (int i = 0, j = 0; i < count; i++, j += 2)
                    {
                        if (i > 0)
                        {
                            sb.Append(" && ");
                        }

                        var subReader = reader[reader[j + 4].ReadValueU16() + 4];
                        sb.Append(DumpConditionalBool(subReader));
                    }

                    return sb.ToString();
                }

                case 5:
                {
                    var count = reader[2].ReadValueU16();
                    var sb = new StringBuilder();

                    for (int i = 0, j = 0; i < count; i++, j += 2)
                    {
                        if (i > 0)
                        {
                            sb.Append(" || ");
                        }

                        var subReader = reader[reader[j + 4].ReadValueU16() + 4];
                        sb.Append(DumpConditionalBool(subReader));
                    }

                    return sb.ToString();
                }

                case 6:
                {
                    return DumpConditionalBool(reader[reader[4].ReadValueU16() + 4]) + " == false";
                }

                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                {
                    var left = DumpConditionalGeneric(reader[reader[4].ReadValueU16() + 4]);
                    var right = DumpConditionalGeneric(reader[reader[6].ReadValueU16() + 4]);

                    var comparisonType = reader[1].ReadValueU8();
                    switch (comparisonType)
                    {
                        case 7:
                        {
                            return left + " == " + right;
                        }

                        case 8:
                        {
                            return left + " != " + right;
                        }

                        case 9:
                        {
                            return left + " < " + right;
                        }

                        case 10:
                        {
                            return left + " <= " + right;
                        }

                        case 11:
                        {
                            return left + " > " + right;
                        }

                        case 12:
                        {
                            return left + " >= " + right;
                        }

                        default:
                        {
                            throw new NotImplementedException();
                        }
                    }
                }

                default:
                {
                    throw new NotImplementedException();
                }
            }
        }

        private static string DumpConditionalInt(ByteBufferReader reader)
        {
            var flags = reader[0].ReadValueU8();
            var flagType = (FlagType)((flags & 0x0F) >> 0);
            var opType = (OpType)((flags & 0xF0) >> 4);

            switch (flagType)
            {
                case FlagType.Int:
                {
                    switch (opType)
                    {
                        case OpType.StaticInt:
                        {
                            return reader[1].ReadValueS32().ToString(CultureInfo.InvariantCulture);
                        }

                        case OpType.Argument:
                        {
                            var value = reader[1].ReadValueS32();
                            if (value == -1)
                            {
                                return "arg";
                            }

                            throw new NotImplementedException();
                        }

                        case OpType.Expression:
                        {
                            return "(" + DumpConditionalIntExpression(reader) + ")";
                        }

                        case OpType.Table:
                        {
                            return "plot.ints[" + reader[1].ReadValueS32().ToString(CultureInfo.InvariantCulture) + "]";
                        }

                        default:
                        {
                            throw new NotImplementedException();
                        }
                    }
                }

                case FlagType.Float:
                {
                    return DumpConditionalFloat(reader);
                }

                default:
                {
                    throw new NotImplementedException();
                }
            }
        }

        private static string DumpConditionalIntExpression(ByteBufferReader reader)
        {
            var op = reader[1].ReadValueU8();
            switch (op)
            {
                case 0:
                {
                    var count = reader[2].ReadValueU16();
                    var sb = new StringBuilder();

                    for (int i = 0, j = 0; i < count; i++, j += 2)
                    {
                        if (i > 0)
                        {
                            sb.Append(" + ");
                        }

                        var subReader = reader[reader[j + 4].ReadValueU16() + 4];
                        sb.Append(DumpConditionalInt(subReader));
                    }

                    return sb.ToString();
                }

                default:
                {
                    throw new NotImplementedException();
                }
            }
        }

        private static string DumpConditionalFloat(ByteBufferReader reader)
        {
            var flags = reader[0].ReadValueU8();
            var flagType = (FlagType)((flags & 0x0F) >> 0);
            var opType = (OpType)((flags & 0xF0) >> 4);

            switch (flagType)
            {
                case FlagType.Int:
                {
                    return DumpConditionalInt(reader);
                }

                case FlagType.Float:
                {
                    switch (opType)
                    {
                        case OpType.StaticFloat:
                        {
                            return reader[1].ReadValueF32().ToString(CultureInfo.InvariantCulture);
                        }

                        case OpType.Expression:
                        {
                            return "(" + DumpConditionalFloatExpression(reader) + ")";
                        }

                        case OpType.Table:
                        {
                            return "plot.floats[" + reader[1].ReadValueS32().ToString(CultureInfo.InvariantCulture) + "]";
                        }

                        default:
                        {
                            throw new NotImplementedException();
                        }
                    }
                }

                default:
                {
                    throw new NotImplementedException();
                }
            }
        }

        private static string DumpConditionalFloatExpression(ByteBufferReader reader)
        {
            var op = reader[1].ReadValueU8();
            switch (op)
            {
                case 0:
                {
                    var count = reader[2].ReadValueU16();
                    var sb = new StringBuilder();

                    for (int i = 0, j = 0; i < count; i++, j += 2)
                    {
                        if (i > 0)
                        {
                            sb.Append(" + ");
                        }

                        var subReader = reader[reader[j + 4].ReadValueU16() + 4];
                        sb.Append(DumpConditionalFloat(subReader));
                    }

                    return sb.ToString();
                }

                case 2:
                {
                    var count = reader[2].ReadValueU16();
                    var sb = new StringBuilder();

                    for (int i = 0, j = 0; i < count; i++, j += 2)
                    {
                        if (i > 0)
                        {
                            sb.Append(" * ");
                        }

                        var subReader = reader[reader[j + 4].ReadValueU16() + 4];
                        sb.Append(DumpConditionalFloat(subReader));
                    }

                    return sb.ToString();
                }

                default:
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
