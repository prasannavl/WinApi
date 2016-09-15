using System;
using WinApi.DwmApi;
using WinApi.User32;

namespace WinApi.XWin
{
    public struct WindowMessage
    {
        public WM Id;
        public IntPtr WParam;
        public IntPtr LParam;
        public IntPtr Result;
        public bool Handled;

        public void SetHandled(bool handled = true)
        {
            Handled = handled;
        }
    }

    public abstract class WindowBase : WindowCoreBase
    {
        protected override void OnSourceConnected()
        {
            base.OnSourceConnected();
            if (Factory == null)
            {
                OnCreate();
            }
        }

        protected virtual void OnCreate() {}

        protected virtual void OnActivate() {}

        protected virtual void OnClose() {}

        protected virtual void OnDestroy() {}

        protected virtual void OnPaint() {}

        protected virtual void OnMessageProcessDefault(ref WindowMessage msg) { }

        protected virtual void OnMessage(ref WindowMessage msg)
        {
            switch (msg.Id)
            {
                case WM.ACTIVATE:
                {
                    OnActivate();
                    return;
                }
                case WM.CREATE:
                {
                    OnCreate();
                    return;
                }
                case WM.DESTROY:
                {
                    OnDestroy();
                    return;
                }
                case WM.CLOSE:
                {
                    OnClose();
                    return;
                }
                case WM.PAINT:
                {
                    PaintStruct ps;
                    var hdc = User32Methods.BeginPaint(Handle, out ps);
                    OnPaint();
                    User32Methods.EndPaint(Handle, ref ps);
                    return;
                }
                default:
                {
                    if (!msg.Handled)
                    {
                        OnMessageProcessDefault(ref msg);
                    }
                    return;
                }
            }
        }

        protected internal override IntPtr WindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            var wmsg = new WindowMessage
            {
                Id = (WM) msg,
                WParam = wParam,
                LParam = lParam,
                Result = IntPtr.Zero,
                Handled = false
            };

            OnMessage(ref wmsg);
            return wmsg.Handled ? wmsg.Result : base.WindowProc(hwnd, msg, wParam, lParam);
        }
    }

    public abstract class MainWindowBase : WindowBase
    {
        protected override void OnMessageProcessDefault(ref WindowMessage msg)
        {
            IntPtr res;
            if (DwmApiMethods.DwmDefWindowProc(Handle, (uint) msg.Id, msg.WParam, msg.LParam, out res) > 0)
            {
                msg.Result = res;
                msg.SetHandled();
            }
            base.OnMessageProcessDefault(ref msg);
        }

        protected override void OnDestroy()
        {
            User32Methods.PostQuitMessage(0);
            base.OnDestroy();
        }
    }

    public sealed class MainWindow : MainWindowBase {}

    public sealed class Window : WindowBase {}
}