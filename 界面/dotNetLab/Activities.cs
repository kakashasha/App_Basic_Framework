
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using dotNetLab.Framework;
using dotNetLab.Widgets;
using dotNetLab.Appearance;
using Microsoft;
using System.Collections;
using System.Drawing.Design;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using dotNetLab.Forms;

namespace dotNetLab
{
   
    namespace Activities
    {
       
        public class AeroWindow:Wnd
        {
            int en;

            [DllImport("dwmapi.dll")]
            private static extern void DwmIsCompositionEnabled(ref int enabledptr);
            [DllImport("dwmapi.dll")]
            private static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margin);
            public struct MARGINS
            {
                public int m_Left;
                public int m_Right;
                public int m_Top;
                public int m_Buttom;
            };
            public AeroWindow():base(false)
            {
                

            }
            protected override void prepareAppearance()
            {
                base.prepareAppearance();
                en = 0;
                MARGINS mg = new MARGINS(); //定义透明扩展区域的大小，这里全部-1，即全部透明
                mg.m_Buttom = -1;
                mg.m_Left = -1;
                mg.m_Right = -1;
                mg.m_Top = -1;

                //判断是否Vista及以上的系统
                if (System.Environment.OSVersion.Version.Major >= 6)
                {
                    DwmIsCompositionEnabled(ref en);    //检测Aero是否为打开
                    if (en > 0)
                    {
                        DwmExtendFrameIntoClientArea(this.Handle, ref mg);   //透明
                    }

                }

               
            }
            protected override void OnDraw()
            {
                base.OnDraw();
                if (en > 0)
                {
                    SolidBrush bsh = new SolidBrush(Color.Black);
                    g.FillRectangle(bsh, this.ClientRectangle);
                    bsh.Dispose();
                }
            }
        }
        //public  class ModernWnd:Wnd
        //{
        //    public delegate void TopMostTipperCallback();
        //    public event TopMostTipperCallback TopMostTipper;
        //    Image img_Up, img_Down;
        //    private ImageView imv_LastRecord;
        //    Rectangle rct_Area_Toast;
        //    private Toast tipper;
        //    protected Pen pen_Border;
        //    protected SolidBrush sldbrh_VerticalBar ;
        //    protected string strCaption;
        //    protected SolidBrush sbrh_Title;
        //    protected Font TitleFont;
        //    const int Guying_HTLEFT = 10;
        //    const int Guying_HTRIGHT = 11;
        //    const int Guying_HTTOP = 12;
        //    const int Guying_HTTOPLEFT = 13;
        //    const int Guying_HTTOPRIGHT = 14;
        //    const int Guying_HTBOTTOM = 15;
        //    const int Guying_HTBOTTOMLEFT = 0x10;
        //    const int Guying_HTBOTTOMRIGHT = 17;
        //    bool bEnableDialog = false;
        //    public ModernWnd():base(true)
        //    {
               
        //    }
        //    protected override void prepareEvents()
        //    {
        //        base.prepareEvents();
        //        this.Resize += ModernWnd_Resize;
        //        imv_LastRecord.Click += Imv_LastRecord_Click;
        //        this.ControlAdded += ModernWnd_ControlAdded;
        //    }

        //    private void ModernWnd_ControlAdded(object sender, ControlEventArgs e)
        //    {
                 
        //    }

        //    private void Imv_LastRecord_Click(object sender, EventArgs e)
        //    {
        //        ReTip();
        //        TopMostTipper();
        //    }

        //    protected void DrawDecoratedImage(Image img,Point pnt)
        //    {
                
        //        g.DrawImage(img, pnt);
        //    }
        //    private void ModernWnd_Resize(object sender, EventArgs e)
        //    {
        //        GetTipperLocation();
        //        this.Refresh();
        //    }
             
        //    void GetTipperLocation()
        //    {
        //        tipper.Location = new Point(this.Width - tipper.Width - 3,
        //            this.Height - tipper.Height - 35);
        //        this.rct_Area_Toast.X = 0;
        //        this.rct_Area_Toast.Y = tipper.Location.Y-tipper.Width/2;
        //        rct_Area_Toast.Width = this.Width;
        //        this.rct_Area_Toast.Height = tipper.Height * 2;
        //    }
        //    protected override void prepareCtrls()
        //    {
        //        base.prepareCtrls();
                
