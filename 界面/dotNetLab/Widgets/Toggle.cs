using dotNetLab.Appearance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Widgets.Design;
using System.Windows.Forms.Design;

namespace dotNetLab.Widgets
{
    [ToolboxItem(true)]
    [DefaultEvent("Click")]
    [Designer(typeof(ToggleDesigner)),ToolboxBitmap(typeof(Toggle),"ICons.Toggle.png")]
    public class Toggle : UIElement
    {
        Color clrBottomColor = Color.BlueViolet, clrBorderColor = Color.DarkGray;

        SolidBrush sBrush_Switch, sbrush_SlideBlock;
        Pen Pen_Rec_Border, Pen_WhiteRect, Pen_WhiteLine;
        Color _BreakColor = Color.White;
        float fRatio_SlideBlock = 0.25f;
        Point pnt_SliderPos;
        [Category("外观")]
        public Color BlockColor
        {
            get { return sbrush_SlideBlock.Color; }
            set
            {

                sbrush_SlideBlock.Color = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Color BorderColor
        {
            get { return Pen_Rec_Border.Color; }
            set
            {
               
                Pen_Rec_Border.Color = value;
                Refresh();
            }
        }

        [Category("外观")]
        public Color BottomColor
        {
            get { return clrBottomColor; }
            set
            {
                clrBottomColor = value;
                sBrush_Switch.Color = value;
                Refresh();
            }
        }
        float WhitLine_X = 0;

        bool bChecked = false;
        [Category("外观")]
        public bool Checked
        {
            get { return bChecked; }
            set
            {
                bChecked = value;
                if (value)
                    pnt_SliderPos.X = (int)(this.Width - (this.Width - 4) * fRatio_SlideBlock - 2);
                else
                    pnt_SliderPos.X = 2;

                Refresh();
            }
        }
        protected override void prepareAppearance()
        {
            clrBottomColor = Color.DodgerBlue;
            this.BackColor = Color.Transparent;
            this.Size = new Size(45, 22);
            pnt_SliderPos = new Point(2, 1);
            Pen_Rec_Border = new Pen(clrBorderColor, 2);
            sBrush_Switch = new SolidBrush(clrBottomColor);
            sbrush_SlideBlock = new SolidBrush(clrBorderColor);
            Pen_WhiteRect = new Pen(Color.White, 2);
            Pen_WhiteLine = new Pen(Color.White, 3.5f);
        }
        protected override void prepareEvents()
        {
            this.Click += Toggle_Click;
            this.Resize += Toggle_Resize;
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            //  正常显示
            if (Enabled)
            {

                sbrush_SlideBlock.Color = BlockColor;
                if (bChecked)
                {
                    sBrush_Switch.Color = clrBottomColor;
                }
                else
                    sBrush_Switch.Color = Color.LightGray;

                g.FillRectangle(sBrush_Switch, 2, 2, this.Width - 4, this.Height - 4);
                g.DrawRectangle(Pen_WhiteRect, 4, 4, this.Width - 8, this.Height - 8);
                g.DrawRectangle(Pen_Rec_Border, 2, 2, this.Width - 4, this.Height - 4);
                if (bChecked)
                    g.FillRectangle(sbrush_SlideBlock, pnt_SliderPos.X
                  , pnt_SliderPos.Y, (this.Width - 4) * fRatio_SlideBlock, this.Height - 2);
                else
                    g.FillRectangle(sbrush_SlideBlock, pnt_SliderPos.X, pnt_SliderPos.Y, (this.Width - 4) * fRatio_SlideBlock, this.Height - 2);
                WhitLine_X = pnt_SliderPos.X > 2 ? pnt_SliderPos.X - 2 : (this.Width - 4) * fRatio_SlideBlock + 3;
                g.DrawLine(Pen_WhiteLine, WhitLine_X, 1, WhitLine_X, this.Height - 1);

            }
            else
            {
                sBrush_Switch.Color = Color.DimGray;
                sbrush_SlideBlock.Color = Color.DimGray;


                g.FillRectangle(sBrush_Switch, 2, 2, this.Width - 4, this.Height - 4);
                g.DrawRectangle(Pen_WhiteRect, 2, 2, this.Width - 4, this.Height - 4);
                g.DrawRectangle(Pen_Rec_Border, 2, 2, this.Width - 4, this.Height - 4);
                g.FillRectangle(sbrush_SlideBlock, pnt_SliderPos.X
                    , pnt_SliderPos.Y, (this.Width - 4) * fRatio_SlideBlock, this.Height - 2);
                WhitLine_X = pnt_SliderPos.X > 2 ? pnt_SliderPos.X - 2 : (this.Width - 4) * fRatio_SlideBlock + 2;
                g.DrawLine(Pen_WhiteLine, WhitLine_X, 1, WhitLine_X, this.Height - 1);
            }
        }

        private void Toggle_Resize(object sender, EventArgs e)
        {
            if (bChecked)
                pnt_SliderPos.X = (int)(this.Width - (this.Width - 4) * fRatio_SlideBlock - 2);
            else
                pnt_SliderPos.X = 2;
            Refresh();
        }

        private void Toggle_Click(object sender, EventArgs e)
        {
            Checked = !bChecked;
            Refresh();
        }


    }
     internal class ToggleDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(ToggleDesignerActionList);
        }
    }

    internal class ToggleDesignerActionList : VisualActionList
    {
        private Toggle tgl;
        public ToggleDesignerActionList(IComponent component,ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            tgl = this.TargetControl as Toggle;
             
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
             AddDesignerActionHeaderItem(APPEARANCE);
            AddDesignerActionPropertyItem("Checked","选中状态");
            AddDesignerActionPropertyItem("BottomColor","选中时颜色");
          
        }
      
       
        
       
       
        public   bool Checked
        {
            get { return tgl.Checked; }
            set
            {
                AssignPropertyValue("Checked",value);
            }
        }
        
        public Color BottomColor
        {
            get { return tgl.BottomColor; }
            set
            {
                AssignPropertyValue("BottomColor",value);
            }
        }
    }
}
