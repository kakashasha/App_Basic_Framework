using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Widgets.UIBinding;
namespace dotNetLab. Widgets
{
   public class DataBindingVisualPropertyEditor : VisualPropertyEditor
    {
       DataBindingPropertyEditorDialog dlg;
       protected override void PrepareShowEditor(out Form frm,Object obj)
       {

           UIElementBinderInfo uif = obj as UIElementBinderInfo;

           dlg = new DataBindingPropertyEditorDialog();
           if (uif != null)
           {
               dlg.txb_DBEngineIndex.Text = uif.DBEngineIndex.ToString();
               dlg.txb_DBField.Text = uif.FieldName;
               dlg.txb_Filter.Text = uif.Filter;
               dlg.txb_TableName.Text = uif.TableName;
               dlg.tgl_UpdateToDB.Checked = uif.StoreInDB;
               dlg.tgl_RealTimeWithDB.Checked = uif.StoreIntoDBRealTime;
           }
           frm = dlg;
       }

       protected override object ReturnEditorValues(ref object obj)
       {
           obj = dlg.Value;
           return obj;
       }
    }
}
