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
    public class AeroView : Control
    {
        protected SolidBrush sldBk;
        protected int nOpacity = 200;
        protected Graphics g, g_Text;
        protected SizeF szf_Text;
        public AeroView()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
              ControlStyles.Opaque, true);

            Prepare();
        }
        protected virtual void Prepare()
        {
            prepareData();
            prepareAppearance();
            prepareCtrls();
            prepareEvents();
            CustomWork();
            UnitCtrls();
        }
        protected virtual void CustomWork() { }
        public void AddControl(Control ctrl)
        {
            this.Controls.Add(ctrl);
        }
        protected virtual void UnitCtrls() { }
        protected virtual void prepareEvents() { }
        protected virtual void prepareAppearance()
        {
            this.Size = new Size(250, 50);
            ForeColor = Color.White;
        }
        protected virtual void prepareData() { }
        protected virtual void prepareCtrls() { }
   
        protected override void OnPaint(PaintEventArgs e)
        {
            
            g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (sldBk == null)
                sldBk = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
            e.Graphics.FillRectangle(sldBk, 0, 0, this.Width, this.Height);
             
           
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x20;
                return cp;
            }
        }
        [Category("外观")]
        public int Opacity
        {
            get
            {
                return nOpacity;
            }

            set
            {
                nOpacity = value;
                this.sldBk.Color = Color.FromArgb(value, FillColor);
                Refresh();
            }
        }
        [Category("外观")]
        public Color FillColor
        {
            get
            {
                if (sldBk == null)
                    sldBk = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
                return sldBk.Color;
            }
            set
            {
                if (sldBk == null)
                    sldBk = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
                sldBk.Color = value;
                Refresh();
            }
        }
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
            }
        }
    }


 
}
