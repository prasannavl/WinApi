using System;
using WinApi.User32;

namespace WinApi.XWin
{
    public abstract class ConstructionParams : IConstructionParams
    {
        public virtual int X => (int)CreateWindowFlags.CW_USEDEFAULT;
        public virtual int Y => (int)CreateWindowFlags.CW_USEDEFAULT;
        public virtual int Width => (int)CreateWindowFlags.CW_USEDEFAULT;
        public virtual int Height => (int)CreateWindowFlags.CW_USEDEFAULT;
        public virtual IntPtr ParentHandle => IntPtr.Zero;
        public virtual IntPtr MenuHandle => IntPtr.Zero;
        public abstract WindowStyles Styles { get; }
        public abstract WindowExStyles ExStyles { get; }
    }

    public interface IConstructionParamsProvider
    {
        IConstructionParams GetConstructionParams();
    }

    public interface IConstructionParams
    {
        WindowStyles Styles { get; }
        WindowExStyles ExStyles { get; }
        int Width { get; }
        int Height { get; }
        int X { get; }
        int Y { get; }
        IntPtr ParentHandle { get; }
        IntPtr MenuHandle { get; }
    }

}