using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Threading;

namespace dotNetLab.Widgets
{
   public abstract class VisualPropertyEditor: UITypeEditor
    {
       
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
           
            if (provider != null)
            {
                IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (windowsFormsEditorService != null)
                {

                    Form frm;
                    PrepareShowEditor( out frm,value);
                    frm.ShowDialog();
                    frm.Dispose();
                    
                    
                }
            }

            return ReturnEditorValues(ref value);
        }
        protected abstract void PrepareShowEditor(out Form frm,Object obj);
        protected abstract object ReturnEditorValues(ref Object obj);
        
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
