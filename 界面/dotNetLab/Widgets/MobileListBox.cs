using dotNetLab.Widgets.Container;
using System;
using System.Collections.Generic;
using System.Text;
using dotNetLab.Widgets.Third_Party ;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace dotNetLab.Widgets
{
   public class MobileListBox : CanvasPanel
    {
       public dotNetLab.Widgets.Third_Party.MobileScrollBar HBar, VBar;
      public  ListContainer UIAdapter;
        [Browsable(false)]
        public ControlCollection Items
        {
            get { return UIAdapter.Items; }
        }
        protected override void prepareCtrls()
        {
            base.prepareCtrls();
            HBar = new MobileScrollBar();
            HBar.Orientation = MobileScrollBar.MetroScrollOrientation.Horizontal;
            HBar.Visible = false;
            VBar = new MobileScrollBar();
            VBar.Orientation = MobileScrollBar.MetroScrollOrientation.Vertical;
            VBar.Visible = false;
            UIAdapter = new ListContainer();
            this.Size = new System.Drawing.Size(200, 300);

        }
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
           
            // this.Radius = 10;
            this.BorderThickness = 1;
            NormalColor = Color.White;
            this.BorderColor = Color.Gray;
            
        }
        public void AddItem(Control control)
        {
            UIAdapter.AddItem(control);
        } 

        public void TopItem(Control c)
        {
            UIAdapter.TopItem(c);
        }
        public void RemoveItem(Control c)
        {
            UIAdapter.RemoveItem(c);
        }
        public void Clear()
        {
            Items.Clear();
        }
        protected override void UnitCtrls()
        {
            base.UnitCtrls();
            this.AddControl(HBar);
            AddControl(VBar);
            AddControl(UIAdapter);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (UIAdapter != null)
            {
                UIAdapter.Size = new System.Drawing.Size(this.Size.Width - 30, this.Size.Height - 30);
                VBar.Size = new Size(VBar.Size.Width, UIAdapter.Height - 10);
               // HBar.Size = new Size(UIAdapter.Width - 5, HBar.Size.Height);
                HBar.Location = new Point(HBar.Location.X, UIAdapter.Height +15);
            }
        }

    }
}
