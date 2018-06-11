using dotNetLab.Appearance;
using dotNetLab.Widgets.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace dotNetLab.Widgets
{
    [DefaultEvent("Click"), DefaultProperty("Text"),Designer(typeof(CircleDesigner))]
    public class Circle : MobilePictureBox
    {
        Pen pen_Content;
        float fStrokeWidth = 2;
        int nWhichShap = 1;
        bool bFill = false;
        Color clrFillColor;
        SolidBrush sldbrh_Fill;
        
        Point pnt_Image_Pos;
        Color clr_Mousedown;
        Image img_Effect;
        bool bMouseDown = false;
        bool bNeedEffect = true;
        bool bClipCircleRegion = false;
        GraphicsPath gp;
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.Width = this.Height = 40;
            sldbrh_Fill = new SolidBrush(Color.White);
            this.pen_Border.Color = Color.DodgerBlue;
            this.pnt_Image_Pos = new Point();
            this.pen_Content = new Pen(Color.DodgerBlue, 2);
            BorderThickness = 2;


        }
        void DrawLine(Graphics g)
        {
            g.DrawLine(pen_Content, this.Width / 4.0f
                        , this.Height / 2.0f, this.Width * 3 / 4.0f, this.Height / 2.0f
                        );
        }
        void DrawCross(Graphics g)
        {
            g.DrawLine(pen_Content, this.Width * 5 / 16f
                       , this.Height * 5 / 16f, this.Width * 11 / 16f, this.Height * 11 / 16f
                       );
            g.DrawLine(pen_Content, this.Width * 11 / 16f
               , this.Height * 5 / 16f, this.Width * 5 / 16f, this.Height * 11 / 16f
               );
        }
        void DrawMaxBox(Graphics g)
        {
            g.DrawRectangle(pen_Content, this.Width * 5 / 16, this.Height * 5 / 16,
                      this.Width * 3 / 8, this.Height * 3 / 8);
            pen_Content.Width += 2.5f;
            g.DrawLine(pen_Content, this.Width * 5 / 16, this.Height * 5 / 16,
                 this.Width * 11 / 16, this.Height * 5 / 16);
            pen_Content.Width = fStrokeWidth;
        }
        void DrawPlus(Graphics g)
        {
            g.DrawLine(pen_Content, this.Width / 4.0f
                        , this.Height / 2.0f, this.Width * 3 / 4.0f, this.Height / 2.0f
                        );
            g.DrawLine(pen_Content, this.Width / 2.0f
               , this.Height / 4f, this.Width / 2.0f, this.Height * 3 / 4.0f
               );
        }
        void DrawImage(Graphics g, Image _img)
        {
            if ( CenterImage)
            {
                CenterPicture(g);
            }
            else
                g.DrawImage(_img, ImagePostion.X, ImagePostion.Y, ImageSize.Width, ImageSize.Height);
        }
        void ResizeMe()
        {
            if (bClipCircleRegion)
            {
                if (gp != null)
                {
                    gp.Reset();
                    gp.Dispose();
                    this.Region.Dispose();
                }
                gp = new GraphicsPath();
                gp.AddEllipse(fStrokeWidth,
                       fStrokeWidth, this.Width - 2 * fStrokeWidth,
                       this.Height - 2 * fStrokeWidth);
                this.Region = new Region(gp);
            }
            Refresh();
        }
        void DrawMouseEffect_Circle(Graphics g)
        {
            g.FillEllipse(sldbrh_Fill, fStrokeWidth,
              fStrokeWidth, this.Width - 2 * fStrokeWidth,
              this.Height - 2 * fStrokeWidth);
        }
        void DrawMouseEffect_Pie(Graphics g)
        {
            sldbrh_Fill.Color = PressEffect(FillColor, 20, false);
        }
        protected override void prepareEvents()
        {

            base.prepareEvents();
            this.MouseDown += RoundX_MouseDown;
            this.MouseUp += RoundX_MouseUp;
            this.Resize += RoundX_Resize;
        }

        private void RoundX_Resize(object sender, EventArgs e)
        {
            ResizeMe();
        }

        private void RoundX_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.bMouseDown = false;
            pen_Border.Color = clrBorder;
            Refresh();
        }

        private void RoundX_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            bMouseDown = true;
            pen_Border.Color = MouseDownColor;
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            if (bFill)
            {
                //if (!bMouseDown)
                //    g.FillEllipse(sldbrh_Fill, fStrokeWidth,
                //    fStrokeWidth, this.Width - 2 * fStrokeWidth,
                //    this.Height - 2 * fStrokeWidth);
                //else
                //    DrawMouseEffect_Pie(g);
                g.FillEllipse(sldbrh_Fill, 3 * fStrokeWidth / 2,
                  3 * fStrokeWidth / 2, this.Width - 3 * fStrokeWidth,
                   this.Height - 3 * fStrokeWidth);
                switch (nWhichShap)
                {
                    case 0: DrawCross(g); break;
                    case 1: DrawLine(g); break;
                    case 2: DrawMaxBox(g); break;
                    case 3: DrawPlus(g); break;

                }
            }
            else
            {
                if (NeedEffect)
                    if (bMouseDown)
                        DrawMouseEffect_Circle(g);

                g.DrawEllipse(pen_Border, 3 * fStrokeWidth / 2,
              3 * fStrokeWidth / 2, this.Width - 3 * fStrokeWidth,
              this.Height - 3 * fStrokeWidth);
            }
            if (img != null)
            {
                if (!bMouseDown)
                    DrawImage(g, Source);
                else
                    if (img_Effect != null)
                    DrawImage(g, Effect);
            }


        }
        
        [Category("外观")]
        public bool Fill
        {
            get { return bFill; }
            set
            {
                bFill = value;
                Refresh();
            }
        }

        [Category("外观")]
        public Color FillColor
        {
            get
            {
                return clrFillColor;

            }

            set
            {
                clrFillColor = value;
                sldbrh_Fill.Color = value;
                MouseDownColor = value;
                Refresh();
            }
        }
        [BrowsableAttribute(false)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        [BrowsableAttribute(false)]
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
        [Category("外观")]
        public int WhichShap
        {
            get
            {
                return nWhichShap;
            }

            set
            {
                nWhichShap = value;
                Refresh();
            }
        }
       
        [Category("外观")]
        public Point ImagePostion
        {
            get
            {
                return pnt_Image_Pos;
            }

            set
            {
                pnt_Image_Pos = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Color MouseDownColor
        {
            get
            {
                return clr_Mousedown;
            }

            set
            {
                clr_Mousedown = value;
                sldbrh_Fill.Color = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Image Effect
        {
            get
            {
                return img_Effect;
            }

            set
            {

                if (img_Effect != null)
                    img.Dispose();
                img_Effect = value;
            }
        }
        
        [Category("外观")]
        public bool NeedEffect
        {
            get
            {
                return bNeedEffect;
            }

            set
            {
                bNeedEffect = value;
            }
        }

        public bool ClipCircleRegion { get {return  bClipCircleRegion; } set { bClipCircleRegion = value; ResizeMe();} }
    }
    internal class CircleDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(CircleDesignerActionList);
        }
    }
    internal class CircleDesignerActionList : ImageViewActionListDesigner
    {
        Circle cir;
        public CircleDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            cir = TargetControl as Circle;
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            base.PrepareDesignerActionItemCollection(DIC);
      
            AddDesignerActionPropertyItem("BorderThickness", "边框粗细");
            AddDesignerActionPropertyItem("Fill", "填充");
            AddDesignerActionPropertyItem("FillColor", "填充颜色");
            AddDesignerActionPropertyItem("WhichShap", "何种形状");
            AddDesignerActionPropertyItem("ImagePostion", "图片坐标");
            AddDesignerActionPropertyItem("MouseDownColor", "按下时颜色");
            AddDesignerActionPropertyItem("NeedEffect", "启用效果");
            AddDesignerActionPropertyItem("Effect", "效果图片");
            AddDesignerActionPropertyItem("ClipCircleRegion", "边框裁剪");
        }
      
        public bool Fill
        {
            get { return cir.Fill ; }
            set
            {
                AssignPropertyValue("Fill", value);
            }
        }
 
        public Color FillColor
        {
            get
            {
                return cir.FillColor;

            }

            set
            {
                AssignPropertyValue("FillColor", value);
            }
        }

        public int WhichShap
        {
            get
            {
                return cir.WhichShap;
            }

            set
            {
                AssignPropertyValue("WhichShap", value);
            }
        } 
       
   
        public Point ImagePostion
        {
            get
            {
                return cir.ImagePostion;
            }

            set
            {
                AssignPropertyValue("ImagePostion", value);
            }
        }
       
        public Color MouseDownColor
        {
            get
            {
                return cir.MouseDownColor;
            }

            set
            {
                AssignPropertyValue("MouseDownColor", value);
            }
        }
     
        public Image Effect
        {
            get
            {
                return cir.Effect;
            }

            set
            {

                AssignPropertyValue("Effect", value);
            }
        }
 
        public float BorderThickness
        {
            get
            {
                return cir.BorderThickness;
            }

            set
            {
                AssignPropertyValue("BorderThickness", value);
            }
        }
        
        public bool NeedEffect
        {
            get
            {
                return cir.NeedEffect;
            }

            set
            {
                AssignPropertyValue("NeedEffect", value);
            }
        }

        public bool  ClipCircleRegion { get {return  cir.ClipCircleRegion; } set { AssignPropertyValue("ClipCircleRegion", value); } }
    }
}
