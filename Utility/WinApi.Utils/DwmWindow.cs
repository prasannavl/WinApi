using System;
using WinApi.Core;
using WinApi.DwmApi;
using WinApi.Helpers;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.UxTheme;
using WinApi.Windows;
using WinApi.Windows.Helpers;

namespace WinApi.Utils
{
    public class DwmWindow : EventedWindowCore
    {
        protected readonly DwmWindowHelper DwmHelper;

        public DwmWindow()
        {
            DwmHelper = new DwmWindowHelper(this);
        }

        protected bool DrawCaptionIcon { get; set; }
        protected bool DrawCaptionTitle { get; set; }
        protected bool AllowSystemMenu { get; set; }

        protected override CreateWindowResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            DwmHelper.Initialize(DrawCaptionIcon, DrawCaptionTitle, AllowSystemMenu);
            return base.OnCreate(ref msg, ref createStruct);
        }

        public override void ClientToScreen(ref Rectangle clientRect, out Rectangle screenRect)
        {
            base.ClientToScreen(ref clientRect, out screenRect);
            screenRect.Top += -DwmHelper.NcOutsetThickness.Top;
        }

        public override void ScreenToClient(ref Rectangle screenRect, out Rectangle clientRect)
        {
            screenRect.Top -= -DwmHelper.NcOutsetThickness.Top;
            base.ScreenToClient(ref screenRect, out clientRect);
        }

        protected override void OnActivate(ref WindowMessage msg, WindowActivateFlag flag, bool isMinimized,
            IntPtr oppositeHwnd)
        {
            DwmHelper.ApplyDwmConfig();
            base.OnActivate(ref msg, flag, isMinimized, oppositeHwnd);
        }

        protected override WindowViewRegionFlags OnNcCalcSize(ref WindowMessage msg, bool shouldCalcValidRects,
            ref NcCalcSizeParams ncCalcSizeParams)
        {
            if (DwmHelper.TryHandleNcCalcSize(ref ncCalcSizeParams))
                return (WindowViewRegionFlags) msg.Result;
            return base.OnNcCalcSize(ref msg, shouldCalcValidRects, ref ncCalcSizeParams);
        }

        protected override HitTestResult OnHitTest(ref WindowMessage msg, ref Point point)
        {
            return DwmHelper.HitTestWithCaptionArea(ref point);
        }
    }
}