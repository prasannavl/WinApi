using System;
using System.Diagnostics;
using WinApi.Core;
using WinApi.DwmApi;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.UxTheme;
using WinApi.Windows;

namespace WinApi.Utils
{
    public sealed class DwmWindowHelper : DwmWindowHelperCore
    {
        public DwmWindowHelper(EventedWindowCore window) : base(window) {}
    }

    public class DwmWindowHelperCore
    {
        private readonly EventedWindowCore m_window;
        public bool BlurBehindEnabled;

        private bool m_isFirstNcCalcDone;
        public Rectangle NcOutsetRect;
        public Rectangle Padding;

        public DwmWindowHelperCore(EventedWindowCore window)
        {
            m_window = window;
        }

        public static int GetSystemTopBorderHeight()
        {
            return User32Methods.GetSystemMetrics(SystemMetrics.SM_CYBORDER);
        }

        public static void SetWindowThemeAttributes(IntPtr handle, bool drawCaptionIcon, bool drawCaptionTitle,
            bool allowSystemMenu)
        {
            WindowThemeNcAttributeFlags flags = 0;
            if (!drawCaptionIcon)
                flags |= WindowThemeNcAttributeFlags.WTNCA_NODRAWICON;
            if (!drawCaptionTitle)
                flags |= WindowThemeNcAttributeFlags.WTNCA_NODRAWCAPTION;
            if (!allowSystemMenu)
                flags |= WindowThemeNcAttributeFlags.WTNCA_NOSYSMENU;
            UxThemeHelpers.SetWindowThemeNonClientAttributes(handle,
                WindowThemeNcAttributeFlags.WTNCA_VALIDBITS & ~WindowThemeNcAttributeFlags.WTNCA_NOMIRRORHELP, flags);
        }

        public static Rectangle GetSystemNcOutsetThickness(EventedWindowCore window)
        {
            var rect = new Rectangle();
            User32Methods.AdjustWindowRectEx(ref rect, window.GetStyles(), false, window.GetExStyles());
            return rect;
        }

        public static void GetSystemSizingFrameThickness(out CartesianValue width)
        {
            var borderPadding = User32Methods.GetSystemMetrics(SystemMetrics.SM_CXPADDEDBORDER);
            width.X = User32Methods.GetSystemMetrics(SystemMetrics.SM_CXSIZEFRAME) + borderPadding;
            width.Y = User32Methods.GetSystemMetrics(SystemMetrics.SM_CYSIZEFRAME) + borderPadding;
        }

        public static HitTestResult FrameHitTest(ref Point point, ref Rectangle frameThickness,
            ref CartesianValue sizingFrameThickness)
        {
            var x = point.X;
            var y = point.Y;

            var topEdge = frameThickness.Top;
            var leftEdge = frameThickness.Left;
            var rightEdge = frameThickness.Right;
            var bottomEdge = frameThickness.Bottom;

            var sizerXWidth = sizingFrameThickness.X;
            var sizerYWidth = sizingFrameThickness.Y;

            if (y < topEdge + sizerYWidth)
            {
                // Inside the top sizing area
                if (x < leftEdge + 2*sizerXWidth)
                {
                    return HitTestResult.HTTOPLEFT;
                }
                if (x >= rightEdge - 2*sizerXWidth)
                {
                    return HitTestResult.HTTOPRIGHT;
                }
                return HitTestResult.HTTOP;
            }
            if (y < bottomEdge - sizerYWidth)
            {
                // Inside the client area
                if (x < leftEdge + sizerXWidth)
                {
                    return HitTestResult.HTLEFT;
                }
                if (x >= rightEdge - sizerXWidth)
                {
                    return HitTestResult.HTRIGHT;
                }
                return HitTestResult.HTCLIENT;
            }
            // Inside the bottom area
            if (x < leftEdge + 2*sizerXWidth)
            {
                return HitTestResult.HTBOTTOMLEFT;
            }
            if (x >= rightEdge - 2*sizerXWidth)
            {
                return HitTestResult.HTBOTTOMRIGHT;
            }
            return HitTestResult.HTBOTTOM;
        }

