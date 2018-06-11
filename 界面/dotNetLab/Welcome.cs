//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Windows.Forms;
//using System.Drawing;
//using System.ComponentModel;
//using System.Drawing.Drawing2D;

//namespace SHIKII.UI
//{
//    public class Colors
//    {
//        public static Color clrVisual_Studio_DeepBlue = Color.FromArgb(0, 122, 204);
//        public static Color clrVisual_Studio_LightBlue = Color.FromArgb(28, 151, 234);
//        public static Color clrBlueViolet = Color.BlueViolet;
//    }
//    class Fonts
//    {
//        public static String YAHEI = "微软雅黑";
//        public static String YUGOTHIC = "Yu Gothic UI Light";
//        public static String DENGXIANG = "等线 Light";
//    }
//    public class Switcher : UserControl
//    {
//        //0 -> Min
//        //1 -> Max
//        //2 -> Close
//        bool _EnableEx = false;
//        [Category("个性化")]
//        public bool EnableEx
//        {
//            get { return _EnableEx; }
//            set
//            {
//                _EnableEx = value;
//                Refresh();
//            }
//        }
//        int _Switch_Feature = 0;
//        [Category("个性化")]
//        public int Switch_Feature
//        {
//            get { return _Switch_Feature; }
//            set
//            {
//                _Switch_Feature = value;
//                Refresh();
//            }
//        }
//        float _CirclePenSize = 2;
//        [Category("个性化")]
//        public float CirclePenSize
//        {
//            get { return _CirclePenSize; }
//            set
//            {
//                _CirclePenSize = value;
//                _Pen_Circle.Width = _CirclePenSize;
//            }
//        }
//        float _InnerPenSize = 2;
//        [Category("个性化")]
//        public float InnerPenSize
//        {
//            get { return _InnerPenSize; }
//            set
//            {
//                _InnerPenSize = value;
//                _Pen_Inner.Width = value;
//            }
//        }
//        Color _CircleColor = Color.Black;
//        [Category("个性化")]
//        public Color CircleColor
//        {
//            get { return _CircleColor; }
//            set
//            {
//                _CircleColor = value;
//                _Pen_Circle.Color = value;
//            }
//        }
//        Color _InnerColor = Color.Red;
//        [Category("个性化")]
//        public Color InnerColor
//        {
//            get { return _InnerColor; }
//            set
//            {
//                _InnerColor = value;
//                _Pen_Inner.Color = value;
//                InnerPen_Leave = value;
//            }
//        }
//        PointF Point_CircleLocation;
//        PointF Point_Left_Up;
//        PointF Point_Left_Down;
//        PointF Point_Right_Up;
//        PointF Point_Right_Down;
//        Pen _Pen_Circle;
//        Pen _Pen_Inner;
//        public Color MouseUpColor = Color.Transparent,
//               MouseLeaveColor = Color.Transparent,
//               MouseDownColor = Color.DarkGray,
//               MouseEnterColor = Color.LightGray;
//        float Radius = 0;
//        public Color InnerPen_Down_Enter = Color.White, InnerPen_Leave = Color.Black;
//        public float fOffset_X = 1, fOffset_Y = 4;

//        public Switcher()
//        {
//            this.BackColor = Color.Transparent;
//            SetStyle(ControlStyles.UserPaint, true);
//            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
//            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
//            _Pen_Circle = new Pen(_CircleColor, _CirclePenSize);
//            _Pen_Inner = new Pen(_InnerColor, _InnerPenSize);
//            this.Size = new Size(20, 20);
//            Point_CircleLocation = new PointF();
//            Point_Left_Down = new PointF();
//            Point_Left_Up = new PointF();
//            Point_Right_Down = new PointF();
//            Point_Right_Up = new PointF();

