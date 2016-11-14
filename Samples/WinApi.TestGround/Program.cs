using NetCoreEx.Geometry;
using System;
using System.Text;
using System.Threading.Tasks;
using WinApi.Desktop;
using WinApi.User32;
using WinApi.Windows;
using WinApi.Windows.Controls;
using WinApi.Windows.Controls.Layouts;

namespace WinApi.TestGround
{
    class Program
    {
        static int Main(string[] args)
        {
            ApplicationHelpers.SetupDefaultExceptionHandlers();
            try
            {
                var factory = WindowFactory.Create();
                using (var win = factory.CreateWindow(() => new MainWindow(),
                    "Hello", constructionParams: new FrameWindowConstructionParams()))
                {
                    win.Show();
                    return new EventLoop().Run(win);
                }
            }
            catch (Exception ex) {
                ApplicationHelpers.ShowCriticalError(ex);
            }
            return 0;
        }

        public sealed class MainWindow : EventedWindowCore
        {
            private const int Iterations = 100_000;

            private readonly HorizontalStretchLayout m_layout = new HorizontalStretchLayout();
            private bool m_done;
            private DateTime m_endTime;
            private DateTime m_startTime;
            private Task m_task;
            private StaticBox m_textBox1;
            private NativeWindow m_textBox2;
            private int m_times;

            protected override void OnCreate(ref CreateWindowPacket packet)
            {
 
                this.m_textBox1 = StaticBox.Create(hParent: this.Handle,
                    styles: WindowStyles.WS_CHILD | WindowStyles.WS_VISIBLE, exStyles: 0);
                
                // You can use this to create the static box like this as well. 
                // But there's rarely any performance benefit in doing so, and
                // this doesn't have a WindowProc that's connected.
                this.m_textBox2 = WindowFactory.CreateExternalWindow("static", hParent: this.Handle,
                    styles: WindowStyles.WS_CHILD | WindowStyles.WS_VISIBLE, exStyles: 0);

                this.m_layout.ClientArea = this.GetClientRect();
                this.m_layout.Margin = new Rectangle(10, 10, 10, 10);
                this.m_layout.Children.Add(this.m_textBox1);
                this.m_layout.Children.Add(this.m_textBox2);
                this.m_layout.PerformLayout();

                var r = new Random();

                this.m_task = Task.Run(() =>
                {
                    while (this.m_times < Iterations)
                    {
                        this.m_times++;
                        this.SetPosition(50, 50, 1200 - r.Next(0, 1100), 900 - r.Next(0, 800));
                    }
                    this.m_endTime = DateTime.Now;
                    this.m_done = true;
                    this.SetPosition(50, 50, 700, 500);
                });
                this.m_startTime = DateTime.Now;
                base.OnCreate(ref packet);
            }

            protected override void OnSize(ref SizePacket packet)
            {
                var size = packet.Size;
                this.m_layout.SetSize(ref size);

                base.OnSize(ref packet);

                if (!this.m_done) return;

                var str = $"\r\n{DateTime.Now}: No. of changes done: {this.m_times}";
                this.m_textBox1.SetText(str);

                var sb = new StringBuilder();

                sb.AppendLine($"Start Time: {this.m_startTime}");
                sb.AppendLine($"End Time: {this.m_endTime}");
                sb.AppendLine();

                if (this.m_endTime != DateTime.MinValue) sb.AppendLine($"Total Time: {this.m_endTime - this.m_startTime}");
                this.m_textBox2.SetText(sb.ToString());
            }
        }
    }
}