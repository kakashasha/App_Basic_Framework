using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{

    [ToolboxItem(true)]
    public class Line : UIElement
    {
        private Pen pen_Line;
        private float lineThickness;
        bool benableArrow = false;
        private float fArrowWidth = 6;
        private float fArrowHeight = 4;
        private AdjustableArrowCap acc = null;
        private bool isNeedRedrawArrow = false;
        bool bRightArrow = true;
        bool bVertical = false;
      


        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.ForeColor = Color.DimGray;
            Size = new Size(150, 4);
            pen_Line = new Pen(ForeColor, 1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            pen_Line.Color = ForeColor;
            pen_Line.Width = lineThickness;
            if (EnableArrow)
            {
                if (acc == null || this.isNeedRedrawArrow)
                {
                    acc = new AdjustableArrowCap(ArrowHeight, ArrowWidth);
                    if (isNeedRedrawArrow)
                        isNeedRedrawArrow = false;
                    if (RightArrow)

                    {
                        pen_Line.StartCap = LineCap.Flat;
                        pen_Line.CustomEndCap = acc;
                    }
                    else
                    {

                        pen_Line.EndCap = LineCap.Flat;
                        pen_Line.CustomStartCap = acc;
                        // pen_Line.CustomEndCap = null;
                    }
                }
               
            }
            // pen_Line.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;//恢复实线  

            //pen_Line.EndCap = LineCap.Custom;
            //pen_Line.CustomEndCap = new CustomLineCap(,)

        if(!bVertical)
            g.DrawLine(pen_Line,0,this.Height/2,Width,Height/2);
        else
            g.DrawLine(pen_Line,this.Width/2,0,Width/2,Height );
        }
        [Category("外观")]
        public bool RightArrow
        {
            get { return bRightArrow; }
            set { bRightArrow = value;isNeedRedrawArrow = true; Invalidate(); }
        }
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; Invalidate(); }
        }
        [Category("外观")]
        public float LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; Invalidate(); }
        }
        [Category("外观")]
        public bool EnableArrow
        {
            get { return benableArrow; }
            set { benableArrow = value; this.Invalidate(); }
        }
        [Category("外观")]
        public float ArrowWidth
        {
            get { return fArrowWidth; }
            set { fArrowWidth = value; isNeedRedrawArrow = true; this.Invalidate(); }
        }
        [Category("外观")]
        public float ArrowHeight
        {
            get { return fArrowHeight; }
            set { fArrowHeight = value; isNeedRedrawArrow = true; this.Invalidate(); }
        }
        [Category("外观")]
        public bool Vertical
        {
            get { return bVertical; }
            set { bVertical = value; Invalidate();}
        }
    }
        
}
