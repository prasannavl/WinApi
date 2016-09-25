using System;
using WinApi.User32;

namespace WinApi.XWin.Helpers
{
    public static unsafe class RectangleHelpers
    {
        public static Rectangle CreateFrom(ref Rectangle lvalue, ref Rectangle rvalue,
            Func<int, int, int> operation,
            Func<int, int, int> flipSideOperation = null)
        {
            if (flipSideOperation == null) flipSideOperation = operation;
            return new Rectangle(
                operation(lvalue.Left, rvalue.Left),
                operation(lvalue.Top, rvalue.Top),
                flipSideOperation(lvalue.Right, rvalue.Right),
                flipSideOperation(lvalue.Bottom, rvalue.Bottom)
            );
        }

        public static void Add(ref Rectangle lvalue, ref Rectangle rvalue)
        {
            lvalue.Left += rvalue.Left;
            lvalue.Top += rvalue.Top;
            lvalue.Right += rvalue.Right;
            lvalue.Bottom += rvalue.Bottom;
        }

        public static void Subtract(ref Rectangle lvalue, ref Rectangle rvalue)
        {
            lvalue.Left -= rvalue.Left;
            lvalue.Top -= rvalue.Top;
            lvalue.Right -= rvalue.Right;
            lvalue.Bottom -= rvalue.Bottom;
        }

        public static void PadInside(ref Rectangle src, ref Rectangle padding)
        {
            src.Top += padding.Top;
            src.Left += padding.Left;
            src.Bottom -= padding.Bottom;
            src.Right -= padding.Right;
        }

        public static void PadOutside(ref Rectangle src, ref Rectangle padding)
        {
            src.Top -= padding.Top;
            src.Left -= padding.Left;
            src.Bottom += padding.Bottom;
            src.Right += padding.Right;
        }
    }
}
