using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;

namespace dotNetLab.Data.Uniting
{
    public enum DBEngineNames
    {
        SQLITE,SQLCE,FireBird,LOCALDB,
        SQLSERVER,POSTGRESQL,MySQL,Oracle
    }
    public partial class UnitDB : DBRoot 
    {

       protected DBEngineNames EngineName;
        #region All Kinds Of DBEngine Connection Strings
        public readonly String SQLSERVERCONNECTIONSTRING = "server={0},{1};Initial Catalog={2};User ID={3};Password ={4}";
        public readonly String LOCALDBCONNECTIONSTRING = "server=(localdb)\\MSSQLLocalDB;AttachDBFilename={0} ;";
        public readonly String POSTGRESQLCONNECTIONSTRING = "server={0};username={1};database={2};port={3};password={4};";
        public readonly String ORACLECONNECTIONSTRING = "user id={0};password={1};data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST={2})(PORT={3}))(CONNECT_DATA=(SERVICE_NAME={4})))";
        public readonly String MYSQLCONNECTIONSTRING = "server={0};user={1};database={2};port={3};password={4};";
        public readonly String FIREBIRDCONNECTIONSTRING = "User=SYSDBA;Password=masterkey;Database={0};Charset=utf8;ServerType=1";
        public readonly String SQLCECONNECTIONSTRING = "data source={0};";
        public readonly String SQLITEConnectionString = "data source={0}";

        #endregion
        // For Huge DB
         public virtual void Connect(DBEngineNames EngineName,String IP,int Port,String UserName,String Pwd,String dbName)
        {
            this.EngineName = EngineName;
             String strConn = null;
            this.TargetTable = TABLENAME;
            switch (EngineName)
            {

                case DBEngineNames.SQLSERVER: 
                    PrepareSQLServerDB();
                    strConn = String.Format(SQLSERVERCONNECTIONSTRING,IP,Port,DBName,UserName,Pwd) ;
                    break;
                case DBEngineNames.POSTGRESQL:
                    PreparePostgresqlDB();
                    strConn = string.Format(POSTGRESQLCONNECTIONSTRING, IP, UserName, dbName, Port, Pwd);
                    break;
                case DBEngineNames.MySQL:
                    PrepareMySQLDB();
                    strConn = string.Format(MYSQLCONNECTIONSTRING, IP, UserName, dbName, Port, Pwd);
                    break;
                
              

                    
            }

            
                if (strConn != null && conn != null)
                {
                    
                    base.Connect(IP, Port, dbName, UserName, Pwd, strConn, ref this.conn, ref this.cmd);
                    ExecuteNonQuery(String.Format("use {0};", dbName),DBOperator.OPERATOR_CONNECT_DB);
                    CheckDefaultTableExist();
                }
                else
                {
                    this.Status = false;
                    HandleError(null, null, DBOperator.OPERATOR_CONNECT_DB);
                }
            
        }
        //For EmbeddedDB Using
        public void Connect(DBEngineNames  Enginename, string DBFilePath)
        {

            try
            {
                this.EngineName = Enginename;
                this.TargetTable = TABLENAME;
                this.DBName = DBFilePath;
                if (conn != null)
                {
                    try
                    {
                        conn.Close();
                        conn.Dispose();
                        cmd.Dispose();
                    }
                    catch (System.Exception ex)
                    {
                    	
                    }
                    
                    // prepareDB();
                }
                bExistFile = true;
                PrepareEmbeddedDB(Enginename);
                conn.Open();
                cmd.Connection = conn;
                this.bConnected = true;
                this.Status = true;
               
                CheckDefaultTableExist();
            }
            catch (Exception e)
            {
                HandleError(e, cmd, DBOperator.OPERATOR_CONNECT_DB);
            }
        }
        public void Connect(DBEngineNames Enginename)
        {
            Connect(Enginename, LOCALDBNAME);
        }
        public void Connect()
        {
            Connect(DBEngineNames.SQLITE);
        }
        void  CheckDefaultTableExist()
         {
             GetAllTableNames();
             int nIndex  = -1;
             //String [] strArr = new String [AllTableNames.Count] ;
             for (int i = 0; i < AllTableNames.Count; i++)
             {
               // APP_EXTENSION_DATA_TABLE
                 if (AllTableNames[i].ToLower().Trim().Equals(TargetTable.ToLower()))
                 {
                     nIndex = 1;
                     return;
                 }
                  
             }

              
             if (nIndex == -1)
             {
                 
                     CreateKeyValueTable(TargetTable);
                 
            }
        }  
        public void CreateKeyValueTable(String strTableName)
        {
            switch (EngineName)
            {
                case DBEngineNames.SQLITE: NewKeyValueTable(strTableName);
                    break;
                case DBEngineNames.SQLCE: NewSQLCEKeyValueTable(strTableName);
                    break;
                case DBEngineNames.FireBird: NewSpecialKeyValueTable(strTableName);
                    break;
                case DBEngineNames.LOCALDB: NewKeyValueTable(strTableName);
                    break;
                case DBEngineNames.SQLSERVER: NewKeyValueTable(strTableName);
                    break;
                case DBEngineNames.POSTGRESQL: NewSpecialKeyValueTable(strTableName);
                    break;
                case DBEngineNames.MySQL: NewSpecialKeyValueTable(strTableName);
                    break;
              
            }
        }
        #region  创建键值表

