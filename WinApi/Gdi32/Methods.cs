using NetCoreEx.Geometry;
using System;
using System.Runtime.InteropServices;
using WinApi.User32;

namespace WinApi.Gdi32
{
    public static class Gdi32Methods
    {
        public const string LibraryName = "gdi32";

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetStockObject(int fnObject);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint SetPixel(IntPtr hdc, int x, int y, uint crColor);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int GetPixelFormat(IntPtr hdc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int SetBkMode(IntPtr hdc, int iBkMode);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, IntPtr pbmi,
            DibBmiColorUsageFlag iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern int GetObject(IntPtr hgdiobj, int cbBuffer, IntPtr lpvObject);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, IntPtr lpvBits);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateBitmapIndirect([In] ref Bitmap lpbm);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateDIBitmap(IntPtr hdc, [In] ref BitmapInfoHeader
                lpbmih, uint fdwInit, byte[] lpbInit, IntPtr lpbmi,
            DibBmiColorUsageFlag fuUsage);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateDIBitmap(IntPtr hdc, [In] ref BitmapInfoHeader
                lpbmih, uint fdwInit, IntPtr lpbInit, IntPtr lpbmi,
            DibBmiColorUsageFlag fuUsage);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int SetDIBitsToDevice(IntPtr hdc, int xDest, int yDest, uint
                dwWidth, uint dwHeight, int xSrc, int ySrc, uint uStartScan, uint cScanLines,
            byte[] lpvBits, IntPtr lpbmi, DibBmiColorUsageFlag fuColorUse);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int SetDIBitsToDevice(IntPtr hdc, int xDest, int yDest, uint
                dwWidth, uint dwHeight, int xSrc, int ySrc, uint uStartScan, uint cScanLines,
            IntPtr lpvBits, IntPtr lpbmi, DibBmiColorUsageFlag fuColorUse);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int SetBitmapBits(IntPtr hbmp, uint cBytes, [In] byte[] lpBits);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int GetBitmapBits(IntPtr hbmp, int cbBuffer, [Out] byte[] lpvBits);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int SetBitmapBits(IntPtr hbmp, uint cBytes, IntPtr lpBits);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int GetBitmapBits(IntPtr hbmp, int cbBuffer, IntPtr lpvBits);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateRectRgnIndirect([In] ref Rectangle lprc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateEllipticRgn(int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateEllipticRgnIndirect([In] ref Rectangle lprc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2,
            int cx, int cy);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1,
            IntPtr hrgnSrc2, RegionModeFlags fnCombineMode);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool OffsetViewportOrgEx(IntPtr hdc, int nXOffset, int nYOffset, out Point lpPoint);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetViewportOrgEx(IntPtr hdc, int x, int y, out Point lpPoint);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int SetMapMode(IntPtr hdc, int fnMapMode);


        /// <summary>
        ///     The GetClipBox function retrieves the dimensions of the tightest bounding rectangle that can be drawn around the
        ///     current visible area on the device. The visible area is defined by the current clipping region or clip path, as
        ///     well as any overlapping windows.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="lprc">A pointer to a RECT structure that is to receive the rectangle dimensions, in logical units.</param>
        /// <returns>If the function succeeds, the return value specifies the clipping box's complexity.</returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType GetClipBox(IntPtr hdc, out Rectangle lprc);

        /// <summary>
        ///     The GetClipRgn function retrieves a handle identifying the current application-defined clipping region for the
        ///     specified device context.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="hrgn">
        ///     A handle to an existing region before the function is called. After the function returns, this
        ///     parameter is a handle to a copy of the current clipping region.
        /// </param>
        /// <returns>
        ///     f the function succeeds and there is no clipping region for the given device context, the return value is
        ///     zero. If the function succeeds and there is a clipping region for the given device context, the return value is 1.
        ///     If an error occurs, the return value is -1.
        /// </returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int GetClipRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType SelectClipRgn(IntPtr hdc, IntPtr hrgn);

        /// <summary>
        ///     The ExtSelectClipRgn function combines the specified region with the current clipping region using the specified
        ///     mode.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="hrgn">
        ///     A handle to the region to be selected. This handle must not be NULL unless the RGN_COPY mode is
        ///     specified.
        /// </param>
        /// <param name="fnMode">The operation to be performed</param>
        /// <returns>The return value specifies the new clipping region's complexity.</returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType ExtSelectClipRgn(IntPtr hdc, IntPtr hrgn, RegionModeFlags fnMode);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType IntersectClipRect(IntPtr hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect
        );

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType ExcludeClipRect(IntPtr hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect
        );

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType OffsetClipRgn(IntPtr hdc,
            int nXOffset,
            int nYOffset
        );

        /// <summary>
        ///     The GetMetaRgn function retrieves the current metaregion for the specified device context.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="hrgn">
        ///     A handle to an existing region before the function is called. After the function returns, this
        ///     parameter is a handle to a copy of the current metaregion.
        /// </param>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetMetaRgn(IntPtr hdc, IntPtr hrgn);

