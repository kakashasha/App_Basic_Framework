using dotNetLab.Data.Uniting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dotNetLab.Data
{
   public class LogPipe   : DBPipe
    {
        protected override void CreateLogTable()
        {
            LogDB.NewTable(LOGTABLE,
                "Fire_Time text,Message text not null");
        }
        protected override void CheckLogTable()
        {
             

            DeleteOldLogTable("AutoCleanOutputLogTime", "Outputs");

            LogDB.GetAllTableNames();
            LOGTABLE = String.Format("Outputs_{0}_{1}", DateTime.Now.Year, DateTime.Now.Month);
          

            int n = LogDB.AllTableNames.IndexOf(LOGTABLE);
            if (n == -1)
                CreateLogTable();
        }
        public String Log
        {
            set
            {
                try
                {
                    CheckLogTable();
                    LogDB.NewRecord(LOGTABLE, String.Format("'{0}','{1}'", Now, value));
                }
                catch (Exception ex)
                {
                    File.AppendAllText("Log.txt", "向日志数据库中添加一条记录时出错 At LogPipe.Log属性", Encoding.Default);

                }
               
            }
        }

        public override UnitDB ThisDB
        {
            get { return LogDB; }
            set
            {
                LogDB = value;
                CheckLogTable();
            }
        }
        
    }
}
