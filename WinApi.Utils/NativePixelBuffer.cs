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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (this.Handle != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.Handle);
                this.Handle = IntPtr.Zero;
                this.BufferLength = 0;
                this.ImageWidth = this.ImageHeight = 0;
            }
        }

        ~NativePixelBuffer()
        {
            this.Dispose();
        }

        public bool CheckSize(int imageWidth, int imageHeight)
        {
            return (imageWidth == this.ImageWidth) && (imageHeight == this.ImageHeight);
        }

        public void EnsureSize(int imageWidth, int imageHeight)
        {
            if (this.CheckSize(imageWidth, imageHeight)) return;
            this.Resize(imageWidth, imageHeight);
        }

        public void Resize(int imageWidth, int imageHeight)
        {
            var stride = 4*((imageWidth*32 + 31)/32);
            var bufferLength = imageHeight*stride;
            if (bufferLength != this.BufferLength)
            {
                if (this.Handle != IntPtr.Zero) Marshal.FreeHGlobal(this.Handle);
                this.Handle = Marshal.AllocHGlobal(bufferLength);
                this.BufferLength = bufferLength;
            }

            this.Stride = stride;
            this.ImageWidth = imageWidth;
            this.ImageHeight = imageHeight;
        }
    }
}