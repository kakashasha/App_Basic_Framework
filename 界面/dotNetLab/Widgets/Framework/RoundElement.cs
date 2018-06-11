
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace dotNetLab.Widgets
{
    public class RoundElement :UIElement
    {
        protected int nRadius = -1;
        protected Color clr_Press;
        protected Color clr_Normal;
        protected SolidBrush sbr_FillRoundRect = null;
        protected Alignments enu_CornerAlignment = Alignments.All;
        protected GraphicsPath gp = null ;
        protected int nBorderThickness = -1;
        protected Pen pen_Border;
        protected Color clrBorder;

        [Category("外观")]
        public virtual Color BorderColor
        {
            get { return clrBorder; }
            set { clrBorder = value; Invalidate(); }
        }
        [Category("外观")]
         public virtual int BorderThickness
        {
            get { return nBorderThickness; }
            set { nBorderThickness = value; Invalidate(); }
        }
        [Category("外观")]
        public virtual int Radius
        {
            get { return nRadius; }
            set
            {
                nRadius = value;
               
             
            }
        }
        
        public GraphicsPath LeftRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);

            Point[] pnts = new Point[4];
            pnts[0] = new Point(radius, 0);
            pnts[1] = new Point(width, 0);
            pnts[2] = new Point(width, height);
            pnts[3] = new Point(radius, height);

            gp.AddLines(pnts);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            return gp;
        }
        public GraphicsPath RightRoundRect(int nHeaderHeight, int x, int y, int width, int height, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            Point[] pnts = new Point[4];
            pnts[0] = new Point(this.Width - radius, height);
            pnts[1] = new Point(this.Width - nHeaderHeight - radius, height);
            pnts[2] = new Point(this.Width - nHeaderHeight - radius, y);
            pnts[3] = new Point(this.Width - radius, y);

            gp.AddLines(pnts);

            return gp;


        }
        public GraphicsPath UpRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            Point[] pnts = new Point[4];
            pnts[0] = new Point(width, radius);
            pnts[1] = new Point(width, height);
            pnts[2] = new Point(0, height);
            pnts[3] = new Point(0, radius);

            gp.AddLines(pnts);

            //gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            //gp.AddArc(x, height - radius, radius, radius, 90, 90);
            //  gp.CloseAllFigures();
            return gp;
        }
        public GraphicsPath LeftUpRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            // gp.AddArc(width - radius, y, radius, radius, 270, 90);
            Point[] pnts = new Point[5];
            pnts[0] = new Point(radius, y);
            pnts[1] = new Point(width, y);
            pnts[2] = new Point(width, height);
            pnts[3] = new Point(x, height);
            pnts[4] = new Point(x, radius);

            gp.AddLines(pnts);

            //gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            //gp.AddArc(x, height - radius, radius, radius, 90, 90);
            //  gp.CloseAllFigures();
            return gp;
        }
        public GraphicsPath RightUpRoundRect(int x, int y, int width, int height, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            // gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            Point[] pnts = new Point[5];
            pnts[0] = new Point(width, radius);
            pnts[1] = new Point(width, height);
            pnts[2] = new Point(0, height);
            pnts[3] = new Point(x, y);
            pnts[4] = new Point(width - radius, y);
            gp.AddLines(pnts);

            //gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            //gp.AddArc(x, height - radius, radius, radius, 90, 90);
            //  gp.CloseAllFigures();
            return gp;
        }
        public GraphicsPath DownRoundRect(int nHeaderHeight, int x, int y, int width, int height, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);

            Point[] pnts = new Point[4];
            pnts[0] = new Point(x, height - radius);
            pnts[1] = new Point(x, height - nHeaderHeight );
            pnts[2] = new Point(width, height - nHeaderHeight );
            pnts[3] = new Point(width, height - radius);

            gp.AddLines(pnts);
            // gp.AddArc(x, height - radius, radius, radius, 90, 90);
            return gp;
        }
        public GraphicsPath LeftDownRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            // gp.AddArc(x, y, radius, radius, 180, 90);
            // gp.AddArc(width - radius, y, radius, radius, 270, 90);
            Point[] pnts = new Point[5];
            pnts[0] = new Point(x,height - radius );
            pnts[1] = new Point(x, y);
            pnts[2] = new Point(width, y);
            pnts[3] = new Point(width, height);
            pnts[4] = new Point(radius, height);

            gp.AddLines(pnts);

            //gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            //gp.AddArc(x, height - radius, radius, radius, 90, 90);
            //  gp.CloseAllFigures();
            return gp;
        }
        public GraphicsPath RightDownRoundRect(int x, int y, int width, int height, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            // gp.AddArc(x, y, radius, radius, 180, 90);
            // gp.AddArc(width - radius, y, radius, radius, 270, 90);
             gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            Point[] pnts = new Point[5];
            pnts[0] = new Point(width-radius, height);
            pnts[1] = new Point(x, height);
            pnts[2] = new Point(x, y);
            pnts[3] = new Point(width, y);
            pnts[4] = new Point(width, height-radius);
            gp.AddLines(pnts);

            //gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            //gp.AddArc(x, height - radius, radius, radius, 90, 90);
            //  gp.CloseAllFigures();
            return gp;
        }
        public GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }

        public GraphicsPath ProvideGraphicsPath(Alignments alg,int nHeaderHeight,int x, int y, int width, int height, int radius)
        {
            switch(alg)
                {
                    case Alignments.Up:
                       gp = UpRoundRect(x, y, width, height, radius);
                        break;
                    case Alignments.Down:
                        gp =   DownRoundRect(nHeaderHeight,x, y, width, height, radius);
                        break;
                    case Alignments.Left:
                        gp =  LeftRoundRect(x, y, width, height, radius);
                        break;
                    case Alignments.Right:
                        gp =   RightRoundRect(nHeaderHeight,x, y, width, height, radius);
                        break;
                    case Alignments.All:
                        gp =   DrawRoundRect(x, y, width, height, radius);
                        break;
                    case Alignments.Left_UP:
                        gp = LeftUpRoundRect(x, y, width, height, radius);
                        break;
                    case Alignments.Left_Down:
                        gp = LeftDownRoundRect(x, y, width, height, radius);
                        break;
                    case Alignments.Right_UP:
                        gp = RightUpRoundRect(x, y, width, height, radius);
                        break;
                    case Alignments.Right_Down:
                        gp =  RightDownRoundRect(x, y, width, height, radius);
                        break;
                    
                }

            return gp;
        }
    }
  
}
/*  internal class MyDesigner : ParentControlDesigner
    {
       // private RoundRectangle MyControl;

        //public override void Initialize(IComponent component)
        //{
        //    base.Initialize(component);

        //   // MyControl = (RoundRectangle)component;

        //   //// bool succ = this.EnableDesignMode(MyControl.MyTabControl, "MyTabControl");
        //   // foreach (Control item in MyControl.Controls)
        //   // {
        //   //       EnableDesignMode(item, item.Name);
        //   // }
        //}
    }*/
