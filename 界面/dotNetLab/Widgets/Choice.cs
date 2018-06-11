using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing.Drawing2D ;
using System.Drawing ;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{
    public class Choice : RoundElement
    {
        protected GraphicsPath gp;
         bool bChecked = false;
        
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            if (gp == null)
            {
                try
                {
                    gp = this.DrawRoundRect(0, 0, this.Width - 1, Height - 1, Radius);
                }
                catch (System.Exception ex)
                {
                	
                }
                
            }
            if (sbr_FillRoundRect == null)
            {
                sbr_FillRoundRect = new SolidBrush(clr_Normal);
            }
            try
            {
                e.Graphics.FillPath(sbr_FillRoundRect, gp);
            }
            catch (System.Exception ex)
            {
            	
            }

            

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (gp != null)
            {
                gp.Dispose();
                gp = null;
            }

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (sbr_FillRoundRect == null)
                sbr_FillRoundRect = new SolidBrush(PressColor);
            sbr_FillRoundRect.Color = PressColor;
            // label1.ForeColor = Color.White;
            Control.ControlCollection arr = (this.Parent as Control).Controls;
            foreach (Control item in arr)
            {
                Choice myItem = (item as Choice);
                if (myItem != this)
                    myItem.LostFocusAppearance();
            }
           
        }

       

// Need To Provide Refresh
        public virtual void LostFocusAppearance()
        {
            if (sbr_FillRoundRect == null)
                sbr_FillRoundRect = new SolidBrush(NormalColor);
            sbr_FillRoundRect.Color = NormalColor;
            //label1.ForeColor = Color.DimGray;


        }

        protected virtual void AdaptHeight()
        {
            Graphics g = this.CreateGraphics();
            MessureText(g);
            /* if (szf_Text.Height > this.Height)
             {

                 this.Height = (int)szf_Text.Height + 4;
             }*/
            g.Dispose();
        }

        protected virtual void AdaptWidth()
        {
            Graphics g = this.CreateGraphics();
            MessureText(g);
            g.Dispose();

        }

        public override int Radius
        {
            get { return base.Radius; }
            set
            {
                base.Radius = value;
                if (gp != null)
                {
                    gp.Dispose();
                    gp = null;
                }

                Refresh();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Font Font
        {
            get { return base.Font; }

            set
            {
                //  label1.Font = value;
                base.Font = value;
                AdaptHeight();
                Refresh();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override String Text
        {
            get { return base.Text; }

            set
            {
                base.Text = value; 
                AdaptWidth();
                Refresh();
                
            }
        }

        public Color NormalColor
        {
            get { return clr_Normal; }
            set
            {
                clr_Normal = value;
                Refresh();
            }

        }

        public Color PressColor
        {
            get { return clr_Press; }
            set
            {
                clr_Press = value;
                Refresh();
            }
        }

        public bool Checked
        {
            get { return bChecked; }
            set { bChecked = value; }
        }
    }
}
