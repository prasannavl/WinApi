using System;
using WinApi.User32;

namespace WinApi.Windows
{
    public interface IConstructionParamsProvider
    {
        IConstructionParams GetConstructionParams();
    }

    public interface IConstructionParams
    {
        WindowStyles Styles { get; }
        WindowExStyles ExStyles { get; }
        uint ControlStyles { get; }
        int Width { get; }
        int Height { get; }
        int X { get; }
        int Y { get; }
        IntPtr ParentHandle { get; }
        IntPtr MenuHandle { get; }
    }

    public class ConstructionParams : IConstructionParams
    {
        public virtual int X => (int) CreateWindowFlags.CW_USEDEFAULT;
        public virtual int Y => (int) CreateWindowFlags.CW_USEDEFAULT;
        public virtual int Width => (int) CreateWindowFlags.CW_USEDEFAULT;
        public virtual int Height => (int) CreateWindowFlags.CW_USEDEFAULT;
        public virtual IntPtr ParentHandle => IntPtr.Zero;
        public virtual IntPtr MenuHandle => IntPtr.Zero;
        public virtual WindowStyles Styles => 0;
        public virtual uint ControlStyles => 0;
        public virtual WindowExStyles ExStyles => 0;
    }

    public class FrameWindowConstructionParams : ConstructionParams
    {
        public override WindowStyles Styles
            => WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS;

        public override WindowExStyles ExStyles
            => WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_WINDOWEDGE;
    }

    public class VisibleChildConstructionParams : ConstructionParams
    {
        public override WindowStyles Styles
            => WindowStyles.WS_VISIBLE | WindowStyles.WS_CHILD;

        public override WindowExStyles ExStyles
            => WindowExStyles.WS_EX_STATICEDGE;
    }
}