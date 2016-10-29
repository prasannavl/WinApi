using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using WinApi.DxUtils.Component;
using WinApi.Windows;

namespace WinApi.DxUtils
{
    public static class DxPainter
    {
        public static void HandlePaint(EventedWindowCore window, Dx11Component resource,
            Action<Dx11Component> handler)
        {
            resource.EnsureInitialized();
            try
            {
                handler(resource);
                resource.D3D.SwapChain.Present(1, 0);
                window.Validate();
            }
            catch (SharpDXException ex)
            {
                if (!resource.PerformResetOnException(ex)) throw;
            }
        }

        public static void HandlePaintD2D(EventedWindowCore window, Dx11Component resource,
            Action<DeviceContext> handler)
        {
            resource.EnsureInitialized();
            try
            {
                var context = resource.D2D.Context;
                context.BeginDraw();
                handler(context);
                context.EndDraw();
                resource.D3D.SwapChain.Present(1, 0);
                window.Validate();
            }
            catch (SharpDXException ex)
            {
                if (!resource.PerformResetOnException(ex)) throw;
            }
        }

        public static void HandlePaintD2DClipped(EventedWindowCore window, Dx11Component resource,
            Action<DeviceContext> handler, RawRectangleF clip, RawColor4? clearColorBeforeClip = null)
        {
            HandlePaintD2DClipped(window, resource, handler, ref clip, clearColorBeforeClip);
        }

        public static void HandlePaintD2DClipped(EventedWindowCore window, Dx11Component resource,
            Action<DeviceContext> handler, ref RawRectangleF clip, RawColor4? clearColorBeforeClip = null)
        {
            resource.EnsureInitialized();
            try
            {
                var context = resource.D2D.Context;
                context.BeginDraw();
                if (clearColorBeforeClip.HasValue) context.Clear(clearColorBeforeClip.Value);
                context.PushAxisAlignedClip(clip,
                    AntialiasMode.Aliased);
                handler(context);
                context.PopAxisAlignedClip();
                context.EndDraw();
                resource.D3D.SwapChain.Present(1, 0);
                window.Validate();
            }
            catch (SharpDXException ex)
            {
                if (!resource.PerformResetOnException(ex)) throw;
            }
        }
    }
}