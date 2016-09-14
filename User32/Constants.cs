using System;

// ReSharper disable InconsistentNaming

namespace WinApi.User32
{

    #region Multi-flag constants

    [Flags]
    public enum WindowStyles
    {
        /// <summary>
        ///     The window has a thin-line border.
        /// </summary>
        WS_BORDER = 0x00800000,

        /// <summary>
        ///     The window has a title bar (includes the WS_BORDER style).
        /// </summary>
        WS_CAPTION = 0x00C00000,

        /// <summary>
        ///     The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the
        ///     WS_POPUP style.
        /// </summary>
        WS_CHILD = 0x40000000,

        /// <summary>
        ///     Same as the WS_CHILD style.
        /// </summary>
        WS_CHILDWINDOW = 0x40000000,

        /// <summary>
        ///     Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when
        ///     creating the parent window.
        /// </summary>
        WS_CLIPCHILDREN = 0x02000000,

        /// <summary>
        ///     Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message,
        ///     the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be
        ///     updated. If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the
        ///     client area of a child window, to draw within the client area of a neighboring child window.
        /// </summary>
        WS_CLIPSIBLINGS = 0x04000000,

        /// <summary>
        ///     The window is initially disabled. A disabled window cannot receive input from the user. To change this after a
        ///     window has been created, use the EnableWindow function.
        /// </summary>
        WS_DISABLED = 0x08000000,

        /// <summary>
        ///     The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title
        ///     bar.
        /// </summary>
        WS_DLGFRAME = 0x00400000,

        /// <summary>
        ///     The window is the first control of a group of controls. The group consists of this first control and all controls
        ///     defined after it, up to the next control with the WS_GROUP style. The first control in each group usually has the
        ///     WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus
        ///     from one control in the group to the next control in the group by using the direction keys.
        ///     You can turn this style on and off to change dialog box navigation. To change this style after a window has been
        ///     created, use the SetWindowLong function.
        /// </summary>
        WS_GROUP = 0x00020000,

        /// <summary>
        ///     The window has a horizontal scroll bar.
        /// </summary>
        WS_HSCROLL = 0x00100000,

        /// <summary>
        ///     The window is initially minimized. Same as the WS_MINIMIZE style.
        /// </summary>
        WS_ICONIC = 0x20000000,

        /// <summary>
        ///     The window is initially maximized.
        /// </summary>
        WS_MAXIMIZE = 0x01000000,

        /// <summary>
        ///     The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must
        ///     also be specified.
        /// </summary>
        WS_MAXIMIZEBOX = 0x00010000,

        /// <summary>
        ///     The window is initially minimized. Same as the WS_ICONIC style.
        /// </summary>
        WS_MINIMIZE = 0x20000000,

        /// <summary>
        ///     The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must
        ///     also be specified.
        /// </summary>
        WS_MINIMIZEBOX = 0x00020000,

        /// <summary>
        ///     The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_TILED style.
        /// </summary>
        WS_OVERLAPPED = 0x00000000,

        /// <summary>
        ///     The window is an overlapped window. Same as the WS_TILEDWINDOW style.
        /// </summary>
        WS_OVERLAPPEDWINDOW =
            WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

        /// <summary>
        ///     The windows is a pop-up window. This style cannot be used with the WS_CHILD style.
        /// </summary>
        WS_POPUP = unchecked ((int) 0x80000000),

        /// <summary>
        ///     The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu
        ///     visible.
        /// </summary>
        WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

        /// <summary>
        ///     The window has a sizing border. Same as the WS_THICKFRAME style.
        /// </summary>
        WS_SIZEBOX = 0x00040000,

        /// <summary>
        ///     The window has a window menu on its title bar. The WS_CAPTION style must also be specified.
        /// </summary>
        WS_SYSMENU = 0x00080000,

        /// <summary>
        ///     The window is a control that can receive the keyboard focus when the user presses the TAB key. Pressing the TAB key
        ///     changes the keyboard focus to the next control with the WS_TABSTOP style.
        ///     You can turn this style on and off to change dialog box navigation. To change this style after a window has been
        ///     created, use the SetWindowLong function. For user-created windows and modeless dialogs to work with tab stops,
        ///     alter the message loop to call the IsDialogMessage function.
        /// </summary>
        WS_TABSTOP = 0x00010000,

        /// <summary>
        ///     The window has a sizing border. Same as the WS_SIZEBOX style.
        /// </summary>
        WS_THICKFRAME = 0x00040000,

        /// <summary>
        ///     The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_OVERLAPPED
        ///     style.
        /// </summary>
        WS_TILED = 0x00000000,

        /// <summary>
        ///     The window is  an overlapped window. Same as the WS_OVERLAPPEDWINDOW style.
        /// </summary>
        WS_TILEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

        /// <summary>
        ///     The window is initially visible.
        ///     This style can be turned on and off by using the ShowWindow or SetWindowPos function.
        /// </summary>
        WS_VISIBLE = 0x10000000,

        /// <summary>
        ///     The window has a vertical scroll bar.
        /// </summary>
        WS_VSCROLL = 0x00200000
    }

    [Flags]
    public enum WindowExStyles
    {
        /// <summary>
        ///     The window accepts drag-drop files.
        /// </summary>
        WS_EX_ACCEPTFILES = 0x00000010,

        /// <summary>
        ///     Forces a top-level window onto the taskbar when the window is visible.
        /// </summary>
        WS_EX_APPWINDOW = 0x00040000,

        /// <summary>
        ///     The window has a border with a sunken edge.
        /// </summary>
        WS_EX_CLIENTEDGE = 0x00000200,

        /// <summary>
        ///     Paints all descendants of a window in bottom-to-top painting order using double-buffering. For more information,
        ///     see Remarks. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
        ///     Windows 2000:  This style is not supported.
        /// </summary>
        WS_EX_COMPOSITED = 0x02000000,

        /// <summary>
        ///     The title bar of the window includes a question mark. When the user clicks the question mark, the cursor changes to
        ///     a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message. The
        ///     child window should pass the message to the parent window procedure, which should call the WinHelp function using
        ///     the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child
        ///     window.
        ///     WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
        /// </summary>
        WS_EX_CONTEXTHELP = 0x00000400,

        /// <summary>
        ///     The window itself contains child windows that should take part in dialog box navigation. If this style is
        ///     specified, the dialog manager recurses into children of this window when performing navigation operations such as
        ///     handling the TAB key, an arrow key, or a keyboard mnemonic.
        /// </summary>
        WS_EX_CONTROLPARENT = 0x00010000,

        /// <summary>
        ///     The window has a double border; the window can, optionally, be created with a title bar by specifying the
        ///     WS_CAPTION style in the dwStyle parameter.
        /// </summary>
        WS_EX_DLGMODALFRAME = 0x00000001,

        /// <summary>
        ///     The window is a layered window. This style cannot be used if the window has a class style of either CS_OWNDC or
        ///     CS_CLASSDC.
        ///     Windows 8:  The WS_EX_LAYERED style is supported for top-level windows and child windows. Previous Windows versions
        ///     support WS_EX_LAYERED only for top-level windows.
        /// </summary>
        WS_EX_LAYERED = 0x00080000,

        /// <summary>
        ///     If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the horizontal
        ///     origin of the window is on the right edge. Increasing horizontal values advance to the left.
        /// </summary>
        WS_EX_LAYOUTRTL = 0x00400000,

        /// <summary>
        ///     The window has generic left-aligned properties. This is the default.
        /// </summary>
        WS_EX_LEFT = 0x00000000,

        /// <summary>
        ///     If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical
        ///     scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.
        /// </summary>
        WS_EX_LEFTSCROLLBAR = 0x00004000,

        /// <summary>
        ///     The window text is displayed using left-to-right reading-order properties. This is the default.
        /// </summary>
        WS_EX_LTRREADING = 0x00000000,

        /// <summary>
        ///     The window is a MDI child window.
        /// </summary>
        WS_EX_MDICHILD = 0x00000040,

        /// <summary>
        ///     A top-level window created with this style does not become the foreground window when the user clicks it. The
        ///     system does not bring this window to the foreground when the user minimizes or closes the foreground window.
        ///     To activate the window, use the SetActiveWindow or SetForegroundWindow function.
        ///     The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the
        ///     WS_EX_APPWINDOW style.
        /// </summary>
        WS_EX_NOACTIVATE = 0x08000000,

        /// <summary>
        ///     The window does not pass its window layout to its child windows.
        /// </summary>
        WS_EX_NOINHERITLAYOUT = 0x00100000,

        /// <summary>
        ///     The child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is
        ///     created or destroyed.
        /// </summary>
        WS_EX_NOPARENTNOTIFY = 0x00000004,

        /// <summary>
        ///     The window does not render to a redirection surface. This is for windows that do not have visible content or that
        ///     use mechanisms other than surfaces to provide their visual.
        /// </summary>
        WS_EX_NOREDIRECTIONBITMAP = 0x00200000,

        /// <summary>
        ///     The window is an overlapped window.
        /// </summary>
        WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,

        /// <summary>
        ///     The window is palette window, which is a modeless dialog box that presents an array of commands.
        /// </summary>
        WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,

        /// <summary>
        ///     The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only
        ///     if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the
        ///     style is ignored.
        ///     Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT
        ///     style, respectively. Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON
        ///     styles.
        /// </summary>
        WS_EX_RIGHT = 0x00001000,

        /// <summary>
        ///     The vertical scroll bar (if present) is to the right of the client area. This is the default.
        /// </summary>
        WS_EX_RIGHTSCROLLBAR = 0x00000000,

        /// <summary>
        ///     If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text
        ///     is displayed using right-to-left reading-order properties. For other languages, the style is ignored.
        /// </summary>
        WS_EX_RTLREADING = 0x00002000,

        /// <summary>
        ///     The window has a three-dimensional border style intended to be used for items that do not accept user input.
        /// </summary>
        WS_EX_STATICEDGE = 0x00020000,

        /// <summary>
        ///     The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a
        ///     normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar
        ///     or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not
        ///     displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE.
        /// </summary>
        WS_EX_TOOLWINDOW = 0x00000080,

        /// <summary>
        ///     The window should be placed above all non-topmost windows and should stay above them, even when the window is
        ///     deactivated. To add or remove this style, use the SetWindowPos function.
        /// </summary>
        WS_EX_TOPMOST = 0x00000008,

        /// <summary>
        ///     The window should not be painted until siblings beneath the window (that were created by the same thread) have been
        ///     painted. The window appears transparent because the bits of underlying sibling windows have already been painted.
        ///     To achieve transparency without these restrictions, use the  SetWindowRgn function.
        /// </summary>
        WS_EX_TRANSPARENT = 0x00000020,

        /// <summary>
        ///     The window has a border with a raised edge.
        /// </summary>
        WS_EX_WINDOWEDGE = 0x00000100
    }

    [Flags]
    public enum WindowClassStyles
    {
        /// <summary>
        ///     Aligns the window's client area on a byte boundary (in the x direction). This style affects the width of the window
        ///     and its horizontal placement on the display.
        /// </summary>
        CS_BYTEALIGNCLIENT = 0x1000,

        /// <summary>
        ///     Aligns the window on a byte boundary (in the x direction). This style affects the width of the window and its
        ///     horizontal placement on the display.
        /// </summary>
        CS_BYTEALIGNWINDOW = 0x2000,

        /// <summary>
        ///     Allocates one device context to be shared by all windows in the class. Because window classes are process specific,
        ///     it is possible for multiple threads of an application to create a window of the same class. It is also possible for
        ///     the threads to attempt to use the device context simultaneously. When this happens, the system allows only one
        ///     thread to successfully finish its drawing operation.
        /// </summary>
        CS_CLASSDC = 0x0040,

        /// <summary>
        ///     Sends a double-click message to the window procedure when the user double-clicks the mouse while the cursor is
        ///     within a window belonging to the class.
        /// </summary>
        CS_DBLCLKS = 0x0008,

        /// <summary>
        ///     Enables the drop shadow effect on a window. The effect is turned on and off through SPI_SETDROPSHADOW. Typically,
        ///     this is enabled for small, short-lived windows such as menus to emphasize their Z-order relationship to other
        ///     windows. Windows created from a class with this style must be top-level windows; they may not be child windows.
        /// </summary>
        CS_DROPSHADOW = 0x00020000,

        /// <summary>
        ///     Indicates that the window class is an application global class. For more information, see the "Application Global
        ///     Classes" section of About Window Classes.
        /// </summary>
        CS_GLOBALCLASS = 0x4000,

        /// <summary>
        ///     Redraws the entire window if a movement or size adjustment changes the width of the client area.
        /// </summary>
        CS_HREDRAW = 0x0002,

        /// <summary>
        ///     Disables Close on the window menu.
        /// </summary>
        CS_NOCLOSE = 0x0200,

        /// <summary>
        ///     Allocates a unique device context for each window in the class.
        /// </summary>
        CS_OWNDC = 0x0020,

        /// <summary>
        ///     Sets the clipping rectangle of the child window to that of the parent window so that the child can draw on the
        ///     parent. A window with the CS_PARENTDC style bit receives a regular device context from the system's cache of device
        ///     contexts. It does not give the child the parent's device context or device context settings. Specifying CS_PARENTDC
        ///     enhances an application's performance.
        /// </summary>
        CS_PARENTDC = 0x0080,

        /// <summary>
        ///     Saves, as a bitmap, the portion of the screen image obscured by a window of this class. When the window is removed,
        ///     the system uses the saved bitmap to restore the screen image, including other windows that were obscured.
        ///     Therefore, the system does not send  WM_PAINT messages to windows that were obscured if the memory used by the
        ///     bitmap has not been discarded and if other screen actions have not invalidated the stored image.
        ///     This style is useful for small windows (for example, menus or dialog boxes) that are displayed briefly and then
        ///     removed before other screen activity takes place. This style increases the time required to display the window,
        ///     because the system must first allocate memory to store the bitmap.
        /// </summary>
        CS_SAVEBITS = 0x0800,

        /// <summary>
        ///     Redraws the entire window if a movement or size adjustment changes the height of the client area.
        /// </summary>
        CS_VREDRAW = 0x0001
    }

    [Flags]
    public enum AnimateWindowFlags
    {
        /// <summary>
        ///     Activates the window. Do not use this value with AW_HIDE.
        /// </summary>
        AW_ACTIVATE = 0x00020000,

        /// <summary>
        ///     Uses a fade effect. This flag can be used only if hwnd is a top-level window.
        /// </summary>
        AW_BLEND = 0x00080000,

        /// <summary>
        ///     Makes the window appear to collapse inward if AW_HIDE is used or expand outward if the AW_HIDE is not used. The
        ///     various direction flags have no effect.
        /// </summary>
        AW_CENTER = 0x00000010,

        /// <summary>
        ///     Hides the window. By default, the window is shown.
        /// </summary>
        AW_HIDE = 0x00010000,

        /// <summary>
        ///     Animates the window from left to right. This flag can be used with roll or slide animation. It is ignored when used
        ///     with AW_CENTER or AW_BLEND.
        /// </summary>
        AW_HOR_POSITIVE = 0x00000001,

        /// <summary>
        ///     Animates the window from right to left. This flag can be used with roll or slide animation. It is ignored when used
        ///     with AW_CENTER or AW_BLEND.
        /// </summary>
        AW_HOR_NEGATIVE = 0x00000002,

        /// <summary>
        ///     Uses slide animation. By default, roll animation is used. This flag is ignored when used with AW_CENTER.
        /// </summary>
        AW_SLIDE = 0x00040000,

        /// <summary>
        ///     Animates the window from top to bottom. This flag can be used with roll or slide animation. It is ignored when used
        ///     with AW_CENTER or AW_BLEND.
        /// </summary>
        AW_VER_POSITIVE = 0x00000004,

        /// <summary>
        ///     Animates the window from bottom to top. This flag can be used with roll or slide animation. It is ignored when used
        ///     with AW_CENTER or AW_BLEND.
        /// </summary>
        AW_VER_NEGATIVE = 0x00000008
    }

    [Flags]
    public enum DrawTextFormatFlags
    {
        /// <summary>
        ///     Justifies the text to the bottom of the rectangle. This value is used only with the DT_SINGLELINE value.
        /// </summary>
        DT_BOTTOM = 0x00000008,

        /// <summary>
        ///     Determines the width and height of the rectangle. If there are multiple lines of text, DrawText uses the width of
        ///     the rectangle pointed to by the lpRect parameter and extends the base of the rectangle to bound the last line of
        ///     text. If the largest word is wider than the rectangle, the width is expanded. If the text is less than the width of
        ///     the rectangle, the width is reduced. If there is only one line of text, DrawText modifies the right side of the
        ///     rectangle so that it bounds the last character in the line. In either case, DrawText returns the height of the
        ///     formatted text but does not draw the text.
        /// </summary>
        DT_CALCRECT = 0x00000400,

        /// <summary>
        ///     Centers text horizontally in the rectangle.
        /// </summary>
        DT_CENTER = 0x00000001,

        /// <summary>
        ///     Duplicates the text-displaying characteristics of a multiline edit control. Specifically, the average character
        ///     width is calculated in the same manner as for an edit control, and the function does not display a partially
        ///     visible last line.
        /// </summary>
        DT_EDITCONTROL = 0x00002000,

        /// <summary>
        ///     For displayed text, if the end of a string does not fit in the rectangle, it is truncated and ellipses are added.
        ///     If a word that is not at the end of the string goes beyond the limits of the rectangle, it is truncated without
        ///     ellipses.
        ///     The string is not modified unless the DT_MODIFYSTRING flag is specified.
        ///     Compare with DT_PATH_ELLIPSIS and DT_WORD_ELLIPSIS.
        /// </summary>
        DT_END_ELLIPSIS = 0x00008000,

        /// <summary>
        ///     Expands tab characters. The default number of characters per tab is eight. The DT_WORD_ELLIPSIS, DT_PATH_ELLIPSIS,
        ///     and DT_END_ELLIPSIS values cannot be used with the DT_EXPANDTABS value.
        /// </summary>
        DT_EXPANDTABS = 0x00000040,

        /// <summary>
        ///     Includes the font external leading in line height. Normally, external leading is not included in the height of a
        ///     line of text.
        /// </summary>
        DT_EXTERNALLEADING = 0x00000200,

        /// <summary>
        ///     Ignores the ampersand (&) prefix character in the text. The letter that follows will not be underlined, but other
        ///     mnemonic-prefix characters are still processed.
        ///     Example:
        ///     input string: "A&bc&&d"
        ///     normal: "Abc&d"
        ///     DT_HIDEPREFIX: "Abc&d"
        ///     Compare with DT_NOPREFIX and DT_PREFIXONLY.
        /// </summary>
        DT_HIDEPREFIX = 0x00100000,

        /// <summary>
        ///     Uses the system font to calculate text metrics.
        /// </summary>
        DT_INTERNAL = 0x00001000,

        /// <summary>
        ///     Aligns text to the left.
        /// </summary>
        DT_LEFT = 0x00000000,

        /// <summary>
        ///     Modifies the specified string to match the displayed text. This value has no effect unless DT_END_ELLIPSIS or
        ///     DT_PATH_ELLIPSIS is specified.
        /// </summary>
        DT_MODIFYSTRING = 0x00010000,

        /// <summary>
        ///     Draws without clipping. DrawText is somewhat faster when DT_NOCLIP is used.
        /// </summary>
        DT_NOCLIP = 0x00000100,

        /// <summary>
        ///     Prevents a line break at a DBCS (double-wide character string), so that the line breaking rule is equivalent to
        ///     SBCS strings. For example, this can be used in Korean windows, for more readability of icon labels. This value has
        ///     no effect unless DT_WORDBREAK is specified.
        /// </summary>
        DT_NOFULLWIDTHCHARBREAK = 0x00080000,

        /// <summary>
        ///     Turns off processing of prefix characters. Normally, DrawText interprets the mnemonic-prefix character & as a
        ///     directive to underscore the character that follows, and the mnemonic-prefix characters && as a directive to print a
        ///     single &. By specifying DT_NOPREFIX, this processing is turned off. For example,
        ///     Example:
        ///     input string: "A&bc&&d"
        ///     normal: "Abc&d"
        ///     DT_NOPREFIX: "A&bc&&d"
        ///     Compare with DT_HIDEPREFIX and DT_PREFIXONLY.
        /// </summary>
        DT_NOPREFIX = 0x00000800,

        /// <summary>
        ///     For displayed text, replaces characters in the middle of the string with ellipses so that the result fits in the
        ///     specified rectangle. If the string contains backslash (\) characters, DT_PATH_ELLIPSIS preserves as much as
        ///     possible of the text after the last backslash.
        ///     The string is not modified unless the DT_MODIFYSTRING flag is specified.
        ///     Compare with DT_END_ELLIPSIS and DT_WORD_ELLIPSIS.
        /// </summary>
        DT_PATH_ELLIPSIS = 0x00004000,

