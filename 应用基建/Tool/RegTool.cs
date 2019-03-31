using Microsoft.Win32;
using System;
using System.Collections.Generic;

using System.Text;


namespace dotNetLab
{
    namespace Tools
    {
        static class RegTool
        {

            static void Sample()
            {

                RegistryKey PathKey = Registry.LocalMachine;
                String strPath = @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment";
                //string[] strArrPath = strPath.Split(new char[] { '\\' });
                //foreach (var item in strArrPath)
                //{
                //    PathKey = PathKey.OpenSubKey(item,true);
                //}
                PathKey = PathKey.OpenSubKey(strPath, true);


                String str = PathKey.GetValue("Path").ToString();
                str += ";C:\\JAVA";

                PathKey.SetValue("Path", str, RegistryValueKind.String);
                PathKey.Close();
            }
            static string GetKeyValueString(RegistryKey value, string KeyName)
            {
                return value.GetValue(KeyName).ToString();
            }
            static bool UpdateRegKeyStringValue(
                RegistryKey rootEntry,
                String RegKeyPath,
                string KeyName,
                string value)
            {
                RegistryKey PathKey = rootEntry;
                String strPath = RegKeyPath;
                PathKey = PathKey.OpenSubKey(strPath, true);
                PathKey.SetValue(KeyName, value, RegistryValueKind.String);
                PathKey.Close();
                return true;
            }
            //默认保存到SOFTWARE\\Microsoft
            public static void SaveRegistry(string Name, string Values)
            {
                try
                {
                    RegistryKey rsg = null;                   //声明一个变量
                    if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft").SubKeyCount <= 0)
                    {
                        Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft");
                        //删除
                        Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft");
                        //创建
                    }
                    rsg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft", true);   //true表可以修改
                    rsg.SetValue(Name, Values);//写入
                    rsg.Close();
                }
                catch (Exception ex)
                {

                }
            }
            /// <summary>
            /// 读取注册表
            /// </summary>
            /// <param name="Name">注册表名称</param>
            /// <param name="Values">注册表值</param>
            /// <returns></returns>
            public static string GetRegistry(string Name )
            {
                try
                {
                    //                 RegistryKey rsg = null;
                    //                 rsg = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).OpenSubKey("Microsoft", true);

                    RegistryKey rsg = null;               //声明变量
                    rsg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft", true); //true表可修改


                    //                 if (null == rsg)
                    //                 {
                    //                     rsg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft", true);
                    //                     rsg.CreateSubKey(Values);
                    //                 }
                    //                 rsg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft", true);

                    string  _value = "null";
                    if (rsg.GetValue(Name) != null) //读取失败返回null
                    {
                         _value = rsg.GetValue(Name).ToString();
                    }
                    
                    rsg.Close();
                    return _value;
                }
                catch (Exception ex)
                {
                    return "null";
                }
            }
        }
    }

}
