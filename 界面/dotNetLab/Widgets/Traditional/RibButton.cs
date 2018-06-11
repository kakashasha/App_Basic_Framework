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
    [DefaultEvent("Click"), DefaultProperty("Text"), ToolboxBitmap(typeof(Button))]
    public class RibButton : TextBlock
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
        Size szImage;
        RectangleF Up_Rect;
        RectangleF Down_Rect;
        Pen Pen_Border;
        private Size imageSize;
        private int nGapBetweenTextImage = 8;
        LeftRightAlignment bIConAlignment = LeftRightAlignment.Left;
        Image imv_Icon = null;
        int nTextImgGap = 4;
  

        #endregion

        #region 消息处理
        void ButtonR_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            ButtonR_MouseEnter(sender, e);
        }

        void ButtonR_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //IsMouseDown = true;
            UpLinearGradientColors[0] = Color.FromArgb(255, 187, 101);
            UpLinearGradientColors[1] = Color.FromArgb(255, 172, 66);
            DownLinearGradientColors[0] = Color.FromArgb(251, 140, 60);
            DownLinearGradientColors[1] = Color.FromArgb(254, 207, 98);
            Uplgb.LinearColors = UpLinearGradientColors;
            Downlgb.LinearColors = DownLinearGradientColors;
            Refresh();
        }

        void ButtonR_MouseLeave(object sender, EventArgs e)
        {

            UpLinearGradientColors[0] = Up_StartColor;
            UpLinearGradientColors[1] = Up_EndColor;
            DownLinearGradientColors[0] = Down_StartColor;
            DownLinearGradientColors[1] = Down_EndColor;
            Pen_Border.Color = Color.FromArgb(139, 174, 218);
            Uplgb.LinearColors = UpLinearGradientColors;
            Downlgb.LinearColors = DownLinearGradientColors;

            Refresh();
        }

        void ButtonR_MouseEnter(object sender, EventArgs e)
        {


            UpLinearGradientColors[0] = Color.FromArgb(255, 254, 237);
            UpLinearGradientColors[1] = Color.FromArgb(255, 237, 188);
            DownLinearGradientColors[0] = Color.FromArgb(255, 218, 120);
            DownLinearGradientColors[1] = Color.FromArgb(255, 232, 167);
            Pen_Border.Color = Color.FromArgb(197, 176, 128);
            Uplgb.LinearColors = UpLinearGradientColors;
            Downlgb.LinearColors = DownLinearGradientColors;
            Refresh();

            //    Uplgb = new LinearGradientBrush(Up_Rect, Up_StartColor, Up_EndColor, LinearGradientMode.Vertical);
            //    Downlgb = new LinearGradientBrush(Down_Rect, Down_StartColor, Down_EndColor, LinearGradientMode.Vertical);
        }
        void ButtonR_Resize(object sender, EventArgs e)
        {
            if (this.Height > 0)
            {
                if (this.Height == 30)
                    this.Height += 1;
                GetPaintFramework();
            }


        }
        #endregion
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            UpLinearGradientColors = new Color[2];
            DownLinearGradientColors = new Color[2];
            this.Width = 100;
            this.Height = 31;
            this.ImageSize = new Size(24, 24);
            Up_StartColor = Color.FromArgb(224, 237, 255);
            Up_EndColor = Color.FromArgb(196, 221, 255);
            Down_StartColor = Color.FromArgb(173, 209, 255);
            Down_EndColor = Color.FromArgb(191, 218, 255);
            BorderColor = Color.FromArgb(139, 174, 218);
            Pen_Border = new Pen(BorderColor, 1);
            ForeColor = Color.FromArgb(21, 66, 139);
            this.bEnableResize = true;
            Up_Rect = new RectangleF();
            Down_Rect = new RectangleF();

            GetPaintFramework();
            this.Font = new Font(Fonts.YAHEI, 11);
        }
        protected override void prepareEvents()
        {
            base.prepareEvents();
            PrepareEvent();
        }

        void GetPaintFramework()
        {

            Up_Rect.Width = this.ClientRectangle.Width;
            Up_Rect.Height = this.ClientRectangle.Height * fSplit;
            Down_Rect.X = 0;
            Down_Rect.Y = Up_Rect.Height;


            Down_Rect.Width = Width;
            Down_Rect.Height = Height * (1 - fSplit);

            Uplgb = new LinearGradientBrush(Up_Rect, Up_StartColor, Up_EndColor, LinearGradientMode.Vertical);
            Downlgb = new LinearGradientBrush(Down_Rect, Down_StartColor, Down_EndColor, LinearGradientMode.Vertical);

        }
        void PrepareEvent()
        {

            this.Resize += new EventHandler(ButtonR_Resize);
            this.MouseEnter += new EventHandler(ButtonR_MouseEnter);
            this.MouseLeave += new EventHandler(ButtonR_MouseLeave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(ButtonR_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(ButtonR_MouseUp);


        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (!Enabled)
            {
                //Pen_Border.Color = Color.Gray;
                //ForeColor = Color.Gray;
                //this.BackColor = Color.LightGray;
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
                //if (Img != null)
                //    g.DrawImage(img, (this.Width - szf_Text.Width) / 2 - nTextImgGap,
                //        this.Height / 2 - ImageSize.Height / 2, ImageSize.Width, ImageSize.Height);
                this.sbrh_Text.Color = this.ForeColor;

                MessureText(e.Graphics);


                if (this.Source != null)
                {
                    if (IConAlignment == LeftRightAlignment.Left)
                    {
                        e.Graphics.DrawString(Text, Font, sbrh_Text, x_Text + GapBetweenTextImage, y_Text);

                        e.Graphics.DrawImage(this.Source, x_Text - GapBetweenTextImage,
                            (Height - this.Source.Height) / 2);
                    }
                    else
                    {
                        e.Graphics.DrawString(Text, Font, sbrh_Text, x_Text - 2 * GapBetweenTextImage, y_Text);
                        e.Graphics.DrawImage(this.Source, x_Text + szf_Text.Width + GapBetweenTextImage,
                            (Height - this.Source.Height) / 2);
                    }
                }
                else
                {
                    e.Graphics.DrawString(Text, Font, sbrh_Text, x_Text, y_Text);
                }

            }

           // base.OnPaint(e);

        }
        [Category("外观")]
        public Image Source
        {
            get { return imv_Icon; }
            set
            {

                if (imv_Icon != null)
                    imv_Icon.Dispose();
                imv_Icon = value;
            }
        }
        [Category("外观")]
        public Size ImageSize
        {
            get { return imageSize; }
            set
            {
                imageSize = value;
                Refresh();
            }
        }
        [Category("外观")]
        public int GapBetweenTextImage
        {
            get { return nGapBetweenTextImage; }
            set
            {
                nGapBetweenTextImage = value;
                Refresh();

            }
        }
        [Category("外观")]
        public LeftRightAlignment IConAlignment
        {
            get { return bIConAlignment; }
            set
            {
                bIConAlignment = value;
                Refresh();
            }
        }

    }
}
