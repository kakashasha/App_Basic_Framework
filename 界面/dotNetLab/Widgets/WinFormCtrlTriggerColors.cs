using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;

namespace dotNetLab.Widgets
{
  public class dotNetLabUISurport
    {

        public static Color PressEffect(Color _c, int nValue, bool bIsAdd)
        {
            int _Red = _c.R;
            int _Green = _c.G;
            int _Blue = _c.B;
            if (bIsAdd)
            {
                if (_c.R + nValue >= 0 && _c.R + nValue < 255)
                    _Red = _c.R + nValue;

                if (_c.G + nValue >= 0 && _c.G + nValue < 255)

                    _Green = _c.G + nValue;

                if (_c.B + nValue >= 0 && _c.B + nValue < 255)
                    _Blue = _c.B + nValue;
            }
            else
            {
                if (_c.R - nValue >= 0)
                    _Red = _c.R - nValue;

                if (_c.G - nValue >= 0)

                    _Green = _c.G - nValue;

                if (_c.B - nValue >= 0)
                    _Blue = _c.B - nValue;
            }

            return Color.FromArgb(_Red, _Green, _c.B);
        }
        public static FontFamily DynamicFont( byte [] byts)
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
            FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
            return myFontFamily;
            //Font myFont = new Font(myFontFamily, 56F, FontStyle.Regular);

            ////将字体显示到控件  
            //label1.Font = myFont;
        }
    }
}
