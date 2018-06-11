using dotNetLab.Widgets.Container;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets.UIBinding
{
    public class BlockUIBinder : UIBinder
    {
        
        public BlockUIBinder(Control panel)
        {
            ctrlWhichContainer = panel;
        }
        protected override void AlignmentRule()
        {
            Color clr = (ctrlWhichContainer as BlockContainer).SelectedColor;
            foreach (Control item in this.controlItems)
            {
                Block bk = item as Block;
                bk.EnableSelect = true;
                bk.SelectedColor = clr;
                try
                {
                    bk.Click -= DefaultClick;
                }
                catch (Exception e)
                {
          
                }
                bk.Click += DefaultClick;
                
                 
            }
        }
        protected virtual void DefaultClick(object sender,EventArgs arg)
        {
            Block bk = lastChild as Block;
            if (lastChild != null)
                bk.Selected = false;
            this.lastChild = sender as Control;
            (this.lastChild as Block).Selected = true;
             
        }
    }
}
