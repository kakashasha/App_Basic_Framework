using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Windows.Forms;
 
using System.IO;

namespace dotNetLab.Vision.Halcon 
{
   public abstract class Invoker:IDisposable
    {
       protected Type type;
        Object host;
        protected BindingFlags NoPublicBindingFlag = BindingFlags.Instance | BindingFlags.NonPublic;
        public String Name;
       
        public string DllPath =  "";
        public string FullClassName = "";
        public  object Host
        {
            get { return host; }
            set
            {
                host = value;
                if (this.type == null)
                {
                    type = Host.GetType();
                    GetMembers();

                }
               
               
            }
        }
        protected abstract void GetMembers();

        protected MethodInfo GetMethod(string Name,params Type [] types )
        {
             return  type.GetMethod(Name, types);
        }
        protected EventInfo GetEvent(string Name)
        {
               
            return type.GetEvent(Name);
        }
        public static void AddRef(string strFileName, byte[] bytArr)
        {
            FileStream fs = new FileStream(strFileName, FileMode.Create);

            fs.Write(bytArr, 0, bytArr.Length);
            fs.Flush();
            fs.Close();
            fs.Dispose();

        }
        public void Dispose()
        {
            host = null;
        }
    }

}