        //        tipper = new Toast();
        //        tipper.Text = "Toas Show";
        //        tipper.Visible = false;
        //        tipper.Width = 300;
        //            imv_LastRecord = new ImageView();
        //            imv_LastRecord.BorderColor = Color.Transparent;
        //            imv_LastRecord.Size = new Size(32, 32);
        //            imv_LastRecord.ImageSize = new SizeF(30, 30);
        //            imv_LastRecord.Source = global::dotNetLab.UI.Mind;
                   
                
        //        GetTipperLocation();
        //    }
        //    protected void AddTipper()
        //    {
        //        AddControl(this.tipper);
            
        //    }
           
        //    public void ReTip()
        //    {
              
        //        tipper.ReShow();
        //    }
        //    protected override void UnitCtrls()
        //    {
        //        base.UnitCtrls();
               
                
        //    }
        //    public void Tip(string strText)
        //    {
        //        tipper.Text = strText;
        //        CommonTip();
        //    }
        //    public void Tip(string strText, Image img)
        //    {
        //        tipper.Text = strText;
        //        tipper.Source = img;
        //        CommonTip();
        //    }
            
        //    public void Tip(string strText, int nInterval, Image img)
        //    {
        //        this.Text = strText;
        //        tipper.Source = img;
        //        CommonTip();
        //    }
        //    void CommonTip()
        //    {
        //        if (tipper.tmr.Enabled == true)
        //            tipper.Shut();
        //        GetTipperLocation();
        //        tipper.Visible = true;
        //        tipper.tmr.Start();
        //        tipper.BringToFront();
        //    }
        //    protected override void prepareAppearance()
        //    {
        //        base.prepareAppearance();
        //        this.BackColor = Color.White;
        //        pen_Border = new Pen(Color.DodgerBlue, 2);
        //        sldbrh_VerticalBar = new SolidBrush(Color.Red);
        //        this.Size = new Size(600, 500);
        //        TitleFont = new Font(Fonts.DENGXIANG, 40);
        //        sbrh_Title = new SolidBrush(Color.LightSeaGreen);
               
        //    }
             
        //    protected override void OnDraw()
        //    {
        //        base.OnDraw();
        //        DrawBorder(g);
        //        DrawVerticalBar(g, this.Width - 6, 50, 6, 75);
                
        //    }
        //    protected virtual void DrawText(Graphics _g,float x,float y)
        //    {
        //        _g.DrawString(strCaption, TitleFont, sbrh_Title, x, y);
        //    }
        //    protected virtual void DrawBorder(Graphics _g)
        //    {
        //        _g.DrawRectangle(pen_Border, 0, 0, this.Width - 1, this.Height - 1);
        //    }
        //    protected virtual void DrawVerticalBar(Graphics _g, float x,
        //      float y, float width, float height)
        //    {
        //        _g.FillRectangle(sldbrh_VerticalBar, x, y, width, height);
        //    }

        //    protected override void WndProc(ref Message m)
        //    {
        //        if (!bEnableDialog)
        //            switch (m.Msg)
        //            {

        //                case 0x0084:
        //                    base.WndProc(ref m);
        //                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
        //                        (int)m.LParam >> 16 & 0xFFFF);
        //                    vPoint = PointToClient(vPoint);
        //                    if (vPoint.X <= 5)
        //                        if (vPoint.Y <= 5)
        //                            m.Result = (IntPtr)Guying_HTTOPLEFT;
        //                        else if (vPoint.Y >= ClientSize.Height - 5)
        //                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
        //                        else m.Result = (IntPtr)Guying_HTLEFT;
        //                    else if (vPoint.X >= ClientSize.Width - 5)
        //                        if (vPoint.Y <= 5)
        //                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
        //                        else if (vPoint.Y >= ClientSize.Height - 5)
        //                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
        //                        else m.Result = (IntPtr)Guying_HTRIGHT;
        //                    else if (vPoint.Y <= 5)
        //                        m.Result = (IntPtr)Guying_HTTOP;
        //                    else if (vPoint.Y >= ClientSize.Height - 5)
        //                        m.Result = (IntPtr)Guying_HTBOTTOM;
        //                    break;
        //                case 0x0201:                //鼠标左键按下的消息   
        //                    m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标   
        //                    m.LParam = IntPtr.Zero; //默认值   
        //                    m.WParam = new IntPtr(2);//鼠标放在标题栏内   
        //                    base.WndProc(ref m);
        //                    break;
        //                default:
        //                    base.WndProc(ref m);
        //                    break;
        //            }
        //        else
        //            base.WndProc(ref m);
        //    }
        //    public Font FontX
        //    {
        //        get
        //        {
        //            return this.TitleFont;
        //        }

