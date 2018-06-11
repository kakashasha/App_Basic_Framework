using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Widgets.Container;

namespace dotNetLab.Widgets
{
    [System.ComponentModel.ToolboxItem(false)]
  public  class ConveyDataToOtherDBPart : CanvasPanel
    {
        private MobileProgressBar mobileProgressBar1;
        private TextBlock textBlock1;
        private TextBlock textBlock2;
        private Card card1;
        public  DBEngineInvoker dbInvoker;
        DataGridRealTime dataGridRealTime;
        private MobileComboBox cmbx_DBEngineName;//cmbx_DBEngineName;
        private TextBlock textBlock3;
        private MobileTextBox txb_DBConnectionSQL;
        private MobileButton mobileButton1;
        private MobileButton mobileButton2;
        private Card card2;
        private TextBlock textBlock4;
        private TextBlock textBlock6;
        public MobileTextBox txb_DBName;
        private System.Windows.Forms.PictureBox pictureBox1;
        public DataBindingPropertyEditorDialog dataBindingPropertyEditorDialog;
        public readonly String SQLSERVERCONNECTIONSTRING = "server={0},{1};Initial Catalog={2};User ID={3};Password ={4}";
        public readonly String LOCALDBCONNECTIONSTRING = "server=(localdb)\\MSSQLLocalDB;AttachDBFilename={0} ;";
        public readonly String POSTGRESQLCONNECTIONSTRING = "server={0};username={1};database={2};port={3};password={4};";
        public readonly String ORACLECONNECTIONSTRING = "user id={0};password={1};data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST={2})(PORT={3}))(CONNECT_DATA=(SERVICE_NAME={4})))";
        public readonly String MYSQLCONNECTIONSTRING = "server={0};user={1};database={2};port={3};password={4};";
        public readonly String FIREBIRDCONNECTIONSTRING = "User=SYSDBA;Password=masterkey;Database={0};Charset=utf8;ServerType=1";
        public readonly String SQLCECONNECTIONSTRING = "data source={0};";
        public readonly String SQLITEConnectionString = "data source={0}";
      protected override void prepareCtrls()
      {
          InitializeComponent();
          cmbx_DBEngineName.Items = new string[]{"SQLITE","SQLCE","FireBird","LOCALDB",
          "SQLSERVER","POSTGRESQL","MySQL" };
          //this.lbl_DBName.Text = dbInvoker
      }

        protected override void prepareEvents()
        {
            base.prepareEvents(); 
            this.cmbx_DBEngineName.SelectedItemChanged += CmbxDbEngineNameOnSelectedItemChanged;
        }

        private void CmbxDbEngineNameOnSelectedItemChanged(object sender, EventArgs e)
        {
            UserControl ctrl = sender as UserControl;
            String strSQL = null;
            switch (ctrl.Text)
            {
                case "SQLITE" : strSQL = SQLITEConnectionString;break;
                case "SQLCE":strSQL = SQLCECONNECTIONSTRING;break ; 
                case "FireBird" :strSQL = FIREBIRDCONNECTIONSTRING;break ;
                case "LOCALDB" :strSQL = LOCALDBCONNECTIONSTRING ; break ; 
                case "SQLSERVER" :strSQL = SQLSERVERCONNECTIONSTRING ;break ; 
                case "POSTGRESQL" :strSQL =POSTGRESQLCONNECTIONSTRING ;break ;
                case "MySQL":strSQL = MYSQLCONNECTIONSTRING; break;
                   
            }

            this.txb_DBConnectionSQL.Text = strSQL;
        }

