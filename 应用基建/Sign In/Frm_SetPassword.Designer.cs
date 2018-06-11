namespace dotNetLab
{
    namespace Common
    {
        partial class Frm_SetPassword
        {
            /// <summary>
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SetPassword));
            this.btn_OK = new dotNetLab.Widgets.MobileButton();
            this.txb_newPassword = new dotNetLab.Widgets.MobileTextBox();
            this.txb_confirmPassword = new dotNetLab.Widgets.MobileTextBox();
            this.textView1 = new dotNetLab.Widgets.TextBlock();
            this.textView2 = new dotNetLab.Widgets.TextBlock();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txv_Status
            // 
            this.txv_Status.Location = new System.Drawing.Point(5, 350);
            this.txv_Status.TabIndex = 5;
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.Transparent;
            this.btn_OK.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btn_OK.ForeColor = System.Drawing.Color.White;
            this.btn_OK.Location = new System.Drawing.Point(306, 312);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.NormalColor = System.Drawing.Color.DodgerBlue;
            this.btn_OK.PressColor = System.Drawing.Color.RoyalBlue;
            this.btn_OK.Radius = 40;
            this.btn_OK.Size = new System.Drawing.Size(128, 43);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "确定";
            this.btn_OK.WhereReturn = ((byte)(0));
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // txb_newPassword
            // 
            this.txb_newPassword.ActiveColor = System.Drawing.Color.Orange;
            this.txb_newPassword.BackColor = System.Drawing.Color.Transparent;
            this.txb_newPassword.DoubleValue = 0D;
            this.txb_newPassword.EnableNullValue = false;
            this.txb_newPassword.FloatValue = 0F;
            this.txb_newPassword.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_newPassword.IntValue = 0;
            this.txb_newPassword.Location = new System.Drawing.Point(201, 153);
            this.txb_newPassword.Name = "txb_newPassword";
            this.txb_newPassword.Size = new System.Drawing.Size(236, 31);
            this.txb_newPassword.StaticColor = System.Drawing.Color.DarkGray;
            this.txb_newPassword.TabIndex = 0;
            // 
            // txb_confirmPassword
            // 
            this.txb_confirmPassword.ActiveColor = System.Drawing.Color.Orange;
            this.txb_confirmPassword.BackColor = System.Drawing.Color.Transparent;
            this.txb_confirmPassword.DoubleValue = 0D;
            this.txb_confirmPassword.EnableNullValue = false;
            this.txb_confirmPassword.FloatValue = 0F;
            this.txb_confirmPassword.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_confirmPassword.IntValue = 0;
            this.txb_confirmPassword.Location = new System.Drawing.Point(201, 234);
            this.txb_confirmPassword.Name = "txb_confirmPassword";
            this.txb_confirmPassword.Size = new System.Drawing.Size(236, 31);
            this.txb_confirmPassword.StaticColor = System.Drawing.Color.DarkGray;
            this.txb_confirmPassword.TabIndex = 1;
            // 
            // textView1
            // 
            this.textView1.BackColor = System.Drawing.Color.Transparent;
            this.textView1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textView1.Location = new System.Drawing.Point(168, 122);
            this.textView1.Name = "textView1";
            this.textView1.Size = new System.Drawing.Size(60, 25);
            this.textView1.TabIndex = 3;
            this.textView1.Text = "新密码";
            this.textView1.WhereReturn = ((byte)(0));
            // 
            // textView2
            // 
            this.textView2.BackColor = System.Drawing.Color.Transparent;
            this.textView2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textView2.Location = new System.Drawing.Point(168, 203);
            this.textView2.Name = "textView2";
            this.textView2.Size = new System.Drawing.Size(78, 25);
            this.textView2.TabIndex = 4;
            this.textView2.Text = "确认密码";
            this.textView2.WhereReturn = ((byte)(0));
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 142);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(142, 137);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(347, 50);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(91, 81);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // Frm_SetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 373);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textView2);
            this.Controls.Add(this.textView1);
            this.Controls.Add(this.txb_confirmPassword);
            this.Controls.Add(this.txb_newPassword);
            this.Controls.Add(this.btn_OK);
            this.EnableDialog = true;
            this.IconImage = ((System.Drawing.Image)(resources.GetObject("$this.IconImage")));
            this.Name = "Frm_SetPassword";
            this.Text = "设置管理员密码";
            this.Controls.SetChildIndex(this.txv_Status, 0);
            this.Controls.SetChildIndex(this.btn_OK, 0);
            this.Controls.SetChildIndex(this.txb_newPassword, 0);
            this.Controls.SetChildIndex(this.txb_confirmPassword, 0);
            this.Controls.SetChildIndex(this.textView1, 0);
            this.Controls.SetChildIndex(this.textView2, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.pictureBox2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

            }

            #endregion
           
            private Widgets.MobileButton btn_OK;
            private Widgets.MobileTextBox txb_newPassword;
            private Widgets.MobileTextBox txb_confirmPassword;
            private Widgets.TextBlock textView1;
            private Widgets.TextBlock textView2;
            private System.Windows.Forms.PictureBox pictureBox1;
            private System.Windows.Forms.PictureBox pictureBox2;
        }
    }
 
}