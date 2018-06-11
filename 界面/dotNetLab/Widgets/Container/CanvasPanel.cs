using dotNetLab.Widgets.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace dotNetLab.Widgets.Container
{
   [Designer(typeof(CanvasPanelDesigner)),ToolboxItem(true)] 
   public class CanvasPanel :RoundElement
    {
        protected int nUITabHeaderItemGap = 0;
        Image source = null;
        Size szImageSize  ;
        Point imagePos  ;
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (gp != null)
            {
                gp.Dispose();
                gp = null;
            }

        }
        protected override void prepareAppearance()
            {
                base.prepareAppearance();
                this.Size = new System.Drawing.Size(200, 200);
                YaHeiFont(11);
                sbrh_Text = new SolidBrush(Color.White);
                sbr_FillRoundRect = new SolidBrush(Color.Gray);
            BackColor = Color.Transparent;
             
            NormalColor = Color.DarkGray;
            szImageSize = new Size(0, 0);
            imagePos = new Point(0, 0);
            }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (this.nRadius > -1)
            {
               

                g.SmoothingMode = SmoothingMode.HighQuality;
               
                 
                    switch (CornerAlignment)
                    {
                        case Alignments.Up:
                            if (gp == null)
                            {
                                gp = UpRoundRect(0, 0, Width -1, Height-1, Radius);

                            }


                            break;
                        case Alignments.Down:
                            if (gp == null)
                            {
                                gp = DownRoundRect(this.Height, 0, 0, Width-1 , Height-1 , Radius);
                            }

                            break;

                        case Alignments.All:
                            if (gp == null)
                            {
                                gp = DrawRoundRect(0, 0, Width -1, Height-1 , Radius);
                            }

                            break;
                        case Alignments.Left_UP:
                            if (gp == null)
                                gp = LeftUpRoundRect(0, 0, Width-1 , Height-1 , Radius);
                            break;
                        case Alignments.Left_Down:
                            ;
                            break;
                        case Alignments.Right_UP:
                            if (gp == null)
                                gp = RightUpRoundRect(0, 0, Width-1 , Height-1 , Radius);
                            break;
                        case Alignments.Right_Down:
                            ;
                            break;

                    }
                if (BorderThickness < 0)
                {
                    if (gp != null)
                        g.FillPath(sbr_FillRoundRect, gp);
                }
                else
                {
                    if (pen_Border == null)
                        pen_Border = new Pen(BorderColor, BorderThickness);
                    else
                    {
                        pen_Border.Color = BorderColor;
                        pen_Border.Width = BorderThickness;
                    }
                    g.FillPath(sbr_FillRoundRect, gp);
                    g.DrawPath(pen_Border, gp);
                }
               
               
            }
            else
            {
                
                   

                if (BorderThickness > 0)
                {
                    if (pen_Border == null)
                        pen_Border = new Pen(BorderColor, BorderThickness);
                    else
                    {
                        pen_Border.Color = BorderColor;
                        pen_Border.Width = BorderThickness;
                    }
                    g.DrawRectangle(pen_Border, 1, 1, this.Width - 2, this.Height - 2);
                    e.Graphics.FillRectangle(sbr_FillRoundRect, 2,2, this.Width - 3, this.Height - 3);
                }
                else
                e.Graphics.FillRectangle(sbr_FillRoundRect, this.ClientRectangle);

            }

            CenterText(e.Graphics, sbrh_Text);
            if (Source != null)
            {
                g.DrawImage(Source, ImagePos.X,imagePos.Y, ImageSize.Width, ImageSize.Height);
            }
        }
        [Category("外观")]
        public Size ImageSize
        {
            get { return szImageSize; }
            set { szImageSize = value;Invalidate(); }
        }
        [Category("外观")]
        public  Point ImagePos
        {
            get { return imagePos; }
            set { imagePos = value; Invalidate(); }
        }
        [Category("外观")]
        public Image Source
        {
            get { return source; }
            set
            {
                if (source != null)
                    source.Dispose();
                source = null;
                source = value;
                Invalidate();
            }
        }
        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;

              
                Refresh();
            }
        }
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override String Text
        {
            get
            {
                return strCaption;
            }

            set
            {

                this.strCaption = value;
                Refresh();
            }
        }
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                if (sbrh_Text == null)
                    sbrh_Text = new SolidBrush(Color.Transparent);
                this.sbrh_Text.Color = value;
                base.ForeColor = value;
                Refresh();
            }
        }
        public override int Radius
        {
            get { return base.nRadius; }
            set { base.nRadius = value;
                if (gp != null)
                {
                    gp.Dispose();
                    gp = null;
                }
                Invalidate(); }
        }
         
        [Category("外观")]
        
        public Color NormalColor
        {
            get
            {return this.clr_Normal;
            }
            set
            { 
                clr_Normal = value;
                this.sbr_FillRoundRect.Color = value;
                Invalidate();
            }
        }
        [Category("外观")]
         
        public Alignments  CornerAlignment {
            get { return enu_CornerAlignment; }
            set { enu_CornerAlignment = value;  
                if (gp != null)
                {
                    gp.Dispose();
                    gp = null;
                } Invalidate(); }
        }
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set { base.BackColor = value; Invalidate(); }
        }
         
        }
  
    internal class CanvasPanelDesigner : VisualPanelDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(CanvasPanelDesignerActionList);
        }
    }
    internal class CanvasPanelDesignerActionList : VisualActionList
    {
        private CanvasPanel cp;
        public CanvasPanelDesignerActionList(IComponent component, ControlDesigner controlDesigner) : 
            base(component, controlDesigner)
        {
            cp =  TargetControl as CanvasPanel;
            
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            AddDesignerActionHeaderItem(APPEARANCE);
            AddDesignerActionPropertyItem("BorderColor", "边框颜色");
            AddDesignerActionPropertyItem("BorderThickness", "边框粗细");
            AddDesignerActionPropertyItem("NormalColor","填充颜色");
            AddDesignerActionPropertyItem("Radius","圆角半径");
            AddDesignerActionPropertyItem("CornerAlignment","圆角布置");
        }

         
        public virtual Color BorderColor
        {
            get { return cp.BorderColor; }
            set { AssignPropertyValue("BorderColor", value); }
        }
     
        public virtual int BorderThickness
        {
            get { return cp.BorderThickness; }
            set { AssignPropertyValue("BorderThickness", value);    }
        }
        public   int Radius
        {
            get { return cp.Radius; }
            set { AssignPropertyValue("Radius",value);}
        }
         
         
        
        public Color NormalColor
        {
            get { return cp.NormalColor; }
            set { AssignPropertyValue("NormalColor",value);}
        }

        public Alignments  CornerAlignment {
            get { return cp.CornerAlignment; }
            set { AssignPropertyValue("CornerAlignment",value);}
        }
    }


}