        void NewKeyValueTable(string strTableName)
        {
            this.NewTable(strTableName, EMBEDDEDTABLEDEF);
        }
        void NewSpecialKeyValueTable(string strTableName)
           {
               this.NewTable(strTableName, "Name varchar(128) primary key, val varchar(512) not null");
           }
        void NewSQLCEKeyValueTable(string strTableName)
           {
               this.NewTable(strTableName, UnitDB.SQLCETABLEDEF);
           }
#endregion
        
          #region   初始化数据库
          #region Huge DB
          //待定
          void PrepareOracleDB()
          {
              //this.conn = new OracleConnection();
              //this.cmd = new OracleCommand();
              //this.DbPar = new OracleParameter("data", System.Data.OracleClient.OracleType.Blob);
          }
       protected   void PreparePostgresqlDB()
          {
              if (!File.Exists("Npgsql.dll"))
              {
                  AddRef("npgsql_2_2_7.nupkg", dotNetLab.Data.DBEngines.PostgreSQL.PostgreSQLRes.npgsql_2_2_7);
                  dotNetLab.Data.Tipper.Tip_Info_Error("请在程序目录下解压nuget压缩包，并选一个dll，放于目录下。");
                  return;
              }
              object obj = this.GetReflectOject("Npgsql.dll", "Npgsql.NpgsqlConnection");

              this.conn =
                  obj
                  as DbConnection;
              this.DbPar =
                  GetReflectOject("Npgsql.dll", "Npgsql.NpgsqlParameter")
                  as DbParameter;
              this.cmd =
                GetReflectOject("Npgsql.dll", "Npgsql.NpgsqlCommand")
                 as DbCommand;

              //this.DbPar =
              //new NpgsqlParameter("data",
              //NpgsqlTypes.NpgsqlDbType.Bytea);
              this.DbPar.ParameterName = "Data";
              this.DbPar.Value = System.Data.SqlDbType.Image;
          }
        protected  void PrepareSQLServerDB()
          {
              this.conn = new SqlConnection();
              this.cmd = new SqlCommand();
              this.DbPar = new SqlParameter();
              this.DbPar.ParameterName = "Data";
              this.DbPar.Value = System.Data.SqlDbType.Image;
          }
       protected   void PrepareMySQLDB()
          {
              if (!File.Exists("MySql.Data.dll"))
              {
                  AddRef("MySql.Data.dll", dotNetLab.Data.DBEngines.MySQL.MySQLDBRes.MySql_Data);
              }
              object obj = this.GetReflectOject("MySql.Data.dll", "MySql.Data.MySqlClient.MySqlConnection");

              this.conn =
                  obj
                  as DbConnection;
              this.DbPar =
                  GetReflectOject("MySql.Data.dll", "MySql.Data.MySqlClient.MySqlParameter")
                  as DbParameter;
              this.cmd =
                GetReflectOject("MySql.Data.dll", "MySql.Data.MySqlClient.MySqlCommand")
                 as DbCommand;

              //this.DbPar =
              //new MysqlParameter("data",
              //MysqlTypes.MysqlDbType.Bytea);
              this.DbPar.ParameterName = "Data";
              this.DbPar.Value = System.Data.SqlDbType.Image;
          }
#endregion
          #region  Embedded DB
          //LocalDB
          void PrepareLocalDB()
          {
              this.conn = new SqlConnection();
              this.cmd = new SqlCommand();
              conn.ConnectionString =
                    string.Format(this.LOCALDBCONNECTIONSTRING,
                    this.DBName
                    );
              DbPar = new SqlParameter();
              this.DbPar.ParameterName = "Data";
              this.DbPar.Value = System.Data.SqlDbType.Image;

          }
          //SQLite
          void CheckSQLiteRefFiles()
          {
              
              if (!File.Exists("System.Data.SQLite.dll"))
              {
                  AddRef("System.Data.SQLite.dll", dotNetLab.Data.DBEngines.SQLite.SqliteDBResource.System_Data_SQLite);
              }
          }
          void PreparesQLiteDB()
          {
              CheckSQLiteRefFiles();
              this.conn =
                 this.GetReflectOject("System.Data.SQLite.dll", "System.Data.SQLite.SQLiteConnection")
                  as DbConnection;
              this.DbPar =
                  GetReflectOject("System.Data.SQLite.dll", "System.Data.SQLite.SQLiteParameter")
                  as DbParameter;
              this.cmd =
                GetReflectOject("System.Data.SQLite.dll", "System.Data.SQLite.SQLiteCommand")
                 as DbCommand;
              this.DbPar.ParameterName = "Data";
              this.DbPar.Value = System.Data.SqlDbType.Image;


              conn.ConnectionString = string.Format("data source={0}", this.DBName);


          }
          //Firebird
          void CheckFirebirdRefFiles()
          {
              if (!File.Exists("shikii"))
              {
                  AddRef("shikii", dotNetLab.Data.DBEngines.FireBirdDB.FireBirdDBRes.shikii);
              }
              if (!File.Exists("FirebirdSql.Data.FirebirdClient.dll"))
              {
                  AddRef("FirebirdSql.Data.FirebirdClient.dll", dotNetLab.Data.DBEngines.FireBirdDB.FireBirdDBRes.FirebirdSql_Data_FirebirdClient);
              }
              if (!File.Exists("fbembed.dll"))
              {
                  AddRef("fbembed.dll", dotNetLab.Data.DBEngines.FireBirdDB.FireBirdDBRes.fbembed);
              }
              if (!File.Exists("icudt30.dll"))
              {
                  AddRef("icudt30.dll", dotNetLab.Data.DBEngines.FireBirdDB.FireBirdDBRes.icudt30);
              }
              if (!File.Exists("icuin30.dll"))
              {
                  AddRef("icuin30.dll", dotNetLab.Data.DBEngines.FireBirdDB.FireBirdDBRes.icuin30);
              }
              if (!File.Exists("icuuc30.dll"))
              {
                  AddRef("icuuc30.dll", dotNetLab.Data.DBEngines.FireBirdDB.FireBirdDBRes.icuuc30);
              }
              if (!File.Exists("ib_util.dll"))
              {
                  AddRef("ib_util.dll", dotNetLab.Data.DBEngines.FireBirdDB.FireBirdDBRes.ib_util);
              }

          }

