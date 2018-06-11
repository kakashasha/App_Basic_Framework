namespace dotNetLab
{
    namespace Data
    {
        partial class ErrorWnd
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
            this.txb_Description = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txb_Description
            // 
            this.txb_Description.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txb_Description.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_Description.Location = new System.Drawing.Point(0, 106);
            this.txb_Description.Multiline = true;
            this.txb_Description.Name = "txb_Description";
            this.txb_Description.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txb_Description.Size = new System.Drawing.Size(541, 336);
            this.txb_Description.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("等线 Light", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(158, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 49);
            this.label1.TabIndex = 1;
            this.label1.Text = "错误窗口";
            // 
            // ErrorWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 442);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_Description);
            this.Name = "ErrorWnd";
            this.Text = "错误窗口";
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            #endregion

            public System.Windows.Forms.TextBox txb_Description;
            private System.Windows.Forms.Label label1;
        }
    }
}