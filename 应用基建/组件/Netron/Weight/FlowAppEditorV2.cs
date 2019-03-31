using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Common 
{
    public class FlowAppEditorV2 : UserControl
    {
        private Button btn_SaveFlowAppFile;
        public FlowEditor flowEditor1;

        public FlowAppEditorV2()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.flowEditor1 = new dotNetLab.Common.FlowEditor();
            this.btn_SaveFlowAppFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowEditor1
            // 
            this.flowEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowEditor1.Location = new System.Drawing.Point(0, 49);
            this.flowEditor1.Name = "flowEditor1";
            this.flowEditor1.Size = new System.Drawing.Size(872, 577);
            this.flowEditor1.TabIndex = 0;
            // 
            // btn_SaveFlowAppFile
            // 
            this.btn_SaveFlowAppFile.Location = new System.Drawing.Point(13, 9);
            this.btn_SaveFlowAppFile.Name = "btn_SaveFlowAppFile";
            this.btn_SaveFlowAppFile.Size = new System.Drawing.Size(125, 29);
            this.btn_SaveFlowAppFile.TabIndex = 1;
            this.btn_SaveFlowAppFile.Text = "保存";
            this.btn_SaveFlowAppFile.UseVisualStyleBackColor = true;
            this.btn_SaveFlowAppFile.Click += new System.EventHandler(this.btn_SaveFlowAppFile_Click);
            // 
            // FlowAppEditorV2
            // 
            this.Controls.Add(this.btn_SaveFlowAppFile);
            this.Controls.Add(this.flowEditor1);
            this.Name = "FlowAppEditorV2";
            this.Size = new System.Drawing.Size(872, 626);
            this.ResumeLayout(false);

        }

        protected virtual void btn_SaveFlowAppFile_Click(object sender, EventArgs e)
        {
           
        }
    }
}
