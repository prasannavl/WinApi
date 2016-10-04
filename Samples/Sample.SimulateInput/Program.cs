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
                // Use window color brush to emulate Win Forms like behaviour
                var factory = WindowFactory.Create(hBgBrush: new IntPtr((int) SystemColor.COLOR_WINDOW));
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
            private readonly HorizontalStretchLayout m_layout = new HorizontalStretchLayout();
            private EditBox m_editBox;
            private StaticBox m_textBox;
            private IntPtr m_timerId;
            private TimerProc m_timerProc;
            private int m_timesExecuted;

            protected override CreateWindowResult OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
            {
                m_textBox = StaticBox.Create(
                    "Ahoy!",
                    hParent: Handle);

                m_editBox = EditBox.Create(
                    "Nothing here yet.",
                    hParent: Handle, 
                    controlStyles: EditBox.EditStyles.ES_MULTILINE | EditBox.EditStyles.ES_WANTRETURN | (EditBox.EditStyles)WindowStyles.WS_VSCROLL);

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
                        var inputs = new Input[8];
                        Input.InitKeyboardInput(out inputs[0], VirtualKey.H, true);
                        Input.InitKeyboardInput(out inputs[1], VirtualKey.H, false);
                        Input.InitKeyboardInput(out inputs[2], VirtualKey.E, true);
                        Input.InitKeyboardInput(out inputs[3], VirtualKey.E, false);
                        Input.InitKeyboardInput(out inputs[4], VirtualKey.L, true);
                        Input.InitKeyboardInput(out inputs[5], VirtualKey.L, false);
                        Input.InitKeyboardInput(out inputs[6], VirtualKey.O, true);
                        Input.InitKeyboardInput(out inputs[7], VirtualKey.O, false);
                        var x = User32Helpers.SendInput(inputs);
                    }
                    catch (Exception ex)
                    {
                        m_editBox.SetText($"ERROR: {ex.Message}\r\n{ex.StackTrace}");
                    }
                };

                m_timerId = User32Methods.SetTimer(Handle, IntPtr.Zero, 20, m_timerProc);
                return base.OnCreate(ref msg, ref createStruct);
            }

            protected override void OnKey(ref WindowMessage msg, VirtualKey key, bool isKeyUp, KeyboardInputState inputState, bool isSystemContext)
            {
                var str = $"\r\n{DateTime.Now} :" +
                              $" {key} => {inputState.IsKeyUpTransition}; " +
                              $"{inputState.RepeatCount}; " +
                              $"{inputState.ScanCode}; " +
                              $"{inputState.IsContextual}; " +
                              $"{inputState.IsExtendedKey}" + "\r\n" +
                              $"No. of text display changes: {m_timesExecuted}" + "\0";
                m_textBox.SetText(str);
                base.OnKey(ref msg, key, isKeyUp, inputState, isSystemContext);
            }

            protected override void OnSize(ref WindowMessage msg, WindowSizeFlag flag, ref Size size)
            {
                m_layout.SetSize(ref size);
                this.SetFocus();
                base.OnSize(ref msg, flag, ref size);
            }
        }
    }
}