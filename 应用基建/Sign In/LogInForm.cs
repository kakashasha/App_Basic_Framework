
using System;
using System.Diagnostics;
using System.Windows.Forms;
using dotNetLab.Forms;
using System.Collections.Generic;
namespace dotNetLab
{
    namespace Common
    {
        public partial class LogInForm : RibbonPage
        {
            public bool bCloseWindow = false;
            
            protected override void prepareCtrls()
            {
                base.prepareCtrls();
                InitializeComponent();
           
                this.Shown += Frm_LogIn_Shown;
                this.edt_Password.txb.UseSystemPasswordChar = true;
            }

            protected override void prepareData()
            {
                base.prepareData();
                R.CompactDB.GetAllTableNames();
                if (!R.CompactDB.AllTableNames.Contains("User_Key"))
                    R.CompactDB.CreateKeyValueTable("User_Key");
             List<String> lst =   R.CompactDB.GetNameColumnValues(R.CompactDB.DefaultTable);

             if (!lst.Contains("Administrator"))
             {
                 R.CompactDB.Write("Administrator", "0");
                }
            }

            private void Frm_LogIn_Shown(object sender, EventArgs e)
            {
                this.bCloseWindow = false;
            }

            void CheckLogIn()
            {
                if (string.IsNullOrEmpty(edt_UserName.Text.Trim())
                    || string.IsNullOrEmpty(edt_Password.Text.Trim())
                    )
                {
                    Tipper.Tip_Info_Error("未完整填写");
                }
                else
                {
                    R.CompactDB.TargetTable = "User_Key";
                    String strPassword =
                        R.CompactDB.FetchValue(this.edt_UserName.Text.Trim());
                        
                    R.CompactDB.TargetTable = R.CompactDB.DefaultTable;
                    if (!R.CompactDB.Status)
                        Tipper.Tip_Info_Error("不存在此用户名！");
                    R.CompactDB.Status = true;
                    if (strPassword != null)
                    {
                        if (strPassword != this.edt_Password.Text.Trim())
                        {
                            Tipper.Tip_Info_Error("密码错误！");
                        }
                        else
                        {
                            R.Pipe.Info("用户： " + this.edt_UserName.Text + "已经登录。");
                            bCloseWindow = true;
                            this.edt_Password.Text = null;
                            this.Close();
                        }

                    }




                }
            }
            private void btn_Signup_Click(object sender, EventArgs e)
            {
                CheckLogIn();

            }

            protected override void prepareEvents()
            {
                base.prepareEvents();
                this.FormClosing += Frm_LogIn_FormClosing;
            }

            private void Frm_LogIn_FormClosing(object sender, FormClosingEventArgs e)
            {
                if (!bCloseWindow)
                {
                   // e.Cancel = true;
                    this.bCloseWindow = false;
                }
            }

            private void btn_Register_Click(object sender, EventArgs e)
            {
                RegisterForm frm = new RegisterForm();
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }
            void ForceClose()
            {
                String strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                Process[] Procs = Process.GetProcessesByName(strProcessName);
                foreach (Process item in Procs)
                {
                    item.Kill();
                }

            }
            private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                ForceClose();
            }
        }

    }
}
