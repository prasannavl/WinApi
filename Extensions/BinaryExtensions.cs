using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApi.Extensions
{
    public static class IntPtrExtensions
    {
        public static int ToSafeInt(this IntPtr ptr)
        {
            return unchecked(IntPtr.Size > 4 ? (int)ptr.ToInt64() : ptr.ToInt32());
        }

        public static uint ToSafeUInt(this IntPtr ptr)
        {
            return IntPtr.Size > 4 ? (uint)ptr.ToInt64() : (uint)ptr.ToInt32();
        }

        public static void BreakAsPtr64(this IntPtr ptr, out int high, out int low)
        {
            var param = ptr.ToInt64();
            low = param.Low();
            high = param.High();
        }

        public static void BreakAsPtr64(this IntPtr ptr, out long high, out long low)
        {
            var param = ptr.ToInt64();
            low = param.LowAsLong();
            high = param.HighAsLong();
        }

        public static void BreakAsPtr32(this IntPtr ptr, out int high, out int low)
        {
            var param = ptr.ToInt32();
            low = param.LowAsInt();
            high = param.HighAsInt();
        }

        public static void BreakAsPtr32(this IntPtr ptr, out short high, out short low)
        {
            var param = ptr.ToInt32();
            low = param.Low();
            high = param.High();
        }

        public static void BreakIntoSafeInt(this IntPtr ptr, out int high, out int low)
        {
            if (IntPtr.Size > 4)
            {
                BreakAsPtr64(ptr, out high, out low);
            }
            else
            {
                BreakAsPtr32(ptr, out high, out low);
            }
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
