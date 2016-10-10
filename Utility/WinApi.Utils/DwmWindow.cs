using System;
using WinApi.Core;
using WinApi.DwmApi;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.UxTheme;
using WinApi.Windows;

namespace WinApi.Utils
{
    public class DwmWindow : EventedWindowCore
    {
        protected Rectangle DwmFrameInsetRect;
        protected Rectangle DwmFrameOutsetRect;
        private bool m_isFirstNcCalcDone;

        protected bool DwmDrawCaptionIcon { get; set; }
        protected bool DwmDrawCaptionTitle { get; set; }
        protected bool DwmBlurBehind { get; set; }
        protected bool DwmAllowSystemMenu { get; set; }

        protected virtual int GetSystemTopBorderHeight()
        {
            return User32Methods.GetSystemMetrics(SystemMetrics.SM_CYBORDER);
        }

        protected override CreateWindowResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            SetupWindowThemeAttributes();
            CalculateNcOutset(ref DwmFrameOutsetRect);
            RedrawFrame();
            return base.OnCreate(ref msg, ref createStruct);
        }

        protected void CalculateNcOutset(ref Rectangle outsetRect)
        {
            User32Methods.AdjustWindowRectEx(ref outsetRect, GetStyles(), false, GetExStyles());
        }

        protected virtual Margins GetDwmMargins()
        {
            return new Margins(DwmFrameInsetRect.Left, DwmFrameInsetRect.Right,
                // -DwmFrameInsetRect.Top
                // Use above instead to add Dwm top frame extension without accounting for the current offset.
                // DwmFrameInsetRect.Top should be ideally the negative value of caption area height including borders. 
                // So offseting this way, instead of subtracting, removes the title bar entirely,
                // with the exception of top border (which is a part of a client area now).
                GetSystemTopBorderHeight() + DwmFrameInsetRect.Top,
                DwmFrameInsetRect.Bottom);
        }

        protected virtual void SetupWindowThemeAttributes()
        {
            WindowThemeNcAttributeFlags flags = 0;
            if (!DwmDrawCaptionIcon)
                flags |= WindowThemeNcAttributeFlags.WTNCA_NODRAWICON;
            if (!DwmDrawCaptionTitle)
                flags |= WindowThemeNcAttributeFlags.WTNCA_NODRAWCAPTION;
            if (!DwmAllowSystemMenu)
                flags |= WindowThemeNcAttributeFlags.WTNCA_NOSYSMENU;
            UxThemeHelpers.SetWindowThemeNonClientAttributes(Handle,
                WindowThemeNcAttributeFlags.WTNCA_VALIDBITS & ~WindowThemeNcAttributeFlags.WTNCA_NOMIRRORHELP, flags);
        }

        protected virtual void SetupDwm()
        {
            var dwmMargins = GetDwmMargins();
            DwmApiMethods.DwmExtendFrameIntoClientArea(Handle, ref dwmMargins);
            var policy = (int) DwmNCRenderingPolicy.DWMNCRP_ENABLED;
            DwmApiHelpers.DwmSetWindowAttribute(Handle, DwmWindowAttributeType.DWMWA_NCRENDERING_POLICY,
                ref policy);
            if (DwmBlurBehind)
                User32ExperimentalHelpers.EnableBlurBehind(Handle);
        }

        protected override void OnActivate(ref WindowMessage msg, WindowActivateFlag flag, bool isMinimized,
            IntPtr oppositeHwnd)
        {
            SetupDwm();
            base.OnActivate(ref msg, flag, isMinimized, oppositeHwnd);
        }

        protected override WindowViewRegionFlags OnNcCalcSize(ref WindowMessage msg, bool shouldCalcValidRects,
            ref NcCalcSizeParams ncCalcSizeParams)
        {
            if (!m_isFirstNcCalcDone)
            {
                // Use default processing the very first time to ensure compatibility - cascade
                // and stack windows don't work otherwise.
                m_isFirstNcCalcDone = true;
                return base.OnNcCalcSize(ref msg, shouldCalcValidRects, ref ncCalcSizeParams);
            }
            ncCalcSizeParams.Region.Output.TargetClientRect.Top += 0;
            ncCalcSizeParams.Region.Output.TargetClientRect.Bottom -= DwmFrameOutsetRect.Bottom;
            ncCalcSizeParams.Region.Output.TargetClientRect.Left -= DwmFrameOutsetRect.Left;
            ncCalcSizeParams.Region.Output.TargetClientRect.Right -= DwmFrameOutsetRect.Right;
            msg.SetHandled();
            return 0;
        }

        protected virtual void GetFrameEdgeRect(out Rectangle rect)
        {
            GetClientRect(out rect);
            User32Helpers.MapWindowPoints(Handle, IntPtr.Zero, ref rect);
            rect.Left += DwmFrameOutsetRect.Left;
            rect.Right += DwmFrameOutsetRect.Right;
            rect.Bottom += DwmFrameOutsetRect.Bottom;
        }

        public static void GetFrameEdgeSizerWidth(out CartesianWidth width)
        {
            var borderPadding = User32Methods.GetSystemMetrics(SystemMetrics.SM_CXPADDEDBORDER);
            width.X = User32Methods.GetSystemMetrics(SystemMetrics.SM_CXSIZEFRAME) + borderPadding;
            width.Y = User32Methods.GetSystemMetrics(SystemMetrics.SM_CYSIZEFRAME) + borderPadding;
        }

        protected static HitTestResult FrameHitTest(ref Point point, ref Rectangle frameEdgeRect,
            ref CartesianWidth frameEdgeSizerWidth)
        {
            var x = point.X;
            var y = point.Y;

            var topEdge = frameEdgeRect.Top;
            var leftEdge = frameEdgeRect.Left;
            var rightEdge = frameEdgeRect.Right;
            var bottomEdge = frameEdgeRect.Bottom;

            var sizerXWidth = frameEdgeSizerWidth.X;
            var sizerYWidth = frameEdgeSizerWidth.Y;

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

        protected override HitTestResult OnHitTest(ref WindowMessage msg, ref Point point)
        {
            Rectangle frameEdge;
            CartesianWidth frameSizerWidth;
            GetFrameEdgeRect(out frameEdge);
            GetFrameEdgeSizerWidth(out frameSizerWidth);
            var captionHeight = -DwmFrameOutsetRect.Top + DwmFrameInsetRect.Top;
            msg.SetHandled();
            var res = FrameHitTest(ref point, ref frameEdge, ref frameSizerWidth);
            if (res == HitTestResult.HTCLIENT)
            {
                if (point.Y < frameEdge.Top + captionHeight)
                {
                    // Also do button hit testing
                    return HitTestResult.HTCAPTION;
                }
            }
            return res;
        }
    }
}