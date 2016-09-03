# WinApi

A direct, highly opinionated CLR library for the native Win32 API.

`Work-In-Progress`

**Goals:**

- Provide both safe (through helpers, and safety wrappers like HandleRefs, SafeHandles), and unsafe wrappers (pure with minimal performance impact), in a clean way supplemented with inline documentation.
- Provide a single DLL that can over time, be a direct equivalent of C/C++ `windows.h` header file for the CLR. Other Windows SDK wrappers may, or may not be in fragmented into separate packages.
- Sufficient base to be able to write custom toolkits over Win32 based on Direct2D, Direct3D or even an external graphics library like Skia, without depending on WPF or WinForms.
- Always retain parity with the native API when it comes to constants (Eg: `WS_OVERLAPPEDWINDOW`, will never be changed to `OverlappedWindow` to look more like C#).
- All structs, flags, should always have the names in the idiomatic C# style. (Eg: `public enum WindowStyles { .. WS_OVERLAPPEDWINDOW = 0x00.  }`). Never WINDOWSTYLE, or MARGINS or RECT. Always `Margin`, `Rectangle`, etc. (It actually is surprisingly clean once drop the usual depencendies like WinForms, or WPF which always provide alternative forms)

**Notes**:

- Provide fully documented API (both from headers and MSDN, where-ever applicable) in the releases. Everything should be `IntelliSense capable`. No MSDN round-trips, while doing low level programming with CLR.
- Everything without full or partial documentation or undecided final method signatures, or marshalling options go into `Staging`, and eventually be moved out when complete.
- Everything that uses undocumented APIs is maintained in a separate `Experimental` namespace similarly.

**Example:**

A simple Win32 C++ Program (Mostly just C really):

```c
#include "stdafx.h"
#include <iostream>

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
     PWSTR pCmdLine, int nCmdShow);
LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam,
     LPARAM lParam);

int wmain()
{
	wWinMain(nullptr, nullptr, nullptr, 0);
}

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, 
    PWSTR pCmdLine, int nCmdShow)
{
	WNDCLASSEX wc = { 0 };
	wc.hInstance = hInstance;
	wc.lpszClassName = L"MainWindow";
	wc.cbSize = sizeof(WNDCLASSEX);
	wc.hIcon = LoadIcon(nullptr, IDI_APPLICATION);
	wc.hCursor = LoadCursor(nullptr, IDC_ARROW);
	wc.style = CS_HREDRAW | CS_VREDRAW;
	wc.lpfnWndProc = WindowProc;
	auto regRes = RegisterClassEx(&wc);
	if (!regRes)
	{
		std::cerr << "window registration failed" << std::endl;
		return regRes;
	}
	auto hwnd = CreateWindowEx(0, wc.lpszClassName, L"Hello", 
        WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
        CW_USEDEFAULT, nullptr, nullptr, hInstance, nullptr);
	if (hwnd == nullptr)
	{
		std::cerr << "window couldn't be created" << std::endl;
		return -1;
	}

	ShowWindow(hwnd, SW_SHOWNORMAL);

	MSG msg = {};
	while (GetMessage(&msg, nullptr, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}

LRESULT HandleDestroy(HWND hWnd)
{
	PostQuitMessage(0);
	return 0;
}

LRESULT HandlePaint(HWND hwnd)
{
	PAINTSTRUCT ps;
	auto hdc = BeginPaint(hwnd, &ps);
	FillRect(hdc, &ps.rcPaint, (HBRUSH)(GetStockObject(WHITE_BRUSH));
	EndPaint(hwnd, &ps);
	return 0;
}

LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)
	{
	case WM_ERASEBKGND:
		break;
	case WM_DESTROY:
		return HandleDestroy(hwnd);
	case WM_PAINT:
		return HandlePaint(hwnd);
	}
	return DefWindowProc(hwnd, uMsg, wParam, lParam);
}
```

And now the direct C# equivalent using WinApi: 

```c#

using System;
using System.Runtime.InteropServices;
using WinApi;

namespace MySuperLowLevelProgram {

 internal class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            var instanceHandle = NativeMethods.GetModuleHandle(IntPtr.Zero);

            var wc = new WindowClassEx()
            {
                Size = (uint)Marshal.SizeOf<WindowClassEx>(),
                ClassName = "MainWindow",
                CursorHandle  = Helpers.LoadCursor(IntPtr.Zero, SystemCursor.IDC_ARROW),
                IconHandle = Helpers.LoadIcon(IntPtr.Zero, SystemIcon.IDI_APPLICATION),
                Style = WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW,
                BackgroundBrushHandle = new IntPtr((int)StockObject.WHITE_BRUSH),
                WindowProc = WindowProc,
                InstanceHandle = instanceHandle,
            };

            var resReg = NativeMethods.RegisterClassEx(ref wc);
            if (resReg == 0)
            {
                Console.Error.WriteLine("registration failed");
                return -1;
            }

            var hwnd = NativeMethods.CreateWindowEx(WindowExStyles.WS_EX_APPWINDOW, 
				wc.ClassName, "Hello", WindowStyles.WS_OVERLAPPEDWINDOW,
                (int) CreateWindowFlags.CW_USEDEFAULT, (int) CreateWindowFlags.CW_USEDEFAULT,
                (int) CreateWindowFlags.CW_USEDEFAULT, (int) CreateWindowFlags.CW_USEDEFAULT,
                IntPtr.Zero, IntPtr.Zero, instanceHandle, IntPtr.Zero);

            if (hwnd == IntPtr.Zero)
            {
                Console.Error.WriteLine("window creation failed");
                return -1;
            }

            NativeMethods.ShowWindow(hwnd, ShowWindowCommands.SW_SHOWNORMAL);
            NativeMethods.UpdateWindow(hwnd);

            Message msg;
            int res;
            while ((res = NativeMethods.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
            {
                NativeMethods.TranslateMessage(ref msg);
                NativeMethods.DispatchMessage(ref msg);
            }

            return res;
        }

        private static IntPtr WindowProc(IntPtr hwnd, uint umsg, 
			IntPtr wParam, IntPtr lParam)
        {
            var msg = (WM) umsg;
            switch (msg)
            {
                case WM.ERASEBKGND:
                    return IntPtr.Zero;
                case WM.CLOSE:
                {
                    NativeMethods.PostQuitMessage(0);
                    break;
                }
                case WM.PAINT:
                {
                    PaintStruct ps;
                    var hdc = NativeMethods.BeginPaint(hwnd, out ps);
                    NativeMethods.FillRect(hdc, ref ps.PaintRectangle, 
						Helpers.GetStockObject(StockObject.WHITE_BRUSH));
                    NativeMethods.EndPaint(hwnd, ref ps);
                    break;
                }
            }
            return NativeMethods.DefWindowProc(hwnd, umsg, wParam, lParam);
        }
    }
```