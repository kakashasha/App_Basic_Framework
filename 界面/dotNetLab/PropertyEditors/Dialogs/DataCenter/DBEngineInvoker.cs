using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data.Common;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{
    public enum DBEngineNames
    {
        SQLITE, SQLCE, FireBird, LOCALDB,
        SQLSERVER, POSTGRESQL, MySQL, Oracle
    }
    public enum DBOperator
    {
        OPERATOR_HEAD,
        OPERATOR_INSERT,
        OPERATOR_DROP_TABLE,
        OPERATOR_DROP_DB,
        OPERATOR_DELETE,
        OPERATOR_UPDATE,
        OPERATOR_QUERY_TABLE,
        OPERATOR_TRUNCATE,
        OPERATOR_CREATE_TABLE,
        OPERATOR_CONNECT_DB,
        OPERATOR_NEW_DB,
        OPERATOR_AFFAIR_BEGIN,
        OPERATOR_AFFAIR_END,
        OPERATOR_CONNECT_DB_AFFAIR,
        OPERATOR_QUERY_ALL_DBNAMES,
        OPERATOR_QUERY_ALL_TABLENAMES,
        OPERATOR_REMOVE_USER,
        OPERATOR_REMOVE_RECORD,
        OPERATOR_STORE_BINARY,
        OPERATOR_FETCH_BINARY,
        OPERATOR_INSERT_BATCHSQL,
        OPERATOR_NEW_TABLE,
        OPERATOR_NEW_VIEW,
        OPERATOR_EXECUTESCRIPT,
        OPERATOR_FAILED,
        OPERATOR_SUCCEED,
        OPERATOR_QUERY_UNIQUE,
        OPERATOR_QUERY_ALL_TABLENAMES_ARRAY,
        
    }
    public class DBEngineInvoker
    {
        Type type;
        Object objDBEngine;
        System.Reflection.PropertyInfo[] PropertyInfos;
        PropertyInfo ThisCommandProperty, TargetTableProperty, AllTableNamesProperty,DatabaseProperty;
        MethodInfo[] MethodInfos;
        public DbCommand ThisCommand
        {
            get { return (DbCommand)ThisCommandProperty.GetValue(objDBEngine, null); }

        }
        public List<String> AllTableNames
        {
            get { return (List<String>)AllTableNamesProperty.GetValue(objDBEngine, null); }

        }
       // public readonly String DataCenterDllPath = "D:/Projects/Shikii_New_App_Framework/DBManager/bin/Debug/shikii.dotNetLab.DataCenter.dll" ;
        PropertyInfo StatusPropertyInfo;
        public String TargetTable
        {
            get
            {
                return TargetTableProperty.GetValue(objDBEngine, null).ToString();
            }
            set
            {
                TargetTableProperty.SetValue(objDBEngine, value, null);
            }
        }
        public bool Status
        {
            get { return (bool)StatusPropertyInfo.GetValue(objDBEngine, null); }
            set
            {
                StatusPropertyInfo.SetValue(objDBEngine, value, null);

            }
        }
        public String Database
        {
            get { return (String)DatabaseProperty.GetValue(objDBEngine, null); }
            set
            {
                DatabaseProperty.SetValue(objDBEngine, value, null);

            }
        }
        public DBEngineInvoker()
        {
          //  DataCenterDllPath =@ "D:\Projects\Shikii_New_App_Framework\DBManager\bin\Debug\shikii.dotNetLab.DataCenter.dll";
            
             String strDir = Path.GetDirectoryName(Application.ExecutablePath);
              String  DataCenterDllPath = strDir + "\\shikii.dotNetLab.DataCenter.dll";

            
            Load(DataCenterDllPath, "dotNetLab.Data.Uniting.UIDBEngine");
        }
        void Load(String strAssemblyName, String ClassFullName_IncludeNamespace)
        {
            try
            {
                //  objDBEngine = System.Activator.CreateInstanceFrom(strAssemblyName, ClassFullName_IncludeNamespace);
                objDBEngine = Assembly.LoadFile(strAssemblyName).CreateInstance(ClassFullName_IncludeNamespace);
                type = objDBEngine.GetType();
                PropertyInfos = type.GetProperties();
                MethodInfos = type.GetMethods();


                AttachMembers();
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }


        }
        
        private void AttachMembers()
        {
            for (int i = 0; i < PropertyInfos.Length; i++)
            {
                if (PropertyInfos[i].Name.Contains("TargetTable"))
                {
                    TargetTableProperty = PropertyInfos[i];
                }
                if (PropertyInfos[i].Name.Equals("ThisDbCommand"))
                {
                    ThisCommandProperty = PropertyInfos[i];
                }
                if (PropertyInfos[i].Name.Equals("Status"))
                {
                    StatusPropertyInfo = PropertyInfos[i];
                }
                if (PropertyInfos[i].Name.Equals("AllTableNames"))
                {
                    AllTableNamesProperty = PropertyInfos[i];
                }
                if (PropertyInfos[i].Name.Equals("Database"))
                {
                    DatabaseProperty = PropertyInfos[i];
                }
            }

        }
        public void Connect()
        {

            try
            {
                MethodInfo mif = type.GetMethod("Connect", new Type[] { });
                mif.Invoke(objDBEngine, null);
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void Connect(DBEngineNames Enginename)
        {
            try
            {
                //MethodInfo mif = type.GetMethod("Connect", new Type[] { typeof(DBEngineNames) });
                MethodInfos = type.GetMethods();
                for (int i = 0; i < MethodInfos.Length; i++)
                {
                    if (MethodInfos[i].Name.Equals("Connect"))
                    {
                        if (MethodInfos[i].GetParameters().Length == 1)
                        {
                            MethodInfos[i].Invoke(objDBEngine, new Object[] { Enginename });
                            break;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void Connect(DBEngineNames Enginename, string DBFilePath)
        {
            try
            {
                // dotNetLab.Tipper.Tip_Info_Done("Enter This DB");
                MethodInfos = type.GetMethods();
                for (int i = 0; i < MethodInfos.Length; i++)
                {
                    if (MethodInfos[i].Name.Equals("Connect"))
                    {
                        if (MethodInfos[i].GetParameters().Length == 2)
                        {
                            //dotNetLab.Tipper.Tip_Info_Done(Enginename.ToString());
                            //  dotNetLab.Tipper.Tip_Info_Done(((int)Enginename).ToString());
                            MethodInfos[i].Invoke(objDBEngine, new Object[] { Enginename, DBFilePath });
                            break;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void Connect(DBEngineNames EngineName, string IP, int Port, string UserName, string Pwd, string dbName)
        {
            try
            {
                MethodInfos = type.GetMethods();
                for (int i = 0; i < MethodInfos.Length; i++)
                {
                    if (MethodInfos[i].Name.Equals("Connect"))
                    {
                        if (MethodInfos[i].GetParameters().Length == 6)
                        {
                            MethodInfos[i].Invoke(objDBEngine, new Object[] { EngineName, IP, Port, UserName, Pwd, dbName });
                            break;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void CopyKeyValueTable(string newTableName, string oldTableName)
        {

            try
            {
                MethodInfo mif = type.GetMethod("CopyKeyValueTable");
                mif.Invoke(objDBEngine, new Object[] { newTableName, oldTableName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void CreateKeyValueTable(string strTableName)
        {
            try
            {
                MethodInfo mif = type.GetMethod("CreateKeyValueTable");
                mif.Invoke(objDBEngine, new Object[] { strTableName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public String[] FetchArray(string strLabelName)
        {
            try
            {
                MethodInfo mif = type.GetMethod("FetchArray", new Type[] { typeof(String) });
                return (String[])mif.Invoke(objDBEngine, new Object[] { strLabelName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
                return null;
            }
        }
        public String[] FetchArray(string strTableName, string strLabelName)
        {
            try
            {
                MethodInfo mif = type.GetMethod("FetchArray", new Type[] { typeof(String), typeof(String) });
                return (String[])mif.Invoke(objDBEngine, new Object[] { strTableName, strLabelName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
                return null;
            }
        }
        public String FetchValue(string strLabelName)
        {
            try
            {
                MethodInfo mif = type.GetMethod("FetchValue", new Type[] { typeof(String) });
                return (String)mif.Invoke(objDBEngine, new Object[] { strLabelName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
                return null;
            }
        }
        public String FetchValue(string strLabelName, string strTableName)
        {
            try
            {
                MethodInfo mif = type.GetMethod("FetchValue", new Type[] { typeof(String), typeof(String) });
                return (String)mif.Invoke(objDBEngine, new Object[] { strLabelName, strTableName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
                return null;
            }

        }
        public void GetAllTableNames()
        {
            try
            {
                MethodInfo mif = type.GetMethod("GetAllTableNames");
                mif.Invoke(objDBEngine, null);
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);

            }
        }
        public string[] GetNameColumnValues(string strTableName)
        {
            try
            {
                MethodInfo mif = type.GetMethod("GetNameColumnValues");
                return (String[])mif.Invoke(objDBEngine, new Object[] { strTableName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
                return null;

            }
        }
        public void Write(string strName, string strValue)
        {
            try
            {
                MethodInfo mif = type.GetMethod("Write", new Type[] { typeof(String), typeof(String) });
                mif.Invoke(objDBEngine, new Object[] { strName, strValue });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);

            }
        }
        public void Write(string strTableName, string strName, string strValue)
        {
            try
            {
                MethodInfo mif = type.GetMethod("Write", new Type[] { typeof(String), typeof(String), typeof(String) });
                mif.Invoke(objDBEngine, new Object[] { strTableName, strName, strValue });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);

            }
        }
        public void NewDB(string DBName)
        {
            try
            {
                MethodInfo mif = type.GetMethod("NewDB");
                mif.Invoke(objDBEngine, new Object[] { DBName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void NewKeyValueView(string ViewName, string strFromTableName, string strColumnName_Name, string strColumnName_Val)
        {
            try
            {
                MethodInfo mif = type.GetMethod("NewKeyValueView");
                mif.Invoke(objDBEngine, new Object[] { ViewName, strFromTableName, strColumnName_Name, strColumnName_Val });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void NewRecord(string strTableName, string strValue)
        {
            try
            {
                MethodInfo mif = type.GetMethod("NewRecord");
                mif.Invoke(objDBEngine, new Object[] { strTableName, strValue });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void NewView(string ViewName, string strTableName, string strColumnNames)
        {
            try
            {
                MethodInfo mif = type.GetMethod("NewView");
                mif.Invoke(objDBEngine, new Object[] { ViewName, strTableName, strColumnNames });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void RemoveDB(string strDBName)
        {
            try
            {
                MethodInfo mif = type.GetMethod("RemoveDB");
                mif.Invoke(objDBEngine, new Object[] { strDBName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void RemoveRecord(string TableName, string strRequirement)
        {
            try
            {
                MethodInfo mif = type.GetMethod("RemoveRecord");
                mif.Invoke(objDBEngine, new Object[] { TableName, strRequirement });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public void RemoveTable(string strTableName)
        {
            try
            {
                MethodInfo mif = type.GetMethod("RemoveTable");
                mif.Invoke(objDBEngine, new Object[] { strTableName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
            }
        }
        public string UniqueResult(string sql)
        {
            try
            {
                MethodInfo mif = type.GetMethod("UniqueResult");
                return (String)mif.Invoke(objDBEngine, new Object[] { sql });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
                return null;
            }
        }
        public void Update(string TableName, string strColumnAssignAndRequirment)
        {
            try
            {
                MethodInfo mif = type.GetMethod("Update");
                mif.Invoke(objDBEngine, new Object[] { TableName, strColumnAssignAndRequirment });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);

            }
        }
        public void Dispose()
        {
            try
            {
                MethodInfo mif = type.GetMethod("Dispose");
                mif.Invoke(objDBEngine, null);
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);

            }
        }
        public List<String> GetTableColumnNames(String strTableName)
        {
            try
            {
                MethodInfo mif = type.GetMethod("GetTableColumnNames");
                return (List<String>)mif.Invoke(objDBEngine, new object[]{ strTableName});
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
                return null;

            }
        }
        public DataTable ProvideTable(string sql, DBOperator dbOperator)
        {
            try
            {
                MethodInfo mif = type.GetMethod("ProvideTable");
                return (DataTable)mif.Invoke(objDBEngine, new Object[] { sql, dbOperator });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
                return null;
            }
        }
        public List<bool> GetColumnTypes(string strTableName)
        {

            try
            {
                MethodInfo mif = type.GetMethod("GetAllColumnTypes");
                return (List<bool>)mif.Invoke(objDBEngine, new Object[] { strTableName });
            }
            catch (System.Exception ex)
            {
                dotNetLab.Tipper.Tip_Info_Error(ex.Message);
                return null;
            }
        }

    }
}
//void FetchBinary(string strTableName, string ColumnName, string strRequirement, out global::System.IO.Stream FetchedStream);
//void FetchBinary(string strTableName, string ColumnName, string strRequirement, string strSaveName);
//void StoreBinary(string strTablename, string strInsertValues, global::System.IO.Stream sm);
//void StoreBinary(string strTablename, string strInsertValues, string strImgName);
/*
 //bool FetchBoolValue(string strLabelName);
           //DateTime FetchDateTimeValue(string strLabelName);
           //void FetchDoubleArray(string strLabelName, out double[] lfArr);
           //double FetchDoubleValue(string strLabelName);
           //void FetchFloatArray(string strLabelName, out float[] fArr);
           //float FetchFloatValue(string strLabelName);
           //void FetchIntArray(string strLabelName, out int[] arr);
           //int FetchIntValue(string strLabelName);
           //int FetchIntValue(string strLabelName, string strTableName);
 */