        /// <summary>
        ///     Draws only an underline at the position of the character following the ampersand (&) prefix character. Does not
        ///     draw any other characters in the string. For example,
        ///     Example:
        ///     input string: "A&bc&&d"n
        ///     normal: "Abc&d"
        ///     DT_PREFIXONLY: " _    "
        ///     Compare with DT_HIDEPREFIX and DT_NOPREFIX.
        /// </summary>
        DT_PREFIXONLY = 0x00200000,

        /// <summary>
        ///     Aligns text to the right.
        /// </summary>
        DT_RIGHT = 0x00000002,

        /// <summary>
        ///     Layout in right-to-left reading order for bidirectional text when the font selected into the hdc is a Hebrew or
        ///     Arabic font. The default reading order for all text is left-to-right.
        /// </summary>
        DT_RTLREADING = 0x00020000,

        /// <summary>
        ///     Displays text on a single line only. Carriage returns and line feeds do not break the line.
        /// </summary>
        DT_SINGLELINE = 0x00000020,

        /// <summary>
        ///     Sets tab stops. Bits 15-8 (high-order byte of the low-order word) of the uFormat parameter specify the number of
        ///     characters for each tab. The default number of characters per tab is eight. The DT_CALCRECT, DT_EXTERNALLEADING,
        ///     DT_INTERNAL, DT_NOCLIP, and DT_NOPREFIX values cannot be used with the DT_TABSTOP value.
        /// </summary>
        DT_TABSTOP = 0x00000080,

        /// <summary>
        ///     Justifies the text to the top of the rectangle.
        /// </summary>
        DT_TOP = 0x00000000,

        /// <summary>
        ///     Centers text vertically. This value is used only with the DT_SINGLELINE value.
        /// </summary>
        DT_VCENTER = 0x00000004,

        /// <summary>
        ///     Breaks words. Lines are automatically broken between words if a word would extend past the edge of the rectangle
        ///     specified by the lpRect parameter. A carriage return-line feed sequence also breaks the line.
        ///     If this is not specified, output is on one line.
        /// </summary>
        DT_WORDBREAK = 0x00000010,

        /// <summary>
        ///     Truncates any word that does not fit in the rectangle and adds ellipses.
        ///     Compare with DT_END_ELLIPSIS and DT_PATH_ELLIPSIS.
        /// </summary>
        DT_WORD_ELLIPSIS = 0x00040000
    }

    [Flags]
    public enum CreateWindowFlags
    {
        /// <summary>
        ///     Use default values
        /// </summary>
        CW_USEDEFAULT = unchecked((int) 0x80000000)
    }

    [Flags]
    public enum CombineRgnStyles
    {
        RGN_AND = 1,
        RGN_OR = 2,
        RGN_XOR = 3,
        RGN_DIFF = 4,
        RGN_COPY = 5,
        RGN_MIN = RGN_AND,
        RGN_MAX = RGN_COPY
    }

    [Flags]
    public enum DeviceCapability
    {
        DC_ACTIVE = 0x0001,
        DC_SMALLCAP = 0x0002,
        DC_ICON = 0x0004,
        DC_TEXT = 0x0008,
        DC_INBUTTON = 0x0010,
        DC_GRADIENT = 0x0020,
        DC_BUTTONS = 0x1000,
        DC_HASDEFID = 0x534B,
        DC_BRUSH = 18,
        DC_PEN = 19,
        DC_FIELDS = 1,
        DC_PAPERS = 2,
        DC_PAPERSIZE = 3,
        DC_MINEXTENT = 4,
        DC_MAXEXTENT = 5,
        DC_BINS = 6,
        DC_DUPLEX = 7,
        DC_SIZE = 8,
        DC_EXTRA = 9,
        DC_VERSION = 10,
        DC_DRIVER = 11,
        DC_BINNAMES = 12,
        DC_ENUMRESOLUTIONS = 13,
        DC_FILEDEPENDENCIES = 14,
        DC_TRUETYPE = 15,
        DC_PAPERNAMES = 16,
        DC_ORIENTATION = 17,
        DC_COPIES = 18,
        DC_BINADJUST = 19,
        DC_EMF_COMPLIANT = 20,
        DC_DATATYPE_PRODUCED = 21,
        DC_COLLATE = 22,
        DC_MANUFACTURER = 23,
        DC_MODEL = 24,
        DC_PERSONALITY = 25,
        DC_PRINTRATE = 26,
        DC_PRINTRATEUNIT = 27,
        DC_PRINTERMEM = 28,
        DC_MEDIAREADY = 29,
        DC_STAPLE = 30,
        DC_PRINTRATEPPM = 31,
        DC_COLORDEVICE = 32,
        DC_NUP = 33,
        DC_MEDIATYPENAMES = 34,
        DC_MEDIATYPES = 35
    }

    [Flags]
    public enum ShowWindowCommands
    {
        /// <summary>
        ///     Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when
        ///     minimizing windows from a different thread.
        /// </summary>
        SW_FORCEMINIMIZE = 11,

        /// <summary>
        ///     Hides the window and activates another window.
        /// </summary>
        SW_HIDE = 0,

        /// <summary>
        ///     Maximizes the specified window.
        /// </summary>
        SW_MAXIMIZE = 3,

        /// <summary>
        ///     Minimizes the specified window and activates the next top-level window in the Z order.
        /// </summary>
        SW_MINIMIZE = 6,

        /// <summary>
        ///     Activates and displays the window. If the window is minimized or maximized, the system restores it to its original
        ///     size and position. An application should specify this flag when restoring a minimized window.
        /// </summary>
        SW_RESTORE = 9,

        /// <summary>
        ///     Activates the window and displays it in its current size and position.
        /// </summary>
        SW_SHOW = 5,

        /// <summary>
        ///     Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess
        ///     function by the program that started the application.
        /// </summary>
        SW_SHOWDEFAULT = 10,

        /// <summary>
        ///     Activates the window and displays it as a maximized window.
        /// </summary>
        SW_SHOWMAXIMIZED = 3,

        /// <summary>
        ///     Activates the window and displays it as a minimized window.
        /// </summary>
        SW_SHOWMINIMIZED = 2,

        /// <summary>
        ///     Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not
        ///     activated.
        /// </summary>
        SW_SHOWMINNOACTIVE = 7,

        /// <summary>
        ///     Displays the window in its current size and position. This value is similar to SW_SHOW, except that the window is
        ///     not activated.
        /// </summary>
        SW_SHOWNA = 8,

        /// <summary>
        ///     Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the
        ///     window is not activated.
        /// </summary>
        SW_SHOWNOACTIVATE = 4,

        /// <summary>
        ///     Activates and displays a window. If the window is minimized or maximized, the system restores it to its original
        ///     size and position. An application should specify this flag when displaying the window for the first time.
        /// </summary>
        SW_SHOWNORMAL = 1
    }

    [Flags]
    public enum WindowLongFlags
    {
        /// <summary>
        ///     Retrieves the extended window styles.
        /// </summary>
        GWL_EXSTYLE = -20,

        /// <summary>
        ///     Retrieves a handle to the application instance.
        /// </summary>
        GWLP_HINSTANCE = -6,

        /// <summary>
        ///     Retrieves a handle to the parent window, if there is one.
        /// </summary>
        GWLP_HWNDPARENT = -8,

        /// <summary>
        ///     Retrieves the identifier of the window.
        /// </summary>
        GWLP_ID = -12,

        /// <summary>
        ///     Retrieves the window styles.
        /// </summary>
        GWL_STYLE = -16,

        /// <summary>
        ///     Retrieves the user data associated with the window. This data is intended for use by the application that created
        ///     the window. Its value is initially zero.
        /// </summary>
        GWLP_USERDATA = -21,

        /// <summary>
        ///     Retrieves the pointer to the window procedure, or a handle representing the pointer to the window procedure. You
        ///     must use the CallWindowProc function to call the window procedure.
        /// </summary>
        GWLP_WNDPROC = -4,

        /// <summary>
        ///     Retrieves the pointer to the dialog box procedure, or a handle representing the pointer to the dialog box
        ///     procedure. You must use the CallWindowProc function to call the dialog box procedure.
        ///     Note: Should be DWLP_MSGRESULT + sizeof(LRESULT)
        /// </summary>
        DWLP_DLGPROC = 0x4,

        /// <summary>
        ///     Retrieves the return value of a message processed in the dialog box procedure.
        /// </summary>
        DWLP_MSGRESULT = 0,

        /// <summary>
        ///     Retrieves extra information private to the application, such as handles or pointers.
        ///     Note: Should be DWLP_DLGPROC + sizeof(DLGPROC)
        /// </summary>
        DWLP_USER = 0x8
    }

    [Flags]
    public enum SetWindowPosFlags
    {
        /// <summary>
        ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts
        ///     the request to the thread that owns the window. This prevents the calling thread from blocking its execution while
        ///     other threads process the request.
        /// </summary>
        SWP_ASYNCWINDOWPOS = 0x4000,

        /// <summary>
        ///     Prevents generation of the WM_SYNCPAINT message.
        /// </summary>
        SWP_DEFERERASE = 0x2000,

        /// <summary>
        ///     Draws a frame (defined in the window's class description) around the window.
        /// </summary>
        SWP_DRAWFRAME = 0x0020,

        /// <summary>
        ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if
        ///     the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's
        ///     size is being changed.
        /// </summary>
        SWP_FRAMECHANGED = 0x0020,

        /// <summary>
        ///     Hides the window.
        /// </summary>
        SWP_HIDEWINDOW = 0x0080,

        /// <summary>
        ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the
        ///     topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
        /// </summary>
        SWP_NOACTIVATE = 0x0010,

        /// <summary>
        ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client
        ///     area are saved and copied back into the client area after the window is sized or repositioned.
        /// </summary>
        SWP_NOCOPYBITS = 0x0100,

        /// <summary>
        ///     Retains the current position (ignores X and Y parameters).
        /// </summary>
        SWP_NOMOVE = 0x0002,

        /// <summary>
        ///     Does not change the owner window's position in the Z order.
        /// </summary>
        SWP_NOOWNERZORDER = 0x0200,

        /// <summary>
        ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area,
        ///     the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a
        ///     result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any
        ///     parts of the window and parent window that need redrawing.
        /// </summary>
        SWP_NOREDRAW = 0x0008,

        /// <summary>
        ///     Same as the SWP_NOOWNERZORDER flag.
        /// </summary>
        SWP_NOREPOSITION = 0x0200,

        /// <summary>
        ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        /// </summary>
        SWP_NOSENDCHANGING = 0x0400,

        /// <summary>
        ///     Retains the current size (ignores the cx and cy parameters).
        /// </summary>
        SWP_NOSIZE = 0x0001,

        /// <summary>
        ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
        /// </summary>
        SWP_NOZORDER = 0x0004,

        /// <summary>
        ///     Displays the window.
        /// </summary>
        SWP_SHOWWINDOW = 0x0040
    }

    [Flags]
    public enum DeviceContextFlags
    {
        /// <summary>
        ///     Returns a DC that corresponds to the window rectangle rather than the client rectangle.
        /// </summary>
        WINDOW = 0x0000000,

        /// <summary>
        ///     Returns a DC from the cache, rather than the OWNDC or CLASSDC window. Essentially overrides CS_OWNDC and
        ///     CS_CLASSDC.
        /// </summary>
        CACHE = 0x0000000,

        /// <summary>
        ///     Does not reset the attributes of this DC to the default attributes when this DC is released.
        /// </summary>
        NORESETATTRS = 0x0000000,

        /// <summary>
        ///     Excludes the visible regions of all child windows below the window identified by hWnd.
        /// </summary>
        CLIPCHILDREN = 0x0000000,

        /// <summary>
        ///     Excludes the visible regions of all sibling windows above the window identified by hWnd.
        /// </summary>
        CLIPSIBLINGS = 0x0000001,

        /// <summary>
        ///     Uses the visible region of the parent window. The parent's WS_CLIPCHILDREN and CS_PARENTDC style bits are ignored.
        ///     The origin is set to the upper-left corner of the window identified by hWnd.
        /// </summary>
        PARENTCLIP = 0x0000002,

        /// <summary>
        ///     The clipping region identified by hrgnClip is excluded from the visible region of the returned DC.
        /// </summary>
        EXCLUDERGN = 0x0000004,

        /// <summary>
        ///     The clipping region identified by hrgnClip is intersected with the visible region of the returned DC.
        /// </summary>
        INTERSECTRGN = 0x0000008,

        /// <summary>
        ///     Undocumented flag
        /// </summary>
        EXCLUDEUPDATE = 0x0000010,

        /// <summary>
        ///     Reserved; do not use.
        /// </summary>
        INTERSECTUPDATE = 0x0000020,

        /// <summary>
        ///     Allows drawing even if there is a LockWindowUpdate call in effect that would otherwise exclude this window. Used
        ///     for drawing during tracking.
        /// </summary>
        LOCKWINDOWUPDATE = 0x0000040,

        /// <summary>
        ///     Reserved; do not use.
        /// </summary>
        VALIDATE = 0x0020000
    }

    [Flags]
    public enum WindowPlacementFlags
    {
        /// <summary>
        ///     The coordinates of the minimized window may be specified.
        ///     This flag must be specified if the coordinates are set in the ptMinPosition member.
        /// </summary>
        SETMINPOSITION = 0x0001,

        /// <summary>
        ///     The restored window will be maximized, regardless of whether it was maximized before it was minimized. This setting
        ///     is only valid the next time the window is restored. It does not change the default restoration behavior.
        ///     This flag is only valid when the SW_SHOWMINIMIZED value is specified for the showCmd member.
        /// </summary>
        RESTORETOMAXIMIZED = 0x0002,

        /// <summary>
        ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts
        ///     the request to the thread that owns the window. This prevents the calling thread from blocking its execution while
        ///     other threads process the request.
        /// </summary>
        ASYNCWINDOWPLACEMENT = 0x0004
    }

    [Flags]
    public enum RedrawWindowFlags
    {
        /// <summary>
        ///     Invalidates lprcUpdate or hrgnUpdate (only one may be non-NULL). If both are NULL, the entire window is
        ///     invalidated.
        /// </summary>
        RDW_INVALIDATE = 0x1,

        /// <summary>
        ///     Causes a WM_PAINT message to be posted to the window regardless of whether any portion of the window is invalid.
        /// </summary>
        RDW_INTERNALPAINT = 0x2,

        /// <summary>
        ///     Causes the window to receive a WM_ERASEBKGND message when the window is repainted. The RDW_INVALIDATE flag must
        ///     also be specified; otherwise, RDW_ERASE has no effect.
        /// </summary>
        RDW_ERASE = 0x4,

        /// <summary>
        ///     Validates lprcUpdate or hrgnUpdate (only one may be non-NULL). If both are NULL, the entire window is validated.
        ///     This flag does not affect internal WM_PAINT messages.
        /// </summary>
        RDW_VALIDATE = 0x8,

        /// <summary>
        ///     Suppresses any pending internal WM_PAINT messages. This flag does not affect WM_PAINT messages resulting from a
        ///     non-NULL update area.
        /// </summary>
        RDW_NOINTERNALPAINT = 0x10,

        /// <summary>
        ///     Suppresses any pending WM_ERASEBKGND messages.
        /// </summary>
        RDW_NOERASE = 0x20,

        /// <summary>
        ///     Excludes child windows, if any, from the repainting operation.
        /// </summary>
        RDW_NOCHILDREN = 0x40,

        /// <summary>
        ///     Includes child windows, if any, in the repainting operation.
        /// </summary>
        RDW_ALLCHILDREN = 0x80,

        /// <summary>
        ///     Causes the affected windows (as specified by the RDW_ALLCHILDREN and RDW_NOCHILDREN flags) to receive WM_NCPAINT,
        ///     WM_ERASEBKGND, and WM_PAINT messages, if necessary, before the function returns.
        /// </summary>
        RDW_UPDATENOW = 0x100,

        /// <summary>
        ///     Causes the affected windows (as specified by the RDW_ALLCHILDREN and RDW_NOCHILDREN flags) to receive WM_NCPAINT
        ///     and WM_ERASEBKGND messages, if necessary, before the function returns. WM_PAINT messages are received at the
        ///     ordinary time.
        /// </summary>
        RDW_ERASENOW = 0x200,

        /// <summary>
        ///     Causes any part of the nonclient area of the window that intersects the update region to receive a WM_NCPAINT
        ///     message. The RDW_INVALIDATE flag must also be specified; otherwise, RDW_FRAME has no effect. The WM_NCPAINT message
        ///     is typically not sent during the execution of RedrawWindow unless either RDW_UPDATENOW or RDW_ERASENOW is
        ///     specified.
        /// </summary>
        RDW_FRAME = 0x400,

        /// <summary>
        ///     Suppresses any pending WM_NCPAINT messages. This flag must be used with RDW_VALIDATE and is typically used with
        ///     RDW_NOCHILDREN. RDW_NOFRAME should be used with care, as it could cause parts of a window to be painted improperly.
        /// </summary>
        RDW_NOFRAME = 0x800
    }

    [Flags]
    public enum ClassLongFlags
    {
        /// <summary>
        ///     Sets the size, in bytes, of the extra memory associated with the class. Setting this value does not change the
        ///     number of extra bytes already allocated.
        /// </summary>
        GCL_CBCLSEXTRA = -20,

        /// <summary>
        ///     Sets the size, in bytes, of the extra window memory associated with each window in the class. Setting this value
        ///     does not change the number of extra bytes already allocated. For information on how to access this memory, see
        ///     SetWindowLong.
        /// </summary>
        GCL_CBWNDEXTRA = -18,

        /// <summary>
        ///     Replaces a handle to the background brush associated with the class.
        /// </summary>
        GCL_HBRBACKGROUND = -10,

        /// <summary>
        ///     Replaces a handle to the cursor associated with the class.
        /// </summary>
        GCL_HCURSOR = -12,

        /// <summary>
        ///     Replaces a handle to the icon associated with the class.
        /// </summary>
        GCL_HICON = -14,

        /// <summary>
        ///     Replace a handle to the small icon associated with the class.
        /// </summary>
        GCL_HICONSM = -34,

        /// <summary>
        ///     Replaces a handle to the module that registered the class.
        /// </summary>
        GCL_HMODULE = -16,

        /// <summary>
        ///     Replaces the address of the menu name string. The string identifies the menu resource associated with the class.
        /// </summary>
        GCL_MENUNAME = -8,

        /// <summary>
        ///     Replaces the window-class style bits.
        /// </summary>
        GCL_STYLE = -26,

        /// <summary>
        ///     Replaces the address of the window procedure associated with the class.
        /// </summary>
        GCL_WNDPROC = -24
    }

    [Flags]
    public enum QueueStatusFlags
    {
        /// <summary>
        ///     An input, WM_TIMER, WM_PAINT, WM_HOTKEY, or posted message is in the queue.
        /// </summary>
        QS_ALLEVENTS = QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY,

        /// <summary>
        ///     Any message is in the queue.
        /// </summary>
        QS_ALLINPUT = QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY | QS_SENDMESSAGE,

        /// <summary>
        ///     A posted message (other than those listed here) is in the queue.
        /// </summary>
        QS_ALLPOSTMESSAGE = 0x0100,

        /// <summary>
        ///     A WM_HOTKEY message is in the queue.
        /// </summary>
        QS_HOTKEY = 0x0080,

        /// <summary>
        ///     An input message is in the queue.
        /// </summary>
        QS_INPUT = QS_MOUSE | QS_KEY | QS_RAWINPUT,

        /// <summary>
        ///     A WM_KEYUP, WM_KEYDOWN, WM_SYSKEYUP, or WM_SYSKEYDOWN message is in the queue.
        /// </summary>
        QS_KEY = 0x0001,

        /// <summary>
        ///     A WM_MOUSEMOVE message or mouse-button message (WM_LBUTTONUP, WM_RBUTTONDOWN, and so on).
        /// </summary>
        QS_MOUSE = QS_MOUSEMOVE | QS_MOUSEBUTTON,

        /// <summary>
        ///     A mouse-button message (WM_LBUTTONUP, WM_RBUTTONDOWN, and so on).
        /// </summary>
        QS_MOUSEBUTTON = 0x0004,

        /// <summary>
        ///     A WM_MOUSEMOVE message is in the queue.
        /// </summary>
        QS_MOUSEMOVE = 0x0002,

        /// <summary>
        ///     A WM_PAINT message is in the queue.
        /// </summary>
        QS_PAINT = 0x0020,

        /// <summary>
        ///     A posted message (other than those listed here) is in the queue.
        /// </summary>
        QS_POSTMESSAGE = 0x0008,

        /// <summary>
        ///     A raw input message is in the queue. For more information, see Raw Input.
        ///     Windows 2000:  This flag is not supported.
        /// </summary>
        QS_RAWINPUT = 0x0400,

        /// <summary>
        ///     A message sent by another thread or application is in the queue.
        /// </summary>
        QS_SENDMESSAGE = 0x0040,

        /// <summary>
        ///     A WM_TIMER message is in the queue.
        /// </summary>
        QS_TIMER = 0x0010,

        QS_REFRESH = QS_HOTKEY | QS_KEY | QS_MOUSEBUTTON | QS_PAINT
    }

