using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.ComponentModel.Design;
using dotNetLab.Widgets.Design;
using System.IO;

namespace dotNetLab.Widgets
{

    [ToolboxItem(true), DefaultEvent("Click"), DefaultProperty("Text"),
        Designer(typeof(FlatDesigner)),ToolboxBitmap( typeof(Button))]
    public class MobileButton : TextBlock
    {
        protected WinForm.UI.Animations.AnimationManager animationManager;
        bool bNeedAnimation = false;
       
        protected bool bNeedResetRegion = false;
        protected Color clr_RegionColor;
        //Bitmap bmp_Normal = null;
        //Bitmap bmp_Press = null;
        bool isNormal = true;
        private Size imageSize;
        private int nGapBetweenTextImage = 8;
        LeftRightAlignment bIConAlignment = LeftRightAlignment.Left;
        Image  imv_Icon = null;
       // Region rgn;
        bool bEnableRound = false;
       
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
          //  WinFormCtrlTriggerColors
            //if(Radius==-1)
            isNormal = true;
            Invalidate();

        }
        protected override void OnResize(EventArgs e)
        {
           
            base.OnResize(e);
            DisposeGp();
             if(bEnableRound)
                nRadius = this.Height - 1;
            Invalidate( );
        }
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            // if(Radius==-1)
            isNormal = false;
            Invalidate();
            if (bNeedAnimation)
                animationManager.StartNewAnimation(e.Location);

        }
        void DisposeGp()
        {
            if (gp != null)
            {
                gp.Dispose();
                gp = null;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            if(nRadius>-1)
            {
                 
                if(gp== null)
                {
                    gp = new GraphicsPath();
                    this.ProvideGraphicsPath(CornerAligment, -1,0, 0, Width-1, Height-1, Radius);
                }
                if (isNormal)
                {
                    sbr_FillRoundRect.Color = NormalColor;
                    
                }
                else
                {

                    if (!NeedAnimation)
                        sbr_FillRoundRect.Color = PressColor;
                    else
                    {
                        
                        sbr_FillRoundRect.Color = NormalColor;
                    }
                }
                e.Graphics.FillPath(sbr_FillRoundRect, gp);
            }
            else
            {
                if (isNormal)
                { 

                     base.BackColor = clr_Normal;
                }
                else
                     if (!NeedAnimation)
                    base.BackColor = clr_Press;
                     
                	
            }


            //if (Radius > -1 && bNeedResetRegion)
            //{
            //    this.BackColor = Color.Transparent;
            //    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //    GraphicsPath gp = DrawRoundRect(e.ClipRectangle.X + 1, e.ClipRectangle.Y + 1, e.ClipRectangle.Width - 2,
            //        e.ClipRectangle.Height - 2, nRadius);
            //    if (sbr_Fill == null)
            //        sbr_Fill = new SolidBrush(clr_RegionColor);
            //    else
            //    {
            //        sbr_Fill.Color = clr_RegionColor;
            //    }
            //    e.Graphics.FillPath(sbr_Fill, gp);
            //    this.Region = new Region(gp);
            //    bNeedResetRegion = false;
            //}
            //using (Pen pen_border = new Pen(WinFormCtrlTriggerColors.PressEffect(BackColor, 20, true), 4))
            //{
            //    e.Graphics.DrawRectangle(pen_border, 1, 1, Width - 2, Height - 2);

            //}
            if (NeedAnimation)
            {
                if (animationManager.IsAnimating())
                {
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    Color bg = clr_Press ;
                    
                    using (SolidBrush hrush = new SolidBrush(Color.FromArgb(50, bg)))
                    {
                        float animationValue = (float)animationManager.GetProgress();
                        float x = animationManager.GetMouseDown().X - animationValue / 2;
                        float y = animationManager.GetMouseDown().Y - animationValue / 2;
                        RectangleF rect = new RectangleF(x, y, animationValue, animationValue);
                        g.FillEllipse(hrush, rect);
                    }
                    //g.SmoothingMode = SmoothingMode.None;
                }
            }
            this.sbrh_Text.Color = this.ForeColor;

            MessureText(e.Graphics);


            if (this.Source != null)
            {
                if (IConAlignment == LeftRightAlignment.Left)
                {
                    e.Graphics.DrawString(Text, Font, sbrh_Text, x_Text + GapBetweenTextImage, y_Text);

                    e.Graphics.DrawImage(this.Source, x_Text - GapBetweenTextImage ,
                        (Height - this.Source.Height) / 2);
                }
                else
                {
                    e.Graphics.DrawString(Text, Font, sbrh_Text, x_Text -2*GapBetweenTextImage, y_Text);
                    e.Graphics.DrawImage(this.Source, x_Text + szf_Text.Width+ GapBetweenTextImage ,
                        (Height - this.Source.Height) / 2);
                }
            }
            else
            {
                e.Graphics.DrawString(Text, Font, sbrh_Text, x_Text  , y_Text);
            }
    }
        protected override void prepareAppearance()
        {
            base.prepareAppearance();

            NormalColor = Color.DodgerBlue;
            PressColor = Color.RoyalBlue;
            this.ForeColor = Color.White;
            sbr_FillRoundRect = new SolidBrush(clr_Normal);
            // this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.Opaque, true);
            this.bEnableResize = true;
            Size = new Size(150, 50);
            animationManager = new WinForm.UI.Animations.AnimationManager(this);
            animationManager.EachTimeStep = 6 ;


        }
        [Browsable(false)]
        public override bool Vertical
        {
            get { return base.Vertical; }
            set { base.Vertical = value; }
        }
        [Category("外观")]
        public bool EnableMobileRound
        {
            get { return bEnableRound; }
            set
            {
                bEnableRound = value;
                if (gp != null)
                {
                    gp.Dispose();
                    gp = null;
                }
                if (value)
                {
                     Radius = this.Height - 1;
                     
                }
                else
                {
                     Radius = -1;
                    
                }
                
               
            }
        }
        [Category("外观")]
        public Alignments CornerAligment
        {
            get { return enu_CornerAlignment; }
            set { enu_CornerAlignment = value; 
                Invalidate(); }
        }
        [Category("外观"), Description("按钮动画")]
        public bool NeedAnimation
        {
            get { return bNeedAnimation; }
            set { bNeedAnimation = value;Invalidate(); }
        }
        [Category("外观"),Description("按钮按下颜色")]
        public Color PressColor
        {
            get
            {
                
                return clr_Press;
            }

            set
            {
                clr_Press = value;
                if (Radius > -1)
                {
                    DisposeGp();

                }
             
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
                Refresh();
            }
        }
        [Category("外观")]
        public Color NormalColor
        {
            get
            {
                return clr_Normal;
            }

            set
            {
                    clr_Normal = value;
                if (Radius > -1)
                {
                    DisposeGp();
                }
                
                Refresh();
            }
        }

        [Category("外观")]
         

        public override  int  Radius
        {
            get { return nRadius; }
            set
            {
                nRadius = value;
                base.BackColor = Color.Transparent;
                DisposeGp();
                    Refresh();
            }
        }
        [Category("外观")]
        public virtual Image Source
        {
            get { return imv_Icon ; }
            set
            {
                
              if(imv_Icon!=null)
                        imv_Icon.Dispose();
                imv_Icon  = value;
            }
        }
        [Category("外观")]
        public Size ImageSize
        {
            get { return imageSize; }
            set
            {
                imageSize = value;
                Refresh();
            }
        }
        [Category("外观")]
        public int GapBetweenTextImage
        {
            get { return nGapBetweenTextImage; }
            set
            {
                nGapBetweenTextImage = value; 
                Refresh();
                
            }
        }
        [Category("外观")]
        public LeftRightAlignment IConAlignment
        {
            get { return bIConAlignment; }
            set
            {
                bIConAlignment = value;
                Refresh();
            }
        }



    }

    internal class FlatDesigner :VisualDesigner
    {
        

        protected override void ProvideType()
        {
            this.type_ActionList = typeof(FlatActionList);
        }
    }
    internal class FlatActionList : VisualActionList
    {
        MobileButton flt;
        public FlatActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            flt = this.TargetControl as MobileButton;
        }
         
        public int Radius
        {
            get { return flt.Radius; }
            set { this.AssignPropertyValue("Radius",value);   }
        }
      
        public Color PressColor
        {
            get
            {

                return flt.PressColor;
            }

            set
            {
                this.AssignPropertyValue("PressColor",value); 
            }
        }

        public String Text
        {
            get { return flt.Text; }
            set { this.AssignPropertyValue( "Text", value); }
        }
        
        public virtual Image Source
        {
            get { return flt.Source; }
            set
            {

                this.AssignPropertyValue("Source", value); 
            }
        }

        public Color NormalColor
        {
            get
            {
                return flt.NormalColor;
            }

            set
            {
                this.AssignPropertyValue("NormalColor", value);
                flt.Refresh();
            }
        }
        public bool NeedAnimation
        {
            get { return flt.NeedAnimation; }
            set { this.AssignPropertyValue("NeedAnimation", value);AssignPropertyValue("PressColor",Color.Cyan) ; }
        }
        public bool EnableMobileRound
        {
            get { return flt.EnableMobileRound; }
            set { AssignPropertyValue("EnableMobileRound", value); AssignPropertyValue("Height", 38); }
        }
        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            DIC.Clear();
            DIC.Add(new DesignerActionHeaderItem("外观"));
            DIC.Add(new DesignerActionPropertyItem("Text", "文本", "外观"));
            DIC.Add(new DesignerActionPropertyItem("Radius", "圆角半径", "外观"));
            DIC.Add(new DesignerActionPropertyItem("NormalColor", "按钮正常颜色", "外观"));
            DIC.Add(new DesignerActionPropertyItem("PressColor", "按钮按下颜色", "外观"));
            DIC.Add(new DesignerActionPropertyItem("Source", "按钮图标", "外观"));
            AddDesignerActionPropertyItem("NeedAnimation", "启用动画");
            AddDesignerActionPropertyItem("EnableMobileRound", "手机风格");
        }

     
    }
   
}
 