          void PrepareFirebirdDB()
          {
              CheckFirebirdRefFiles();

              object obj = this.GetReflectOject("FirebirdSql.Data.FirebirdClient.dll", "FirebirdSql.Data.FirebirdClient.FbConnection");

              this.conn =
                  obj
                  as DbConnection;
              this.DbPar =
                  GetReflectOject("FirebirdSql.Data.FirebirdClient.dll", "FirebirdSql.Data.FirebirdClient.FbParameter")
                  as DbParameter;
              this.cmd =
                GetReflectOject("FirebirdSql.Data.FirebirdClient.dll", "FirebirdSql.Data.FirebirdClient.FbCommand")
                 as DbCommand;
              this.DbPar.ParameterName = "Data";
              this.DbPar.Value = System.Data.SqlDbType.Image;

              conn.ConnectionString = string.Format("User=SYSDBA;Password=masterkey;Database={0};Charset=utf8;ServerType=1", this.DBName);

              if (!File.Exists(this.DBName))
              {
                  try
                  {

                      obj.GetType().GetMethod("CreateDatabaseImpl", BindingFlags.NonPublic | BindingFlags.Static).Invoke(obj, new object[] { conn.ConnectionString, 4096, true, true });
                  }
                  catch (System.Exception ex)
                  {
                      HandleError(ex, this.cmd, DBOperator.OPERATOR_NEW_DB);
                  }
              }

          }
          //SQLCE
          void CheckSQLCERefFiles()
          {
              if (!File.Exists("shikii"))
              {
                  AddRef("shikii", dotNetLab.Data.DBEngines.SQLCE.SqlCERes.shikii);
              }
              if (!File.Exists("System.Data.SqlServerCe.dll"))
              {
                  AddRef("System.Data.SqlServerCe.dll", dotNetLab.Data.DBEngines.SQLCE.SqlCERes.System_Data_SqlServerCe);
              }
              if (!File.Exists("sqlceqp40.dll"))
              {
                  AddRef("sqlceqp40.dll", dotNetLab.Data.DBEngines.SQLCE.SqlCERes.sqlceqp40);
              }
              if (!File.Exists("sqlcese40.dll"))
              {
                  AddRef("sqlcese40.dll", dotNetLab.Data.DBEngines.SQLCE.SqlCERes.sqlcese40);
              }
              if (!File.Exists("sqlceme40.dll"))
              {
                  AddRef("sqlceme40.dll", dotNetLab.Data.DBEngines.SQLCE.SqlCERes.sqlceme40);
              }
              if (!File.Exists("sqlceer40EN.dll"))
              {
                  AddRef("sqlceer40EN.dll", dotNetLab.Data.DBEngines.SQLCE.SqlCERes.sqlceer40EN);
              }
              if (!File.Exists("sqlceca40.dll"))
              {
                  AddRef("sqlceca40.dll", dotNetLab.Data.DBEngines.SQLCE.SqlCERes.sqlceca40);
              }
              if (!File.Exists("sqlcecompact40.dll"))
              {
                  AddRef("sqlcecompact40.dll", dotNetLab.Data.DBEngines.SQLCE.SqlCERes.sqlcecompact40);
              }
          }

