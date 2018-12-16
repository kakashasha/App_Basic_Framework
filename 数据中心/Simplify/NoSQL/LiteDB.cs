using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
 
namespace dotNetLab.Data.Uniting
{
    public class LiteDBEx : NoSQLDB
    {
         Object db;

        public LiteDBEx()
        {
           if(!File.Exists("LiteDB.dll"))
            {
                UnitDB.AddRef("LiteDB.dll", dotNetLab.Data.DBEngines.LiteDB.LiteDBResource.LiteDB);
            }
             
        }

        public override bool Connect()
        {
            try
            {


             db =   System.Activator.CreateInstanceFrom("LiteDB.dll", "LiteDB.LiteDatabase", true, BindingFlags.Default, null, new object[] { this.DBName,null,null },null,null);
            
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public override List<string> TableSetNames
        {
            get
            {
                MethodInfo mif = db.GetType().GetMethod("GetCollectionNames", new Type[] { });
                IEnumerable<String> TableSetNames = mif.Invoke(db,null) as IEnumerable<String>;
                IEnumerator<String> myenumerator = TableSetNames.GetEnumerator();
                List<String> lst = new List<string>();
                while (myenumerator.MoveNext())
                {
                    lst.Add(myenumerator.Current);
                }
                return lst;
            }
        }
        //相同数据结构的表集合
        public override TableSet NewTableSet(String DataSetName)
        {
            try
            {
                TableSet tableSet = new TableSetEx();
                MethodInfo [] temps = db.GetType().GetMethods( BindingFlags.Instance|BindingFlags.Public);

                List<MethodInfo> mifs = new List<MethodInfo>();
                foreach (var item in temps)
                {
                    if (item.Name.Equals("GetCollection"))
                        mifs.Add(item);
                }
                if (mifs.Count == 3)

                {
                    mifs[2] = mifs[2].MakeGenericMethod(typeof(Table));
                    tableSet.Host = mifs[2].Invoke(db, new object[] { DataSetName });
                }
                else
                    return null;


            
                return tableSet;
            }
            catch (Exception ex)
            {
                return null;
                
            }
           

          
        }

        public override TableSet GetTableSet(String DataSetName)
        {
            MethodInfo[] temps = db.GetType().GetMethods();

            List<MethodInfo> mifs = new List<MethodInfo>();
            foreach (var item in temps)
            {
                if (item.Name.Equals("GetCollection"))
                    mifs.Add(item);
            }
            if (mifs.Count == 3)

            {
                var TableSetM = mifs[0].Invoke(db,new object[] { DataSetName });
                TableSet tableSet = new TableSetEx();
                tableSet.Host = tableSet;

                return tableSet;
            }
            else
                return null;



        }

        internal class TableSetEx : TableSet
        {
            List<Object> IDs;
            public override Table this[string index]
            {

                get
                {
                    return GetTable(index);
                }
                set
                {
                    Table tbl = null;
                    foreach (var item in Tables)
                    {
                        if (item.Name.Equals(index))
                            tbl = item;
                    }
                    tbl = value;
                }

            }
            public override Table this[int index]
            {
                get { return Tables[index]; }
                set
                {
                    Tables[index] = value;
                }
            }

            public override Table GetTable(string sName)
            {
                Table tbl = null ;
                foreach (var item in Tables)
                {
                    if (item.Name.Equals(sName))
                      tbl = item;
                }
                return tbl;
            }

            public override Table NewTable (Type type_Table, string tableName)
            {
                try
                {
                    Table tbl = System.Activator.CreateInstance(type_Table) as Table;
                    MethodInfo mif = this.Host.GetType().GetMethod("Insert", new Type[] { typeof(Table) });
                    Object obj = mif.Invoke(Host, new object[] { tbl });
                    this.Tables.Add(tbl);
                    if (IDs == null)
                        IDs = new List<object>();
                    IDs.Add(obj);
                    return tbl;
                }
                catch (Exception ex)
                {

                    return null;
                }
               
                 
            }
            public override void RemoveTable(String TableName)
            {
                try
                {
                    Table table = null;
                    foreach (var item in Tables)
                    {
                        if (item.Name.Equals(TableName))
                        {
                            table = item;
                            break;
                        }
                    }
                    int nIndex = this.Tables.IndexOf(table);


                    MethodInfo[] mifs = this.Host.GetType().GetMethods();
                    List<MethodInfo> deleteMethods = new List<MethodInfo>();
                    foreach (var item in mifs)
                    {
                        if (item.Name.Equals("Delete"))
                            deleteMethods.Add(item);
                    }

                    if (deleteMethods.Count == 3)
                    {
                        deleteMethods[2].Invoke(Host, new object[] { IDs[nIndex] });
                    }
                    else
                    {
                        Console.Write("Rmove Table Failed !");
                        return;
                    }


                    Tables.Remove(table);
                    IDs.RemoveAt(nIndex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " +ex.StackTrace);
                }
               
            }

            //DataSet相同类型的表集合，obj 表（非传统）
            public override bool UpdateTable (  Object table)
            {
                // bool b = DataSet.Update(obj);
                try
                {


                    Type type_Table = table.GetType();

                    MethodInfo mif = this.Host.GetType().GetMethod("Update", new Type[] { typeof(Table) });
                    return (bool)mif.Invoke(Host, new object[] { table });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + ex.StackTrace);
                    return false;
                }

            }
        }


    }
}

/*
 原文:C# 泛型 无法将类型xx隐式转换为“T”

直接将泛型转为T是不能转换的 要先转Object

public static T GetValue<T>(string inValue)
       {
           if (typeof(T) == typeof(Bitmap))
           {
               return (T)(Object)new Bitmap(inValue);
           }
           else
           {
             //一般类型
               return (T)Convert.ChangeType(inValue, typeof(T));
           }
           throw new Exception("");
       }
     
     */
