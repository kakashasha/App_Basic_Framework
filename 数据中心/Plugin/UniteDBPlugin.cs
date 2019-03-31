using dotNetLab.Data.Uniting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace dotNetLab.Data 
{
    // Export Key-value table to  Classes
  public class UniteDBPlugin
    {
        /*
        public string {0}
        [
           get[ return {1}.FetchValue("{0}", "{2}");]
           set[ {1}.Write("{2}", "{3}", value);]
        ]
         */
        //DBName_VARIABLE 例如： App.CompactDB ;
        //0
        public  String  ExportTableProperties(UnitDB CompactDB,String DBName_VARIABLE = "App.CompactDB",String tblName= "App_Extension_Data_Table")
        {
            String[] strArr =  CompactDB.GetNameColumnValues(tblName).ToArray();
            StringBuilder sb = new StringBuilder();
            string format = CommonRes.Property_Format;
            for (int i = 0; i < strArr.Length; i++)
                sb.Append(string.Format(format, strArr[i], DBName_VARIABLE,  tblName, DBName_VARIABLE, tblName , strArr[i]));
            sb.Replace('[', '{');
            sb.Replace(']', '}');
            sb.Append("\r\n");
            return sb.ToString();
        }
        //1
        public String  GenerateClassString( String tblName,String str )
        {
            string format = CommonRes.TableClass;
           format = String.Format(format, tblName, str);
            format = format.Replace('[', '{');
            format = format.Replace(']', '}');
            return format;
        }
        //2
        public void GenerateCSFiles(string csFolderPath,String strTableName,String str)
        {
            try
            {

            File.WriteAllText(csFolderPath + "\\" + strTableName + ".cs", str, Encoding.UTF8);
            }
            catch (Exception ex)
            {

                
            }
        }

        
        
    }
}
