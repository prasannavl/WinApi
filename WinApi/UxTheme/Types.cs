using NetCoreEx.Geometry;
using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace WinApi.UxTheme
{
    public enum WindowThemeAttributeType
    {
        /// <summary>Specifies non-client related attributes. PvAttribute must be a pointer of type WTA_OPTIONS</summary>
        WTA_NONCLIENT = 1
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowThemeAttributeOptions
    {
        public uint Flags;
        public uint Mask;
    }

    [Flags]
    public enum WindowThemeNcAttributeFlags
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
        WTNCA_NOMIRRORHELP = 0x00000008,

        /// <summary>
        ///     All valid bits
        /// </summary>
        WTNCA_VALIDBITS = WTNCA_NODRAWCAPTION | WTNCA_NODRAWICON | WTNCA_NOMIRRORHELP | WTNCA_NOSYSMENU
    }

    [Flags]
    public enum OpenThemeDataFlags
    {
        OTD_FORCE_RECT_SIZING = 0x00000001, // make all parts size to rect,
        OTD_NONCLIENT = 0x00000002 // set if hTheme to be used for nonclient area,
    }

    [Flags]
    public enum DrawThemeBackgroundFlags
    {
        DTBG_CLIPRECT = 0x00000001, // rcClip has been specified,
        DTBG_DRAWSOLID = 0x00000002, // DEPRECATED: draw transparent/alpha images as solid,
        DTBG_OMITBORDER = 0x00000004, // don't draw border of part,
        DTBG_OMITCONTENT = 0x00000008, // don't draw content area of part,
        DTBG_COMPUTINGREGION = 0x00000010, // TRUE if calling to compute region,
        DTBG_MIRRORDC = 0x00000020, // assume the hdc is mirrorred and,
        DTBG_NOMIRROR = 0x00000040, // don't mirror the output, overrides everything else ,

        DTBG_VALIDBITS =
            DTBG_CLIPRECT |
            DTBG_DRAWSOLID |
            DTBG_OMITBORDER |
            DTBG_OMITCONTENT |
            DTBG_COMPUTINGREGION |
            DTBG_MIRRORDC |
            DTBG_NOMIRROR
    }

    public enum PropertyOrigin
    {
        /// <summary>
        ///     Property was found in the state section.
        /// </summary>
        PO_STATE = 0,

        /// <summary>
        ///     Property was found in the part section.
        /// </summary>
        PO_PART = 1,

        /// <summary>
        ///     Property was found in the class section.
        /// </summary>
        PO_CLASS = 2,

        /// <summary>
        ///     Property was found in the list of global variables.
        /// </summary>
        PO_GLOBAL = 3,

        /// <summary>
        ///     Property was not found.
        /// </summary>
        PO_NOTFOUND = 4
    }

    public enum ThemeSize
    {
        /// <summary>
        ///     Receives the minimum size of a visual style part.
        /// </summary>
        TS_MIN,

        /// <summary>
        ///     Receives the size of the visual style part that will best fit the available space.
        /// </summary>
        TS_TRUE,

        /// <summary>
        ///     Receives the size that the theme manager uses to draw a part.
        /// </summary>
        TS_DRAW
    }

    /// <summary>
    ///     Defines the options for the DrawThemeBackgroundEx function.
    /// </summary>
    public struct DrawThemeBackgroundOptions
    {
        /// <summary>
        ///     Size of the structure. Set this to sizeof(DTBGOPTS).
        /// </summary>
        public uint Size;

        public DrawThemeBackgroundFlags Flags;

        /// <summary>
        ///     A RECT that specifies the bounding rectangle of the clip region.
        /// </summary>
        public Rectangle ClipRect;
    }

    #region Parts and States

    public enum WindowParts
    {
        WP_CAPTION = 1,
        WP_SMALLCAPTION = 2,
        WP_MINCAPTION = 3,
        WP_SMALLMINCAPTION = 4,
        WP_MAXCAPTION = 5,
        WP_SMALLMAXCAPTION = 6,
        WP_FRAMELEFT = 7,
        WP_FRAMERIGHT = 8,
        WP_FRAMEBOTTOM = 9,
        WP_SMALLFRAMELEFT = 10,
        WP_SMALLFRAMERIGHT = 11,
        WP_SMALLFRAMEBOTTOM = 12,
        WP_SYSBUTTON = 13,
        WP_MDISYSBUTTON = 14,
        WP_MINBUTTON = 15,
        WP_MDIMINBUTTON = 16,
        WP_MAXBUTTON = 17,
        WP_CLOSEBUTTON = 18,
        WP_SMALLCLOSEBUTTON = 19,
        WP_MDICLOSEBUTTON = 20,
        WP_RESTOREBUTTON = 21,
        WP_MDIRESTOREBUTTON = 22,
        WP_HELPBUTTON = 23,
        WP_MDIHELPBUTTON = 24,
        WP_HORZSCROLL = 25,
        WP_HORZTHUMB = 26,
        WP_VERTSCROLL = 27,
        WP_VERTTHUMB = 28,
        WP_DIALOG = 29,
        WP_CAPTIONSIZINGTEMPLATE = 30,
        WP_SMALLCAPTIONSIZINGTEMPLATE = 31,
        WP_FRAMELEFTSIZINGTEMPLATE = 32,
        WP_SMALLFRAMELEFTSIZINGTEMPLATE = 33,
        WP_FRAMERIGHTSIZINGTEMPLATE = 34,
        WP_SMALLFRAMERIGHTSIZINGTEMPLATE = 35,
        WP_FRAMEBOTTOMSIZINGTEMPLATE = 36,
        WP_SMALLFRAMEBOTTOMSIZINGTEMPLATE = 37,
        WP_FRAME = 38
    }

    public enum FrameStates
    {
        FS_ACTIVE = 1,
        FS_INACTIVE = 2
    }

    public enum CaptionStates
    {
        CS_ACTIVE = 1,
        CS_INACTIVE = 2,
        CS_DISABLED = 3
    }

    public enum MaximizedCaptionStates
    {
        MXCS_ACTIVE = 1,
        MXCS_INACTIVE = 2,
        MXCS_DISABLED = 3
    }

    public enum MinimizedCaptionStates
    {
        MNCS_ACTIVE = 1,
        MNCS_INACTIVE = 2,
        MNCS_DISABLED = 3
    }

    public enum HScrollStates
    {
        HSS_NORMAL = 1,
        HSS_HOT = 2,
        HSS_PUSHED = 3,
        HSS_DISABLED = 4
    }

    public enum HThumbStates
    {
        HTS_NORMAL = 1,
        HTS_HOT = 2,
        HTS_PUSHED = 3,
        HTS_DISABLED = 4
    }

    public enum VScrollStates
    {
        VSS_NORMAL = 1,
        VSS_HOT = 2,
        VSS_PUSHED = 3,
        VSS_DISABLED = 4
    }

    public enum VThumbStates
    {
        VTS_NORMAL = 1,
        VTS_HOT = 2,
        VTS_PUSHED = 3,
        VTS_DISABLED = 4
    }

    public enum SysButtonStates
    {
        SBS_NORMAL = 1,
        SBS_HOT = 2,
        SBS_PUSHED = 3,
        SBS_DISABLED = 4
    }

    public enum MinimizeButtonStates
    {
        MINBS_NORMAL = 1,
        MINBS_HOT = 2,
        MINBS_PUSHED = 3,
        MINBS_DISABLED = 4
    }

    public enum MaximizeButtonStates
    {
        MAXBS_NORMAL = 1,
        MAXBS_HOT = 2,
        MAXBS_PUSHED = 3,
        MAXBS_DISABLED = 4
    }

    public enum RestoreButtonStates
    {
        RBS_NORMAL = 1,
        RBS_HOT = 2,
        RBS_PUSHED = 3,
        RBS_DISABLED = 4
    }

    public enum HelpButtonStates
    {
        HBS_NORMAL = 1,
        HBS_HOT = 2,
        HBS_PUSHED = 3,
        HBS_DISABLED = 4
    }

    public enum CloseButtonStates
    {
        CBS_NORMAL = 1,
        CBS_HOT = 2,
        CBS_PUSHED = 3,
        CBS_DISABLED = 4
    }

    public enum SmallCloseButtonStates
    {
        SCBS_NORMAL = 1,
        SCBS_HOT = 2,
        SCBS_PUSHED = 3,
        SCBS_DISABLED = 4
    }

    public enum FrameBottomStates
    {
        FRB_ACTIVE = 1,
        FRB_INACTIVE = 2
    }

    public enum FrameLeftStates
    {
        FRL_ACTIVE = 1,
        FRL_INACTIVE = 2
    }

    public enum FrameRightStates
    {
        FRR_ACTIVE = 1,
        FRR_INACTIVE = 2
    }

    public enum SmallCaptionStates
    {
        SCS_ACTIVE = 1,
        SCS_INACTIVE = 2,
        SCS_DISABLED = 3
    }

    public enum SmallFrameBottomStates
    {
        SFRB_ACTIVE = 1,
        SFRB_INACTIVE = 2
    }

    public enum SmallFrameLeftStates
    {
        SFRL_ACTIVE = 1,
        SFRL_INACTIVE = 2
    }

    public enum SmallFrameRightStates
    {
        SFRR_ACTIVE = 1,
        SFRR_INACTIVE = 2
    }

    #endregion
}