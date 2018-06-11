using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

using System.Runtime.InteropServices;
using System.Text;


namespace dotNetLab
{
    namespace Tools
    {
        class FontTool
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            static extern int WriteProfileString(string lpszSection, string lpszKeyName, string lpszString);
            [DllImport("user32.dll")]
            public static extern int SendMessage(int hWnd, // handle to destination window 
            uint Msg, // message 
            int wParam, // first message parameter 
            int lParam // second message parameter 
            );
            [DllImport("gdi32")]
            public static extern int AddFontResource(string lpFileName);
            public void InstallFont(string strSourceFontPath)
            {
                string WinFontDir = System.Environment.GetEnvironmentVariable("windir") + "\\fonts";
                string FontFileName = Path.GetFileName(strSourceFontPath);
                string FontName = Path.GetFileNameWithoutExtension(strSourceFontPath);
                int Ret;
                int Res;
                string FontPath;
                const int WM_FONTCHANGE = 0x001D;
                const int HWND_BROADCAST = 0xffff;
                FontPath = WinFontDir + "\\" + FontFileName;
                if (!File.Exists(FontPath))
                {
                    File.Copy(strSourceFontPath, FontPath);
                    Ret = AddFontResource(FontPath);
                    Res = SendMessage(HWND_BROADCAST, WM_FONTCHANGE, 0, 0);
                    Ret = WriteProfileString("fonts", FontName + "(TrueType)", FontFileName);
                }

            }

            public static FontFamily DynamicFont(String fontName, byte[] byts)
            {
                //从外部文件加载字体文件  
                PrivateFontCollection font = new PrivateFontCollection();
                IntPtr ptr = Marshal.AllocHGlobal(byts.Length);
                for (int i = 0; i < byts.Length; i++)
                {
                    Marshal.WriteByte(ptr, i, byts[i]);
                }
                font.AddMemoryFont(ptr, byts.Length);

                //定义成新的字体对象  
                FontFamily myFontFamily = new FontFamily(fontName, font);
                return myFontFamily;
                //Font myFont = new Font(myFontFamily, 56F, FontStyle.Regular);

                ////将字体显示到控件  
                //label1.Font = myFont;
            }
        }
    }

}