    [Flags]
    public enum PeekMessageFlags
    {
        /// <summary>
        ///     Messages are not removed from the queue after processing by PeekMessage.
        /// </summary>
        PM_NOREMOVE = 0x0000,

        /// <summary>
        ///     Messages are removed from the queue after processing by PeekMessage.
        /// </summary>
        PM_REMOVE = 0x0001,

        /// <summary>
        ///     Prevents the system from releasing any thread that is waiting for the caller to go idle (see WaitForInputIdle).
        ///     Combine this value with either PM_NOREMOVE or PM_REMOVE.
        /// </summary>
        PM_NOYIELD = 0x0002,

        /// <summary>
        ///     Process mouse and keyboard messages.
        /// </summary>
        PM_QS_INPUT = QueueStatusFlags.QS_INPUT << 16,

        /// <summary>
        ///     Process paint messages.
        /// </summary>
        PM_QS_PAINT = QueueStatusFlags.QS_PAINT << 16,

        /// <summary>
        ///     Process all posted messages, including timers and hotkeys.
        /// </summary>
        PM_QS_POSTMESSAGE =
            (QueueStatusFlags.QS_POSTMESSAGE | QueueStatusFlags.QS_HOTKEY | QueueStatusFlags.QS_TIMER) << 16,

        /// <summary>
        ///     Process all sent messages.
        /// </summary>
        PM_QS_SENDMESSAGE = QueueStatusFlags.QS_SENDMESSAGE << 16
    }

    [Flags]
    public enum ChildWindowFromPointFlags
    {
        /// <summary>
        ///     Does not skip any child windows
        /// </summary>
        CWP_ALL = 0x0000,

        /// <summary>
        ///     Skips disabled child windows
        /// </summary>
        CWP_SKIPDISABLED = 0x0002,

        /// <summary>
        ///     Skips invisible child windows
        /// </summary>
        CWP_SKIPINVISIBLE = 0x0001,

        /// <summary>
        ///     Skips transparent child windows
        /// </summary>
        CWP_SKIPTRANSPARENT = 0x0004
    }

    [Flags]
    public enum SystemParamtersInfoFlags
    {
        None = 0x00,

        /// <summary>Writes the new system-wide parameter setting to the user profile.</summary>
        SPIF_UPDATEINIFILE = 0x01,

        /// <summary>Broadcasts the WM_SETTINGCHANGE message after updating the user profile.</summary>
        SPIF_SENDCHANGE = 0x02,

        /// <summary>Same as SPIF_SENDCHANGE.</summary>
        SPIF_SENDWININICHANGE = 0x02
    }

    [Flags]
    public enum ArrangeFlags
    {
        /// <summary>
        ///     Start at the lower-left corner of the work area.
        /// </summary>
        ARW_BOTTOMLEFT = 0x0000,

        /// <summary>
        ///     Start at the lower-right corner of the work area.
        /// </summary>
        ARW_BOTTOMRIGHT = 0x0001,

        /// <summary>
        ///     Start at the upper-left corner of the work area.
        /// </summary>
        ARW_TOPLEFT = 0x0002,

        /// <summary>
        ///     Start at the upper-right corner of the work area.
        /// </summary>
        ARW_TOPRIGHT = 0x0003,

        /// <summary>
        ///     Arrange left (valid with ARW_BOTTOMRIGHT and ARW_TOPRIGHT only).
        /// </summary>
        ARW_LEFT = 0x0000,

        /// <summary>
        ///     Arrange right (valid with ARW_BOTTOMLEFT and ARW_TOPLEFT only).
        /// </summary>
        ARW_RIGHT = 0x0000,

        /// <summary>
        ///     Arrange up (valid with ARW_BOTTOMLEFT and ARW_BOTTOMRIGHT only).
        /// </summary>
        ARW_UP = 0x0004,

        /// <summary>
        ///     Arrange down (valid with ARW_TOPLEFT and ARW_TOPRIGHT only).
        /// </summary>
        ARW_DOWN = 0x0004,

        /// <summary>
        ///     Hide minimized windows by moving them off the visible area of the screen.
        /// </summary>
        ARW_HIDE = 0x0008
    }

    [Flags]
    public enum KeyModifierFlags
    {

        /// <summary>
        /// Either ALT key must be held down.
        /// </summary>

        MOD_ALT = 0x0001,

        /// <summary>
        /// Either CTRL key must be held down.
        /// </summary>

        MOD_CONTROL = 0x0002,

        /// <summary>
        ///  Changes the hotkey behavior so that the keyboard auto-repeat does not yield multiple hotkey notifications. 
        /// Windows Vista:  This flag is not supported.
        /// </summary>

        MOD_NOREPEAT = 0x4000,

        /// <summary>
        /// Either SHIFT key must be held down.
        /// </summary>

        MOD_SHIFT = 0x0004,

        /// <summary>
        /// Either WINDOWS key was held down. These keys are labeled with the Windows logo. Keyboard shortcuts that involve the WINDOWS key are reserved for use by the operating system.
        /// </summary>

        MOD_WIN = 0x0008
    }

    #endregion

    #region Singular constants

    public enum SystemCursor
    {
        /// <summary>
        ///     Standard arrow and small hourglass
        /// </summary>
        IDC_APPSTARTING = 32650,

        /// <summary>
        ///     Standard arrow
        /// </summary>
        IDC_ARROW = 32512,

        /// <summary>
        ///     Crosshair
        /// </summary>
        IDC_CROSS = 32515,

        /// <summary>
        ///     Hand
        /// </summary>
        IDC_HAND = 32649,

        /// <summary>
        ///     Arrow and question mark
        /// </summary>
        IDC_HELP = 32651,

        /// <summary>
        ///     I-beam
        /// </summary>
        IDC_IBEAM = 32513,

        /// <summary>
        ///     Obsolete for applications marked version 4.0 or later.
        /// </summary>
        IDC_ICON = 32641,

        /// <summary>
        ///     Slashed circle
        /// </summary>
        IDC_NO = 32648,

        /// <summary>
        ///     Obsolete for applications marked version 4.0 or later. Use IDC_SIZEALL.
        /// </summary>
        IDC_SIZE = 32640,

        /// <summary>
        ///     Four-pointed arrow pointing north, south, east, and west
        /// </summary>
        IDC_SIZEALL = 32646,

        /// <summary>
        ///     Double-pointed arrow pointing northeast and southwest
        /// </summary>
        IDC_SIZENESW = 32643,

        /// <summary>
        ///     Double-pointed arrow pointing north and south
        /// </summary>
        IDC_SIZENS = 32645,

        /// <summary>
        ///     Double-pointed arrow pointing northwest and southeast
        /// </summary>
        IDC_SIZENWSE = 32642,

        /// <summary>
        ///     Double-pointed arrow pointing west and east
        /// </summary>
        IDC_SIZEWE = 32644,

        /// <summary>
        ///     Vertical arrow
        /// </summary>
        IDC_UPARROW = 32516,

        /// <summary>
        ///     Hourglass
        /// </summary>
        IDC_WAIT = 32514
    }

    public enum SystemIcon
    {
        IDI_APPLICATION = 32512,
        IDI_HAND = 32513,
        IDI_QUESTION = 32514,
        IDI_EXCLAMATION = 32515,
        IDI_ASTERISK = 32516,
        IDI_WINLOGO = 32517,
        IDI_WARNING = IDI_EXCLAMATION,
        IDI_ERROR = IDI_HAND,
        IDI_INFORMATION = IDI_ASTERISK
    }

    public enum HwndZOrder
    {
        /// <summary>
        ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses
        ///     its topmost status and is placed at the bottom of all other windows.
        /// </summary>
        HWND_BOTTOM = 1,

        /// <summary>
        ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if
        ///     the window is already a non-topmost window.
        /// </summary>
        HWND_NOTOPMOST = -2,

        /// <summary>
        ///     Places the window at the top of the Z order.
        /// </summary>
        HWND_TOP = 0,

        /// <summary>
        ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is
        ///     deactivated.
        /// </summary>
        HWND_TOPMOST = -1
    }

    public enum WindowRegionType
    {
        /// <summary>
        ///     The specified window does not have a region, or an error occurred while attempting to return the region.
        /// </summary>
        ERROR = 0,

        /// <summary>
        ///     The region is empty.
        /// </summary>
        NULLREGION,

        /// <summary>
        ///     The region is a single rectangle.
        /// </summary>
        SIMPLEREGION,

        /// <summary>
        ///     The region is more than one rectangle.
        /// </summary>
        COMPLEXREGION
    }

    public enum GetWindowFlag
    {
        /// <summary>
        ///     The retrieved handle identifies the child window at the top of the Z order, if the specified window is a parent
        ///     window; otherwise, the retrieved handle is NULL. The function examines only child windows of the specified window.
        ///     It does not examine descendant windows.
        /// </summary>
        GW_CHILD = 5,

        /// <summary>
        ///     The retrieved handle identifies the enabled popup window owned by the specified window (the search uses the first
        ///     such window found using GW_HWNDNEXT); otherwise, if there are no enabled popup windows, the retrieved handle is
        ///     that of the specified window.
        /// </summary>
        GW_ENABLEDPOPUP = 6,

        /// <summary>
        ///     The retrieved handle identifies the window of the same type that is highest in the Z order.
        ///     If the specified window is a topmost window, the handle identifies a topmost window. If the specified window is a
        ///     top-level window, the handle identifies a top-level window. If the specified window is a child window, the handle
        ///     identifies a sibling window.
        /// </summary>
        GW_HWNDFIRST = 0,

        /// <summary>
        ///     The retrieved handle identifies the window of the same type that is lowest in the Z order.
        ///     If the specified window is a topmost window, the handle identifies a topmost window. If the specified window is a
        ///     top-level window, the handle identifies a top-level window. If the specified window is a child window, the handle
        ///     identifies a sibling window.
        /// </summary>
        GW_HWNDLAST = 1,

        /// <summary>
        ///     The retrieved handle identifies the window below the specified window in the Z order.
        ///     If the specified window is a topmost window, the handle identifies a topmost window. If the specified window is a
        ///     top-level window, the handle identifies a top-level window. If the specified window is a child window, the handle
        ///     identifies a sibling window.
        /// </summary>
        GW_HWNDNEXT = 2,

        /// <summary>
        ///     The retrieved handle identifies the window above the specified window in the Z order.
        ///     If the specified window is a topmost window, the handle identifies a topmost window. If the specified window is a
        ///     top-level window, the handle identifies a top-level window. If the specified window is a child window, the handle
        ///     identifies a sibling window.
        /// </summary>
        GW_HWNDPREV = 3,

        /// <summary>
        ///     The retrieved handle identifies the specified window's owner window, if any. For more information, see Owned
        ///     Windows.
        /// </summary>
        GW_OWNER = 4
    }

    public enum AlphaFormat : byte
    {
        AC_SRC_OVER = 0x00,
        AC_SRC_ALPHA = 0x01
    }

    public enum WindowMonitorFlag
    {
        /// <summary>
        /// Returns NULL.
        /// </summary>
        MONITOR_DEFAULTTONULL = 0,
        /// <summary>
        /// Returns a handle to the primary display monitor.
        /// </summary>
        MONITOR_DEFAULTTOPRIMARY = 1,
        /// <summary>
        /// Returns a handle to the display monitor that is nearest to the window.
        /// </summary>
        MONITOR_DEFAULTTONEAREST = 2,
    }

    public enum SystemMetrics
    {
        /// <summary>
        ///     The flags that specify how the system arranged minimized windows. For more information, see the Remarks section in
        ///     this topic.
        /// </summary>
        SM_ARRANGE = 56,

        /// <summary>
        ///     The value that specifies how the system is started:
        ///     0 Normal boot
        ///     1 Fail-safe boot
        ///     2 Fail-safe with network boot
        ///     A fail-safe boot (also called SafeBoot, Safe Mode, or Clean Boot) bypasses the user startup files.
        /// </summary>
        SM_CLEANBOOT = 67,

        /// <summary>
        ///     The number of display monitors on a desktop. For more information, see the Remarks section in this topic.
        /// </summary>
        SM_CMONITORS = 80,

        /// <summary>
        ///     The number of buttons on a mouse, or zero if no mouse is installed.
        /// </summary>
        SM_CMOUSEBUTTONS = 43,

        /// <summary>
        ///     Reflects the state of the laptop or slate mode, 0 for Slate Mode and non-zero otherwise. When this system metric
        ///     changes, the system sends a broadcast message via WM_SETTINGCHANGE with "ConvertibleSlateMode" in the LPARAM. Note
        ///     that this system metric doesn't apply to desktop PCs. In that case, use GetAutoRotationState.
        /// </summary>
        SM_CONVERTIBLESLATEMODE = 0x2003,

        /// <summary>
        ///     The width of a window border, in pixels. This is equivalent to the SM_CXEDGE value for
        ///     windows with the 3-D look.
        /// </summary>
        SM_CXBORDER = 5,

        /// <summary>
        ///     The width of a cursor, in pixels. The system cannot create cursors of other sizes.
        /// </summary>
        SM_CXCURSOR = 13,

        /// <summary>
        ///     This value is the same as SM_CXFIXEDFRAME.
        /// </summary>
        SM_CXDLGFRAME = 7,

        /// <summary>
        ///     The width of the rectangle around the location of a first click in a double-click sequence,
        ///     in pixels. The second click must occur within the rectangle that is defined by SM_CXDOUBLECLK and SM_CYDOUBLECLK
        ///     for the system to consider the two
        ///     clicks a double-click. The two clicks must also occur within a specified time.
        ///     To set the width of the double-click rectangle, call
        ///     SystemParametersInfo with SPI_SETDOUBLECLKWIDTH.
        /// </summary>
        SM_CXDOUBLECLK = 36,

        /// <summary>
        ///     The number of pixels on either side of a mouse-down point that  the mouse
        ///     pointer can move before a drag operation begins. This allows the user to click and release the
        ///     mouse button easily without unintentionally starting a drag operation. If this value is negative, it is subtracted
        ///     from the left of the mouse-down point and added to the right of it.
        /// </summary>
        SM_CXDRAG = 68,

        /// <summary>
        ///     The width of a 3-D border, in pixels. This metric is the 3-D counterpart of SM_CXBORDER.
        /// </summary>
        SM_CXEDGE = 45,

        /// <summary>
        ///     The thickness of the frame around the perimeter of a window that has a caption but is not sizable, in pixels.
        ///     SM_CXFIXEDFRAME is the height of the horizontal border, and SM_CYFIXEDFRAME is the width of the vertical border.
        ///     This value is the same as SM_CXDLGFRAME.
        /// </summary>
        SM_CXFIXEDFRAME = 7,

        /// <summary>
        ///     The width of the left and right edges of the focus rectangle that the DrawFocusRect draws. This value is in pixels.
        ///     Windows 2000:  This value is not supported.
        /// </summary>
        SM_CXFOCUSBORDER = 83,

        /// <summary>
        ///     This value is the same as SM_CXSIZEFRAME.
        /// </summary>
        SM_CXFRAME = 32,

        /// <summary>
        ///     The width of the client area for a full-screen window on the primary display monitor, in pixels. To
        ///     get the coordinates of the portion of the screen that is not obscured by the system taskbar or by application
        ///     desktop
        ///     toolbars, call the
        ///     SystemParametersInfo function with
        ///     the SPI_GETWORKAREA value.
        /// </summary>
        SM_CXFULLSCREEN = 16,

        /// <summary>
        ///     The width of the arrow bitmap on a horizontal scroll bar, in pixels.
        /// </summary>
        SM_CXHSCROLL = 21,

        /// <summary>
        ///     The width of the thumb box in a horizontal scroll bar, in pixels.
        /// </summary>
        SM_CXHTHUMB = 10,

        /// <summary>
        ///     The default width of an icon, in pixels. The
        ///     LoadIcon function can load only icons with the  dimensions that SM_CXICON and SM_CYICON specifies.
        /// </summary>
        SM_CXICON = 11,

        /// <summary>
        ///     The width of a grid cell for items in large icon view, in pixels. Each item fits into a rectangle of size
        ///     SM_CXICONSPACING by SM_CYICONSPACING when arranged. This value is always greater than or equal to SM_CXICON.
        /// </summary>
        SM_CXICONSPACING = 38,

        /// <summary>
        ///     The default width, in pixels, of a maximized top-level window on the primary display monitor.
        /// </summary>
        SM_CXMAXIMIZED = 61,

        /// <summary>
        ///     The default maximum width of a window that has a caption and sizing borders, in pixels. This metric
        ///     refers to the entire desktop. The user cannot drag the window frame to a size larger than these dimensions. A
        ///     window can override this value by processing the
        ///     WM_GETMINMAXINFO message.
        /// </summary>
        SM_CXMAXTRACK = 59,

        /// <summary>
        ///     The width of the default menu check-mark bitmap, in pixels.
        /// </summary>
        SM_CXMENUCHECK = 71,

        /// <summary>
        ///     The width of menu bar buttons, such as the child window close button that is used in the multiple document
        ///     interface, in pixels.
        /// </summary>
        SM_CXMENUSIZE = 54,

        /// <summary>
        ///     The minimum width of a window, in pixels.
        /// </summary>
        SM_CXMIN = 28,

        /// <summary>
        ///     The width of a minimized window, in pixels.
        /// </summary>
        SM_CXMINIMIZED = 57,

        /// <summary>
        ///     The width of a grid cell for a minimized window, in pixels. Each minimized window fits into a rectangle
        ///     this size when arranged. This value is always greater than or equal to SM_CXMINIMIZED.
        /// </summary>
        SM_CXMINSPACING = 47,

        /// <summary>
        ///     The minimum tracking width of a window, in pixels. The user cannot drag the window frame to a size
        ///     smaller than these dimensions. A window can override this value by processing the
        ///     WM_GETMINMAXINFO message.
        /// </summary>
        SM_CXMINTRACK = 34,

        /// <summary>
        ///     The amount of border padding for captioned windows, in pixels.
        ///     Windows XP/2000:  This value is not supported.
        /// </summary>
        SM_CXPADDEDBORDER = 92,

        /// <summary>
        ///     The width of the screen of the primary display monitor, in pixels. This is the same value
        ///     obtained by calling
        ///     GetDeviceCaps as follows: GetDeviceCaps(
        ///     hdcPrimaryMonitor, HORZRES).
        /// </summary>
        SM_CXSCREEN = 0,

        /// <summary>
        ///     The width of a button in a window caption or title bar, in pixels.
        /// </summary>
        SM_CXSIZE = 30,

        /// <summary>
        ///     The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
        ///     SM_CXSIZEFRAME is the width of the horizontal border, and SM_CYSIZEFRAME is the height of the vertical border.
        ///     This value is the same as SM_CXFRAME.
        /// </summary>
        SM_CXSIZEFRAME = 32,

        /// <summary>
        ///     The recommended width of a small icon, in pixels. Small icons typically appear in window captions and in
        ///     small icon view.
        /// </summary>
        SM_CXSMICON = 49,

        /// <summary>
        ///     The width of small caption buttons, in pixels.
        /// </summary>
        SM_CXSMSIZE = 52,

        /// <summary>
        ///     The width of the virtual screen, in pixels. The virtual screen is the bounding rectangle of all
        ///     display monitors. The SM_XVIRTUALSCREEN metric is the coordinates for the left side of
        ///     the virtual screen.
        /// </summary>
        SM_CXVIRTUALSCREEN = 78,

        /// <summary>
        ///     The width of a vertical scroll bar, in pixels.
        /// </summary>
        SM_CXVSCROLL = 2,

        /// <summary>
        ///     The height of a window border, in pixels. This is equivalent to the SM_CYEDGE value for
        ///     windows with the 3-D look.
        /// </summary>
        SM_CYBORDER = 6,

        /// <summary>
        ///     The height of a caption area, in pixels.
        /// </summary>
        SM_CYCAPTION = 4,

        /// <summary>
        ///     The height of a cursor, in pixels. The system cannot create cursors of other sizes.
        /// </summary>
        SM_CYCURSOR = 14,

        /// <summary>
        ///     This value is the same as SM_CYFIXEDFRAME.
        /// </summary>
        SM_CYDLGFRAME = 8,

        /// <summary>
        ///     The height of the rectangle around the location of a first click in a double-click sequence,
        ///     in pixels. The second click must occur within the rectangle defined by SM_CXDOUBLECLK and SM_CYDOUBLECLK for the
        ///     system to consider the two
        ///     clicks a double-click. The two clicks must also occur within a specified time.
        ///     To set the height of the double-click rectangle, call
        ///     SystemParametersInfo with SPI_SETDOUBLECLKHEIGHT.
        /// </summary>
        SM_CYDOUBLECLK = 37,

        /// <summary>
        ///     The number of pixels above and below a mouse-down point that the mouse
        ///     pointer can move before a drag operation begins. This allows the user to click and release the
        ///     mouse button easily without unintentionally starting a drag operation. If this value is negative, it is subtracted
        ///     from above the mouse-down point and added below it.
        /// </summary>
        SM_CYDRAG = 69,

        /// <summary>
        ///     The height of a 3-D border, in pixels. This is the 3-D counterpart of SM_CYBORDER.
        /// </summary>
        SM_CYEDGE = 46,

