using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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

        public static void EndInvokeApp(Form PG)
        {
            Application.Run(PG);
        }
    }
}