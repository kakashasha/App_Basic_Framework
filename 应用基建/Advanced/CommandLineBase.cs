using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace dotNetLab.Common 
{
  public  class CommandLineBase : IDisposable
    {


       protected Process p;
       
        List<String> Lines;

        public CommandLineBase()
        {
            p = new Process();
            Lines = new List<string>();
           
           

        }

        private void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {

                this.Lines.Add(e.Data);
                    
                
            }
           
             
        }

        public void Start(String strEXEName)
        {
            p.StartInfo.FileName = strEXEName;
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.OutputDataReceived += P_OutputDataReceived;
            p.Start();//启动程序
            p.StandardInput.AutoFlush = true;
            p.BeginOutputReadLine();
            // p.WaitForExit();
        }
        public void Start( )
        {
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.OutputDataReceived += P_OutputDataReceived;
            p.Start();//启动程序
            p.StandardInput.AutoFlush = true;
            p.BeginOutputReadLine();
            // p.WaitForExit();
        }
        public String []   Execute(String str,int TimeForWait)
        {
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(str);
            this.Lines.Clear();
            p.StandardInput.Flush();
            Thread.Sleep(TimeForWait);
            return Lines.ToArray();
        }

        public virtual void Dispose()
        {
            try
            {
                p.StandardInput.WriteLine("exit");
                p.Close();
                p.Dispose();
            }
            catch (Exception ex)
            {

            }
         
            
        }

        ~CommandLineBase()
        {
            Dispose();
        }

        //public String GetResultString()
        //{

        //    //if (p.HasExited)
        //    //    return p.StandardOutput.ReadToEnd();
        //    //else
        //    //    return "Failed !";
        //}


    }
}