        //        set
        //        {
        //            TitleFont = value;
        //            Refresh();
        //        }
        //    }
        //    [BrowsableAttribute(false)]
        //    public override Font Font
        //    {
        //        get
        //        {
        //            return base.Font;
        //        }

        //        set
        //        {
        //            base.Font = value;
        //        }
        //    }
        //    public override string Text
        //    {
        //        get
        //        {
        //            return strCaption;
        //        }

        //        set
        //        {
        //            strCaption = value;
        //            Refresh();
        //        }
        //    }

        //    public virtual bool EnableDialog
        //    {
        //        get
        //        {
        //            return bEnableDialog;
        //        }
        //        set
        //        {
        //            bEnableDialog = value;
                   
        //        }
        //    }

        //    public Image Img_Up
        //    {
        //        get
        //        {
        //            return img_Up;
        //        }

        //        set
        //        {
        //            if (img_Up != null)
        //                img_Up.Dispose();
        //            img_Up = value;
                    
        //        }
        //    }

        //    public Image Img_Down
        //    {
        //        get
        //        {
        //            return img_Down;
        //        }

        //        set
        //        {
        //            if (img_Down != null)
        //                img_Down.Dispose();
        //            img_Down = value;
        //        }
        //    }
        //    public override Color ForeColor
        //    {
        //        get
        //        {
                 
        //            return base.ForeColor;
        //        }

        //        set
        //        {
        //            base.ForeColor = value;
        //            if (this.sbrh_Title == null)
        //                sbrh_Title = new SolidBrush(Color.Black);
        //            sbrh_Title.Color = value;
        //            Refresh();
        //        }
        //    }
        //    public string InfoTip
        //    {
        //          set
        //        {
        //            tipper.GetEmbeddedImages();
        //            Tip(value, tipper.img_Info);
        //        }
        //    }

        //    public string ErrorTip
        //    {
        //        set
        //        {
        //            tipper.GetEmbeddedImages();
        //            Tip(value, tipper.img_Error);
        //        }
        //    }
        //    public string DoneTip
        //    {
        //        set
        //        {
        //            tipper.GetEmbeddedImages();
        //            Tip(value, tipper.img_Done);
        //        }
        //    }
        //    public string UnderWorkingTip
        //    {
        //        set
        //        {
        //            tipper.GetEmbeddedImages();
        //            Tip(value, tipper.img_Underworking);
        //        }
        //    }
        //    protected ImageView LastRecordView
        //    {
        //        get
        //        {
        //            return imv_LastRecord;
        //        }
        //    }
        //}
        //public class Session:ModernWnd
        //{
        //    Revert rvt_Back;
        //    Point pnt_TitlePos;
        //    [Category("外观")]
        //    public Point TitlePos
        //    {
        //        get
        //        {
        //            return pnt_TitlePos;
        //        }

        //        set
        //        {
        //            pnt_TitlePos = value;
        //            Refresh();
        //        }
        //    }
        //    protected override void prepareData()
        //    {
        //        pnt_TitlePos = new Point(80, 10) ;
        //        base.prepareData();
        //    }
        //    protected override void OnDraw()
        //    {
        //        DrawBorder(g);
        //        DrawVerticalBar(g, this.Width - 7, 30, 6, 75);
        //        DrawText(g, pnt_TitlePos.X,pnt_TitlePos.Y);
        //    }
        //    protected override void prepareCtrls()
        //    {
        //        base.prepareCtrls();
        //        rvt_Back = new Revert();
        //        rvt_Back.Size = new System.Drawing.Size(45, 45);
        //        rvt_Back.Location = new Point(10,10);

