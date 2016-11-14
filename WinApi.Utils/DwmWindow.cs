using NetCoreEx.Geometry;
using System;
using WinApi.Core;
using WinApi.DwmApi;
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
            this.DwmHelper = new DwmWindowHelper(this);
        }

        protected bool DrawCaptionIcon { get; set; }
        protected bool DrawCaptionTitle { get; set; }
        protected bool AllowSystemMenu { get; set; }

        protected override void OnCreate(ref CreateWindowPacket packet)
        {
            this.DwmHelper.Initialize(this.DrawCaptionIcon, this.DrawCaptionTitle, this.AllowSystemMenu);
            base.OnCreate(ref packet);
        }

        public override void ClientToScreen(ref Rectangle clientRect, out Rectangle screenRect)
        {
            base.ClientToScreen(ref clientRect, out screenRect);
            screenRect.Top += -this.DwmHelper.NcOutsetThickness.Top;
        }

        public override void ScreenToClient(ref Rectangle screenRect, out Rectangle clientRect)
        {
            screenRect.Top -= -this.DwmHelper.NcOutsetThickness.Top;
            base.ScreenToClient(ref screenRect, out clientRect);
        }

        protected override void OnActivate(ref ActivatePacket packet)
        {
            this.DwmHelper.ApplyDwmConfig();
            base.OnActivate(ref packet);
        }

        protected override void OnNcCalcSize(ref NcCalcSizePacket packet)
        {
            if (this.DwmHelper.TryHandleNcCalcSize(ref packet)) return;
            base.OnNcCalcSize(ref packet);
        }

        protected override void OnNcHitTest(ref NcHitTestPacket packet)
        {
            packet.Result = this.DwmHelper.HitTestWithCaptionArea(packet.Point);
        }
    }
}