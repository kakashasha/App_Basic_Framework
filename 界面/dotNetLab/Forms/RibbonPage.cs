
using dotNetLab.Appearance;
using dotNetLab.Widgets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Forms
{
    public class RibbonPage : Wnd
    {
        bool isMax = false;
        public TextBlock txv_Status;
        Switcher[] FormSwitchers;

        int nRect_Top_Up_Height = 14, nRect_Top_Height = 40;
        Pen Pen_Border, Pen_Top_Underline;
        SolidBrush sldBrush_Main, sldBrush_Text;
        Color BorderColor, FontColor, Top_UnderlineColor, MainPanelColor;
        Color Top_Up_Color_Start, Top_Up_Color_End;
        Color Top_Down_Color_End, Top_Down_Color_Start;
        Rectangle Rect_Top_Up, Rect_Top_Down;
        LinearGradientBrush Lgb_Top_Up, Lgb_Top_Down;
        MobilePictureBox imv_Theme;
        GraphicsPath FormPath, FormPath_Draw;
        Rectangle arcRect, arcRect_Right;
        public Region FormRegion;
        public int Radius = 8;
        private Font TitleFont;
        private string strCaption;
        const int Guying_HTLEFT = 10;
        const int Guying_HTRIGHT = 11;
        const int Guying_HTTOP = 12;
        const int Guying_HTTOPLEFT = 13;
        const int Guying_HTTOPRIGHT = 14;
        const int Guying_HTBOTTOM = 15;
        const int Guying_HTBOTTOMLEFT = 0x10;
        const int Guying_HTBOTTOMRIGHT = 17;
        bool bEnableDialog = false;
        [Category("外观")]
        public Image IconImage

        {
            get
            {
                return imv_Theme.Source;
            }
            set
            {
                imv_Theme.Source = value;

            }
        }


        public RibbonPage() : base(true)
        {

        }
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            FormPath = new GraphicsPath();
            FormPath_Draw = new GraphicsPath();
            imv_Theme = new MobilePictureBox();
            imv_Theme.BorderColor = Color.Transparent;
            imv_Theme.Location = new Point(10, 4);
            imv_Theme.Size = new Size(32, 32);
            imv_Theme.ImageSize = new Size(30, 30);
            txv_Status = new TextBlock();
            txv_Status.Font = new Font(Fonts.YAHEI, 11);
            txv_Status.ForeColor = Color.FromArgb(62, 106, 170);
            PrepareColor();
            Init();
        }
        void Init()
        {

            this.BackColor = Color.White;
            arcRect = new Rectangle(0, 0, Radius, Radius);
            Pen_Border = new Pen(BorderColor, 2f);
            Pen_Top_Underline = new Pen(Top_UnderlineColor, 2);
            sldBrush_Main = new SolidBrush(MainPanelColor);
            sldBrush_Text = new SolidBrush(FontColor);
            txv_Status.Location = new Point(10, this.Height - 30);
            txv_Status.Text = "就绪";

            this.Controls.Add(txv_Status);
            arcRect_Right = new Rectangle(0, 0, Radius, Radius);

            this.Controls.Add(imv_Theme);
            PrepareSwitcher();

            PrepareColor();
            PrepareSize();
            FontX = new Font(Fonts.YAHEI, 12);
            this.Resize += new EventHandler(it_Resize);
        }
        void PrepareSwitcher()
        {

            int z = 3;
            FormSwitchers = new Switcher[3];
            //close 2
            //mini 0
            //Max 1
            for (int i = 0; i < 3; i++)
            {
                FormSwitchers[i] = new Switcher();
                FormSwitchers[i].Switch_Feature = i;
                FormSwitchers[i].EnableEx = true;
                FormSwitchers[i].InnerColor = Color.FromArgb(21, 66, 139);
                FormSwitchers[i].CircleColor = Color.Transparent;
                FormSwitchers[i].Size = new Size(30, 30);
                FormSwitchers[i].Location = new Point(this.ClientSize.Width - 10 - (z--) * 30, 0);
                FormSwitchers[i].MouseEnterColor = Color.FromArgb(71, 116, 189);
                FormSwitchers[i].MouseDownColor = Color.FromArgb(0, 46, 119);
                FormSwitchers[i].MouseLeaveColor = Color.FromArgb(21, 66, 139);
                this.Controls.Add(FormSwitchers[i]);
            }
            FormSwitchers[1].InnerPenSize = 1;
            FormSwitchers[1].fOffset_X -= 1;
            FormSwitchers[0].Click += new EventHandler(FormSwitcher_Mini_Click);
            FormSwitchers[1].Click += new EventHandler(FormSwitcher_Max_Click);
            FormSwitchers[2].Click += new EventHandler(FormSwitcher_Close_Click);


        }
        void PrepareSize()
        {


            if (this.WindowState != FormWindowState.Minimized)
            {

                Rect_Top_Up = new Rectangle(0, 0, this.Width, nRect_Top_Up_Height);
                Rect_Top_Down = new Rectangle(0, nRect_Top_Up_Height, this.Width,
                    nRect_Top_Height - nRect_Top_Up_Height);
                Lgb_Top_Up = new LinearGradientBrush(Rect_Top_Up, Top_Up_Color_Start,
                    Top_Up_Color_End, LinearGradientMode.Vertical);
                Lgb_Top_Down = new LinearGradientBrush(Rect_Top_Down, Top_Down_Color_Start,
                    Top_Down_Color_End, LinearGradientMode.Vertical);

            }
            GetRibbonRectPath();
            this.Region = new Region(FormPath);
        }
        void PrepareColor()
        {
            // BorderColor = Color.FromArgb(59, 90, 130);
            BorderColor = Color.DodgerBlue;
            Top_Up_Color_Start = Color.FromArgb(226, 235, 248);
            Top_Up_Color_End = Color.FromArgb(219, 233, 252);
            Top_Down_Color_Start = Color.FromArgb(204, 223, 247);
            Top_Down_Color_End = Color.FromArgb(224, 237, 252);
            FontColor = Color.FromArgb(62, 106, 170);
            Top_UnderlineColor = Color.FromArgb(220, 244, 254);
            MainPanelColor = Color.FromArgb(194, 217, 247);
        }
        void GetRibbonRectPath()
        {

            //左上角
            FormPath.AddArc(arcRect, 180, 90);
            FormPath_Draw.AddArc(arcRect, 180, 90);
            FormPath.AddLine(Radius, 0, this.ClientRectangle.Width - Radius, 0);
            FormPath_Draw.AddLine(Radius, 0, this.ClientRectangle.Width - Radius, 0);
            // 右上角
            arcRect_Right.X = this.ClientRectangle.Right - Radius - 1;
            FormPath.AddArc(arcRect_Right, 270, 90);
            FormPath_Draw.AddArc(arcRect_Right, 270, 90);
            FormPath.AddLine(this.ClientRectangle.Width, Radius, this.ClientRectangle.Width, this.ClientRectangle.Height);

            FormPath.AddLine(this.ClientRectangle.Width, this.ClientRectangle.Height, 0, this.ClientRectangle.Height);

            FormPath.AddLine(0, this.ClientRectangle.Height - Radius, 0, Radius);

            FormPath_Draw.AddLine(this.ClientRectangle.Width - 1, Radius, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
            FormPath_Draw.AddLine(this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1, 0, this.ClientRectangle.Height - 1);
            FormPath_Draw.AddLine(0, this.ClientRectangle.Height - Radius, 0, Radius);
        }

        void it_Resize(object sender, EventArgs e)
        {
            FormPath.Reset();
            FormPath_Draw.Reset();
            int z = 3;
            for (int i = 0; i < 3; i++)
                FormSwitchers[i].Location = new Point(this.ClientSize.Width - 10 - (z--) * 30, 9);
            PrepareSize();
            txv_Status.Location = new Point(5, this.Height - txv_Status.Height);
            this.Refresh();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
                g.DrawRectangle(Pen_Top_Underline, 1, 1, this.ClientSize.Width - 2, this.ClientSize.Height - 2);
            // e.Graphics.DrawRectangle(Pen_Top_Underline, 2, 2, it.ClientSize.Width - 4, it.ClientSize.Height - 4);
            g.FillRectangle(Lgb_Top_Up, Rect_Top_Up);
            g.FillRectangle(Lgb_Top_Down, Rect_Top_Down);
            g.DrawLine(Pen_Top_Underline, 0, nRect_Top_Height, this.Width, nRect_Top_Height);
            g.DrawPath(Pen_Border, FormPath_Draw);
            g.DrawString(strCaption, FontX, sldBrush_Text, 50, 10);
            base.OnPaint(e);
        }

        void FormSwitcher_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void FormSwitcher_Max_Click(object sender, EventArgs e)
        {
            if (!isMax)
            {
                this.WindowState = FormWindowState.Maximized;
                isMax = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                isMax = false;
            }

        }
        void FormSwitcher_Mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        public Font FontX
        {
            get
            {
                return this.TitleFont;
            }

            set
            {
                TitleFont = value;
                Refresh();
            }
        }
        [BrowsableAttribute(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;
            }
        }
        public override string Text
        {
            get
            {
                return strCaption;
            }

            set
            {
                strCaption = value;
                Refresh();
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (!bEnableDialog)
                switch (m.Msg)
                {

                    case 0x0084:
                        base.WndProc(ref m);
                        Point vPoint = new Point((int)m.LParam & 0xFFFF,
                            (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);
                        if (vPoint.X <= 5)
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)Guying_HTTOPLEFT;
                            else if (vPoint.Y >= ClientSize.Height - 5)
                                m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                            else m.Result = (IntPtr)Guying_HTLEFT;
                        else if (vPoint.X >= ClientSize.Width - 5)
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)Guying_HTTOPRIGHT;
                            else if (vPoint.Y >= ClientSize.Height - 5)
                                m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                            else m.Result = (IntPtr)Guying_HTRIGHT;
                        else if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOP;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOM;
                        break;
                    case 0x0201:                //鼠标左键按下的消息   
                        m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标   
                        m.LParam = IntPtr.Zero; //默认值   
                        m.WParam = new IntPtr(2);//鼠标放在标题栏内   
                        base.WndProc(ref m);
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            else
                base.WndProc(ref m);
        }
        [Category("外观")]
        public bool EnableDialog
        {
            get
            {
                return bEnableDialog;
            }
            set
            {
                bEnableDialog = value;
                this.FormSwitchers[1].Enabled = false;
            }
        }
    }
    public class RibbonPageEx : RibbonPage
    {
        Panel pnl_Main;
        public Control Child
        {
            set
            {
                value.Dock = DockStyle.Fill;
                this.pnl_Main.Controls.Add(value);
            }
        }
        protected override void prepareCtrls()
        {

            base.prepareCtrls();
            pnl_Main = new Panel();
            this.pnl_Main.Location = new System.Drawing.Point(4, 44);
            this.pnl_Main.Size = new System.Drawing.Size(this.Width - 8, this.Height - 44 - this.txv_Status.Height);
        }
        protected override void prepareEvents()
        {
            base.prepareEvents();
            this.Resize += RibbonWindowEx_Resize;
        }
        void RibbonWindowEx_Resize(object sender, EventArgs e)
        {
            this.pnl_Main.Location = new System.Drawing.Point(4, 44);
            this.pnl_Main.Size = new System.Drawing.Size(this.Width - 8,
                this.Height - 44 - this.txv_Status.Height);
        }
        protected override void UnitCtrls()
        {
            base.UnitCtrls();
            this.AddControl(pnl_Main);
        }
    }
}
