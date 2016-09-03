using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WinApi
{
    /// Pending review items below: They work. However, still up for further review.
    /// CharSet, SetLastError, ref [In/Out], marshalling performance,
    /// proper structures for parameter flags already defined, etc has to be reviewed
    /// Once satisfactory, and address the most optimal scenario, move out of staging.

    public delegate IntPtr GetMsgProc(int code, IntPtr wParam, IntPtr lParam);

    [SuppressUnmanagedCodeSecurity]
    public static partial class NativeMethods
    {

        //        [DllImport("user32.dll")]

        // Definite review needed before making it public:
        //        internal static extern uint SendInput(uint nInputs,
        //            [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs,

        //            int cbSize);
    }
}