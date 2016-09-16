using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApi.Extensions
{
    public static class IntPtrExtensions
    {
        public static int ToLowInt32(this IntPtr ptr)
        {
            return unchecked(IntPtr.Size > 4 ? (int)ptr.ToInt64() : ptr.ToInt32());
        }

        public static uint ToLowUInt32(this IntPtr ptr)
        {
            return IntPtr.Size > 4 ? (uint)ptr.ToInt64() : (uint)ptr.ToInt32();
        }

        public static void BreakInt64Into32(this IntPtr ptr, out int high32, out int low32)
        {
            var param = ptr.ToInt64();
            low32 = param.Low();
            high32 = param.High();
        }

        public static void BreakInt64Into32(this IntPtr ptr, out long high32, out long low32)
        {
            var param = ptr.ToInt64();
            low32 = param.LowAsLong();
            high32 = param.HighAsLong();
        }

        public static void BreakInt32Into16(this IntPtr ptr, out int high16, out int low16)
        {
            var param = ptr.ToInt32();
            low16 = param.LowAsInt();
            high16 = param.HighAsInt();
        }

        public static void BreakInt32Into16(this IntPtr ptr, out short high16, out short low16)
        {
            var param = ptr.ToInt32();
            low16 = param.Low();
            high16 = param.High();
        }

        public static void BreakLowInt32To16(this IntPtr ptr, out short high16, out short low16)
        {
            var param = ptr.ToLowInt32();
            low16 = param.Low();
            high16 = param.High();
        }

        public static void BreakLowInt32To16(this IntPtr ptr, out int high16, out int low16)
        {
            var param = ptr.ToLowInt32();
            low16 = param.LowAsInt();
            high16 = param.HighAsInt();
        }
    }

    public static class IntExtensions
    {
        public static short Low(this int dword)
        {
            return unchecked ((short)dword);
        }

        public static short High(this int dword)
        {
            return unchecked((short) (dword >> 16 & 0xffff));
        }

        public static int LowAsInt(this int dword)
        {
            return dword & 0xffff;
        }

        public static int HighAsInt(this int dword)
        {
            return dword >> 16 & 0xffff;
        }
    }

    public static class UIntExtensions
    {
        public static ushort Low(this uint dword)
        {
            return unchecked((ushort)dword);
        }

        public static ushort High(this uint dword)
        {
            return unchecked((ushort)(dword >> 16 & 0xffff));
        }

        public static uint LowAsInt(this uint dword)
        {
            return dword & 0xffff;
        }

        public static uint HighAsInt(this uint dword)
        {
            return dword >> 16 & 0xffff;
        }
    }

    public static class LongExtensions
    {
        public static int Low(this long qword)
        {
            return unchecked((int) qword);
        }

        public static int High(this long qword)
        {
            return unchecked((int) (qword >> 32 & 0xffffffff));
        }

        public static long LowAsLong(this long qword)
        {
            return qword & 0xffffffff;
        }

        public static long HighAsLong(this long qword)
        {
            return qword >> 32 & 0xffffffff;
        }
    }

    public static class ULongExtensions
    {
        public static uint Low(this ulong qword)
        {
            return unchecked((uint)qword);
        }

        public static uint High(this ulong qword)
        {
            return unchecked((uint)(qword >> 32 & 0xffffffff));
        }

        public static ulong LowAsLong(this ulong qword)
        {
            return qword & 0xffffffff;
        }

        public static ulong HighAsLong(this ulong qword)
        {
            return qword >> 32 & 0xffffffff;
        }
    }
}
