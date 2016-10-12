using System;
using System.Runtime.InteropServices;
using WinApi.Core;

namespace WinApi.Utils
{
    public class NativePixelBuffer : IDisposable
    {
        public IntPtr Handle { get; private set; }
        public Size ImageSize { get; private set; }
        public int BufferLength { get; private set; }
        public int Stride { get; private set; }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Handle);
                Handle = IntPtr.Zero;
                BufferLength = 0;
                ImageSize = new Size();
            }
        }

        public void EnsureSize(ref Size targetSize)
        {
            var size = targetSize;
            if (size == ImageSize) return;

            var stride = 4*((size.Width*32 + 31)/32);
            var bufferLength = size.Height*stride;
            if (bufferLength != BufferLength)
            {
                if (Handle != IntPtr.Zero)
                    Marshal.FreeHGlobal(Handle);
                Handle = Marshal.AllocHGlobal(bufferLength);
                BufferLength = bufferLength;
            }

            Stride = stride;
            ImageSize = size;
        }
    }
}