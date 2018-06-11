using dotNetLab.Appearance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{

    public class Notification : AeroView
    {
        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;
        int nOpacityValue = 200;
        MobilePictureBox imv_Close;
        Image img_TipperMark;
        string strCaption = "Notification";
        private SolidBrush sbrh_Text;
        public System.Timers.Timer tmr;
        public Image img_Info, img_Error, img_Done, img_Underworking;
        public delegate void ShutCallback();
        ShutCallback shut;
        bool bIsNeedResize = false;
        private SolidBrush sbr_Background = null;
        Color clr_Background = Color.Black;
        private GraphicsPath gp = null ;
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
            PrepareBufferDraw();
        }

        private void PrepareBufferDraw()
        {

            // Retrieves the BufferedGraphicsContext for the 
            // current application domain.
            context = BufferedGraphicsManager.Current;
        }

        protected override void UnitCtrls()
        {
            base.UnitCtrls();

           // AddControl(imv_Close);


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
            //imv_Close.Location = new Point(
            //    this.Width - imv_Close.Size.Width - 3, 1);
            bIsNeedResize = true;
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
        public   GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics xg = e.Graphics;
            
            if (bIsNeedResize)
            {
                if(grafx!=null)
                grafx.Dispose();
                context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
                grafx = context.Allocate(xg,
                     new Rectangle(0, 0, this.Width, this.Height));
            }
            
           // grafx.Graphics.FillPath(sbr_Background,);
            if (Source != null)
            {
               
                grafx.Graphics.DrawImage(Source, 10,
                    (this.Height - Source.Height) / 2, 32, 32
                    );
                grafx.Graphics.DrawString(Text, Font, sbrh_Text,
                   Source.Width + 20,
                     (int)(this.Height - this.szf_Text.Height) / 2);
            }
            grafx.Graphics.DrawString(Text, Font, sbrh_Text,
                  Source.Width + 20,
                    (int)(this.Height - this.szf_Text.Height) / 2);
            grafx.Render();
        }
        
        [Browsable(false)]
        public override Color BackColor { get { return base.BackColor; } set { base.BackColor = value; Invalidate(); } }
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

        public Color Background
        {
            get { return clr_Background; }
            set { clr_Background = value; Invalidate();}
        }

        public int OpacityValue
        {
            get { return nOpacityValue; }
            set { nOpacityValue = value; this.Invalidate(); }
        }
    }
}
