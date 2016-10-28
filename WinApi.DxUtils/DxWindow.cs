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

        protected override void OnCreate(ref CreateWindowPacket packet)
        {
            Dx.Initialize(Handle, GetClientSize());
            base.OnCreate(ref packet);
        }

        protected virtual void OnDxPaint(Dx11Component resource) {}

        protected override void OnPaint(ref PaintPacket packet)
        {
            DxPainter.HandlePaint(this, Dx, OnDxPaint);
        }

        protected override void OnSize(ref SizePacket packet)
        {
            Dx.Resize(packet.Size);
            base.OnSize(ref packet);
        }

        private bool m_isFirstBkgdErased;

        protected override void OnEraseBkgnd(ref EraseBkgndPacket packet)
        {
            if (m_isFirstBkgdErased)
            {
                packet.Result = EraseBackgroundResult.DisableDefaultErase;
                return;
            }
            m_isFirstBkgdErased = true;
            base.OnEraseBkgnd(ref packet);
        }

        protected override void Dispose(bool disposing)
        {
            Dx.Dispose();
            base.Dispose(disposing);
        }
    }
}