//        }
//        void DrawCross(Graphics Canvas)
//        {
//            float temp = 0;
//            temp = 10.0f;
//            Point_Left_Up.X = 6.0f / 10.0f * Width / 2.0f;
//            Point_Left_Up.Y = Point_Left_Up.X;
//            Point_Right_Down.X = 14.0f / 20.0f * Width;
//            Point_Right_Down.Y = Point_Right_Down.X;
//            Point_Left_Down.X = Point_Left_Up.X;
//            Point_Left_Down.Y = Point_Right_Down.Y;
//            Point_Right_Up.X = Point_Left_Down.Y;
//            Point_Right_Up.Y = Point_Left_Down.X;
//            Canvas.DrawLine(_Pen_Inner, Point_Left_Up, Point_Right_Down);
//            Canvas.DrawLine(_Pen_Inner, Point_Left_Down, Point_Right_Up);
//        }
//        void DrawMini(Graphics Canvas)
//        {
//            float temp = 0;
//            temp = 10.0f;
//            Point_Left_Up.X = Width / 4;
//            Point_Left_Up.Y = this.Height / 2 + this.Height / 8;
//            Point_Right_Up.X = 3.0f / 4.0f * Width;
//            Point_Right_Up.Y = Point_Left_Up.Y;
//            Canvas.DrawLine(_Pen_Inner, Point_Left_Up, Point_Right_Up);

//        }
//        void DrawMax(Graphics Canvas)
//        {
//            float temp = 0;
//            temp = 10.0f;
//            Point_Left_Up.X = Width / 4.0f + fOffset_X;
//            Point_Left_Up.Y = Height / 6.0f + fOffset_Y;
//            Point_Right_Up.X = Point_Left_Up.X + Width / 2.0f;
//            Point_Right_Up.Y = Point_Left_Up.Y;
//            Point_Left_Down.X = Width / 4.0f;
//            Point_Left_Down.Y = Width - Width / 6.0f - fOffset_Y;
//            Point_Right_Down.X = Point_Right_Up.X;
//            Point_Right_Down.Y = Point_Left_Down.Y;
//            Canvas.DrawRectangle(_Pen_Inner, Point_Left_Up.X, Point_Left_Up.Y, Point_Right_Up.X
//                - Point_Left_Up.X, Point_Left_Down.Y - Point_Left_Up.Y);
//            Canvas.DrawLine(_Pen_Inner, Point_Left_Up.X, Point_Left_Up.Y + 1, Point_Right_Up.X
//                , Point_Right_Up.Y + 1);
//            Canvas.DrawLine(_Pen_Inner, Point_Left_Up.X, Point_Left_Up.Y + 2, Point_Right_Up.X
//            , Point_Right_Up.Y + 2);
//        }
//        void DrawCommonPart(Graphics Canvas)
//        {
//            float temp = 0;
//            temp = 10.0f;
//            Point_CircleLocation.X = 2.0f / 10.0f * this.Width / 2;
//            Point_CircleLocation.Y = 2.0f / 10.0f * this.Width / 2;
//            Radius = 8.0f / temp * Width / 2;
//            Canvas.DrawEllipse(_Pen_Circle, Point_CircleLocation.X
//        , Point_CircleLocation.Y, Radius * 2, Radius * 2
//         );
//        }
//        protected override void OnPaint(PaintEventArgs e)
//        {
//            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
//            base.OnPaint(e);
//            DrawCommonPart(e.Graphics);
//            switch (_Switch_Feature)
//            {
//                case 0: DrawMini(e.Graphics); break;
//                case 1: DrawMax(e.Graphics); break;
//                case 2: DrawCross(e.Graphics); break;
//            }



//        }
//        protected override void OnMouseUp(MouseEventArgs e)
//        {

//            base.OnMouseUp(e);

//            OnMouseEnter(e);
//            Refresh();

//        }
//        protected override void OnMouseDown(MouseEventArgs e)
//        {
//            base.OnMouseDown(e);
//            if (!_EnableEx)
//            {
//                this.BackColor = MouseDownColor;

//            }
//            else
//                _Pen_Inner.Color = MouseDownColor;

