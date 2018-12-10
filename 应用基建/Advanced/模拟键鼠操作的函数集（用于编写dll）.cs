/*
 extern "C" 
{
	void __declspec(dllexport)  CloseWnd(HWND hwnd)
	{
		BringToFront(hwnd);
		SendMessage(hwnd, WM_CLOSE, 0, 0);
	}
	void __declspec(dllexport) BringToFront(HWND hwnd)
	{
		ShowWindow(hwnd, SW_NORMAL);
		SetForegroundWindow(hwnd);
	}
	void __declspec(dllexport) SendKeys(const char *str) {
		DWORD sc, shift;
		unsigned char vkey;
		int i;
		for (i = 0; str[i] != '\0'; i++) {
			sc = OemKeyScan(str[i]);
			shift = sc >> 16;
			vkey = MapVirtualKey(sc & 0xffff, 1);
			if (shift)
				keybd_event(VK_SHIFT, 0, 0, 0);

			keybd_event(vkey, 0, 0, 0);
			keybd_event(vkey, 0, KEYEVENTF_KEYUP, 0);

			if (shift)
				keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
		}
	}
	void __declspec(dllexport) FindChildWnd(HWND ParentHwnd,const char *wndName,HWND & childWnd)
	{
	     childWnd =	FindWindowEx(ParentHwnd, 0, NULL, wndName);
	}
	LPRECT __declspec(dllexport) GetWndRect(HWND hwnd)
	{
		LPRECT rct = new  RECT();
		GetWindowRect(hwnd, rct);
		return rct;
	}
	void __declspec(dllexport) GetWndLocation(HWND hwnd,int & X,int & Y)
	{
		LPRECT rct = GetWndRect(hwnd);
		 X = rct->left;
		 Y = rct->top;
		 delete rct;
	}
	void __declspec(dllexport) MouseLeftClick(int x,int y)
	{
		SetCursorPos(x, y);
		mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
	}
	void __declspec(dllexport) OffSetLeftUpCorner(HWND hwnd,int offsetX,int offsetY,int & X,int & Y)
	{
		GetWndLocation(hwnd, X, Y);
		X += offsetX;
		Y += offsetY;
	}
	void __declspec(dllexport) OffSetLeftDownCorner(HWND hwnd, int offsetX, int offsetY, int & X, int & Y)
	{
		LPRECT rct = GetWndRect(hwnd);
		 X = rct->left;
		 Y = rct->bottom;
		 delete rct;
		 X += offsetX;
		 Y += offsetY;
	}
	void __declspec(dllexport) OffSetRightUpCorner(HWND hwnd, int offsetX, int offsetY, int & X, int & Y)
	{
		LPRECT rct = GetWndRect(hwnd);
		X = rct->right;
		Y = rct->top;
		delete rct;
		X += offsetX;
		Y += offsetY;
	}
	void __declspec(dllexport) OffSetRightDownCorner(HWND hwnd, int offsetX, int offsetY, int & X, int & Y)
	{
		LPRECT rct = GetWndRect(hwnd);
		X = rct->right;
		Y = rct->bottom;
		delete rct;
		X += offsetX;
		Y += offsetY;
	}


}
 
//shikii 在C#中发送中文
  public void SendCHS(string strText)
    {
        Clipboard.SetText(strText);    // 将字符串复制到剪贴板，相当于^c
        SendKeys.SendWait("^v");   // CTRL+V，粘贴
    }

    //shikii 获取当前鼠标坐标（全局）
    int x = Control.MousePosition.X;
     int y = Control.MousePosition.Y;
 */