/* [ToolboxItem(true), DefaultEvent("Click"), DefaultProperty("Text")]
   [Designer(typeof(MyDesigner))]
   [DesignTimeVisible(true),ToolboxBitmap(typeof(Panel))]
   public class RoundRectangle :RoundElement
   {
      /*protected Bitmap bmp_Normal = null;
      protected Bitmap bmp_Press = null;#1#
      protected bool isNormal = true;
      private Size imageSize;

       protected Pen pen_Border;


       protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
       {
           //WinFormCtrlTriggerColors
           //if(Radius==-1)
           isNormal = true;
           Refresh();

       }
       protected override void OnResize(EventArgs e)
       {
           base.OnResize(e);


       }
       protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
       {
           // if(Radius==-1)
           isNormal = false;
           Refresh();

       }

       protected override void OnPaint(PaintEventArgs e)
       {

           if(nRadius>-1)
           {
               Graphics g = e.Graphics;

               g.SmoothingMode = SmoothingMode.HighQuality;
               if (bmp_Normal == null)
               {
                   bmp_Normal = new Bitmap(this.Width, this.Height);
                   Graphics gx = Graphics.FromImage(bmp_Normal);
                   gx.SmoothingMode = SmoothingMode.HighQuality;

                       gx.FillPath(new SolidBrush(clr_Normal), DrawRoundRect(0, 0, Width - 1, Height - 1, Radius));
                  // }

                   gx.Dispose();

               }
               if(bmp_Press==null)
               {
                   bmp_Press = new Bitmap(this.Width, this.Height);
                   Graphics gx = Graphics.FromImage(bmp_Press);
                   gx.SmoothingMode = SmoothingMode.HighQuality;
                   //if (!AntiAlias)
                   //{
                   //    gx.FillPath(new SolidBrush(clr_Press), DrawRoundRect(0, 0, Width - 2, Height - 2, Radius));
                   //    gp_CutRegion = DrawRoundRect(0, 0, Width - 1, Height - 1, Radius);
                   //    this.Region = new Region(gp_CutRegion);
                   //}
                   //else
                   //{
                       gx.FillPath(new SolidBrush(clr_Press), DrawRoundRect(0, 0, Width - 1, Height - 1, Radius));

                 //  }
                       gx.Dispose();
               }
               if (isNormal)
               {
                   g.DrawImage(bmp_Normal, 0, 0);
                   //if(!AntiAlias)
                   //{

                   //pen_Border.Color = clr_Normal;
                   //g.DrawPath(pen_Border, gp_CutRegion);
                   //}

               }

               else
               {


                   g.DrawImage(bmp_Press, 0, 0);
                   //if (!AntiAlias)
                   //{
                   //    pen_Border.Color = clr_Press;
                   //    g.DrawPath(pen_Border, gp_CutRegion);
                   //}
               }
           }
           else
           {
               if (isNormal)
               { 

                    base.BackColor = clr_Normal;
               }
               else
                    base.BackColor = clr_Press;
           }

   }



       protected override void prepareAppearance()
       {
           base.prepareAppearance();

           NormalColor = Color.DodgerBlue;
           PressColor = Color.RoyalBlue;
           this.ForeColor = Color.White;
           pen_Border = new Pen(clr_Normal, 1);



           Size = new Size(150, 50);


       }

       [Category("外观")]
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
                   if (bmp_Press != null)
                       bmp_Press.Dispose();
                   bmp_Press = null;

               }

               Refresh();

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
                   if (bmp_Normal != null)
                       bmp_Normal.Dispose();
                   bmp_Normal = null;
               }

               Refresh();
           }
       }
       [Category("外观")]
       public override int Radius
       {
           get { return base.Radius; }
           set
           {
               base.Radius = value;
               base.BackColor = Color.Transparent;
               if(bmp_Normal != null)
               {
                   bmp_Normal.Dispose();
                   bmp_Normal = null;
               }
               if (bmp_Press != null)
               {
                   bmp_Press.Dispose();
                   bmp_Press =null ;
               }
                   Refresh();
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


   }*/
