using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using WinApi.Core;
using WinApi.Desktop;
using WinApi.Gdi32;
using WinApi.User32;
using WinApi.Windows;
using WinApi.Windows.Controls;
using WinApi.Windows.Helpers;

namespace Sample.OpenGL
{
    /// <summary>
    ///     NOTE: This references a modified version of OpenGL.Net, with GitHub PR #13 applied.
    ///     If it isn't applied, inside the `CreateDeviceContext` method of OpenGlWindowBase
    ///     the `DeviceContextFactory.Create` should be passed a throw-away `Control` class
    ///     from Windows.Forms that wraps the Handle IntPtr. Or else, this code will not compile.
    ///     A patched version of OpenGL.Net with PR #13 applied can be found at
    ///     https://github.com/prasannavl/OpenGL.Net meanwhile.
    /// </summary>
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                ApplicationHelpers.SetupDefaultExceptionHandlers();
                Gl.Initialize();
                var factory = WindowFactory.Create(hBgBrush: Gdi32Helpers.GetStockObject(StockObject.BLACK_BRUSH));
                using (var win = Window.Create<AppWindow>(factory: factory, text: "Hello"))
                {
                    win.Show();
                    return new EventLoop().Run(win);
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelpers.ShowError(ex);
                return 1;
            }
        }
    }

    public sealed class AppWindow : OpenGlWindow
    {
        protected override void OnGlContextCreated()
        {
            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadIdentity();
            Gl.Ortho(0.0, 1.0f, 0.0, 1.0, 0.0, 1.0);

            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();
            base.OnGlContextCreated();
        }

        protected override void OnGlPaint(ref PaintStruct ps)
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            var size = this.GetClientSize();
            Gl.Viewport(0, 0, size.Width, size.Height);
            Gl.Begin(PrimitiveType.Triangles);
            Gl.Color3(1.0f, 0.0f, 0.0f);
            Gl.Vertex2(0.0f, 0.0f);
            Gl.Color3(0.0f, 1.0f, 0.0f);
            Gl.Vertex2(0.5f, 1.0f);
            Gl.Color3(0.0f, 0.0f, 1.0f);
            Gl.Vertex2(1.0f, 0.0f);
            Gl.End();
            this.DeviceContext.SwapBuffers();
        }
    }
}