using dotNetLab.Data.Uniting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dotNetLab.Data 
{
   public abstract class DBPipe
    {
        public UnitDB LogDB;
        protected String DBFilePath = "Log.mdf";
        public readonly String NORMAL_DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public string LOGTABLE = "LogTable";
        public DBPipe()
        {
            
        }

        public abstract UnitDB ThisDB { get; set; }
         
        protected abstract void CreateLogTable();
        protected abstract void CheckLogTable();
        
        protected String Now
        {
            get
            {
                return DateTime.Now.ToString(NORMAL_DATE_FORMAT);

            }
        }
      protected void DeleteOldLogTable(String FieldName, String PrefixTableName)
        {
            int nAutoClearOutputLogTime = 0;
            String str = LogDB.FetchValue(FieldName);
            if (str == null)
            {
                LogDB.Write(FieldName, "3");
            }
            else
            {
                try
                {

                    nAutoClearOutputLogTime = int.Parse(str);
                }
                catch (Exception ex)
                {
                    LogDB.Write(FieldName, "3");
                    nAutoClearOutputLogTime = 3;
                }
            }
            DateTime nowDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM"));
            DateTime beforeDate = nowDate.AddMonths(-nAutoClearOutputLogTime);
            String NeedDeletedTableName = String.Format(PrefixTableName + "_{0}_{1}", beforeDate.Year, beforeDate.Month);
            LogDB.RemoveTable(NeedDeletedTableName);
        }

    }
}
