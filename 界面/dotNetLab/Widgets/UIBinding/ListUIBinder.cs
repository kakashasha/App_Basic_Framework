using dotNetLab.Widgets.Container;
using dotNetLab.Widgets.Third_Party;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets.UIBinding
{
    public class ListUIBinder : UIBinder
    {
        Control ExternalPanel = null ;
       public MobileScrollBar VBar = null;
       public  MobileScrollBar HBar = null;
        int nOriginalY = 0;
        int nOriginalX = 0;
       
        public ListUIBinder( Control parentcontrol)
        {
            this.ctrlWhichContainer = parentcontrol;
            ctrlWhichContainer.Text = null;


        }

        void  BindScrollBar(Control c)
        {
            MobileScrollBar mobileScrollBar = c as MobileScrollBar;
            c.BringToFront();
            c.Visible = false;
            ListContainer lc = ctrlWhichContainer as ListContainer;
            if (mobileScrollBar.Orientation == MobileScrollBar.MetroScrollOrientation.Horizontal)
            {
                HBar = mobileScrollBar;
                HBar.Width = lc.Width-2;
                HBar.Location = new Point(ExternalPanel.Width /2 - lc.Width/2, ExternalPanel.Height - HBar.Height-2);

               
            }
            else
            {
                VBar = mobileScrollBar;
                VBar.Height = lc.Height -2;
                VBar.Location = new Point(ExternalPanel.Width - VBar.Width -2, ExternalPanel.Height / 2 - VBar.Height / 2);
            }
                
            try
            {
                mobileScrollBar.ValueChanged -= MobileScrollBar_ValueChanged;
                
                
            }
            catch (Exception e)
            {

              
            }
            mobileScrollBar.ValueChanged += MobileScrollBar_ValueChanged;
             
        }
        void ScrollBarAction(Control container,Control Holder,Object sender, int Delta, int newValue, MobileScrollBar.ScrollDirection direction)
        {

            if (sender == HBar)
            {
                if (Holder.Width > container.Width)
                {
                    int nTotalMoveLen = (Holder.Width - container.Width);
                    if (direction == MobileScrollBar.ScrollDirection.Left)
                    {
                        int MovedLen = (int)((Delta / 100.0f) * nTotalMoveLen);
                         if (HBar.Value != 0)
                            Holder.Location = new Point(Holder.Location.X + MovedLen, Holder.Location.Y);
                         else
                         {
                            Holder.Location = new Point(nOriginalX, Holder.Location.Y);
                         }
                    }
                    else
                    {
                        int MovedLen = (int)((Delta / 100.0f) * nTotalMoveLen);
                         if (HBar.Value != 100)
                        
                            Holder.Location = new Point(Holder.Location.X - MovedLen, Holder.Location.Y);
                         
                         else
                         {
                            Holder.Location = new Point(nOriginalX - nTotalMoveLen, Holder.Location.Y);
                        }
                    }
                }
            }
            else
            {
               
                if (Holder.Height > container.Height)
                {
                    int nTotalMoveLen = (Holder.Height - container.Height);
                    if (direction == MobileScrollBar.ScrollDirection.Down)
                    {

                        int MovedLen = (int)((Delta / 100.0f) * nTotalMoveLen);
                        if (VBar.Value != 100)
                        {
                            Holder.Location = new Point(Holder.Location.X, Holder.Location.Y - MovedLen);
                        }
                        else
                        {
                            Holder.Location = new Point(Holder.Location.X, nOriginalY - nTotalMoveLen);
                        }
                    }
                    else
                    {
                        int MovedLen = (int)((Delta / 100.0f) * nTotalMoveLen);
                        if (VBar.Value != 0)
                            Holder.Location = new Point(Holder.Location.X, Holder.Location.Y + MovedLen);
                        else
                            Holder.Location = new Point(Holder.Location.X, nOriginalY);
                    }
                }
            }
        }
        private void MobileScrollBar_ValueChanged(object sender,int Delta ,int newValue, MobileScrollBar.ScrollDirection direction)
        {
            Control Holder = ctrlWhichContainer.Controls[0];
            ScrollBarAction(this.ctrlWhichContainer,Holder,sender, Delta, newValue, direction);
        }

        protected override void AlignmentRule()
        {
            if (ExternalPanel == null)
                ExternalPanel = ctrlWhichContainer.Parent;
            if(ExternalPanel != null)
            {
                ctrlWhichContainer.Location = new Point(ExternalPanel.Width / 2 - ctrlWhichContainer.Width / 2,
                    ExternalPanel.Height / 2 - ctrlWhichContainer.Height / 2
                    );
                foreach (Control item in ExternalPanel.Controls)
                {
                    if (item is MobileScrollBar)
                    {
                        BindScrollBar(item);
                    }

                }
            }
           
           
        }
    }
}
