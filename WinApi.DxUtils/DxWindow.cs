using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.DxUtils.Component;
using WinApi.User32;
using WinApi.Windows;

namespace WinApi.DxUtils
{
    public class DxWindow : EventedWindowCore
    {
        protected readonly Dx11Component Dx = new Dx11Component();

        private bool m_isFirstBkgdErased;

        protected override void OnCreate(ref CreateWindowPacket packet)
        {
            this.Dx.Initialize(this.Handle, this.GetClientSize());
            base.OnCreate(ref packet);
        }

        protected virtual void OnDxPaint(Dx11Component resource) {}

        protected override void OnPaint(ref PaintPacket packet)
        {
            DxPainter.HandlePaint(this, this.Dx, this.OnDxPaint);
        }

        protected override void OnSize(ref SizePacket packet)
        {
            this.Dx.Resize(packet.Size);
            base.OnSize(ref packet);
        }

        protected override void OnEraseBkgnd(ref EraseBkgndPacket packet)
        {
            if (this.m_isFirstBkgdErased)
            {
                packet.Result = EraseBackgroundResult.DisableDefaultErase;
                return;
            }
            this.m_isFirstBkgdErased = true;
            base.OnEraseBkgnd(ref packet);
        }

        protected override void Dispose(bool disposing)
        {
            this.Dx.Dispose();
            base.Dispose(disposing);
        }
    }
}