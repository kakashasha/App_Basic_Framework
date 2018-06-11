using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using dotNetLab.Widgets.Container;
using dotNetLab.Widgets.Design;

namespace dotNetLab.Widgets
{
    public enum Alignments
    {
        Up,Down,Left,Right,All,Left_UP,Left_Down,Right_UP,Right_Down
    } 
    [Designer(typeof(CardDesigner)),ToolboxBitmap(typeof(GroupBox))]
    public class Card :CanvasPanel
    {
        private Alignments enm_HeaderAlignment = Alignments.Up;  
        private bool isNeedRefreshHeader = false;
        Color clrHeadColor = Color.DodgerBlue;
        int nHeadHeight = 50;
        private SolidBrush sbr_Header;
        private GraphicsPath gp_Header = null;
        private GraphicsPath gp_Border = null;
       
      //  private bool bVertical = true;
       // private int nControlCount = 0;
      

        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.NormalColor  = Color.Snow;
           // sbr_FileRoundRect = new SolidBrush(NormalColor);
            sbr_Header = new SolidBrush(clrHeadColor);
            this.Radius = 10;
              this.clrBorder = Color.Gray;
            pen_Border = new Pen(clrBorder, 1);
            
        }

        protected override void OnResize(EventArgs e)
        {
           

            base.OnResize(e);
            isNeedRefreshHeader = true;


            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
            if (gp_Border == null || isNeedRefreshHeader)
            {
                gp_Border = DrawRoundRect(0, 0, this.Width - 1, this.Height - 1, Radius);
                pen_Border.Color = clrBorder;

            }
            e.Graphics.DrawPath(pen_Border, gp_Border);
            if (gp_Header == null || this.isNeedRefreshHeader)
            {
                if (gp_Header != null)
                    gp_Header.Dispose();
                switch (HeaderAlignment)
                {
                     case Alignments.Up:  gp_Header = UpRoundRect(0, 0, this.Width - 1, HeadHeight, this.Radius); break;
                     case Alignments.Left: gp_Header = LeftRoundRect(0, 0, HeadHeight, this.Height-1, this.Radius);break;
                      case Alignments.Down: gp_Header = DownRoundRect(HeadHeight, 0, 0, Width - 1, Height - 1, Radius);break;     
                     case  Alignments.Right:gp_Header = RightRoundRect(HeadHeight, 0, 0, Width - 1, Height - 1, Radius);break;
                }
                 
                if (isNeedRefreshHeader)
                    isNeedRefreshHeader = false;
            }
             if(gp_Header!= null)
            e.Graphics.FillPath(sbr_Header, gp_Header);


        }
        public override int Radius
        {
            get { return base.Radius; }
            set
            {
                base.Radius = value;

                this.isNeedRefreshHeader = true;
                Refresh();
            }
        }
        [Category("外观")]
        public Color HeadColor { get { return clrHeadColor; } set { clrHeadColor = value; sbr_Header.Color = value; this.Invalidate(); } }
        [Category("外观")]
        public int HeadHeight { get { return nHeadHeight; } set { nHeadHeight = value; this.isNeedRefreshHeader = true; this.Invalidate(); } }
        [Category("外观")]
        public override Color BorderColor
        {
            get { return base.BorderColor; }
            set
            {
                clrBorder = value;
                pen_Border.Color = value;
                if (gp_Border != null)
                    gp_Border.Dispose();
                gp_Border = null;
                Invalidate();
            }
        }
        [Category("外观")]
        public Alignments HeaderAlignment
        {
            get { return enm_HeaderAlignment;
             
               
            }
            set { enm_HeaderAlignment = value;
                isNeedRefreshHeader = true; Invalidate(); }
        }
    }

    internal class CardDesigner : VisualPanelDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(CardDesignerActionList);
        }

      
    }

    internal class CardDesignerActionList : VisualActionList
    {
        private Card card;
        public CardDesignerActionList(IComponent component,ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            card = TargetControl as Card;
        }
         
        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            AddDesignerActionHeaderItem(APPEARANCE);
            AddDesignerActionPropertyItem("HeadColor", "深色部分颜色");
            AddDesignerActionPropertyItem("HeadHeight", "深色部分高度");
            AddDesignerActionPropertyItem("BorderColor", "外边框颜色");
            AddDesignerActionPropertyItem("HeaderAlignment", "深色部分布局");
            AddDesignerActionPropertyItem("Radius", "圆角半径");
           // DIC.Add(new DesignerActionHeaderItem(APPEARANCE));
           /* DIC.Add(new DesignerActionPropertyItem("HeadColor", "深色部分颜色", APPEARANCE));
            DIC.Add(new DesignerActionPropertyItem("HeadHeight", "深色部分高度", APPEARANCE));
            DIC.Add(new DesignerActionPropertyItem("BorderColor", "外边框颜色", APPEARANCE));
            DIC.Add(new DesignerActionPropertyItem("HeaderAlignment", "深色部分布局", APPEARANCE));
            DIC.Add(new DesignerActionPropertyItem("Radius", "圆角半径", APPEARANCE));*/
           // DIC.Add(new Design)
            // To Add A Method
           // DIC.Add(new DesignerActionMethodItem(this,"ShowMe", "点我",APPEARANCE));
        }

       
        public Color HeadColor { get {  

        			return card.HeadColor; } set {  AssignPropertyValue("HeadColor",value); } }
   
        public int HeadHeight { get { return card.HeadHeight; } set {  AssignPropertyValue("HeadHeight",value); } }
     
        public Color BorderColor
        {
            get { return card.BorderColor; }
            set
            {
                 AssignPropertyValue("BorderColor",value);
            }
        }
        
        public Alignments HeaderAlignment
        {
            get { return card.HeaderAlignment; }
            set {  AssignPropertyValue("HeaderAlignment",value); }
        }
        public  int Radius
        {
            get { return card.Radius; }
            set
            {
                 AssignPropertyValue("Radius",value);
            }
        }
    }
}
