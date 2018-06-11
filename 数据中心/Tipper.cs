using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
namespace dotNetLab.Data
{
  
   public class Tipper
    {
        public static void Tip_Info_Done(string strMsg)
        {
            MessageBox.Show(strMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Tip_Info_Error(string strMsg)
        {
            MessageBox.Show(strMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