        //    }
        //    protected override void prepareEvents()
        //    {
        //        base.prepareEvents();
        //        rvt_Back.Click += Rvt_Back_Click;
        //    }
        //    protected override void UnitCtrls()
        //    {
        //        base.UnitCtrls();
        //        this.AddControl(rvt_Back);
        //        ImageView imv = LastRecordView;
        //       imv.Location = new Point(this.Width - imv.Width, 2);
        //    }
        //    private void Rvt_Back_Click(object sender, EventArgs e)
        //    {
        //        this.Close();
        //    }
        //    protected override void prepareAppearance()
        //    {
        //        base.prepareAppearance();
        //        TitleFont = new Font(Fonts.DENGXIANG, 35);
        //    }
        //}
      
     
        [ToolboxItem(false)]
        [Browsable(false)]
        public class Switcher : UserControl
        {
            //0 -> Min
            //1 -> Max
            //2 -> Close
            bool _EnableEx = false;
            [Category("个性化")]
            public bool EnableEx
            {
                get { return _EnableEx; }
                set
                {
                    _EnableEx = value;
                    Refresh();
                }
            }
            int _Switch_Feature = 0;
            [Category("个性化")]
            public int Switch_Feature
            {
                get { return _Switch_Feature; }
                set
                {
                    _Switch_Feature = value;
                    Refresh();
                }
            }
            float _CirclePenSize = 2;
            [Category("个性化")]
            public float CirclePenSize
            {
                get { return _CirclePenSize; }
                set
                {
                    _CirclePenSize = value;
                    _Pen_Circle.Width = _CirclePenSize;
                }
            }
            float _InnerPenSize = 2;
            [Category("个性化")]
            public float InnerPenSize
            {
                get { return _InnerPenSize; }
                set
                {
                    _InnerPenSize = value;
                    _Pen_Inner.Width = value;
                }
            }
            Color _CircleColor = Color.Black;
            [Category("个性化")]
            public Color CircleColor
            {
                get { return _CircleColor; }
                set
                {
                    _CircleColor = value;
                    _Pen_Circle.Color = value;
                }
            }
            Color _InnerColor = Color.Red;
            [Category("个性化")]
            public Color InnerColor
            {
                get { return _InnerColor; }
                set
                {
                    _InnerColor = value;
                    _Pen_Inner.Color = value;
                    InnerPen_Leave = value;
                }
            }
            PointF Point_CircleLocation;
            PointF Point_Left_Up;
            PointF Point_Left_Down;
            PointF Point_Right_Up;
            PointF Point_Right_Down;
            Pen _Pen_Circle;
            Pen _Pen_Inner;
            public Color MouseUpColor = Color.Transparent,
                   MouseLeaveColor = Color.Transparent,
                   MouseDownColor = Color.DarkGray,
                   MouseEnterColor = Color.LightGray;
            float Radius = 0;
            public Color InnerPen_Down_Enter = Color.White, InnerPen_Leave = Color.Black;
            public float fOffset_X = 1, fOffset_Y = 4;

