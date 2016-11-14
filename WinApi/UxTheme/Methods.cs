using NetCoreEx.Geometry;
using System;
using System.Runtime.InteropServices;
using WinApi.Core;
using WinApi.User32;

namespace WinApi.UxTheme
{
    public static class UxThemeMethods
    {
        public const string LibraryName = "uxtheme";

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult SetWindowThemeAttribute(
            IntPtr hwnd,
            WindowThemeAttributeType eAttributeType,
            IntPtr pvAttribute,
            uint size);

        /// <summary>
        ///     Opens the theme data for a window and its associated class.
        /// </summary>
        /// <param name="hwnd">Handle of the window for which theme data is required.</param>
        /// <param name="lpszClassList">Pointer to a string that contains a semicolon-separated list of classes.</param>
        /// <returns>
        ///     Type: HTHEME
        ///     OpenThemeData tries to match each class, one at a time, to a class data section in the active theme.If a match is
        ///     found, an associated HTHEME handle is returned.If no match is found NULL is returned.
        /// </returns>
        /// <remarks>
        ///     The pszClassList parameter contains a list, not just a single name, to provide the class an opportunity to get the
        ///     best match between the class and the current visual style. For example, a button might pass L"OkButton;Button" if
        ///     its ID is ID_OK. If the current visual style has an entry for OkButton, that is used; otherwise no visual style is
        ///     applied.
        ///     Class names for the Aero theme are defined in AeroStyle.xml.
        /// </remarks>
        [DllImport(LibraryName, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenThemeData(IntPtr hwnd, [In] string lpszClassList);

        [DllImport(LibraryName, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenThemeDataEx(IntPtr hwnd, [In] string lpszClassList, OpenThemeDataFlags dwFlags);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult CloseThemeData(IntPtr hTheme);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IsAppThemed();

        /// <summary>
        ///     Causes a window to use a different set of visual style information than its class normally uses.
        /// </summary>
        /// <param name="hwnd">Handle to the window whose visual style information is to be changed.</param>
        /// <param name="pszSubAppName">
        ///     Pointer to a string that contains the application name to use in place of the calling
        ///     application's name. If this parameter is NULL, the calling application's name is used.
        /// </param>
        /// <param name="pszSubIdList">
        ///     Pointer to a string that contains a semicolon-separated list of CLSID names to use in place
        ///     of the actual list passed by the window's class. If this parameter is NULL, the ID list from the calling class is
        ///     used.
        /// </param>
        /// <remarks>
        ///     The theme manager retains the pszSubAppName and the pszSubIdList associations through the lifetime of the window,
        ///     even if visual styles subsequently change. The window is sent a WM_THEMECHANGED message at the end of a
        ///     SetWindowTheme call, so that the new visual style can be found and applied.
        ///     When pszSubAppName and pszSubIdList are NULL, the theme manager removes the previously applied associations.You can
        ///     prevent visual styles from being applied to a specified window by specifying an empty string, (L" "), which does
        ///     not match any section entries.
        /// </remarks>
        [DllImport(LibraryName, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern HResult SetWindowTheme(IntPtr hwnd, [In] string pszSubAppName, [In] string pszSubIdList);

        /// <summary>
        ///     Draws the part of a parent control that is covered by a partially-transparent or alpha-blended child control.
        /// </summary>
        /// <param name="hwnd">The child control.</param>
        /// <param name="hdc">The child control's DC.</param>
        /// <param name="prc">
        ///     The area to be drawn. The rectangle is in the child window's coordinates. If this parameter is NULL,
        ///     the area to be drawn includes the entire area occupied by the child control.
        /// </param>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult DrawThemeParentBackground(
            IntPtr hwnd,
            IntPtr hdc,
            [In] ref Rectangle prc);

        /// <summary>
        ///     Retrieves whether the background specified by the visual style has transparent pieces or alpha-blended pieces.
        /// </summary>
        /// <param name="hTheme">Handle to a window's specified theme data. Use OpenThemeData to create an HTHEME.</param>
        /// <param name="iPartId">Value of type int that specifies the part. See Parts and States.</param>
        /// <param name="iStateId">Value of type int that specifies the state of the part. See Parts and States.</param>
        /// <returns>
        ///     Returns one of the following values.
        ///     TRUE - The theme-specified background for a particular iPartId and iStateId has transparent pieces or alpha-blended
        ///     pieces.
        ///     FALSE - The theme-specified background for a particular iPartId and iStateId does not have transparent pieces or
        ///     alpha-blended pieces.
        /// </returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IsThemeBackgroundPartiallyTransparent(
            IntPtr hTheme,
            int iPartId,
            int iStateId);


        /// <summary>
        ///     Draws the border and fill defined by the visual style for the specified control part.
        /// </summary>
        /// <param name="hTheme">Handle to a window's specified theme data. Use OpenThemeData to create an HTHEME.</param>
        /// <param name="hdc">HDC used for drawing the theme-defined background image.</param>
        /// <param name="iPartId">Value of type int that specifies the part to draw. See Parts and States.</param>
        /// <param name="iStateId">Value of type int that specifies the state of the part to draw. See Parts and States.</param>
        /// <param name="pRect">
        ///     Pointer to a RECT structure that contains the rectangle, in logical coordinates, in which the
        ///     background image is drawn.
        /// </param>
        /// <param name="pClipRect">
        ///     Pointer to a RECT structure that contains a clipping rectangle. This parameter may be set to
        ///     NULL.
        /// </param>
        /// <remarks>
        ///     Drawing operations are scaled to fit and not exceed the rectangle specified in pRect. Your application should
        ///     not draw outside the rectangle specified by pClipRect.
        /// </remarks>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult DrawThemeBackground(
            IntPtr hTheme,
            IntPtr hdc,
            int iPartId,
            int iStateId,
            [In] ref Rectangle pRect,
            [In] ref Rectangle pClipRect
        );

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult DrawThemeBackground(
            IntPtr hTheme,
            IntPtr hdc,
            int iPartId,
            int iStateId,
            [In] ref Rectangle pRect,
            IntPtr pClipRect
        );

        /// <summary>
        ///     Draws the background image defined by the visual style for the specified control part.
        /// </summary>
        /// <param name="hTheme">Handle to a window's specified theme data. Use OpenThemeData to create an HTHEME.</param>
        /// <param name="hdc">HDC used for drawing the theme-defined background image.</param>
        /// <param name="iPartId">Value of type int that specifies the part to draw. See Parts and States.</param>
        /// <param name="iStateId">Value of type int that specifies the state of the part to draw. See Parts and States.</param>
        /// <param name="pRect">
        ///     Pointer to a RECT structure that contains the rectangle, in logical coordinates, in which the
        ///     background image is drawn.
        /// </param>
        /// <param name="pOptions">
        ///     Pointer to a DTBGOPTS structure that contains clipping information. This parameter may be set to
        ///     NULL.
        /// </param>
        /// <returns></returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult DrawThemeBackgroundEx(
            IntPtr hTheme,
            IntPtr hdc,
            int iPartId,
            int iStateId,
            [In] ref Rectangle pRect,
            [In] ref DrawThemeBackgroundOptions pOptions);

        /// <summary>
        ///     Retrieves the size of the content area for the background defined by the visual style.
        /// </summary>
        /// <param name="hTheme">Handle to a window's specified theme data. Use OpenThemeData to create an HTHEME.</param>
        /// <param name="hdc">HDC to use when drawing. This parameter may be set to NULL.</param>
        /// <param name="iPartId">Value of type int that specifies the part that contains the content area. See Parts and States.</param>
        /// <param name="iStateId">
        ///     Value of type int that specifies the state of the part that contains the content area. See Parts
        ///     and States.
        /// </param>
        /// <param name="pBoundingRect">
        ///     Pointer to a RECT structure that contains the total background rectangle, in logical
        ///     coordinates. This is the area inside the borders or margins.
        /// </param>
        /// <param name="pContentRect">
        ///     Pointer to a RECT structure that receives the content area background rectangle, in logical
        ///     coordinates. This rectangle is calculated to fit the content area.
        /// </param>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult GetThemeBackgroundContentRect(
            IntPtr hTheme,
            IntPtr hdc,
            int iPartId,
            int iStateId,
            [In] ref Rectangle pBoundingRect,
            out Rectangle pContentRect
        );


        /// <summary>
        ///     Retrieves the value of a color property.
        /// </summary>
        /// <param name="hTheme">Handle to a window's specified theme data. Use OpenThemeData to create an HTHEME.</param>
        /// <param name="iPartId">Value of type int that specifies the part that contains the content area. See Parts and States.</param>
        /// <param name="iStateId">
        ///     Value of type int that specifies the state of the part that contains the content area. See Parts
        ///     and States.
        /// </param>
        /// <param name="iPropId">
        ///     Value of type int that specifies the property to retrieve. For a list of possible values, see
        ///     Property Identifiers.
        /// </param>
        /// <param name="pColor">Pointer to a COLORREF structure that receives the color value.</param>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern HResult GetThemeColor(
            IntPtr hTheme,
            int iPartId,
            int iStateId,
            int iPropId,
            out uint pColor);
    }
}