 

using System;

using System.Windows.Forms;
 
using dotNetLab.Forms;
namespace dotNetLab
{
    namespace Common
    {

        public partial class AdiministratorForm : RibbonPage
    {
        public bool bAdmin = false;
        protected override void prepareCtrls()
        {
            base.prepareCtrls();
            InitializeComponent();
            this.edt_Password.txb.UseSystemPasswordChar = true;
            edt_Password.txb.KeyUp += Txb_KeyUp;
        }
        private void Txb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == '\r')
                btn_OK_Click(btn_OK, e);
        }
        void checkAdminPwdExist(String strAdmin)
        {
            
            if (strAdmin == "0")
            {
                MessageBox.Show("是否未设置管理员密码？");

                SetPasswordForm fs = new SetPasswordForm();
                this.Hide();
                fs.ShowDialog();
                this.Show();
            }

        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            String strAdmin = R.CompactDB.FetchValue("Administrator");
            checkAdminPwdExist(strAdmin);
            if (strAdmin != edt_Password.Text )
                {
                   MessageBox.Show("密码错误" ) ;
                    this.bAdmin = false;
                }
                else
                {
                    this.bAdmin = true; 
                    this.Close();
                    
                }
             
           
        }
    }
    }
}