          void PrepareSQLCEDB()
          {
              CheckSQLCERefFiles();
              object obj = this.GetReflectOject("System.Data.SqlServerCe.dll", "System.Data.SqlServerCe.SqlCeConnection");

              this.conn =
                  obj
                  as DbConnection;
              this.DbPar =
                  GetReflectOject("System.Data.SqlServerCe.dll", "System.Data.SqlServerCe.SqlCeParameter")
                  as DbParameter;
              this.cmd =
                GetReflectOject("System.Data.SqlServerCe.dll", "System.Data.SqlServerCe.SqlCeCommand")
                 as DbCommand;
              this.DbPar.ParameterName = "Data";
              this.DbPar.Value = System.Data.SqlDbType.Image;
           
              conn.ConnectionString = string.Format("data source={0};", this.DBName);
            if (!File.Exists(DBName))
            {
                Object objEngine = GetReflectOject("System.Data.SqlServerCe.dll", "System.Data.SqlServerCe.SqlCeEngine");
                objEngine.GetType().GetProperty("LocalConnectionString").SetValue(objEngine, conn.ConnectionString, null);
                objEngine.GetType().GetMethod("CreateDatabase").Invoke(objEngine, null);
                IDisposable disposeObj = objEngine as IDisposable;
                disposeObj.Dispose();
            }
            if (!File.Exists(this.DBName))
              {
                  this.NewDB(this.DBName);
              }

          }
#endregion
          #endregion
          void PrepareEmbeddedDB(DBEngineNames Enginename)
        {
            switch (Enginename)
            {
                case DBEngineNames.SQLITE:
                    
                    PreparesQLiteDB();
                    break;
                case DBEngineNames.SQLCE:
                    PrepareSQLCEDB();
                    break;
                case DBEngineNames.FireBird:
                    PrepareFirebirdDB();
                    break;
                case DBEngineNames.LOCALDB:
                    PrepareLocalDB();
                    break;
                
                
            }
        }
        public void GetAllTableNames()
         {
            AllTableNames.Clear();
             switch (EngineName)
             {
                 case DBEngineNames.SQLITE:
                     SQLiteGetAllTableNames();
                     break;
                 case DBEngineNames.SQLCE:
                     SQLServerGetAllTableNames();
                     break;
                 case DBEngineNames.FireBird:
                     FirebirdGetAllTableNames();
                     break;
                 case DBEngineNames.LOCALDB:
                     SQLServerGetAllTableNames();
                     break;
                 case DBEngineNames.SQLSERVER:
                     SQLServerGetAllTableNames();
                     break;
                 case DBEngineNames.MySQL:
                     SQLServerGetAllTableNames();
                     break;
                 case DBEngineNames.POSTGRESQL:
                     PostGresqlGetTableNames();
                     break;
                     
             }
         }
        #region GetTableNames
        void PostGresqlGetTableNames()
        {
            AllTableNames.Clear();
            dt = this.ProvideTable("SELECT  tablename   FROM   pg_tables;", DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String strTableName = dt.Rows[i][0].ToString();
                if (!strTableName.Contains("pg_") && !strTableName.Contains("sql_"))
                    this.AllTableNames.Add(strTableName);
            }

        }
     
