using System;
using System.Collections.Generic;
using System.Text;
using dotNetLab.Widgets;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{
    public  class ListBoxItem_TableName : Choice
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public EventHandler ExternalCall;
        public Object OtherObj;
        
        protected override void prepareCtrls()
        {
           
            InitializeComponent();
            BackColor = Color.Transparent;
            clr_Normal = Color.Transparent;
            clr_Press = Color.DodgerBlue;
            this.Radius = 10;
        }
        
        
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListBoxItem_TableName));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(31, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // ListBoxItem_TableName
            // 
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ListBoxItem_TableName";
            this.Size = new System.Drawing.Size(92, 28);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void prepareEvents()
        {
            base.prepareEvents();
            pictureBox1.MouseUp += ListBoxItem_TableName_MouseUp;
            pictureBox1.DoubleClick += new EventHandler(pictureBox1_DoubleClick);
            label1.DoubleClick += new EventHandler(label1_DoubleClick);
            label1.MouseUp += ListBoxItem_TableName_MouseUp;
            
        }

        private void ListBoxItem_TableName_MouseUp(object sender, MouseEventArgs e)
        {
           
            this.OnMouseUp(e);
        }

        void label1_DoubleClick(object sender, EventArgs e)
        {

            base.OnDoubleClick(e);
        }

        void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            base.OnDoubleClick(e);
        }

       

        void pictureBox1_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
           
        }

        void label1_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (ExternalCall != null)
                ExternalCall(this, e);
            base.OnMouseUp(e);
            label1.ForeColor = Color.White;
            Checked = true;
            Refresh();
        }

       
        public override void LostFocusAppearance()
        {
            base.LostFocusAppearance(); 
             label1.ForeColor = Color.DimGray;
            Checked = false;
             Refresh();

        }

        protected override void AdaptHeight()
        {
            base.AdaptHeight();
            label1.Font = this.Font;
            if (szf_Text.Height > this.Height)
            {

                this.Height = (int)szf_Text.Height + 4;
            }
        }

        protected override void AdaptWidth()
        {
            base.AdaptWidth();
            label1.Text = base.Text;
            
            if (szf_Text.Width > this.Width - 40)
            {

                this.Width = (int)szf_Text.Width + 50;
            }
        }

         
        
    }
}
