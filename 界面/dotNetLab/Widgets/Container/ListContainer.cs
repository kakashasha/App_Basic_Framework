using dotNetLab.Widgets.Design;
using dotNetLab.Widgets.UIBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing;

namespace dotNetLab.Widgets.Container
{
    [ToolboxItem(true), Designer(typeof(ListContainerDesigner))]
    public class ListContainer : Container
    {
        ListUIBinder lstUIiBinder;
        //it is for hold the Items And Help Scrollbar up,down,right,left
        CanvasControl ctrl_ForHold;
        bool bFirstBindUI = true;
        private int nElementVerticalGap = 0;
       
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.NormalColor = Color.Silver;
        }
        protected override void ProvideUIBinder()
        {
            if (ctrl_ForHold == null)
            {
                ctrl_ForHold = new CanvasControl();
                ctrl_ForHold.BackColor = Color.Transparent;
                ctrl_ForHold.Size = this.Size;
                this.Controls.Add(ctrl_ForHold);
            }
            if (lstUIiBinder == null)
            {
                lstUIiBinder = new ListUIBinder(this);
                UIBinder = lstUIiBinder;
            }

        }
        [Browsable(false)]
        public ControlCollection Items
        {
            get { return ctrl_ForHold.Controls; }
        }

        public int ElementVerticalGap
        {
            get { return nElementVerticalGap; }
            set { nElementVerticalGap = value; }
        }


        public void TopItem(Control c)
        {
            AddItem(c);
            ctrl_ForHold.Controls.SetChildIndex(c, 0);
        }

        public void AddItem(Control c)
        {
            if (bFirstBindUI)
            {
                UIBinder.BindUI();
                this.Parent.Text = null;
                bFirstBindUI = false;
            }
           
            int n = ctrl_ForHold.Controls.Count;

            if (n > 0)
            {
                int x = 0;
                int y = ctrl_ForHold.Controls[n - 1].Location.Y + ctrl_ForHold.Controls[n - 1].Height;
                c.Location = new System.Drawing.Point(x, y+ElementVerticalGap);
                if (ctrl_ForHold.Height - y - c.Height < 0)
                {

                    ctrl_ForHold.Height += Math.Abs(ctrl_ForHold.Height - y - c.Height-ElementVerticalGap);
                    lstUIiBinder.VBar.Visible = true;
                    float nDelta = Math.Abs(this.Height - ctrl_ForHold.Height);
                    float ratio  = nDelta / this.Height;
                    if (ratio > 1)
                        ratio = 0.9f;
                    this.lstUIiBinder.VBar.ThumbHeight = (int) (lstUIiBinder.VBar.Height *(1- ratio));
                    //this.bShowVBar = true;
                }

                if (ctrl_ForHold.Width - x - c.Width < 0)
                {
                    ctrl_ForHold.Width += Math.Abs(ctrl_ForHold.Width - x - c.Width);

                    lstUIiBinder.HBar.Visible = true;
                    float nDelta = Math.Abs(this.Width - ctrl_ForHold.Width);
                    float ratio = nDelta / this.Width;
                    if (ratio > 1)
                        ratio = 0.9f;
                    this.lstUIiBinder.HBar.ThumbWidth = (int)(lstUIiBinder.HBar.Width * (1 - ratio));

                }

            }
             
            else
            {
                int x = 0;
                int y = c.Location.Y;
                c.Location = new System.Drawing.Point(x, y + ElementVerticalGap);
                if (ctrl_ForHold.Height - y - c.Height < 0)
                {

                    ctrl_ForHold.Height += Math.Abs(ctrl_ForHold.Height - y - c.Height - ElementVerticalGap);
                    lstUIiBinder.VBar.Visible = true;
                    float nDelta = Math.Abs(this.Height - ctrl_ForHold.Height);
                    float ratio = nDelta / this.Height;
                    if (ratio > 1)
                        ratio = 0.1f;
                    this.lstUIiBinder.VBar.ThumbHeight = (int)(lstUIiBinder.VBar.Height * (1 - ratio));
                    //this.bShowVBar = true;
                }

                if (ctrl_ForHold.Width - x - c.Width < 0)
                {
                    ctrl_ForHold.Width += Math.Abs(ctrl_ForHold.Width - x - c.Width);

                    lstUIiBinder.HBar.Visible = true;
                    float nDelta = Math.Abs(this.Width - ctrl_ForHold.Width);
                    float ratio = nDelta / this.Width;
                    if (ratio > 1)
                        ratio = 0.1f;
                    this.lstUIiBinder.HBar.ThumbWidth = (int)(lstUIiBinder.HBar.Width * (1 - ratio));

                }
            }
            ctrl_ForHold.Controls.Add(c);
            
          
        }
        public void RemoveItem(Control c)
        {
            int nHeight = c.Height;
            int nIndex = ctrl_ForHold.Controls.IndexOf(c);
            if (nIndex < ctrl_ForHold.Controls.Count - 1)
            {

                ctrl_ForHold.Controls.Remove(c);
                for (int i = nIndex; i < ctrl_ForHold.Controls.Count; i++)
                {
                    ctrl_ForHold.Controls[i].Location = new System.Drawing.Point(0, ctrl_ForHold.Controls[i].Location.Y + nHeight);
                }
            }
            else
            {
                ctrl_ForHold.Controls.Remove(c);

            }
            this.Height -= nHeight;
            UIBinder.BindUI();
        }

        internal class CanvasControl : UserControl
        {
            public CanvasControl()
                {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
            this.UpdateStyles();
                }
        }
    
        
    }

    internal class ListContainerDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(ListContainerDesignerActionList);
        }
    }
    internal class ListContainerDesignerActionList : ContainerDesignerActionList
    {
        private ListContainer bc;
        VisualDesigner vpd;
        public ListContainerDesignerActionList(IComponent component, ControlDesigner controlDesigner) :
            base(component, controlDesigner)
        {
            bc = this.TargetControl as ListContainer;
            vpd = controlDesigner as VisualDesigner;
        }


         

        
    }
}