        void SQLServerGetAllTableNames()
        {
            try
            {
                //SELECT Name FROM SysObjects Where XType='U' ORDER BY Name
                AllTableNames.Clear();
                if(EngineName == DBEngineNames.MySQL)
                    dt = this.ProvideTable(String.Format("select Table_Name from information_schema.TABLES where table_schema = '{0}'", DBName), DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);
                else if(EngineName == DBEngineNames.SQLCE)
                    dt = this.ProvideTable(String.Format("select Table_Name from information_schema.TABLES", DBName), DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);
                else
                dt = this.ProvideTable(String.Format("select Table_Name from information_schema.TABLES where table_catalog = '{0}'", DBName), DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String strTableName = dt.Rows[i][0].ToString();
                     
                    this.AllTableNames.Add(strTableName);
                }
            }
            catch (System.Exception ex)
            {
            	
            }
           
           
            //information_schema.views,
          
        }
      
        void FirebirdGetAllTableNames()
        {
            dt =
        ProvideTable("SELECT RDB$RELATION_NAME AS TABLE_NAME FROM RDB$RELATIONS WHERE RDB$SYSTEM_FLAG = 0 or RDB$SYSTEM_FLAG = 1  AND RDB$VIEW_SOURCE IS NULL;",
              DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AllTableNames.Add(dt.Rows[i][0].ToString());
            }
         
        }
        void SQLiteGetAllTableNames()
        {

            try
            {
                dt = this.ProvideTable("select name from sqlite_master where type='table' or type='view' order by name;", DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AllTableNames.Add(dt.Rows[i][0].ToString());
                }
            }
            catch (Exception e)
            {
                HandleError(e, null, DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);
            }



        }

        #endregion

