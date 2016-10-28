using System;
using System.Runtime.InteropServices;
using WinApi.Core;

namespace WinApi.Utils
{
    public class NativePixelBuffer : IDisposable
    {
        public IntPtr Handle { get; private set; }
        public int BufferLength { get; private set; }
        public int Stride { get; private set; }
        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }

        ~NativePixelBuffer()
        {
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (Handle != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Handle);
                Handle = IntPtr.Zero;
                BufferLength = 0;
                ImageWidth = ImageHeight = 0;
            }
        }

        public bool CheckSize(int imageWidth, int imageHeight)
        {
            return imageWidth == ImageWidth && imageHeight == ImageHeight;
        }

        public void EnsureSize(int imageWidth, int imageHeight)
        {
            if (CheckSize(imageWidth, imageHeight)) return;
            Resize(imageWidth, imageHeight);
        }

        public void Resize(int imageWidth, int imageHeight)
        {
            var stride = 4*((imageWidth*32 + 31)/32);
            var bufferLength = imageHeight * stride;
            if (bufferLength != BufferLength)
            {
                if (Handle != IntPtr.Zero)
                    Marshal.FreeHGlobal(Handle);
                Handle = Marshal.AllocHGlobal(bufferLength);
                BufferLength = bufferLength;
            }

            Stride = stride;
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
        }
    }
}