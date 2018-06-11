using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using System.Text;
using System.Windows.Forms;
using Common;

namespace dotNetLab
{
    namespace Common
    {
        public class AppManager
        {
           
            public void  UniqueApp()
            {
                Process[] allPro = Process.GetProcesses();
                for (int i = 0; i < allPro.Length; i++)
                {
                    if (allPro[i].ProcessName == Process.GetCurrentProcess().ProcessName && allPro[i].Id != Process.GetCurrentProcess().Id)
                    {
                        MessageBox.Show("程序正在运行中", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if(!File.Exists("NetConfig.exe"))
                {
                    FileStream fs = new FileStream("NetConfig.exe", FileMode.Create);
                    fs.Write( AppComRes.NetCofig, 0,  AppComRes.NetCofig.Length);
                    fs.Close();
                    fs.Dispose();
                }
            }
            public void InitSubForm<T>(out T frm)
            {
                frm = System.Activator.CreateInstance<T>();
                Form _frm = frm as Form;
                _frm.StartPosition = FormStartPosition.CenterScreen;
                _frm.FormClosing += SubFormClosing;
            }

            public void SubFormClosing(object sender,
               FormClosingEventArgs e)
            {
                e.Cancel = true;
                Form frm = sender as Form;
                frm.Hide();
            }
        }

    }
}