        public virtual int GetTopBorderHeight()
        {
            return GetSystemTopBorderHeight();
        }

        protected virtual Margins GetDwmMargins()
        {
            return new Margins(Padding.Left, Padding.Right,
                // -Padding.Top
                // Use above instead to add Dwm top frame extension without accounting for the current offset.
                // Padding.Top should be ideally the negative value of caption area height including borders. 
                // So offseting this way, instead of subtracting, removes the title bar entirely,
                // with the exception of top border (which is a part of a client area now).
                GetTopBorderHeight() + Padding.Top,
                Padding.Bottom);
        }

        public virtual void GetSizingFrameThickness(out CartesianValue width)
        {
            GetSystemSizingFrameThickness(out width);
        }

        public virtual void GetFrameThickness(out Rectangle rect)
        {
            m_window.GetClientRect(out rect);
            User32Helpers.MapWindowPoints(m_window.Handle, IntPtr.Zero, ref rect);
            rect.Left += NcOutsetRect.Left;
            rect.Right += NcOutsetRect.Right;
            rect.Bottom += NcOutsetRect.Bottom;
        }

        public virtual void ApplyDwmConfig()
        {
            var window = m_window;
            var dwmMargins = GetDwmMargins();
            DwmApiMethods.DwmExtendFrameIntoClientArea(window.Handle, ref dwmMargins);
            var policy = (int) DwmNCRenderingPolicy.DWMNCRP_ENABLED;
            DwmApiHelpers.DwmSetWindowAttribute(window.Handle, DwmWindowAttributeType.DWMWA_NCRENDERING_POLICY,
                ref policy);
            if (BlurBehindEnabled)
                User32ExperimentalHelpers.EnableBlurBehind(window.Handle);
        }

        public virtual void OnCreate(bool drawCaptionIcon, bool drawCaptionTitle,
            bool allowSystemMenu)
        {
            var window = m_window;
            SetWindowThemeAttributes(window.Handle, drawCaptionIcon, drawCaptionTitle, allowSystemMenu);
            NcOutsetRect = GetSystemNcOutsetThickness(window);
            window.RedrawFrame();
        }

        public virtual HitTestResult HitTest(ref Point point)
        {
            Rectangle frameThickness;
            CartesianValue sizingFrameThickness;
            GetFrameThickness(out frameThickness);
            GetSizingFrameThickness(out sizingFrameThickness);
            return FrameHitTest(ref point, ref frameThickness, ref sizingFrameThickness);
        }

        public virtual HitTestResult HitTestWithCaptionArea(ref Point point)
        {
            var res = HitTest(ref point);
            var clientPoint = point;
            User32Helpers.MapWindowPoints(IntPtr.Zero, m_window.Handle, ref clientPoint);
            return res == HitTestResult.HTCLIENT && clientPoint.Y < GetCaptionHeight() ? HitTestResult.HTCAPTION : res;
        }

        public virtual int GetCaptionHeight()
        {
            return -NcOutsetRect.Top + Padding.Top + GetTopBorderHeight();
        }

        public virtual bool TryHandleNcCalcSize(ref NcCalcSizeParams ncCalcSizeParams)
        {
            if (!m_isFirstNcCalcDone)
            {
                // Use default processing the very first time to ensure compatibility - cascade
                // and stack windows don't work otherwise.
                m_isFirstNcCalcDone = true;
                return false;
            }
            ncCalcSizeParams.Region.Output.TargetClientRect.Top += 0;
            ncCalcSizeParams.Region.Output.TargetClientRect.Bottom -= NcOutsetRect.Bottom;
            ncCalcSizeParams.Region.Output.TargetClientRect.Left -= NcOutsetRect.Left;
            ncCalcSizeParams.Region.Output.TargetClientRect.Right -= NcOutsetRect.Right;
            return true;
        }
    }
}