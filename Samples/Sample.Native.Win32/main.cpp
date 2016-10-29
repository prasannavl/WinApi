#define WIN32_LEAN_AND_MEAN

#include <windows.h>
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
	WNDCLASSEX wc = {0};
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
	FillRect(hdc, &ps.rcPaint, (HBRUSH)(COLOR_WINDOW));
	EndPaint(hwnd, &ps);
	return 0;
}

LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)
	{
	case WM_ERASEBKGND:
		return 1;
	case WM_DESTROY:
		return HandleDestroy(hwnd);
	case WM_PAINT:
		return HandlePaint(hwnd);
	}
	return DefWindowProc(hwnd, uMsg, wParam, lParam);
}
