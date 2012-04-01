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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Gibbed.IO;

namespace Gibbed.MassEffect3.ConditionalDump
{
    public class ConditionalsFile
    {
        public Endian Endian;
        public uint Version;

        private readonly Dictionary<uint, byte[]> _Buffers
            = new Dictionary<uint, byte[]>();

        public ReadOnlyCollection<int> Ids
        {
            get { return new ReadOnlyCollection<int>(this._Conditionals.Keys.ToArray()); }
        }

        private readonly Dictionary<int, uint> _Conditionals
            = new Dictionary<int, uint>();

        public void Serialize(Stream output)
        {
            throw new NotSupportedException();
        }

        public void Deserialize(Stream input)
        {
            var magic = input.ReadValueU32(Endian.Little);
            if (magic != 0x434F4E44 && // COND
                magic.Swap() != 0x434F4E44)
            {
                throw new FormatException();
            }
            var endian = magic == 0x434F4E44 ? Endian.Little : Endian.Big;

            var version = input.ReadValueU32(endian);
            if (version != 1)
            {
                throw new FormatException();
            }
            this.Version = version;

            var unknown08 = input.ReadValueU16(endian);
            var count = input.ReadValueU16(endian);

            var ids = new int[count];
            var offsets = new uint[count];
            for (ushort i = 0; i < count; i++)
            {
                ids[i] = input.ReadValueS32(endian);
                offsets[i] = input.ReadValueU32(endian);
            }

            var sortedOffsets = offsets
                .OrderBy(o => o)
                .Distinct()
                .ToArray();

            this._Buffers.Clear();
            for (int i = 0; i < sortedOffsets.Length; i++)
            {
                var offset = sortedOffsets[i];
                if (offset == 0)
                {
                    continue;
                }

                var nextOffset = i + 1 < sortedOffsets.Length
                                     ? sortedOffsets[i + 1]
                                     : input.Length;

                input.Seek(offset, SeekOrigin.Begin);

                var length = (int)(nextOffset - offset);

                var bytes = input.ReadBytes(length);
                this._Buffers.Add(offset, bytes);
            }

            this._Conditionals.Clear();
            for (int i = 0; i < count; i++)
            {
                this._Conditionals.Add(ids[i], offsets[i]);
            }

            this.Endian = endian;
        }

        public byte[] GetConditional(int id)
        {
            if (this._Conditionals.ContainsKey(id) == false)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            var offset = this._Conditionals[id];
            if (this._Buffers.ContainsKey(offset) == false)
            {
                throw new InvalidOperationException();
            }

            return (byte[])this._Buffers[offset].Clone();
        }
    }
}
