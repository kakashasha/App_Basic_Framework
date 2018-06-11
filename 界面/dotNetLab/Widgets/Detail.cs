using dotNetLab.Appearance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using dotNetLab.Widgets.Design;

namespace dotNetLab.Widgets
{
    [ToolboxItem(true)]
    [DefaultEvent("Click"), ToolboxBitmap(typeof(HelpProvider)),Designer(typeof(DetailDesigner))]
    public class Detail : UIElement
    {
        float fExternalWidth = 2;
        bool isMouseDown = false;
        float fInnerWidth = 3.5f;
        Pen ExternalPen, InnerPen;
        float fEnclosingGap = 2;
        SolidBrush sldInner;
        Color clrExternalColor = Color.Gray, clrInnerColor = Color.DodgerBlue;
         
        protected override void prepareAppearance()
        {

            this.Size = new System.Drawing.Size(40, 40);
            ExternalPen = new Pen(clrExternalColor, fExternalWidth);
            InnerPen = ExternalPen;
            sldInner = new SolidBrush(clrInnerColor);
            this.BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode =  System.Drawing.Drawing2D.SmoothingMode .AntiAlias;
            if (!isMouseDown)
            {
                g.DrawEllipse(ExternalPen, 2, 2, this.Width - 4, this.Height - 4);
                g.FillEllipse(sldInner, Width / 4 - fInnerWidth, Height / 2 - fInnerWidth, 2 * fInnerWidth, 2 * fInnerWidth);
                g.FillEllipse(sldInner, Width / 2 - fInnerWidth, Height / 2 - fInnerWidth, 2 * fInnerWidth, 2 * fInnerWidth);
                g.FillEllipse(sldInner, 3 * Width / 4 - fInnerWidth, Height / 2 - fInnerWidth, 2 * fInnerWidth, 2 * fInnerWidth);
            }
            else
            {
                g.DrawEllipse(ExternalPen, 4, 4, this.Width - 8, this.Height - 8);
                g.FillEllipse(sldInner, (Width) / 4 - fInnerWidth+ fEnclosingGap, (Height) / 2 - fInnerWidth, 2 * fInnerWidth, 2 * fInnerWidth);
                g.FillEllipse(sldInner, (Width) / 2 - fInnerWidth, (Height) / 2 - fInnerWidth, 2 * fInnerWidth, 2 * fInnerWidth);
                g.FillEllipse(sldInner, 3 * (Width) / 4 - fInnerWidth-fEnclosingGap, (Height) / 2 - fInnerWidth, 2 * fInnerWidth, 2 * fInnerWidth);
            }

        }
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            isMouseDown = true;
            Refresh();
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            isMouseDown = false;
            Refresh();
            base.OnMouseUp(e);
        }
        [Category("外观")]
        public float ExternalWidth
        {
            get { return fExternalWidth; }
            set
            {
                fExternalWidth = value;
                ExternalPen.Width = value;
                Refresh();
            }
        }
        [Category("外观")]
        public float InnerWidth
        {
            get { return fInnerWidth; }
            set
            {
                fInnerWidth = value;

                Refresh();
            }
        }
        [Category("外观")]
        public Color InnerColor
        {
            get { return clrInnerColor; }
            set
            {
                clrInnerColor = value;
                sldInner.Color = value;
                Refresh();
            }

        }
        [Category("外观")]
        public Color ExternalColor
        {
            get { return clrExternalColor; }
            set
            {
                clrExternalColor = value;
                ExternalPen.Color = value;
                Refresh();
            }
        }
        [Category("外观")]
        public float EnclosingGap
        {
            get { return fEnclosingGap; }
            set { fEnclosingGap = value; }
        }
    }

    internal class DetailDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            type_ActionList = typeof(DetailDesignerActionList);
        }
    }
    internal class DetailDesignerActionList : VisualActionList
    {
        private Detail dt;
        public DetailDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            dt = TargetControl  as Detail;
            
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            AddDesignerActionHeaderItem(APPEARANCE);
            AddDesignerActionPropertyItem("ExternalWidth","边框粗细");
            AddDesignerActionPropertyItem("ExternalColor","边框颜色");
            AddDesignerActionPropertyItem("InnerWidth","内圆直径");
            AddDesignerActionPropertyItem("InnerColor","内圆颜色");
           
            AddDesignerActionPropertyItem("EnclosingGap","内圆间隔");
        }
        
    public float ExternalWidth
    {
        get { return dt.ExternalWidth; }
        set
        {
             AssignPropertyValue("",value);
        }
    }
  
    public float InnerWidth
    {
        get { return dt.InnerWidth; }
        set
        {
            AssignPropertyValue("InnerWidth",value);
        }
    }
     
    public Color InnerColor
    {
        get { return dt.InnerColor; }
        set
        {
            AssignPropertyValue("InnerColor",value);
        }

    }
     
    public Color ExternalColor
    {
        get { return dt.ExternalColor; }
        set
        {
            AssignPropertyValue("ExternalColor",value);
        }
    }
    
    public float EnclosingGap
    {
        get { return dt.EnclosingGap; }
        set { AssignPropertyValue("EnclosingGap",value);}
    }

      
    }
}
