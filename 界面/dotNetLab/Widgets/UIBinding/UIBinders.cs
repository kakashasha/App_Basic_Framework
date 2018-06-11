using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets.UIBinding
{
    public abstract class UIBinder 
    {
        protected List<Control> controlItems;
        public Control ctrlWhichContainer;
        protected int UIAlignmentGap = 0 ;
        protected Control lastChild = null;
        protected abstract void AlignmentRule();
       
        public  void BindUI()
       {
            if (controlItems != null)
                controlItems.Clear();
            else
                controlItems = new List<Control>();
         
            if (ctrlWhichContainer == null)
                return;
            foreach (Control item in ctrlWhichContainer.Controls)
            {
                controlItems.Add(item);
            }
         
            AlignmentRule();
            
    }
    }
}
