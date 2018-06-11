using System;
using System.Collections.Generic;
 
using System.Text;
using System.Windows.Forms;
using dotNetLab.Widgets.Container;

namespace dotNetLab.Widgets
{
    [System.ComponentModel.ToolboxItem(false)]
    public class SQL_Query_Part :  CanvasPanel
    {
        private MobileTextBox txb_SQL;
        private TextBlock textBlock1;
        private System.Windows.Forms.ComboBox comboBox1;
        private TextBlock textBlock2;
      public  DBEngineInvoker dbInvoker;
      DataGridRealTime dataGridRealTime;
      public DataBindingPropertyEditorDialog dataBindingPropertyEditorDialog;
      private TextBlock textBlock3;
      private MobileTextBox txb_PatternSearch;
      private DataGridView dataGridView1;
      private System.Windows.Forms.LinkLabel lnk_FullScreen;
        protected override void prepareCtrls()
        {
            InitializeComponent() ;
            dataGridRealTime = new DataGridRealTime(ref dataGridView1);
        }
        protected override void prepareEvents()
        {
            base.prepareEvents();
            txb_SQL.txb.KeyUp +=  txb_SQL_KeyUp;
            txb_PatternSearch.txb.KeyUp += new System.Windows.Forms.KeyEventHandler(txb_KeyUp);
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            lnk_FullScreen.Click += Lnk_FullScreen_OnClick;
        }

