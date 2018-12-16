using System;
using System.Collections.Generic;
using System.Text;
namespace dotNetLab.Data.Uniting
{
    public abstract class NoSQLDB : DB
    {

        public abstract bool Connect();

        //public abstract Table NewTable(String DataSetName);
        //public abstract bool UpdateTable(Table tbl);

        public abstract TableSet NewTableSet(String TableSetName);
 
        public abstract List<String> TableSetNames { get; }
        public abstract TableSet GetTableSet(String DataSetName);

        //数据库单元结构
        public abstract class DBUnit
        {
            public String Name { get; set; }
        }
        public abstract class Table : DBUnit
        {
            public TableSet TableSet { get; set; }
            
        }
        public abstract class TableSet : DBUnit
        {
          public  Object Host;
            List<Table> tables;
            public List<Table> Tables
            {
                get { return tables; }
                set { tables = value; }
            }

            protected TableSet()
            {
                tables = new List<Table>();
            }
            //记得在新建表后将其添加到Tables中
            public abstract Table NewTable ( Type type_Table,String tableName);
         
          public abstract Table this[String index] { get;set; }
          public abstract Table this[int index] { get; set; }

            public abstract Table GetTable(String Name);
            public abstract bool UpdateTable(Object obj);
            public abstract void RemoveTable(String TableName);

        }
        // Open database (or create if not exits)
//using(var db = new LiteDatabase(@"MyData.db"))
//{
//    // Get customer collection
//    var customers = db.GetCollection<Customer>("customers");

//    // Create your new customer instance
//    var customer = new Customer
//    {
//        Name = "John Doe",
//        Phones = new string[] { "8000-0000", "9000-0000" },
//        IsActive = true
//    };

//    // Insert new customer document (Id will be auto-incremented)
//    customers.Insert(customer);

//    // Update a document inside a collection
//    customer.Name = "Joana Doe";

//    customers.Update(customer);

//    // Index document using a document property
//    customers.EnsureIndex(x => x.Name);

//    // Use Linq to query documents
//    var results = customers.Find(x => x.Name.StartsWith("Jo"));
//}
    }
}
