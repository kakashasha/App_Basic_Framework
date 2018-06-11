using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using System.Text;


namespace dotNetLab
{
    namespace Data
    {
        public delegate void ExecuteCallback(ref StringBuilder sb);
        public delegate void FillSQLScriptCallback(int nIndex,
        ref StringBuilder sb);
        public delegate void EndFillSQLScriptCallback(ref StringBuilder sb);
        public class ErrorInfo
        {
            Exception e;
            String strCommandText;
            public Exception ExceptionInfo
            {
                get { return e; }
                set { this.e = value; }
            }
            public string CommandText
            {
                get
                {
                    return strCommandText;
                }
                set
                {
                    this.strCommandText = value;
                }
            }
            public string Message
            {
                get
                {
                    return e.Message;
                }
            }
            public string ErrorMethod
            {
                get
                {
                    return e.TargetSite.ToString();
                }
            }
            public string ErrorAppClass
            {
                get
                {
                    return e.Source;
                }
            }
        }
        public   enum DBOperator
        {
            OPERATOR_HEAD,
            OPERATOR_INSERT  ,
            OPERATOR_DROP_TABLE  ,
            OPERATOR_DROP_DB  ,
            OPERATOR_DELETE  ,
            OPERATOR_UPDATE  ,
            OPERATOR_QUERY_TABLE,
            OPERATOR_TRUNCATE ,
            OPERATOR_CREATE_TABLE  ,
            OPERATOR_CONNECT_DB  ,
            OPERATOR_NEW_DB  ,
            OPERATOR_AFFAIR_BEGIN  ,
            OPERATOR_AFFAIR_END  ,
            OPERATOR_CONNECT_DB_AFFAIR  ,
            OPERATOR_QUERY_ALL_DBNAMES  ,
            OPERATOR_QUERY_ALL_TABLENAMES  ,
            OPERATOR_REMOVE_USER  ,
            OPERATOR_STORE_BINARY  ,
            OPERATOR_FETCH_BINARY  ,
            OPERATOR_INSERT_BATCHSQL ,
            OPERATOR_NEW_TABLE ,
            OPERATOR_NEW_VIEW,
            OPERATOR_EXECUTESCRIPT,
            OPERATOR_FAILED,
            OPERATOR_SUCCEED,
            OPERATOR_QUERY_UNIQUE,
            OPERATOR_QUERY_ALL_TABLENAMES_ARRAY,
        }
        public abstract class DBRoot 
        {
#if DATA_DEBUG
            ErrorWnd errorWnd;
            public delegate void ErrorDescCallback(string strError);
            ErrorDescCallback ErrorDescHandler;
#endif

            public DbCommand ThisDbCommand
            {
                get { return this.cmd; }
            }
            protected bool bRemoteMode = false;
            ErrorInfo eif;
            public ErrorInfo ErrorInformation
            {
                get{ return eif; }
            }
            public delegate void DBDiagnoseCallBack(ErrorInfo e, DBOperator byt_Operator);
            public event DBDiagnoseCallBack DBDiagnoseHandler;
            public event ExecuteCallback Execute;
            public event FillSQLScriptCallback FillSQLScript;
            public event EndFillSQLScriptCallback EndFillSQLScript;
            public delegate object RemoteHandleCallback(string strSQL, DBOperator byt_Operator);
            public event RemoteHandleCallback RemoteHandle;
            protected String strErrorInfo;
            public DataTable dt;
            protected DbConnection conn;
            protected DbConnectionStringBuilder ncs;
            protected DbCommand cmd;
            protected DbDataReader reader;
            protected String strIP;
            protected String DBName;
            
            protected String strPassword;
            protected string UserName;
            protected int nPort;
            private List<String> lst_TableNames;
             List<String> lst_ColumnNames;
            //true 为文本，fale 为非文本
             List<bool> lst_ColumnTypes;