//            Refresh();
//        }
//        protected override void OnMouseEnter(EventArgs e)
//        {
//            base.OnMouseEnter(e);
//            if (!_EnableEx)
//            {
//                this.BackColor = MouseEnterColor;
//                _Pen_Inner.Color = InnerPen_Down_Enter;
//            }
//            else
//                _Pen_Inner.Color = MouseEnterColor;
//            Refresh();
//        }
//        protected override void OnMouseLeave(EventArgs e)
//        {
//            base.OnMouseLeave(e);
//            if (!_EnableEx)
//            {
//                this.BackColor = MouseLeaveColor;
//                _Pen_Inner.Color = InnerPen_Leave;
//            }
//            else
//                _Pen_Inner.Color = MouseLeaveColor;
//            Refresh();
//        }
//    }
//    public class RibbonWindow : Wnd
//    {
//        bool isMax = false;
//        public TextView txv_Status;
//        Switcher[] FormSwitchers;

//        int nRect_Top_Up_Height = 14, nRect_Top_Height = 40;
//        Pen Pen_Border, Pen_Top_Underline;
//        SolidBrush sldBrush_Main, sldBrush_Text;
//        Color BorderColor, FontColor, Top_UnderlineColor, MainPanelColor;
//        Color Top_Up_Color_Start, Top_Up_Color_End;
//        Color Top_Down_Color_End, Top_Down_Color_Start;
//        Rectangle Rect_Top_Up, Rect_Top_Down;
//        LinearGradientBrush Lgb_Top_Up, Lgb_Top_Down;
//        ImageView imv_Theme;
//        GraphicsPath FormPath, FormPath_Draw;
//        Rectangle arcRect, arcRect_Right;
//        public Region FormRegion;
//        public int Radius = 8;
//        private Font TitleFont;
//        private string strCaption;
//        const int Guying_HTLEFT = 10;
//        const int Guying_HTRIGHT = 11;
//        const int Guying_HTTOP = 12;
//        const int Guying_HTTOPLEFT = 13;
//        const int Guying_HTTOPRIGHT = 14;
//        const int Guying_HTBOTTOM = 15;
//        const int Guying_HTBOTTOMLEFT = 0x10;
//        const int Guying_HTBOTTOMRIGHT = 17;
//        bool bEnableDialog = false;
//        [Category("外观")]
//        public Image IconImage

//        {
//            get
//            {
//                return imv_Theme.Source;
//            }
//            set
//            {
//                imv_Theme.Source = value;

//            }
//        }


//        public RibbonWindow() : base(true)
//        {

//        }
//        protected override void prepareAppearance()
//        {
//            base.prepareAppearance();
//            FormPath = new GraphicsPath();
//            FormPath_Draw = new GraphicsPath();
//            imv_Theme = new ImageView();
//            imv_Theme.BorderColor = Color.Transparent;
//            imv_Theme.Location = new Point(10, 4);
//            imv_Theme.Size = new Size(32, 32);
//            imv_Theme.ImageSize = new Size(30, 30);
//            txv_Status = new TextView();
//            txv_Status.Font = new Font(Fonts.YAHEI, 11);
//            txv_Status.ForeColor = Color.FromArgb(62, 106, 170);
//            PrepareColor();
//            Init();
//        }
//        void Init()
//        {

//            this.BackColor = Color.White;
//            arcRect = new Rectangle(0, 0, Radius, Radius);
//            Pen_Border = new Pen(BorderColor, 2f);
//            Pen_Top_Underline = new Pen(Top_UnderlineColor, 2);
//            sldBrush_Main = new SolidBrush(MainPanelColor);
//            sldBrush_Text = new SolidBrush(FontColor);
//            txv_Status.Location = new Point(10, this.Height - 30);
//            txv_Status.Text = "就绪";

//            this.Controls.Add(txv_Status);
//            arcRect_Right = new Rectangle(0, 0, Radius, Radius);

//            this.Controls.Add(imv_Theme);
//            PrepareSwitcher();

//            PrepareColor();
//            PrepareSize();
//            FontX = new Font(Fonts.YAHEI, 12);
//            this.Resize += new EventHandler(it_Resize);
//        }
//        void PrepareSwitcher()
//        {

