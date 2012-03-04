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

namespace Gibbed.MassEffect3.UnpackSFXArchive
{
    public static class LZMA
    {
        private static bool Is64Bit = DetectIs64Bit();
        private static bool DetectIs64Bit()
        {
            return Marshal.SizeOf(IntPtr.Zero) == 8;
        }

        public enum ErrorCode : int
        {
            OK = 0,
            DATA = 1,
            MEM = 2,
            CRC = 3,
            UNSUPPORTED = 4,
            PARAM = 5,
            INPUT_EOF = 6,
            OUTPUT_EOF = 7,
            READ = 8,
            WRITE = 9,
            PROGRESS = 10,
            FAIL = 11,
            THREAD = 12,
        }

        private sealed class Native32
        {
            [DllImport("lzma_32.dll",
                EntryPoint = "#67",
                CallingConvention = CallingConvention.StdCall)]
            internal static extern int Compress(
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
            internal static extern int Decompress(
                byte[] dest,
                ref uint destLen,
                byte[] src,
                ref uint srcLen,
                byte[] props,
                uint propsSize);
        }

        private sealed class Native64
        {
            [DllImport("lzma_64.dll",
                EntryPoint = "#67",
                CallingConvention = CallingConvention.StdCall)]
            internal static extern int Compress(
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
            internal static extern int Decompress(
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
                return (ErrorCode)Native64.Compress(
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
            else
            {
                return (ErrorCode)Native32.Compress(
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
                return (ErrorCode)Native64.Decompress(
                    dest, ref destLen, src, ref srcLen, props, propsSize);
            }
            else
            {
                return (ErrorCode)Native32.Decompress(
                    dest, ref destLen, src, ref srcLen, props, propsSize);
            }
        }
    }
}
