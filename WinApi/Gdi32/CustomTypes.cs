using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinApi.Gdi32
{
    public class NativeBitmapInfoHandle : CriticalHandle
    {
        public unsafe NativeBitmapInfoHandle(ref BitmapInfo bitmapInfo) : base(new IntPtr(0))
        {
            var quads = bitmapInfo.Colors;
            var quadsLength = quads.Length;
            if (quadsLength == 0) { quadsLength = 1; }
            var success = false;
            var ptr = IntPtr.Zero;
            try
            {
                ptr =
                    Marshal.AllocHGlobal(Marshal.SizeOf<BitmapInfoHeader>() + Marshal.SizeOf<RgbQuad>()*quadsLength);
                var headerPtr = (BitmapInfoHeader*) ptr.ToPointer();
                *headerPtr = bitmapInfo.Header;
                var quadPtr = (RgbQuad*) (headerPtr + 1);
                var i = 0;
                for (; i < quads.Length; i++) { *(quadPtr + i) = quads[i]; }
                if (i == 0) { *quadPtr = new RgbQuad(); }
                this.SetHandle(ptr);
                success = true;
            }
            finally
            {
                if (!success)
                {
                    this.SetHandleAsInvalid();
                    Marshal.FreeHGlobal(ptr);
                }
            }
        }

        public override bool IsInvalid => this.handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(this.handle);
            return true;
        }

        public IntPtr GetDangerousHandle() => this.handle;
    }
}