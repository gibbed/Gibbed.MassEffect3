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
using System.Runtime.InteropServices;

namespace Gibbed.MassEffect3.SFXArchiveUnpack
{
    public static class LZMA
    {
        private static readonly bool Is64Bit = DetectIs64Bit();
        private static bool DetectIs64Bit()
        {
            return Marshal.SizeOf(IntPtr.Zero) == 8;
        }

        public enum ErrorCode
        {
            Ok = 0,
            Data = 1,
            Mem = 2,
            Crc = 3,
            Unsupported = 4,
            Param = 5,
            InputEof = 6,
            OutputEof = 7,
            Read = 8,
            Write = 9,
            Progress = 10,
            Fail = 11,
            Thread = 12,
        }

        private static class Native32
        {
            [DllImport("lzma_32.dll",
                EntryPoint = "#67",
                CallingConvention = CallingConvention.StdCall)]
            internal static extern int CompressInternal(
                byte[] dest,
                ref uint destLen,
                byte[] src,
                uint srcLen,
                byte[] outProps,
                ref uint outPropsSize,
                int level,
                uint dictSize,
                int lc,
                int lp,
                int pb,
                int fb,
                int numThreads);

            [DllImport("lzma_32.dll",
                EntryPoint = "#68",
                CallingConvention = CallingConvention.StdCall)]
            internal static extern int DecompressInternal(
                byte[] dest,
                ref uint destLen,
                byte[] src,
                ref uint srcLen,
                byte[] props,
                uint propsSize);
        }

        private static class Native64
        {
            [DllImport("lzma_64.dll",
                EntryPoint = "#67",
                CallingConvention = CallingConvention.StdCall)]
            internal static extern int CompressInternal(
                byte[] dest,
                ref uint destLen,
                byte[] src,
                uint srcLen,
                byte[] outProps,
                ref uint outPropsSize,
                int level,
                uint dictSize,
                int lc,
                int lp,
                int pb,
                int fb,
                int numThreads);

            [DllImport("lzma_64.dll",
                EntryPoint = "#68",
                CallingConvention = CallingConvention.StdCall)]
            internal static extern int DecompressInternal(
                byte[] dest,
                ref uint destLen,
                byte[] src,
                ref uint srcLen,
                byte[] props,
                uint propsSize);
        }

        public static ErrorCode Compress(
            byte[] dest,
            ref uint destLen,
            byte[] src,
            uint srcLen,
            byte[] outProps,
            ref uint outPropsSize,
            int level,
            uint dictSize,
            int lc,
            int lp,
            int pb,
            int fb,
            int numThreads)
        {
            if (Is64Bit == true)
            {
                return (ErrorCode)Native64.CompressInternal(
                    dest,
                    ref destLen,
                    src,
                    srcLen,
                    outProps,
                    ref outPropsSize,
                    level,
                    dictSize,
                    lc,
                    lp,
                    pb,
                    fb,
                    numThreads);
            }

            return (ErrorCode)Native32.CompressInternal(
                dest,
                ref destLen,
                src,
                srcLen,
                outProps,
                ref outPropsSize,
                level,
                dictSize,
                lc,
                lp,
                pb,
                fb,
                numThreads);
        }

        public static ErrorCode Decompress(
            byte[] dest,
            ref uint destLen,
            byte[] src,
            ref uint srcLen,
            byte[] props,
            uint propsSize)
        {
            if (Is64Bit == true)
            {
                return (ErrorCode)Native64.DecompressInternal(
                    dest, ref destLen, src, ref srcLen, props, propsSize);
            }

            return (ErrorCode)Native32.DecompressInternal(
                dest, ref destLen, src, ref srcLen, props, propsSize);
        }
    }
}