        private void Lnk_FullScreen_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(comboBox1.Text))
            {
                DataGridRealTime dataRealTime_MaxWindow = null;
                String CurrentTableName = comboBox1.Text;
                Form frm = new Form();
                frm.Text = CurrentTableName;
                DataGridView dgv = new DataGridView();
                dgv.Dock = DockStyle.Fill;
                dgv.DataSource = dbInvoker.
                    ProvideTable(String.Format("select * from {0}", 
                        CurrentTableName), DBOperator.OPERATOR_QUERY_TABLE);
                this.dataGridView1.DataSource = dgv.DataSource;
                frm.Controls.Add(dgv);
                frm.WindowState = FormWindowState.Maximized;
                frm.Font = new System.Drawing.Font("微软雅黑", 11);
                 dataRealTime_MaxWindow = new DataGridRealTime(ref dgv);
                dataRealTime_MaxWindow.dataBindingPropertyEditorDialog = dataBindingPropertyEditorDialog;
                dataRealTime_MaxWindow.Connect(ref dgv, ref dbInvoker, CurrentTableName);
                 
                frm.FormClosing +=   Maxfrm_FormClosing;
                frm.Show();
            }
           
        }

        private void Maxfrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Control frm = sender as Control;
             DataGridView dgv = frm.Controls[0] as DataGridView;
            dataGridView1.DataSource = dgv.DataSource;
        }

        void txb_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {

                if (String.IsNullOrEmpty(txb_PatternSearch.Text))
                {
                 
                   dbInvoker.GetAllTableNames();
                   comboBox1.Items.Clear();
                   this.comboBox1.Items.AddRange(dbInvoker.AllTableNames.ToArray());
                }
                else
                {
                    dbInvoker.GetAllTableNames();

                    String strPattern = txb_PatternSearch.Text.Trim().ToLower();
                    comboBox1.Items.Clear();
                    for (int i = 0; i < dbInvoker.AllTableNames.Count; i++)
                    {
                        if (dbInvoker.AllTableNames[i].ToLower().Contains(strPattern))
                        {
                            this.comboBox1.Items.Add(dbInvoker.AllTableNames[i]);
                        }

                    }
                   
                   
                }

            }
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridRealTime.Connect(ref dataGridView1,ref dbInvoker,comboBox1.Text);
            if (dataBindingPropertyEditorDialog != null)
            {
                dataBindingPropertyEditorDialog.txb_TableName.Text = comboBox1.Text;
            }
           dataGridView1.DataSource = dbInvoker.ProvideTable(String.Format("select * from {0}", comboBox1.Text), DBOperator.OPERATOR_QUERY_TABLE);
        }

        void txb_SQL_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (!String.IsNullOrEmpty(txb_SQL.Text.Trim()))
                {
                     this.dataGridView1.DataSource = dbInvoker.ProvideTable(txb_SQL.Text.Trim(), DBOperator.OPERATOR_QUERY_TABLE);
                }
                else
                {
                    dbInvoker.GetAllTableNames();
                    comboBox1.Items.Clear(); 
                    this.comboBox1.Items.AddRange(dbInvoker.AllTableNames.ToArray());
                }
           }
        }
        private void InitializeComponent()
        {
            this.txb_SQL = new dotNetLab.Widgets.MobileTextBox();
            this.textBlock1 = new dotNetLab.Widgets.TextBlock();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBlock2 = new dotNetLab.Widgets.TextBlock();
            this.textBlock3 = new dotNetLab.Widgets.TextBlock();
            this.txb_PatternSearch = new dotNetLab.Widgets.MobileTextBox();
            this.lnk_FullScreen = new System.Windows.Forms.LinkLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txb_SQL
            // 
            this.txb_SQL.ActiveColor = System.Drawing.Color.Cyan;
            this.txb_SQL.BackColor = System.Drawing.Color.Transparent;
            this.txb_SQL.DataBindingInfo = null;
            this.txb_SQL.DoubleValue = double.NaN;
            this.txb_SQL.EnableMobileRound = true;
            this.txb_SQL.EnableNullValue = false;
            this.txb_SQL.FillColor = System.Drawing.Color.Transparent;
            this.txb_SQL.FloatValue = float.NaN;
            this.txb_SQL.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_SQL.ForeColor = System.Drawing.Color.Black;
            this.txb_SQL.IntValue = -2147483648;
            this.txb_SQL.LineThickness = 2F;
            this.txb_SQL.Location = new System.Drawing.Point(97, 15);
            this.txb_SQL.MainBindableProperty = "";
            this.txb_SQL.Name = "txb_SQL";
            this.txb_SQL.Radius = 30;
            this.txb_SQL.Size = new System.Drawing.Size(467, 31);
            this.txb_SQL.StaticColor = System.Drawing.Color.Gray;
            this.txb_SQL.TabIndex = 16;
            this.txb_SQL.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txb_SQL.TextBackColor = System.Drawing.Color.White;
            this.txb_SQL.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_SQL.UIElementBinders = null;
            this.txb_SQL.WhitePattern = false;
            // 
            // textBlock1
            // 
            this.textBlock1.BackColor = System.Drawing.Color.Transparent;
            this.textBlock1.BorderColor = System.Drawing.Color.Empty;
            this.textBlock1.BorderThickness = -1;
            this.textBlock1.DataBindingInfo = null;
            this.textBlock1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock1.LEDStyle = false;
            this.textBlock1.Location = new System.Drawing.Point(12, 19);
            this.textBlock1.MainBindableProperty = "SQL 语句";
            this.textBlock1.Name = "textBlock1";
            this.textBlock1.Radius = -1;
            this.textBlock1.Size = new System.Drawing.Size(82, 25);
            this.textBlock1.TabIndex = 15;
            this.textBlock1.Text = "SQL 语句";
            this.textBlock1.UIElementBinders = null;
            this.textBlock1.Vertical = false;
            this.textBlock1.WhereReturn = ((byte)(0));
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(346, 50);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 28);
            this.comboBox1.TabIndex = 18;
            // 
            // textBlock2
            // 
            this.textBlock2.BackColor = System.Drawing.Color.Transparent;
            this.textBlock2.BorderColor = System.Drawing.Color.Empty;
            this.textBlock2.BorderThickness = -1;
            this.textBlock2.DataBindingInfo = null;
            this.textBlock2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock2.LEDStyle = false;
            this.textBlock2.Location = new System.Drawing.Point(289, 54);
            this.textBlock2.MainBindableProperty = "表名";
            this.textBlock2.Name = "textBlock2";
            this.textBlock2.Radius = -1;
            this.textBlock2.Size = new System.Drawing.Size(42, 25);
            this.textBlock2.TabIndex = 15;
            this.textBlock2.Text = "表名";
            this.textBlock2.UIElementBinders = null;
            this.textBlock2.Vertical = false;
            this.textBlock2.WhereReturn = ((byte)(0));
            // 
            // textBlock3
            // 
            this.textBlock3.BackColor = System.Drawing.Color.Transparent;
            this.textBlock3.BorderColor = System.Drawing.Color.Empty;
            this.textBlock3.BorderThickness = -1;
            this.textBlock3.DataBindingInfo = null;
            this.textBlock3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock3.LEDStyle = false;
            this.textBlock3.Location = new System.Drawing.Point(27, 55);
            this.textBlock3.MainBindableProperty = "模糊查询";
            this.textBlock3.Name = "textBlock3";
            this.textBlock3.Radius = -1;
            this.textBlock3.Size = new System.Drawing.Size(78, 25);
            this.textBlock3.TabIndex = 15;
            this.textBlock3.Text = "模糊查询";
            this.textBlock3.UIElementBinders = null;
            this.textBlock3.Vertical = false;
            this.textBlock3.WhereReturn = ((byte)(0));
            // 
            // txb_PatternSearch
            // 
            this.txb_PatternSearch.ActiveColor = System.Drawing.Color.Orange;
            this.txb_PatternSearch.BackColor = System.Drawing.Color.Transparent;
            this.txb_PatternSearch.DataBindingInfo = null;
            this.txb_PatternSearch.DoubleValue = double.NaN;
            this.txb_PatternSearch.EnableMobileRound = true;
            this.txb_PatternSearch.EnableNullValue = false;
            this.txb_PatternSearch.FillColor = System.Drawing.Color.Transparent;
            this.txb_PatternSearch.FloatValue = float.NaN;
            this.txb_PatternSearch.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_PatternSearch.ForeColor = System.Drawing.Color.Black;
            this.txb_PatternSearch.IntValue = -2147483648;
            this.txb_PatternSearch.LineThickness = 2F;
            this.txb_PatternSearch.Location = new System.Drawing.Point(116, 49);
            this.txb_PatternSearch.MainBindableProperty = "";
            this.txb_PatternSearch.Name = "txb_PatternSearch";
            this.txb_PatternSearch.Radius = -1;
            this.txb_PatternSearch.Size = new System.Drawing.Size(171, 31);
            this.txb_PatternSearch.StaticColor = System.Drawing.Color.DarkGray;
            this.txb_PatternSearch.TabIndex = 19;
            this.txb_PatternSearch.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txb_PatternSearch.TextBackColor = System.Drawing.SystemColors.Window;
            this.txb_PatternSearch.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_PatternSearch.UIElementBinders = null;
            this.txb_PatternSearch.WhitePattern = false;
            // 
            // lnk_FullScreen
            // 
            this.lnk_FullScreen.AutoSize = true;
            this.lnk_FullScreen.Location = new System.Drawing.Point(518, 56);
            this.lnk_FullScreen.Name = "lnk_FullScreen";
            this.lnk_FullScreen.Size = new System.Drawing.Size(39, 20);
            this.lnk_FullScreen.TabIndex = 20;
            this.lnk_FullScreen.TabStop = true;
            this.lnk_FullScreen.Text = "全屏";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(589, 334);
            this.dataGridView1.TabIndex = 21;
            // 
            // SQL_Query_Part
            // 
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lnk_FullScreen);
            this.Controls.Add(this.txb_PatternSearch);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txb_SQL);
            this.Controls.Add(this.textBlock3);
            this.Controls.Add(this.textBlock2);
            this.Controls.Add(this.textBlock1);
            this.Name = "SQL_Query_Part";
            this.NormalColor = System.Drawing.Color.Transparent;
            this.Size = new System.Drawing.Size(605, 480);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