            public String Database
             {
                 get { return DBName; }
                set{DBName = value ;}
             }
            public List<bool> ColumnTypes
             {
                 get { return lst_ColumnTypes; }
                 set { lst_ColumnTypes = value; }
             } 
            public List<String> AllTableNames
            {
                get { return lst_TableNames; }
                set { lst_TableNames = value; }
            }
            public List<String> AllColumnNames
            {
                get
                {
                    return lst_ColumnNames;
                }
                set { lst_ColumnNames = value; }
            }
            bool bStatus = true;
            StringBuilder sb;
            protected DbParameter DbPar;
            public string BinaryParam = "@data";
            public readonly string CREATEVIEW = "CREATE VIEW {0} AS SELECT {1} FROM  {2};";
            public object Log
            {
                set
                {
                    System.Diagnostics.Debug.WriteLine(value);
                  
                   
                }

            }
            //事务
            protected DbTransaction tsn;
         
          
            public DbParameter DBParameter
            {
                get
                {
                    return DbPar;
                }
            }

            public bool  Status
            {
                get
                {
                    return bStatus;
                }

                set
                {
                    bStatus = value;
                }
            }
            public DBRoot()
            {
                this.lst_TableNames = new List<string>();
                lst_ColumnNames = new List<string>();
                lst_ColumnTypes = new List<bool>();
            }
            public DataTable ProvideTable(string sql, DBOperator byt_Operator)
            {
                try
                {
                    if(dt!= null)
                    {
                    dt.Clear();
                    dt.Dispose();
                        
                    }
                    dt = new DataTable();
                        cmd.CommandText = sql;
                    if (!bRemoteMode)
                    {
                        reader = cmd.ExecuteReader();
                        dt.Load(reader);
                        reader.Close();
                    }
                    else
                    {
                       dt  = RemoteHandle(sql, byt_Operator) as DataTable;
                        if (dt != null)
                            Status = true;
                        else
                        {
                            Status = false;
                        }
                    }
                   
                    this.Status = true;
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_QUERY_TABLE);
                    return null;
                }

