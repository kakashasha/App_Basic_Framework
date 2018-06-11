﻿
using System;
using System.IO;
using System.Text;
 
using dotNetLab.Data.DBEngines.SQLite;
using dotNetLab.Data.Embedded;
using dotNetLab.Data.Uniting;
namespace dotNetLab.Data
{
    public class UIConsole
    {
        public delegate void UIOutput_ErrCallback(String str);

        public readonly string FIELD_FIRE_TIME_1 = "Fire_Time";
        public readonly string FIELD_MESSAGE_2 = "Message";
        public readonly string FIELD_LOCATION_3 = "Location";
        public readonly string FIELD_Action_4 = "Action";
        public readonly string FIELD_MessageType_5 = "MessageType";
   
        public delegate void UIOutput_InfoCallback(String str);
        public event UIOutput_ErrCallback UIOutput_Err;
        public event UIOutput_InfoCallback UIOutput_Info;
        public UnitDB LogDB;
        public readonly String NORMAL_DATE_FORMAT = "yyyy-MM-dd hh:mm:ss";
        public readonly string LOGTABLE = "LogTable";
        public UIConsole()
        {
            LogDB = new UnitDB();
           
            LogDB.DBDiagnoseHandler += LogDbOnDbDiagnoseHandler;
            LogDB.Connect(DBEngineNames.SQLITE, "Log.mdf");
            CheckLogTable();
        }
        private void CheckLogTable()
        {
            LogDB.GetAllTableNames();
            int n =LogDB.AllTableNames.IndexOf(LOGTABLE);
            if(n==-1)
                CreateLogTable();
        }
        void CreateLogTable()
        {
            LogDB.NewTable(LOGTABLE,
                "Fire_Time text primary key,Message text not null,Location text not null,Action text not null,MessageType text not null");
        }
        protected String Now
        {
            get
            {
              return  DateTime.Now.ToString(NORMAL_DATE_FORMAT);
                
            }
        }
        //For DB Error
        public void Error(ErrorInfo e)
        {
            if (UIOutput_Err != null)
            { 
                 
                UIOutput_Err(
                    e.Message);
                
            }
            else
            {
                Console.WriteLine("日志数据库出现异常：未设置UIOutput_Err事件");
                String str = String.Format("\r\n{0} 日志数据库出现异常：未设置UIOutput_Err事件。",Now);
                File.AppendAllText("Log.txt",str,Encoding.Default);
                Error("日志数据库出现异常：未设置UIOutput_Err事件。");
            }
            LogDB.NewRecord(LOGTABLE,String.Format("'{0}','{1}','{2}','No','Error'",Now,e.Message+e.CommandText.Replace('\'',' '),e.ExceptionInfo.StackTrace));
        }
        //For Normal Error
        public void Error(Exception e)
        {
            if (UIOutput_Err != null)
            {

                UIOutput_Err(
                    e.Message);

            }
            else
            {
                Console.WriteLine("日志数据库出现异常：未设置UIOutput_Err事件");
                String str = String.Format("\r\n{0} 日志数据库出现异常：未设置UIOutput_Err事件。", Now);
                File.AppendAllText("Log.txt", str, Encoding.Default);
                Error("日志数据库出现异常：未设置UIOutput_Err事件。");
            }
            LogDB.NewRecord(LOGTABLE, String.Format("'{0}','{1}','{2}','No','Error'", Now, e.Message  , e  .StackTrace));
        }
        public void Error(String err)
        {
            if (UIOutput_Err != null)
            {
                UIOutput_Err(err);
            }
            else
            {
                String str = String.Format("\r\n{0} 日志数据库出现异常：未设置UIOutput_Err事件。",Now);
                File.AppendAllText("Log.txt",str,Encoding.Default);
            
            }
            LogDB.NewRecord(LOGTABLE,String.Format("'{0}','{1}','No','No','Error'",Now,err));
        }
        public void Info(String info, string strLocation, String strAction)
        {
            if (UIOutput_Info != null)
            {
                UIOutput_Info(info);
            }
            else
            {
                String str = String.Format("\r\n{0} 日志数据库出现异常：未设置UIOutput_Info事件。",Now);
                Console.WriteLine("日志数据库出现异常：未设置UIOutput_Info事件。");
                File.AppendAllText("Log.txt",str,Encoding.Default);
                
            }
            LogDB.NewRecord( LOGTABLE,String.Format("'{0}','{1}','{2}','{3}','Info'",Now,info,strLocation,strAction));
        }
        public void Info(String info)
        {
            Info(info,"No","No");
        }
        private void LogDbOnDbDiagnoseHandler(ErrorInfo errorInfo, DBOperator bytOperator)
        {
             File.AppendAllText("Log.txt",string.Format("\r\n{0}日志数据库出现异常：{1} ，操作为{2}",
                 Now,errorInfo.Message,bytOperator.ToString()),Encoding.Default);
        }
    }
}