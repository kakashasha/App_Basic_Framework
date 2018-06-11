using System;
using System.Collections.Generic;
using System.Text;
 

namespace dotNetLab.Data.Uniting
{
   public class UIDBEngine : UnitDB
    {
       public UIDBEngine()
       {
           this.DBDiagnoseHandler += new DBDiagnoseCallBack(UIDBEngine_DBDiagnoseHandler);
       }

       void UIDBEngine_DBDiagnoseHandler(ErrorInfo e, DBOperator byt_Operator)
       {
           dotNetLab.Data.Tipper.Tip_Info_Error(String.Format("{0}\r\n{1}\r\n{2}", e.Message, e.CommandText, byt_Operator));
       }
    }
}
