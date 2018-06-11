using dotNetLab.Widgets.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{
    [ToolboxItem(true), Designer(typeof(MobileProgressBarDesigner)), ToolboxBitmap(typeof(ProgressBar))]
    public class MobileProgressBar : UIElement
    {
        public enum ProgressBarStyles
        {
            Ring,Line,Steps
        }
        ProgressBarStyles progressBarStyle = ProgressBarStyles.Line;
        private SolidBrush sbr_Bottom = null;
        private SolidBrush sbr_Progress = null;
        Alignments enm_Alignment = Alignments.Left;
        private StringBuilder sb;
        Color clrProgress = Color.DodgerBlue;
        int nRingThickness = 8;
        private float nValue = 100;
        public  Color clrSplitLineColor = Color.White;
        public  float  fSplitLineThickness = 1f;
        public int nEqualDivideParts = 5;
        private SolidBrush sbr_Ring = null;
        private SolidBrush sbr_Center = null;
        protected int nInnerSize = 30;
        protected Pen pen_Split = null;
        private GraphicsPath gp = null;
        private Region rgn = null;
        readonly float PI = 3.141592654F;
        float nCurrentStep = 5;
      

        protected override void prepareAppearance()
        {
            base.prepareAppearance();


            sbr_Bottom = new SolidBrush(Color.Silver);
            sbr_Progress = new SolidBrush(Color.DodgerBlue);
            this.Size = new Size(100, 20);
            ForeColor = Color.DarkCyan;
             
            // pen_Border = new Pen(Color.Silver, fBorderThickness);
            Font = new Font("微软雅黑", 11, FontStyle.Bold);

            sbr_Ring = new SolidBrush(Color.DodgerBlue);
            sbr_Center = new SolidBrush(Color.White);
          
            ForeColor = Color.DimGray;
            Value = 70;
            nInnerSize = 80;
            pen_Split = new Pen(SplitLineColor, SplitLineThickness);
            
        }
        protected override void OnResize(EventArgs e)
        {
           
            if (gp != null)
            {
                gp.Dispose();
                rgn.Dispose();
                rgn = null;
                gp = null;
            }
        }
        
      void LineProgressBar(Graphics g)
        {
            if (sbr_Progress == null)
                sbr_Progress = new SolidBrush(Color.DodgerBlue);
            sbr_Progress.Color = ProgressColor;
            g.FillRectangle(sbr_Bottom,0,0,Width-1,Height -1 );

          if(sbrh_Text == null)
              sbrh_Text = new SolidBrush(ForeColor);
            if (Value >= 43 )
              
                sbrh_Text.Color = Color.White;
            else
                sbrh_Text.Color = ForeColor;

                Text = String.Format("{0}%", nValue);
          if (Alignment == Alignments.Left)
          {
                
              g.FillRectangle(sbr_Progress,1 ,1,(Width -3)*Value/100,Height-3);
              CenterText(g, sbrh_Text);

    }
          else
          {
              MessureText(g);
              //this.Size = new Size((int)(szf_Text.Height+4),this.Height);
              if(sb== null)
                  sb = new StringBuilder();
              else
              {
                  sb.Remove(0, sb.Length);
              }


sb.AppendFormat("{0}\r\n % ", Value);
            
              g.FillRectangle(sbr_Progress,1 ,Height-(Height-3)* Value/100-2, Width -3 ,(Height-3)* Value/100  );
             g.DrawString(sb.ToString(),Font,sbrh_Text,this.Width/2-szf_Text.Height/2-1, Height/2-szf_Text.Width/2);
}}

        void RingProgressBar(Graphics g)
        {
            if (gp == null)
            {
                gp = new GraphicsPath();
                gp.AddEllipse(0, 0, Width - 1, Height - 1);
                rgn = new Region(gp);
            }
            nInnerSize = (Height / 2 - nRingThickness) * 2;
            g.FillEllipse(sbr_Bottom, 0, 0, Width - 1, Height - 1);
           g.FillPie(sbr_Ring, 0, 0, Width - 1, Height - 1, -90, 360 * Value / 100);
            if(ProgressBarStyle == ProgressBarStyles.Steps)
            {
                if(pen_Split == null)
                {
                    pen_Split = new Pen(SplitLineColor);
                }
                pen_Split.Color = SplitLineColor;
                pen_Split.Width = SplitLineThickness;
                int n = (int)(360.0f / EqualDivideParts);
                int radius = this.Width / 2;
                int centerX = this.Width / 2;
                int centerY = this.Height / 2;
                int nTimes = 0;
               
                for (int i = 0; i < EqualDivideParts; i++)
                {

                  
                    nTimes = i * n-90;
                    int x = (int)(centerX + radius * Math.Cos( nTimes* PI / 180));
                    int y = (int)(centerY + radius * Math.Sin(nTimes * PI / 180));
                    g.DrawLine(pen_Split, this.Width / 2, this.Height / 2, x, y);
                }
                
            }

            g.FillEllipse(sbr_Center, this.Width / 2 - nInnerSize / 2, Height / 2 - nInnerSize / 2, nInnerSize - 1, nInnerSize - 1);
            if (sbrh_Text == null)
                sbrh_Text = new SolidBrush(ForeColor);
            if (ProgressBarStyle == ProgressBarStyles.Steps)
            {
                Text = String.Format("{0}", Step);
            }
            else
                Text = String.Format("{0}%", nValue);
            if (sbrh_Text == null)
                sbrh_Text = new SolidBrush(Color.CornflowerBlue);
            this.sbrh_Text.Color = ForeColor;
            CenterText(g, sbrh_Text);
        }
      protected override void OnPaint(PaintEventArgs e)
      {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Graphics g = e.Graphics;
            if (progressBarStyle == ProgressBarStyles.Line)
                LineProgressBar(g);
            else
                RingProgressBar(g);


        }
        [Category("外观")]
        public ProgressBarStyles ProgressBarStyle
        {
            get { return progressBarStyle; }
            set { progressBarStyle = value;
                if (value == ProgressBarStyles.Ring)
                {

                    this.ForeColor = Color.DimGray;
                    this.Size = new Size(100, 100);
                    this.RingThickness = 8;
                }
                else if(value == ProgressBarStyles.Steps)
                {
                   
                    this.Size = new Size(150, 150);
                    this.RingThickness = 13;
                    Value = 0;
                }
                    Refresh(); }
        }
        [Category("外观")]
        public Color RingColor
        {
            get { return sbr_Ring.Color; }
            set { sbr_Ring.Color = value; Invalidate(); }
        }
        [Category("外观")]
        public Color CenterColor
        {
            get { return sbr_Center.Color; }
            set { sbr_Center.Color = value; Invalidate(); }
        }

        [Category("外观")]
        public int RingThickness
        {
            get { return nRingThickness; }
            set
            {
                nRingThickness = value;
               

                Invalidate();
            }
        }

        [Category("外观")]
        public int CenterSize
        {
            get
            {
               return  this.nInnerSize;
            }
            set
            {
                nInnerSize = value;
            }
        }
        [Category("外观")]
        public Color ProgressColor
      {
          get { return clrProgress; }
          set { clrProgress = value;  Invalidate();}
      }
      
     
        [Category("外观")]
        public virtual float Value
      {
          get { return nValue; }
          set { nValue = value;this.Invalidate( ); }
      }

        public override Color ForeColor
      {
          get { return base.ForeColor; }
          set { base.ForeColor = value;
             
               this.Invalidate(); }
      }
        [Category("外观")]
        public Color BottomColor
        {
          get { return sbr_Bottom.Color; }
          set { sbr_Bottom.Color = value; Invalidate( ); }
        }
        [Category("外观")]
        public Alignments Alignment
        {
            get { return enm_Alignment; }
            set { enm_Alignment = value;
                Font = new Font("微软雅黑",10);
                Invalidate(); }
        }
        [Category("外观")]
        public int EqualDivideParts
        {
            get { return nEqualDivideParts; }
            set {
                if (ProgressBarStyle == ProgressBarStyles.Steps)
                {
                    nEqualDivideParts = value;
                    Step = value - 1;
                    Invalidate();
                }
            }
        }
        [Category("外观")]
        public Color SplitLineColor
        {
            get { return clrSplitLineColor; }
            set
            {
                if (ProgressBarStyle == ProgressBarStyles.Steps)
                {
                    clrSplitLineColor = value;
                    Invalidate();
                }
            }
        }
        [Category("外观")]
        public float SplitLineThickness
        {
            get { return fSplitLineThickness; }
            set
            {
                if (ProgressBarStyle == ProgressBarStyles.Steps)
                {
                    fSplitLineThickness = value;
                    Invalidate();
                }
            }
        }
        [Category("外观")]
        public float Step
        {
            get
            {
                return nCurrentStep;
            }
            set
            {
                if (value <= EqualDivideParts && ProgressBarStyle == ProgressBarStyles.Steps)
                {
                    nCurrentStep = value;
                    Value = (int)(value / EqualDivideParts * 100);
                    Invalidate();
                }
            }
        }
        public override string MainBindableProperty
        {
            get
            {
                return Value.ToString();
                
            }
            set
            {
                try
                {
                    Value = float.Parse(value);
                    base.MainBindableProperty = value;
                }
                catch (Exception e)
                {
                    Value = 0;
                    Console.WriteLine("出现转换错误"+e.ToString());

                }
                
                
                
            } 
        }
    }
     
    internal class MobileProgressBarDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(MobileProgressBarDesignerActionList);
        }
    }
    internal class MobileProgressBarDesignerActionList : VisualActionList
    {
        MobileProgressBar lps;
        public MobileProgressBarDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            lps = TargetControl as MobileProgressBar;
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            AddDesignerActionPropertyItem("ProgressBarStyle", "进度条风格");
            AddDesignerActionHeaderItem("公共外观");
            DIC.Add(new DesignerActionPropertyItem("ForeColor", "字体颜色","公共外观"));
            DIC.Add(new DesignerActionPropertyItem("Value", "进度", "公共外观"));
            DIC.Add(new DesignerActionPropertyItem("BottomColor", "未完成进度颜色", "公共外观"));

            AddDesignerActionPropertyItem("Font", "字体");
            DIC.Add(new DesignerActionHeaderItem("线性进度条外观"));
            DIC.Add(new DesignerActionPropertyItem("ProgressColor", "进度条颜色", "线性进度条外观"));
           
            DIC.Add(new DesignerActionHeaderItem("环状进度条外观"));
            DIC.Add(new DesignerActionPropertyItem("RingColor", "圆环颜色", "环状进度条外观"));
            DIC.Add(new DesignerActionPropertyItem("CenterColor", "圆环中心区域颜色", "环状进度条外观"));
            DIC.Add(new DesignerActionPropertyItem("RingThickness", "圆环厚度", "环状进度条外观"));
            AddDesignerActionHeaderItem("步骤进度条外观");
            AddDesignerActionPropertyItem("EqualDivideParts", "步骤总数", "步骤进度条外观");
            AddDesignerActionPropertyItem("SplitLineColor", "均分线颜色", "步骤进度条外观");
            AddDesignerActionPropertyItem("SplitLineThickness", "均分线粗细", "步骤进度条外观");
            AddDesignerActionPropertyItem("Step", "当前步骤", "步骤进度条外观");

        }
        public MobileProgressBar.ProgressBarStyles ProgressBarStyle
        {
            get { return lps.ProgressBarStyle; }
            set
            {
                AssignPropertyValue("ProgressBarStyle", value);
            }
        }
        public Color ProgressColor
        {
            get { return lps.ProgressColor; }
            set{ AssignPropertyValue("ProgressColor", value);}
        }

        public float Step
        {
            get
            {
                return lps.Step;
            }
            set
            {
                AssignPropertyValue("Step", value);
            }
        }
        public int EqualDivideParts
        {
            get { return lps.EqualDivideParts; }
            set
            {
                AssignPropertyValue("EqualDivideParts", value);
            }
        }
        
        public Color SplitLineColor
        {
            get { return lps.SplitLineColor; }
            set
            {
                AssignPropertyValue("SplitLineColor", value);
            }
        }
     
        public float SplitLineThickness
        {
            get { return lps.SplitLineThickness; }
            set
            {
                AssignPropertyValue("SplitLineThickness", value);
            }
        }
        public float Value
        {
            get { return lps.Value; }
            set{ AssignPropertyValue("Value",value);}
        }
        public Color ForeColor
        {
            get { return lps.ForeColor; }
            set{ AssignPropertyValue("ForeColor",value);}
        }
        public Color BottomColor
        {
            get { return lps.BottomColor; }
            set{ AssignPropertyValue("BottomColor",value);}
        }
        public Color RingColor
        {
            get { return lps.RingColor; }
            set { AssignPropertyValue("RingColor", value); }
        }
        public Color CenterColor
        {
            get { return lps.CenterColor; }
            set { AssignPropertyValue("CenterColor", value); }
        }
        public Font Font
        {
            get { return lps.Font; }
            set { AssignPropertyValue("Font", value); }
        }
        public int RingThickness
        {
            get { return lps.RingThickness; }
            set { AssignPropertyValue("RingThickness", value); }
        }
    }
}
/*[Category("外观")]
        public Color BorderColor
        {
            get { return pen_Border.Color; }
            set { 
                
                  
                pen_Border.Color = value;
                Invalidate();
            }
        }

        public float BorderThickness
        {
            get { return fBorderThickness; }
            set { fBorderThickness = value;
                pen_Border.Width = value;
                Invalidate();
            }
        }*/