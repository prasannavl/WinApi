using System;
using System.Runtime.InteropServices;
using System.Security;
using WinApi.Core;
using WinApi.User32;

namespace WinApi.Gdi32
{
    [SuppressUnmanagedCodeSecurity]
    public static class Gdi32Methods
    {
        public const string LibraryName = "gdi32";

        [DllImport(LibraryName)]
        public static extern IntPtr GetStockObject(int fnObject);

        [DllImport(LibraryName)]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport(LibraryName)]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport(LibraryName)]
        public static extern int DeleteObject(IntPtr hObject);

        [DllImport(LibraryName)]
        public static extern int DeleteDC(IntPtr hdc);

        [DllImport(LibraryName)]
        public static extern IntPtr CreateRectRgnIndirect([In] ref Rectangle lprc);

        [DllImport(LibraryName)]
        public static extern IntPtr CreateEllipticRgn(int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect);

        [DllImport(LibraryName)]
        public static extern IntPtr CreateEllipticRgnIndirect([In] ref Rectangle lprc);

        [DllImport(LibraryName)]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2,
            int cx, int cy);

        [DllImport(LibraryName)]
        public static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1,
            IntPtr hrgnSrc2, int fnCombineMode);

        [DllImport(LibraryName)]
        public static extern int OffsetViewportOrgEx(IntPtr hdc, int nXOffset, int nYOffset, out Point lpPoint);

        [DllImport(LibraryName)]
        public static extern int SetViewportOrgEx(IntPtr hdc, int x, int y, out Point lpPoint);

        [DllImport(LibraryName)]
        public static extern int SetMapMode(IntPtr hdc, int fnMapMode);

        [DllImport(LibraryName)]
        public static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport(LibraryName)]
        public static extern int ExtSelectClipRgn(IntPtr hdc, IntPtr hrgn, int fnMode);

        [DllImport(LibraryName)]
        public static extern int FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr);

        [DllImport(LibraryName)]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport(LibraryName)]
        public static extern int FrameRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr, int nWidth,
            int nHeight);

        [DllImport(LibraryName)]
        public static extern int PaintRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport(LibraryName)]
        public static extern int InvertRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport(LibraryName)]
        public static extern int LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        [DllImport(LibraryName)]
        public static extern int MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);

        [DllImport(LibraryName)]
        public static extern int RoundRect(IntPtr hdc, int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect, int nWidth, int nHeight);

        [DllImport(LibraryName)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport(LibraryName, CharSet = CharSet.Auto)]
        public static extern int TextOut(IntPtr hdc, int nXStart, int nYStart,
            string lpString, int cbString);

        [DllImport(LibraryName)]
        public static extern int BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight,
            IntPtr hdcSrc,
            int nXSrc, int nYSrc, BitBltFlags dwRop);

        [DllImport(LibraryName)]
        public static extern int StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest,
            int nWidthDest, int nHeightDest,
            IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            BitBltFlags dwRop);

        [DllImport(LibraryName)]
        public static extern IntPtr PathToRegion(IntPtr hdc);

        [DllImport(LibraryName)]
        public static extern IntPtr CreatePolygonRgn(
            Point[] lppt,
            int cPoints,
            int fnPolyFillMode);

        [DllImport(LibraryName)]
        public static extern IntPtr CreatePolyPolygonRgn(Point[] lppt,
            int[] lpPolyCounts,
            int nCount, int fnPolyFillMode);
    }
}