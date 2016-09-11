using System;
using WinApi.User32;

namespace WinApi.XWin
{
    public interface IEventLoop
    {
        int Run();
    }

    public abstract class EventLoopBase : IEventLoop
    {
        private object m_state;

        protected EventLoopBase(object state)
        {
            m_state = state;
        }

        public virtual int Run()
        {
            Message msg;
            int res;
            while ((res = User32Methods.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
            {
                User32Methods.TranslateMessage(ref msg);
                User32Methods.DispatchMessage(ref msg);
            }
            return res;
        }
    }

    public sealed class EventLoop : EventLoopBase
    {
        public EventLoop(object state = null) : base(state) {}
    }

    public abstract class RealtimeEventLoopBase : IEventLoop
    {
        private object m_state;

        protected RealtimeEventLoopBase(object state)
        {
            m_state = state;
        }

        public virtual int Run()
        {
            Message msg;
            var quitMsg = (uint) WM.QUIT;
            do
            {
                if (User32Helpers.PeekMessage(out msg, IntPtr.Zero, 0, 0, PeekMessageFlags.PM_REMOVE) != 0)
                {
                    User32Methods.TranslateMessage(ref msg);
                    User32Methods.DispatchMessage(ref msg);
                }
            } while (msg.Value != quitMsg);
            return 0;
        }
    }

    public sealed class RealtimeEventLoop : RealtimeEventLoopBase
    {
        public RealtimeEventLoop(object state = null) : base(state) {}
    }

    public abstract class InterceptableEventLoopBase : IEventLoop
    {
        private object m_state;

        protected InterceptableEventLoopBase(object state)
        {
            m_state = state;
        }

        public virtual int Run()
        {
            Message msg;
            int res;
            while ((res = User32Methods.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
                if (Preprocess(ref msg))
                {
                    User32Methods.TranslateMessage(ref msg);
                    if (PostTranslate(ref msg))
                        User32Methods.DispatchMessage(ref msg);
                    PostProcess(ref msg);
                }
            return res;
        }

        protected bool Preprocess(ref Message msg) => true;
        protected bool PostTranslate(ref Message msg) => true;
        protected void PostProcess(ref Message msg) {}
    }

    public sealed class InterceptableEventLoop : InterceptableEventLoopBase
    {
        public InterceptableEventLoop(object state = null) : base(state) {}
    }

    public abstract class InterceptableRealtimeEventLoopBase : IEventLoop
    {
        private object m_state;

        protected InterceptableRealtimeEventLoopBase(object state)
        {
            m_state = state;
        }

        public virtual int Run()
        {
            Message msg;
            var quitMsg = (uint) WM.QUIT;
            do
            {
                if (User32Helpers.PeekMessage(out msg, IntPtr.Zero, 0, 0, PeekMessageFlags.PM_REMOVE) != 0)
                    if (Preprocess(ref msg))
                    {
                        User32Methods.TranslateMessage(ref msg);
                        if (PostTranslate(ref msg))
                            User32Methods.DispatchMessage(ref msg);
                        PostProcess(ref msg);
                    }
            } while (msg.Value != quitMsg);
            return 0;
        }

        protected bool Preprocess(ref Message msg) => true;
        protected bool PostTranslate(ref Message msg) => true;
        protected void PostProcess(ref Message msg) {}
    }

    public sealed class InterceptableRealtimeEventLoop : InterceptableRealtimeEventLoopBase
    {
        public InterceptableRealtimeEventLoop(object state = null) : base(state) {}
    }
}