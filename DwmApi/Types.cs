using System;

// ReSharper disable InconsistentNaming

namespace WinApi.DwmApi
{
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
}