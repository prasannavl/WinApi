using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinApi.Kernel32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ShortPoint
    {
        public ShortPoint(short x, short y)
        {
            X = x;
            Y = y;
        }

        public short X, Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ShortRectangle
    {
        public ShortRectangle(short left = 0, short top = 0, short right = 0, short bottom = 0)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public short Left, Top, Right, Bottom;

        public short Width
        {
            get { return (short) (Right - Left); }
            set { Right = (short) (Left + value); }
        }

        public short Height
        {
            get { return (short) (Bottom - Top); }
            set { Bottom = (short) (Top + value); }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SystemInfo
    {
        public ushort ProcessorArchitecture;
        ushort Reserved;
        public uint PageSize;
        public IntPtr MinimumApplicationAddress;
        public IntPtr MaximumApplicationAddress;
        public IntPtr ActiveProcessorMask;
        public uint NumberOfProcessors;
        public uint ProcessorType;
        public uint AllocationGranularity;
        public ushort ProcessorLevel;
        public ushort ProcessorRevision;
        public uint OemId => ((uint) ProcessorArchitecture << 8) | Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SecurityAttributes
    {
        public uint Length;
        public IntPtr SecurityDescriptor;
        public uint IsHandleInheritedValue;

        public bool IsHandleInherited => IsHandleInheritedValue > 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FileTime
    {
        public uint Low;
        public uint High;

        public ulong Value => ((ulong) High << 32) | Low;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SystemTime
    {
        public ushort Year;
        public ushort Month;
        public ushort DayOfWeek;
        public ushort Day;
        public ushort Hour;
        public ushort Minute;
        public ushort Second;
        public ushort Milliseconds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FileAttributeData
    {
        public FileAttributes Attributes;
        public FileTime CreationTime;
        public FileTime LastAccessTime;
        public FileTime LastWriteTime;

        public uint FileSizeHigh;
        public uint FileSizeLow;

        public ulong FileSize => ((ulong)FileSizeHigh << 32) | FileSizeLow;
    }
}