        /// <summary>
        ///     The thickness of the frame around the perimeter of a window that has a caption but is not sizable, in pixels.
        ///     SM_CXFIXEDFRAME is the height of the horizontal border, and SM_CYFIXEDFRAME is the width of the vertical border.
        ///     This value is the same as SM_CYDLGFRAME.
        /// </summary>
        SM_CYFIXEDFRAME = 8,

        /// <summary>
        ///     The height of the top and bottom edges of the focus rectangle drawn
        ///     by
        ///     DrawFocusRect. This value is in pixels.
        ///     Windows 2000:  This value is not supported.
        /// </summary>
        SM_CYFOCUSBORDER = 84,

        /// <summary>
        ///     This value is the same as SM_CYSIZEFRAME.
        /// </summary>
        SM_CYFRAME = 33,

        /// <summary>
        ///     The height of the client area for a full-screen window on the primary display monitor, in pixels. To
        ///     get the coordinates of the portion of the screen not obscured by the system taskbar or by application desktop
        ///     toolbars, call the
        ///     SystemParametersInfo function with
        ///     the SPI_GETWORKAREA value.
        /// </summary>
        SM_CYFULLSCREEN = 17,

        /// <summary>
        ///     The height of a horizontal scroll bar, in
        ///     pixels.
        /// </summary>
        SM_CYHSCROLL = 3,

        /// <summary>
        ///     The default height of an icon, in pixels. The
        ///     LoadIcon function can load only icons with the
        ///     dimensions SM_CXICON and SM_CYICON.
        /// </summary>
        SM_CYICON = 12,

        /// <summary>
        ///     The height of a grid cell for items in large icon view, in pixels. Each item fits into a rectangle of size
        ///     SM_CXICONSPACING by SM_CYICONSPACING when arranged. This value is always greater than or equal to SM_CYICON.
        /// </summary>
        SM_CYICONSPACING = 39,

        /// <summary>
        ///     For double byte character set versions of the system, this is the height of the Kanji window at the bottom
        ///     of the screen, in pixels.
        /// </summary>
        SM_CYKANJIWINDOW = 18,

        /// <summary>
        ///     The default height, in pixels, of a maximized top-level window on the primary display monitor.
        /// </summary>
        SM_CYMAXIMIZED = 62,

        /// <summary>
        ///     The default maximum height of a window that has a caption and sizing borders, in pixels. This metric
        ///     refers to the entire desktop. The user cannot drag the window frame to a size larger than these dimensions. A
        ///     window can override this value by processing the
        ///     WM_GETMINMAXINFO message.
        /// </summary>
        SM_CYMAXTRACK = 60,

        /// <summary>
        ///     The height of a single-line menu bar, in pixels.
        /// </summary>
        SM_CYMENU = 15,

        /// <summary>
        ///     The height of the default menu check-mark bitmap, in pixels.
        /// </summary>
        SM_CYMENUCHECK = 72,

        /// <summary>
        ///     The height of menu bar buttons, such as the child window close button that is used in the multiple document
        ///     interface, in pixels.
        /// </summary>
        SM_CYMENUSIZE = 55,

        /// <summary>
        ///     The minimum height of a window, in pixels.
        /// </summary>
        SM_CYMIN = 29,

        /// <summary>
        ///     The height of a minimized window, in pixels.
        /// </summary>
        SM_CYMINIMIZED = 58,

        /// <summary>
        ///     The height of a grid cell for a minimized window, in pixels. Each minimized window fits into a rectangle
        ///     this size when arranged. This value is always greater than or equal to SM_CYMINIMIZED.
        /// </summary>
        SM_CYMINSPACING = 48,

        /// <summary>
        ///     The minimum tracking height of a window, in pixels. The user cannot drag the window frame to a size
        ///     smaller than these dimensions. A window can override this value by processing the
        ///     WM_GETMINMAXINFO message.
        /// </summary>
        SM_CYMINTRACK = 35,

        /// <summary>
        ///     The height of the screen of the primary display monitor, in pixels. This is the same value
        ///     obtained by calling
        ///     GetDeviceCaps as follows: GetDeviceCaps(
        ///     hdcPrimaryMonitor, VERTRES).
        /// </summary>
        SM_CYSCREEN = 1,

        /// <summary>
        ///     The height of a button in a window caption or title bar, in pixels.
        /// </summary>
        SM_CYSIZE = 31,

        /// <summary>
        ///     The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
        ///     SM_CXSIZEFRAME is the width of the horizontal border, and SM_CYSIZEFRAME is the height of the vertical border.
        ///     This value is the same as SM_CYFRAME.
        /// </summary>
        SM_CYSIZEFRAME = 33,

        /// <summary>
        ///     The height of a small caption, in pixels.
        /// </summary>
        SM_CYSMCAPTION = 51,

        /// <summary>
        ///     The recommended height of a small icon, in pixels. Small icons typically appear in window captions and in
        ///     small icon view.
        /// </summary>
        SM_CYSMICON = 50,

        /// <summary>
        ///     The height of small caption buttons, in pixels.
        /// </summary>
        SM_CYSMSIZE = 53,

        /// <summary>
        ///     The height of the virtual screen, in pixels. The virtual screen is the bounding rectangle of all
        ///     display monitors. The SM_YVIRTUALSCREEN metric is the coordinates for the top of
        ///     the virtual screen.
        /// </summary>
        SM_CYVIRTUALSCREEN = 79,

        /// <summary>
        ///     The height of the arrow bitmap on a vertical scroll bar, in
        ///     pixels.
        /// </summary>
        SM_CYVSCROLL = 20,

        /// <summary>
        ///     The height of the thumb box in a vertical scroll bar, in pixels.
        /// </summary>
        SM_CYVTHUMB = 9,

        /// <summary>
        ///     Nonzero if User32.dll supports DBCS; otherwise, 0.
        /// </summary>
        SM_DBCSENABLED = 42,

        /// <summary>
        ///     Nonzero if the debug version of User.exe is installed; otherwise, 0.
        /// </summary>
        SM_DEBUG = 22,

        /// <summary>
        ///     Nonzero  if the current operating system is Windows 7  or Windows Server 2008 R2 and the Tablet PC Input service is
        ///     started; otherwise, 0. The return value is a bitmask that specifies the type of digitizer input supported by the
        ///     device. For more information, see Remarks.
        ///     Windows Server 2008, Windows Vista, and Windows XP/2000:  This value  is not supported.
        /// </summary>
        SM_DIGITIZER = 94,

        /// <summary>
        ///     Nonzero if Input Method Manager/Input Method Editor features are enabled; otherwise, 0.
        ///     SM_IMMENABLED indicates whether the system is ready to use a Unicode-based IME on a Unicode application.
        ///     To ensure that a language-dependent IME works, check SM_DBCSENABLED and the system ANSI code page.
        ///     Otherwise the ANSI-to-Unicode conversion may not be performed correctly, or some components like fonts
        ///     or registry settings may not be present.
        /// </summary>
        SM_IMMENABLED = 82,

        /// <summary>
        ///     Nonzero if there are digitizers in the system; otherwise, 0.
        ///     SM_MAXIMUMTOUCHES returns the aggregate maximum of the maximum number of contacts supported by every digitizer in
        ///     the system.  If the system has only single-touch digitizers, the return value is 1. If the system has  multi-touch
        ///     digitizers, the return value is the number of simultaneous contacts the hardware can provide.
        ///     Windows Server 2008, Windows Vista, and Windows XP/2000:  This value is not supported.
        /// </summary>
        SM_MAXIMUMTOUCHES = 95,

        /// <summary>
        ///     Nonzero if the current operating system is the Windows XP, Media Center Edition, 0 if not.
        /// </summary>
        SM_MEDIACENTER = 87,

        /// <summary>
        ///     Nonzero if drop-down menus are right-aligned with the corresponding menu-bar item; 0 if the menus are
        ///     left-aligned.
        /// </summary>
        SM_MENUDROPALIGNMENT = 40,

        /// <summary>
        ///     Nonzero if the system is enabled for Hebrew and Arabic languages, 0 if not.
        /// </summary>
        SM_MIDEASTENABLED = 74,

        /// <summary>
        ///     Nonzero if a mouse is installed; otherwise, 0. This value is rarely zero, because of support for virtual mice and
        ///     because some systems detect the presence of the port instead of the presence of a mouse.
        /// </summary>
        SM_MOUSEPRESENT = 19,

        /// <summary>
        ///     Nonzero if a mouse with a horizontal scroll wheel is installed; otherwise 0.
        /// </summary>
        SM_MOUSEHORIZONTALWHEELPRESENT = 91,

        /// <summary>
        ///     Nonzero if a mouse with a vertical scroll wheel is installed; otherwise 0.
        /// </summary>
        SM_MOUSEWHEELPRESENT = 75,

        /// <summary>
        ///     The least significant bit is set if a network is present; otherwise, it is cleared. The other bits are
        ///     reserved for future use.
        /// </summary>
        SM_NETWORK = 63,

        /// <summary>
        ///     Nonzero if the Microsoft Windows for Pen computing extensions are installed; zero otherwise.
        /// </summary>
        SM_PENWINDOWS = 41,

        /// <summary>
        ///     This system metric is used in a Terminal Services environment to determine if the current Terminal Server session
        ///     is being remotely controlled. Its value is nonzero if the current
        ///     session is remotely controlled; otherwise, 0.
        ///     You can use terminal services management tools such as Terminal Services Manager (tsadmin.msc) and shadow.exe to
        ///     control a remote session. When a session is being remotely controlled, another user can view the contents of that
        ///     session and potentially interact with it.
        /// </summary>
        SM_REMOTECONTROL = 0x2001,

        /// <summary>
        ///     This system metric is used in a Terminal Services environment. If the calling process is associated
        ///     with a Terminal Services client session, the return value is nonzero. If the calling process is
        ///     associated with the Terminal Services console session, the return value is 0. Windows Server 2003 and Windows XP:
        ///     The console session
        ///     is not necessarily the physical console. For more information, see WTSGetActiveConsoleSessionId.
        /// </summary>
        SM_REMOTESESSION = 0x1000,

        /// <summary>
        ///     Nonzero if all the display monitors have the same color format, otherwise, 0. Two
        ///     displays can have the same bit depth, but different color formats. For example, the red, green,
        ///     and blue pixels can be encoded with different numbers of bits, or those bits can be located in
        ///     different places in a pixel color value.
        /// </summary>
        SM_SAMEDISPLAYFORMAT = 81,

        /// <summary>
        ///     This system metric should be ignored; it always returns 0.
        /// </summary>
        SM_SECURE = 44,

        /// <summary>
        ///     The build number if the system is Windows Server 2003 R2; otherwise, 0.
        /// </summary>
        SM_SERVERR2 = 89,

        /// <summary>
        ///     Nonzero if the user requires an application to present information visually in situations
        ///     where it would otherwise present the information only in audible form; otherwise, 0.
        /// </summary>
        SM_SHOWSOUNDS = 70,

        /// <summary>
        ///     Nonzero if the current session is shutting down; otherwise, 0.
        ///     Windows 2000:  This value is not supported.
        /// </summary>
        SM_SHUTTINGDOWN = 0x2000,

        /// <summary>
        ///     Nonzero if the computer has a low-end (slow) processor; otherwise, 0.
        /// </summary>
        SM_SLOWMACHINE = 73,

        /// <summary>
        ///     Nonzero if the current operating system is Windows 7 Starter Edition, Windows Vista Starter, or Windows XP Starter
        ///     Edition; otherwise, 0.
        /// </summary>
        SM_STARTER = 88,

        /// <summary>
        ///     Nonzero if the meanings of the left and right mouse buttons are swapped; otherwise, 0.
        /// </summary>
        SM_SWAPBUTTON = 23,

        /// <summary>
        ///     Reflects the state of the docking mode, 0 for Undocked Mode and non-zero otherwise. When this system metric
        ///     changes, the system sends a broadcast message via WM_SETTINGCHANGE with "SystemDockMode" in the LPARAM.
        /// </summary>
        SM_SYSTEMDOCKED = 0x2004,

        /// <summary>
        ///     Nonzero if the current operating system is the Windows XP Tablet PC edition or if the current operating system is
        ///     Windows Vista or Windows 7 and the Tablet PC Input service is started; otherwise, 0. The SM_DIGITIZER setting
        ///     indicates the type of digitizer input supported by a device running Windows 7 or Windows Server 2008 R2. For more
        ///     information, see Remarks.
        /// </summary>
        SM_TABLETPC = 86,

        /// <summary>
        ///     The coordinates for the left side of the virtual screen. The virtual screen is the bounding
        ///     rectangle of all display monitors. The SM_CXVIRTUALSCREEN metric is the width
        ///     of the virtual screen.
        /// </summary>
        SM_XVIRTUALSCREEN = 76,

        /// <summary>
        ///     The coordinates for the top of the virtual screen. The virtual screen is the bounding
        ///     rectangle of all display monitors. The SM_CYVIRTUALSCREEN metric is the height of the virtual screen.
        /// </summary>
        SM_YVIRTUALSCREEN = 77
    }

    public enum SystemParametersAccessibilityInfo
    {
        /// <summary>
        ///     Retrieves information about the time-out period associated with the accessibility features. The pvParam parameter
        ///     must point to an ACCESSTIMEOUT structure that receives the information. Set the cbSize member of this structure and
        ///     the uiParam parameter to sizeof(ACCESSTIMEOUT).
        /// </summary>
        SPI_GETACCESSTIMEOUT = 0x003C,

        /// <summary>
        ///     Determines whether audio descriptions are enabled or disabled. The pvParam parameter is a pointer to an
        ///     AUDIODESCRIPTION structure. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(AUDIODESCRIPTION).
        ///     While it is possible for users who have visual impairments to hear the audio in video content, there is a lot of
        ///     action in video that does not have corresponding audio. Specific audio description of what is happening in a video
        ///     helps these users understand the content better. This flag enables you to determine whether audio descriptions have
        ///     been enabled and in which language.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETAUDIODESCRIPTION = 0x0074,

        /// <summary>
        ///     Determines whether animations are enabled or disabled. The pvParam parameter must point to a BOOL variable that
        ///     receives TRUE if animations are enabled, or FALSE otherwise.
        ///     Display features such as flashing, blinking, flickering, and moving content can cause seizures in users with
        ///     photo-sensitive epilepsy. This flag enables you to determine whether such animations have been disabled in the
        ///     client area.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETCLIENTAREAANIMATION = 0x1042,

        /// <summary>
        ///     Determines whether overlapped content is enabled or disabled. The pvParam parameter must point to a BOOL variable
        ///     that receives TRUE if enabled, or FALSE otherwise.
        ///     Display features such as background images, textured backgrounds, water marks on documents, alpha blending, and
        ///     transparency can reduce the contrast between the foreground and background, making it harder for users with low
        ///     vision to see objects on the screen. This flag enables you to determine whether such overlapped content has been
        ///     disabled.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETDISABLEOVERLAPPEDCONTENT = 0x1040,

        /// <summary>
        ///     Retrieves information about the FilterKeys accessibility feature. The pvParam parameter must point to a FILTERKEYS
        ///     structure that receives the information. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(FILTERKEYS).
        /// </summary>
        SPI_GETFILTERKEYS = 0x0032,

        /// <summary>
        ///     Retrieves the height, in pixels, of the top and bottom edges of the focus rectangle drawn with DrawFocusRect. The
        ///     pvParam parameter must point to a UINT value.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETFOCUSBORDERHEIGHT = 0x2010,

        /// <summary>
        ///     Retrieves the width, in pixels, of the left and right edges of the focus rectangle drawn with DrawFocusRect. The
        ///     pvParam parameter must point to a UINT.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETFOCUSBORDERWIDTH = 0x200E,

        /// <summary>
        ///     Retrieves information about the HighContrast accessibility feature. The pvParam parameter must point to a
        ///     HIGHCONTRAST structure that receives the information. Set the cbSize member of this structure and the uiParam
        ///     parameter to sizeof(HIGHCONTRAST).
        ///     For a general discussion, see Remarks.
        /// </summary>
        SPI_GETHIGHCONTRAST = 0x0042,

        /// <summary>
        ///     Retrieves a value that determines whether Windows 8 is displaying apps using the default scaling plateau for the
        ///     hardware or going to the next higher plateau. This value is based on the current "Make everything on your screen
        ///     bigger" setting, found in the Ease of Access section of PC settings: 1 is on, 0 is off.
        ///     Apps can provide text and image resources for each of several scaling plateaus: 100%, 140%, and 180%. Providing
        ///     separate resources optimized for a particular scale avoids distortion due to resizing. Windows 8 determines the
        ///     appropriate scaling plateau based on a number of factors, including screen size and pixel density. When "Make
        ///     everything on your screen bigger" is selected (SPI_GETLOGICALDPIOVERRIDE returns a value of 1), Windows uses
        ///     resources from the next higher plateau. For example, in the case of hardware that Windows determines should use a
        ///     scale of SCALE_100_PERCENT, this override causes Windows to use the SCALE_140_PERCENT scale value, assuming that it
        ///     does not violate other constraints.
        ///     Note  You should not use this value. It might be altered or unavailable in subsequent versions of Windows. Instead,
        ///     use the GetScaleFactorForDevice function or the DisplayProperties class to retrieve the preferred scaling factor.
        ///     Desktop applications should use desktop logical DPI rather than scale factor. Desktop logical DPI can be retrieved
        ///     through the GetDeviceCaps function.
        /// </summary>
        SPI_GETLOGICALDPIOVERRIDE = 0x009E,

        /// <summary>
        ///     Retrieves the time that notification pop-ups should be displayed, in seconds. The pvParam parameter must point to a
        ///     ULONG that receives the message duration.
        ///     Users with visual impairments or cognitive conditions such as ADHD and dyslexia might need a longer time to read
        ///     the text in notification messages. This flag enables you to retrieve the message duration.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETMESSAGEDURATION = 0x2016,

        /// <summary>
        ///     Retrieves the state of the Mouse ClickLock feature. The pvParam parameter must point to a BOOL variable that
        ///     receives TRUE if enabled, or FALSE otherwise. For more information, see About Mouse Input.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETMOUSECLICKLOCK = 0x101E,

        /// <summary>
        ///     Retrieves the time delay before the primary mouse button is locked. The pvParam parameter must point to DWORD that
        ///     receives the time delay, in milliseconds. This is only enabled if SPI_SETMOUSECLICKLOCK is set to TRUE. For more
        ///     information, see About Mouse Input.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETMOUSECLICKLOCKTIME = 0x2008,

        /// <summary>
        ///     Retrieves information about the MouseKeys accessibility feature. The pvParam parameter must point to a MOUSEKEYS
        ///     structure that receives the information. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(MOUSEKEYS).
        /// </summary>
        SPI_GETMOUSEKEYS = 0x0036,

        /// <summary>
        ///     Retrieves the state of the Mouse Sonar feature. The pvParam parameter must point to a BOOL variable that receives
        ///     TRUE if enabled or FALSE otherwise. For more information, see About Mouse Input.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETMOUSESONAR = 0x101C,

        /// <summary>
        ///     Retrieves the state of the Mouse Vanish feature. The pvParam parameter must point to a BOOL variable that receives
        ///     TRUE if enabled or FALSE otherwise. For more information, see About Mouse Input.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETMOUSEVANISH = 0x1020,

        /// <summary>
        ///     Determines whether a screen reviewer utility is running. A screen reviewer utility directs textual information to
        ///     an output device, such as a speech synthesizer or Braille display. When this flag is set, an application should
        ///     provide textual information in situations where it would otherwise present the information  graphically.
        ///     The pvParam parameter is a pointer to a BOOLvariable that receives TRUE if a screen reviewer utility is running, or
        ///     FALSE otherwise.
        ///     Note  Narrator, the screen reader that is included with Windows, does not set the SPI_SETSCREENREADER or
        ///     SPI_GETSCREENREADER flags.
        /// </summary>
        SPI_GETSCREENREADER = 0x0046,

        /// <summary>
        ///     This parameter is not supported.
        ///     Windows Server 2003 and Windows XP/2000:  The user should control this setting through the Control Panel.
        /// </summary>
        SPI_GETSERIALKEYS = 0x003E,

        /// <summary>
        ///     Determines whether the Show Sounds accessibility flag is on or off. If it is on, the user requires an application
        ///     to present information visually in situations where it would otherwise present the information only in audible
        ///     form. The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is on, or FALSE if it
        ///     is off.
        ///     Using this value is equivalent to calling GetSystemMetrics with SM_SHOWSOUNDS. That is the recommended call.
        /// </summary>
        SPI_GETSHOWSOUNDS = 0x0038,

        /// <summary>
        ///     Retrieves information about the SoundSentry accessibility feature. The pvParam parameter must point to a
        ///     SOUNDSENTRY structure that receives the information. Set the cbSize member of this structure and the uiParam
        ///     parameter to sizeof(SOUNDSENTRY).
        /// </summary>
        SPI_GETSOUNDSENTRY = 0x0040,

