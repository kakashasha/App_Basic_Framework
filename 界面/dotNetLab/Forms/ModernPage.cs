 
 
using dotNetLab.Widgets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Forms
{
    public class ModernPage : ModernWnd
    {

        private Triangle tng;
        private Panel pnl_Switchers;
        Switcher[] switchers;
        protected Point pnt_TitlePos;
       
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
        protected Switcher[] Switchers
        {
            get
            {

                return this.switchers;
            }
        }

        protected Triangle DecoratedTriangle
        {
            get
            {
                return this.tng;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            DrawBorder(g);
            DrawVerticalBar(g, this.Width - 7, 50, 6, 75);
            DrawText(g, pnt_TitlePos.X, pnt_TitlePos.Y);
            if (this.EnableDrawUpDownPattern)
            {
                this.DrawUpDownDecoratePatern(g, DrawUpDownPatternTimes);

            }

        }
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.pnt_TitlePos = new Point(60, 10);
        }
        protected override void prepareCtrls()
        {
            base.prepareCtrls();

            prepareCtrlBoxs();
            prepareDecorateTriangle();
        }
        protected override void UnitCtrls()
        {
            base.UnitCtrls();
            this.Controls.Add(pnl_Switchers);
            this.Controls.Add(this.tng);



        }
        private void prepareDecorateTriangle()
        {
            tng = new Triangle();
            tng.Location = new Point(2, 65);
            tng.BackColor = Color.MediumPurple;

        }
        protected override void prepareEvents()
        {
            base.prepareEvents();
            this.Resize += Activity_Resize;
            foreach (var i in switchers)
            {
                i.Click += Switch_Clicked;
            }
        }
        private void Activity_Resize(object sender, EventArgs e)
        {
            pnl_Switchers.Location =
                new Point(this.Width - 3 * switchers[0].Width - 2, 2);

            Refresh();
        }
        private void Switch_Clicked(object sender, EventArgs e)
        {
            Switcher sw = sender as Switcher;
            switch (sw.Switch_Feature)
            {
                case 0: this.WindowState = FormWindowState.Minimized; break;
                case 2: Close(); break;
                case 1:
                    if (!EnableDialog)
                    {
                        bool b = (bool)this.Tag;
                        b = !b;
                        this.Tag = b;
                        if (b)
                            this.WindowState = FormWindowState.Maximized;
                        else
                        {
                            this.WindowState = FormWindowState.Normal;
                        }
                    }
                    break;
            }
        }
        protected void prepareCtrlBoxs()
        {
            this.Tag = false;
            int nSize = 35;
            pnl_Switchers = new Panel();
            pnl_Switchers.Width = nSize * 3;
            pnl_Switchers.Height = nSize;
            pnl_Switchers.BackColor = Color.Transparent;
            switchers = new Switcher[3];
            for (int i = 0; i < 3; i++)
            {
                switchers[i] = new Switcher();
                switchers[i].CircleColor = Color.Transparent;
                switchers[i].Size = new Size(nSize, nSize);
                switchers[i].InnerColor = Color.DimGray;
                switchers[i].InnerPenSize = 1f;
                switchers[i].Location = new Point(i * nSize, 0);
            }
            switchers[0].InnerPenSize = 1.6f;
            switchers[2].InnerPenSize = 1.6f;
            switchers[0].Switch_Feature = 0;
            switchers[1].Switch_Feature = 1;
            switchers[1].Enabled = true;
            switchers[2].Switch_Feature = 2;
            switchers[2].MouseEnterColor = Color.Red;
            switchers[2].MouseDownColor = Color.FromArgb(150, Color.Red);
            pnl_Switchers.Controls.AddRange(switchers);
            pnl_Switchers.Location = new Point(this.Width - 3 * nSize - 5, 2);
        }
        
    }
}
