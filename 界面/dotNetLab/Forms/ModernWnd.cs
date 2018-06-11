using dotNetLab.Appearance;
using dotNetLab.Widgets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Forms
{
    public class ModernWnd : Wnd
    {
        Image img_Up, img_Down;
        Rectangle rct_Area_Toast;
         protected Toast tipper;
        protected Pen pen_Border;
        protected SolidBrush sldbrh_VerticalBar;
        protected string strCaption;
        protected SolidBrush sbrh_Title;
        protected Font TitleFont;
        const int Guying_HTLEFT = 10;
        const int Guying_HTRIGHT = 11;
        const int Guying_HTTOP = 12;
        const int Guying_HTTOPLEFT = 13;
        const int Guying_HTTOPRIGHT = 14;
        const int Guying_HTBOTTOM = 15;
        const int Guying_HTBOTTOMLEFT = 0x10;
        const int Guying_HTBOTTOMRIGHT = 17;
      protected  bool bEnableDrawUpDownPattern = false;
        private int nDrawUpDownPatternTimes = 2;

        bool bEnableDialog = false;
        public ModernWnd() : base(true)
        {

        }
        protected override void prepareEvents()
        {
            base.prepareEvents();
            this.Resize += ModernWnd_Resize;
          
            
        }
        protected void DrawDecoratedImage(Graphics g ,Image img, Point pnt)
        {

            g.DrawImage(img, pnt);
        }
        private void ModernWnd_Resize(object sender, EventArgs e)
        {
            GetTipperLocation();
            this.Refresh();
        }

        void GetTipperLocation()
        {
            tipper.Location = new Point(this.Width - tipper.Width - 3,
                this.Height - tipper.Height - 35);
            this.rct_Area_Toast.X = 0;
            this.rct_Area_Toast.Y = tipper.Location.Y - tipper.Width / 2;
            rct_Area_Toast.Width = this.Width;
            this.rct_Area_Toast.Height = tipper.Height * 2;
        }
        protected override void prepareCtrls()
        {
            base.prepareCtrls();

            tipper = new Toast();
            tipper.Text = "Toas Show";
            tipper.Visible = false;
            tipper.Width = 300;
             
            GetTipperLocation();
        }
          void AddTipper()
        {
            AddControl(this.tipper);
           

        }
        public void EnableToast()
        {
            AddTipper();
            this.tipper.BringToFront();
        }
        public void ReTip()
        {

            tipper.ReShow();
        }
        protected override void UnitCtrls()
        {
            base.UnitCtrls();


        }
        public void Tip(string strText)
        {
            tipper.Text = strText;
            CommonTip();
        }
        public void Tip(string strText, Image img)
        {
            tipper.Text = strText;
            tipper.Source = img;
            CommonTip();
        }

        public void Tip(string strText, int nInterval, Image img)
        {
            this.Text = strText;
            tipper.Source = img;
            CommonTip();
        }
 
        void CommonTip()
        {
            if(this.Controls.IndexOf(tipper)==-1)
                AddTipper();
            if (tipper.tmr.Enabled == true)
                tipper.Shut();
            GetTipperLocation();
            tipper.Visible = true;
            tipper.tmr.Start();
            tipper.BringToFront();
        }
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.BackColor = Color.White;
            pen_Border = new Pen(Color.DodgerBlue, 2);
            sldbrh_VerticalBar = new SolidBrush(Color.Red);
            this.Size = new Size(600, 500);
            TitleFont = new Font(Fonts.DENGXIANG, 35);
            sbrh_Title = new SolidBrush(Color.LightSeaGreen);

        }
        protected virtual void DrawUpDownDecoratePatern(Graphics g,int times)
        {
            for (int i = 0; i < times; i++)
            {
                if (Img_Up != null)
                {
                    g.DrawImage(img_Up, new Point(this.Width - img_Up.Width - 118,4));
                }
                if (Img_Down != null)
                {
                    g.DrawImage(Img_Down,  this.Width - Img_Down.Width - 7, Height - Img_Down.Height - 5 ,img_Down.Width,img_Down.Height);
                }
            }
            
        }
        protected override void OnPaint( PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            DrawBorder(g);
            DrawVerticalBar(g, this.Width - 6, 50, 6, 75);
          
            
        }
        protected virtual void DrawText(Graphics _g, float x, float y)
        {
            if(sbrh_Title == null)
            sbrh_Title = new SolidBrush(Color.LightSeaGreen);
            if(TitleFont == null)
                TitleFont = new Font(Fonts.DENGXIANG, 35);
            _g.DrawString(strCaption, TitleFont, sbrh_Title, x, y);
        }
        protected virtual void DrawBorder(Graphics _g)
        {
            if(pen_Border== null)
                pen_Border = new Pen(Color.DodgerBlue,2);
            _g.DrawRectangle(pen_Border, 1, 1, this.Width - 2, this.Height - 2);
        }
        protected virtual void DrawVerticalBar(Graphics _g, float x,
          float y, float width, float height)
        {
            if (sldbrh_VerticalBar == null)
                sldbrh_VerticalBar = new SolidBrush(Color.Red);
            _g.FillRectangle(sldbrh_VerticalBar, x, y, width, height);
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
        public bool EnableDrawUpDownPattern
        {
            get { return bEnableDrawUpDownPattern; }
            set
            {
                bEnableDrawUpDownPattern = value;
                Invalidate();
            }
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

        public virtual bool EnableDialog
        {
            get
            {
                return bEnableDialog;
            }
            set
            {
                bEnableDialog = value;

            }
        }

        public Image Img_Up
        {
            get
            {
                return img_Up;
            }

            set
            {
                if (img_Up != null)
                    img_Up.Dispose();
                img_Up = value;

            }
        }

        public Image Img_Down
        {
            get
            {
                return img_Down;
            }

            set
            {
                if (img_Down != null)
                    img_Down.Dispose();
                img_Down = value;
            }
        }
        public override Color ForeColor
        {
            get
            {

                return base.ForeColor;
            }

            set
            {
                base.ForeColor = value;
                if (this.sbrh_Title == null)
                    sbrh_Title = new SolidBrush(Color.Black);
                sbrh_Title.Color = value;
                Refresh();
            }
        }
        public string InfoTip
        {
            set
            {
                tipper.GetEmbeddedImages();
                Tip(value, tipper.img_Info);
            }
        }

        public string ErrorTip
        {
            set
            {
                tipper.GetEmbeddedImages();
                Tip(value, tipper.img_Error);
            }
        }
        public string DoneTip
        {
            set
            {
                tipper.GetEmbeddedImages();
                Tip(value, tipper.img_Done);
            }
        }
        public string UnderWorkingTip
        {
            set
            {
                tipper.GetEmbeddedImages();
                Tip(value, tipper.img_Underworking);
            }
        }

        public int DrawUpDownPatternTimes
        {
            get { return nDrawUpDownPatternTimes; }
            set { nDrawUpDownPatternTimes = value; Invalidate(); }
        }

        public class Toast : AeroView
        {

            MobilePictureBox imv_Close;
            Image img_TipperMark;
            string strCaption = "Toast";
            private SolidBrush sbrh_Text;
            public System.Timers.Timer tmr;
            public Image img_Info, img_Error, img_Done, img_Underworking;
            public delegate void ShutCallback();
            ShutCallback shut;
            public void GetEmbeddedImages()
            {
                if (Source != null)
                {

                    if (Source != null)
                    {
                        if (img_Info == Source)
                            img_Info = global::dotNetLab.UI.Info;
                        else if (img_Error == Source)
                            img_Error = global::dotNetLab.UI.Error;
                        else if (img_Done == Source)
                            img_Done = global::dotNetLab.UI.Done;
                        else if (img_Underworking == Source)
                            img_Underworking = global::dotNetLab.UI.Underworking;
                    }
                }
                else
                {
                    img_Error = global::dotNetLab.UI.Error;
                    img_Info = global::dotNetLab.UI.Info;
                    img_Done = global::dotNetLab.UI.Done;
                    img_Underworking = global::dotNetLab.UI.Underworking;

                }
            }
            protected override void prepareAppearance()
            {
                base.prepareAppearance();
                Font = new Font(Fonts.YAHEI, 12);
                GetEmbeddedImages();
            }
            protected override void UnitCtrls()
            {
                base.UnitCtrls();

                AddControl(imv_Close);


            }
            protected override void prepareCtrls()
            {
                base.prepareCtrls();

                imv_Close = new MobilePictureBox();

                imv_Close.Size = new Size(12, 12);
                imv_Close.Location = new Point(
                     this.Width - imv_Close.Size.Width, 0);
                imv_Close.ImageSize = new SizeF(10, 10);
                imv_Close.BorderColor = Color.Transparent;
                imv_Close.BackColor = Color.FromArgb(10, Color.Black);
                imv_Close.Source = global::dotNetLab.UI.Close_Small;
                tmr = new System.Timers.Timer();
                tmr.Interval = 4000;
                tmr.Elapsed += Tmr_Elapsed;
                shut = Shut;
            }
            protected override void prepareEvents()
            {
                base.prepareEvents();

                this.Resize += Toast_Resize;
                imv_Close.Click += Imv_Close_Click;
            }
            private void Imv_Close_Click(object sender, EventArgs e)
            {
                this.Shut();
            }
            private void Toast_Resize(object sender, EventArgs e)
            {
                imv_Close.Location = new Point(
                    this.Width - imv_Close.Size.Width - 3, 1);
                Refresh();
            }
            private void Tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                this.Invoke(shut);
            }
            public void ReShow()
            {

                if (tmr.Enabled == true)
                    Shut();
                this.Visible = true;
                this.tmr.Start();
            }
            public void Shut()
            {
                tmr.Stop();
                this.Visible = false;
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                Graphics xg = e.Graphics;
                if (Source != null)
                {
                    xg.DrawImage(Source, 10,
                        (this.Height - Source.Height) / 2, 32, 32
                        );
                    xg.DrawString(Text, Font, sbrh_Text,
                       Source.Width + 20,
                         (int)(this.Height - this.szf_Text.Height) / 2);
                }
            }
            public override Font Font
            {
                get
                {
                    return base.Font;
                }

                set
                {
                    base.Font = value;
                    AutoText();
                }
            }
            void AutoText()
            {
                szf_Text = this.CreateGraphics().MeasureString(strCaption, Font);
                this.Width = (int)(szf_Text.Width + 80);
                Refresh();
            }
            [Browsable(true)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public override String Text
            {
                get
                {
                    return strCaption;
                }

                set
                {

                    this.strCaption = value;
                    AutoText();

                }
            }
            public override Color ForeColor
            {
                get
                {
                    return base.ForeColor;
                }

                set
                {
                    if (sbrh_Text == null)
                        sbrh_Text = new SolidBrush(Color.White);
                    this.sbrh_Text.Color = value;
                    base.ForeColor = value;
                    Refresh();
                }
            }
            [Category("外观")]
            public Image Source
            {
                get
                {
                    return img_TipperMark;
                }
                set
                {
                    if (img_TipperMark != null)
                        img_TipperMark.Dispose();
                    img_TipperMark = value;
                    Refresh();
                }
            }
           


        }


    }

}