        /// <summary>
        ///     Retrieves information about the StickyKeys accessibility feature. The pvParam parameter must point to a STICKYKEYS
        ///     structure that receives the information. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(STICKYKEYS).
        /// </summary>
        SPI_GETSTICKYKEYS = 0x003A,

        /// <summary>
        ///     Retrieves information about the ToggleKeys accessibility feature. The pvParam parameter must point to a TOGGLEKEYS
        ///     structure that receives the information. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(TOGGLEKEYS).
        /// </summary>
        SPI_GETTOGGLEKEYS = 0x0034,

        /// <summary>
        ///     Sets the time-out period associated with the accessibility features. The pvParam parameter must point to an
        ///     ACCESSTIMEOUT structure that contains the new parameters. Set the cbSize member of this structure and the uiParam
        ///     parameter to sizeof(ACCESSTIMEOUT).
        /// </summary>
        SPI_SETACCESSTIMEOUT = 0x003D,

        /// <summary>
        ///     Turns the audio descriptions feature on or off. The pvParam parameter is a pointer to an AUDIODESCRIPTION
        ///     structure.
        ///     While it is possible for users who are visually impaired to hear the audio in video content, there is a lot of
        ///     action in video that does not have corresponding audio. Specific audio description of what is happening in a video
        ///     helps these users understand the content better. This flag enables you to enable or disable audio descriptions in
        ///     the languages they are provided in.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETAUDIODESCRIPTION = 0x0075,

        /// <summary>
        ///     Turns client area animations on or off. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to enable
        ///     animations and other transient effects in the client area, or FALSE to disable them.
        ///     Display features such as flashing, blinking, flickering, and moving content can cause seizures in users with
        ///     photo-sensitive epilepsy. This flag enables you to enable or disable all such animations.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETCLIENTAREAANIMATION = 0x1043,

        /// <summary>
        ///     Turns overlapped content (such as background images and watermarks) on or off. The pvParam parameter is a BOOL
        ///     variable. Set pvParam to TRUE to disable overlapped content, or FALSE to enable overlapped content.
        ///     Display features such as background images, textured backgrounds, water marks on documents, alpha blending, and
        ///     transparency can reduce the contrast between the foreground and background, making it harder for users with low
        ///     vision to see objects on the screen. This flag enables you to enable or disable all such overlapped content.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETDISABLEOVERLAPPEDCONTENT = 0x1041,

        /// <summary>
        ///     Sets the parameters of the FilterKeys accessibility feature. The pvParam parameter must point to a FILTERKEYS
        ///     structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(FILTERKEYS).
        /// </summary>
        SPI_SETFILTERKEYS = 0x0033,

        /// <summary>
        ///     Sets the height of the top and bottom edges of the focus rectangle drawn with DrawFocusRect to the value of the
        ///     pvParam parameter.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETFOCUSBORDERHEIGHT = 0x2011,

        /// <summary>
        ///     Sets the height of the left and right edges of the focus rectangle drawn with DrawFocusRect to the value of the
        ///     pvParam parameter.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETFOCUSBORDERWIDTH = 0x200F,

        /// <summary>
        ///     Sets the parameters of the HighContrast accessibility feature. The pvParam parameter must point to a HIGHCONTRAST
        ///     structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(HIGHCONTRAST).
        /// </summary>
        SPI_SETHIGHCONTRAST = 0x0043,

        /// <summary>
        ///     Do not use.
        /// </summary>
        SPI_SETLOGICALDPIOVERRIDE = 0x009F,

        /// <summary>
        ///     Sets the time that notification pop-ups should be displayed, in seconds. The pvParam parameter specifies the
        ///     message duration.
        ///     Users with visual impairments or cognitive conditions such as ADHD and dyslexia might need a longer time to read
        ///     the text in notification messages. This flag enables you to set the message duration.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETMESSAGEDURATION = 0x2017,

        /// <summary>
        ///     Turns the Mouse ClickLock accessibility feature on or off. This feature temporarily locks down the primary mouse
        ///     button when that button is clicked and held down for the time specified by SPI_SETMOUSECLICKLOCKTIME. The pvParam
        ///     parameter specifies TRUE for on, or FALSE for off. The default is off. For more information, see Remarks and
        ///     AboutMouse Input.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETMOUSECLICKLOCK = 0x101F,

        /// <summary>
        ///     Adjusts the time delay before the primary mouse button is locked. The uiParam parameter should be set to 0. The
        ///     pvParam parameter points to a DWORD that specifies the time delay in milliseconds. For example, specify 1000 for a
        ///     1 second delay. The default is 1200. For more information, see About Mouse Input.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETMOUSECLICKLOCKTIME = 0x2009,

        /// <summary>
        ///     Sets the parameters of the MouseKeys accessibility feature. The pvParam parameter must point to a MOUSEKEYS
        ///     structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(MOUSEKEYS).
        /// </summary>
        SPI_SETMOUSEKEYS = 0x0037,

        /// <summary>
        ///     Turns the Sonar accessibility feature on or off. This feature briefly shows several concentric circles around the
        ///     mouse pointer when the user presses and releases the CTRL key. The pvParam parameter specifies TRUE for on and
        ///     FALSE for off. The default is off. For more information, see About Mouse Input.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETMOUSESONAR = 0x101D,

        /// <summary>
        ///     Turns the Vanish feature on or off. This feature hides the mouse pointer when the user types; the pointer reappears
        ///     when the user moves the mouse. The pvParam parameter specifies TRUE for on and FALSE for off. The default is off.
        ///     For more information, see About Mouse Input.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETMOUSEVANISH = 0x1021,

        /// <summary>
        ///     Determines whether a screen review utility is running. The uiParam parameter specifies TRUE for on, or FALSE for
        ///     off.
        ///     Note  Narrator, the screen reader that is included with Windows, does not set the SPI_SETSCREENREADER or
        ///     SPI_GETSCREENREADER flags.
        /// </summary>
        SPI_SETSCREENREADER = 0x0047,

        /// <summary>
        ///     This parameter is not supported.
        ///     Windows Server 2003 and Windows XP/2000:  The user should control this setting through the Control Panel.
        /// </summary>
        SPI_SETSERIALKEYS = 0x003F,

        /// <summary>
        ///     Turns the ShowSounds accessibility feature on or off. The uiParam parameter specifies TRUE for on, or FALSE for
        ///     off.
        /// </summary>
        SPI_SETSHOWSOUNDS = 0x0039,

        /// <summary>
        ///     Sets the parameters of the SoundSentry accessibility feature. The pvParam parameter must point to a SOUNDSENTRY
        ///     structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(SOUNDSENTRY).
        /// </summary>
        SPI_SETSOUNDSENTRY = 0x0041,

        /// <summary>
        ///     Sets the parameters of the StickyKeys accessibility feature. The pvParam parameter must point to a STICKYKEYS
        ///     structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(STICKYKEYS).
        /// </summary>
        SPI_SETSTICKYKEYS = 0x003B,

        /// <summary>
        ///     Sets the parameters of the ToggleKeys accessibility feature. The pvParam parameter must point to a TOGGLEKEYS
        ///     structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(TOGGLEKEYS).
        /// </summary>
        SPI_SETTOGGLEKEYS = 0x0035
    }

    public enum SystemParametersDesktopInfo
    {
        /// <summary>
        ///     Determines whether ClearType is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE if
        ///     ClearType is enabled, or FALSE otherwise.
        ///     ClearType is a software technology that improves the readability of text on liquid crystal display (LCD) monitors.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETCLEARTYPE = 0x1048,

        /// <summary>
        ///     Retrieves the full path of the bitmap file for the desktop wallpaper. The pvParam parameter must point to a buffer
        ///     to receive the null-terminated path string. Set the uiParam parameter to the size, in characters, of the pvParam
        ///     buffer. The returned string will not exceed MAX_PATH characters. If there is no desktop wallpaper, the returned
        ///     string is empty.
        /// </summary>
        SPI_GETDESKWALLPAPER = 0x0073,

        /// <summary>
        ///     Determines whether the drop shadow effect is enabled. The pvParam parameter must point to a BOOL variable that
        ///     returns TRUE if enabled or FALSE if disabled.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETDROPSHADOW = 0x1024,

        /// <summary>
        ///     Determines whether native User menus have flat menu appearance. The pvParam parameter must point to a BOOL variable
        ///     that returns TRUE if the flat menu appearance is set, or FALSE otherwise.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETFLATMENU = 0x1022,

        /// <summary>
        ///     Determines whether the font smoothing feature is enabled. This feature uses font antialiasing to make font curves
        ///     appear smoother by painting pixels at different gray levels.
        ///     The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is enabled, or FALSE if  it
        ///     is not.
        /// </summary>
        SPI_GETFONTSMOOTHING = 0x004A,

        /// <summary>
        ///     Retrieves a contrast value that is used in ClearType smoothing. The pvParam parameter must point to a UINT that
        ///     receives the information. Valid contrast values are from 1000 to 2200. The default value is 1400.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETFONTSMOOTHINGCONTRAST = 0x200C,

        /// <summary>
        ///     Retrieves the font smoothing orientation. The pvParam parameter must point to a UINT that receives the information.
        ///     The possible values are FE_FONTSMOOTHINGORIENTATIONBGR (blue-green-red) and FE_FONTSMOOTHINGORIENTATIONRGB
        ///     (red-green-blue).
        ///     Windows XP/2000:  This parameter is not supported until Windows XP with SP2.
        /// </summary>
        SPI_GETFONTSMOOTHINGORIENTATION = 0x2012,

        /// <summary>
        ///     Retrieves the type of font smoothing. The pvParam parameter must point to a UINT that receives the information. The
        ///     possible values are FE_FONTSMOOTHINGSTANDARD and FE_FONTSMOOTHINGCLEARTYPE.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETFONTSMOOTHINGTYPE = 0x200A,

        /// <summary>
        ///     Retrieves the size of the work area on the primary display monitor. The work area is the portion of the screen not
        ///     obscured by the system taskbar or by application desktop toolbars. The pvParam parameter must point to a RECT
        ///     structure that receives the coordinates of the work area, expressed in virtual screen coordinates.
        ///     To get the work area of a monitor other than the primary display monitor, call the GetMonitorInfo function.
        /// </summary>
        SPI_GETWORKAREA = 0x0030,

        /// <summary>
        ///     Turns ClearType on or off. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to enable ClearType, or
        ///     FALSE to disable it.
        ///     ClearType is a software technology that improves the readability of text on LCD monitors.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETCLEARTYPE = 0x1049,

        /// <summary>
        ///     Reloads the system cursors. Set the uiParam parameter to zero and the pvParam parameter to NULL.
        /// </summary>
        SPI_SETCURSORS = 0x0057,

        /// <summary>
        ///     Sets the current desktop pattern by causing Windows to read the Pattern= setting from the WIN.INI file.
        /// </summary>
        SPI_SETDESKPATTERN = 0x0015,

        /// <summary>
        ///     Note  When the SPI_SETDESKWALLPAPER flag is used, SystemParametersInfo returns TRUE unless there is an error (like
        ///     when the specified file doesn't exist).
        /// </summary>
        SPI_SETDESKWALLPAPER = 0x0014,

        /// <summary>
        ///     Enables or disables the drop shadow effect. Set pvParam to TRUE to enable the drop shadow effect or FALSE to
        ///     disable it. You must also have CS_DROPSHADOW in the window class style.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETDROPSHADOW = 0x1025,

        /// <summary>
        ///     Enables or disables flat menu appearance for native User menus. Set pvParam to TRUE to enable flat menu appearance
        ///     or FALSE to disable it.
        ///     When enabled, the menu bar uses COLOR_MENUBAR for the menubar background, COLOR_MENU for the menu-popup background,
        ///     COLOR_MENUHILIGHT for the fill of the current menu selection, and COLOR_HILIGHT for the outline of the current menu
        ///     selection. If disabled, menus are drawn using the same metrics and colors as in Windows 2000.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETFLATMENU = 0x1023,

        /// <summary>
        ///     Enables or disables the font smoothing feature, which uses font antialiasing to make font curves appear smoother by
        ///     painting pixels at different gray levels.
        ///     To enable the feature, set the uiParam parameter to TRUE. To disable the feature, set uiParam to FALSE.
        /// </summary>
        SPI_SETFONTSMOOTHING = 0x004B,

        /// <summary>
        ///     Sets the contrast value used in ClearType smoothing. The pvParam parameter is the contrast value. Valid contrast
        ///     values are from 1000 to 2200. The default value is 1400.
        ///     SPI_SETFONTSMOOTHINGTYPE must also be set to FE_FONTSMOOTHINGCLEARTYPE.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETFONTSMOOTHINGCONTRAST = 0x200D,

        /// <summary>
        ///     Sets the font smoothing orientation. The pvParam parameter is either FE_FONTSMOOTHINGORIENTATIONBGR
        ///     (blue-green-red) or FE_FONTSMOOTHINGORIENTATIONRGB (red-green-blue).
        ///     Windows XP/2000:  This parameter is not supported until Windows XP with SP2.
        /// </summary>
        SPI_SETFONTSMOOTHINGORIENTATION = 0x2013,

        /// <summary>
        ///     Sets the font smoothing type. The pvParam parameter is either FE_FONTSMOOTHINGSTANDARD, if standard anti-aliasing
        ///     is used, or FE_FONTSMOOTHINGCLEARTYPE, if ClearType is used. The default is FE_FONTSMOOTHINGSTANDARD.
        ///     SPI_SETFONTSMOOTHING must also be set.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETFONTSMOOTHINGTYPE = 0x200B,

        /// <summary>
        ///     Sets the size of the work area. The work area is the portion of the screen not obscured by the system taskbar or by
        ///     application desktop toolbars. The pvParam parameter is a pointer to a RECT structure that specifies the new work
        ///     area rectangle, expressed in virtual screen coordinates. In a system with multiple display monitors, the function
        ///     sets the work area of the monitor that contains the specified rectangle.
        /// </summary>
        SPI_SETWORKAREA = 0x002F
    }

    public enum SystemParametersIconInfo
    {
        /// <summary>
        ///     Retrieves the metrics associated with icons. The pvParam parameter must point to an ICONMETRICS structure that
        ///     receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
        /// </summary>
        SPI_GETICONMETRICS = 0x002D,

        /// <summary>
        ///     Retrieves the logical font information for the current icon-title font. The uiParam parameter specifies the size of
        ///     a LOGFONT structure, and the pvParam parameter must point to the LOGFONT structure to fill in.
        /// </summary>
        SPI_GETICONTITLELOGFONT = 0x001F,

        /// <summary>
        ///     Determines whether icon-title wrapping is enabled. The pvParam parameter must point to a BOOL variable that
        ///     receives TRUE if enabled, or FALSE otherwise.
        /// </summary>
        SPI_GETICONTITLEWRAP = 0x0019,

        /// <summary>
        ///     Sets or retrieves the width, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large
        ///     icon view.
        ///     To set this value, set uiParam to the new value and set pvParam to NULL. You cannot set this value to less than
        ///     SM_CXICON.
        ///     To retrieve this value, pvParam must point to an integer that receives the  current value.
        /// </summary>
        SPI_ICONHORIZONTALSPACING = 0x000D,

        /// <summary>
        ///     Sets or retrieves the height, in pixels, of an icon cell.
        ///     To set this value, set uiParam to the new value and set pvParam to NULL. You cannot set this value to less than
        ///     SM_CYICON.
        ///     To retrieve this value, pvParam must point to an integer that receives the  current value.
        /// </summary>
        SPI_ICONVERTICALSPACING = 0x0018,

        /// <summary>
        ///     Sets the metrics associated with icons. The pvParam parameter must point to an ICONMETRICS structure that contains
        ///     the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
        /// </summary>
        SPI_SETICONMETRICS = 0x002E,

        /// <summary>
        ///     Reloads the system icons. Set the uiParam parameter to zero and the pvParam parameter to NULL.
        /// </summary>
        SPI_SETICONS = 0x0058,

        /// <summary>
        ///     Sets the font that is used for icon titles. The uiParam parameter specifies the size of a LOGFONT structure, and
        ///     the pvParam parameter must point to a LOGFONT structure.
        /// </summary>
        SPI_SETICONTITLELOGFONT = 0x0022,

        /// <summary>
        ///     Turns icon-title wrapping on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
        /// </summary>
        SPI_SETICONTITLEWRAP = 0x001A
    }

    public enum SystemParametersInputInfo
    {
        /// <summary>
        ///     Determines whether the warning beeper is on.
        ///     The pvParam parameter must point to a BOOL variable that receives TRUE if the beeper is on, or FALSE if it is off.
        /// </summary>
        SPI_GETBEEP = 0x0001,

        /// <summary>
        ///     Retrieves a BOOL indicating whether an application can reset the screensaver's timer by calling the SendInput
        ///     function to simulate keyboard or mouse input. The pvParam parameter must point to a BOOL variable that receives
        ///     TRUE if the simulated input will be blocked, or FALSE otherwise.
        /// </summary>
        SPI_GETBLOCKSENDINPUTRESETS = 0x1026,

        /// <summary>
        ///     Retrieves the current contact visualization setting. The pvParam parameter must point to a ULONG variable that
        ///     receives the setting. For more information, see Contact Visualization.
        /// </summary>
        SPI_GETCONTACTVISUALIZATION = 0x2018,

        /// <summary>
        ///     Retrieves the input locale identifier for the system default input language. The pvParam parameter must point to an
        ///     HKL variable that receives this value. For more information, see Languages, Locales, and Keyboard Layouts.
        /// </summary>
        SPI_GETDEFAULTINPUTLANG = 0x0059,

        /// <summary>
        ///     Retrieves the current gesture visualization setting. The pvParam parameter must point to a ULONG variable that
        ///     receives the setting. For more information, see Gesture Visualization.
        /// </summary>
        SPI_GETGESTUREVISUALIZATION = 0x201A,

        /// <summary>
        ///     Determines whether menu access keys are always underlined. The pvParam parameter must point to a BOOL variable that
        ///     receives TRUE if menu access keys are always underlined, and FALSE if they are underlined only when the menu is
        ///     activated by the keyboard.
        /// </summary>
        SPI_GETKEYBOARDCUES = 0x100A,

        /// <summary>
        ///     Retrieves the keyboard repeat-delay setting, which is a value in the range from 0 (approximately 250 ms delay)
        ///     through 3 (approximately 1 second delay). The actual delay associated with each value may vary depending on the
        ///     hardware. The pvParam parameter must point to an integer variable that receives the setting.
        /// </summary>
        SPI_GETKEYBOARDDELAY = 0x0016,

        /// <summary>
        ///     Determines whether the user relies on the keyboard instead of the mouse, and wants applications to display keyboard
        ///     interfaces that would otherwise be hidden. The pvParam parameter must point to a BOOL variable that receives TRUE
        ///     if the user relies on the keyboard; or FALSE otherwise.
        /// </summary>
        SPI_GETKEYBOARDPREF = 0x0044,

        /// <summary>
        ///     Retrieves the keyboard repeat-speed setting, which is a value in the range from 0 (approximately 2.5 repetitions
        ///     per second) through 31 (approximately 30 repetitions per second). The actual repeat rates are hardware-dependent
        ///     and may vary from a linear scale by as much as 20%. The pvParam parameter must point to a DWORD variable that
        ///     receives the setting.
        /// </summary>
        SPI_GETKEYBOARDSPEED = 0x000A,

        /// <summary>
        ///     Retrieves the two mouse threshold values and the mouse acceleration. The pvParam parameter must point to an array
        ///     of three integers that receives these values. See mouse_event for further information.
        /// </summary>
        SPI_GETMOUSE = 0x0003,

        /// <summary>
        ///     Retrieves the height, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent to
        ///     generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the height.
        /// </summary>
        SPI_GETMOUSEHOVERHEIGHT = 0x0064,

        /// <summary>
        ///     Retrieves the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle for TrackMouseEvent
        ///     to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the time.
        /// </summary>
        SPI_GETMOUSEHOVERTIME = 0x0066,

        /// <summary>
        ///     Retrieves the width, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent to
        ///     generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the width.
        /// </summary>
        SPI_GETMOUSEHOVERWIDTH = 0x0062,

        /// <summary>
        ///     Retrieves the current mouse speed. The mouse speed determines how far the pointer will move based on the distance
        ///     the mouse moves. The pvParam parameter must point to an integer that receives a value which ranges between 1
        ///     (slowest) and 20 (fastest). A value of 10 is the default. The value can be set by an end-user using the mouse
        ///     control panel application or by an application using SPI_SETMOUSESPEED.
        /// </summary>
        SPI_GETMOUSESPEED = 0x0070,

        /// <summary>
        ///     Determines whether the Mouse Trails feature is enabled. This feature improves the visibility of mouse cursor
        ///     movements by briefly showing a trail of cursors and quickly erasing them.
        ///     The pvParam parameter must point to an integer variable that receives a value. if  the value is zero or 1, the
        ///     feature is disabled. If the value is greater than 1, the feature is enabled and the value indicates the number of
        ///     cursors drawn in the trail. The uiParam parameter is not used.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_GETMOUSETRAILS = 0x005E,