                return dt;
            }
            public string UniqueResult(string sql)
            {
                cmd.CommandText = sql;
                String strResult = null;
                try
                {
                    if (!bRemoteMode)
                    {
                        strResult = cmd.ExecuteScalar().ToString();
                    this.Status = true;

                    }
                    else
                    {
                        strResult = RemoteHandle(sql, DBOperator.OPERATOR_QUERY_UNIQUE) as String;
                        if (strResult != null)
                            this.Status = true;
                        else
                            Status = false;
                    }
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_QUERY_UNIQUE);
                    return null;
                }
                return strResult;

            }
            void  CheckRemote(string str,DBOperator dboperator)
            {
                this.Status = Convert.ToBoolean(RemoteHandle(str,  dboperator));
            }
            public void RemoveTable(string strTableName)
            {
                try
                {
                   cmd.CommandText = string.Format("drop table  {0} ;", strTableName);
                    if(!bRemoteMode)
                    {
                        cmd.ExecuteNonQuery();
                    this.Status = true;

                    }
                    else
                    {
                        CheckRemote (cmd.CommandText, DBOperator.OPERATOR_DROP_TABLE) ;
                         
                    }
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_DROP_TABLE);

                }
            }
            /*2017-11-12
             自此及以下未进行Remote 更改
                 
                 */
            public void RemoveRecord(string TableName, String strRequirement)
            {
                try
                {
                    cmd.CommandText = string.Format("Delete from  {0} where {1} ;", TableName, strRequirement);
                    if (!bRemoteMode)
                    {
                        cmd.ExecuteNonQuery();
                        this.Status = true;

                    }
                    else
                    {
                        CheckRemote(cmd.CommandText, DBOperator.OPERATOR_DELETE);

                    }
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_DELETE);
                }
            }
            public void Update(string TableName, string strColumnAssignAndRequirment)
            {
                try
                {
                    cmd.CommandText =
                     string.Format
                        ("update  {0} set {1}",
                        TableName, strColumnAssignAndRequirment);
                    if (!bRemoteMode)
                    {
                        cmd.ExecuteNonQuery();
                        this.Status = true;

                    }
                    else
                    {
                        CheckRemote(cmd.CommandText, DBOperator.OPERATOR_UPDATE);

                    }
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_UPDATE);

                }
            }
            public virtual void NewDB(string DBName)
            {

                try
                {
                    cmd.CommandText = string.Format("create database {0} ;", DBName);
                    if (!bRemoteMode)
                    {
                        cmd.ExecuteNonQuery();
                        this.Status = true;

                    }
                    else
                    {
                        CheckRemote(cmd.CommandText, DBOperator.OPERATOR_NEW_DB);

                    }
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_NEW_DB);
                }

            }
            public void RemoveDB(string strDBName)
            {
                try
                {
                    cmd.CommandText = string.Format("DROP database {0} ;", strDBName);
                    if (!bRemoteMode)
                    {
                        cmd.ExecuteNonQuery();
                        this.Status = true;

                    }
                    else
                    {
                        CheckRemote(cmd.CommandText, DBOperator.OPERATOR_DROP_DB);

                    }
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_DROP_DB);
                }
            }
            public void NewTable(string tablename,string tableDef)
            {
                try
                {
                    cmd.CommandText = string.Format("create table {0}({1}) ;", 
                        tablename,tableDef);
                    if (!bRemoteMode)
                    {
                        cmd.ExecuteNonQuery();
                        this.Status = true;

                    }
                    else
                    {
                        CheckRemote(cmd.CommandText, DBOperator.OPERATOR_NEW_TABLE);

                    }
                }
                catch (Exception e)
                {

                    HandleError(e, cmd, DBOperator.OPERATOR_NEW_TABLE);
                }
                
            }
            public void NewRecord(string strTableName,string strValue)
            {
                try
                {
                    cmd.CommandText = string.Format("insert into {0} values({1})", strTableName, strValue);
                    if (!bRemoteMode)
                    {
                        cmd.ExecuteNonQuery();
                        this.Status = true;

                    }
                    else
                    {
                        CheckRemote(cmd.CommandText, DBOperator.OPERATOR_INSERT);

                    }
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_INSERT);
                }

            }
            // strColumnNames such as " age,sex,... "
            public void NewView(string ViewName,  string strTableName,string strColumnNames)
            {
                try
                {
                    
                    this.cmd.CommandText = 
                        string.Format(CREATEVIEW, ViewName,strColumnNames, strTableName);
                    if (!bRemoteMode)
                    {
                        cmd.ExecuteNonQuery();
                        this.Status = true;
                    }
                    else
                        CheckRemote(cmd.CommandText, DBOperator.OPERATOR_NEW_VIEW);
                }
                catch (Exception ex)
                {

                    HandleError(ex, cmd, DBOperator.OPERATOR_NEW_VIEW);
                }

            }
            public void NewKeyValueView(string ViewName, string strFromTableName,
                string  strColumnName_Name,string strColumnName_Val)
            {
                string str = string.Format("{0} as Name,{1} as Val",
                    strColumnName_Name, strColumnName_Val
                    );
                NewView(ViewName, strFromTableName, str);
            }
            public void ExecuteNonQuery(string sql, DBOperator byt_Operator)
            {
                try
                {
                    cmd.CommandText = sql;
                    if (!bRemoteMode)
                    {
                        cmd.ExecuteNonQuery();
                        this.Status = true;
                    }
                    else
                        CheckRemote(cmd.CommandText, byt_Operator);
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, byt_Operator);
                }

            }
            protected void HandleError(Exception e, DbCommand cmm_, DBOperator byt_Operator)
            {
                
                try
                {
                    this.bStatus = false;
                    this.strErrorInfo = e.Message;
                    if (eif == null)
                        eif = new ErrorInfo();
                    this.eif.CommandText = cmm_.CommandText;
                    this.eif.ExceptionInfo = e;
#if DATA_DEBUG
                errorWnd.Invoke(ErrorDescHandler, new object[] {e.Message});
#endif
                    this.DBDiagnoseHandler( eif, byt_Operator);
                }
                catch (System.Exception ex)
                {
                	    
                }
            }
          
            public virtual void Connect(string IP, int Port, string dbname,
                string User, string Password, string strConnstr,
                ref DbConnection cnn, ref DbCommand cmm)
            {
                this.strIP = IP;
                this.nPort = Port;
                this.DBName = dbname;
                this.UserName = User;
                this.strPassword = Password;
                cnn.ConnectionString = strConnstr;
#if DATA_DEBUG
                errorWnd = new ErrorWnd();
                this.ErrorDescHandler = errorWnd.ErrorRecord;
                new Thread(BootErrorWnd).Start() ;
#endif
                try
                {
                    cnn.Open();
                    cmm.Connection = cnn;
                    this.Status = true;
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_CONNECT_DB);

                }


            }


            public object GetReflectOject(string strDllPath, string strObjectFullName)
            {
                return System.Activator.CreateInstanceFrom(
                 strDllPath, strObjectFullName).Unwrap();
                 
            }
           
