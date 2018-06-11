using dotNetLab.Appearance;
using dotNetLab.Widgets;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;


namespace dotNetLab.Forms
{
    public class Session : ModernWnd
    {
        Revert rvt_Back;
        Point pnt_TitlePos;
        [Category("外观")]
        public Point TitlePos
        {
            get
            {
                return pnt_TitlePos;
            }

            set
            {
                pnt_TitlePos = value;
                Refresh();
            }
        }
        protected override void prepareData()
        {
            pnt_TitlePos = new Point(80, 10);
            base.prepareData();
        }
        protected override void DrawUpDownDecoratePatern(Graphics g, int times)
        {
            for (int i = 0; i < times; i++)
            {
                if (Img_Up != null)
                {
                    g.DrawImage(Img_Up, new Point(this.Width - Img_Up.Width - 7, 4));
                }
                if (Img_Down != null)
                {
                    g.DrawImage(Img_Down, this.Width - Img_Down.Width - 7, Height - Img_Down.Height - 5, Img_Down.Width, Img_Down.Height);
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            DrawBorder(g);
            DrawVerticalBar(g, this.Width-6-2, 80, 6, 75);
            DrawText(g, pnt_TitlePos.X, pnt_TitlePos.Y);
            DrawUpDownDecoratePatern(g,DrawUpDownPatternTimes);
            DrawUpDownDecoratePatern(g,DrawUpDownPatternTimes);
        }
        protected override void prepareCtrls()
        {
            base.prepareCtrls();
          
            rvt_Back = new Revert();
            rvt_Back.Size = new System.Drawing.Size(45, 45);
            rvt_Back.Location = new Point(10, 10);

        }
        protected override void prepareEvents()
        {
            base.prepareEvents();
            rvt_Back.Click += Rvt_Back_Click;
        }
        protected override void UnitCtrls()
        {
            base.UnitCtrls();
            this.AddControl(rvt_Back);


        }
        private void Rvt_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            TitleFont = new Font(Fonts.DENGXIANG, 30);
        }
       
       
    }
    class Revert : UIElement
    {
        protected PointF[] pnt_Arr;
        Pen Pen_Draw;
        Color clrDraw, _MouseEnterColor,
            _MouseLeaveColor, _MouseDownColor;
        float PatternWidth;
        Point PatternStart;
        float fOffset = 4;
        SolidBrush sldInnerPattern;
        [Browsable(false)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                base.ForeColor = value;

            }
        }
        [BrowsableAttribute(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Color MouseDownColor
        {
            get { return _MouseDownColor; }
            set
            {
                _MouseDownColor = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Color MouseLeaveColor
        {
            get { return _MouseLeaveColor; }
            set
            {
                _MouseLeaveColor = value;
                clrDraw = value;
                Pen_Draw.Color = value;
                sldInnerPattern.Color = value;
                Refresh();

            }
        }
        [Category("外观")]
        public Color MouseEnterColor
        {
            get { return _MouseEnterColor; }
            set
            {
                _MouseEnterColor = value;
                Refresh();
            }
        }
        [Category("外观")]
        public float Offset
        {
            get { return fOffset; }
            set
            {
                fOffset = value;
                Refresh();
            }
        }
        protected override void Prepare()
        {
            base.Prepare();
            prepareAppearance();
            prepareData();
        }
        protected override void prepareData()
        {
            base.prepareData();
            pnt_Arr = new PointF[10];
            PatternStart = new Point(2, 2);
            InitPointArray((ClientRectangle.Width - 2 * PatternStart.X - 2 * PatternWidth) / 2.0f + fOffset);
        }
        protected override void prepareAppearance()
        {
            this.BackColor = Color.Transparent;
            _MouseDownColor = _MouseEnterColor = Color.DimGray;

            clrDraw = Color.Gray;
            _MouseLeaveColor = clrDraw;
            sldInnerPattern = new SolidBrush(Color.Gray);
            this.Size = new Size(40, 40);
            Pen_Draw = new Pen(clrDraw, PatternWidth);

            PatternWidth = 2;
            base.prepareAppearance();

        }

        protected override void prepareEvents()
        {
            this.Resize += new EventHandler(BackTap_Resize);
            this.MouseEnter += new EventHandler(BackTap_MouseEnter);
            this.MouseLeave += new EventHandler(BackTap_MouseLeave);
            this.MouseDown += BackTap_MouseDown;
            base.prepareEvents();
        }

        void BackTap_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Pen_Draw.Color = _MouseDownColor;
            sldInnerPattern.Color = _MouseDownColor;
            Refresh();
        }

        void BackTap_MouseLeave(object sender, EventArgs e)
        {
            Pen_Draw.Color = _MouseLeaveColor;
            sldInnerPattern.Color = _MouseLeaveColor;
            Refresh();
        }

        void BackTap_MouseEnter(object sender, EventArgs e)
        {

            Pen_Draw.Color = _MouseEnterColor;
            sldInnerPattern.Color = _MouseEnterColor;
            Refresh();
        }

        void BackTap_Resize(object sender, EventArgs e)
        {
            InitPointArray((ClientRectangle.Width - 2 * PatternStart.X - 2 * PatternWidth) / 2.0f + fOffset);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawEllipse(Pen_Draw, PatternStart.X, PatternStart.Y,
                ClientRectangle.Width - 2 * PatternStart.X,
                ClientRectangle.Height - 2 * PatternStart.Y);
            g.FillPolygon(sldInnerPattern, pnt_Arr);

        }
        protected void InitPointArray(float r)
        {
            int i = 0;
            pnt_Arr[i++] = new PointF(1.0f / 3 * r, r);
            pnt_Arr[i++] = new PointF((5.0f / 6) * r, 4.0f / 6 * r);
            pnt_Arr[i++] = new PointF(r, 2.0f / 3 * r);
            pnt_Arr[i++] = new PointF((2.5f + 2) / 6.0f * r, 11.0f / 12 * r);
            pnt_Arr[i++] = new PointF(2 * r - (1 / 3.0f) * r, 11.0f / 12 * r);
            pnt_Arr[i++] = new PointF(2 * r - (1 / 3.0f) * r, 11.0f / 12 * r + 1 / 6.0f * r);
            pnt_Arr[i++] = new PointF((2.5f + 2) / 6.0f * r, 13.0f / 12 * r);
            pnt_Arr[i++] = new PointF(r, 2 * r - 4.0f / 6 * r);
            pnt_Arr[i++] = new PointF(5.0f / 6 * r, 2.0f * r - 4 / 6.0f * r);
            pnt_Arr[i++] = new PointF((1 / 3.0f) * r, r);

        }


    }
}
