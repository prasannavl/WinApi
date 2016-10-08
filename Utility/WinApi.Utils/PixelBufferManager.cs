using System;
using System.Runtime.InteropServices;
using WinApi.Core;

namespace WinApi.Utils
{
    public class PixelBufferManager : IDisposable
    {
        public IntPtr PixelBufferPtr { get; private set; }
        public Size PixelBufferImageSize { get; private set; }
        public int PixelBufferSize { get; private set; }
        public int PixelBufferStride { get; private set; }

        public void Dispose()
        {
            if (PixelBufferPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(PixelBufferPtr);
                PixelBufferPtr = IntPtr.Zero;
                PixelBufferSize = 0;
                PixelBufferImageSize = new Size();
            }
        }

        public void ResizePixelBuffersIfRequired(ref Size targetSize)
        {
            var size = targetSize;
            if (targetSize == PixelBufferImageSize) return;

            var stride = 4*((size.Width*32 + 31)/32);
            var pixelBufferRequiredSize = size.Height*stride;
            if (pixelBufferRequiredSize != PixelBufferSize)
            {
                if (PixelBufferPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(PixelBufferPtr);
                PixelBufferPtr = Marshal.AllocHGlobal(pixelBufferRequiredSize);
                PixelBufferSize = pixelBufferRequiredSize;
            }

            PixelBufferStride = stride;
            PixelBufferImageSize = size;
        }
    }
}