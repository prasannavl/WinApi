using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace WinApi.Gdi32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BitmapInfoHeader
    {
        public uint Size;
        public int Width;
        public int Height;
        public ushort Planes;
        public ushort BitCount;
        public BitmapCompressionMode CompressionMode;
        public uint SizeImage;
        public int XPxPerMeter;
        public int YPxPerMeter;
        public uint ClrUsed;
        public uint ClrImportant;
    }

    public struct BitmapInfo
    {
        public BitmapInfoHeader Header;
        public RgbQuad[] Colors;

        public static NativeBitmapInfoHandle NativeAlloc(ref BitmapInfo bitmapInfo)
        {
            return new NativeBitmapInfoHandle(ref bitmapInfo);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RgbQuad
    {
        public byte Blue;
        public byte Green;
        public byte Red;
        private byte Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Bitmap
    {
        /// <summary>
        ///     The bitmap type. This member must be zero.
        /// </summary>
        public int Type;

        /// <summary>
        ///     The width, in pixels, of the bitmap. The width must be greater than zero.
        /// </summary>
        public int Width;

        /// <summary>
        ///     The height, in pixels, of the bitmap. The height must be greater than zero.
        /// </summary>
        public int Height;

        /// <summary>
        ///     The number of bytes in each scan line. This value must be divisible by 2, because the system assumes that the bit
        ///     values of a bitmap form an array that is word aligned.
        /// </summary>
        public int WidthBytes;

        /// <summary>
        ///     The count of color planes.
        /// </summary>
        public ushort Planes;

        /// <summary>
        ///     The number of bits required to indicate the color of a pixel.
        /// </summary>
        public ushort BitsPerPixel;

        /// <summary>
        ///     A pointer to the location of the bit values for the bitmap. The bmBits member must be a pointer to an array of
        ///     character (1-byte) values.
        /// </summary>
        public IntPtr Bits;
    }

    public enum BitmapInfoColorFormat
    {
        DIB_RGB_COLORS = 0 /* color table in RGBs */,
        DIB_PAL_COLORS = 1 /* color table in palette indices */
    }

    public enum BitmapCompressionMode
    {
        BI_RGB = 0,
        BI_RLE8 = 1,
        BI_RLE4 = 2,
        BI_BITFIELDS = 3,
        BI_JPEG = 4,
        BI_PNG = 5
    }

    public enum StockObject
    {
        /// <summary>
        ///     Black brush.
        /// </summary>
        BLACK_BRUSH = 4,

        /// <summary>
        ///     Dark gray brush.
        /// </summary>
        DKGRAY_BRUSH = 3,

        /// <summary>
        ///     Solid color brush. The default color is white. The color can be changed by using the SetDCBrushColor function. For
        ///     more information, see the Remarks section.
        /// </summary>
        DC_BRUSH = 18,

        /// <summary>
        ///     Gray brush.
        /// </summary>
        GRAY_BRUSH = 2,

        /// <summary>
        ///     Hollow brush (equivalent to NULL_BRUSH).
        /// </summary>
        HOLLOW_BRUSH = NULL_BRUSH,

        /// <summary>
        ///     Light gray brush.
        /// </summary>
        LTGRAY_BRUSH = 1,

        /// <summary>
        ///     Null brush (equivalent to HOLLOW_BRUSH).
        /// </summary>
        NULL_BRUSH = 5,

        /// <summary>
        ///     White brush.
        /// </summary>
        WHITE_BRUSH = 0,

        /// <summary>
        ///     Black pen.
        /// </summary>
        BLACK_PEN = 7,

        /// <summary>
        ///     Solid pen color. The default color is white. The color can be changed by using the SetDCPenColor function. For more
        ///     information, see the Remarks section.
        /// </summary>
        DC_PEN = 19,

        /// <summary>
        ///     Null pen. The null pen draws nothing.
        /// </summary>
        NULL_PEN = 8,

        /// <summary>
        ///     White pen.
        /// </summary>
        WHITE_PEN = 6,

        /// <summary>
        ///     Windows fixed-pitch (monospace) system font.
        /// </summary>
        ANSI_FIXED_FONT = 11,

        /// <summary>
        ///     Windows variable-pitch (proportional space) system font.
        /// </summary>
        ANSI_VAR_FONT = 12,

        /// <summary>
        ///     Device-dependent font.
        /// </summary>
        DEVICE_DEFAULT_FONT = 14,

        /// <summary>
        ///     Default font for user interface objects such as menus and dialog boxes. It is not recommended that you use
        ///     DEFAULT_GUI_FONT or SYSTEM_FONT to obtain the font used by dialogs and windows; for more information, see the
        ///     remarks section.
        ///     The default font is Tahoma.
        /// </summary>
        DEFAULT_GUI_FONT = 17,

        /// <summary>
        ///     Original equipment manufacturer (OEM) dependent fixed-pitch (monospace) font.
        /// </summary>
        OEM_FIXED_FONT = 10,

        /// <summary>
        ///     System font. By default, the system uses the system font to draw menus, dialog box controls, and text. It is not
        ///     recommended that you use DEFAULT_GUI_FONT or SYSTEM_FONT to obtain the font used by dialogs and windows; for more
        ///     information, see the remarks section.
        ///     The default system font is Tahoma.
        /// </summary>
        SYSTEM_FONT = 13,

        /// <summary>
        ///     Fixed-pitch (monospace) system font. This stock object is provided only for compatibility with 16-bit Windows
        ///     versions earlier than 3.0.
        /// </summary>
        SYSTEM_FIXED_FONT = 16,

        /// <summary>
        ///     Default palette. This palette consists of the static colors in the system palette.
        /// </summary>
        DEFAULT_PALETTE = 15
    }

    public enum DibBmiColorUsageFlag
    {
        DIB_RGB_COLORS = 0 /* color table in RGBs */,
        DIB_PAL_COLORS = 1 /* color table in palette indices */
    }

    public enum StockBrush
    {
        WHITE_BRUSH = 0,
        LTGRAY_BRUSH = 1,
        GRAY_BRUSH = 2,
        DKGRAY_BRUSH = 3,
        BLACK_BRUSH = 4,
        NULL_BRUSH = 5,
        HOLLOW_BRUSH = NULL_BRUSH
    }

    public enum StockPen
    {
        WHITE_PEN = 6,
        BLACK_PEN = 7,
        NULL_PEN = 8
    }

    public enum StockFont
    {
        OEM_FIXED_FONT = 10,
        ANSI_FIXED_FONT = 11,
        ANSI_VAR_FONT = 12,
        SYSTEM_FONT = 13,
        DEVICE_DEFAULT_FONT = 14,
        SYSTEM_FIXED_FONT = 16,
        DEFAULT_GUI_FONT = 17
    }

    public enum DcRegionType
    {
        Clip = 1,
        Meta = 2,
        IntersectedMetaClip = 3,
        System = 4
    }

    [Flags]
    public enum BitBltFlags
    {
        /// <summary>dest = source</summary>
        SRCCOPY = 0x00CC0020,

        /// <summary>dest = source OR dest</summary>
        SRCPAINT = 0x00EE0086,

        /// <summary>dest = source AND dest</summary>
        SRCAND = 0x008800C6,

        /// <summary>dest = source XOR dest</summary>
        SRCINVERT = 0x00660046,

        /// <summary>dest = source AND (NOT dest)</summary>
        SRCERASE = 0x00440328,

        /// <summary>dest = (NOT source)</summary>
        NOTSRCCOPY = 0x00330008,

        /// <summary>dest = (NOT src) AND (NOT dest)</summary>
        NOTSRCERASE = 0x001100A6,

        /// <summary>dest = (source AND pattern)</summary>
        MERGECOPY = 0x00C000CA,

        /// <summary>dest = (NOT source) OR dest</summary>
        MERGEPAINT = 0x00BB0226,

        /// <summary>dest = pattern</summary>
        PATCOPY = 0x00F00021,

        /// <summary>dest = DPSnoo</summary>
        PATPAINT = 0x00FB0A09,

        /// <summary>dest = pattern XOR dest</summary>
        PATINVERT = 0x005A0049,

        /// <summary>dest = (NOT dest)</summary>
        DSTINVERT = 0x00550009,

        /// <summary>dest = BLACK</summary>
        BLACKNESS = 0x00000042,

        /// <summary>dest = WHITE</summary>
        WHITENESS = 0x00FF0062,

        /// <summary>
        ///     Capture window as seen on screen.  This includes layered windows
        ///     such as WPF windows with AllowsTransparency="true"
        /// </summary>
        CAPTUREBLT = 0x40000000,

        /// <summary>
        ///     Prevents the bitmap from being mirrored.
        /// </summary>
        NOMIRRORBITMAP = unchecked((int) 0x80000000)
    }
}