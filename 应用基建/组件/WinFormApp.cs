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

namespace dotNetLab.Common
{
    public class WinFormApp
    {
        public static  AppManager _manager;
        public static bool CheckTrialVersion = true;
       
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
                if (!WinFormApp.CheckTrialVersion)
                    goto cc;
              String strValue  = RegTool.GetRegistry("KeyCode","");
                String KeyValue = File.ReadAllText("key.lic", Encoding.Default);
                if(strValue=="null" &&  String.IsNullOrEmpty(KeyValue) )
                {
                    RegTool.SaveRegistry("KeyCode", "0");
                }
                else
                {
                    if (KeyValue == CommandInvoker.ExecuteCMD("wmic cpu get processorid"))
                    {
                        goto cc;
                    }
                }
               if(strValue=="0")
                {
                    strValue = RegTool.GetRegistry("TrialStartDay", "");
                   
                    if (strValue == "0")
                        RegTool.SaveRegistry("TrialStartDay", DateTime.Now.ToString("yyyy-MM-dd"));
                    else
                    {
                       TimeSpan ts  = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) - DateTime.Parse(strValue);
                       if (ts.Days >= 30)
                        {
                            Tipper.Error = "软件试用已到期，请联系视觉软件厂商\r\n李孟铖,Tel:18025366950";
                            return;
                        }
                    }
                }
               else
                {
                  
                     String str =  strValue ;
                    if(str == CommandInvoker.ExecuteCMD("wmic cpu get processorid"))
                    {
                        ;
                    }
                    else
                    {
                        
                    }
                }

                cc:;
                 


            }
            Application.Run(PG);
        }

    }
}