//            int z = 3;
//            FormSwitchers = new Switcher[3];
//            //close 2
//            //mini 0
//            //Max 1
//            for (int i = 0; i < 3; i++)
//            {
//                FormSwitchers[i] = new Switcher();
//                FormSwitchers[i].Switch_Feature = i;
//                FormSwitchers[i].EnableEx = true;
//                FormSwitchers[i].InnerColor = Color.FromArgb(21, 66, 139);
//                FormSwitchers[i].CircleColor = Color.Transparent;
//                FormSwitchers[i].Size = new Size(30, 30);
//                FormSwitchers[i].Location = new Point(this.ClientSize.Width - 10 - (z--) * 30, 0);
//                FormSwitchers[i].MouseEnterColor = Color.FromArgb(71, 116, 189);
//                FormSwitchers[i].MouseDownColor = Color.FromArgb(0, 46, 119);
//                FormSwitchers[i].MouseLeaveColor = Color.FromArgb(21, 66, 139);
//                this.Controls.Add(FormSwitchers[i]);
//            }
//            FormSwitchers[1].InnerPenSize = 1;
//            FormSwitchers[1].fOffset_X -= 1;
//            FormSwitchers[0].Click += new EventHandler(FormSwitcher_Mini_Click);
//            FormSwitchers[1].Click += new EventHandler(FormSwitcher_Max_Click);
//            FormSwitchers[2].Click += new EventHandler(FormSwitcher_Close_Click);


//        }
//        void PrepareSize()
//        {


//            if (this.WindowState != FormWindowState.Minimized)
//            {

//                Rect_Top_Up = new Rectangle(0, 0, this.Width, nRect_Top_Up_Height);
//                Rect_Top_Down = new Rectangle(0, nRect_Top_Up_Height, this.Width,
//                    nRect_Top_Height - nRect_Top_Up_Height);
//                Lgb_Top_Up = new LinearGradientBrush(Rect_Top_Up, Top_Up_Color_Start,
//                    Top_Up_Color_End, LinearGradientMode.Vertical);
//                Lgb_Top_Down = new LinearGradientBrush(Rect_Top_Down, Top_Down_Color_Start,
//                    Top_Down_Color_End, LinearGradientMode.Vertical);

//            }
//            GetRibbonRectPath();
//            this.Region = new Region(FormPath);
//        }
//        void PrepareColor()
//        {
//            // BorderColor = Color.FromArgb(59, 90, 130);
//            BorderColor = Color.DodgerBlue;
//            Top_Up_Color_Start = Color.FromArgb(226, 235, 248);
//            Top_Up_Color_End = Color.FromArgb(219, 233, 252);
//            Top_Down_Color_Start = Color.FromArgb(204, 223, 247);
//            Top_Down_Color_End = Color.FromArgb(224, 237, 252);
//            FontColor = Color.FromArgb(62, 106, 170);
//            Top_UnderlineColor = Color.FromArgb(220, 244, 254);
//            MainPanelColor = Color.FromArgb(194, 217, 247);
//        }
//        void GetRibbonRectPath()
//        {

//            //左上角
//            FormPath.AddArc(arcRect, 180, 90);
//            FormPath_Draw.AddArc(arcRect, 180, 90);
//            FormPath.AddLine(Radius, 0, this.ClientRectangle.Width - Radius, 0);
//            FormPath_Draw.AddLine(Radius, 0, this.ClientRectangle.Width - Radius, 0);
//            // 右上角
//            arcRect_Right.X = this.ClientRectangle.Right - Radius - 1;
//            FormPath.AddArc(arcRect_Right, 270, 90);
//            FormPath_Draw.AddArc(arcRect_Right, 270, 90);
//            FormPath.AddLine(this.ClientRectangle.Width, Radius, this.ClientRectangle.Width, this.ClientRectangle.Height);

//            FormPath.AddLine(this.ClientRectangle.Width, this.ClientRectangle.Height, 0, this.ClientRectangle.Height);

//            FormPath.AddLine(0, this.ClientRectangle.Height - Radius, 0, Radius);

