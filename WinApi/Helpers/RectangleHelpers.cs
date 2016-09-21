using System;
using WinApi.User32;

namespace WinApi.Helpers
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
            fixed (Rectangle* t = &lvalue)
            fixed (Rectangle* r = &rvalue)
            {
                Add(t, r);
            }
        }

        public static void Add(Rectangle* lvalue, Rectangle* rvalue)
        {
            lvalue->Left += rvalue->Left;
            lvalue->Top += rvalue->Top;
            lvalue->Right += rvalue->Right;
            lvalue->Bottom += rvalue->Bottom;
        }

        public static void Subtract(ref Rectangle lvalue, ref Rectangle rvalue)
        {
            fixed (Rectangle* t = &lvalue)
            fixed (Rectangle* r = &rvalue)
            {
                Subtract(t, r);
            }
        }

        public static void Subtract(Rectangle* lvalue, Rectangle* rvalue)
        {
            lvalue->Left -= rvalue->Left;
            lvalue->Top -= rvalue->Top;
            lvalue->Right -= rvalue->Right;
            lvalue->Bottom -= rvalue->Bottom;
        }
    }
}
