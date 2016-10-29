using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinApi.KernelBase
{
    public static class Kernel32Methods
    {
        public const string LibraryName = "kernelbase";

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool CompareObjectHandles(IntPtr hFirstObjectHandle, IntPtr hSecondObjectHandle);
    }
}