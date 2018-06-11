using System;
using System.Collections.Generic;
using System.IO;

using System.Reflection;
using System.Text;

 

namespace dotNetLab

{
    namespace Tools
    {
  public  class ResDll
    {
        Stream fs;
        String strExecuteModualPath;
        String strExecuteModualDirectory;
        Assembly _ResDll;
        String strErorr;
        String strDllNameWithoutExt;
        public ResDll()
        {
             
            strExecuteModualDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);
        }
        public bool Load(String strDllWithoutExt)
        {
            try
            {
                _ResDll = Assembly.LoadFile(String.Format("{0}\\{1}.dll", strExecuteModualDirectory,
                strDllWithoutExt));
                this.strDllNameWithoutExt = strDllWithoutExt;
                return true;
            }
            catch (System.Exception ex)
            {
                strErorr = ex.ToString();
                return false;
            }

        }
        public Stream FetchRes(String strResName)
        {
            //R为文件名，资源必须设置为嵌入式的
            fs = _ResDll.GetManifestResourceStream(
                String.Format("{0}.R.{1}",
                this.strDllNameWithoutExt, strResName));
            return fs;
        }
    }
    }

}
