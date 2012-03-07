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

namespace Gibbed.MassEffect3.FileFormats.Unreal
{
    public class FileWriter : ISerializer
    {
        public uint Version
        {
            get { throw new NotImplementedException(); }
        }

        public SerializeMode Mode
        {
            get { throw new NotImplementedException(); }
        }

        public void Serialize(ref bool value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref bool value, Func<ISerializer, bool> condition, Func<bool> defaultValue)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref byte value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref byte value, Func<ISerializer, bool> condition, Func<byte> defaultValue)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref int value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref int value, Func<ISerializer, bool> condition, Func<int> defaultValue)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref uint value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref uint value, Func<ISerializer, bool> condition, Func<uint> defaultValue)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref float value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref float value, Func<ISerializer, bool> condition, Func<float> defaultValue)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref string value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref string value, Func<ISerializer, bool> condition, Func<string> defaultValue)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref Guid value)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref Guid value, Func<ISerializer, bool> condition, Func<Guid> defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SerializeEnum<TEnum>(ref TEnum value)
        {
            throw new NotImplementedException();
        }

        public void SerializeEnum<TEnum>(ref TEnum value, Func<ISerializer, bool> condition, Func<TEnum> defaultValue)
        {
            throw new NotImplementedException();
        }

        public void Serialize<TType>(ref TType value) where TType : ISerializable, new()
        {
            throw new NotImplementedException();
        }

        public void Serialize<TType>(ref TType value, Func<ISerializer, bool> condition, Func<TType> defaultValue) where TType : ISerializable, new()
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref System.Collections.BitArray list)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref System.Collections.BitArray list, Func<ISerializer, bool> condition, Func<System.Collections.BitArray> defaultList)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref List<byte> list)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref List<byte> list, Func<ISerializer, bool> condition, Func<List<byte>> defaultList)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref List<int> list)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref List<int> list, Func<ISerializer, bool> condition, Func<List<int>> defaultList)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref List<float> list)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref List<float> list, Func<ISerializer, bool> condition, Func<List<float>> defaultList)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref List<string> list)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref List<string> list, Func<ISerializer, bool> condition, Func<List<string>> defaultList)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref List<Guid> list)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref List<Guid> list, Func<ISerializer, bool> condition, Func<List<Guid>> defaultList)
        {
            throw new NotImplementedException();
        }

        public void SerializeEnum<TEnum>(ref List<TEnum> list)
        {
            throw new NotImplementedException();
        }

        public void SerializeEnum<TEnum>(ref List<TEnum> list, Func<ISerializer, bool> condition, Func<List<TEnum>> defaultList)
        {
            throw new NotImplementedException();
        }

        public void Serialize<TType>(ref List<TType> list) where TType : ISerializable, new()
        {
            throw new NotImplementedException();
        }

        public void Serialize<TType>(ref List<TType> list, Func<ISerializer, bool> condition, Func<List<TType>> defaultList) where TType : ISerializable, new()
        {
            throw new NotImplementedException();
        }
    }
}