//            FormPath_Draw.AddLine(this.ClientRectangle.Width - 1, Radius, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
//            FormPath_Draw.AddLine(this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1, 0, this.ClientRectangle.Height - 1);
//            FormPath_Draw.AddLine(0, this.ClientRectangle.Height - Radius, 0, Radius);
//        }

//        void it_Resize(object sender, EventArgs e)
//        {
//            FormPath.Reset();
//            FormPath_Draw.Reset();
//            int z = 3;
//            for (int i = 0; i < 3; i++)
//                FormSwitchers[i].Location = new Point(this.ClientSize.Width - 10 - (z--) * 30, 9);
//            PrepareSize();
//            txv_Status.Location = new Point(5, this.Height - txv_Status.Height);
//            this.Refresh();
//        }
//        protected override void OnDraw()
//        {

//            g.DrawRectangle(Pen_Top_Underline, 1, 1, this.ClientSize.Width - 2, this.ClientSize.Height - 2);
//            // e.Graphics.DrawRectangle(Pen_Top_Underline, 2, 2, it.ClientSize.Width - 4, it.ClientSize.Height - 4);
//            g.FillRectangle(Lgb_Top_Up, Rect_Top_Up);
//            g.FillRectangle(Lgb_Top_Down, Rect_Top_Down);
//            g.DrawLine(Pen_Top_Underline, 0, nRect_Top_Height, this.Width, nRect_Top_Height);
//            g.DrawPath(Pen_Border, FormPath_Draw);
//            g.DrawString(strCaption, FontX, sldBrush_Text, 50, 10);
//            base.OnDraw();
//        }

//        void FormSwitcher_Close_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }
//        void FormSwitcher_Max_Click(object sender, EventArgs e)
//        {
//            if (!isMax)
//            {
//                this.WindowState = FormWindowState.Maximized;
//                isMax = true;
//            }
//            else
//            {
//                this.WindowState = FormWindowState.Normal;
//                isMax = false;
//            }

//        }
//        void FormSwitcher_Mini_Click(object sender, EventArgs e)
//        {
//            this.WindowState = FormWindowState.Minimized;
//        }
//        public Font FontX
//        {
//            get
//            {
//                return this.TitleFont;
//            }

//            set
//            {
//                TitleFont = value;
//                Refresh();
//            }
//        }
//        [BrowsableAttribute(false)]
//        public override Font Font
//        {
//            get
//            {
//                return base.Font;
//            }

//            set
//            {
//                base.Font = value;
//            }
//        }
//        public override string Text
//        {
//            get
//            {
//                return strCaption;
//            }

//            set
//            {
//                strCaption = value;
//                Refresh();
//            }
//        }
//        protected override void WndProc(ref Message m)
//        {
//            if (!bEnableDialog)
//                switch (m.Msg)
//                {

//                    case 0x0084:
//                        base.WndProc(ref m);
//                        Point vPoint = new Point((int)m.LParam & 0xFFFF,
//                            (int)m.LParam >> 16 & 0xFFFF);
//                        vPoint = PointToClient(vPoint);
//                        if (vPoint.X <= 5)
//                            if (vPoint.Y <= 5)
//                                m.Result = (IntPtr)Guying_HTTOPLEFT;
//                            else if (vPoint.Y >= ClientSize.Height - 5)
//                                m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
//                            else m.Result = (IntPtr)Guying_HTLEFT;
//                        else if (vPoint.X >= ClientSize.Width - 5)
//                            if (vPoint.Y <= 5)
//                                m.Result = (IntPtr)Guying_HTTOPRIGHT;
//                            else if (vPoint.Y >= ClientSize.Height - 5)
//                                m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
//                            else m.Result = (IntPtr)Guying_HTRIGHT;
//                        else if (vPoint.Y <= 5)
//                            m.Result = (IntPtr)Guying_HTTOP;
//                        else if (vPoint.Y >= ClientSize.Height - 5)
//                            m.Result = (IntPtr)Guying_HTBOTTOM;
//                        break;
//                    case 0x0201:                //鼠标左键按下的消息   
//                        m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标   
//                        m.LParam = IntPtr.Zero; //默认值   
//                        m.WParam = new IntPtr(2);//鼠标放在标题栏内   
//                        base.WndProc(ref m);
//                        break;
//                    default:
//                        base.WndProc(ref m);
//                        break;
//                }
//            else
//                base.WndProc(ref m);
//        }
//        [Category("外观")]
//        public bool EnableDialog
//        {
//            get
//            {
//                return bEnableDialog;
//            }
//            set
//            {
//                bEnableDialog = value;
//                this.FormSwitchers[1].Enabled = false;
//            }
//        }
//    }
//    public struct DrageMoveArgs
//    {
//        public Control ctrlParent;
//        public int X, Y;
//        public int nSelfWidth;
//        public int nSelfHight;
//        public int nParentWidth;
//        public int nParentHeight;

