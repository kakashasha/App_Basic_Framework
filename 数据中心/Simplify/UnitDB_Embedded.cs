using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Data;
using System.Data.Common;
namespace dotNetLab.Data
{
    namespace Uniting
    {
		public    partial class UnitDB 
        {
           
            //Passed Data Operate Info
            public enum NotifyMessages { Read, Write, NoControlRead, NoControlWrite}
            public delegate void DBDataNotifyCallback(Control c, NotifyMessages nmgs,params string [] strArr);
            public  DBDataNotifyCallback Global_DBDataNotify = null;
			protected bool bConnected = false;
			protected bool bExistFile = false;
			public const char SPLITMARK = '^';
			string strTargetTable = "";
            public const string FIELD_VALUE = "Val";
            public const string FIELD_NAME = "Name";
            protected const string BADCONNECTION = "未能连接本地数据库";
            int nError = -99999;
            // String str = "User=SYSDBA;Password=masterkey;Database=ray_linn.fdb;Charset=NONE;ServerType=1";

            public void Write(string strTableName, string strName, String strValue)
            {
                if (!bConnected)
                {
                    HandleError(null, null, DBOperator.OPERATOR_CONNECT_DB);
                    return;
                }
                if (IsExistRecord(strName, strTableName))
                    this.Update(strTableName, string.Format("Val='{0}' Where Name='{1}' ", strValue, strName));
                else
                    this.NewRecord(strTableName, string.Format("'{0}','{1}'", strName, strValue));
                if (Global_DBDataNotify != null)
                {

                    Global_DBDataNotify(null, NotifyMessages.NoControlWrite,strTableName,strName,strValue);
                }
            }
            public string FetchValue(string strLabelName, string strTableName)
            {
                if (!bConnected)
                {
                    HandleError(null, null, DBOperator.OPERATOR_CONNECT_DB);
                    return null;
                }
                if (Global_DBDataNotify != null)
                {

                    Global_DBDataNotify(null, NotifyMessages.NoControlRead, strTableName, strLabelName);
                }
                return this.UniqueResult(
                    string.Format("select Val from {0} where Name='{1}'; "
                    , strTableName, strLabelName));
               
            }
            public void Write(string strName, string strValue)
            {
                Write(TargetTable, strName, strValue);
            }
            public string FetchValue(string strLabelName)
            {
                if (!bConnected)
                {
                    HandleError(null, null, DBOperator.OPERATOR_CONNECT_DB);
                    return null;
                }
                if (Global_DBDataNotify != null)
                {

                    Global_DBDataNotify(null, NotifyMessages.NoControlRead, TargetTable, strLabelName);
                }
                return this.UniqueResult(
                    string.Format("select Val from {0} where Name='{1}'; "
                    , TargetTable, strLabelName));
            }
            public int FetchIntValue(string strLabelName)
            {
                string temp = FetchValue(strLabelName);
                try
                {
                    return int.Parse(temp);
                }
                catch (Exception e)
                {
                    this.strErrorInfo = e.ToString();
                    return nError;
                }


            }
            public int FetchIntValue(string strLabelName,string strTableName)
            {
                string temp = FetchValue(strLabelName, strTableName);
                try
                {
                    return int.Parse(temp);
                }
                catch (Exception e)
                {
                    this.strErrorInfo = e.ToString();
                    return nError;
                }


            }
            public float FetchFloatValue(string strLabelName)
            {
                string temp = FetchValue(strLabelName);
                try
                {
                    return float.Parse(temp);
                }
                catch (Exception e)
                {
                    this.strErrorInfo = e.ToString();
                    return nError;
                }
            }
            public double FetchDoubleValue(string strLabelName)
            {
                string temp = FetchValue(strLabelName);
                try
                {
                    return double.Parse(temp);
                }
                catch (Exception e)
                {
                    this.strErrorInfo = e.ToString();
                    return nError;
                }

            }
            public DateTime FetchDateTimeValue(string strLabelName)
            {
                string temp = FetchValue(strLabelName);
                try
                {
                    return DateTime.Parse(temp);
                }
                catch (Exception e)
                {
                    this.strErrorInfo = e.ToString();
                    return DateTime.MinValue;
                }
            }
            public bool FetchBoolValue(string strLabelName)
            {
                string temp = FetchValue(strLabelName);
                try
                {
                    return Boolean.Parse(temp);
                }
                catch (Exception e)
                {
                    this.strErrorInfo = e.ToString();
                    return false;
                }
            }
            public void WriteArray<T>(String strTableName ,String strName, T[] tArr)
            {

                for (int i = 0; i < tArr.Length; i++)
                {
                    this.AppendItem( strTableName, strName, tArr[i].ToString());
                }

            }
            public void WriteArray<T>(String strName, T[] tArr)
            {
               
                WriteArray<T>(TargetTable, strName, tArr);

            }
            public void AppendItem(string strLabelName, String obj)
            {
                AppendItem(TargetTable, strLabelName, obj);
            }
            public void AppendItem(string strTableName, string strLabelName, String obj)
            {
                
                StringBuilder sb = new StringBuilder();
                String[] strArr_Write = null ;
                String[] strArr =   FetchArray(strTableName,strLabelName) ;
                if(obj==null)
                    return  ;
                if(strArr==null)
                {
                    if(obj==null)
                        return ;
                    String str = FetchValue(strTableName,strLabelName) ;
                    if(str==null||str.Equals(""))
                    {
                        Write(strTableName,strLabelName,obj.ToString());
                        return ;
                    }
                    else {
                        strArr_Write = new String[]{str, obj};
                    }

                }
                else {
                    strArr_Write = new String[strArr.Length + 1];
                    for (int i = 0; i < strArr.Length; i++) {
                        strArr_Write[i] = strArr[i] ;
                    }
                    strArr_Write[strArr_Write.Length-1] = obj ;
                }
                for (int i = 0; i < strArr_Write.Length; i++)
                {
                    if (i == 0)
                    {
                        sb.Append(strArr_Write[i] );
                    }
                    else
                        sb.Append(String.Format("{0}{1}", SPLITMARK, strArr_Write[i] ));
                }
                this.Write(strTableName,strLabelName, sb.ToString());
                sb.Remove(0,sb.Length) ;
                sb = null ;
               
            }
            public void AppendUniqueItem(String strTableName, String strLabelName, String obj)
            {
                String[] strArr_Write = null;
                String[] strArr = FetchArray(strTableName, strLabelName);
                if (obj == null)
                    return;
                if (strArr == null)
                {
                    if (obj == null)
                        return;
                    String str = FetchValue(strTableName, strLabelName);
                    if (str == null || str.Equals(""))
                    {
                        Write(strTableName, strLabelName, obj.ToString());
                        return;
                    }
                    else
                    {
                        strArr_Write = new String[] {str, obj};
                    }
                }
                else
                {
                    strArr_Write = new String[strArr.Length + 1];
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        strArr_Write[i] = strArr[i];
                    }
                    strArr_Write[strArr_Write.Length - 1] = obj;
                }
                StringBuilder sb = new StringBuilder();
                List<String> lst_Arr = new List<String>();
                //  String str = obj.ToString() ;
                if (strArr == null)
                {
                    for (int i = 0; i < strArr_Write.Length; i++)
                    {
                        if (lst_Arr.Contains(strArr_Write[i]))
                            continue;
                        lst_Arr.Add(strArr_Write[i]);
                    }
                }
                else
                {
                    for (int j = 0; j < strArr_Write.Length; j++)
                    {
                        if (!lst_Arr.Contains(strArr_Write[j]))
                            lst_Arr.Add(strArr_Write[j]);
                    }
                }


