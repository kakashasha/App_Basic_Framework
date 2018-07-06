using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace dotNetLab.Data.Uniting
{
    public   partial class UnitDB : DBRoot 
    {
        protected static string LOCALDBNAME = "shikii";
        protected const string TABLENAME = "App_Extension_Data_Table";
        public const string EMBEDDEDTABLEDEF = "Name nvarchar(128) primary key,Val nvarchar(512) not null";
        public const string SQLCETABLEDEF =
       "Name national character varying(128) primary key ,Val  national character varying(512)";
        public string DefaultTable { get { return TABLENAME; } }
        public List<String> GetNameColumnValues(String strTableName)
        {
            try
            {
                DataTable dt = this.ProvideTable(String.Format("select Name from {0}; ", strTableName), DBOperator.OPERATOR_QUERY_ALL_TABLENAMES);
                if (dt == null)
                    return new List<string>() ;
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                        return  new  List<String>();
                }
                List<String> lst = new List<String>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   lst.Add( dt.Rows[i][0].ToString());
                }
                return lst;
            }
            catch (Exception e)
            {

                return null;
            }




        }
       

        public virtual void CopyKeyValueTable(string newTableName, string oldTableName)
        {
            CreateKeyValueTable(newTableName);
            this.ExecuteNonQuery(
                string.Format("insert into {0} SELECT * FROM {1}"
                , newTableName, oldTableName)
                , DBOperator.OPERATOR_INSERT_BATCHSQL);
        }
    }
}