        /// <summary>
        ///     Retrieves the routing setting for wheel button input. The routing setting determines whether wheel button input is
        ///     sent to the app with focus (foreground) or the app under the mouse cursor.
        ///     The pvParam parameter must point to a DWORD variable that receives the routing option.
        ///     If  the value is zero or MOUSEWHEEL_ROUTING_FOCUS, mouse wheel input is delivered to the app with focus. If the
        ///     value is 1 or MOUSEWHEEL_ROUTING_HYBRID (default), mouse wheel input is delivered to the app with focus (desktop
        ///     apps) or the app under the mouse cursor (Windows Store apps).
        ///     The uiParam parameter is not used.
        /// </summary>
        SPI_GETMOUSEWHEELROUTING = 0x201C,

        /// <summary>
        ///     Retrieves the current pen gesture visualization setting. The pvParam parameter must point to a ULONG variable that
        ///     receives the setting. For more information, see Pen Visualization.
        /// </summary>
        SPI_GETPENVISUALIZATION = 0x201E,

        /// <summary>
        ///     Determines whether the snap-to-default-button feature is enabled. If enabled, the mouse cursor automatically moves
        ///     to the default button, such as OK or Apply, of a dialog box. The pvParam parameter must point to a BOOL variable
        ///     that receives TRUE if the feature is on, or FALSE if it is off.
        /// </summary>
        SPI_GETSNAPTODEFBUTTON = 0x005F,

        /// <summary>
        ///     Starting with Windows 8: Determines whether the system language bar is enabled or disabled. The pvParam parameter
        ///     must point to a BOOL variable that receives TRUE if the language bar is enabled, or FALSE otherwise.
        /// </summary>
        SPI_GETSYSTEMLANGUAGEBAR = 0x1050,

        /// <summary>
        ///     Starting with Windows 8: Determines whether the active input settings have Local (per-thread, TRUE) or Global
        ///     (session, FALSE) scope. The pvParam parameter must point to a BOOL variable.
        /// </summary>
        SPI_GETTHREADLOCALINPUTSETTINGS = 0x104E,

        /// <summary>
        ///     Retrieves the number of characters to scroll when the horizontal mouse wheel is moved. The pvParam parameter must
        ///     point to a UINT variable that receives the number of lines. The default value is 3.
        /// </summary>
        SPI_GETWHEELSCROLLCHARS = 0x006C,

        /// <summary>
        ///     Retrieves the number of lines to scroll when the vertical mouse wheel is moved. The pvParam parameter must point to
        ///     a UINT variable that receives the number of lines. The default value is 3.
        /// </summary>
        SPI_GETWHEELSCROLLLINES = 0x0068,

        /// <summary>
        ///     Turns the warning beeper on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
        /// </summary>
        SPI_SETBEEP = 0x0002,

        /// <summary>
        ///     Determines whether an application can reset the screensaver's timer by calling the SendInput function to simulate
        ///     keyboard or mouse input. The uiParam parameter specifies TRUE if the screensaver will not be deactivated by
        ///     simulated input, or FALSE if the screensaver will be deactivated by simulated input.
        /// </summary>
        SPI_SETBLOCKSENDINPUTRESETS = 0x1027,

        /// <summary>
        ///     Sets the current contact visualization setting. The pvParam parameter must point to a ULONG variable that
        ///     identifies the setting. For more information, see Contact Visualization.
        ///     Note  If contact visualizations are disabled, gesture visualizations cannot be enabled.
        /// </summary>
        SPI_SETCONTACTVISUALIZATION = 0x2019,

        /// <summary>
        ///     Sets the default input language for the system shell and applications. The specified language must be displayable
        ///     using the current system character set. The pvParam parameter must point to an HKL variable that contains the input
        ///     locale identifier for the default language. For more information, see Languages, Locales, and Keyboard Layouts.
        /// </summary>
        SPI_SETDEFAULTINPUTLANG = 0x005A,

        /// <summary>
        ///     Sets the double-click time for the mouse to the value of the uiParam parameter. If the uiParam value is greater
        ///     than 5000 milliseconds, the system sets the double-click time to 5000 milliseconds.
        ///     The double-click time is the maximum number of milliseconds that can occur between the first and second clicks of a
        ///     double-click. You can also call the SetDoubleClickTime function to set the double-click time. To get the current
        ///     double-click time, call the GetDoubleClickTime function.
        /// </summary>
        SPI_SETDOUBLECLICKTIME = 0x0020,

        /// <summary>
        ///     Sets the height of the double-click rectangle to the value of the uiParam parameter.
        ///     The double-click rectangle is the rectangle within which the second click of a double-click must fall for it to be
        ///     registered as a double-click.
        ///     To retrieve the height of the double-click rectangle, call  GetSystemMetrics with the SM_CYDOUBLECLK flag.
        /// </summary>
        SPI_SETDOUBLECLKHEIGHT = 0x001E,

        /// <summary>
        ///     Sets the width of the double-click rectangle to the value of the uiParam parameter.
        ///     The double-click rectangle is the rectangle within which the second click of a double-click must fall for it to be
        ///     registered as a double-click.
        ///     To retrieve the width of the double-click rectangle, call GetSystemMetrics with the SM_CXDOUBLECLK flag.
        /// </summary>
        SPI_SETDOUBLECLKWIDTH = 0x001D,

        /// <summary>
        ///     Sets the current gesture visualization setting. The pvParam parameter must point to a ULONG variable that
        ///     identifies the setting. For more information, see Gesture Visualization.
        ///     Note  If contact visualizations are disabled, gesture visualizations cannot be enabled.
        /// </summary>
        SPI_SETGESTUREVISUALIZATION = 0x201B,

        /// <summary>
        ///     Sets the underlining of menu access key letters. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to
        ///     always underline menu access keys, or FALSE to underline menu access keys only when the menu is activated from the
        ///     keyboard.
        /// </summary>
        SPI_SETKEYBOARDCUES = 0x100B,

        /// <summary>
        ///     Sets the keyboard repeat-delay setting. The uiParam parameter must specify 0, 1, 2, or 3, where zero sets the
        ///     shortest delay approximately 250 ms) and 3 sets the longest delay (approximately 1 second). The actual delay
        ///     associated with each value may vary depending on the hardware.
        /// </summary>
        SPI_SETKEYBOARDDELAY = 0x0017,

        /// <summary>
        ///     Sets the keyboard preference. The uiParam parameter specifies TRUE if the user relies on the keyboard instead of
        ///     the mouse, and wants applications to display keyboard interfaces that would otherwise be hidden; uiParam is FALSE
        ///     otherwise.
        /// </summary>
        SPI_SETKEYBOARDPREF = 0x0045,

        /// <summary>
        ///     Sets the keyboard repeat-speed setting. The uiParam parameter must specify a value in the range from 0
        ///     (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second). The actual repeat
        ///     rates are hardware-dependent and may vary from a linear scale by as much as 20%. If uiParam is greater than 31, the
        ///     parameter is set to 31.
        /// </summary>
        SPI_SETKEYBOARDSPEED = 0x000B,

        /// <summary>
        ///     Sets the hot key set for switching between input languages. The uiParam and pvParam parameters are not used. The
        ///     value sets the shortcut keys in the keyboard property sheets by reading the registry again. The registry must be
        ///     set before this flag is used. the path in the registry is
        ///     HKEY_CURRENT_USER\Keyboard Layout\Toggle. Valid values are "1" = ALT+SHIFT, "2" = CTRL+SHIFT, and "3" = none.
        /// </summary>
        SPI_SETLANGTOGGLE = 0x005B,

        /// <summary>
        ///     Sets the two mouse threshold values and the mouse acceleration. The pvParam parameter must point to an array of
        ///     three integers that specifies these values. See mouse_event for further information.
        /// </summary>
        SPI_SETMOUSE = 0x0004,

        /// <summary>
        ///     Swaps or restores the meaning of the left and right mouse buttons. The uiParam parameter specifies TRUE to swap the
        ///     meanings of the buttons, or FALSE to restore their original meanings.
        ///     To retrieve the current setting, call GetSystemMetrics with the SM_SWAPBUTTON flag.
        /// </summary>
        SPI_SETMOUSEBUTTONSWAP = 0x0021,

        /// <summary>
        ///     Sets the height, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent to
        ///     generate a WM_MOUSEHOVER message. Set the uiParam parameter to the new height.
        /// </summary>
        SPI_SETMOUSEHOVERHEIGHT = 0x0065,

        /// <summary>
        ///     Sets the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle for TrackMouseEvent to
        ///     generate a WM_MOUSEHOVER message. This is used only if you pass HOVER_DEFAULT in the dwHoverTime parameter in the
        ///     call to TrackMouseEvent. Set the uiParamparameter to the new time.
        ///     The time specified should be between USER_TIMER_MAXIMUM and USER_TIMER_MINIMUM. If uiParam is less than
        ///     USER_TIMER_MINIMUM, the function will use USER_TIMER_MINIMUM. If uiParam is greater than USER_TIMER_MAXIMUM, the
        ///     function will be USER_TIMER_MAXIMUM.
        ///     Windows Server 2003 and Windows XP:  The operating system does not enforce the use of USER_TIMER_MAXIMUM and
        ///     USER_TIMER_MINIMUM until Windows Server 2003 with SP1 and Windows XP with SP2.
        /// </summary>
        SPI_SETMOUSEHOVERTIME = 0x0067,

        /// <summary>
        ///     Sets the width, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent to
        ///     generate a WM_MOUSEHOVER message. Set the uiParam parameter to the new width.
        /// </summary>
        SPI_SETMOUSEHOVERWIDTH = 0x0063,

        /// <summary>
        ///     Sets the current mouse speed. The pvParam parameter is an integer between 1 (slowest) and 20 (fastest). A value of
        ///     10 is the default. This value is typically set using the mouse control panel application.
        /// </summary>
        SPI_SETMOUSESPEED = 0x0071,

        /// <summary>
        ///     Enables or disables the Mouse Trails feature, which improves the visibility of mouse cursor movements by briefly
        ///     showing a trail of cursors and quickly erasing them.
        ///     To disable the feature, set the uiParam parameter to zero or 1. To enable the  feature, set uiParam to a value
        ///     greater than 1 to indicate the number of cursors drawn in the trail.
        ///     Windows 2000:  This parameter is not supported.
        /// </summary>
        SPI_SETMOUSETRAILS = 0x005D,

        /// <summary>
        ///     Sets the routing setting for wheel button input. The routing setting determines whether wheel button input is sent
        ///     to the app with focus (foreground) or the app under the mouse cursor.
        ///     The pvParam parameter must point to a DWORD variable that receives the routing option.
        ///     If  the value is zero or MOUSEWHEEL_ROUTING_FOCUS, mouse wheel input is delivered to the app with focus. If the
        ///     value is 1 or MOUSEWHEEL_ROUTING_HYBRID (default), mouse wheel input is delivered to the app with focus (desktop
        ///     apps) or the app under the mouse cursor (Windows Store apps).
        ///     Set the uiParam parameter to zero.
        /// </summary>
        SPI_SETMOUSEWHEELROUTING = 0x201D,

        /// <summary>
        ///     Sets the current pen gesture visualization setting. The pvParam parameter must point to a ULONG variable that
        ///     identifies the setting. For more information, see Pen Visualization.
        /// </summary>
        SPI_SETPENVISUALIZATION = 0x201F,

        /// <summary>
        ///     Enables or disables the snap-to-default-button feature. If enabled, the mouse cursor automatically moves to the
        ///     default button, such as OK or Apply, of a dialog box. Set the uiParam parameter to TRUE to enable the feature, or
        ///     FALSE to disable it. Applications should use the ShowWindow function when displaying a dialog box so the dialog
        ///     manager can position the mouse cursor.
        /// </summary>
        SPI_SETSNAPTODEFBUTTON = 0x0060,

        /// <summary>
        ///     Starting with Windows 8: Turns the legacy language bar feature on or off. The pvParam parameter is a pointer to a
        ///     BOOL variable. Set pvParam to TRUE to enable the legacy language bar, or FALSE to disable it. The flag is supported
        ///     on Windows 8 where the legacy language bar is replaced by Input Switcher and therefore turned off by default.
        ///     Turning the legacy language bar on is provided for compatibility reasons and has no effect on the Input Switcher.
        /// </summary>
        SPI_SETSYSTEMLANGUAGEBAR = 0x1051,

        /// <summary>
        ///     Starting with Windows 8: Determines whether the active input settings have Local (per-thread, TRUE) or Global
        ///     (session, FALSE) scope. The pvParam parameter must point to a BOOL variable, casted by PVOID.
        /// </summary>
        SPI_SETTHREADLOCALINPUTSETTINGS = 0x104F,

        /// <summary>
        ///     Sets the number of characters to scroll when the horizontal mouse wheel is moved. The number of characters is set
        ///     from the uiParam parameter.
        /// </summary>
        SPI_SETWHEELSCROLLCHARS = 0x006D,

        /// <summary>
        ///     Sets the number of lines to scroll when the vertical mouse wheel is moved. The number of lines is set from the
        ///     uiParam parameter.
        ///     The number of lines is the suggested number of lines to scroll when the mouse wheel is rolled without using
        ///     modifier keys. If the number is 0, then no scrolling should occur. If the number of lines to scroll is  greater
        ///     than the number of lines viewable, and in particular if it is WHEEL_PAGESCROLL (#defined as UINT_MAX), the scroll
        ///     operation should be interpreted as clicking once in the page down or page up regions of the scroll bar.
        /// </summary>
        SPI_SETWHEELSCROLLLINES = 0x0069
    }

    public enum SystemParametersMenuInfo
    {
        /// <summary>
        ///     Determines whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu-bar item. The
        ///     pvParam parameter must point to a BOOL variable that receives TRUE if right-aligned, or FALSE otherwise.
        /// </summary>
        SPI_GETMENUDROPALIGNMENT = 0x001B,

        /// <summary>
        ///     Determines whether menu fade animation is enabled. The pvParam parameter must point to a BOOL variable that
        ///     receives TRUE when fade animation is enabled and FALSE when it isdisabled. If fade animation is disabled, menus use
        ///     slide animation. This flag is ignored unless menu animation is enabled, which you can do using the
        ///     SPI_SETMENUANIMATION flag. For more information, see AnimateWindow.
        /// </summary>
        SPI_GETMENUFADE = 0x1012,

        /// <summary>
        ///     Retrieves the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor
        ///     is over a submenu item. The pvParam parameter must point to a DWORD variable that receives the time of the delay.
        /// </summary>
        SPI_GETMENUSHOWDELAY = 0x006A,

        /// <summary>
        ///     Sets the alignment value of pop-up menus. The uiParam parameter specifies TRUE for right alignment, or FALSE for
        ///     left alignment.
        /// </summary>
        SPI_SETMENUDROPALIGNMENT = 0x001C,

        /// <summary>
        ///     Enables or disables menu fade animation. Set pvParam to TRUE to enable the menu fade effect or FALSE to disable it.
        ///     If fade animation is disabled, menus use slide animation. he The menu fade effect is possible only if the system
        ///     has a color depth of more than 256 colors. This flag is ignored unless SPI_MENUANIMATION is also set. For more
        ///     information, see AnimateWindow.
        /// </summary>
        SPI_SETMENUFADE = 0x1013,

        /// <summary>
        ///     Sets uiParam to the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse
        ///     cursor is over a submenu item.
        /// </summary>
        SPI_SETMENUSHOWDELAY = 0x006B
    }

    public enum SystemParametersPowerInfo
    {
        /// <summary>
        ///     This parameter is not supported.
        ///     Windows Server 2003 and Windows XP/2000:  Determines whether the low-power phase of screen saving is enabled. The
        ///     pvParam parameter must point to a BOOL variable that receives TRUE if enabled, or FALSE if disabled. This flag is
        ///     supported for 32-bit applications only.
        /// </summary>
        SPI_GETLOWPOWERACTIVE = 0x0053,

        /// <summary>
        ///     This parameter is not supported.
        ///     Windows Server 2003 and Windows XP/2000:  Retrieves the time-out value for the low-power phase of screen saving.
        ///     The pvParam parameter must point to an integer variable that receives the value. This flag is supported for 32-bit
        ///     applications only.
        /// </summary>
        SPI_GETLOWPOWERTIMEOUT = 0x004F,

        /// <summary>
        ///     This parameter is not supported. When the power-off phase of screen saving is enabled, the
        ///     GUID_VIDEO_POWERDOWN_TIMEOUT power setting is greater than zero.
        ///     Windows Server 2003 and Windows XP/2000:  Determines whether the power-off phase of screen saving is enabled. The
        ///     pvParam parameter must point to a BOOL variable that receives TRUE if enabled, or FALSE if disabled. This flag is
        ///     supported for 32-bit applications only.
        /// </summary>
        SPI_GETPOWEROFFACTIVE = 0x0054,

        /// <summary>
        ///     This parameter is not supported. Instead, check the GUID_VIDEO_POWERDOWN_TIMEOUT power setting.
        ///     Windows Server 2003 and Windows XP/2000:  Retrieves the time-out value for the power-off phase of screen saving.
        ///     The pvParam parameter must point to an integer variable that receives the value. This flag is supported for 32-bit
        ///     applications only.
        /// </summary>
        SPI_GETPOWEROFFTIMEOUT = 0x0050,

        /// <summary>
        ///     This parameter is not supported.
        ///     Windows Server 2003 and Windows XP/2000:  Activates or deactivates the low-power phase of screen saving. Set
        ///     uiParam to 1 to activate, or zero to deactivate. The pvParam parameter must be NULL. This flag is supported for
        ///     32-bit applications only.
        /// </summary>
        SPI_SETLOWPOWERACTIVE = 0x0055,

        /// <summary>
        ///     This parameter is not supported.
        ///     Windows Server 2003 and Windows XP/2000:  Sets the time-out value, in seconds, for the low-power phase of screen
        ///     saving. The uiParam parameter specifies the new value. The pvParam parameter must be NULL. This flag is supported
        ///     for 32-bit applications only.
        /// </summary>
        SPI_SETLOWPOWERTIMEOUT = 0x0051,

        /// <summary>
        ///     This parameter is not supported. Instead, set the GUID_VIDEO_POWERDOWN_TIMEOUT power setting.
        ///     Windows Server 2003 and Windows XP/2000:  Activates or deactivates the power-off phase of screen saving. Set
        ///     uiParam to 1 to activate, or zero to deactivate. The pvParam parameter must be NULL. This flag is supported for
        ///     32-bit applications only.
        /// </summary>
        SPI_SETPOWEROFFACTIVE = 0x0056,

        /// <summary>
        ///     This parameter is not supported. Instead, set the GUID_VIDEO_POWERDOWN_TIMEOUT power setting to a time-out value.
        ///     Windows Server 2003 and Windows XP/2000:  Sets the time-out value, in seconds, for the power-off phase of screen
        ///     saving. The uiParam parameter specifies the new value. The pvParam parameter must be NULL. This flag is supported
        ///     for 32-bit applications only.
        /// </summary>
        SPI_SETPOWEROFFTIMEOUT = 0x0052
    }

    public enum SystemParametersScreenSaverInfo
    {
        /// <summary>
        ///     Determines whether screen saving is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE
        ///     if screen saving is enabled, or FALSE otherwise.
        ///     Windows 7, Windows Server 2008 R2, and Windows 2000:  The function returns TRUE even when screen saving is not
        ///     enabled. For more information and a workaround, see KB318781.
        /// </summary>
        SPI_GETSCREENSAVEACTIVE = 0x0010,

        /// <summary>
        ///     Determines whether a screen saver is currently running on the window station of the calling process. The pvParam
        ///     parameter must point to a BOOL variable that receives TRUE if a screen saver is currently running, or FALSE
        ///     otherwise. Note that only the interactive window station, WinSta0, can have a screen saver running.
        /// </summary>
        SPI_GETSCREENSAVERRUNNING = 0x0072,

        /// <summary>
        ///     Determines whether the screen saver  requires a password to display the Windows desktop. The pvParam parameter must
        ///     point to a BOOL variable that receives TRUE if the screen saver requires a password, or FALSE otherwise. The
        ///     uiParam parameter is ignored.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETSCREENSAVESECURE = 0x0076,

        /// <summary>
        ///     Retrieves the screen saver time-out value, in seconds. The pvParam parameter must point to an integer variable that
        ///     receives the value.
        /// </summary>
        SPI_GETSCREENSAVETIMEOUT = 0x000E,

        /// <summary>
        ///     Sets the state of the screen saver. The uiParam parameter specifies TRUE to activate screen saving, or FALSE to
        ///     deactivate it.
        ///     If the machine has entered power saving mode or system lock state, an ERROR_OPERATION_IN_PROGRESS exception occurs.
        /// </summary>
        SPI_SETSCREENSAVEACTIVE = 0x0011,

        /// <summary>
        ///     Sets whether the screen saver requires the user to enter a password to display the Windows desktop. The uiParam
        ///     parameter is a BOOL variable. The pvParam parameter is ignored. Set uiParam to TRUE to require a password, or FALSE
        ///     to not require a password.
        ///     If the machine has entered power saving mode or system lock state, an ERROR_OPERATION_IN_PROGRESS exception occurs.
        ///     Windows Server 2003 and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETSCREENSAVESECURE = 0x0077,

