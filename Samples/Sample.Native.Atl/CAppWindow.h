#pragma once

#include "pch.h"

class CAppWindow : public CWindowImpl<CAppWindow, CWindow, CFrameWinTraits>
{
private:
	BEGIN_MSG_MAP(CAppWindow)
		MSG_WM_CREATE(OnCreate)
		MSG_WM_DESTROY(OnDestroy)
		MSG_WM_PAINT(OnPaint)
		MSG_WM_ERASEBKGND(OnEraseBkgnd)
		END_MSG_MAP()

public:
	DECLARE_WND_CLASS(L"AppWindow")
	int Run();

protected:
	LRESULT OnCreate(LPCREATESTRUCT lpcreatestruct);
	void OnDestroy();
	LRESULT OnEraseBkgnd(HDC hdc);
	void OnPaint(HDC hdc);
};