#if DATA_DEBUG
            void BootErrorWnd()
            {
                errorWnd.ShowDialog();
            }
#endif
           


            protected bool IsDouble(string str)
            {
                try
                {
                    double i = Convert.ToDouble(str);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            protected bool IsDateTime(string str)
            {
                try
                {
                    DateTime ui = Convert.ToDateTime(str);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            // dbpar= new MySqlParameter(this.BinaryParam, DbType.Binary);
            //strInsertValues ="'xxxx',@binary"
            //example :cmpct.StoreBinary("la", "'Canny',@data", "E:/Canny.jpg");
            public void BatchSQL(int nRecords,
              int nEachSendNum,
              string strHeadsql
              )
            {
                if (sb != null)
                    sb.Remove(0, sb.Length);
                else
                    sb = new StringBuilder();
                bool b = Execute == null;
                bool c = FillSQLScript == null;
                bool d = EndFillSQLScript == null;
                if (b || c || d)
                {
                    Log = "Event Uncomplete ?";
                    return  ;
                }

                int nTimes = nRecords / nEachSendNum;
#if TEST
                Log=string.Format("Total Num(s) are(is) {0} ", nRecords);
                Log=string.Format("Need Send Time(s) are(is) {0} ",nTimes);
                Log=string.Format("Head SQL are(is) {0} ", strHeadsql);
#endif
                int nIndex = 0;
                try
                {
                    for (int i = 0; i < nTimes; i++)
                    {
                        sb.Remove(0, sb.Length);
                        sb.Append(strHeadsql);
                        for (int j = 0; j < nEachSendNum; j++)
                        {
                            FillSQLScript(nIndex++, ref sb);

                        }
                        EndFillSQLScript(ref sb);

                        sb.Append(");");
                        Execute(ref sb);
                    }
                    sb.Remove(0, sb.Length);
                    sb.Append(strHeadsql);
                    if (nRecords - nTimes * nEachSendNum > 0)
                    {
                        for (int i = 0; i < nRecords; i++)
                        {
                            if (nIndex == nRecords)
                                break;
                            FillSQLScript(nIndex++, ref sb);
                        }
                        EndFillSQLScript(ref sb);
                        sb.Append(");");
                        Execute(ref sb);
                    }
                    this.Status = true;

                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_INSERT_BATCHSQL);
                }

            }
            public String BinaryToBase64Str(String strFileName)
            {


                FileStream fs = new FileStream(strFileName, FileMode.Open);
                byte[] arr = new byte[fs.Length];
                fs.Read(arr, 0, (int)fs.Length);

                fs.Close();
                fs.Dispose();
                return Convert.ToBase64String(arr); ;
            }
            public byte[] Base64StrToBinary(String strBase64Binary)
            {

                return Convert.FromBase64String(strBase64Binary);
            }
            public Image Base64StrToImage(String strBase64Binary)
            {
                byte[] bytArr = Base64StrToBinary(strBase64Binary);

                MemoryStream ms = new MemoryStream(bytArr);
                Bitmap bmp = new Bitmap(ms);
                //bmp.Save(@"d:\"test.bmp", ImageFormat.Bmp);  
                //bmp.Save(@"d:\"test.gif", ImageFormat.Gif);  
                //bmp.Save(@"d:\"test.png", ImageFormat.Png);  
                ms.Close();
                return bmp;
            }
            public String DecodeImageToBase64Str(Image img)
            {
                GC.Collect();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                byte[] bytArr = ms.GetBuffer();
                return Convert.ToBase64String(bytArr);
            }

            public virtual void Dispose()
            {
                this.conn.Close();
                this.conn.Dispose();
                cmd.Dispose();
            }

#if TRANSACTION
            //protected abstract void TransactionConnect(out DbConnection cnn, out DbCommand cmm);

            //public void BeiginTransaction()
            //{
            //    try
            //    {
            //        TransactionConnect(out _cnn, out _cmd);
            //        tsn = _cnn.BeginTransaction();
            //        _cmd.Connection = _cnn;
            //        _cmd.Transaction = tsn;
            //    }
            //    catch (Exception e)
            //    {
            //        HandleError(e,_cmd, DBOperator.OPERATOR_AFFAIR_BEGIN);

            //    }

            //}
            //public void EndTransaction()
            //{
            //    try
            //    {
            //        tsn.Commit();
            //        _cnn.Close();
            //        _cnn.Dispose();
            //        _cmd.Dispose();
            //    }
            //    catch (Exception e)
            //    {
            //        tsn.Rollback();
            //        HandleError(e,_cmd, DBOperator.OPERATOR_AFFAIR_END);

            //    }
            //    finally
            //    {
            //        _cnn.Close();
            //        _cnn.Dispose();
            //        _cmd.Dispose();
            //    }

            //}
#endif
        }
        
    }
}