            public Switcher()
            {
                this.BackColor = Color.Transparent;
                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
                SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
                _Pen_Circle = new Pen(_CircleColor, _CirclePenSize);
                _Pen_Inner = new Pen(_InnerColor, _InnerPenSize);
                this.Size = new Size(20, 20);
                Point_CircleLocation = new PointF();
                Point_Left_Down = new PointF();
                Point_Left_Up = new PointF();
                Point_Right_Down = new PointF();
                Point_Right_Up = new PointF();

            }
            void DrawCross(Graphics Canvas)
            {
               
               
                Point_Left_Up.X = 6.0f / 10.0f * Width / 2.0f;
                Point_Left_Up.Y = Point_Left_Up.X;
                Point_Right_Down.X = 14.0f / 20.0f * Width;
                Point_Right_Down.Y = Point_Right_Down.X;
                Point_Left_Down.X = Point_Left_Up.X;
                Point_Left_Down.Y = Point_Right_Down.Y;
                Point_Right_Up.X = Point_Left_Down.Y;
                Point_Right_Up.Y = Point_Left_Down.X;
                Canvas.DrawLine(_Pen_Inner, Point_Left_Up, Point_Right_Down);
                Canvas.DrawLine(_Pen_Inner, Point_Left_Down, Point_Right_Up);
            }
            void DrawMini(Graphics Canvas)
            {
                 
              
                Point_Left_Up.X = Width / 4;
                Point_Left_Up.Y = this.Height / 2 + this.Height / 8;
                Point_Right_Up.X = 3.0f / 4.0f * Width;
                Point_Right_Up.Y = Point_Left_Up.Y;
                Canvas.DrawLine(_Pen_Inner, Point_Left_Up, Point_Right_Up);

            }
            void DrawMax(Graphics Canvas)
            {
                
                Point_Left_Up.X = Width / 4.0f + fOffset_X;
                Point_Left_Up.Y = Height / 6.0f + fOffset_Y;
                Point_Right_Up.X = Point_Left_Up.X + Width / 2.0f;
                Point_Right_Up.Y = Point_Left_Up.Y;
                Point_Left_Down.X = Width / 4.0f;
                Point_Left_Down.Y = Width - Width / 6.0f - fOffset_Y;
                Point_Right_Down.X = Point_Right_Up.X;
                Point_Right_Down.Y = Point_Left_Down.Y;
                Canvas.DrawRectangle(_Pen_Inner, Point_Left_Up.X, Point_Left_Up.Y, Point_Right_Up.X
                    - Point_Left_Up.X, Point_Left_Down.Y - Point_Left_Up.Y);
                Canvas.DrawLine(_Pen_Inner, Point_Left_Up.X, Point_Left_Up.Y + 1, Point_Right_Up.X
                    , Point_Right_Up.Y + 1);
                Canvas.DrawLine(_Pen_Inner, Point_Left_Up.X, Point_Left_Up.Y + 2, Point_Right_Up.X
                , Point_Right_Up.Y + 2);
            }
            void DrawCommonPart(Graphics Canvas)
            {
                float temp = 0;
                temp = 10.0f;
                Point_CircleLocation.X = 2.0f / 10.0f * this.Width / 2;
                Point_CircleLocation.Y = 2.0f / 10.0f * this.Width / 2;
                Radius = 8.0f / temp * Width / 2;
                Canvas.DrawEllipse(_Pen_Circle, Point_CircleLocation.X
            , Point_CircleLocation.Y, Radius * 2, Radius * 2
             );
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                base.OnPaint(e);
                DrawCommonPart(e.Graphics);
                switch (_Switch_Feature)
                {
                    case 0: DrawMini(e.Graphics); break;
                    case 1: DrawMax(e.Graphics); break;
                    case 2: DrawCross(e.Graphics); break;
                }



            }
            protected override void OnMouseUp(MouseEventArgs e)
            {

                base.OnMouseUp(e);

                OnMouseEnter(e);
                Refresh();

            }
            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                if (!_EnableEx)
                {
                    this.BackColor = MouseDownColor;

                }
                else
                    _Pen_Inner.Color = MouseDownColor;

                Refresh();
            }
            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                if (!_EnableEx)
                {
                    this.BackColor = MouseEnterColor;
                    _Pen_Inner.Color = InnerPen_Down_Enter;
                }
                else
                    _Pen_Inner.Color = MouseEnterColor;
                Refresh();
            }
            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                if (!_EnableEx)
                {
                    this.BackColor = MouseLeaveColor;
                    _Pen_Inner.Color = InnerPen_Leave;
                }
                else
                    _Pen_Inner.Color = MouseLeaveColor;
                Refresh();
            }
        }
      
    }
}


