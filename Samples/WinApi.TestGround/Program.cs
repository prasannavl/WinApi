using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinApi.Core;
using WinApi.Desktop;
using WinApi.DwmApi;
using WinApi.Extensions;
using WinApi.Gdi32;
using WinApi.Kernel32;
using WinApi.User32;
using WinApi.User32.Experimental;
using WinApi.UxTheme;
using WinApi.Windows;
using WinApi.Windows.Controls;
using WinApi.Windows.Controls.Layouts;
using WinApi.Windows.Helpers;

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
                    text: "Hello", constructionParams: new FrameWindowConstructionParams()))
                {
                    win.Show();
                    return new EventLoop().Run(win);
                }
            }
            catch (Exception ex)
            {
                ApplicationHelpers.ShowCriticalError(ex);
            }
            return 0;
        }

        public sealed class MainWindow : EventedWindowCore
        {
            private Task m_task;
            private bool m_done;
            private StaticBox m_textBox1;
            private StaticBox m_textBox2;
            private DateTime m_startTime;
            private DateTime m_endTime;
            private int m_times;
            private const int Iterations = 100_000;

            private readonly HorizontalStretchLayout m_layout = new HorizontalStretchLayout();

            protected override void OnCreate(ref CreateWindowPacket packet)
            {
                m_textBox1 = StaticBox.Create(hParent: Handle, styles: WindowStyles.WS_CHILD | WindowStyles.WS_VISIBLE, exStyles: 0);
                m_textBox2 = StaticBox.Create(hParent: Handle, styles: WindowStyles.WS_CHILD | WindowStyles.WS_VISIBLE, exStyles: 0);

                m_layout.ClientArea = GetClientRect();
                m_layout.Margin = new Rectangle(10, 10, 10, 10);
                m_layout.Children.Add(m_textBox1);
                m_layout.Children.Add(m_textBox2);
                m_layout.PerformLayout();

                var r = new Random();

                m_task = Task.Run(() =>
                {
                    while (m_times < Iterations)
                    {
                        m_times++;
                        this.SetPosition(50, 50, 1200 - r.Next(0, 1100), 900 - r.Next(0, 800));
                    }
                    m_endTime = DateTime.Now;
                    m_done = true;
                    this.SetPosition(50, 50, 700, 500);
                });
                m_startTime = DateTime.Now;
                base.OnCreate(ref packet);
            }

            protected override void OnSize(ref SizePacket packet)
            {
                var size = packet.Size;
                m_layout.SetSize(ref size);

                base.OnSize(ref packet);

                if (!m_done) return;

                var str = $"\r\n{DateTime.Now}: No. of changes done: {m_times}";
                m_textBox1.SetText(str);

                var sb = new StringBuilder();

                sb.AppendLine($"Start Time: {m_startTime}");
                sb.AppendLine($"End Time: {m_endTime}");
                sb.AppendLine();

                if (m_endTime != DateTime.MinValue)
                    sb.AppendLine($"Total Time: {m_endTime - m_startTime}");
                m_textBox2.SetText(sb.ToString());
            }
        }
    }
}