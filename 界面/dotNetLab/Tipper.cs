using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace dotNetLab
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
        public static void Tip_Info_Ask(string strMsg)
        {
            MessageBox.Show(strMsg, "询问", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        public static String Info
        {
            set
            {
                Tip_Info_Done(value);
            }
        }
        public static String Ask
        {
            set
            {
                Tip_Info_Ask(value);
            }
        }
        public static String Error
        {
            set
            {
                Tip_Info_Error(value);
            }

        }
    }

     
}