        /// <summary>
        ///     The SetMetaRgn function intersects the current clipping region for the specified device context with the current
        ///     metaregion and saves the combined region as the new metaregion for the specified device context. The clipping
        ///     region is reset to a null region.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <returns>The return value specifies the new clipping region's complexity.</returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType SetMetaRgn(IntPtr hdc);

        /// <summary>
        ///     The LPtoDP function converts logical coordinates into device coordinates. The conversion depends on the mapping
        ///     mode of the device context, the settings of the origins and extents for the window and viewport, and the world
        ///     transformation.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="lpPoints">
        ///     A pointer to an array of POINT structures. The x-coordinates and y-coordinates contained in each
        ///     of the POINT structures will be transformed.
        /// </param>
        /// <param name="nCount">The number of points in the array.</param>
        /// <remarks>
        ///     The LPtoDP function fails if the logical coordinates exceed 32 bits, or if the converted device coordinates exceed
        ///     27 bits. In the case of such an overflow, the results for all the points are undefined.
        ///     LPtoDP calculates complex floating-point arithmetic, and it has a caching system for efficiency.Therefore, the
        ///     conversion result of an initial call to LPtoDP might not exactly match the conversion result of a later call to
        ///     LPtoDP.We recommend not to write code that relies on the exact match of the conversion results from multiple calls
        ///     to LPtoDP even if the parameters that are passed to each call are identical.
        /// </remarks>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool LPtoDP(IntPtr hdc, [In] [Out] ref Point lpPoints, int nCount);

        /// <summary>
        ///     The DPtoLP function converts device coordinates into logical coordinates. The conversion depends on the mapping
        ///     mode of the device context, the settings of the origins and extents for the window and viewport, and the world
        ///     transformation.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="lpPoints">
        ///     A pointer to an array of POINT structures. The x-coordinates and y-coordinates contained in each
        ///     of the POINT structures will be transformed.
        /// </param>
        /// <param name="nCount">The number of points in the array.</param>
        /// <remarks>
        ///     The DPtoLP function fails if the device coordinates exceed 27 bits, or if the converted logical coordinates exceed
        ///     32 bits. In the case of such an overflow, the results for all the points are undefined.
        /// </remarks>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool DPtoLP(IntPtr hdc, [In] [Out] ref Point lpPoints, int nCount);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SelectClipPath(IntPtr hdc, RegionModeFlags iMode);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool FrameRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr, int nWidth,
            int nHeight);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool PaintRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool InvertRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool MoveToEx(IntPtr hdc, int x, int y, out Point lpPoint);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect, int nWidth, int nHeight);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool TextOut(IntPtr hdc, int nXStart, int nYStart,
            string lpString, int cbString);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight,
            IntPtr hdcSrc,
            int nXSrc, int nYSrc, BitBltFlags dwRop);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest,
            int nWidthDest, int nHeightDest,
            IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            BitBltFlags dwRop);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int SaveDC(IntPtr hdc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool RestoreDC(IntPtr hdc, int nSavedDc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr PathToRegion(IntPtr hdc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreatePolygonRgn(
            Point[] lppt,
            int cPoints,
            int fnPolyFillMode);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CreatePolyPolygonRgn(Point[] lppt,
            int[] lpPolyCounts,
            int nCount, int fnPolyFillMode);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport(LibraryName, ExactSpelling = true, EntryPoint = "GdiAlphaBlend")]
        public static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest,
            int nWidthDest, int nHeightDest,
            IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            BlendFunction blendFunction);

        /// <summary>
        ///     The GetRandomRgn function copies the system clipping region of a specified device context to a specific region.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="hrgn">
        ///     A handle to a region. Before the function is called, this identifies an existing region. After the
        ///     function returns, this identifies a copy of the current system region. The old region identified by hrgn is
        ///     overwritten.
        /// </param>
        /// <param name="iNum">This parameter must be SYSRGN.</param>
        /// <returns>
        ///     If the function succeeds, the return value is 1. If the function fails, the return value is -1. If the region
        ///     to be retrieved is NULL, the return value is 0. If the function fails or the region to be retrieved is NULL, hrgn
        ///     is not initialized.
        /// </returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int GetRandomRgn(IntPtr hdc, IntPtr hrgn, DcRegionType iNum);

        /// <summary>
        ///     The OffsetRgn function moves a region by the specified offsets.
        /// </summary>
        /// <param name="hrgn">Handle to the region to be moved.</param>
        /// <param name="nXOffset">Specifies the number of logical units to move left or right.</param>
        /// <param name="nYOffset">Specifies the number of logical units to move up or down.</param>
        /// <returns>The return value specifies the new region's complexity. </returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType OffsetRgn(IntPtr hrgn, int nXOffset, int nYOffset);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType GetRgnBox(IntPtr hWnd, out Rectangle lprc);
    }
}