/*
   public class RibbonWindowEx:RibbonWindow
        {
            Panel MenuPanel;
            Panel ToolPanel;
            Panel ContentPanel;

            public Control Content
            {
               
                set
                {
                    value.Dock = DockStyle.Fill;
                    value.BackColor = Color.Transparent;
                    ContentPanel.Controls.Add(value);
                }
            }
           
            public Panel ContentBox
            {
                get
                {
                    return ContentPanel;
                }
                 
            }

            protected override void prepareCtrls()
            {
                base.prepareCtrls();
                MenuPanel = new Panel();
                ContentPanel = new Panel();
                MenuPanel.BackColor = Color.Transparent;
                ContentPanel.BackColor = Color.Transparent;
            }
            protected override void prepareEvents()
            {
                base.prepareEvents();
                this.Resize += Frm_Main_Resize;
            }
            protected override void UnitCtrls()
            {
                base.UnitCtrls();
                AddControl(this.MenuPanel);
                AddControl(this.ContentPanel);
            }
            private void Frm_Main_Resize(object sender, EventArgs e)
            {
                MenuPanel.Location = new Point(4, 42);
                MenuPanel.Size = new Size(this.Width - 8, 30);
                ContentPanel.Location = new Point(4, 72);
                ContentPanel.Size = new Size(this.Width - 8, 90);
            }
            public class MenuPad
            {
                Panel pnl_Main;
                Panel pnl_Tools;
                List<String> lst_ItemText;
                List<MenuButton> lst_ItemBtn;
                List<UserControl> lst_ItemUserControl;
                // MenuButton btns;
                Dictionary<MenuButton, UserControl> dct_RibbonPad;
              
                public MenuPad(Panel pnl_Main,Panel pnl_Tools)
                {
                    InitCollections();
                    this.pnl_Main = pnl_Main;
                    this.pnl_Tools = pnl_Tools;
                }

                private void InitCollections()
                {
                    dct_RibbonPad =
                         new Dictionary<MenuButton, UserControl>();
                    lst_ItemBtn = new List<MenuButton>();
                    lst_ItemText = new List<string>();
                    lst_ItemUserControl = new List<UserControl>();
                }
                void PrepareSubItem(MenuButton btn,UserControl ctrl)
                {
                   

                      
                }
                public String [] MenuItems
                {
                    set
                    {
                        lst_ItemText.AddRange(value);
                    }
                    get
                    {
                        return lst_ItemText.ToArray();
                    }
                }

                public Panel  MainPanel
                {
                    get
                    {
                        return pnl_Main;
                    }

                    set
                    {
                        pnl_Main = value;
                    }
                }

                public Panel ToolPanel
                {
                    get
                    {
                        return pnl_Tools;
                    }

                    set
                    {
                        pnl_Tools = value;
                    }
                }

                void PrepareMenuItem(String [] strArr)
                {
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        lst_ItemBtn.Add(new MenuButton());
                        lst_ItemUserControl.Add(new UserControl());
                        MenuButton btn = lst_ItemBtn[lst_ItemBtn.Count - 1];
                        UserControl ctrl = lst_ItemUserControl[lst_ItemUserControl.Count - 1];
                        btn.Width = 80;
                        btn.Height = 30;
                        btn.Text = strArr[i];
                        btn.Location = new Point(0 + 80 * i, MainPanel.Location.Y);
                        ctrl.Dock = DockStyle.Fill;
                        this.ToolPanel.Controls.Add(ctrl);
                        dct_RibbonPad.Add(btn,ctrl);
                    }
                   
                }
                public class MenuButton : TextView
                {
                    #region 字段

                    Color[] UpLinearGradientColors;
                    Color[] DownLinearGradientColors;
                    Color BorderColor;
                    //上渐变颜色
                    Color Up_StartColor, Up_EndColor;
                    //下渐变颜色
                    Color Down_StartColor, Down_EndColor;
                    //上下渐变分隔
                    float fSplit = 0.4f;
                    LinearGradientBrush Uplgb;
                    LinearGradientBrush Downlgb;
                    RectangleF Up_Rect;
                    RectangleF Down_Rect;
                    Pen Pen_Border;
                    bool _Disable = false;
                    #endregion
                    #region 属性
                    public bool Disable
                    {
                        get { return _Disable; }
                        set
                        {
                            _Disable = value;
                            if (value)
                            {
                                Pen_Border.Color = Color.Gray;
                                this.ForeColor = Color.Gray;

                            }
                            else
                            {
                                Pen_Border.Color = BorderColor;
                                this.ForeColor = Color.FromArgb(21, 66, 139);
                            }

                            Refresh();
                        }
                    }
                    #endregion
                    #region 消息处理
                    void ButtonR_Resize(object sender, EventArgs e)
                    {
                        if (this.Height > 0)
                        {
                            if (this.Height == 30)
                                this.Height += 1;

                            GetPaintFramework();
                        }


                    }
                    public void Activated()
                    {
                        UpLinearGradientColors[0] = Color.FromArgb(255, 254, 237);
                        UpLinearGradientColors[1] = Color.FromArgb(255, 237, 188);
                        DownLinearGradientColors[0] = Color.FromArgb(255, 218, 120);
                        DownLinearGradientColors[1] = Color.FromArgb(255, 232, 167);
                        Pen_Border.Color = Color.FromArgb(197, 176, 128);
                        Uplgb.LinearColors = UpLinearGradientColors;
                        Downlgb.LinearColors = DownLinearGradientColors;
                    }
                    public void Normal()
                    {
                        Up_StartColor = Color.FromArgb(224, 237, 255);

                        Up_EndColor = Color.FromArgb(196, 221, 255);
                        Down_StartColor = Color.FromArgb(173, 209, 255);
                        Down_EndColor = Color.FromArgb(191, 218, 255);
                        BorderColor = Color.FromArgb(139, 174, 218);
                    }
                    private void MenuButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
                    {

                        Activated();
                        Refresh();
                    }
                    #endregion
                    protected override void prepareAppearance()
                    {
                        base.prepareAppearance();
                        UpLinearGradientColors = new Color[2];
                        DownLinearGradientColors = new Color[2];

                        Up_StartColor = Color.FromArgb(224, 237, 255);

                        Up_EndColor = Color.FromArgb(196, 221, 255);
                        Down_StartColor = Color.FromArgb(173, 209, 255);
                        Down_EndColor = Color.FromArgb(191, 218, 255);
                        BorderColor = Color.FromArgb(139, 174, 218);
                        Pen_Border = new Pen(BorderColor, 1);
                        this.Font = new Font("微软雅黑", 10);
                        this.ForeColor = Color.FromArgb(21, 66, 139);

                        Up_Rect = new RectangleF();
                        Down_Rect = new RectangleF();
                        this.bEnableResize = true;
                        GetPaintFramework();
                    }
                    protected override void prepareEvents()
                    {
                        base.prepareEvents();
                        this.Resize += new EventHandler(ButtonR_Resize);
                        this.MouseDown += MenuButton_MouseDown;

                    }
                    protected override void OnDraw()
                    {
                        if (Disable == true)
                        {
                            Pen_Border.Color = Color.Gray;
                            this.ForeColor = Color.Gray;
                            this.BackColor = Color.LightGray;
                            g.DrawRectangle(Pen_Border, ClientRectangle.X, ClientRectangle.Y
                               , ClientSize.Width - 1, ClientSize.Height - 1
                                );

                        }
                        else
                        {
                            g.FillRectangle(Uplgb, Up_Rect);
                            g.FillRectangle(Downlgb, Down_Rect);
                            g.DrawRectangle(Pen_Border, ClientRectangle.X, ClientRectangle.Y
                                , ClientSize.Width - 1, ClientSize.Height - 1
                                 );
                        }
                        base.OnDraw();


                    }
                    void GetPaintFramework()
                    {
                        if (this.ClientSize.Width == 0)
                            this.Size = new Size(100, 30);
                        Up_Rect.X = 0;
                        Up_Rect.Y = 0;
                        Up_Rect.Width = this.ClientRectangle.Width;
                        Up_Rect.Height = this.ClientRectangle.Height * fSplit;
                        Down_Rect.X = 0;
                        Down_Rect.Y = Up_Rect.Height;


                        Down_Rect.Width = Width;
                        Down_Rect.Height = Height * (1 - fSplit);

                        Uplgb = new LinearGradientBrush(Up_Rect, Up_StartColor, Up_EndColor, LinearGradientMode.Vertical);
                        Downlgb = new LinearGradientBrush(Down_Rect, Down_StartColor, Down_EndColor, LinearGradientMode.Vertical);

                    }

                }
            }
        }
 
 */