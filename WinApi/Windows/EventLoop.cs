using System;
using WinApi.User32;
using WinApi.Windows.Helpers;

namespace WinApi.Windows
{
    public interface IEventLoop
    {
        int Run(WindowCore mainWindow = null);
    }

    public abstract class EventLoopCore : IEventLoop
    {
        protected object State;

        protected EventLoopCore(object state)
        {
            this.State = state;
        }

        public int Run(WindowCore mainWindow = null)
        {
            Action destroyHandler = () => { MessageHelpers.PostQuitMessage(); };
            if (mainWindow != null) mainWindow.Destroyed += destroyHandler;
            var res = this.RunCore();
            // Technically, this can be avoided by setting the handler to auto disconnect.
            // However, this helps keep the mainWindow alive, and use this instead of 
            // GC.KeepAlive pattern.
            if (mainWindow != null) mainWindow.Destroyed -= destroyHandler;
            return res;
        }

        public abstract int RunCore();
    }


    public class EventLoop : EventLoopCore
    {
        public EventLoop(object state = null) : base(state) {}

        public override int RunCore()
        {
            Message msg;
            int res;
            while ((res = User32Methods.GetMessage(out msg, IntPtr.Zero, 0, 0)) > 0)
            {
                User32Methods.TranslateMessage(ref msg);
                User32Methods.DispatchMessage(ref msg);
            }
            return res;
        }
    }

    public class RealtimeEventLoop : EventLoopCore
    {
        public RealtimeEventLoop(object state = null) : base(state) {}

        public override int RunCore()
        {
            Message msg;
            var quitMsgId = (uint) WM.QUIT;
            do
            {
                if (User32Helpers.PeekMessage(out msg, IntPtr.Zero, 0, 0, PeekMessageFlags.PM_REMOVE))
                {
                    User32Methods.TranslateMessage(ref msg);
                    User32Methods.DispatchMessage(ref msg);
                }
            } while (msg.Value != quitMsgId);
            return 0;
        }
    }

    public abstract class InterceptableEventLoopCore : EventLoopCore
    {
        protected InterceptableEventLoopCore(object state) : base(state) {}
        protected virtual bool Preprocess(ref Message msg) => true;
        protected virtual bool PostTranslate(ref Message msg) => true;
        protected virtual void PostProcess(ref Message msg) {}
    }

    public abstract class InterceptableEventLoopBase : InterceptableEventLoopCore
    {
        protected InterceptableEventLoopBase(object state) : base(state) {}

        public override int RunCore()
        {
            Message msg;
            int res;
            while ((res = User32Methods.GetMessage(out msg, IntPtr.Zero, 0, 0)) > 0)
            {
                if (this.Preprocess(ref msg))
                {
                    User32Methods.TranslateMessage(ref msg);
                    if (this.PostTranslate(ref msg)) User32Methods.DispatchMessage(ref msg);
                    this.PostProcess(ref msg);
                }
            }
            return res;
        }
    }

    public abstract class InterceptableRealtimeEventLoopBase : InterceptableEventLoopCore
    {
        protected InterceptableRealtimeEventLoopBase(object state) : base(state) {}

        public override int RunCore()
        {
            Message msg;
            var quitMsg = (uint) WM.QUIT;
            do
            {
                if (User32Helpers.PeekMessage(out msg, IntPtr.Zero, 0, 0, PeekMessageFlags.PM_REMOVE))
                    if (this.Preprocess(ref msg))
                    {
                        User32Methods.TranslateMessage(ref msg);
                        if (this.PostTranslate(ref msg)) User32Methods.DispatchMessage(ref msg);
                        this.PostProcess(ref msg);
                    }
            } while (msg.Value != quitMsg);
            return 0;
        }
    }
}