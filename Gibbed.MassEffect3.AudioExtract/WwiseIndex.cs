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
using Gibbed.IO;

namespace Gibbed.MassEffect3.AudioExtract
{
    public class WwiseIndex : FileFormats.Unreal.ISerializable
    {
        public List<string> Strings = new List<string>();
        public List<Resource> Resources = new List<Resource>();

        public static WwiseIndex Load(Stream input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var magic = input.ReadValueU32(Endian.Little);
            if (magic != 0x58444957 &&
                magic.Swap() != 0x58444957)
            {
                throw new FormatException("invalid magic");
            }
            var endian = magic == 0x58444957 ? Endian.Little : Endian.Big;

            var version = input.ReadValueU32(endian);
            if (version != 1)
            {
                throw new FormatException("unexpected version");
            }

            var reader = new FileFormats.Unreal.FileReader(input, version, endian);

            var index = new WwiseIndex();
            index.Serialize(reader);
            return index;
        }

        public void Serialize(FileFormats.Unreal.ISerializer stream)
        {
            stream.Serialize(ref this.Strings);
            stream.Serialize(ref this.Resources);
        }

        public class Resource : FileFormats.Unreal.ISerializable
        {
            public FileHash Hash;
            public List<Instance> Instances = new List<Instance>();

            public void Serialize(FileFormats.Unreal.ISerializer stream)
            {
                if (stream.Mode == FileFormats.Unreal.SerializeMode.Reading)
                {
                    uint a = 0, b = 0, c = 0, d = 0;
                    stream.Serialize(ref a);
                    stream.Serialize(ref b);
                    stream.Serialize(ref c);
                    stream.Serialize(ref d);
                    this.Hash = new FileHash(a, b, c, d);
                }
                else
                {
                    throw new NotSupportedException();
                }

                stream.Serialize(ref this.Instances);
            }
        }

        public class Instance : FileFormats.Unreal.ISerializable
        {
            public int PathIndex;
            public int NameIndex;
            public int ActorIndex;
            public int GroupIndex;
            public int LocaleIndex;
            public int FileIndex;
            public bool IsPackage;
            public int Offset;
            public int Size;

            public void Serialize(FileFormats.Unreal.ISerializer stream)
            {
                stream.Serialize(ref this.PathIndex);
                stream.Serialize(ref this.NameIndex);
                stream.Serialize(ref this.ActorIndex);
                stream.Serialize(ref this.GroupIndex);
                stream.Serialize(ref this.LocaleIndex);
                stream.Serialize(ref this.FileIndex);
                stream.Serialize(ref this.IsPackage);
                stream.Serialize(ref this.Offset);
                stream.Serialize(ref this.Size);
            }
        }

        public struct FileHash
        {
            public readonly uint A;
            public readonly uint B;
            public readonly uint C;
            public readonly uint D;

            public static FileHash Compute(byte[] bytes)
            {
                var md5 = System.Security.Cryptography.MD5.Create();
                return new FileHash(md5.ComputeHash(bytes));
            }

            public FileHash(uint a, uint b, uint c, uint d)
            {
                this.A = a;
                this.B = b;
                this.C = c;
                this.D = d;
            }

            public FileHash(byte[] bytes)
            {
                if (bytes.Length != 16)
                {
                    throw new ArgumentException("must be 16 bytes", "bytes");
                }

                this.A = BitConverter.ToUInt32(bytes, 0).Swap();
                this.B = BitConverter.ToUInt32(bytes, 4).Swap();
                this.C = BitConverter.ToUInt32(bytes, 8).Swap();
                this.D = BitConverter.ToUInt32(bytes, 12).Swap();
            }

            public override string ToString()
            {
                return string.Format("{0:X8}{1:X8}{2:X8}{3:X8}",
                                     this.A,
                                     this.B,
                                     this.C,
                                     this.D);
            }

            public override bool Equals(object obj)
            {
                if (obj == null || obj.GetType() != this.GetType())
                {
                    return false;
                }

                return (FileHash)obj == this;
            }

            public static bool operator !=(FileHash a, FileHash b)
            {
                return
                    a.A != b.A ||
                    a.B != b.B ||
                    a.C != b.C ||
                    a.D != b.D;
            }

            public static bool operator ==(FileHash a, FileHash b)
            {
                return
                    a.A == b.A &&
                    a.B == b.B &&
                    a.C == b.C &&
                    a.D == b.D;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 23 + this.A.GetHashCode();
                    hash = hash * 23 + this.B.GetHashCode();
                    hash = hash * 23 + this.C.GetHashCode();
                    hash = hash * 23 + this.D.GetHashCode();
                    return hash;
                }
            }
        }
    }
}
