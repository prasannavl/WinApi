using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace WinApi.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X, Y;
    }
}