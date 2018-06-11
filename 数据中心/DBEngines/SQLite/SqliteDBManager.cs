using dotNetLab.Data.Embedded;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Data.DBEngines.SQLite;
using dotNetLab.Data.Uniting;

namespace dotNetLab
{
    namespace Data
    {
      public  class SQLiteDBManager
        {
            UnitDB ndb;
            public UIConsole console;
            public UnitDB CompactDB { get { return ndb; } set { ndb = value; } }
            public SQLiteDBManager()
            {
                console = new UIConsole(); 
                ndb = new UnitDB ();
                ndb.DBDiagnoseHandler += Ndb_DBDiagnoseHandler;
                ndb.Connect();
                
            }

            private void Ndb_DBDiagnoseHandler(ErrorInfo e, DBOperator byt_Operator)
            {
                 if(byt_Operator== DBOperator.OPERATOR_CONNECT_DB)
                {
                    Tipper.Tip_Info_Error("连接到本地数据库失败！");
                }
                 
                    
                console.Error(e);
            }
        }
    }
}