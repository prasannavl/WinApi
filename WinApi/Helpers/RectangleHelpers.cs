using System;
using WinApi.User32;

namespace WinApi.Helpers
{
    public static class RectangleHelpers
    {
        public static Rectangle CreateFrom(ref Rectangle lvalue, ref Rectangle rvalue,
            Func<int, int, int> leftTopOperation,
            Func<int, int, int> rightBottomOperation = null)
        {
            if (rightBottomOperation == null) rightBottomOperation = leftTopOperation;
            return new Rectangle(
                leftTopOperation(lvalue.Left, rvalue.Left),
                leftTopOperation(lvalue.Top, rvalue.Top),
                rightBottomOperation(lvalue.Right, rvalue.Right),
                rightBottomOperation(lvalue.Bottom, rvalue.Bottom)
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

        public static void Translate(ref Rectangle src, int x, int y)
        {
            src.Top += y;
            src.Left += x;
            src.Bottom += y;
            src.Right += x;
        }

        public static void Scale(ref Rectangle src, int x, int y)
        {
            src.Top *= y;
            src.Left *= x;
            src.Bottom *= y;
            src.Right *= x;
        }
    }
}
