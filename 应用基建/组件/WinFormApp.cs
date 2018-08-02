using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using dotNetLab.Widgets;
using System;
using System.IO;
using dotNetLab.Data;
using dotNetLab.Data.Uniting;

namespace dotNetLab.Common
{
    public class WinFormApp
    {
       public static  AppManager _manager;
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

         
    }
}