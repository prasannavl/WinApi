using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace WinApi.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(Point other)
        {
            return (this.X == other.X) && (this.Y == other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && this.Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked { return (this.X*397) ^ this.Y; }
        }

        public int X, Y;

        public static bool operator ==(Point left, Point right)
        {
            return (left.X == right.X) && (left.Y == right.Y);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Size
    {
        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public bool Equals(Size other)
        {
            return (this.Width == other.Width) && (this.Height == other.Height);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Size && this.Equals((Size) obj);
        }

        public override int GetHashCode()
        {
            unchecked { return (this.Width*397) ^ this.Height; }
        }

        public int Width;
        public int Height;

        public static bool operator ==(Size left, Size right)
        {
            return (left.Width == right.Width) && (left.Height == right.Height);
        }

        public static bool operator !=(Size left, Size right)
        {
            return !(left == right);
        }
    }

    public enum HResult : uint
    {
        /// <summary>
        ///     Operation successful
        /// </summary>
        S_OK = 0x00000000,

        /// <summary>
        ///     Not implemented
        /// </summary>
        E_NOTIMPL = 0x80004001,

        /// <summary>
        ///     No such interface supported
        /// </summary>
        E_NOINTERFACE = 0x80004002,

        /// <summary>
        ///     Pointer that is not valid
        /// </summary>
        E_POINTER = 0x80004003,

        /// <summary>
        ///     Operation aborted
        /// </summary>
        E_ABORT = 0x80004004,

        /// <summary>
        ///     Unspecified failure
        /// </summary>
        E_FAIL = 0x80004005,

        /// <summary>
        ///     Unexpected failure
        /// </summary>
        E_UNEXPECTED = 0x8000FFFF,

        /// <summary>
        ///     General access denied error
        /// </summary>
        E_ACCESSDENIED = 0x80070005,

        /// <summary>
        ///     Handle that is not valid
        /// </summary>
        E_HANDLE = 0x80070006,

        /// <summary>
        ///     Failed to allocate necessary memory
        /// </summary>
        E_OUTOFMEMORY = 0x8007000E,

        /// <summary>
        ///     One or more arguments are not valid
        /// </summary>
        E_INVALIDARG = 0x80070057
    }
}