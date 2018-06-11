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
    [DefaultEvent("Click"), DefaultProperty("Text")]
    public class ModernCheckBox :UIElement
    {
          Color clr_Border = Color.DimGray;
          Color clr_Inner_Fill = Color.DodgerBlue;
        private Pen pen_Border = null ;
        private float borderThickness = 1;
        private SolidBrush sbr_Checked;
        private float penddingRatio = 1 / 8.0f;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawRectangle(pen_Border,0,0,this.Width-1,this.Height-1);
            g.FillRectangle(sbr_Checked,penddingRatio*Width,penddingRatio*Height,
                Width - penddingRatio*Width*2-1, Height - penddingRatio*Height*2-1);
        }

        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.Size = new Size(24,24);
            if(pen_Border==null)
                pen_Border = new Pen(clr_Border,BorderThickness);
            
            if (sbr_Checked == null)
                sbr_Checked = new SolidBrush(clr_Inner_Fill);
            
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Refresh();
            
        }

        [Category("外观")]
        public Color BorderColor
        {
            get { return clr_Border; }
            set
            {
                clr_Border = value; 
                if(pen_Border==null)
                    pen_Border = new Pen(clr_Border,BorderThickness);
                else
                {
                    pen_Border.Color = clr_Border;
                }
                Refresh(); 
            }
        }
        [Category("外观")]
        public Color CheckedColor
        {
            get { return clr_Inner_Fill; }
            set
            {
                clr_Inner_Fill = value;
                if (sbr_Checked == null)
                    sbr_Checked = new SolidBrush(clr_Inner_Fill);
                else
                {
                    sbr_Checked.Color = value;
                }
                Refresh();
            }
        }
        [Category("外观")]
        public float BorderThickness
        {
            get { return borderThickness; }
            set
            {
                borderThickness = value; 
                if(pen_Border==null)
                    pen_Border = new Pen(clr_Border,borderThickness);
                else
                {
                    pen_Border.Width = borderThickness;
                }
                Refresh();
            }
        }

        public float PenddingRatio
        {
            get { return penddingRatio; }
            set
            {
                penddingRatio = value;
                
                Refresh();
            }
        }
    }
}
