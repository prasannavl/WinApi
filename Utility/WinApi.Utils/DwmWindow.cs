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
        private readonly DwmWindowHelper m_dwmHelper;

        public DwmWindow()
        {
            m_dwmHelper = new DwmWindowHelper(this);
        }

        protected bool DrawCaptionIcon { get; set; }
        protected bool DrawCaptionTitle { get; set; }
        protected bool AllowSystemMenu { get; set; }

        protected override CreateWindowResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            m_dwmHelper.OnCreate(DrawCaptionIcon, DrawCaptionTitle, AllowSystemMenu);
            return base.OnCreate(ref msg, ref createStruct);
        }

        protected override void OnActivate(ref WindowMessage msg, WindowActivateFlag flag, bool isMinimized,
            IntPtr oppositeHwnd)
        {
            m_dwmHelper.ApplyDwmConfig();
            base.OnActivate(ref msg, flag, isMinimized, oppositeHwnd);
        }

        protected override WindowViewRegionFlags OnNcCalcSize(ref WindowMessage msg, bool shouldCalcValidRects,
            ref NcCalcSizeParams ncCalcSizeParams)
        {
            if (m_dwmHelper.TryHandleNcCalcSize(ref ncCalcSizeParams))
                return (WindowViewRegionFlags) msg.Result;
            return base.OnNcCalcSize(ref msg, shouldCalcValidRects, ref ncCalcSizeParams);
        }

        protected override HitTestResult OnHitTest(ref WindowMessage msg, ref Point point)
        {
            var res = m_dwmHelper.HitTest(ref point);
            if (res == HitTestResult.HTCLIENT)
            {
                if (point.Y < m_dwmHelper.GetCaptionHeight())
                {
                    // Also do button hit testing
                    return HitTestResult.HTCAPTION;
                }
            }
            return res;
        }
    }
}