//    }

//    public enum Orientations
//    {
//        Axis_X, Axis_Y, None
//    }

//    public class Wnd : Form
//    {
//        Point mPoint_Drag = new Point();
//        protected Graphics g;
//        private Widgets.RibButton ribButton1;
//        bool bModernUI = true;
//        public Wnd(bool bModernUI)
//        {

//            if (bModernUI)
//                PrepareModernUI();
//            Prepare();
//        }
//        public Wnd()
//        {
//        }
//        protected virtual void Prepare()
//        {

//            prepareData();
//            prepareAppearance();
//            prepareCtrls();
//            UnitCtrls();
//            prepareEvents();
//        }
//        protected void PrepareModernUI()
//        {
//            this.FormBorderStyle = FormBorderStyle.None;
//            this.StartPosition = FormStartPosition.CenterScreen;
//            this.MouseDown += this.Me_MouseDown;
//            this.MouseMove += this.Me_MouseMove;
//        }
//        public void InvokeForm()
//        {

//        }
//        public void AddControl(Control ctrl)
//        {
//            this.Controls.Add(ctrl);
//        }
//        protected virtual void prepareEvents() { }
//        protected virtual void prepareAppearance() { }
//        protected virtual void prepareData() { }
//        protected virtual void prepareCtrls() { }
//        protected virtual void OnDraw() { }
//        protected virtual void UnitCtrls() { }
//        protected override void OnPaint(PaintEventArgs e)
//        {

//            g = e.Graphics;
//            g.SmoothingMode = SmoothingMode.AntiAlias;
//            OnDraw();
//            base.OnPaint(e);
//        }
//        public void YaHeiFont(int nFontSize)
//        {
//            this.Font = new Font(Fonts.YAHEI, nFontSize);
//        }
//        protected void Me_MouseDown(object sender, MouseEventArgs e)
//        {
//            mPoint_Drag.X = e.X;
//            mPoint_Drag.Y = e.Y;
//        }
//        protected void Me_MouseMove(object sender, MouseEventArgs e)
//        {

//            if (e.Button == MouseButtons.Left)
//            {
//                Point myPosittion = MousePosition;
//                myPosittion.Offset(-mPoint_Drag.X, -mPoint_Drag.Y);
//                Location = myPosittion;
//            }
//        }


//    }

//    [ToolboxItem(false)]
//    public class View : UserControl
//    {
//        public delegate void DragCallback(ref DrageMoveArgs e);
//        public event DragCallback DragMove;
//        public delegate void DoTaskCallback();
//        public event DoTaskCallback DoTask;
//        protected SizeF szf_Text;
//        protected Graphics g, g_Text;
//        protected String strCaption;
//        protected SolidBrush sbrh_Text;
//        protected float x_Text;
//        protected float y_Text;
//        protected Font fnt_cmp;
//        protected DrageMoveArgs dma;
//        private Orientations enu_Orientation;
//        protected Point pnt_Modify;
//        protected GraphicsPath gp;
//        public View()
//        {
//            SetStyle(ControlStyles.UserPaint, true);
//            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
//            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
//            this.BackColor = Color.Transparent;
//            g_Text = this.CreateGraphics();
//            pnt_Modify = new Point();
//            Prepare();


