 
//using Framework.Res;
using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using dotNetLab.Forms;
namespace dotNetLab
{
    namespace Common
    {

        public partial class RegisterForm : RibbonPage
        {

        protected override void prepareCtrls()
       
        {
            InitializeComponent();
            this.edt_Password.txb.UseSystemPasswordChar = true ;
            
        }
        void RegisterUser()
        {
            R.CompactDB.TargetTable = "User_Key";
            R.CompactDB.Write(this.edt_UserName.Text.Trim(), 
                edt_Password.Text.Trim());
            R.CompactDB.TargetTable = R.CompactDB.DefaultTable;
            if(!R.CompactDB.Status)
                Tipper.Tip_Info_Error("内部异常！");
            R.CompactDB.Status = true;
        }
       void checkAdminPwdExist()
        {
            String strAdmin = R.CompactDB.FetchValue("Administrator");
            if (strAdmin == "0")
            {
                Tipper.Tip_Info_Ask("是否未设置管理员密码？");

                SetPasswordForm fs = new SetPasswordForm();
                this.Hide();
                fs.ShowDialog();
                
            }
            
        }
        private void btn_Register_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edt_UserName.Text.Trim())
              || string.IsNullOrEmpty(edt_Password.Text.Trim()))
            {
                Tipper.Tip_Info_Error("未正确填写");
                return;
            }
            checkAdminPwdExist();
            AdiministratorForm frm = new AdiministratorForm();
            frm.ShowDialog();
            if (frm.bAdmin)
            {

                RegisterUser();
                Tipper.Tip_Info_Done("已成功注册！");

            }
            else
                Tipper.Tip_Info_Error("注册失败！");
            frm.Close();
            frm.Dispose();
            this.Close();

        }

        
    }
    }
}
