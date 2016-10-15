using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using WinApi.Core;
using WinApi.User32;
using WinApi.Windows;

namespace WinApi.DxUtils
{
    public class DxWindow : EventedWindowCore
    {
        protected readonly Dx11MetaResource Dx = new Dx11MetaResource();

        protected override CreateWindowResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            Dx.Initialize(Handle, GetClientSize());
            return base.OnCreate(ref msg, ref createStruct);
        }

        protected virtual void OnDxPaint(Dx11MetaResource resource) {}

        protected override void OnPaint(ref WindowMessage msg, IntPtr cHdc)
        {
            DxPainter.HandlePaint(this, Dx, OnDxPaint);
        }

        protected override void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size)
        {
            Dx.Resize(size);
        }

        private bool m_isFirstBkgdErased;


        protected override EraseBackgroundResult OnEraseBkgnd(ref WindowMessage msg, IntPtr cHdc)
        {
            if (m_isFirstBkgdErased)
            {
                return EraseBackgroundResult.DisableDefaultErase;
            }
            m_isFirstBkgdErased = true;
            return base.OnEraseBkgnd(ref msg, cHdc);
        }

        protected override void Dispose(bool disposing)
        {
            Dx.Dispose();
            base.Dispose(disposing);
        }
    }
}