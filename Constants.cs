using System;

// ReSharper disable InconsistentNaming

namespace WinApi
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
        WS_POPUP = unchecked((int) 0x80000000),

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
    public enum WindowThemeNCAttribute
    {
        /// <summary>
        ///     Prevents the window caption from being drawn.
        /// </summary>
        WTNCA_NODRAWCAPTION = 0x00000001,

        /// <summary>
        ///     Prevents the system icon from being drawn.
        /// </summary>
        WTNCA_NODRAWICON = 0x00000002,

        /// <summary>
        ///     Prevents the system icon menu from appearing.
        /// </summary>
        WTNCA_NOSYSMENU = 0x00000004,

        /// <summary>
        ///     Prevents mirroring of the question mark, even in right-to-left (RTL) layout.
        /// </summary>
        WTNCA_NOMIRRORHELP = 0x00000008
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
    public enum DwmNCRenderingPolicy
    {
        /// <summary>
        ///     The non-client rendering area is rendered based on the window style.
        /// </summary>
        DWMNCRP_USEWINDOWSTYLE,

        /// <summary>
        ///     The non-client area rendering is disabled; the window style is ignored.
        /// </summary>
        DWMNCRP_DISABLED,

        /// <summary>
        ///     The non-client area rendering is enabled; the window style is ignored.
        /// </summary>
        DWMNCRP_ENABLED,

        /// <summary>
        ///     The maximum recognized DWMNCRENDERINGPOLICY value, used for validation purposes.
        /// </summary>
        DWMNCRP_LAST
    }

    [Flags]
    public enum DwmFlip3DPolicy
    {
        /// <summary>
        ///     Use the window's style and visibility settings to determine whether to hide or include the window in Flip3D
        ///     rendering.
        /// </summary>
        DWMFLIP3D_DEFAULT,

        /// <summary>
        ///     Exclude the window from Flip3D and display it below the Flip3D rendering
        /// </summary>
        DWMFLIP3D_EXCLUDEBELOW,

        /// <summary>
        ///     Exclude the window from Flip3D and display it above the Flip3D rendering
        /// </summary>
        DWMFLIP3D_EXCLUDEABOVE,

        /// <summary>
        ///     The maximum recognized DWMFLIP3DWINDOWPOLICY value, used for validation purposes
        /// </summary>
        DWMFLIP3D_LAST
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

    #endregion

    #region Singular constants

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

    public enum WindowThemeAttributeType
    {
        /// <summary>Specifies non-client related attributes. PvAttribute must be a pointer of type WTA_OPTIONS</summary>
        WTA_NONCLIENT = 1
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

    public enum DwmWindowAttributeType
    {
        /// <summary>
        ///     Use with DwmGetWindowAttribute. Discovers whether non-client rendering is enabled. The retrieved value is of type
        ///     BOOL. TRUE if non-client rendering is enabled; otherwise, FALSE.
        /// </summary>
        DWMWA_NCRENDERING_ENABLED = 1,

        /// <summary>
        ///     Use with DwmSetWindowAttribute. Sets the non-client rendering policy. The pvAttribute parameter points to a value
        ///     from the DWMNCRENDERINGPOLICY enumeration.
        /// </summary>
        DWMWA_NCRENDERING_POLICY,

        /// <summary>
        ///     Use with DwmSetWindowAttribute. Enables or forcibly disables DWM transitions. The pvAttribute parameter points to a
        ///     value of TRUE to disable transitions or FALSE to enable transitions.
        /// </summary>
        DWMWA_TRANSITIONS_FORCEDISABLED,

        /// <summary>
        ///     Use with DwmSetWindowAttribute. Enables content rendered in the non-client area to be visible on the frame drawn by
        ///     DWM. The pvAttribute parameter points to a value of TRUE to enable content rendered in the non-client area to be
        ///     visible on the frame; otherwise, it points to FALSE.
        /// </summary>
        DWMWA_ALLOW_NCPAINT,

        /// <summary>
        ///     Use with DwmGetWindowAttribute. Retrieves the bounds of the caption button area in the window-relative space. The
        ///     retrieved value is of type RECT.
        /// </summary>
        DWMWA_CAPTION_BUTTON_BOUNDS,

        /// <summary>
        ///     Use with DwmSetWindowAttribute. Specifies whether non-client content is right-to-left (RTL) mirrored. The
        ///     pvAttribute parameter points to a value of TRUE if the non-client content is right-to-left (RTL) mirrored;
        ///     otherwise, it points to FALSE.
        /// </summary>
        DWMWA_NONCLIENT_RTL_LAYOUT,

        /// <summary>
        ///     Use with DwmSetWindowAttribute. Forces the window to display an iconic thumbnail or peek representation (a static
        ///     bitmap), even if a live or snapshot representation of the window is available. This value normally is set during a
        ///     window's creation and not changed throughout the window's lifetime. Some scenarios, however, might require the
        ///     value to change over time. The pvAttribute parameter points to a value of TRUE to require a iconic thumbnail or
        ///     peek representation; otherwise, it points to FALSE.
        /// </summary>
        DWMWA_FORCE_ICONIC_REPRESENTATION,

        /// <summary>
        ///     Use with DwmSetWindowAttribute. Sets how Flip3D treats the window. The pvAttribute parameter points to a value from
        ///     the DWMFLIP3DWINDOWPOLICY enumeration.
        /// </summary>
        DWMWA_FLIP3D_POLICY,

        /// <summary>
        ///     Use with DwmGetWindowAttribute. Retrieves the extended frame bounds rectangle in screen space. The retrieved value
        ///     is of type RECT.
        /// </summary>
        DWMWA_EXTENDED_FRAME_BOUNDS,

        /// <summary>
        ///     Use with DwmSetWindowAttribute. The window will provide a bitmap for use by DWM as an iconic thumbnail or peek
        ///     representation (a static bitmap) for the window. DWMWA_HAS_ICONIC_BITMAP can be specified with
        ///     DWMWA_FORCE_ICONIC_REPRESENTATION. DWMWA_HAS_ICONIC_BITMAP normally is set during a window's creation and not
        ///     changed throughout the window's lifetime. Some scenarios, however, might require the value to change over time. The
        ///     pvAttribute parameter points to a value of TRUE to inform DWM that the window will provide an iconic thumbnail or
        ///     peek representation; otherwise, it points to FALSE.
        ///     Windows Vista and earlier:  This value is not supported.
        /// </summary>
        DWMWA_HAS_ICONIC_BITMAP,

        /// <summary>
        ///     Use with DwmSetWindowAttribute. Do not show peek preview for the window. The peek view shows a full-sized preview
        ///     of the window when the mouse hovers over the window's thumbnail in the taskbar. If this attribute is set, hovering
        ///     the mouse pointer over the window's thumbnail dismisses peek (in case another window in the group has a peek
        ///     preview showing). The pvAttribute parameter points to a value of TRUE to prevent peek functionality or FALSE to
        ///     allow it.
        ///     Windows Vista and earlier:  This value is not supported.
        /// </summary>
        DWMWA_DISALLOW_PEEK,

        /// <summary>
        ///     Use with DwmSetWindowAttribute. Prevents a window from fading to a glass sheet when peek is invoked. The
        ///     pvAttribute parameter points to a value of TRUE to prevent the window from fading during another window's peek or
        ///     FALSE for normal behavior.
        ///     Windows Vista and earlier:  This value is not supported.
        /// </summary>
        DWMWA_EXCLUDED_FROM_PEEK,

        /// <summary>
        ///     Use with DwmGetWindowAttribute. Cloaks the window such that it is not visible to the user. The window is still
        ///     composed by DWM.
        ///     Using with DirectComposition:  Use the DWMWA_CLOAK flag to cloak the layered child window when animating a
        ///     representation of the window's content via a DirectComposition visual which has been associated with the layered
        ///     child window. For more details on this usage case, see How to How to animate the bitmap of a layered child window.
        ///     Windows 7 and earlier:  This value is not supported.
        /// </summary>
        DWMWA_CLOAK,

        /// <summary>
        ///     Use with DwmGetWindowAttribute. If the window is cloaked, provides one of the following values explaining why:
        ///     DWM_CLOAKED_APP 	    0x0000001	The window was cloaked by its owner application.
        ///     DWM_CLOAKED_SHELL       0x0000002	The window was cloaked by the Shell.
        ///     DWM_CLOAKED_INHERITED   0x0000004	The cloak value was inherited from its owner window.
        /// </summary>
        DWMWA_CLOAKED,

        /// <summary>
        ///     Use with DwmSetWindowAttribute. Freeze the window's thumbnail image with its current visuals. Do no further live
        ///     updates on the thumbnail image to match the window's contents.
        ///     Windows 7 and earlier:  This value is not supported.
        /// </summary>
        DWMWA_FREEZE_REPRESENTATION,

        /// <summary>
        ///     The maximum recognized DWMWINDOWATTRIBUTE value, used for validation purposes.
        /// </summary>
        DWMWA_LAST
    }

    public enum WM : uint
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