        /// <summary>
        ///     Sets the screen saver time-out value to the value of the uiParam parameter. This value is the amount of time, in
        ///     seconds, that the system must be idle before the screen saver activates.
        ///     If the machine has entered power saving mode or system lock state, an ERROR_OPERATION_IN_PROGRESS exception occurs.
        /// </summary>
        SPI_SETSCREENSAVETIMEOUT = 0x000F
    }

    public enum SystemParametersTimeoutInfo
    {
        /// <summary>
        ///     Retrieves the number of milliseconds that a thread can go without dispatching a message before the system considers
        ///     it unresponsive. The pvParam parameter must point to an integer variable that receives the value.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETHUNGAPPTIMEOUT = 0x0078,

        /// <summary>
        ///     Retrieves the number of milliseconds that the system waits before terminating an application that does not respond
        ///     to a shutdown request. The pvParam parameter must point to an integer variable that receives the value.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETWAITTOKILLTIMEOUT = 0x007A,

        /// <summary>
        ///     Retrieves the number of milliseconds that the service control manager waits before terminating a service that does
        ///     not respond to a shutdown request. The pvParam parameter must point to an integer variable that receives the value.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETWAITTOKILLSERVICETIMEOUT = 0x007C,

        /// <summary>
        ///     Sets the hung application time-out to the value of the uiParam parameter. This value is the number of milliseconds
        ///     that a thread can go without dispatching a message before the system considers it unresponsive.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETHUNGAPPTIMEOUT = 0x0079,

        /// <summary>
        ///     Sets the application shutdown request time-out to the value of the uiParam parameter. This value is the number of
        ///     milliseconds that the system waits before terminating an application that does not respond to a shutdown request.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETWAITTOKILLTIMEOUT = 0x007B,

        /// <summary>
        ///     Sets the service shutdown request time-out to the value of the uiParam parameter. This value is the number of
        ///     milliseconds that the system waits before terminating a service that does not respond to a shutdown request.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETWAITTOKILLSERVICETIMEOUT = 0x007D
    }

    public enum SystemParametersUiEffectsInfo
    {
        /// <summary>
        ///     Determines whether the slide-open effect for combo boxes is enabled. The pvParam parameter must point to a BOOL
        ///     variable that receives TRUE for enabled, or FALSE for disabled.
        /// </summary>
        SPI_GETCOMBOBOXANIMATION = 0x1004,

        /// <summary>
        ///     Determines whether the cursor has a shadow around it. The pvParam parameter must point to a BOOL variable that
        ///     receives TRUE if the shadow is enabled, FALSE if it is disabled. This effect appears only if the system has a color
        ///     depth of more than 256 colors.
        /// </summary>
        SPI_GETCURSORSHADOW = 0x101A,

        /// <summary>
        ///     Determines whether the gradient effect for window title bars is enabled. The pvParam parameter must point to a BOOL
        ///     variable that receives TRUE for enabled, or FALSE for disabled. For more information about the gradient effect, see
        ///     the GetSysColor function.
        /// </summary>
        SPI_GETGRADIENTCAPTIONS = 0x1008,

        /// <summary>
        ///     Determines whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled. The
        ///     pvParam parameter must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
        ///     Hot tracking means that when the cursor moves over an item, it is highlighted but not selected. You can query this
        ///     value to decide whether to use hot tracking in the user interface of your application.
        /// </summary>
        SPI_GETHOTTRACKING = 0x100E,

        /// <summary>
        ///     Determines whether the smooth-scrolling effect for list boxes is enabled. The pvParam parameter must point to a
        ///     BOOL variable that receives TRUE for enabled, or FALSE for disabled.
        /// </summary>
        SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006,

        /// <summary>
        ///     Determines whether the menu animation feature is enabled. This master switch must be on to enable menu animation
        ///     effects. The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE
        ///     if it is disabled.
        ///     If animation is enabled, SPI_GETMENUFADE indicates whether menus use fade or slide animation.
        /// </summary>
        SPI_GETMENUANIMATION = 0x1002,

        /// <summary>
        ///     Same as SPI_GETKEYBOARDCUES.
        /// </summary>
        SPI_GETMENUUNDERLINES = 0x100A,

        /// <summary>
        ///     Determines whether the selection fade effect is enabled. The pvParam parameter must point to a BOOL variable that
        ///     receives TRUE if enabled or FALSE if disabled.
        ///     The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading
        ///     out after the menu is dismissed.
        /// </summary>
        SPI_GETSELECTIONFADE = 0x1014,

        /// <summary>
        ///     Determines whether ToolTip animation is enabled. The pvParam parameter must point to a BOOL variable that receives
        ///     TRUE if enabled or FALSE if disabled. If ToolTip animation is enabled, SPI_GETTOOLTIPFADE indicates whether
        ///     ToolTips use fade or slide animation.
        /// </summary>
        SPI_GETTOOLTIPANIMATION = 0x1016,

        /// <summary>
        ///     If SPI_SETTOOLTIPANIMATION is enabled, SPI_GETTOOLTIPFADE indicates whether ToolTip animation uses a fade effect or
        ///     a slide effect. The pvParam parameter must point to a BOOL variable that receives TRUE for fade animation or FALSE
        ///     for slide animation. For more information on slide and fade effects, see AnimateWindow.
        /// </summary>
        SPI_GETTOOLTIPFADE = 0x1018,

        /// <summary>
        ///     Determines whether UI effects are enabled or disabled. The pvParam parameter must point to a BOOL variable that
        ///     receives TRUE if all UI effects are enabled, or FALSE if they are disabled.
        /// </summary>
        SPI_GETUIEFFECTS = 0x103E,

        /// <summary>
        ///     Enables or disables the slide-open effect for combo boxes. Set the pvParam parameter to TRUE to enable the gradient
        ///     effect, or FALSE to disable it.
        /// </summary>
        SPI_SETCOMBOBOXANIMATION = 0x1005,

        /// <summary>
        ///     Enables or disables a shadow around the cursor. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to
        ///     enable the shadow or FALSE to disable the shadow. This effect appears only if the system has a color depth of more
        ///     than 256 colors.
        /// </summary>
        SPI_SETCURSORSHADOW = 0x101B,

        /// <summary>
        ///     Enables or disables the gradient effect for window title bars. Set the pvParam parameter to TRUE to enable it, or
        ///     FALSE to disable it. The gradient effect is possible only if the system has a color depth of more than 256 colors.
        ///     For more information about the gradient effect, see the GetSysColor function.
        /// </summary>
        SPI_SETGRADIENTCAPTIONS = 0x1009,

        /// <summary>
        ///     Enables or disables hot tracking of user-interface elements such as menu names on menu bars. Set the pvParam
        ///     parameter to TRUE to enable it, or FALSE to disable it.
        ///     Hot-tracking means that when the cursor moves over an item, it is highlighted but not selected.
        /// </summary>
        SPI_SETHOTTRACKING = 0x100F,

        /// <summary>
        ///     Enables or disables the smooth-scrolling effect for list boxes. Set the pvParam parameter to TRUE to enable the
        ///     smooth-scrolling effect, or FALSE to disable it.
        /// </summary>
        SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007,

        /// <summary>
        ///     Enables or disables menu animation. This master switch must be on for any menu animation to occur. The pvParam
        ///     parameter is a BOOL variable; set pvParam to TRUE to enable animation and FALSE to disable animation.
        ///     If animation is enabled, SPI_GETMENUFADE indicates whether menus use fade or slide animation.
        /// </summary>
        SPI_SETMENUANIMATION = 0x1003,

        /// <summary>
        ///     Same as SPI_SETKEYBOARDCUES.
        /// </summary>
        SPI_SETMENUUNDERLINES = 0x100B,

        /// <summary>
        ///     Set pvParam to TRUE to enable the selection fade effect or FALSE to disable it.
        ///     The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading
        ///     out after the menu is dismissed. The selection fade effect is possible only if the system has a color depth of more
        ///     than 256 colors.
        /// </summary>
        SPI_SETSELECTIONFADE = 0x1015,

        /// <summary>
        ///     Set pvParam to TRUE to enable ToolTip animation or FALSE to disable it. If enabled, you can use SPI_SETTOOLTIPFADE
        ///     to specify fade or slide animation.
        /// </summary>
        SPI_SETTOOLTIPANIMATION = 0x1017,

        /// <summary>
        ///     If the SPI_SETTOOLTIPANIMATION flag is enabled, use SPI_SETTOOLTIPFADE to indicate whether ToolTip animation uses a
        ///     fade effect or a slide effect. Set pvParam to TRUE for fade animation or FALSE for slide animation. The tooltip
        ///     fade effect is possible only if the system has a color depth of more than 256 colors. For more information on the
        ///     slide and fade effects, see the AnimateWindowfunction.
        /// </summary>
        SPI_SETTOOLTIPFADE = 0x1019,

        /// <summary>
        ///     Enables or disables UI effects. Set the pvParam parameter to TRUE to enable all UI effects or FALSE to disable all
        ///     UI effects.
        /// </summary>
        SPI_SETUIEFFECTS = 0x103F
    }

    public enum SystemParametersWindowInfo
    {
        /// <summary>
        ///     Determines whether active window tracking (activating the window the mouse is on) is on or off. The pvParam
        ///     parameter must point to a BOOL variable that receives TRUE for on, or FALSE for off.
        /// </summary>
        SPI_GETACTIVEWINDOWTRACKING = 0x1000,

        /// <summary>
        ///     Determines whether windows activated through active window tracking will be brought to the top. The pvParam
        ///     parameter must point to a BOOL variable that receives TRUE for on, or FALSE for off.
        /// </summary>
        SPI_GETACTIVEWNDTRKZORDER = 0x100C,

        /// <summary>
        ///     Retrieves the active window tracking delay, in milliseconds. The pvParam parameter must point to a DWORD variable
        ///     that receives the time.
        /// </summary>
        SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002,

        /// <summary>
        ///     Retrieves the animation effects associated with user actions. The pvParam parameter must point to an ANIMATIONINFO
        ///     structure that receives the information. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(ANIMATIONINFO).
        /// </summary>
        SPI_GETANIMATION = 0x0048,

        /// <summary>
        ///     Retrieves the border multiplier factor that determines the width of a window's sizing border. The pvParamparameter
        ///     must point to an integer variable that receives this value.
        /// </summary>
        SPI_GETBORDER = 0x0005,

        /// <summary>
        ///     Retrieves the caret width in edit controls, in pixels. The pvParam parameter must point to a DWORD variable that
        ///     receives this value.
        /// </summary>
        SPI_GETCARETWIDTH = 0x2006,

        /// <summary>
        ///     Determines whether a window is docked when it is moved to the top, left, or right edges of a monitor or monitor
        ///     array. The pvParam parameter must point to a BOOL variable that receives TRUE if enabled, or FALSE otherwise.
        ///     Use SPI_GETWINARRANGING to determine whether this behavior is enabled.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETDOCKMOVING = 0x0090,

        /// <summary>
        ///     Determines whether a maximized window is restored when its caption bar is dragged. The pvParam parameter must point
        ///     to a BOOL variable that receives TRUE if enabled, or FALSE otherwise.
        ///     Use SPI_GETWINARRANGING to determine whether this behavior is enabled.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETDRAGFROMMAXIMIZE = 0x008C,

        /// <summary>
        ///     Determines whether dragging of full windows is enabled. The pvParam parameter must point to a BOOL variable that
        ///     receives TRUE if enabled, or FALSE otherwise.
        /// </summary>
        SPI_GETDRAGFULLWINDOWS = 0x0026,

        /// <summary>
        ///     Retrieves the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch
        ///     request. The pvParam parameter must point to a DWORD variable that receives the value.
        /// </summary>
        SPI_GETFOREGROUNDFLASHCOUNT = 0x2004,

        /// <summary>
        ///     Retrieves the amount of time following user input, in milliseconds, during which the system will not allow
        ///     applications to force themselves into the foreground. The pvParam parameter must point to a DWORD variable that
        ///     receives the time.
        /// </summary>
        SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000,

        /// <summary>
        ///     Retrieves the metrics associated with minimized windows. The pvParam parameter must point to a MINIMIZEDMETRICS
        ///     structure that receives the information. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(MINIMIZEDMETRICS).
        /// </summary>
        SPI_GETMINIMIZEDMETRICS = 0x002B,

        /// <summary>
        ///     Retrieves the threshold in pixels where docking behavior is triggered by using a mouse to drag a window to the edge
        ///     of a monitor or monitor array. The default threshold is 1. The pvParam parameter must point to a DWORD variable
        ///     that receives the value.
        ///     Use SPI_GETWINARRANGING to determine whether this behavior is enabled.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETMOUSEDOCKTHRESHOLD = 0x007E,

        /// <summary>
        ///     Retrieves the threshold in pixels where undocking behavior is triggered by using a mouse to drag a window from the
        ///     edge of a monitor or a monitor array toward the center. The default threshold is 20.
        ///     Use SPI_GETWINARRANGING to determine whether this behavior is enabled.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETMOUSEDRAGOUTTHRESHOLD = 0x0084,

        /// <summary>
        ///     Retrieves the threshold in pixels from the top of a monitor or a monitor array where a vertically maximized window
        ///     is restored when dragged with the mouse. The default threshold is 50.
        ///     Use SPI_GETWINARRANGING to determine whether this behavior is enabled.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETMOUSESIDEMOVETHRESHOLD = 0x0088,

        /// <summary>
        ///     Retrieves the metrics associated with the nonclient area of nonminimized windows. The pvParam parameter must point
        ///     to a NONCLIENTMETRICS structure that receives the information. Set the cbSize member of this structure and the
        ///     uiParam parameter to sizeof(NONCLIENTMETRICS).
        /// </summary>
        SPI_GETNONCLIENTMETRICS = 0x0029,

        /// <summary>
        ///     Retrieves the threshold in pixels where docking behavior is triggered by using a pen to drag a window to the edge
        ///     of a monitor or monitor array. The default is 30.
        ///     Use SPI_GETWINARRANGING to determine whether this behavior is enabled.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETPENDOCKTHRESHOLD = 0x0080,

        /// <summary>
        ///     Retrieves the threshold in pixels where undocking behavior is triggered by using a pen to drag a window from the
        ///     edge of a monitor or monitor array toward its center. The default threshold is 30.
        ///     Use SPI_GETWINARRANGING to determine whether this behavior is enabled.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETPENDRAGOUTTHRESHOLD = 0x0086,

        /// <summary>
        ///     Retrieves the threshold in pixels from the top of a monitor or monitor array where a vertically maximized window
        ///     is restored when dragged with the mouse. The default threshold is 50.
        ///     Use SPI_GETWINARRANGING to determine whether this behavior is enabled.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETPENSIDEMOVETHRESHOLD = 0x008A,

        /// <summary>
        ///     Determines whether the IME status window is visible (on a per-user basis). The pvParam parameter must point to a
        ///     BOOL variable that receives TRUE if the status window is visible, or FALSE if it is not.
        /// </summary>
        SPI_GETSHOWIMEUI = 0x006E,

        /// <summary>
        ///     Determines whether a window is vertically maximized when it is sized to the top or bottom of a monitor or monitor
        ///     array. The pvParam parameter must point to a BOOL variable that receives TRUE if enabled, or FALSE otherwise.
        ///     Use SPI_GETWINARRANGING to determine whether this behavior is enabled.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETSNAPSIZING = 0x008E,

        /// <summary>
        ///     Determines whether window arrangement is enabled. The pvParam parameter must point to a BOOL variable that receives
        ///     TRUE if enabled, or FALSE otherwise.
        ///     Window arrangement reduces the number of mouse, pen, or touch interactions needed to move and size top-level
        ///     windows by simplifying the default behavior of a window when it is dragged or sized.
        ///     The following parameters retrieve individual window arrangement settings:
        ///     SPI_GETDOCKMOVING
        ///     SPI_GETMOUSEDOCKTHRESHOLD
        ///     SPI_GETMOUSEDRAGOUTTHRESHOLD
        ///     SPI_GETMOUSESIDEMOVETHRESHOLD
        ///     SPI_GETPENDOCKTHRESHOLD
        ///     SPI_GETPENDRAGOUTTHRESHOLD
        ///     SPI_GETPENSIDEMOVETHRESHOLD
        ///     SPI_GETSNAPSIZING
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_GETWINARRANGING = 0x0082,

        /// <summary>
        ///     Sets active window tracking (activating the window the mouse is on) either on or off. Set pvParam to TRUE for on or
        ///     FALSE for off.
        /// </summary>
        SPI_SETACTIVEWINDOWTRACKING = 0x1001,

        /// <summary>
        ///     Determines whether or not windows activated through active window tracking should be brought to the top. Set
        ///     pvParam to TRUE for on or FALSE for off.
        /// </summary>
        SPI_SETACTIVEWNDTRKZORDER = 0x100D,

        /// <summary>
        ///     Sets the active window tracking delay. Set pvParam to the number of milliseconds to delay before activating the
        ///     window under the mouse pointer.
        /// </summary>
        SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003,

        /// <summary>
        ///     Sets the animation effects associated with user actions. The pvParam parameter must point to an ANIMATIONINFO
        ///     structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(ANIMATIONINFO).
        /// </summary>
        SPI_SETANIMATION = 0x0049,

        /// <summary>
        ///     Sets the border multiplier factor that determines the width of a window's sizing border. The uiParam parameter
        ///     specifies the new value.
        /// </summary>
        SPI_SETBORDER = 0x0006,

        /// <summary>
        ///     Sets the caret width in edit controls. Set pvParam to the desired width, in pixels. The default and minimum value
        ///     is 1.
        /// </summary>
        SPI_SETCARETWIDTH = 0x2007,

        /// <summary>
        ///     Sets whether a window is docked when it is moved to the top, left, or right docking targets on a monitor or monitor
        ///     array. Set pvParam to TRUE for on or FALSE for off.
        ///     SPI_GETWINARRANGING must be TRUE to enable this behavior.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETDOCKMOVING = 0x0091,

        /// <summary>
        ///     Sets whether a maximized window is restored when its caption bar is dragged. Set pvParam to TRUE for on or FALSE
        ///     for off.
        ///     SPI_GETWINARRANGING must be TRUE to enable this behavior.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETDRAGFROMMAXIMIZE = 0x008D,

        /// <summary>
        ///     Sets dragging of full windows either on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
        /// </summary>
        SPI_SETDRAGFULLWINDOWS = 0x0025,

        /// <summary>
        ///     Sets the height, in pixels, of the rectangle used to detect the start of a drag operation. Set uiParam to the new
        ///     value. To retrieve the drag height, call GetSystemMetrics with the SM_CYDRAG flag.
        /// </summary>
        SPI_SETDRAGHEIGHT = 0x004D,

        /// <summary>
        ///     Sets the width, in pixels, of the rectangle used to detect the start of a drag operation. Set uiParam to the new
        ///     value. To retrieve the drag width, call GetSystemMetrics with the SM_CXDRAG flag.
        /// </summary>
        SPI_SETDRAGWIDTH = 0x004C,

        /// <summary>
        ///     Sets the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch
        ///     request. Set pvParam to the number of times to flash.
        /// </summary>
        SPI_SETFOREGROUNDFLASHCOUNT = 0x2005,

        /// <summary>
        ///     Sets the amount of time following user input, in milliseconds, during which the system does not allow applications
        ///     to force themselves into the foreground. Set pvParam to the new time-out value.
        ///     The calling thread must be able to change the foreground window, otherwise the call fails.
        /// </summary>
        SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001,

        /// <summary>
        ///     Sets the metrics associated with minimized windows. The pvParam parameter must point to a MINIMIZEDMETRICS
        ///     structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
        ///     sizeof(MINIMIZEDMETRICS).
        /// </summary>
        SPI_SETMINIMIZEDMETRICS = 0x002C,

        /// <summary>
        ///     Sets the threshold in pixels where docking behavior is triggered by using a mouse to drag a window to the edge of a
        ///     monitor or monitor array. The default threshold is 1. The pvParam parameter must point to a DWORD variable that
        ///     contains the new value.
        ///     SPI_GETWINARRANGING must be TRUE to enable this behavior.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETMOUSEDOCKTHRESHOLD = 0x007F,

        /// <summary>
        ///     Sets the threshold in pixels where undocking behavior is triggered by using a mouse to drag a window from the edge
        ///     of a monitor or monitor array to its center. The default threshold is 20. The pvParam parameter must point to a
        ///     DWORD variable that contains the new value.
        ///     SPI_GETWINARRANGING must be TRUE to enable this behavior.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETMOUSEDRAGOUTTHRESHOLD = 0x0085,

        /// <summary>
        ///     Sets the threshold in pixels from the top of the monitor where a vertically maximized window is restored when
        ///     dragged with the mouse. The default threshold is 50. The pvParam parameter must point to a DWORD variable that
        ///     contains the new value.
        ///     SPI_GETWINARRANGING must be TRUE to enable this behavior.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETMOUSESIDEMOVETHRESHOLD = 0x0089,

