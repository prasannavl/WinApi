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

        protected override void OnCreate(ref CreateWindowPacket packet)
        {
            DwmHelper.Initialize(DrawCaptionIcon, DrawCaptionTitle, AllowSystemMenu);
            base.OnCreate(ref packet);
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

        protected override void OnActivate(ref ActivatePacket packet)
        {
            DwmHelper.ApplyDwmConfig();
            base.OnActivate(ref packet);
        }

        protected override void OnNcCalcSize(ref NcCalcSizePacket packet)
        {
            if (!DwmHelper.TryHandleNcCalcSize(ref packet))
                base.OnNcCalcSize(ref packet);
        }

        protected override void OnNcHitTest(ref NcHitTestPacket packet)
        {
            packet.Result = DwmHelper.HitTestWithCaptionArea(packet.Point);
        }
    }
}