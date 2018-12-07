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
               String strTrial =  R.CompactDB.FetchValue("SheynQi");
                if (strTrial == null)
                    R.CompactDB.Write("SheynQi", "30");
                int nTrialDay = R.CompactDB.FetchIntValue("SheynQi");
              String strValue  =  
                 File.ReadAllText("key.lic", Encoding.Default);
                if(!String.IsNullOrEmpty( strValue) )
                {
                 
                    if (strValue == CommandInvoker.ExecuteCMD("wmic cpu get processorid"))
                    {
                        goto cc;
                    }
                }
                
                    strValue = RegTool.GetRegistry("TrialStartDay", "");
                   
                    if (strValue == "null")
                        RegTool.SaveRegistry("TrialStartDay", DateTime.Now.ToString("yyyy-MM-dd"));
                    else
                    {
                       TimeSpan ts  = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) - DateTime.Parse(strValue);
                       if (ts.Days >= nTrialDay)
                        {
                            Tipper.Error = "软件试用已到期，请联系视觉软件厂商\r\n李孟铖,Tel:18025366950";
                            return;
                        }
                    }
                
               

                cc:;
            }
            Form frm = PG.GetType().GetMethod("ShowQuickBuildForm").Invoke(PG, null) as Form ; 
            
            Application.Run(frm);
        }

    }
}