                for (int i = 0; i < lst_Arr.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.Append(lst_Arr[i]);
                    }
                    else
                        sb.Append(String.Format("{0}{1}", SPLITMARK, lst_Arr[i]));
                }
                this.Write(strTableName, strLabelName, sb.ToString());
                sb.Remove(0, sb.Length);
                sb = null;
            }
            
            public String[] FetchArray(string strLabelName)
            {

               /* String str = FetchValue(strLabelName);
                if (str.Contains(SPLITMARK.ToString()))
                    return str.Split(new char[] { SPLITMARK });
                else
                    return null;*/
                return FetchArray(DefaultTable, strLabelName);
            }
            public String[] FetchArray(String strTableName, string strLabelName)
            {

               /* String str = FetchValue(strLabelName, strTableName);
                if (str.Contains(SPLITMARK.ToString()))
                    return str.Split(new char[] { SPLITMARK });
                else
                    return null;*/
                String str = FetchValue( strLabelName,strTableName);
                     
                if(str== null)
                    return null ;
                else  {
                    if (str.Contains(SPLITMARK.ToString()))
                        try {
                            String [] str_  = str.Split('^');
                            return str_ ;
                        }
                        catch ( Exception e)
                        {
                          
                            return null ;
                        }
                    else
                        return null;
                }
            }
            public void FetchFloatArray(string strLabelName, out float[] fArr)
            {
                String[] pArr = FetchArray(strLabelName);
                fArr = new float[pArr.Length];
                for (int i = 0; i < fArr.Length; i++)
                {
                    fArr[i] = Convert.ToSingle(pArr[i]);
                }

            }
            public void FetchDoubleArray(string strLabelName, out double[] lfArr)
            {
                String[] pArr = FetchArray(strLabelName);
                lfArr = new double[pArr.Length];
                for (int i = 0; i < lfArr.Length; i++)
                {
                    lfArr[i] = Convert.ToDouble(pArr[i]);
                }
            }
            public void FetchIntArray(String strLabelName, out int[] arr)
            {
                String[] pArr = FetchArray(strLabelName);
                arr = new int[pArr.Length];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = Convert.ToInt32(pArr[i]);
                }
            }
            protected bool IsExistRecord(string strName, string Tablename)
            {
                string temp = this.UniqueResult(
                    string.Format("select count(*) from {0} where Name='{1}'"
                     , Tablename, strName
                     ));
                try
                {
                    if (int.Parse(temp) > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {

                    return false;
                }

            }
            public string TargetTable {
				get {
					return strTargetTable;
				}

				set {
					strTargetTable = value;
				}
			}
		}

    }
}
