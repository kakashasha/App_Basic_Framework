using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace dotNetLab.Common 
{
   public class DebugCommand :CommandLineBase
    {
        public bool Attach(String id, int waitTime_MillSecs)
        {
           String  [] str =  this.Execute(String.Format("a  {0}", id), waitTime_MillSecs);
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].Contains("Error"))
                    return false;
            }
            return true;
        }
        public bool Detach(int waitTime_MillSecs)
        {
            String[] str = this.Execute( "de" , waitTime_MillSecs);
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].Contains("Error"))
                    return false;
            }
            return true;
        }

        public override void Dispose()
        {
            base.Dispose();
            Process [] processes = Process.GetProcessesByName("Mdbg");
            foreach (var item in processes)
            {
                item.Kill();
            }
        }

         



    }
}
