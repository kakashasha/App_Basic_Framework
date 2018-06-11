using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using dotNetLab.Widgets.Design;

namespace dotNetLab.Widgets
{
  [ToolboxItem(true),Designer(typeof(RingProgressDesigner)), ToolboxBitmap(typeof(ProgressBar))]
   public class RingProgress :UIElement
  {
        private SolidBrush sbr_Bottom = null;
      private SolidBrush sbr_Ring = null;
      private SolidBrush sbr_Center = null;
      protected int nInnerSize = 30;
      private float nValue = 100;
      private GraphicsPath gp = null;
      private Region rgn = null;
        

      protected override void prepareAppearance()
      {
          base.prepareAppearance();
          sbr_Ring = new SolidBrush(Color.DodgerBlue);
          sbr_Center = new SolidBrush(Color.White);
          sbr_Bottom = new SolidBrush(Color.Silver);
          this.Size = new Size(100,100);
            ForeColor = Color.DimGray;
            Value = 70;
          nInnerSize = 80;
          Font = new Font("微软雅黑",12,FontStyle.Bold);
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
          e.Graphics.FillPie(sbr_Ring,0,0,Width-1,Height -1,-90,360* Value/100);
          e.Graphics.FillEllipse(sbr_Center,this.Width/2 - nInnerSize/2 ,Height /2 - nInnerSize/2,nInnerSize-1,nInnerSize-1);
          if(sbrh_Text == null)
              sbrh_Text = new SolidBrush(ForeColor);
          Text = String.Format("{0}%", nValue);
          CenterText(g,sbrh_Text);
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
      //[Category("外观") ]
      //public int CenterCircleSize
      //{
      //    get { return nInnerSize; }
      //    set { nInnerSize = value;this.Invalidate(); }
      //}
         [Category("外观")]
        public int RingThickness
        {
            get { return Height / 2 - this.nInnerSize/2; }
            set
            {
                
                nInnerSize  = (Height / 2 - value) * 2;
                Invalidate();
            }
        }
 
        [Category("外观")]
      public float Value
      {
          get { return nValue; }
          set { nValue = value;this.Invalidate(rgn); }
      }

      public override Color ForeColor
      {
          get { return base.ForeColor; }
          set { base.ForeColor = value;
              if (sbrh_Text == null)
                  sbrh_Text = new SolidBrush(Color.CornflowerBlue);
              this.sbrh_Text.Color = value; this.Invalidate(); }
      }
      [Category("外观")]
      public Color BottomColor
      {
          get { return sbr_Bottom.Color; }
          set { sbr_Bottom.Color = value; Invalidate(rgn); }
      }
  }
// CenterColor RingThickness Value ForeColor BottomColor
    internal class RingProgressDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(RingProgressDesignerActionList);
        }
    }

    internal class RingProgressDesignerActionList :VisualActionList
    {
        private RingProgress rp;
        public RingProgressDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            rp = TargetControl as RingProgress;
        }


        //  
        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            DIC.Add(new DesignerActionHeaderItem("外观"));  
            DIC.Add(new DesignerActionPropertyItem("RingColor", "圆环颜色", "外观"));
            DIC.Add(new DesignerActionPropertyItem("CenterColor", "圆环中心区域颜色", "外观"));
            AddDesignerActionPropertyItem("Font", "字体");
            DIC.Add(new DesignerActionPropertyItem("ForeColor", "字体颜色", "外观"));
            DIC.Add(new DesignerActionPropertyItem("RingThickness", "圆环厚度", "外观"));
            DIC.Add(new DesignerActionPropertyItem("Value", "进度", "外观"));
            DIC.Add(new DesignerActionPropertyItem("BottomColor", "未完成进度颜色", "外观"));
            
        }
        public Font Font
        {
            get { return rp.Font; }
            set { AssignPropertyValue("Font", value); }
        }
        public Color RingColor
        {
            get { return rp.RingColor; }
            set{ AssignPropertyValue("RingColor",value);}
        }
        public Color CenterColor
        {
            get { return rp.CenterColor; }
            set{ AssignPropertyValue("CenterColor",value);}
        }
        public int RingThickness
        {
            get { return rp.RingThickness; }
            set{ AssignPropertyValue("RingThickness",value);}
        }
        public float Value
        {
            get { return rp.Value; }
            set{ AssignPropertyValue("Value",value);}
        }
        public Color ForeColor
        {
            get { return rp.ForeColor; }
            set{ AssignPropertyValue("ForeColor",value);}
        }
        public Color BottomColor
        {
            get { return rp.BottomColor; }
            set{ AssignPropertyValue("BottomColor",value);}
        }
    }
}
