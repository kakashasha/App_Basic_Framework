namespace dotNetLab
{
    namespace Common
    {
partial class Frm_Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Register));
            this.textView2 = new dotNetLab.Widgets.TextBlock();
            this.edt_Password = new dotNetLab.Widgets.MobileTextBox();
            this.textView1 = new dotNetLab.Widgets.TextBlock();
            this.edt_UserName = new dotNetLab.Widgets.MobileTextBox();
            this.btn_Register = new dotNetLab.Widgets.MobileButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txv_Status
            // 
            this.txv_Status.Location = new System.Drawing.Point(5, 437);
            this.txv_Status.TabIndex = 5;
            // 
            // textView2
            // 
            this.textView2.BackColor = System.Drawing.Color.Transparent;
            this.textView2.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.textView2.Location = new System.Drawing.Point(223, 276);
            this.textView2.Name = "textView2";
            this.textView2.Size = new System.Drawing.Size(45, 27);
            this.textView2.TabIndex = 4;
            this.textView2.Text = "密码";
            this.textView2.WhereReturn = ((byte)(0));
            // 
            // edt_Password
            // 
            this.edt_Password.ActiveColor = System.Drawing.Color.Orange;
            this.edt_Password.BackColor = System.Drawing.Color.Transparent;
            this.edt_Password.DoubleValue = 0D;
            this.edt_Password.EnableNullValue = false;
            this.edt_Password.FloatValue = 0F;
            this.edt_Password.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.edt_Password.IntValue = 0;
            this.edt_Password.Location = new System.Drawing.Point(298, 276);
            this.edt_Password.Name = "edt_Password";
            this.edt_Password.Size = new System.Drawing.Size(196, 31);
            this.edt_Password.StaticColor = System.Drawing.Color.DarkGray;
            this.edt_Password.TabIndex = 1;
            // 
            // textView1
            // 
            this.textView1.BackColor = System.Drawing.Color.Transparent;
            this.textView1.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.textView1.Location = new System.Drawing.Point(223, 220);
            this.textView1.Name = "textView1";
            this.textView1.Size = new System.Drawing.Size(65, 27);
            this.textView1.TabIndex = 3;
            this.textView1.Text = "用户名";
            this.textView1.WhereReturn = ((byte)(0));
            // 
            // edt_UserName
            // 
            this.edt_UserName.ActiveColor = System.Drawing.Color.Orange;
            this.edt_UserName.BackColor = System.Drawing.Color.Transparent;
            this.edt_UserName.DoubleValue = 0D;
            this.edt_UserName.EnableNullValue = false;
            this.edt_UserName.FloatValue = 0F;
            this.edt_UserName.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.edt_UserName.IntValue = 0;
            this.edt_UserName.Location = new System.Drawing.Point(298, 217);
            this.edt_UserName.Name = "edt_UserName";
            this.edt_UserName.Size = new System.Drawing.Size(196, 31);
            this.edt_UserName.StaticColor = System.Drawing.Color.DarkGray;
            this.edt_UserName.TabIndex = 0;
            // 
            // btn_Register
            // 
            this.btn_Register.BackColor = System.Drawing.Color.Transparent;
            this.btn_Register.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btn_Register.ForeColor = System.Drawing.Color.White;
            this.btn_Register.Location = new System.Drawing.Point(399, 382);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.NormalColor = System.Drawing.Color.DodgerBlue;
            this.btn_Register.PressColor = System.Drawing.Color.RoyalBlue;
            this.btn_Register.Radius = 40;
            this.btn_Register.Size = new System.Drawing.Size(124, 44);
            this.btn_Register.TabIndex = 2;
            this.btn_Register.Text = "注册";
            this.btn_Register.WhereReturn = ((byte)(0));
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(53, 183);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(399, 51);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(145, 127);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // Frm_Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 460);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_Register);
            this.Controls.Add(this.textView2);
            this.Controls.Add(this.edt_Password);
            this.Controls.Add(this.textView1);
            this.Controls.Add(this.edt_UserName);
            this.EnableDialog = true;
            this.IconImage = ((System.Drawing.Image)(resources.GetObject("$this.IconImage")));
            this.Name = "Frm_Register";
            this.Text = "新用户";
            this.Controls.SetChildIndex(this.txv_Status, 0);
            this.Controls.SetChildIndex(this.edt_UserName, 0);
            this.Controls.SetChildIndex(this.textView1, 0);
            this.Controls.SetChildIndex(this.edt_Password, 0);
            this.Controls.SetChildIndex(this.textView2, 0);
            this.Controls.SetChildIndex(this.btn_Register, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.pictureBox2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Widgets.TextBlock textView2;
        private Widgets.MobileTextBox edt_Password;
        private Widgets.TextBlock textView1;
        private Widgets.MobileTextBox edt_UserName;
        private Widgets.MobileButton btn_Register;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
    }
    
}