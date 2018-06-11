using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace MetroFramework.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class WinApi
    {
        public struct POINT
        {
            public int x;

            public int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public struct SIZE
        {
            public int cx;

            public int cy;

            public SIZE(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ARGB
        {
            public byte Blue;

            public byte Green;

            public byte Red;

            public byte Alpha;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;

            public byte BlendFlags;

            public byte SourceConstantAlpha;

            public byte AlphaFormat;
        }

        public struct TCHITTESTINFO
        {
            public Point pt;

            public uint flags;
        }

        public struct RECT
        {
            public int Left;

            public int Top;

            public int Right;

            public int Bottom;

            public RECT(Rectangle rc)
            {
                this.Left = rc.Left;
                this.Top = rc.Top;
                this.Right = rc.Right;
                this.Bottom = rc.Bottom;
            }

            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(this.Left, this.Top, this.Right, this.Bottom);
            }
        }

        public struct NCCALCSIZE_PARAMS
        {
            public WinApi.RECT rect0;

            public WinApi.RECT rect1;

            public WinApi.RECT rect2;

            public IntPtr lppos;
        }

        public struct MINMAXINFO
        {
            public WinApi.POINT ptReserved;

            public WinApi.POINT ptMaxSize;

            public WinApi.POINT ptMaxPosition;

            public WinApi.POINT ptMinTrackSize;

            public WinApi.POINT ptMaxTrackSize;
        }

        public struct APPBARDATA
        {
            public uint cbSize;

            public IntPtr hWnd;

            public uint uCallbackMessage;

            public WinApi.ABE uEdge;

            public WinApi.RECT rc;

            public int lParam;
        }

        public struct WindowPos
        {
            public int hwnd;

            public int hWndInsertAfter;

            public int x;

            public int y;

            public int cx;

            public int cy;

            public int flags;
        }

        public enum ABM : uint
        {
            New,
            Remove,
            QueryPos,
            SetPos,
            GetState,
            GetTaskbarPos,
            Activate,
            GetAutoHideBar,
            SetAutoHideBar,
            WindowPosChanged,
            SetState
        }

        public enum ABE : uint
        {
            Left,
            Top,
            Right,
            Bottom
        }

        public enum ScrollBar
        {
            SB_HORZ,
            SB_VERT,
            SB_CTL,
            SB_BOTH
        }

        public enum HitTest
        {
            HTNOWHERE,
            HTCLIENT,
            HTCAPTION,
            HTGROWBOX = 4,
            HTSIZE = 4,
            HTMINBUTTON = 8,
            HTMAXBUTTON,
            HTLEFT,
            HTRIGHT,
            HTTOP,
            HTTOPLEFT,
            HTTOPRIGHT,
            HTBOTTOM,
            HTBOTTOMLEFT,
            HTBOTTOMRIGHT,
            HTREDUCE = 8,
            HTZOOM,
            HTSIZEFIRST,
            HTSIZELAST = 17,
            HTTRANSPARENT = -1
        }

        public enum TabControlHitTest
        {
            TCHT_NOWHERE = 1
        }

        public enum Messages : uint
        {
            WM_NULL,
            WM_CREATE,
            WM_DESTROY,
            WM_MOVE,
            WM_SIZE = 5u,
            WM_ACTIVATE,
            WM_SETFOCUS,
            WM_KILLFOCUS,
            WM_ENABLE = 10u,
            WM_SETREDRAW,
            WM_SETTEXT,
            WM_GETTEXT,
            WM_GETTEXTLENGTH,
            WM_PAINT,
            WM_CLOSE,
            WM_QUERYENDSESSION,
            WM_QUERYOPEN = 19u,
            WM_ENDSESSION = 22u,
            WM_QUIT = 18u,
            WM_ERASEBKGND = 20u,
            WM_SYSCOLORCHANGE,
            WM_SHOWWINDOW = 24u,
            WM_WININICHANGE = 26u,
            WM_SETTINGCHANGE = 26u,
            WM_DEVMODECHANGE,
            WM_ACTIVATEAPP,
            WM_FONTCHANGE,
            WM_TIMECHANGE,
            WM_CANCELMODE,
            WM_SETCURSOR,
            WM_MOUSEACTIVATE,
            WM_CHILDACTIVATE,
            WM_QUEUESYNC,
            WM_GETMINMAXINFO,
            WM_PAINTICON = 38u,
            WM_ICONERASEBKGND,
            WM_NEXTDLGCTL,
            WM_SPOOLERSTATUS = 42u,
            WM_DRAWITEM,
            WM_MEASUREITEM,
            WM_DELETEITEM,
            WM_VKEYTOITEM,
            WM_CHARTOITEM,
            WM_SETFONT,
            WM_GETFONT,
            WM_SETHOTKEY,
            WM_GETHOTKEY,
            WM_QUERYDRAGICON = 55u,
            WM_COMPAREITEM = 57u,
            WM_GETOBJECT = 61u,
            WM_COMPACTING = 65u,
            WM_COMMNOTIFY = 68u,
            WM_WINDOWPOSCHANGING = 70u,
            WM_WINDOWPOSCHANGED,
            WM_POWER,
            WM_COPYDATA = 74u,
            WM_CANCELJOURNAL,
            WM_NOTIFY = 78u,
            WM_INPUTLANGCHANGEREQUEST = 80u,
            WM_INPUTLANGCHANGE,
            WM_TCARD,
            WM_HELP,
            WM_USERCHANGED,
            WM_NOTIFYFORMAT,
            WM_CONTEXTMENU = 123u,
            WM_STYLECHANGING,
            WM_STYLECHANGED,
            WM_DISPLAYCHANGE,
            WM_GETICON,
            WM_SETICON,
            WM_NCCREATE,
            WM_NCDESTROY,
            WM_NCCALCSIZE,
            WM_NCHITTEST,
            WM_NCPAINT,
            WM_NCACTIVATE,
            WM_GETDLGCODE,
            WM_SYNCPAINT,
            WM_NCMOUSEMOVE = 160u,
            WM_NCLBUTTONDOWN,
            WM_NCLBUTTONUP,
            WM_NCLBUTTONDBLCLK,
            WM_NCRBUTTONDOWN,
            WM_NCRBUTTONUP,
            WM_NCRBUTTONDBLCLK,
            WM_NCMBUTTONDOWN,
            WM_NCMBUTTONUP,
            WM_NCMBUTTONDBLCLK,
            WM_NCXBUTTONDOWN = 171u,
            WM_NCXBUTTONUP,
            WM_NCXBUTTONDBLCLK,
            WM_INPUT = 255u,
            WM_KEYFIRST,
            WM_KEYDOWN = 256u,
            WM_KEYUP,
            WM_CHAR,
            WM_DEADCHAR,
            WM_SYSKEYDOWN,
            WM_SYSKEYUP,
            WM_SYSCHAR,
            WM_SYSDEADCHAR,
            WM_UNICHAR = 265u,
            WM_KEYLAST = 264u,
            WM_IME_STARTCOMPOSITION = 269u,
            WM_IME_ENDCOMPOSITION,
            WM_IME_COMPOSITION,
            WM_IME_KEYLAST = 271u,
            WM_INITDIALOG,
            WM_COMMAND,
            WM_SYSCOMMAND,
            WM_TIMER,
            WM_HSCROLL,
            WM_VSCROLL,
            WM_INITMENU,
            WM_INITMENUPOPUP,
            WM_MENUSELECT = 287u,
            WM_MENUCHAR,
            WM_ENTERIDLE,
            WM_MENURBUTTONUP,
            WM_MENUDRAG,
            WM_MENUGETOBJECT,
            WM_UNINITMENUPOPUP,
            WM_MENUCOMMAND,
            WM_CHANGEUISTATE,
            WM_UPDATEUISTATE,
            WM_QUERYUISTATE,
            WM_CTLCOLOR = 25u,
            WM_CTLCOLORMSGBOX = 306u,
            WM_CTLCOLOREDIT,
            WM_CTLCOLORLISTBOX,
            WM_CTLCOLORBTN,
            WM_CTLCOLORDLG,
            WM_CTLCOLORSCROLLBAR,
            WM_CTLCOLORSTATIC,
            WM_MOUSEFIRST = 512u,
            WM_MOUSEMOVE = 512u,
            WM_LBUTTONDOWN,
            WM_LBUTTONUP,
            WM_LBUTTONDBLCLK,
            WM_RBUTTONDOWN,
            WM_RBUTTONUP,
            WM_RBUTTONDBLCLK,
            WM_MBUTTONDOWN,
            WM_MBUTTONUP,
            WM_MBUTTONDBLCLK,
            WM_MOUSEWHEEL,
            WM_XBUTTONDOWN,
            WM_XBUTTONUP,
            WM_XBUTTONDBLCLK,
            WM_MOUSELAST = 525u,
            WM_PARENTNOTIFY = 528u,
            WM_ENTERMENULOOP,
            WM_EXITMENULOOP,
            WM_NEXTMENU,
            WM_SIZING,
            WM_CAPTURECHANGED,
            WM_MOVING,
            WM_POWERBROADCAST = 536u,
            WM_DEVICECHANGE,
            WM_MDICREATE = 544u,
            WM_MDIDESTROY,
            WM_MDIACTIVATE,
            WM_MDIRESTORE,
            WM_MDINEXT,
            WM_MDIMAXIMIZE,
            WM_MDITILE,
            WM_MDICASCADE,
            WM_MDIICONARRANGE,
            WM_MDIGETACTIVE,
            WM_MDISETMENU = 560u,
            WM_ENTERSIZEMOVE,
            WM_EXITSIZEMOVE,
            WM_DROPFILES,
            WM_MDIREFRESHMENU,
            WM_IME_SETCONTEXT = 641u,
            WM_IME_NOTIFY,
            WM_IME_CONTROL,
            WM_IME_COMPOSITIONFULL,
            WM_IME_SELECT,
            WM_IME_CHAR,
            WM_IME_REQUEST = 648u,
            WM_IME_KEYDOWN = 656u,
            WM_IME_KEYUP,
            WM_MOUSEHOVER = 673u,
            WM_MOUSELEAVE = 675u,
            WM_NCMOUSELEAVE = 674u,
            WM_WTSSESSION_CHANGE = 689u,
            WM_TABLET_FIRST = 704u,
            WM_TABLET_LAST = 735u,
            WM_CUT = 768u,
            WM_COPY,
            WM_PASTE,
            WM_CLEAR,
            WM_UNDO,
            WM_RENDERFORMAT,
            WM_RENDERALLFORMATS,
            WM_DESTROYCLIPBOARD,
            WM_DRAWCLIPBOARD,
            WM_PAINTCLIPBOARD,
            WM_VSCROLLCLIPBOARD,
            WM_SIZECLIPBOARD,
            WM_ASKCBFORMATNAME,
            WM_CHANGECBCHAIN,
            WM_HSCROLLCLIPBOARD,
            WM_QUERYNEWPALETTE,
            WM_PALETTEISCHANGING,
            WM_PALETTECHANGED,
            WM_HOTKEY,
            WM_PRINT = 791u,
            WM_PRINTCLIENT,
            WM_APPCOMMAND,
            WM_THEMECHANGED,
            WM_HANDHELDFIRST = 856u,
            WM_HANDHELDLAST = 863u,
            WM_AFXFIRST,
            WM_AFXLAST = 895u,
            WM_PENWINFIRST,
            WM_PENWINLAST = 911u,
            WM_USER = 1024u,
            WM_REFLECT = 8192u,
            WM_APP = 32768u,
            WM_DWMCOMPOSITIONCHANGED = 798u,
            SC_MOVE = 61456u,
            SC_MINIMIZE = 61472u,
            SC_MAXIMIZE = 61488u,
            SC_RESTORE = 61728u
        }

        public enum Bool
        {
            False,
            True
        }

        public const int Autohide = 1;

        public const int AlwaysOnTop = 2;

        public const int MfByposition = 1024;

        public const int MfRemove = 4096;

        public const int TCM_HITTEST = 4883;

        public const int ULW_COLORKEY = 1;

        public const int ULW_ALPHA = 2;

        public const int ULW_OPAQUE = 4;

        public const byte AC_SRC_OVER = 0;

        public const byte AC_SRC_ALPHA = 1;

        public const int GW_HWNDFIRST = 0;

        public const int GW_HWNDLAST = 1;

        public const int GW_HWNDNEXT = 2;

        public const int GW_HWNDPREV = 3;

        public const int GW_OWNER = 4;

        public const int GW_CHILD = 5;

        public const int HC_ACTION = 0;

        public const int WH_CALLWNDPROC = 4;

        public const int GWL_WNDPROC = -4;

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern WinApi.Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref WinApi.POINT pptDst, ref WinApi.SIZE psize, IntPtr hdcSrc, ref WinApi.POINT pprSrc, int crKey, ref WinApi.BLENDFUNCTION pblend, int dwFlags);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern WinApi.Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern WinApi.Bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int W, int H, uint uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr wnd, int msg, bool param, int lparam);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr SHAppBarMessage(WinApi.ABM dwMessage, [In] ref WinApi.APPBARDATA pData);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hrgnclip, uint fdwOptions);

        [DllImport("user32.dll")]
        public static extern bool ShowScrollBar(IntPtr hWnd, int bar, int cmd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr handle);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDC);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hwnd, char[] className, int maxCount);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hwnd, int uCmd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, ref WinApi.RECT lpRect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, [In] [Out] ref Rectangle rect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool InvalidateRect(IntPtr hwnd, ref Rectangle rect, bool bErase);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool ValidateRect(IntPtr hwnd, ref Rectangle rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetWindowRect(IntPtr hWnd, [In] [Out] ref Rectangle rect);

        public static int LoWord(int dwValue)
        {
            return dwValue & 65535;
        }

        public static int HiWord(int dwValue)
        {
            return dwValue >> 16 & 65535;
        }
    }
}
