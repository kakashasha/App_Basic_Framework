

using System;

using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Forms;
namespace dotNetLab
{
    namespace Common
    {
        public partial class SetPasswordForm : RibbonPage
        {
            protected override void prepareCtrls()
            {
                base.prepareCtrls();
                InitializeComponent();
                this.txb_confirmPassword.txb.UseSystemPasswordChar
                    = txb_newPassword.txb.UseSystemPasswordChar = true;
            }
            private void checkPassword()
            {
                if (string.IsNullOrEmpty(txb_newPassword.Text.Trim())
                  || string.IsNullOrEmpty(txb_confirmPassword.Text.Trim()))
                {
                    Tipper.Tip_Info_Error("未正确填写");
                    return;
                }
                if (!txb_confirmPassword.Text.Equals(txb_newPassword.Text))
                {
                    Tipper.Tip_Info_Error("两次输入密码不一致");
                    return;
                }
                else
                {
                    R.CompactDB.Write("Administrator", this.txb_newPassword.Text.Trim());
                    if (R.CompactDB.Status)

                        Tipper.Tip_Info_Done("密码设置成功");
                    else
                    {
                        Tipper.Tip_Info_Error("密码设置失败");
                        return;
                    }
                    R.CompactDB.Status = true;

                    this.Close();
                }

            }
            private void btn_OK_Click(object sender, EventArgs e)
            {
                checkPassword();

            }
        }
    }
}
