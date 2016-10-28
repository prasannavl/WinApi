using System;
using System.Runtime.ExceptionServices;
using WinApi.Core;
using WinApi.Desktop;
using WinApi.User32;
using WinApi.Windows;
using WinApi.Windows.Controls;
using WinApi.Windows.Controls.Layouts;

namespace Sample.SimulateInput
{
    class Program
    {
        [HandleProcessCorruptedStateExceptions]
        static int Main(string[] args)
        {
            ApplicationHelpers.SetupDefaultExceptionHandlers();

            try
            {
                var factory = WindowFactory.Create();
                using (var win = Window.Create<SampleWindow>(factory: factory, text: "Hello"))
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

        public sealed class SampleWindow : Window
        {
            private readonly Input[] m_inputs = new Input[11];
            private readonly HorizontalStretchLayout m_layout = new HorizontalStretchLayout();
            private EditBox m_editBox;
            private StaticBox m_textBox;
            private IntPtr m_timerId;
            private TimerProc m_timerProc;
            private int m_timesExecuted;

            protected override void OnCreate(ref CreateWindowPacket packet)
            {
                m_textBox = StaticBox.Create(
                    "Ahoy!",
                    hParent: Handle);

                m_editBox = EditBox.Create(
                    "Nothing here yet.",
                    hParent: Handle,
                    controlStyles:
                    EditBox.EditStyles.ES_MULTILINE | EditBox.EditStyles.ES_WANTRETURN |
                    (EditBox.EditStyles) WindowStyles.WS_VSCROLL);

                m_layout.ClientArea = GetClientRect();
                m_layout.Margin = new Rectangle(10, 10, 10, 10);
                m_layout.Children.Add(m_textBox);
                m_layout.Children.Add(m_editBox);
                m_layout.PerformLayout();
                m_timerProc = (wnd, uMsg, eventId, millis) =>
                {
                    try
                    {
                        m_timesExecuted++;
                        Input.InitKeyboardInput(out m_inputs[0], VirtualKey.H, false);
                        Input.InitKeyboardInput(out m_inputs[1], VirtualKey.H, true);
                        Input.InitKeyboardInput(out m_inputs[3], VirtualKey.E, false);
                        Input.InitKeyboardInput(out m_inputs[4], VirtualKey.E, true);
                        Input.InitKeyboardInput(out m_inputs[5], VirtualKey.L, false);
                        Input.InitKeyboardInput(out m_inputs[6], VirtualKey.L, true);
                        Input.InitKeyboardInput(out m_inputs[7], VirtualKey.L, false);
                        Input.InitKeyboardInput(out m_inputs[8], VirtualKey.L, true);
                        Input.InitKeyboardInput(out m_inputs[9], VirtualKey.O, false);
                        Input.InitKeyboardInput(out m_inputs[10], VirtualKey.O, true);
                        var x = User32Helpers.SendInput(m_inputs);
                    }
                    catch (Exception ex)
                    {
                        m_editBox.SetText($"ERROR: {ex.Message}\r\n{ex.StackTrace}");
                    }
                };

                m_timerId = User32Methods.SetTimer(Handle, IntPtr.Zero, 20, m_timerProc);
                base.OnCreate(ref packet);
            }

            protected override void OnKey(ref KeyPacket packet)
            {
                var key = packet.Key;
                var inputState = packet.InputState;
                        var str = $"\r\n{DateTime.Now} :" +
                  $" {key} => {inputState.IsKeyUpTransition}; " +
                  $"{inputState.RepeatCount}; " +
                  $"{inputState.ScanCode}; " +
                  $"{inputState.IsContextual}; " +
                  $"{inputState.IsExtendedKey}" + "\r\n" +
                  $"No. of text display changes: {m_timesExecuted}" + "\0";
                        m_textBox.SetText(str);
                        base.OnKey(ref packet);
            }

            protected override void OnMouseButton(ref MouseButtonPacket packet)
            {
                var button = packet.Button;
                var isButtonDown = packet.IsButtonDown;
                if ((button == MouseButton.Left) && isButtonDown)
                    SetFocus();
                base.OnMouseButton(ref packet);
            }

            protected override void OnSize(ref SizePacket packet)
            {
                var size = packet.Size;
                m_layout.SetSize(ref size);
                base.OnSize(ref packet);
            }
        }
    }
}