        private void InitializeComponent()
      {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConveyDataToOtherDBPart));
            this.mobileProgressBar1 = new dotNetLab.Widgets.MobileProgressBar();
            this.card1 = new dotNetLab.Widgets.Card();
            this.textBlock1 = new dotNetLab.Widgets.TextBlock();
            this.textBlock2 = new dotNetLab.Widgets.TextBlock();
            this.cmbx_DBEngineName = new dotNetLab.Widgets.MobileComboBox();
            this.textBlock3 = new dotNetLab.Widgets.TextBlock();
            this.txb_DBConnectionSQL = new dotNetLab.Widgets.MobileTextBox();
            this.mobileButton1 = new dotNetLab.Widgets.MobileButton();
            this.mobileButton2 = new dotNetLab.Widgets.MobileButton();
            this.card2 = new dotNetLab.Widgets.Card();
            this.textBlock4 = new dotNetLab.Widgets.TextBlock();
            this.textBlock6 = new dotNetLab.Widgets.TextBlock();
            this.txb_DBName = new dotNetLab.Widgets.MobileTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mobileProgressBar1
            // 
            this.mobileProgressBar1.Alignment = dotNetLab.Widgets.Alignments.Left;
            this.mobileProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.mobileProgressBar1.BottomColor = System.Drawing.Color.Silver;
            this.mobileProgressBar1.CenterColor = System.Drawing.Color.White;
            this.mobileProgressBar1.CenterSize = 104;
            this.mobileProgressBar1.DataBindingInfo = null;
            this.mobileProgressBar1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.mobileProgressBar1.ForeColor = System.Drawing.Color.DimGray;
            this.mobileProgressBar1.Location = new System.Drawing.Point(49, 28);
            this.mobileProgressBar1.MainBindableProperty = "70";
            this.mobileProgressBar1.Name = "mobileProgressBar1";
            this.mobileProgressBar1.ProgressBarStyle = dotNetLab.Widgets.MobileProgressBar.ProgressBarStyles.Ring;
            this.mobileProgressBar1.ProgressColor = System.Drawing.Color.DodgerBlue;
            this.mobileProgressBar1.RingColor = System.Drawing.Color.DodgerBlue;
            this.mobileProgressBar1.RingThickness = 8;
            this.mobileProgressBar1.Size = new System.Drawing.Size(120, 120);
            this.mobileProgressBar1.TabIndex = 0;
            this.mobileProgressBar1.UIElementBinders = null;
            this.mobileProgressBar1.Value = 70F;
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.Transparent;
            this.card1.BorderColor = System.Drawing.Color.DarkGray;
            this.card1.BorderThickness = -1;
            this.card1.Controls.Add(this.textBlock1);
            this.card1.Controls.Add(this.mobileProgressBar1);
            this.card1.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.card1.DataBindingInfo = null;
            this.card1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.card1.HeadColor = System.Drawing.Color.DeepPink;
            this.card1.HeaderAlignment = dotNetLab.Widgets.Alignments.Down;
            this.card1.HeadHeight = 40;
            this.card1.ImagePos = new System.Drawing.Point(0, 0);
            this.card1.ImageSize = new System.Drawing.Size(0, 0);
            this.card1.Location = new System.Drawing.Point(10, 239);
            this.card1.MainBindableProperty = "card1";
            this.card1.Name = "card1";
            this.card1.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.card1.Radius = 10;
            this.card1.Size = new System.Drawing.Size(221, 208);
            this.card1.Source = null;
            this.card1.TabIndex = 1;
            this.card1.Text = "card1";
            this.card1.UIElementBinders = null;
            // 
            // textBlock1
            // 
            this.textBlock1.BackColor = System.Drawing.Color.Transparent;
            this.textBlock1.BorderColor = System.Drawing.Color.Empty;
            this.textBlock1.BorderThickness = -1;
            this.textBlock1.DataBindingInfo = null;
            this.textBlock1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock1.ForeColor = System.Drawing.Color.White;
            this.textBlock1.LEDStyle = false;
            this.textBlock1.Location = new System.Drawing.Point(72, 175);
            this.textBlock1.MainBindableProperty = "操作进度";
            this.textBlock1.Name = "textBlock1";
            this.textBlock1.Radius = -1;
            this.textBlock1.Size = new System.Drawing.Size(78, 25);
            this.textBlock1.TabIndex = 1;
            this.textBlock1.Text = "操作进度";
            this.textBlock1.UIElementBinders = null;
            this.textBlock1.Vertical = false;
            this.textBlock1.WhereReturn = ((byte)(0));
            // 
            // textBlock2
            // 
            this.textBlock2.BackColor = System.Drawing.Color.Transparent;
            this.textBlock2.BorderColor = System.Drawing.Color.Empty;
            this.textBlock2.BorderThickness = -1;
            this.textBlock2.DataBindingInfo = null;
            this.textBlock2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock2.LEDStyle = false;
            this.textBlock2.Location = new System.Drawing.Point(37, 69);
            this.textBlock2.MainBindableProperty = "要转存的数据库名";
            this.textBlock2.Name = "textBlock2";
            this.textBlock2.Radius = -1;
            this.textBlock2.Size = new System.Drawing.Size(150, 25);
            this.textBlock2.TabIndex = 2;
            this.textBlock2.Text = "要转存的数据库名";
            this.textBlock2.UIElementBinders = null;
            this.textBlock2.Vertical = false;
            this.textBlock2.WhereReturn = ((byte)(0));
            // 
            // cmbx_DBEngineName
            // 
            this.cmbx_DBEngineName.BackColor = System.Drawing.Color.White;
            this.cmbx_DBEngineName.BorderColor = System.Drawing.Color.Gray;
            this.cmbx_DBEngineName.DataBindingInfo = null;
            this.cmbx_DBEngineName.DisplayItems = 0;
            this.cmbx_DBEngineName.EnableAnimation = false;
            this.cmbx_DBEngineName.Items = null;
            this.cmbx_DBEngineName.Location = new System.Drawing.Point(198, 66);
            this.cmbx_DBEngineName.MainBindableProperty = "";
            this.cmbx_DBEngineName.Name = "cmbx_DBEngineName";
            this.cmbx_DBEngineName.SelectedItem = null;
            this.cmbx_DBEngineName.Size = new System.Drawing.Size(339, 31);
            this.cmbx_DBEngineName.TabIndex = 3;
            this.cmbx_DBEngineName.UIElementBinders = null;
            // 
            // textBlock3
            // 
            this.textBlock3.BackColor = System.Drawing.Color.Transparent;
            this.textBlock3.BorderColor = System.Drawing.Color.Empty;
            this.textBlock3.BorderThickness = -1;
            this.textBlock3.DataBindingInfo = null;
            this.textBlock3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock3.LEDStyle = false;
            this.textBlock3.Location = new System.Drawing.Point(54, 121);
            this.textBlock3.MainBindableProperty = "连接字符串";
            this.textBlock3.Name = "textBlock3";
            this.textBlock3.Radius = -1;
            this.textBlock3.Size = new System.Drawing.Size(96, 25);
            this.textBlock3.TabIndex = 2;
            this.textBlock3.Text = "连接字符串";
            this.textBlock3.UIElementBinders = null;
            this.textBlock3.Vertical = false;
            this.textBlock3.WhereReturn = ((byte)(0));
            // 
            // txb_DBConnectionSQL
            // 
            this.txb_DBConnectionSQL.ActiveColor = System.Drawing.Color.Cyan;
            this.txb_DBConnectionSQL.BackColor = System.Drawing.Color.Transparent;
            this.txb_DBConnectionSQL.DataBindingInfo = null;
            this.txb_DBConnectionSQL.DoubleValue = double.NaN;
            this.txb_DBConnectionSQL.EnableMobileRound = true;
            this.txb_DBConnectionSQL.EnableNullValue = false;
            this.txb_DBConnectionSQL.FillColor = System.Drawing.Color.Transparent;
            this.txb_DBConnectionSQL.FloatValue = float.NaN;
            this.txb_DBConnectionSQL.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_DBConnectionSQL.ForeColor = System.Drawing.Color.Black;
            this.txb_DBConnectionSQL.IntValue = -2147483648;
            this.txb_DBConnectionSQL.LineThickness = 2F;
            this.txb_DBConnectionSQL.Location = new System.Drawing.Point(196, 114);
            this.txb_DBConnectionSQL.MainBindableProperty = "";
            this.txb_DBConnectionSQL.Name = "txb_DBConnectionSQL";
            this.txb_DBConnectionSQL.Radius = 31;
            this.txb_DBConnectionSQL.Size = new System.Drawing.Size(339, 32);
            this.txb_DBConnectionSQL.StaticColor = System.Drawing.Color.Gray;
            this.txb_DBConnectionSQL.TabIndex = 4;
            this.txb_DBConnectionSQL.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txb_DBConnectionSQL.TextBackColor = System.Drawing.SystemColors.Window;
            this.txb_DBConnectionSQL.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_DBConnectionSQL.UIElementBinders = null;
            this.txb_DBConnectionSQL.WhitePattern = false;
            // 
            // mobileButton1
            // 
            this.mobileButton1.BackColor = System.Drawing.Color.Transparent;
            this.mobileButton1.BorderColor = System.Drawing.Color.Empty;
            this.mobileButton1.BorderThickness = -1;
            this.mobileButton1.CornerAligment = dotNetLab.Widgets.Alignments.All;
            this.mobileButton1.DataBindingInfo = null;
            this.mobileButton1.EnableMobileRound = true;
            this.mobileButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.mobileButton1.ForeColor = System.Drawing.Color.White;
            this.mobileButton1.GapBetweenTextImage = 8;
            this.mobileButton1.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
            this.mobileButton1.ImageSize = new System.Drawing.Size(0, 0);
            this.mobileButton1.LEDStyle = false;
            this.mobileButton1.Location = new System.Drawing.Point(396, 169);
            this.mobileButton1.MainBindableProperty = "交接数据";
            this.mobileButton1.Name = "mobileButton1";
            this.mobileButton1.NeedAnimation = true;
            this.mobileButton1.NormalColor = System.Drawing.Color.DodgerBlue;
            this.mobileButton1.PressColor = System.Drawing.Color.Cyan;
            this.mobileButton1.Radius = 37;
            this.mobileButton1.Size = new System.Drawing.Size(139, 38);
            this.mobileButton1.Source = null;
            this.mobileButton1.TabIndex = 5;
            this.mobileButton1.Text = "交接数据";
            this.mobileButton1.UIElementBinders = null;
            this.mobileButton1.Vertical = false;
            this.mobileButton1.WhereReturn = ((byte)(0));
            // 
            // mobileButton2
            // 
            this.mobileButton2.BackColor = System.Drawing.Color.Transparent;
            this.mobileButton2.BorderColor = System.Drawing.Color.Empty;
            this.mobileButton2.BorderThickness = -1;
            this.mobileButton2.CornerAligment = dotNetLab.Widgets.Alignments.All;
            this.mobileButton2.DataBindingInfo = null;
            this.mobileButton2.EnableMobileRound = true;
            this.mobileButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.mobileButton2.ForeColor = System.Drawing.Color.White;
            this.mobileButton2.GapBetweenTextImage = 8;
            this.mobileButton2.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
            this.mobileButton2.ImageSize = new System.Drawing.Size(0, 0);
            this.mobileButton2.LEDStyle = false;
            this.mobileButton2.Location = new System.Drawing.Point(255, 169);
            this.mobileButton2.MainBindableProperty = "尝试连接";
            this.mobileButton2.Name = "mobileButton2";
            this.mobileButton2.NeedAnimation = true;
            this.mobileButton2.NormalColor = System.Drawing.Color.MediumVioletRed;
            this.mobileButton2.PressColor = System.Drawing.Color.Pink;
            this.mobileButton2.Radius = 37;
            this.mobileButton2.Size = new System.Drawing.Size(135, 38);
            this.mobileButton2.Source = null;
            this.mobileButton2.TabIndex = 5;
            this.mobileButton2.Text = "尝试连接";
            this.mobileButton2.UIElementBinders = null;
            this.mobileButton2.Vertical = false;
            this.mobileButton2.WhereReturn = ((byte)(0));
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.Transparent;
            this.card2.BorderColor = System.Drawing.Color.DarkGray;
            this.card2.BorderThickness = -1;
            this.card2.Controls.Add(this.textBlock4);
            this.card2.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.card2.DataBindingInfo = null;
            this.card2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.card2.HeadColor = System.Drawing.Color.LimeGreen;
            this.card2.HeaderAlignment = dotNetLab.Widgets.Alignments.Right;
            this.card2.HeadHeight = 40;
            this.card2.ImagePos = new System.Drawing.Point(0, 0);
            this.card2.ImageSize = new System.Drawing.Size(0, 0);
            this.card2.Location = new System.Drawing.Point(242, 239);
            this.card2.MainBindableProperty = "card2";
            this.card2.Name = "card2";
            this.card2.NormalColor = System.Drawing.Color.WhiteSmoke;
            this.card2.Radius = 10;
            this.card2.Size = new System.Drawing.Size(347, 208);
            this.card2.Source = null;
            this.card2.TabIndex = 6;
            this.card2.Text = "card2";
            this.card2.UIElementBinders = null;
            // 
            // textBlock4
            // 
            this.textBlock4.BackColor = System.Drawing.Color.Transparent;
            this.textBlock4.BorderColor = System.Drawing.Color.Empty;
            this.textBlock4.BorderThickness = -1;
            this.textBlock4.DataBindingInfo = null;
            this.textBlock4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock4.ForeColor = System.Drawing.Color.White;
            this.textBlock4.LEDStyle = false;
            this.textBlock4.Location = new System.Drawing.Point(310, 34);
            this.textBlock4.MainBindableProperty = "数据交接明细";
            this.textBlock4.Name = "textBlock4";
            this.textBlock4.Radius = -1;
            this.textBlock4.Size = new System.Drawing.Size(27, 140);
            this.textBlock4.TabIndex = 0;
            this.textBlock4.Text = "数据交接明细";
            this.textBlock4.UIElementBinders = null;
            this.textBlock4.Vertical = true;
            this.textBlock4.WhereReturn = ((byte)(0));
            // 
            // textBlock6
            // 
            this.textBlock6.BackColor = System.Drawing.Color.Transparent;
            this.textBlock6.BorderColor = System.Drawing.Color.Empty;
            this.textBlock6.BorderThickness = -1;
            this.textBlock6.DataBindingInfo = null;
            this.textBlock6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock6.LEDStyle = false;
            this.textBlock6.Location = new System.Drawing.Point(47, 26);
            this.textBlock6.MainBindableProperty = "当前数据库名";
            this.textBlock6.Name = "textBlock6";
            this.textBlock6.Radius = -1;
            this.textBlock6.Size = new System.Drawing.Size(114, 25);
            this.textBlock6.TabIndex = 7;
            this.textBlock6.Text = "当前数据库名";
            this.textBlock6.UIElementBinders = null;
            this.textBlock6.Vertical = false;
            this.textBlock6.WhereReturn = ((byte)(0));
            // 
            // txb_DBName
            // 
            this.txb_DBName.ActiveColor = System.Drawing.Color.Orange;
            this.txb_DBName.BackColor = System.Drawing.Color.Transparent;
            this.txb_DBName.DataBindingInfo = null;
            this.txb_DBName.DoubleValue = double.NaN;
            this.txb_DBName.EnableMobileRound = true;
            this.txb_DBName.EnableNullValue = false;
            this.txb_DBName.FillColor = System.Drawing.Color.Transparent;
            this.txb_DBName.FloatValue = float.NaN;
            this.txb_DBName.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_DBName.ForeColor = System.Drawing.Color.Black;
            this.txb_DBName.IntValue = -2147483648;
            this.txb_DBName.LineThickness = 2F;
            this.txb_DBName.Location = new System.Drawing.Point(198, 23);
            this.txb_DBName.MainBindableProperty = "";
            this.txb_DBName.Name = "txb_DBName";
            this.txb_DBName.Radius = -1;
            this.txb_DBName.Size = new System.Drawing.Size(337, 31);
            this.txb_DBName.StaticColor = System.Drawing.Color.DarkGray;
            this.txb_DBName.TabIndex = 8;
            this.txb_DBName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txb_DBName.TextBackColor = System.Drawing.SystemColors.Window;
            this.txb_DBName.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_DBName.UIElementBinders = null;
            this.txb_DBName.WhitePattern = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(49, 162);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // ConveyDataToOtherDBPart
            // 
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txb_DBName);
            this.Controls.Add(this.textBlock6);
            this.Controls.Add(this.card2);
            this.Controls.Add(this.mobileButton2);
            this.Controls.Add(this.mobileButton1);
            this.Controls.Add(this.txb_DBConnectionSQL);
            this.Controls.Add(this.cmbx_DBEngineName);
            this.Controls.Add(this.textBlock3);
            this.Controls.Add(this.textBlock2);
            this.Controls.Add(this.card1);
            this.Name = "ConveyDataToOtherDBPart";
            this.NormalColor = System.Drawing.Color.Transparent;
            this.Size = new System.Drawing.Size(605, 480);
            this.card1.ResumeLayout(false);
            this.card2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

      }
  }
}
