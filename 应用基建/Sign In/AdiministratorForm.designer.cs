namespace dotNetLab
{
    namespace Common
    {
        partial class AdiministratorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdiministratorForm));
            this.edt_Password = new dotNetLab.Widgets.MobileTextBox();
            this.textView1 = new dotNetLab.Widgets.TextBlock();
            this.btn_OK = new dotNetLab.Widgets.MobileButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txv_Status
            // 
            this.txv_Status.Location = new System.Drawing.Point(5, 364);
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
            this.edt_Password.Location = new System.Drawing.Point(92, 229);
            this.edt_Password.Name = "edt_Password";
            this.edt_Password.Size = new System.Drawing.Size(286, 31);
            this.edt_Password.StaticColor = System.Drawing.Color.DarkGray;
            this.edt_Password.TabIndex = 0;
            // 
            // textView1
            // 
            this.textView1.BackColor = System.Drawing.Color.Transparent;
            this.textView1.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.textView1.Location = new System.Drawing.Point(59, 176);
            this.textView1.Name = "textView1";
            this.textView1.Size = new System.Drawing.Size(45, 27);
            this.textView1.TabIndex = 2;
            this.textView1.Text = "密码";
            this.textView1.WhereReturn = ((byte)(0));
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.Transparent;
            this.btn_OK.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btn_OK.ForeColor = System.Drawing.Color.White;
            this.btn_OK.Location = new System.Drawing.Point(274, 316);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.NormalColor = System.Drawing.Color.DodgerBlue;
            this.btn_OK.PressColor = System.Drawing.Color.RoyalBlue;
            this.btn_OK.Radius = 45;
            this.btn_OK.Size = new System.Drawing.Size(130, 47);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.Text = "确认";
            this.btn_OK.WhereReturn = ((byte)(0));
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(274, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 99);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // AdiministratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 387);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.textView1);
            this.Controls.Add(this.edt_Password);
            this.EnableDialog = true;
            this.IconImage = ((System.Drawing.Image)(resources.GetObject("$this.IconImage")));
            this.Name = "AdiministratorForm";
            this.Text = "管理员口令";
            this.Controls.SetChildIndex(this.txv_Status, 0);
            this.Controls.SetChildIndex(this.edt_Password, 0);
            this.Controls.SetChildIndex(this.textView1, 0);
            this.Controls.SetChildIndex(this.btn_OK, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

            }

            #endregion

            private Widgets.MobileTextBox edt_Password;
            private Widgets.TextBlock textView1;
            private Widgets.MobileButton btn_OK;
            private System.Windows.Forms.PictureBox pictureBox1;
        }
    }
}