        /// <summary>
        ///     Sets the metrics associated with the nonclient area of nonminimized windows. The pvParam parameter must point to a
        ///     NONCLIENTMETRICS structure that contains the new parameters. Set the cbSize member of this structure and the
        ///     uiParam parameter to sizeof(NONCLIENTMETRICS). Also, the lfHeight member of the LOGFONT structure must be a
        ///     negative value.
        /// </summary>
        SPI_SETNONCLIENTMETRICS = 0x002A,

        /// <summary>
        ///     Sets the threshold in pixels where docking behavior is triggered by using a pen to drag a window to the edge of a
        ///     monitor or monitor array. The default threshold is 30. The pvParam parameter must point to a DWORD variable that
        ///     contains the new value.
        ///     SPI_GETWINARRANGING must be TRUE to enable this behavior.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETPENDOCKTHRESHOLD = 0x0081,

        /// <summary>
        ///     Sets the threshold in pixels where undocking behavior is triggered by using a pen to drag a window from the edge of
        ///     a monitor or monitor array to its center. The default threshold is 30. The pvParam parameter must point to a DWORD
        ///     variable that contains the new value.
        ///     SPI_GETWINARRANGING must be TRUE to enable this behavior.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETPENDRAGOUTTHRESHOLD = 0x0087,

        /// <summary>
        ///     Sets the threshold in pixels from the top of the monitor where a vertically maximized window is restored when
        ///     dragged with a pen. The default threshold is 50. The pvParam parameter must point to a DWORD variable that contains
        ///     the new value.
        ///     SPI_GETWINARRANGING must be TRUE to enable this behavior.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETPENSIDEMOVETHRESHOLD = 0x008B,

        /// <summary>
        ///     Sets whether the IME status window is visible or not on a per-user basis. The uiParam parameter specifies TRUE for
        ///     on or FALSE for off.
        /// </summary>
        SPI_SETSHOWIMEUI = 0x006F,

        /// <summary>
        ///     Sets whether a window is vertically maximized when it is sized to the top or bottom of the monitor. Set pvParam to
        ///     TRUE for on or FALSE for off.
        ///     SPI_GETWINARRANGING must be TRUE to enable this behavior.
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETSNAPSIZING = 0x008F,

        /// <summary>
        ///     Sets whether window arrangement is enabled. Set pvParam to TRUE for on or FALSE for off.
        ///     Window arrangement reduces the number of mouse, pen, or touch interactions needed to move and size top-level
        ///     windows by simplifying the default behavior of a window when it is dragged or sized.
        ///     The following parameters set individual window arrangement settings:
        ///     SPI_SETDOCKMOVING
        ///     SPI_SETMOUSEDOCKTHRESHOLD
        ///     SPI_SETMOUSEDRAGOUTTHRESHOLD
        ///     SPI_SETMOUSESIDEMOVETHRESHOLD
        ///     SPI_SETPENDOCKTHRESHOLD
        ///     SPI_SETPENDRAGOUTTHRESHOLD
        ///     SPI_SETPENSIDEMOVETHRESHOLD
        ///     SPI_SETSNAPSIZING
        ///     Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This parameter is not supported.
        /// </summary>
        SPI_SETWINARRANGING = 0x0083
    }

    public enum VirtualKey
    {
        LBUTTON = 0x01,
        RBUTTON = 0x02,
        CANCEL = 0x03,
        MBUTTON = 0x04 /* NOT contiguous with L & RBUTTON */,
        XBUTTON1 = 0x05 /* NOT contiguous with L & RBUTTON */,
        XBUTTON2 = 0x06 /* NOT contiguous with L & RBUTTON */,
        BACK = 0x08,
        TAB = 0x09,
        CLEAR = 0x0C,
        RETURN = 0x0D,
        SHIFT = 0x10,
        CONTROL = 0x11,
        MENU = 0x12,
        PAUSE = 0x13,
        CAPITAL = 0x14,
        KANA = 0x15,
        HANGEUL = 0x15 /* old name - should be here for compatibility */,
        HANGUL = 0x15,
        JUNJA = 0x17,
        FINAL = 0x18,
        HANJA = 0x19,
        KANJI = 0x19,
        ESCAPE = 0x1B,
        CONVERT = 0x1C,
        NONCONVERT = 0x1D,
        ACCEPT = 0x1E,
        MODECHANGE = 0x1F,
        SPACE = 0x20,
        PRIOR = 0x21,
        NEXT = 0x22,
        END = 0x23,
        HOME = 0x24,
        LEFT = 0x25,
        UP = 0x26,
        RIGHT = 0x27,
        DOWN = 0x28,
        SELECT = 0x29,
        PRINT = 0x2A,
        EXECUTE = 0x2B,
        SNAPSHOT = 0x2C,
        INSERT = 0x2D,
        DELETE = 0x2E,
        HELP = 0x2F,
        LWIN = 0x5B,
        RWIN = 0x5C,
        APPS = 0x5D,
        SLEEP = 0x5F,
        NUMPAD0 = 0x60,
        NUMPAD1 = 0x61,
        NUMPAD2 = 0x62,
        NUMPAD3 = 0x63,
        NUMPAD4 = 0x64,
        NUMPAD5 = 0x65,
        NUMPAD6 = 0x66,
        NUMPAD7 = 0x67,
        NUMPAD8 = 0x68,
        NUMPAD9 = 0x69,
        MULTIPLY = 0x6A,
        ADD = 0x6B,
        SEPARATOR = 0x6C,
        SUBTRACT = 0x6D,
        DECIMAL = 0x6E,
        DIVIDE = 0x6F,
        F1 = 0x70,
        F2 = 0x71,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 0x78,
        F10 = 0x79,
        F11 = 0x7A,
        F12 = 0x7B,
        F13 = 0x7C,
        F14 = 0x7D,
        F15 = 0x7E,
        F16 = 0x7F,
        F17 = 0x80,
        F18 = 0x81,
        F19 = 0x82,
        F20 = 0x83,
        F21 = 0x84,
        F22 = 0x85,
        F23 = 0x86,
        F24 = 0x87,
        NUMLOCK = 0x90,
        SCROLL = 0x91,
        OEM_NEC_EQUAL = 0x92, // '=' key on numpad,
        OEM_FJ_JISHO = 0x92, // 'Dictionary' key,
        OEM_FJ_MASSHOU = 0x93, // 'Unregister word' key,
        OEM_FJ_TOUROKU = 0x94, // 'Register word' key,
        OEM_FJ_LOYA = 0x95, // 'Left OYAYUBI' key,
        OEM_FJ_ROYA = 0x96, // 'Right OYAYUBI' key,
        LSHIFT = 0xA0,
        RSHIFT = 0xA1,
        LCONTROL = 0xA2,
        RCONTROL = 0xA3,
        LMENU = 0xA4,
        RMENU = 0xA5,
        BROWSER_BACK = 0xA6,
        BROWSER_FORWARD = 0xA7,
        BROWSER_REFRESH = 0xA8,
        BROWSER_STOP = 0xA9,
        BROWSER_SEARCH = 0xAA,
        BROWSER_FAVORITES = 0xAB,
        BROWSER_HOME = 0xAC,
        VOLUME_MUTE = 0xAD,
        VOLUME_DOWN = 0xAE,
        VOLUME_UP = 0xAF,
        MEDIA_NEXT_TRACK = 0xB0,
        MEDIA_PREV_TRACK = 0xB1,
        MEDIA_STOP = 0xB2,
        MEDIA_PLAY_PAUSE = 0xB3,
        LAUNCH_MAIL = 0xB4,
        LAUNCH_MEDIA_SELECT = 0xB5,
        LAUNCH_APP1 = 0xB6,
        LAUNCH_APP2 = 0xB7,
        OEM_1 = 0xBA, // ';:' for US,
        OEM_PLUS = 0xBB, // '+' any country,
        OEM_COMMA = 0xBC, // ',' any country,
        OEM_MINUS = 0xBD, // '-' any country,
        OEM_PERIOD = 0xBE, // '.' any country,
        OEM_2 = 0xBF, // '/?' for US,
        OEM_3 = 0xC0, // '`~' for US,
        OEM_4 = 0xDB, //  '[{' for US,
        OEM_5 = 0xDC, //  '\|' for US,
        OEM_6 = 0xDD, //  ']}' for US,
        OEM_7 = 0xDE, //  ''"' for US,
        OEM_8 = 0xDF,
        OEM_AX = 0xE1, //  'AX' key on Japanese AX kbd,
        OEM_102 = 0xE2, //  "<>" or "\|" on RT 102-key kbd.,
        ICO_HELP = 0xE3, //  Help key on ICO,
        ICO_00 = 0xE4, //  00 key on ICO,
        PROCESSKEY = 0xE5,
        ICO_CLEAR = 0xE6,
        PACKET = 0xE7,
        OEM_RESET = 0xE9,
        OEM_JUMP = 0xEA,
        OEM_PA1 = 0xEB,
        OEM_PA2 = 0xEC,
        OEM_PA3 = 0xED,
        OEM_WSCTRL = 0xEE,
        OEM_CUSEL = 0xEF,
        OEM_ATTN = 0xF0,
        OEM_FINISH = 0xF1,
        OEM_COPY = 0xF2,
        OEM_AUTO = 0xF3,
        OEM_ENLW = 0xF4,
        OEM_BACKTAB = 0xF5,
        ATTN = 0xF6,
        CRSEL = 0xF7,
        EXSEL = 0xF8,
        EREOF = 0xF9,
        PLAY = 0xFA,
        ZOOM = 0xFB,
        NONAME = 0xFC,
        PA1 = 0xFD,
        OEM_CLEAR = 0xFE,
        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4a,
        K = 0x4b,
        L = 0x4c,
        M = 0x4d,
        N = 0x4e,
        O = 0x4f,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x5a,
        D0 = 0x30,
        D1 = 0x31,
        D2 = 0x32,
        D3 = 0x33,
        D4 = 0x34,
        D5 = 0x35,
        D6 = 0x36,
        D7 = 0x37,
        D8 = 0x38,
        D9 = 0x39
    }

    public enum WM
    {
        NULL = 0x0000,
        CREATE = 0x0001,
        DESTROY = 0x0002,
        MOVE = 0x0003,
        SIZE = 0x0005,
        ACTIVATE = 0x0006,
        SETFOCUS = 0x0007,
        KILLFOCUS = 0x0008,
        ENABLE = 0x000A,
        SETREDRAW = 0x000B,
        SETTEXT = 0x000C,
        GETTEXT = 0x000D,
        GETTEXTLENGTH = 0x000E,
        PAINT = 0x000F,
        CLOSE = 0x0010,
        QUERYENDSESSION = 0x0011,
        QUERYOPEN = 0x0013,
        ENDSESSION = 0x0016,
        QUIT = 0x0012,
        ERASEBKGND = 0x0014,
        SYSCOLORCHANGE = 0x0015,
        SHOWWINDOW = 0x0018,
        WININICHANGE = 0x001A,
        SETTINGCHANGE = WININICHANGE,
        DEVMODECHANGE = 0x001B,
        ACTIVATEAPP = 0x001C,
        FONTCHANGE = 0x001D,
        TIMECHANGE = 0x001E,
        CANCELMODE = 0x001F,
        SETCURSOR = 0x0020,
        MOUSEACTIVATE = 0x0021,
        CHILDACTIVATE = 0x0022,
        QUEUESYNC = 0x0023,
        GETMINMAXINFO = 0x0024,
        PAINTICON = 0x0026,
        ICONERASEBKGND = 0x0027,
        NEXTDLGCTL = 0x0028,
        SPOOLERSTATUS = 0x002A,
        DRAWITEM = 0x002B,
        MEASUREITEM = 0x002C,
        DELETEITEM = 0x002D,
        VKEYTOITEM = 0x002E,
        CHARTOITEM = 0x002F,
        SETFONT = 0x0030,
        GETFONT = 0x0031,
        SETHOTKEY = 0x0032,
        GETHOTKEY = 0x0033,
        QUERYDRAGICON = 0x0037,
        COMPAREITEM = 0x0039,
        GETOBJECT = 0x003D,
        COMPACTING = 0x0041,
        COMMNOTIFY = 0x0044 /* no longer suported */,
        WINDOWPOSCHANGING = 0x0046,
        WINDOWPOSCHANGED = 0x0047,
        POWER = 0x0048,
        COPYDATA = 0x004A,
        CANCELJOURNAL = 0x004B,
        NOTIFY = 0x004E,
        INPUTLANGCHANGEREQUEST = 0x0050,
        INPUTLANGCHANGE = 0x0051,
        TCARD = 0x0052,
        HELP = 0x0053,
        USERCHANGED = 0x0054,
        NOTIFYFORMAT = 0x0055,
        CONTEXTMENU = 0x007B,
        STYLECHANGING = 0x007C,
        STYLECHANGED = 0x007D,
        DISPLAYCHANGE = 0x007E,
        GETICON = 0x007F,
        SETICON = 0x0080,
        NCCREATE = 0x0081,
        NCDESTROY = 0x0082,
        NCCALCSIZE = 0x0083,
        NCHITTEST = 0x0084,
        NCPAINT = 0x0085,
        NCACTIVATE = 0x0086,
        GETDLGCODE = 0x0087,
        SYNCPAINT = 0x0088,
        NCMOUSEMOVE = 0x00A0,
        NCLBUTTONDOWN = 0x00A1,
        NCLBUTTONUP = 0x00A2,
        NCLBUTTONDBLCLK = 0x00A3,
        NCRBUTTONDOWN = 0x00A4,
        NCRBUTTONUP = 0x00A5,
        NCRBUTTONDBLCLK = 0x00A6,
        NCMBUTTONDOWN = 0x00A7,
        NCMBUTTONUP = 0x00A8,
        NCMBUTTONDBLCLK = 0x00A9,
        NCXBUTTONDOWN = 0x00AB,
        NCXBUTTONUP = 0x00AC,
        NCXBUTTONDBLCLK = 0x00AD,
        INPUT_DEVICE_CHANGE = 0x00FE,
        INPUT = 0x00FF,
        KEYFIRST = 0x0100,
        KEYDOWN = 0x0100,
        KEYUP = 0x0101,
        CHAR = 0x0102,
        DEADCHAR = 0x0103,
        SYSKEYDOWN = 0x0104,
        SYSKEYUP = 0x0105,
        SYSCHAR = 0x0106,
        SYSDEADCHAR = 0x0107,
        UNICHAR = 0x0109,
        KEYLAST = 0x0109,
        IME_STARTCOMPOSITION = 0x010D,
        IME_ENDCOMPOSITION = 0x010E,
        IME_COMPOSITION = 0x010F,
        IME_KEYLAST = 0x010F,
        INITDIALOG = 0x0110,
        COMMAND = 0x0111,
        SYSCOMMAND = 0x0112,
        TIMER = 0x0113,
        HSCROLL = 0x0114,
        VSCROLL = 0x0115,
        INITMENU = 0x0116,
        INITMENUPOPUP = 0x0117,
        GESTURE = 0x0119,
        GESTURENOTIFY = 0x011A,
        MENUSELECT = 0x011F,
        MENUCHAR = 0x0120,
        ENTERIDLE = 0x0121,
        MENURBUTTONUP = 0x0122,
        MENUDRAG = 0x0123,
        MENUGETOBJECT = 0x0124,
        UNINITMENUPOPUP = 0x0125,
        MENUCOMMAND = 0x0126,
        CHANGEUISTATE = 0x0127,
        UPDATEUISTATE = 0x0128,
        QUERYUISTATE = 0x0129,
        CTLCOLORMSGBOX = 0x0132,
        CTLCOLOREDIT = 0x0133,
        CTLCOLORLISTBOX = 0x0134,
        CTLCOLORBTN = 0x0135,
        CTLCOLORDLG = 0x0136,
        CTLCOLORSCROLLBAR = 0x0137,
        CTLCOLORSTATIC = 0x0138,
        MOUSEFIRST = 0x0200,
        MOUSEMOVE = 0x0200,
        LBUTTONDOWN = 0x0201,
        LBUTTONUP = 0x0202,
        LBUTTONDBLCLK = 0x0203,
        RBUTTONDOWN = 0x0204,
        RBUTTONUP = 0x0205,
        RBUTTONDBLCLK = 0x0206,
        MBUTTONDOWN = 0x0207,
        MBUTTONUP = 0x0208,
        MBUTTONDBLCLK = 0x0209,
        MOUSEWHEEL = 0x020A,
        XBUTTONDOWN = 0x020B,
        XBUTTONUP = 0x020C,
        XBUTTONDBLCLK = 0x020D,
        MOUSEHWHEEL = 0x020E,
        MOUSELAST = 0x020E,
        PARENTNOTIFY = 0x0210,
        ENTERMENULOOP = 0x0211,
        EXITMENULOOP = 0x0212,
        NEXTMENU = 0x0213,
        SIZING = 0x0214,
        CAPTURECHANGED = 0x0215,
        MOVING = 0x0216,
        POWERBROADCAST = 0x0218,
        DEVICECHANGE = 0x0219,
        MDICREATE = 0x0220,
        MDIDESTROY = 0x0221,
        MDIACTIVATE = 0x0222,
        MDIRESTORE = 0x0223,
        MDINEXT = 0x0224,
        MDIMAXIMIZE = 0x0225,
        MDITILE = 0x0226,
        MDICASCADE = 0x0227,
        MDIICONARRANGE = 0x0228,
        MDIGETACTIVE = 0x0229,
        MDISETMENU = 0x0230,
        ENTERSIZEMOVE = 0x0231,
        EXITSIZEMOVE = 0x0232,
        DROPFILES = 0x0233,
        MDIREFRESHMENU = 0x0234,
        POINTERDEVICECHANGE = 0x238,
        POINTERDEVICEINRANGE = 0x239,
        POINTERDEVICEOUTOFRANGE = 0x23A,
        TOUCH = 0x0240,
        NCPOINTERUPDATE = 0x0241,
        NCPOINTERDOWN = 0x0242,
        NCPOINTERUP = 0x0243,
        POINTERUPDATE = 0x0245,
        POINTERDOWN = 0x0246,
        POINTERUP = 0x0247,
        POINTERENTER = 0x0249,
        POINTERLEAVE = 0x024A,
        POINTERACTIVATE = 0x024B,
        POINTERCAPTURECHANGED = 0x024C,
        TOUCHHITTESTING = 0x024D,
        POINTERWHEEL = 0x024E,
        POINTERHWHEEL = 0x024F,
        IME_SETCONTEXT = 0x0281,
        IME_NOTIFY = 0x0282,
        IME_CONTROL = 0x0283,
        IME_COMPOSITIONFULL = 0x0284,
        IME_SELECT = 0x0285,
        IME_CHAR = 0x0286,
        IME_REQUEST = 0x0288,
        IME_KEYDOWN = 0x0290,
        IME_KEYUP = 0x0291,
        MOUSEHOVER = 0x02A1,
        MOUSELEAVE = 0x02A3,
        NCMOUSEHOVER = 0x02A0,
        NCMOUSELEAVE = 0x02A2,
        WTSSESSION_CHANGE = 0x02B1,
        TABLET_FIRST = 0x02c0,
        TABLET_LAST = 0x02df,
        DPICHANGED = 0x02E0,
        CUT = 0x0300,
        COPY = 0x0301,
        PASTE = 0x0302,
        CLEAR = 0x0303,
        UNDO = 0x0304,
        RENDERFORMAT = 0x0305,
        RENDERALLFORMATS = 0x0306,
        DESTROYCLIPBOARD = 0x0307,
        DRAWCLIPBOARD = 0x0308,
        PAINTCLIPBOARD = 0x0309,
        VSCROLLCLIPBOARD = 0x030A,
        SIZECLIPBOARD = 0x030B,
        ASKCBFORMATNAME = 0x030C,
        CHANGECBCHAIN = 0x030D,
        HSCROLLCLIPBOARD = 0x030E,
        QUERYNEWPALETTE = 0x030F,
        PALETTEISCHANGING = 0x0310,
        PALETTECHANGED = 0x0311,
        HOTKEY = 0x0312,
        PRINT = 0x0317,
        PRINTCLIENT = 0x0318,
        APPCOMMAND = 0x0319,
        THEMECHANGED = 0x031A,
        CLIPBOARDUPDATE = 0x031D,
        DWMCOMPOSITIONCHANGED = 0x031E,
        DWMNCRENDERINGCHANGED = 0x031F,
        DWMCOLORIZATIONCOLORCHANGED = 0x0320,
        DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
        DWMSENDICONICTHUMBNAIL = 0x0323,
        DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326,
        GETTITLEBARINFOEX = 0x033F,
        HANDHELDFIRST = 0x0358,
        HANDHELDLAST = 0x035F,
        AFXFIRST = 0x0360,
        AFXLAST = 0x037F,
        PENWINFIRST = 0x0380,
        PENWINLAST = 0x038F,
        APP = 0x8000,
        USER = 0x0400
    }

    #endregion


}