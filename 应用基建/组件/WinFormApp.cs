using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using dotNetLab.Widgets;
using System;
using System.IO;
using dotNetLab.Data;
using dotNetLab.Data.Uniting;
using dotNetLab.Network;
using dotNetLab.Tools;
 
using dotNetLab.Common.Tool;
using System.Diagnostics;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace dotNetLab.Common
{
    public class WinFormApp
    {
        public static  AppManager _manager;
        public static bool CheckTrialVersion = false;
        public static void  BegineInvokeApp()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           

            _manager = new AppManager();
            _manager.UniqueApp();
            
        }
        
        public static void EndInvokeApp(Form PG,params object[] objs)
        {
            if (objs.Length >0)
            {

               MobileListBox ltb  = objs[0] as MobileListBox;
               R.Pipe.UIOutput_Info += (str) =>
                   {
                     
                       Action<MobileListBox,String,bool> action_DisplayLogMessage = PG.DisplayLogMessage ;

                       PG.Invoke(action_DisplayLogMessage, ltb, str, false);
                   };
               R.Pipe.UIOutput_Err += (str) =>
               {
                   Action<MobileListBox,String,bool> action_DisplayLogMessage = PG.DisplayLogMessage ;
                   PG.Invoke(action_DisplayLogMessage, ltb, str, true);
               };


            }
            SetupBackgroundWorker();
            PG.FormClosing += (s, e) =>
             {
                Process [] processes  = 
                 Process.GetProcessesByName(Path.GetFileNameWithoutExtension(R.CompactDB.FetchValue("BackgroundWorkerExeName")));
                 foreach (var item in processes)
                 {
                     item.Kill();
                 }
                 Process.GetCurrentProcess().Kill();
                  
             };
            Application.Run(PG);
        }

        public static void EndInvokeApp(Form PG, Action AfterLogPadInited,params object[] objs)
        {
            if (objs.Length > 0)
            {

                MobileListBox ltb = objs[0] as MobileListBox;
                R.Pipe.UIOutput_Info += (str) =>
                {

                    Action<MobileListBox, String, bool> action_DisplayLogMessage = PG.DisplayLogMessage;

                    PG.Invoke(action_DisplayLogMessage, ltb, str, false);
                };
                R.Pipe.UIOutput_Err += (str) =>
                { 
                    Action<MobileListBox, String, bool> action_DisplayLogMessage = PG.DisplayLogMessage;
                    PG.Invoke(action_DisplayLogMessage, ltb, str, true);
                };
                AfterLogPadInited();

                String strEnableProtect = R.CompactDB.FetchValue("shikii");
                if (strEnableProtect == "0")
                    R.CompactDB.Write("shikii", "True");
                WinFormApp.CheckTrialVersion = R.CompactDB.FetchBoolValue("shikii");


                if (!WinFormApp.CheckTrialVersion)
                    goto cc;
               String strTrial =  R.CompactDB.FetchValue("SheynQi");
                if (strTrial == null)
                    R.CompactDB.Write("SheynQi", "30");
                int nTrialDay = R.CompactDB.FetchIntValue("SheynQi");
                //试用期不得大于90天
                if (nTrialDay > 90)
                    return;
                
                String strValue  =  
                 File.ReadAllText("key.lic", Encoding.Default).Trim();
                if(strValue.Contains("\r\n"))
                {
                    String [] temp = strValue.Split('\r', '\n') ;
                    if(temp.Length >1 && !string.IsNullOrEmpty(temp[2]))
                    {
                       strValue  = temp[2].Trim();
                    }
                }
                if(!String.IsNullOrEmpty( strValue) )
                {
                    string cpuid = CommandInvoker.ExecuteCMD("wmic cpu get processorid").Trim();
                    if (strValue ==cpuid)
                    {
                        goto cc;
                    }
                }
                    strValue = RegTool.GetRegistry("TrialStartDay");
                   
                    if (strValue == "null")
                        RegTool.SaveRegistry("TrialStartDay", DateTime.Now.ToString("yyyy-MM-dd"));
                    else
                    {
                       TimeSpan ts  = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) - DateTime.Parse(strValue);
                       if (ts.Days >= nTrialDay)
                        {
                            Tipper.Error = R.CompactDB.FetchValue("WarnUserText",true,"软件已经到期！");//"软件试用已到期，请联系视觉软件厂商\r\n李孟铖,Tel:18025366950";
                        return;
                        }
                    }
                
               

                cc:;
            }


            Form frm = PG.GetType().GetMethod("ShowQuickBuildForm").Invoke(PG, null) as Form;
            SetupBackgroundWorker();
            PG.FormClosing += (s, e) =>
            {
                Process[] processes =
                 Process.GetProcessesByName(Path.GetFileNameWithoutExtension(R.CompactDB.FetchValue("BackgroundWorkerExeName")));
                foreach (var item in processes)
                {
                    item.Kill();
                }
                Process.GetCurrentProcess().Kill();

            };
            Application.Run(frm);
        }

        static void SetupBackgroundWorker()
        {
           
             GenerateBackgroundWorker(R.CompactDB.FetchValue("BackgroundWorkerExeName" ,true, "ShikiiBackground.exe"));
         
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = R.CompactDB.FetchValue("BackgroundWorkerExeName", true, "ShikiiBackground.exe"); //启动的应用程序名称
                                                                                                                      //如果传多个参数用空格隔开
                startInfo.Arguments = R.CompactDB.FetchValue("BackgroundWorkerExecuteDllNames");
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                
                Process.Start(startInfo);
              //  R.Pipe.Info("后台任务执行已经启动");
            }
            catch (Exception ex)
            {
              //  R.Pipe.Info("后台任务执行未能成功启动 " +ex.Message + ex.Source);

            }
           
        }
        public static bool GenerateBackgroundWorker(String exeName)
        {

            if (exeName == "0")
                exeName = "ShikiiBackground.exe";
               

            if(File.Exists(exeName))
            {
                Process[] processes =
              Process.GetProcessesByName(Path.GetFileNameWithoutExtension(R.CompactDB.FetchValue("BackgroundWorkerExeName")));
                foreach (var item in processes)
                {
                    item.Kill();
                }
                try{File.Delete(exeName);}catch {}
               
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
            
           
            //是否内存中生成输出 
            paras.GenerateInMemory = false;
            //是否生成可执行文件
            paras.GenerateExecutable = true;
            paras.OutputAssembly = exeName;
        
            string code = AppComRes.ShikiiBackgroundWorker;
            //编译代码
            CompilerResults result = complier.CompileAssemblyFromSource(paras, code);

            if (result.Errors.Count == 0)
            {
                if (result.CompiledAssembly != null)
                    


                return true;
            }
            else
            {

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < result.Errors.Count; i++)
                {
                    stringBuilder.AppendFormat("{0}:{1}\r\n", result.Errors[i].Line, result.Errors[i].ErrorText);
                }


                return false;
            }
            return false;
        }

    }
}