/*     使用DbParameter 来存储二进制数据及解析二进制数据
 * 
 *             public virtual void StoreBinary(String strTablename,
                String strInsertValues,
                string strImgName)
            {
                try
                {
                    cmd.CommandText =
                    String.Format(
                    "insert into {0} values({1}) ;"
                    , strTablename, strInsertValues);
                    FileStream fs = new FileStream(strImgName, FileMode.Open);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    fs.Close();
                    DbPar.Value = buffer;
                    cmd.Parameters.Add(DbPar);
                    int nAffectedRows = cmd.ExecuteNonQuery();
                    this.Status = true;
                }
                catch (Exception e)
                {
                    
                  HandleError(e, cmd, DBOperator.OPERATOR_STORE_BINARY);
                }
                
                 
            }
            //example :cmpct.StoreBinary("la", "'Canny',@data", "E:/Canny.jpg");
            public void StoreBinary(String strTablename,
               String strInsertValues,
               Stream sm)
            {
                try
                {
                    cmd.CommandText =
                    String.Format(
                    "insert into {0} values({1}) ;"
                    , strTablename, strInsertValues);
                    BinaryReader bin = new BinaryReader(sm);
                    byte[] buffer = new byte[bin.BaseStream.Length];
                    bin.Read(buffer, 0, (int)bin.BaseStream.Length);
                    bin.Close();
                    bin.BaseStream.Dispose();
                    DbPar.Value = buffer;
                    cmd.Parameters.Add(DbPar);
                    int nAffectedRows = cmd.ExecuteNonQuery();
                    this.Status = true;
                }
                catch (Exception e)
                {

                    HandleError(e, cmd, DBOperator.OPERATOR_STORE_BINARY);
                }


            }
             public void FetchBinary(string strTableName,
                                string ColumnName,
                               string strRequirement, 
                               out Stream FetchedStream)
            {
                FetchedStream = null;
                try
                {
                    
                    cmd.CommandText =
                        String.Format(
                        "Select {0} from {1} where {2} ;"
                        , ColumnName, strTableName, strRequirement);
                    byte[] bytArr = (byte[])cmd.ExecuteScalar();
                    FetchedStream = new MemoryStream(bytArr.Length);
                    FetchedStream.Write(bytArr, 0, bytArr.Length);
                    this.Status = true;
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_FETCH_BINARY);
                }
            }
            public void FetchBinary(string strTableName,
                    string ColumnName,
                   string strRequirement,
                   String strSaveName)
            {
                 
                try
                {

                    cmd.CommandText =
                        String.Format(
                        "Select {0} from {1} where {2} ;"
                        , ColumnName, strTableName, strRequirement);
                    byte[] bytArr = (byte[])cmd.ExecuteScalar();
                    FileStream fs = new FileStream(strSaveName, FileMode.CreateNew);
                    fs.Write(bytArr, 0, bytArr.Length);
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                    this.Status = true;
                }
                catch (Exception e)
                {
                    HandleError(e, cmd, DBOperator.OPERATOR_FETCH_BINARY);
                }
            }
     
     */