        public void GetAllViewNames()
        {
            String strSQL = "select table_Name from INFORMATION_SCHEMA.VIEWS ;";
            switch (EngineName)
            {
                case DBEngineNames.SQLITE:
                    strSQL =
"select name from sqlite_master where  type='view' order by name;"
;
                    break;
                case DBEngineNames.SQLCE:
                    ;
                    break;
                case DBEngineNames.FireBird:
                    strSQL =
"SELECT RDB$RELATION_NAME AS TABLE_NAME FROM RDB$RELATIONS WHERE   RDB$SYSTEM_FLAG = 1  AND RDB$VIEW_SOURCE IS NULL;"
;
                    break;
                case DBEngineNames.LOCALDB:
                    ;
                    break;
                case DBEngineNames.SQLSERVER:
                    ;
                    break;
                case DBEngineNames.POSTGRESQL:
                    strSQL =
"SELECT viewname FROM   pg_views  WHERE schemaname ='public' "
;
                    break;
                case DBEngineNames.MySQL:
                    ;
                    break;

            }
            dt = this.ProvideTable(strSQL, DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AllTableNames.Add(dt.Rows[i][0].ToString());
            }
        }
        void AddRef(string strFileName, byte[] bytArr)
        {
            FileStream fs = new FileStream(strFileName, FileMode.Create);

            fs.Write(bytArr, 0, bytArr.Length);
            fs.Flush();
            fs.Close();
            fs.Dispose();

        }
      public List<String> GetTableColumnNames(String strTableName)
        {
            AllColumnNames.Clear();
            switch (EngineName)
            {
                case DBEngineNames.SQLITE:
                    SQLiteGetTableColumnNames(strTableName);
                    
                    break;
                case DBEngineNames.SQLCE:
                    SQLSERVER_LOCALDB_SQLCE_GetTableColumnNames(strTableName);
                    break;
                case DBEngineNames.FireBird:
                    FirebirdGetTableColumnNames(strTableName);
                    break;
                case DBEngineNames.LOCALDB:
                    SQLSERVER_LOCALDB_SQLCE_GetTableColumnNames(strTableName);
                    break;
                case DBEngineNames.SQLSERVER:
                    SQLSERVER_LOCALDB_SQLCE_GetTableColumnNames(strTableName);
                    break;
                case DBEngineNames.POSTGRESQL:
                    break;
                case DBEngineNames.MySQL: MySQLGetTableColumnNames(strTableName);
                    break;
                
                 
            }
            if (AllColumnNames.Count >0)
                return AllColumnNames;
            else
                return null; 

        }
      #region GetTableColumnNames
        void SQLiteGetTableColumnNames(String strTableName)
        {
            AllColumnNames.Clear();
            string SQLFormat = "PRAGMA table_info({0})";
            dt = this.ProvideTable(String.Format(SQLFormat, strTableName), DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);
            for (int i = 0; i < dt.Rows.Count; i++)
                AllColumnNames.Add(dt.Rows[i][1].ToString());
        }
        void MySQLGetTableColumnNames(String strTableName)
        {
            String SQLFormat = "select COLUMN_NAME from information_schema.COLUMNS where table_name = '{0}' and TABLE_SCHEMA ='{1}'; ";
            dt = this.ProvideTable(String.Format(SQLFormat, strTableName,DBName), DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AllColumnNames.Add(dt.Rows[i][0].ToString());
            }
           
        }
        void SQLSERVER_LOCALDB_SQLCE_GetTableColumnNames(String strTableName)
        {
             
            if(EngineName != DBEngineNames.SQLCE)
               dt = this.ProvideTable(String.Format("select COLUMN_NAME from information_schema.COLUMNS where table_name = '{0}' and table_catalog ='{1}';", strTableName, DBName), DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);
            else
               dt = this.ProvideTable(String.Format("select COLUMN_NAME from information_schema.COLUMNS where table_name = '{0}';", strTableName), DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AllColumnNames.Add(dt.Rows[i][0].ToString());
            }

        }
        void PostgreSQLGetTableColumnNames(String strTableName)
        {
            String SQLFormat = "select column_name from information_schema.columns where table_name = '{0}' and table_catalog='{1}'";
            dt = this.ProvideTable(String.Format(SQLFormat, strTableName,DBName), DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AllColumnNames.Add(dt.Rows[i][0].ToString());
            }
        }
        void  FirebirdGetTableColumnNames(String strTableName)
        {
            String strFormat = "SELECT RDB$FIELD_NAME FROM RDB$RELATION_FIELDS WHERE  RDB$RELATION_NAME= '{0}' ;";
            dt = this.ProvideTable(String.Format(strFormat, strTableName.ToUpper()), DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AllColumnNames.Add(dt.Rows[i][0].ToString());
            }
        }

        #endregion

