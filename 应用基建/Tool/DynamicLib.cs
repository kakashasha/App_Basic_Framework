using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab
{
    namespace Common
    {
        public class DynamicLib
        {
            protected Assembly assembly; //利用dll的路径加载,同时将此程序集所依赖的程序集加载进来,需后辍名.dll
            protected string strDllName_;
            public bool Load(string strDllPath)
            {
                try
                {

                    //    Assembly.LoadFile 只加载指定文件，并不会自动加载依赖程序集.Assmbly.Load无需后辍名
                    assembly = Assembly.LoadFrom(strDllPath);
                    strDllName_ = strDllPath;
                    return true;
                    
                }
                catch (Exception e)
                {

                    MessageBox.Show(
                       String.Format("调用外部组件{0}失败\r\n失败原因{1}",
                       strDllPath, e.Message)
                       , "提示", MessageBoxButtons.OK, MessageBoxIcon.Error
                       );
                    return false;
                }
            }
            
            public Object GetInstance(string strClassName)
            {
                try
                {
                Type type_Instance = assembly.GetType(strClassName);//用类型的命名空间和名称获得类型
                object obj = Activator.CreateInstance(type_Instance);
                return obj;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(
                       String.Format("调用外部组件{0}失败\r\n失败原因{1}",
                       strDllName_, ex.Message)
                       , "提示", MessageBoxButtons.OK, MessageBoxIcon.Error
                       );
                    return null;
                }
            }
            //public void EndLoad()
            //{
            //    try
            //    {
            //        obj = Activator.CreateInstance(ExternalClass);//利用指定的参数实例话类型}                                      
            //        //FetchMethods();
            //        this.bIsLoadSucceed = true;

            //    }
            //    catch (Exception e)
            //    {

            //        MessageBox.Show(
            //           String.Format("调用外部组件{0}失败\r\n失败原因{1}",
            //           strDllName_, e.Message)
            //           , "提示", MessageBoxButtons.OK, MessageBoxIcon.Error
            //           );
            //        this.bIsLoadSucceed = false;
            //    }
            //}

            public bool CallMethod(object obj,string strMethodName,params object [] objArr_Params)
            {
                try
                {
                    MethodInfo mif = obj.GetType().GetMethod(strMethodName);
                    if (mif == null)
                        return false;
                    mif.Invoke(obj, objArr_Params);
                    return true;
                }
                catch (System.Exception ex)
                {
                    return false;  
                }
               
            }
            public bool CallProperty(object obj, string strPropertyName, string PropertyVal)
            {
                try
                {
                    PropertyInfo mif = obj.GetType().GetProperty(strPropertyName);
                    if (mif == null)
                        return false;
                    mif.SetValue(obj, PropertyVal,null);
                    return true;
                }
                catch (System.Exception ex)
                {
                    return false;
                }
            }
            
           
        }
         
    }
}