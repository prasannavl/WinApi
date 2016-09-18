#include "pch.h"
#include "CAppWindow.h"

int CAppWindow::Run()
{
	auto hwnd = Create(0, 0, L"Hello");
	ShowWindow(SW_SHOWNORMAL);
	MSG msg;
	BOOL result;
	while (result = GetMessage(&msg, 0, 0, 0))
	{
		if (result == -1)
			return GetLastError();
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}

LRESULT CAppWindow::OnCreate(LPCREATESTRUCT lpcreatestruct)
{
	return 0;
}

void CAppWindow::OnDestroy()
{
	PostQuitMessage(0);
}

LRESULT CAppWindow::OnEraseBkgnd(HDC hdc)
{
	SetMsgHandled(true);
	return 1;
}

void CAppWindow::OnPaint(HDC hdc)
{
	PAINTSTRUCT ps;
	BeginPaint(&ps);
	FillRect(hdc, &ps.rcPaint, (HBRUSH)COLOR_WINDOW);
	EndPaint(&ps);
	SetMsgHandled(true);
}