//        }



//        protected virtual void Prepare()
//        {
//            prepareData();
//            prepareAppearance();
//            prepareCtrls();
//            prepareEvents();
//            CustomWork();
//            UnitCtrls();
//        }
//        protected virtual void CustomWork() { }
//        public void AddControl(Control ctrl)
//        {
//            this.Controls.Add(ctrl);
//        }
//        protected virtual void UnitCtrls() { }
//        protected virtual void prepareEvents() { }
//        protected virtual void prepareAppearance() { }
//        protected virtual void prepareData() { }
//        protected virtual void prepareCtrls() { }
//        protected virtual void OnDraw() { }
//        protected override void OnPaint(PaintEventArgs e)
//        {

//            g = e.Graphics;
//            g.SmoothingMode = SmoothingMode.AntiAlias;
//            OnDraw();
//            base.OnPaint(e);
//        }
//        protected override CreateParams CreateParams
//        {
//            get
//            {
//                CreateParams cp = base.CreateParams;
//                cp.ExStyle = 0x20;
//                return cp;
//            }
//        }
//        public void YaHeiFont(int nFontSize)
//        {
//            this.Font = new Font(Fonts.YAHEI, nFontSize);
//        }
//        public Font DengXianFont(int nFontSize)
//        {
//            return new Font(Fonts.DENGXIANG, nFontSize);
//        }
//        protected virtual void CenterText(SolidBrush slbh_Text)
//        {
//            slbh_Text.Color = this.ForeColor;
//            MessureText();

//            g.DrawString(Text, Font, slbh_Text,
//            x_Text, y_Text);
//            this.fnt_cmp = Font;
//        }
//        protected void MessureText()
//        {

//            szf_Text = g_Text.MeasureString(strCaption, Font);
//            x_Text = this.Width / 2 - szf_Text.Width / 2;
//            y_Text = this.Height / 2 - szf_Text.Height / 2;
//        }
//        protected Color PressEffect(Color _c, int nValue, bool bIsAdd)
//        {
//            int _Red = _c.R;
//            int _Green = _c.G;
//            int _Blue = _c.B;
//            if (bIsAdd)
//            {
//                if (_c.R + nValue >= 0 && _c.R + nValue < 255)
//                    _Red = _c.R + nValue;

//                if (_c.G + nValue >= 0 && _c.G + nValue < 255)

//                    _Green = _c.G + nValue;

//                if (_c.B + nValue >= 0 && _c.B + nValue < 255)
//                    _Blue = _c.B + nValue;
//            }
//            else
//            {
//                if (_c.R - nValue >= 0)
//                    _Red = _c.R - nValue;

//                if (_c.G - nValue >= 0)

//                    _Green = _c.G - nValue;

//                if (_c.B - nValue >= 0)
//                    _Blue = _c.B - nValue;
//            }

//            return Color.FromArgb(_Red, _Green, _c.B);
//        }
//        [BrowsableAttribute(false)]
//        public override RightToLeft RightToLeft
//        {
//            get
//            {
//                return base.RightToLeft;
//            }

//            set
//            {
//                base.RightToLeft = value;
//            }
//        }
//        [Category("外观")]
//        public Orientations Orientation
//        {
//            get
//            {
//                return enu_Orientation;
//            }

//            set
//            {
//                enu_Orientation = value;
//                Refresh();
//            }
//        }

//        public virtual void PrepareDragable()
//        {

//            this.MouseMove += this.Me_MouseMove;
//        }

//        protected virtual void HandleDragMove(DrageMoveArgs e)
//        {
//            this.DragMove(ref e);
//            pnt_Modify.X = e.X;
//            pnt_Modify.Y = e.Y;
//            this.Location = pnt_Modify;

//        }

