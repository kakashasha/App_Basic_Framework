using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
namespace dotNetLab.Debug
{

    /*
     需要注意！要手动 MethodNames.Add,确保ClassFullName已经赋值

    */
    public class CodeEngine
    {
        
        string encryptKey = "Oyea";    //定义密钥                        
        protected Object Host;

        public String LastErrorInfo = null;
        protected Assembly CompilledAssembly = null;
        public String Code = null;
         Dictionary<String, MethodInfo> MethodSet;
        public List<String> MethodNames;
        public String ClassFullName = null;
        public CodeEngine()
        {
            MethodSet = new Dictionary<String, MethodInfo>();
            MethodNames = new List<string>();
        }
        protected virtual void Load(string classFullName)
        {
            if(classFullName == null)
            {
                Tipper.Error = "At CodeEngine.Load : classFullName 不能为空！";
                return;
            }
            try
            {
                ClassFullName = classFullName;

                Host = this.CompilledAssembly.CreateInstance(ClassFullName);
                MethodSet.Clear();

                if (Host == null)
                {
                    Console.WriteLine( "At  CodeEngine.Load : 未能加载内存中的dll!");
                    return;
                }
                for (int i = 0; i < MethodNames.Count; i++)
                {

                    MethodSet.Add(MethodNames[i], Host.GetType().GetMethod(MethodNames[i]));
                }


            }
            catch (System.Exception ex)
            {
                MessageBox.Show("At  CodeEngine.Load : "+ ex.Message + " \r\n");
            }
        }

        public virtual Object Invoke( String ThisMethodName,Object[] objArgs)
        {
            try
            {
                PrepareRuntime();
                MethodInfo mif = MethodSet[ThisMethodName];
                return mif.Invoke(Host, objArgs);

            }
            catch (Exception ex)
            {
                Tipper.Error = "At CodeEngine.Invoke:" + ex.Message + " " + ex.StackTrace;
                return null;
            }
        }

        public void ForcePrepareRuntime(string strcode)
        {

            CompileDll(strcode);

            Load(ClassFullName);

        }
        public void PrepareRuntime()
        {
            try
            {
                if (Host == null)
                {
                    CompileDll(Code);
                    Load(ClassFullName);
                }
            }
            catch (System.Exception ex)
            {
                Tipper.Error = "At ScriptEngine.PrepareRunTime() : " + ex.Message;
            }


        }
        /// <summary>
        /// 动态编译并执行代码
        /// </summary>
        /// <param name="code">代码</param>
        /// <returns>返回输出内容</returns>
        public bool CompileDll(string code)
        {
            // CheckClassName(ref code);
            if (String.IsNullOrEmpty(code))
            {
                Tipper.Error = "At ScriptCodeEngine.CompileDll() : " + "code 参数为空";

            }
            ICodeCompiler complier = new CSharpCodeProvider().CreateCompiler();
            //设置编译参数
            CompilerParameters paras = new CompilerParameters();
            //引入第三方dll
            paras.ReferencedAssemblies.Add("System.dll");
            paras.ReferencedAssemblies.Add("System.Data.dll");
            paras.ReferencedAssemblies.Add("System.Drawing.dll");
            paras.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            paras.ReferencedAssemblies.Add("System.Xml.dll");
            paras.ReferencedAssemblies.Add("System.Core.dll");
            paras.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            paras.ReferencedAssemblies.Add("shikii.dotNetLab.HalconLib.dll");
            paras.ReferencedAssemblies.Add("halcondotnet.dll");

            //引入自定义dll
            //  paras.ReferencedAssemblies.Add(@"D:\自定义方法\自定义方法\bin\LogHelper.dll");
            //是否内存中生成输出 
            paras.GenerateInMemory = true;
            //是否生成可执行文件
            paras.GenerateExecutable = false;
            LoadExternalDlls(paras);

            //编译代码
            CompilerResults result = complier.CompileAssemblyFromSource(paras, code);

            if (result.Errors.Count == 0)
            {
                if (result.CompiledAssembly != null)
                    this.CompilledAssembly = result.CompiledAssembly;


                return true;
            }
            else
            {

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < result.Errors.Count; i++)
                {
                    stringBuilder.AppendFormat("{0}:{1}\r\n", result.Errors[i].Line, result.Errors[i].ErrorText);
                }
                this.LastErrorInfo = stringBuilder.ToString();

                return false;
            }
        }
        void LoadExternalDlls(CompilerParameters paras)
        {
            string[] defFiles = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.csref");
            if (defFiles.Length > 0)
            {
                for (int i = 0; i < defFiles.Length; i++)
                {
                    String[] strArr = File.ReadAllLines(defFiles[i]);
                    int nDllPaths = strArr.Length;
                    for (int z = 0; z < nDllPaths; z++)
                    {

                        if (File.Exists(strArr[z]))
                        {
                            paras.ReferencedAssemblies.Add(strArr[z]);
                        }
                    }

                }
            }
        }

        #region 加密字符串  
        /// <summary> /// 加密字符串   
        /// </summary>  
        /// <param name="str">要加密的字符串</param>  
        /// <returns>加密后的字符串</returns>  
        public string Encrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象   

            byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    

            byte[] data = Encoding.Unicode.GetBytes(str);//定义字节数组，用来存储要加密的字符串  

            MemoryStream MStream = new MemoryStream(); //实例化内存流对象      

            //使用内存流实例化加密流对象   
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);

            CStream.Write(data, 0, data.Length);  //向加密流中写入数据      

            CStream.FlushFinalBlock();              //释放加密流      

            return Convert.ToBase64String(MStream.ToArray());//返回加密后的字符串  
        }
        #endregion

        #region 解密字符串   
        /// <summary>  
        /// 解密字符串   
        /// </summary>  
        /// <param name="str">要解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
        public string Decrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象    

            byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    

            byte[] data = Convert.FromBase64String(str);//定义字节数组，用来存储要解密的字符串  

            MemoryStream MStream = new MemoryStream(); //实例化内存流对象      

            //使用内存流实例化解密流对象       
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);

            CStream.Write(data, 0, data.Length);      //向解密流中写入数据     

            CStream.FlushFinalBlock();               //释放解密流      

            return Encoding.Unicode.GetString(MStream.ToArray());       //返回解密后的字符串  
        }
        #endregion
    }
}



       



       