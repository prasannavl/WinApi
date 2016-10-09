using System;
using WinApi.Core;

namespace WinApi.DxUtils.Contexts
{
    public interface IGraphicsContext : IDisposable {
        void Init(IntPtr hwnd, ref Size size, bool deferInitUntilFirstDraw = true);
        void Draw();
        void Resize(ref Size size);
    }
}