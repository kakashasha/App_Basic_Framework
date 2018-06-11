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
    [ToolboxItem(false)]
    public class Triangle : UIElement
    {
        GraphicsPath gp;
        PointF[] pntArr;
        Region rgn;
        public Triangle()
        {
            DrawTriangle();
            Size = new Size(25, 48);
        }
        void DrawTriangle()
        {
            pntArr[0].X = 0;
            pntArr[0].Y = 0;
            pntArr[1].X = this.Width - 1;
            pntArr[1].Y = this.Height / 2;
            pntArr[2].X = 0;
            pntArr[2].Y = this.Height;
            if (gp.PointCount > 0)
                gp.Reset();
            gp.AddPolygon(pntArr);
            this.Region = new Region(gp);
            Refresh();
        }
        protected override void prepareEvents()
        {
            this.Resize += Triangle_Resize;
            base.prepareEvents();
        }

        private void Triangle_Resize(object sender, EventArgs e)
        {
            DrawTriangle();
        }

        protected override void prepareData()
        {
            gp = new GraphicsPath();
            rgn = new Region();
            pntArr = new PointF[3];
            for (int i = 0; i < pntArr.Length; i++)
            {
                pntArr[i] = new Point();
            }

            base.prepareData();
        }
        protected override void prepareAppearance()
        {
            this.BackColor = Color.Gray;
            base.prepareAppearance();

        }

    }
}