        public   List<bool> GetAllColumnTypes(String strTableName)
        {
            ColumnTypes.Clear();
            switch (EngineName)
            {
                case DBEngineNames.SQLITE: SQLiteGetColumnTypes(strTableName);
                    break;
                case DBEngineNames.SQLCE: SQLSERVERGetColumnTypes(strTableName);
                    break;
                case DBEngineNames.FireBird: FirebirdGetColumnTypes(strTableName);
                    break;
                case DBEngineNames.LOCALDB: SQLSERVERGetColumnTypes(strTableName);
                    break;
                case DBEngineNames.SQLSERVER: SQLSERVERGetColumnTypes(strTableName);
                    break;
                case DBEngineNames.POSTGRESQL: PostgresqlGetColumnTypes(strTableName);
                    break;
                case DBEngineNames.MySQL:
                    MysqlGetColumnTypes(  strTableName);
                    break;
                 
                 
            }
            if (ColumnTypes.Count > 0)
                return ColumnTypes;
            else
                return null;

        }
      #region GetAllColumnType
        void SQLiteGetColumnTypes(String strTableName)
         {
            StringBuilder sb = new StringBuilder () ;
          
             String str = UniqueResult(String.Format("select sql from sqlite_master where tbl_Name='{0}'",
                 strTableName));
              int nColumnTypes = 0 ;
             sb.Append( str.ToLower().Trim().Replace(strTableName.ToLower()," "));
              String [] strArr = sb.ToString().Split( new String [] { "table" },StringSplitOptions.None);
              if (strArr.Length > 1)
              {
                  sb.Remove(0, sb .Length - 1);
                   sb.Append(strArr[1].Trim()).Remove(0,1).Remove(sb.Length-1,1) ;
                     strArr = sb.ToString().Split(',') ;
                    nColumnTypes  = strArr.Length ;
                    String[] strX = null;
                     for (int i = 0; i < nColumnTypes; i++)
                     {
                         strX =  strArr[i].Trim().Split(' ');
                         for (int j = 0,X =0 ; j < strX.Length; j++)
                         {
                             if (!String.IsNullOrEmpty(strX[j]))
                             {

                                 X++;
                                 if (X > 1)
                                 {
                                     if (strX[j].Contains("char") || strX[j].Contains("text") || strX[j].Contains("time"))
                                         ColumnTypes.Add(true);
                                     else
                                     {
                                         ColumnTypes.Add(false);
                                     }
                                     break;
                                 }
                             }
                         }
                     }

                  
                   
               }
              sb.Remove(0, sb.Length - 1);
              
         }
        void SQLSERVERGetColumnTypes(String strTableName)
        {
            if(EngineName== DBEngineNames.SQLCE)
                dt = this.ProvideTable(String.Format("select data_type from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='{0}'  ", strTableName  ), DBOperator.OPERATOR_QUERY_TABLE);
           
            else
            dt = this.ProvideTable(String.Format("select data_type from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='{0}' and TABLE_CATALOG='{1}'", strTableName,DBName), DBOperator.OPERATOR_QUERY_TABLE);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String str = dt.Rows[i][0].ToString().ToLower();
                if (str.Contains("char") || str.Contains("text") || str.Contains("time"))
                    ColumnTypes.Add(true);   
                else
                {
                    ColumnTypes.Add(false);
                }
            }
        }
        void FirebirdGetColumnTypes(String strTableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT A.RDB$FIELD_NAME, B.RDB$FIELD_TYPE, B.RDB$FIELD_LENGTH, B.RDB$FIELD_PRECISION, B.RDB$FIELD_SCALE,C.RDB$TYPE_NAME,C.RDB$FIELD_NAME");
            sb.Append(" FROM RDB$RELATION_FIELDS A, RDB$FIELDS B, RDB$TYPES C WHERE A.RDB$RELATION_NAME = '{0}'");
            sb.Append(" AND A.RDB$FIELD_SOURCE = B.RDB$FIELD_NAME  AND C.RDB$TYPE = B.RDB$FIELD_TYPE AND C.RDB$FIELD_NAME='RDB$FIELD_TYPE';");
            dt = this.ProvideTable(String.Format(sb.ToString(), strTableName), DBOperator.OPERATOR_QUERY_TABLE);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String str = dt.Rows[i][5].ToString().ToLower();
                if (str.Contains("varying") || str.Contains("text") ||
                    str.Contains("time")||str.Contains("date")||str.Contains("cstring"))
                    ColumnTypes.Add(true);
                else
                {
                    ColumnTypes.Add(false);
                }
            }
        }
        void PostgresqlGetColumnTypes(String strTableName)
        {
            // select data_type from information_schema.columns-- where table_name = '{0}'
            dt = this.ProvideTable(String.Format("select data_type from information_schema.columns where table_name = '{0}'", strTableName), DBOperator.OPERATOR_QUERY_TABLE);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String str = dt.Rows[i][0].ToString().ToLower();
                if (str.Contains("char") || str.Contains("text") ||
                    str.Contains("time")|| str.Contains("date")||str.Contains("interval"))
                    ColumnTypes.Add(true);
                else
                {
                    ColumnTypes.Add(false);
                }
            }
        }
        void MysqlGetColumnTypes(String strTableName)
        {
            dt = this.ProvideTable(String.Format("select data_type from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='{0}' and TABLE_SCHEMA='{1}'", strTableName, DBName), DBOperator.OPERATOR_QUERY_TABLE);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String str = dt.Rows[i][0].ToString().ToLower();
                if (str.Contains("char") || str.Contains("text") || str.Contains("time")||str.Contains("date"))
                    ColumnTypes.Add(true);
                else
                {
                    ColumnTypes.Add(false);
                }
            }
        }
        #endregion


    }
}
