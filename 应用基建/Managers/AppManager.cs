using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using System.Text;
using System.Windows.Forms;
using Common;
using dotNetLab.Forms;
using dotNetLab.Widgets;

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
            public static Form ShowPage(Type typeForm)
            {
                Form frm = System.Activator.CreateInstance(typeForm) as Form;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
                frm.FormClosed += (s, e) =>
                {
                    frm.Dispose();
                };
                return frm;
            }
            public static Form ShowFixedPage(Type typeForm)
            {
                Form frm = ShowPage(typeForm) ;
                if(frm is ModernWnd)
                {
                    ModernWnd wnd = frm as ModernWnd;
                    wnd.EnableDialog = true;
                }
                else
                {
                    frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                }
                return frm;
            }

            public static void ShowCompactDBEditor()
            {
                DBEngineInvoker dbInvoker = new DBEngineInvoker();
                dbInvoker.Connect(DBEngineNames.SQLITE, "shikii.db");
                if (dbInvoker.Status)
                {

                    DBEngineEditorPage EditorPage = AppManager.ShowPage(typeof(DBEngineEditorPage)) as DBEngineEditorPage;
                    EditorPage.dbInvoker = dbInvoker;

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
