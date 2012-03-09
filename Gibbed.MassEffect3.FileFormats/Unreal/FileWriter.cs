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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Gibbed.IO;

namespace Gibbed.MassEffect3.FileFormats.Unreal
{
    public class FileWriter : ISerializer
    {
        private Stream Output;

        public Endian Endian;
        public uint Version { get; private set; }
        public SerializeMode Mode { get { return SerializeMode.Writing; } }

        public FileWriter(Stream output, uint version)
            : this(output, version, Endian.Little)
        {
        }

        public FileWriter(Stream output, uint version, Endian endian)
        {
            this.Output = output;
            this.Version = version;
            this.Endian = endian;
        }

        public void Serialize(ref bool value)
        {
            this.Output.WriteValueB32(value, this.Endian);
        }

        public void Serialize(ref bool value, Func<ISerializer, bool> condition, Func<bool> defaultValue)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref value);
            }
        }

        public void Serialize(ref byte value)
        {
            this.Output.WriteValueU8(value);
        }

        public void Serialize(ref byte value, Func<ISerializer, bool> condition, Func<byte> defaultValue)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref int value)
        {
            this.Output.WriteValueS32(value, this.Endian);
        }

        public void Serialize(ref int value, Func<ISerializer, bool> condition, Func<int> defaultValue)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref value);
            }
        }

        public void Serialize(ref uint value)
        {
            this.Output.WriteValueU32(value, this.Endian);
        }

        public void Serialize(ref uint value, Func<ISerializer, bool> condition, Func<uint> defaultValue)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref value);
            }
        }

        public void Serialize(ref float value)
        {
            this.Output.WriteValueF32(value, this.Endian);
        }

        public void Serialize(ref float value, Func<ISerializer, bool> condition, Func<float> defaultValue)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref value);
            }
        }

        private void WriteString(string value)
        {
            if (value == null || value.Length == 0)
            {
                this.Output.WriteValueS32(0, this.Endian);
                return;
            }

            bool isUnicode = false;

            // detect unicode
            if (value.Any(c => c > 0xFF) == true)
            {
                this.Output.WriteValueS32(-(value.Length + 1), this.Endian);
                this.Output.WriteString(value, this.Endian == Endian.Little ?
                    Encoding.Unicode : Encoding.BigEndianUnicode);
                this.Output.WriteValueU16(0, this.Endian);
            }
            else
            {
                var bytes = new byte[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    bytes[i] = (byte)value[i];
                }

                this.Output.WriteValueS32(bytes.Length + 1, this.Endian);
                this.Output.WriteBytes(bytes);
                this.Output.WriteValueU8(0);
            }
        }

        public void Serialize(ref string value)
        {
            this.WriteString(value);
        }

        public void Serialize(ref string value, Func<ISerializer, bool> condition, Func<string> defaultValue)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref value);
            }
        }

        public void Serialize(ref Guid value)
        {
            this.Output.WriteValueGuid(value, this.Endian);
        }

        public void Serialize(ref Guid value, Func<ISerializer, bool> condition, Func<Guid> defaultValue)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref value);
            }
        }

        public void SerializeEnum<TEnum>(ref TEnum value)
        {
            this.Output.WriteValueEnum<TEnum>(value, this.Endian);
        }

        public void SerializeEnum<TEnum>(ref TEnum value, Func<ISerializer, bool> condition, Func<TEnum> defaultValue)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue");
            }

            if (condition(this) == false)
            {
                this.SerializeEnum<TEnum>(ref value);
            }
        }

        public void Serialize<TType>(ref TType value) where TType : ISerializable, new()
        {
            if (value == null)
            {
                throw new ArgumentException("value cannot be null", "value");
            }

            value.Serialize(this);
        }

        public void Serialize<TType>(ref TType value, Func<ISerializer, bool> condition, Func<TType> defaultValue) where TType : ISerializable, new()
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultValue == null)
            {
                throw new ArgumentNullException("defaultValue");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref value);
            }
        }

        public void Serialize(ref System.Collections.BitArray list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            uint count = ((uint)list.Count + 31) / 32;
            this.Output.WriteValueU32(count, this.Endian);

            for (uint i = 0; i < count; i++)
            {
                uint offset = i * 32;
                int bits = 0;

                for (int bit = 0; bit < 32 && offset + bit < list.Count; bit++)
                {
                    bits |= (list.Get((int)(offset + bit)) ? 1 : 0) << bit;
                }

                this.Output.WriteValueS32(bits, this.Endian);
            }
        }

        public void Serialize(ref System.Collections.BitArray list, Func<ISerializer, bool> condition, Func<System.Collections.BitArray> defaultList)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultList == null)
            {
                throw new ArgumentNullException("defaultList");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref list);
            }
        }

        private void WriteBasicList<TType>(List<TType> list, Action<FileWriter, TType> writeValue)
        {
            this.Output.WriteValueS32(list.Count, this.Endian);
            foreach (var item in list)
            {
                writeValue(this, item);
            }
        }

        public void Serialize(ref List<byte> list)
        {
            this.WriteBasicList<byte>(list, (w, v) => w.Output.WriteValueU8(v));
        }

        public void Serialize(ref List<byte> list, Func<ISerializer, bool> condition, Func<List<byte>> defaultList)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultList == null)
            {
                throw new ArgumentNullException("defaultList");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref list);
            }
        }

        public void Serialize(ref List<int> list)
        {
            this.WriteBasicList<int>(list, (w, v) => w.Output.WriteValueS32(v, w.Endian));
        }

        public void Serialize(ref List<int> list, Func<ISerializer, bool> condition, Func<List<int>> defaultList)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultList == null)
            {
                throw new ArgumentNullException("defaultList");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref list);
            }
        }

        public void Serialize(ref List<float> list)
        {
            this.WriteBasicList<float>(list, (w, v) => w.Output.WriteValueF32(v, w.Endian));
        }

        public void Serialize(ref List<float> list, Func<ISerializer, bool> condition, Func<List<float>> defaultList)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultList == null)
            {
                throw new ArgumentNullException("defaultList");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref list);
            }
        }

        public void Serialize(ref List<string> list)
        {
            this.WriteBasicList<string>(list, (w, v) => w.WriteString(v));
        }

        public void Serialize(ref List<string> list, Func<ISerializer, bool> condition, Func<List<string>> defaultList)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultList == null)
            {
                throw new ArgumentNullException("defaultList");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref list);
            }
        }

        public void Serialize(ref List<Guid> list)
        {
            this.WriteBasicList<Guid>(list, (w, v) => w.Output.WriteValueGuid(v, w.Endian));
        }

        public void Serialize(ref List<Guid> list, Func<ISerializer, bool> condition, Func<List<Guid>> defaultList)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultList == null)
            {
                throw new ArgumentNullException("defaultList");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref list);
            }
        }

        public void SerializeEnum<TEnum>(ref List<TEnum> list)
        {
            this.WriteBasicList<TEnum>(list, (w, v) => w.Output.WriteValueEnum<TEnum>(v, w.Endian));
        }

        public void SerializeEnum<TEnum>(ref List<TEnum> list, Func<ISerializer, bool> condition, Func<List<TEnum>> defaultList)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultList == null)
            {
                throw new ArgumentNullException("defaultList");
            }

            if (condition(this) == false)
            {
                this.SerializeEnum<TEnum>(ref list);
            }
        }

        public void Serialize<TType>(ref List<TType> list) where TType : ISerializable, new()
        {
            this.Output.WriteValueS32(list.Count, this.Endian);
            foreach (var item in list)
            {
                if (item == null)
                {
                    throw new ArgumentException("items in list cannot be null", "list");
                }
                item.Serialize(this);
            }
        }

        public void Serialize<TType>(ref List<TType> list, Func<ISerializer, bool> condition, Func<List<TType>> defaultList) where TType : ISerializable, new()
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (defaultList == null)
            {
                throw new ArgumentNullException("defaultList");
            }

            if (condition(this) == false)
            {
                this.Serialize(ref list);
            }
        }
    }
}
