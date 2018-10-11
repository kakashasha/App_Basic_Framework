namespace dotNetLab
{
    namespace Common
    {
        partial class  LogInForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInForm));
            this.edt_UserName = new dotNetLab.Widgets.MobileTextBox();
            this.textView1 = new dotNetLab.Widgets.TextBlock();
            this.edt_Password = new dotNetLab.Widgets.MobileTextBox();
            this.textView2 = new dotNetLab.Widgets.TextBlock();
            this.btn_Signup = new dotNetLab.Widgets.MobileButton();
            this.btn_Register = new dotNetLab.Widgets.MobileButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txv_Status
            // 
            this.txv_Status.Location = new System.Drawing.Point(5, 450);
            this.txv_Status.Size = new System.Drawing.Size(60, 26);
            this.txv_Status.TabIndex = 8;
            // 
            // edt_UserName
            // 
            this.edt_UserName.ActiveColor = System.Drawing.Color.Orange;
            this.edt_UserName.BackColor = System.Drawing.Color.Transparent;
            this.edt_UserName.DataBindingInfo = null;
            this.edt_UserName.DoubleValue = double.NaN;
            this.edt_UserName.EnableMobileRound = true;
            this.edt_UserName.EnableNullValue = false;
            this.edt_UserName.FillColor = System.Drawing.Color.Transparent;
            this.edt_UserName.FloatValue = float.NaN;
            this.edt_UserName.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.edt_UserName.ForeColor = System.Drawing.Color.Black;
            this.edt_UserName.GreyPattern = false;
            this.edt_UserName.IntValue = -2147483648;
            this.edt_UserName.LineThickness = 2F;
            this.edt_UserName.Location = new System.Drawing.Point(321, 190);
            this.edt_UserName.MainBindableProperty = "";
            this.edt_UserName.Name = "edt_UserName";
            this.edt_UserName.Radius = -1;
            this.edt_UserName.Size = new System.Drawing.Size(210, 31);
            this.edt_UserName.StaticColor = System.Drawing.Color.DarkGray;
            this.edt_UserName.TabIndex = 0;
            this.edt_UserName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.edt_UserName.TextBackColor = System.Drawing.SystemColors.Window;
            this.edt_UserName.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.edt_UserName.UIElementBinders = null;
            this.edt_UserName.WhitePattern = false;
            // 
            // textView1
            // 
            this.textView1.BackColor = System.Drawing.Color.Transparent;
            this.textView1.BorderColor = System.Drawing.Color.Empty;
            this.textView1.BorderThickness = -1;
            this.textView1.DataBindingInfo = null;
            this.textView1.EnableFlag = false;
            this.textView1.EnableTextRenderHint = false;
            this.textView1.FlagAlign = dotNetLab.Widgets.Alignments.Left;
            this.textView1.FlagColor = System.Drawing.Color.DodgerBlue;
            this.textView1.FlagThickness = 5;
            this.textView1.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.textView1.GapBetweenTextFlag = 10;
            this.textView1.LEDStyle = false;
            this.textView1.Location = new System.Drawing.Point(250, 193);
            this.textView1.MainBindableProperty = "用户名";
            this.textView1.Name = "textView1";
            this.textView1.Radius = -1;
            this.textView1.Size = new System.Drawing.Size(65, 27);
            this.textView1.TabIndex = 6;
            this.textView1.TabStop = false;
            this.textView1.Text = "用户名";
            this.textView1.UIElementBinders = null;
            this.textView1.UnderLine = false;
            this.textView1.UnderLineColor = System.Drawing.Color.DarkGray;
            this.textView1.UnderLineThickness = 2F;
            this.textView1.Vertical = false;
            this.textView1.WhereReturn = ((byte)(0));
            // 
            // edt_Password
            // 
            this.edt_Password.ActiveColor = System.Drawing.Color.Orange;
            this.edt_Password.BackColor = System.Drawing.Color.Transparent;
            this.edt_Password.DataBindingInfo = null;
            this.edt_Password.DoubleValue = double.NaN;
            this.edt_Password.EnableMobileRound = true;
            this.edt_Password.EnableNullValue = false;
            this.edt_Password.FillColor = System.Drawing.Color.Transparent;
            this.edt_Password.FloatValue = float.NaN;
            this.edt_Password.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.edt_Password.ForeColor = System.Drawing.Color.Black;
            this.edt_Password.GreyPattern = false;
            this.edt_Password.IntValue = -2147483648;
            this.edt_Password.LineThickness = 2F;
            this.edt_Password.Location = new System.Drawing.Point(321, 264);
            this.edt_Password.MainBindableProperty = "";
            this.edt_Password.Name = "edt_Password";
            this.edt_Password.Radius = -1;
            this.edt_Password.Size = new System.Drawing.Size(210, 31);
            this.edt_Password.StaticColor = System.Drawing.Color.DarkGray;
            this.edt_Password.TabIndex = 1;
            this.edt_Password.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.edt_Password.TextBackColor = System.Drawing.SystemColors.Window;
            this.edt_Password.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.edt_Password.UIElementBinders = null;
            this.edt_Password.WhitePattern = false;
            // 
            // textView2
            // 
            this.textView2.BackColor = System.Drawing.Color.Transparent;
            this.textView2.BorderColor = System.Drawing.Color.Empty;
            this.textView2.BorderThickness = -1;
            this.textView2.DataBindingInfo = null;
            this.textView2.EnableFlag = false;
            this.textView2.EnableTextRenderHint = false;
            this.textView2.FlagAlign = dotNetLab.Widgets.Alignments.Left;
            this.textView2.FlagColor = System.Drawing.Color.DodgerBlue;
            this.textView2.FlagThickness = 5;
            this.textView2.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.textView2.GapBetweenTextFlag = 10;
            this.textView2.LEDStyle = false;
            this.textView2.Location = new System.Drawing.Point(260, 264);
            this.textView2.MainBindableProperty = "密码";
            this.textView2.Name = "textView2";
            this.textView2.Radius = -1;
            this.textView2.Size = new System.Drawing.Size(45, 27);
            this.textView2.TabIndex = 7;
            this.textView2.TabStop = false;
            this.textView2.Text = "密码";
            this.textView2.UIElementBinders = null;
            this.textView2.UnderLine = false;
            this.textView2.UnderLineColor = System.Drawing.Color.DarkGray;
            this.textView2.UnderLineThickness = 2F;
            this.textView2.Vertical = false;
            this.textView2.WhereReturn = ((byte)(0));
            // 
            // btn_Signup
            // 
            this.btn_Signup.BackColor = System.Drawing.Color.Transparent;
            this.btn_Signup.BorderColor = System.Drawing.Color.Empty;
            this.btn_Signup.BorderThickness = -1;
            this.btn_Signup.CornerAligment = dotNetLab.Widgets.Alignments.All;
            this.btn_Signup.DataBindingInfo = null;
            this.btn_Signup.EnableFlag = false;
            this.btn_Signup.EnableMobileRound = false;
            this.btn_Signup.EnableTextRenderHint = false;
            this.btn_Signup.FlagAlign = dotNetLab.Widgets.Alignments.Left;
            this.btn_Signup.FlagColor = System.Drawing.Color.DodgerBlue;
            this.btn_Signup.FlagThickness = 5;
            this.btn_Signup.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btn_Signup.ForeColor = System.Drawing.Color.White;
            this.btn_Signup.GapBetweenTextFlag = 10;
            this.btn_Signup.GapBetweenTextImage = 8;
            this.btn_Signup.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
            this.btn_Signup.ImageSize = new System.Drawing.Size(0, 0);
            this.btn_Signup.LEDStyle = false;
            this.btn_Signup.Location = new System.Drawing.Point(441, 370);
            this.btn_Signup.MainBindableProperty = "登录";
            this.btn_Signup.Name = "btn_Signup";
            this.btn_Signup.NeedAnimation = false;
            this.btn_Signup.NormalColor = System.Drawing.Color.DodgerBlue;
            this.btn_Signup.PressColor = System.Drawing.Color.RoyalBlue;
            this.btn_Signup.Radius = 40;
            this.btn_Signup.Size = new System.Drawing.Size(120, 45);
            this.btn_Signup.Source = null;
            this.btn_Signup.TabIndex = 4;
            this.btn_Signup.Text = "登录";
            this.btn_Signup.UIElementBinders = null;
            this.btn_Signup.UnderLine = false;
            this.btn_Signup.UnderLineColor = System.Drawing.Color.DarkGray;
            this.btn_Signup.UnderLineThickness = 2F;
            this.btn_Signup.Vertical = false;
            this.btn_Signup.WhereReturn = ((byte)(0));
            this.btn_Signup.Click += new System.EventHandler(this.btn_Signup_Click);
            // 
            // btn_Register
            // 
            this.btn_Register.BackColor = System.Drawing.Color.Transparent;
            this.btn_Register.BorderColor = System.Drawing.Color.Empty;
            this.btn_Register.BorderThickness = -1;
            this.btn_Register.CornerAligment = dotNetLab.Widgets.Alignments.All;
            this.btn_Register.DataBindingInfo = null;
            this.btn_Register.EnableFlag = false;
            this.btn_Register.EnableMobileRound = false;
            this.btn_Register.EnableTextRenderHint = false;
            this.btn_Register.FlagAlign = dotNetLab.Widgets.Alignments.Left;
            this.btn_Register.FlagColor = System.Drawing.Color.DodgerBlue;
            this.btn_Register.FlagThickness = 5;
            this.btn_Register.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btn_Register.ForeColor = System.Drawing.Color.White;
            this.btn_Register.GapBetweenTextFlag = 10;
            this.btn_Register.GapBetweenTextImage = 8;
            this.btn_Register.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
            this.btn_Register.ImageSize = new System.Drawing.Size(0, 0);
            this.btn_Register.LEDStyle = false;
            this.btn_Register.Location = new System.Drawing.Point(289, 370);
            this.btn_Register.MainBindableProperty = "注册";
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.NeedAnimation = false;
            this.btn_Register.NormalColor = System.Drawing.Color.DodgerBlue;
            this.btn_Register.PressColor = System.Drawing.Color.RoyalBlue;
            this.btn_Register.Radius = 40;
            this.btn_Register.Size = new System.Drawing.Size(120, 45);
            this.btn_Register.Source = null;
            this.btn_Register.TabIndex = 3;
            this.btn_Register.Text = "注册";
            this.btn_Register.UIElementBinders = null;
            this.btn_Register.UnderLine = false;
            this.btn_Register.UnderLineColor = System.Drawing.Color.DarkGray;
            this.btn_Register.UnderLineThickness = 2F;
            this.btn_Register.Vertical = false;
            this.btn_Register.WhereReturn = ((byte)(0));
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(40, 160);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(170, 170);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(424, 47);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(146, 137);
            this.pictureBox2.TabIndex = 34;
            this.pictureBox2.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.Location = new System.Drawing.Point(497, 450);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(67, 15);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "退出应用";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // LogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 476);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_Register);
            this.Controls.Add(this.btn_Signup);
            this.Controls.Add(this.textView2);
            this.Controls.Add(this.edt_Password);
            this.Controls.Add(this.textView1);
            this.Controls.Add(this.edt_UserName);
            this.EnableDialog = true;
            this.IconImage = ((System.Drawing.Image)(resources.GetObject("$this.IconImage")));
            this.Name = "LogInForm";
            this.Text = "登录";
            this.Controls.SetChildIndex(this.txv_Status, 0);
            this.Controls.SetChildIndex(this.edt_UserName, 0);
            this.Controls.SetChildIndex(this.textView1, 0);
            this.Controls.SetChildIndex(this.edt_Password, 0);
            this.Controls.SetChildIndex(this.textView2, 0);
            this.Controls.SetChildIndex(this.btn_Signup, 0);
            this.Controls.SetChildIndex(this.btn_Register, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.pictureBox2, 0);
            this.Controls.SetChildIndex(this.linkLabel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            #endregion
            private Widgets.TextBlock textView1;
            private Widgets.MobileTextBox edt_Password;
            private Widgets.TextBlock textView2;
            private Widgets.MobileButton btn_Signup;
            private Widgets.MobileButton btn_Register;
            private System.Windows.Forms.PictureBox pictureBox1;
            private System.Windows.Forms.PictureBox pictureBox2;
            private System.Windows.Forms.LinkLabel linkLabel1;
            public Widgets.MobileTextBox edt_UserName;
        }
    }
}