//        void CollectDragMoveInfo(Point pnt)
//        {
//            dma.X = pnt.X;
//            dma.Y = pnt.Y;
//            dma.nParentWidth = Parent.Width;
//            dma.nParentHeight = Parent.Height;
//            dma.nSelfWidth = this.Width;
//            dma.nSelfHight = this.Height;
//        }
//        protected void Me_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
//        {
//            if (e.Button == System.Windows.Forms.MouseButtons.Left)
//            {
//                switch (enu_Orientation)
//                {
//                    case Orientations.Axis_X:
//                        pnt_Modify.X = Location.X + e.X;
//                        pnt_Modify.X -= this.Width / 2;
//                        pnt_Modify.Y = Location.Y;
//                        CollectDragMoveInfo(pnt_Modify);
//                        HandleDragMove(dma);
//                        if (DoTask != null)
//                            DoTask();
//                        break;
//                    case Orientations.Axis_Y:
//                        pnt_Modify.X = Location.X;
//                        pnt_Modify.Y = Location.Y + e.Y;
//                        pnt_Modify.Y -= this.Height / 2;
//                        CollectDragMoveInfo(pnt_Modify);
//                        HandleDragMove(dma);
//                        if (DoTask != null)
//                            DoTask();
//                        break;
//                    case Orientations.None:
//                        pnt_Modify.X = Location.X + e.X;
//                        pnt_Modify.Y = Location.Y + e.Y;
//                        CollectDragMoveInfo(pnt_Modify);
//                        HandleDragMove(dma);

//                        break;

//                }

//            }
//        }
//        public new bool Enabled
//        {
//            get
//            {
//                return base.Enabled;
//            }
//            set
//            {
//                base.Enabled = value;
//                Refresh();
//            }
//        }

//    }
//    [ToolboxItem(false)]
//    public class AeroView : Control
//    {
//        protected SolidBrush sldBk;
//        protected int nOpacity = 200;
//        protected Graphics g, g_Text;
//        protected SizeF szf_Text;
//        public AeroView()
//        {
//            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
//              ControlStyles.Opaque, true);

//            Prepare();
//        }
//        protected virtual void Prepare()
//        {
//            prepareData();
//            prepareAppearance();
//            prepareCtrls();
//            prepareEvents();
//            CustomWork();
//            UnitCtrls();
//        }
//        protected virtual void CustomWork() { }
//        public void AddControl(Control ctrl)
//        {
//            this.Controls.Add(ctrl);
//        }
//        protected virtual void UnitCtrls() { }
//        protected virtual void prepareEvents() { }
//        protected virtual void prepareAppearance()
//        {
//            this.Size = new Size(250, 50);
//            ForeColor = Color.White;
//        }
//        protected virtual void prepareData() { }
//        protected virtual void prepareCtrls() { }
//        protected virtual void OnDraw() { }
//        protected override void OnPaint(PaintEventArgs e)
//        {

//            g = e.Graphics;
//            g.SmoothingMode = SmoothingMode.AntiAlias;
//            if (sldBk == null)
//                sldBk = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
//            e.Graphics.FillRectangle(sldBk, 0, 0, this.Width, this.Height);
//            OnDraw();
//            base.OnPaint(e);
//        }
//        protected override CreateParams CreateParams
//        {
//            get
//            {
//                CreateParams cp = base.CreateParams;
//                cp.ExStyle = 0x20;
//                return cp;
//            }
//        }
//        [Category("外观")]
//        public int Opacity
//        {
//            get
//            {
//                return nOpacity;
//            }

//            set
//            {
//                nOpacity = value;
//                this.sldBk.Color = Color.FromArgb(value, FillColor);
//                Refresh();
//            }
//        }
//        [Category("外观")]
//        public Color FillColor
//        {
//            get
//            {
//                if (sldBk == null)
//                    sldBk = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
//                return sldBk.Color;
//            }
//            set
//            {
//                if (sldBk == null)
//                    sldBk = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
//                sldBk.Color = value;
//                Refresh();
//            }
//        }
//        [Browsable(false)]
//        public override Color BackColor
//        {
//            get
//            {
//                return base.BackColor;
//            }

//            set
//            {
//                base.BackColor = value;
//            }
//        }
//    }
//}
