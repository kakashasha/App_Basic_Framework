
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{
    [ToolboxItem(false)]
    public class Perl :RoundElement
    {
           private SolidBrush sbr_Bottom = null;
      private SolidBrush sbr_Ring = null;
      private SolidBrush sbr_Center = null;
      protected int nInnerSize = 30;
        private SolidBrush sbr_RayReflect = null;
     
      private Region rgn = null;

      protected override void prepareAppearance()
      {
          base.prepareAppearance();
          sbr_Ring = new SolidBrush(Color.DodgerBlue);
          sbr_Center = new SolidBrush(Color.DodgerBlue);
          sbr_Bottom = new SolidBrush(Color.Silver);
          sbr_RayReflect = new SolidBrush(Color.White);
          this.Size = new Size(20,20);
            
            
          CenterCircleSize = 15;
          
      }

      protected override void OnResize(EventArgs e)
      {
          base.OnResize(e);
          if (gp != null)
          {
              gp.Dispose();
              rgn.Dispose();
              rgn = null;
              gp = null;
          }
      }

      protected override void OnPaint(PaintEventArgs e)
      {
          if (gp == null)
          {
              gp = new GraphicsPath();
              gp.AddEllipse(0,0,Width-1,Height -1);
              rgn = new Region(gp);
          }
     
          e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
          Graphics g =e.Graphics;
          
          g.FillEllipse(sbr_Bottom,0,0,Width-1,Height -1 );
          e.Graphics.FillEllipse(sbr_Ring,2 ,2,Width-5,Height-5);
          e.Graphics.FillEllipse(sbr_Center,this.Width/2 - nInnerSize/2  ,Height /2 - nInnerSize/2 ,nInnerSize-1,nInnerSize-1);
          e.Graphics.FillEllipse(sbr_RayReflect,this.Width/2  + nInnerSize/8  ,Height /2 - nInnerSize/2+ nInnerSize/8+2,nInnerSize/3 -2,nInnerSize/3 -2);
         /* if(sbrh_Text == null)
              sbrh_Text = new SolidBrush(ForeColor);
          Text = String.Format("{0}%", nValue);
          CenterText(g,sbrh_Text);*/
        }
      [Category("外观")]
      public Color RingColor
      {
          get { return sbr_Ring.Color; }
          set { sbr_Ring.Color = value; Invalidate();}
      }
      [Category("外观")]
      public Color CenterColor
      {
          get { return sbr_Center.Color; }
          set { sbr_Center.Color = value;  Invalidate(); }
      }
      [Category("外观")]
      public int CenterCircleSize
      {
          get { return nInnerSize; }
          set { nInnerSize = value;this.Invalidate(); }
      }
    

      
      [Category("外观")]
      public Color BottomColor
      {
          get { return sbr_Bottom.Color; }
          set { sbr_Bottom.Color = value; Invalidate(rgn); }
      }
        [Category("外观")]
        public Color RayReflectColor
        {
            get { return sbr_RayReflect.Color; }
            set { sbr_RayReflect.Color = value; Invalidate(rgn); }
        }
    }
}
