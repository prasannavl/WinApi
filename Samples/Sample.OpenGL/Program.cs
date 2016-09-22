using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using WinApi.Desktop;
using WinApi.Helpers;
using WinApi.XWin;

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
                ApplicationHelpers.InitializeCriticalErrorDisplay();
                Gl.Initialize();
                var factory = WindowFactory.Create("MainWindow");
                using (var win = factory.CreateFrameWindow<OpenGlAppWindow>(text: "Hello"))
                {
                    win.Show();
                    return new EventLoop(win).Run();
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelpers.ShowError(ex);
                return 1;
            }
        }
    }

    public class OpenGlAppWindow : OpenGlMainWindowBase
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

        protected override void OnGlPaint()
        {
            var size = GetClientSize();
            Gl.Viewport(0, 0, size.Width, size.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            Gl.Begin(PrimitiveType.Triangles);
            Gl.Color3(1.0f, 0.0f, 0.0f);
            Gl.Vertex2(0.0f, 0.0f);
            Gl.Color3(0.0f, 1.0f, 0.0f);
            Gl.Vertex2(0.5f, 1.0f);
            Gl.Color3(0.0f, 0.0f, 1.0f);
            Gl.Vertex2(1.0f, 0.0f);
            Gl.End();
            DeviceContext.SwapBuffers();
            base.OnGlPaint();
        }
    }
}