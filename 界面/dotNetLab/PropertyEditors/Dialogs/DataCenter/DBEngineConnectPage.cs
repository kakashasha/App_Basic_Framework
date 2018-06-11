using System;
using System.Collections.Generic;
using System.Text;
using dotNetLab.Forms;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace dotNetLab.Widgets
{
    public class DBEngineConnectPage : Session
    {
        public Object DataBindingPropertyEditorDialogObject;
        private ColorDecorator colorDecorator1;
        private TextBlock textBlock1;
        private MobileButton btn_AttempConnecting;
        private MobileButton btn_Connect;
        public MobileComboBox cmbx_DBEngineNames;
        bool bEmbeddedDB = true;
        DBEngineInvoker dbInvoker;
        private Card card1;
        private MobileTextBox txb_pwd;
        private MobileTextBox txb_UserName;
        private MobileTextBox txb_Host;
        private MobileTextBox txb_Port;
        private TextBlock textBlock5;
        private TextBlock textBlock4;
        private TextBlock textBlock3;
        private TextBlock textBlock2;
        private Card card2;
        private TextBlock textBlock8;
        private TextBlock textBlock9;
        private Card card3;
        private Detail btn_ViewDBFilePath;
        private MobileTextBox txb_EmbeddedDBName;
        private TextBlock textBlock6;
        private TextBlock textBlock11;
        private TextBlock textBlock10;
        private TextBlock textBlock7;
        private TextBlock textBlock12;
        private TextBlock textBlock14;
        private Direction btn_LoadConnCofig;
        private TextBlock textblock20;
        private Direction btn_SaveConnCofig;
        private MobileTextBox txb_HugeDBName;
        private TextBlock textBlock15;
        public Toggle tgl_EmbeddedConfig;
        private TextBlock textBlock13;
        List<String> lst_DBEngineNames;

        public List<String> DBEngineNames
        {
            get { return lst_DBEngineNames; }
            set { lst_DBEngineNames = value; }
        }
        public bool EmbeddedDB
        {
            get { return bEmbeddedDB; }
            set
            {
                bEmbeddedDB = value;
                if (!value)
                {


                    txb_HugeDBName.Enabled = txb_Host.Enabled = txb_Port.Enabled = txb_pwd.Enabled = txb_UserName.Enabled = true;
                  txb_HugeDBName.StaticColor =  txb_Host.StaticColor = txb_Port.StaticColor = txb_Port.StaticColor = txb_pwd.StaticColor = txb_UserName.StaticColor = Color.Crimson;
                }
                else
                {

                    txb_HugeDBName.Enabled = txb_Host.Enabled = txb_Port.Enabled = txb_pwd.Enabled = txb_UserName.Enabled = false;
                    txb_HugeDBName.StaticColor = txb_Host.StaticColor = txb_Port.StaticColor = txb_Port.StaticColor = txb_pwd.StaticColor = txb_UserName.StaticColor = Color.LightGray;
                }
            }
        }
        protected override void prepareData()
        {
            base.prepareData();
            dbInvoker = new DBEngineInvoker();
        }
        protected override void prepareCtrls()
        {
            base.prepareCtrls();
            InitializeComponent();
           // dbInvoker.Connect(DBEngineNames.SQLITE, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\A.db");

        }
        protected override void prepareEvents()
        {
            base.prepareEvents();
           // this.Load += new EventHandler(DBEngineConnectPage_Load);
            btn_ViewDBFilePath.Click += new EventHandler(btn_ViewDBFilePath_Click);
            this.btn_AttempConnecting.Click += new EventHandler(btn_AttempConnecting_Click);
            this.btn_Connect.Click += new EventHandler(btn_Connect_Click);
            this.FormClosing += new FormClosingEventHandler(DBEngineConnectPage_FormClosing);
            this.btn_LoadConnCofig.Click += new EventHandler(btn_LoadConnCofig_Click);
            this.btn_SaveConnCofig.Click += new EventHandler(btn_SaveConnCofig_Click);
            this.cmbx_DBEngineNames.Items = new String[]{
              "SQLITE","SQLCE","FireBird","LOCALDB",
          "SQLSERVER","POSTGRESQL","MySQL" 
          };

            this.cmbx_DBEngineNames.SelectedItem = "SQLITE";
            this.cmbx_DBEngineNames.SelectedItemChanged += new EventHandler(mobileComboBox1_SelectedItemChanged);
            EmbeddedDB = true;
            lst_DBEngineNames = new List<String>();
            lst_DBEngineNames.AddRange(cmbx_DBEngineNames.Items);
            bEmbeddedDB = true;

        }

        void btn_SaveConnCofig_Click(object sender, EventArgs e)
        {
            if (EmbeddedDB)
                SaveEmbeddedConnectionConfig();
            else
                SaveHugeDBConnectionConfig();
        }

        void btn_LoadConnCofig_Click(object sender, EventArgs e)
        {
            if (tgl_EmbeddedConfig.Checked)
                LoadEmbeddedConnectionConfig();
            else
                LoadHugeDBConnectionConfig();
        }

        void  SaveEmbeddedConnectionConfig ( )
        {
            if (bEmbeddedDB)
            {
                string strDesignTimeConfig = "D:/EmbeddedDBDesignTimeConnection.txt";
           
                File.WriteAllText(strDesignTimeConfig, String.Format("{0}\r\n{1}", cmbx_DBEngineNames.SelectedItem, txb_EmbeddedDBName.Text.Trim()), Encoding.Default);
                DoneTip = "已成功保存";
                card3.Refresh();
            }
        }

        void  LoadEmbeddedConnectionConfig ( )
        {
            string strDesignTimeConfig = "D:/EmbeddedDBDesignTimeConnection.txt";
 
           
            if (File.Exists(strDesignTimeConfig))
            {
                String[] strLines = File.ReadAllLines(strDesignTimeConfig, Encoding.Default);
                if (strLines != null)
                {
                    if (strLines.Length < 2)
                        dotNetLab.Tipper.Tip_Info_Error("出现错误，‘D:/EmbeddedDBDesignTimeConnection.txt’内容行不能少于2行");
                    else
                    {
                        if (strLines.Length == 2)
                        {
                            EmbeddedDB = true;
                            // String str = lst_DBEngineNames.IndexOf(strLines[0]);
                            this.cmbx_DBEngineNames.SelectedItem = strLines[0];
                            txb_EmbeddedDBName.Text = strLines[1];

                        }

                        
                        else
                            dotNetLab.Tipper.Tip_Info_Error("出现错误，‘D:/EmbeddedDBDesignTimeConnection.txt’内容行必须为6行");



                    }
                }
                else
                {
                    dotNetLab.Tipper.Tip_Info_Error("出现错误，‘D:/EmbeddedDBDesignTimeConnection.txt’内容不能为空");
                }
            }
            else
            {
                dotNetLab.Tipper.Tip_Info_Error("出现错误，未存在‘D:/EmbeddedDBDesignTimeConnection.txt’");
                DialogResult dlt = MessageBox.Show("是否需要创建‘D:/EmbeddedDBDesignTimeConnection.txt’", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlt == DialogResult.Yes)
                {
                    File.WriteAllText(strDesignTimeConfig, "SQLITE\r\nshikii");
                }


            }
        }

        void  SaveHugeDBConnectionConfig ( )
        {
            string strDesignTimeConfig = "D:/HugeDBDesignTimeConnection.txt";
           
            
           File.WriteAllText(strDesignTimeConfig, String.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{5}",
                    cmbx_DBEngineNames.SelectedItem, txb_Host.Text.Trim(), txb_Port.Text.Trim(), txb_UserName.Text.Trim(), txb_pwd.Text.Trim(), txb_HugeDBName.Text.Trim()), Encoding.Default);
           DoneTip = "已成功保存";
           card3.Refresh();
        }

        void  LoadHugeDBConnectionConfig ( )
        {
            string strDesignTimeConfig = "D:/HugeDBDesignTimeConnection.txt";
            if (File.Exists(strDesignTimeConfig))
            {
              String [] strLines =  File.ReadAllLines(strDesignTimeConfig, Encoding.Default);
              if (strLines != null)
              {
                  if(strLines.Length<2)
                      dotNetLab.Tipper.Tip_Info_Error("出现错误，‘D:/HugeDBDesignTimeConnection.txt’内容行不能少于2行");
                  else
                  {
                      
                      
                          if (strLines.Length == 6)
                          {
                              EmbeddedDB = false;
                              this.cmbx_DBEngineNames.SelectedItem = strLines[0];
                              txb_Host.Text = strLines[1];
                              txb_Port.Text = strLines[2];
                              txb_UserName.Text = strLines[3];
                              txb_pwd.Text = strLines[4];
                              txb_HugeDBName.Text = strLines[5];
                          }
                          else
                              dotNetLab.Tipper.Tip_Info_Error("出现错误，‘D:/HugeDBDesignTimeConnection.txt’内容行必须为6行");
                          
                      

                  }
                }
              else
              {
                  dotNetLab.Tipper.Tip_Info_Error("出现错误，‘D:/HugeDBDesignTimeConnection.txt’内容不能为空");
              }
            }
            else
            {
                dotNetLab.Tipper.Tip_Info_Error("出现错误，未存在‘D:/HugeDBDesignTimeConnection.txt’");
                DialogResult dlt = MessageBox.Show("是否需要创建‘D:/HugeDBDesignTimeConnection.txt’", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
              if (dlt == DialogResult.Yes)
              {
                  File.WriteAllText(strDesignTimeConfig, "SQLITE\r\nshikii");
             }
                 

            }
        }
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.EnableDrawUpDownPattern = true;
            Img_Up = dotNetLab.UI.RibbonGeometry;
            Img_Down = dotNetLab.UI.RibbonSchoolStuff;
        }

        void btn_Connect_Click(object sender, EventArgs e)
        {
            int n = lst_DBEngineNames.IndexOf(cmbx_DBEngineNames.SelectedItem);
            if (bEmbeddedDB)
            {
                dbInvoker.Connect((DBEngineNames)n, txb_EmbeddedDBName.Text.Trim());
            }
            else
            {
                dbInvoker.Connect((DBEngineNames)n, txb_Host.Text.Trim(), txb_Port.IntValue,
                txb_UserName.Text.Trim(), txb_pwd.Text.Trim(), txb_HugeDBName.Text.Trim());

            }
            if (dbInvoker.Status)
            {
                //To DO
                this.Hide();
                DBEngineEditorPage EditorPage = new DBEngineEditorPage();
                EditorPage.dbInvoker = dbInvoker;
                EditorPage.DataBindingPropertyEditorDialogObject = DataBindingPropertyEditorDialogObject;
                EditorPage.ShowDialog();
                EditorPage.Dispose();
                this.Show();
            }
            else
            {
                ;
            }
        }

        void DBEngineConnectPage_FormClosing(object sender, FormClosingEventArgs e)
        {
                if (dbInvoker.ThisCommand!= null)
                {

                    if (dbInvoker.Status)
                        dbInvoker.Dispose();
                }
                 
            }

        void btn_AttempConnecting_Click(object sender, EventArgs e)
        {
            int n = lst_DBEngineNames.IndexOf(cmbx_DBEngineNames.SelectedItem);
            if (bEmbeddedDB)
            {
                dbInvoker.Connect((DBEngineNames)n,txb_EmbeddedDBName.Text.Trim());
            }
            else
            {
                dbInvoker.Connect((DBEngineNames)n,txb_Host.Text.Trim(),txb_Port.IntValue,
                    txb_UserName.Text.Trim(),txb_pwd.Text.Trim(),txb_HugeDBName.Text.Trim());
                
            }
            if (dbInvoker.Status)
            {
                this.InfoTip = "成功连接到数据库";
                card3.Refresh();
                dbInvoker.Dispose();
            }
            else
            {
                this.ErrorTip = "未能连接到数据库";
                card3.Refresh();
            }

        }

        void btn_ViewDBFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.txb_EmbeddedDBName.Text =  ofd.FileName ;
            }
            ofd.Dispose();
            ofd = null;
        }

        void mobileComboBox1_SelectedItemChanged(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            if (ctrl.Text.Equals("MySQL") || ctrl.Text.Equals("POSTGRESQL") || ctrl.Text.Equals("SQLSERVER"))
            {
                 EmbeddedDB = false;
                 
            }
            else
            {
                 EmbeddedDB = true;
                 
            }
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBEngineConnectPage));
            this.colorDecorator1 = new dotNetLab.Widgets.ColorDecorator();
            this.textBlock1 = new dotNetLab.Widgets.TextBlock();
            this.btn_AttempConnecting = new dotNetLab.Widgets.MobileButton();
            this.btn_Connect = new dotNetLab.Widgets.MobileButton();
            this.cmbx_DBEngineNames = new dotNetLab.Widgets.MobileComboBox();
            this.card1 = new dotNetLab.Widgets.Card();
            this.textBlock8 = new dotNetLab.Widgets.TextBlock();
            this.txb_HugeDBName = new dotNetLab.Widgets.MobileTextBox();
            this.txb_pwd = new dotNetLab.Widgets.MobileTextBox();
            this.txb_UserName = new dotNetLab.Widgets.MobileTextBox();
            this.txb_Host = new dotNetLab.Widgets.MobileTextBox();
            this.txb_Port = new dotNetLab.Widgets.MobileTextBox();
            this.textBlock15 = new dotNetLab.Widgets.TextBlock();
            this.textBlock5 = new dotNetLab.Widgets.TextBlock();
            this.textBlock4 = new dotNetLab.Widgets.TextBlock();
            this.textBlock3 = new dotNetLab.Widgets.TextBlock();
            this.textBlock2 = new dotNetLab.Widgets.TextBlock();
            this.card2 = new dotNetLab.Widgets.Card();
            this.tgl_EmbeddedConfig = new dotNetLab.Widgets.Toggle();
            this.textBlock14 = new dotNetLab.Widgets.TextBlock();
            this.btn_LoadConnCofig = new dotNetLab.Widgets.Direction();
            this.textBlock13 = new dotNetLab.Widgets.TextBlock();
            this.textblock20 = new dotNetLab.Widgets.TextBlock();
            this.btn_SaveConnCofig = new dotNetLab.Widgets.Direction();
            this.textBlock10 = new dotNetLab.Widgets.TextBlock();
            this.textBlock7 = new dotNetLab.Widgets.TextBlock();
            this.textBlock11 = new dotNetLab.Widgets.TextBlock();
            this.textBlock9 = new dotNetLab.Widgets.TextBlock();
            this.card3 = new dotNetLab.Widgets.Card();
            this.textBlock12 = new dotNetLab.Widgets.TextBlock();
            this.btn_ViewDBFilePath = new dotNetLab.Widgets.Detail();
            this.txb_EmbeddedDBName = new dotNetLab.Widgets.MobileTextBox();
            this.textBlock6 = new dotNetLab.Widgets.TextBlock();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tipper
            // 
            this.tipper.Location = new System.Drawing.Point(454, 507);
            // 
            // colorDecorator1
            // 
            this.colorDecorator1.BackColor = System.Drawing.Color.White;
            this.colorDecorator1.DataBindingInfo = null;
            this.colorDecorator1.Location = new System.Drawing.Point(4, 534);
            this.colorDecorator1.Name = "colorDecorator1";
            this.colorDecorator1.Size = new System.Drawing.Size(150, 53);
            this.colorDecorator1.TabIndex = 1;
            this.colorDecorator1.UIElementBinders = null;
            // 
            // textBlock1
            // 
            this.textBlock1.BackColor = System.Drawing.Color.Transparent;
            this.textBlock1.BorderColor = System.Drawing.Color.Empty;
            this.textBlock1.BorderThickness = -1;
            this.textBlock1.DataBindingInfo = null;
            this.textBlock1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock1.LEDStyle = false;
            this.textBlock1.Location = new System.Drawing.Point(9, 116);
            this.textBlock1.Name = "textBlock1";
            this.textBlock1.Radius = -1;
            this.textBlock1.Size = new System.Drawing.Size(96, 25);
            this.textBlock1.TabIndex = 3;
            this.textBlock1.Text = "数据库引擎";
            this.textBlock1.UIElementBinders = null;
            this.textBlock1.Vertical = false;
            this.textBlock1.WhereReturn = ((byte)(0));
            // 
            // btn_AttempConnecting
            // 
            this.btn_AttempConnecting.BackColor = System.Drawing.Color.Transparent;
            this.btn_AttempConnecting.BorderColor = System.Drawing.Color.Empty;
            this.btn_AttempConnecting.BorderThickness = -1;
            this.btn_AttempConnecting.CornerAligment = dotNetLab.Widgets.Alignments.All;
            this.btn_AttempConnecting.DataBindingInfo = null;
            this.btn_AttempConnecting.EnableMobileRound = true;
            this.btn_AttempConnecting.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_AttempConnecting.ForeColor = System.Drawing.Color.White;
            this.btn_AttempConnecting.GapBetweenTextImage = 8;
            this.btn_AttempConnecting.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
            this.btn_AttempConnecting.ImageSize = new System.Drawing.Size(0, 0);
            this.btn_AttempConnecting.LEDStyle = false;
            this.btn_AttempConnecting.Location = new System.Drawing.Point(18, 336);
            this.btn_AttempConnecting.Name = "btn_AttempConnecting";
            this.btn_AttempConnecting.NeedAnimation = true;
            this.btn_AttempConnecting.NormalColor = System.Drawing.Color.DodgerBlue;
            this.btn_AttempConnecting.PressColor = System.Drawing.Color.Cyan;
            this.btn_AttempConnecting.Radius = 37;
            this.btn_AttempConnecting.Size = new System.Drawing.Size(233, 38);
            this.btn_AttempConnecting.Source = null;
            this.btn_AttempConnecting.TabIndex = 5;
            this.btn_AttempConnecting.Text = "尝试连接";
            this.btn_AttempConnecting.UIElementBinders = null;
            this.btn_AttempConnecting.Vertical = false;
            this.btn_AttempConnecting.WhereReturn = ((byte)(0));
            // 
            // btn_Connect
            // 
            this.btn_Connect.BackColor = System.Drawing.Color.Transparent;
            this.btn_Connect.BorderColor = System.Drawing.Color.Empty;
            this.btn_Connect.BorderThickness = -1;
            this.btn_Connect.CornerAligment = dotNetLab.Widgets.Alignments.All;
            this.btn_Connect.DataBindingInfo = null;
            this.btn_Connect.EnableMobileRound = true;
            this.btn_Connect.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Connect.ForeColor = System.Drawing.Color.White;
            this.btn_Connect.GapBetweenTextImage = 8;
            this.btn_Connect.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
            this.btn_Connect.ImageSize = new System.Drawing.Size(0, 0);
            this.btn_Connect.LEDStyle = false;
            this.btn_Connect.Location = new System.Drawing.Point(18, 385);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.NeedAnimation = true;
            this.btn_Connect.NormalColor = System.Drawing.Color.DodgerBlue;
            this.btn_Connect.PressColor = System.Drawing.Color.Cyan;
            this.btn_Connect.Radius = 37;
            this.btn_Connect.Size = new System.Drawing.Size(233, 38);
            this.btn_Connect.Source = null;
            this.btn_Connect.TabIndex = 5;
            this.btn_Connect.Text = "连接";
            this.btn_Connect.UIElementBinders = null;
            this.btn_Connect.Vertical = false;
            this.btn_Connect.WhereReturn = ((byte)(0));
            // 
            // cmbx_DBEngineNames
            // 
            this.cmbx_DBEngineNames.BackColor = System.Drawing.Color.White;
            this.cmbx_DBEngineNames.BorderColor = System.Drawing.Color.Gray;
            this.cmbx_DBEngineNames.DataBindingInfo = null;
            this.cmbx_DBEngineNames.DisplayItems = 0;
            this.cmbx_DBEngineNames.EnableAnimation = false;
            this.cmbx_DBEngineNames.Items = null;
            this.cmbx_DBEngineNames.Location = new System.Drawing.Point(18, 147);
            this.cmbx_DBEngineNames.Name = "cmbx_DBEngineNames";
            this.cmbx_DBEngineNames.SelectedItem = null;
            this.cmbx_DBEngineNames.Size = new System.Drawing.Size(233, 31);
            this.cmbx_DBEngineNames.TabIndex = 8;
            this.cmbx_DBEngineNames.UIElementBinders = null;
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.Transparent;
            this.card1.BorderColor = System.Drawing.Color.Gray;
            this.card1.BorderThickness = -1;
            this.card1.Controls.Add(this.textBlock8);
            this.card1.Controls.Add(this.txb_HugeDBName);
            this.card1.Controls.Add(this.txb_pwd);
            this.card1.Controls.Add(this.txb_UserName);
            this.card1.Controls.Add(this.txb_Host);
            this.card1.Controls.Add(this.txb_Port);
            this.card1.Controls.Add(this.textBlock15);
            this.card1.Controls.Add(this.textBlock5);
            this.card1.Controls.Add(this.textBlock4);
            this.card1.Controls.Add(this.textBlock3);
            this.card1.Controls.Add(this.textBlock2);
            this.card1.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.card1.DataBindingInfo = null;
            this.card1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.card1.HeadColor = System.Drawing.Color.Crimson;
            this.card1.HeaderAlignment = dotNetLab.Widgets.Alignments.Up;
            this.card1.HeadHeight = 50;
            this.card1.ImagePos = new System.Drawing.Point(0, 0);
            this.card1.ImageSize = new System.Drawing.Size(0, 0);
            this.card1.Location = new System.Drawing.Point(334, 86);
            this.card1.Name = "card1";
            this.card1.NormalColor = System.Drawing.Color.Snow;
            this.card1.Radius = 10;
            this.card1.Size = new System.Drawing.Size(375, 310);
            this.card1.Source = null;
            this.card1.TabIndex = 13;
            this.card1.Text = "card1";
            this.card1.UIElementBinders = null;
            // 
            // textBlock8
            // 
            this.textBlock8.BackColor = System.Drawing.Color.Transparent;
            this.textBlock8.BorderColor = System.Drawing.Color.Empty;
            this.textBlock8.BorderThickness = -1;
            this.textBlock8.DataBindingInfo = null;
            this.textBlock8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock8.ForeColor = System.Drawing.Color.White;
            this.textBlock8.LEDStyle = false;
            this.textBlock8.Location = new System.Drawing.Point(139, 14);
            this.textBlock8.Name = "textBlock8";
            this.textBlock8.Radius = -1;
            this.textBlock8.Size = new System.Drawing.Size(114, 25);
            this.textBlock8.TabIndex = 13;
            this.textBlock8.Text = "中大型数据库";
            this.textBlock8.UIElementBinders = null;
            this.textBlock8.Vertical = false;
            this.textBlock8.WhereReturn = ((byte)(0));
            // 
            // txb_HugeDBName
            // 
            this.txb_HugeDBName.ActiveColor = System.Drawing.Color.Orange;
            this.txb_HugeDBName.BackColor = System.Drawing.Color.Transparent;
            this.txb_HugeDBName.DataBindingInfo = null;
            this.txb_HugeDBName.DoubleValue = double.NaN;
            this.txb_HugeDBName.EnableMobileRound = false;
            this.txb_HugeDBName.EnableNullValue = false;
            this.txb_HugeDBName.FillColor = System.Drawing.Color.Transparent;
            this.txb_HugeDBName.FloatValue = float.NaN;
            this.txb_HugeDBName.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_HugeDBName.ForeColor = System.Drawing.Color.Black;
            this.txb_HugeDBName.IntValue = -2147483648;
            this.txb_HugeDBName.LineThickness = 2F;
            this.txb_HugeDBName.Location = new System.Drawing.Point(108, 242);
            this.txb_HugeDBName.Name = "txb_HugeDBName";
            this.txb_HugeDBName.Radius = -1;
            this.txb_HugeDBName.Size = new System.Drawing.Size(224, 31);
            this.txb_HugeDBName.StaticColor = System.Drawing.Color.Crimson;
            this.txb_HugeDBName.TabIndex = 9;
            this.txb_HugeDBName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb_HugeDBName.TextBackColor = System.Drawing.Color.White;
            this.txb_HugeDBName.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_HugeDBName.UIElementBinders = null;
            this.txb_HugeDBName.WhitePattern = false;
            // 
            // txb_pwd
            // 
            this.txb_pwd.ActiveColor = System.Drawing.Color.Orange;
            this.txb_pwd.BackColor = System.Drawing.Color.Transparent;
            this.txb_pwd.DataBindingInfo = null;
            this.txb_pwd.DoubleValue = double.NaN;
            this.txb_pwd.EnableMobileRound = false;
            this.txb_pwd.EnableNullValue = false;
            this.txb_pwd.FillColor = System.Drawing.Color.Transparent;
            this.txb_pwd.FloatValue = float.NaN;
            this.txb_pwd.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_pwd.ForeColor = System.Drawing.Color.Black;
            this.txb_pwd.IntValue = -2147483648;
            this.txb_pwd.LineThickness = 2F;
            this.txb_pwd.Location = new System.Drawing.Point(108, 192);
            this.txb_pwd.Name = "txb_pwd";
            this.txb_pwd.Radius = -1;
            this.txb_pwd.Size = new System.Drawing.Size(224, 31);
            this.txb_pwd.StaticColor = System.Drawing.Color.Crimson;
            this.txb_pwd.TabIndex = 9;
            this.txb_pwd.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb_pwd.TextBackColor = System.Drawing.Color.White;
            this.txb_pwd.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_pwd.UIElementBinders = null;
            this.txb_pwd.WhitePattern = false;
            // 
            // txb_UserName
            // 
            this.txb_UserName.ActiveColor = System.Drawing.Color.Orange;
            this.txb_UserName.BackColor = System.Drawing.Color.Transparent;
            this.txb_UserName.DataBindingInfo = null;
            this.txb_UserName.DoubleValue = double.NaN;
            this.txb_UserName.EnableMobileRound = false;
            this.txb_UserName.EnableNullValue = false;
            this.txb_UserName.FillColor = System.Drawing.Color.Transparent;
            this.txb_UserName.FloatValue = float.NaN;
            this.txb_UserName.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_UserName.ForeColor = System.Drawing.Color.Black;
            this.txb_UserName.IntValue = -2147483648;
            this.txb_UserName.LineThickness = 2F;
            this.txb_UserName.Location = new System.Drawing.Point(108, 152);
            this.txb_UserName.Name = "txb_UserName";
            this.txb_UserName.Radius = -1;
            this.txb_UserName.Size = new System.Drawing.Size(224, 31);
            this.txb_UserName.StaticColor = System.Drawing.Color.Crimson;
            this.txb_UserName.TabIndex = 10;
            this.txb_UserName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb_UserName.TextBackColor = System.Drawing.Color.White;
            this.txb_UserName.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_UserName.UIElementBinders = null;
            this.txb_UserName.WhitePattern = false;
            // 
            // txb_Host
            // 
            this.txb_Host.ActiveColor = System.Drawing.Color.Orange;
            this.txb_Host.BackColor = System.Drawing.Color.Transparent;
            this.txb_Host.DataBindingInfo = null;
            this.txb_Host.DoubleValue = double.NaN;
            this.txb_Host.EnableMobileRound = false;
            this.txb_Host.EnableNullValue = false;
            this.txb_Host.FillColor = System.Drawing.Color.Transparent;
            this.txb_Host.FloatValue = float.NaN;
            this.txb_Host.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_Host.ForeColor = System.Drawing.Color.Black;
            this.txb_Host.IntValue = -2147483648;
            this.txb_Host.LineThickness = 2F;
            this.txb_Host.Location = new System.Drawing.Point(108, 78);
            this.txb_Host.Name = "txb_Host";
            this.txb_Host.Radius = -1;
            this.txb_Host.Size = new System.Drawing.Size(224, 31);
            this.txb_Host.StaticColor = System.Drawing.Color.Crimson;
            this.txb_Host.TabIndex = 11;
            this.txb_Host.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb_Host.TextBackColor = System.Drawing.Color.White;
            this.txb_Host.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_Host.UIElementBinders = null;
            this.txb_Host.WhitePattern = false;
            // 
            // txb_Port
            // 
            this.txb_Port.ActiveColor = System.Drawing.Color.Orange;
            this.txb_Port.BackColor = System.Drawing.Color.Transparent;
            this.txb_Port.DataBindingInfo = null;
            this.txb_Port.DoubleValue = double.NaN;
            this.txb_Port.EnableMobileRound = false;
            this.txb_Port.EnableNullValue = false;
            this.txb_Port.FillColor = System.Drawing.Color.Transparent;
            this.txb_Port.FloatValue = float.NaN;
            this.txb_Port.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_Port.ForeColor = System.Drawing.Color.Black;
            this.txb_Port.IntValue = -2147483648;
            this.txb_Port.LineThickness = 2F;
            this.txb_Port.Location = new System.Drawing.Point(108, 115);
            this.txb_Port.Name = "txb_Port";
            this.txb_Port.Radius = -1;
            this.txb_Port.Size = new System.Drawing.Size(224, 31);
            this.txb_Port.StaticColor = System.Drawing.Color.Crimson;
            this.txb_Port.TabIndex = 12;
            this.txb_Port.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb_Port.TextBackColor = System.Drawing.Color.White;
            this.txb_Port.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_Port.UIElementBinders = null;
            this.txb_Port.WhitePattern = false;
            // 
            // textBlock15
            // 
            this.textBlock15.BackColor = System.Drawing.Color.Transparent;
            this.textBlock15.BorderColor = System.Drawing.Color.Empty;
            this.textBlock15.BorderThickness = -1;
            this.textBlock15.DataBindingInfo = null;
            this.textBlock15.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock15.LEDStyle = false;
            this.textBlock15.Location = new System.Drawing.Point(15, 248);
            this.textBlock15.Name = "textBlock15";
            this.textBlock15.Radius = -1;
            this.textBlock15.Size = new System.Drawing.Size(78, 25);
            this.textBlock15.TabIndex = 6;
            this.textBlock15.Text = "数据库名";
            this.textBlock15.UIElementBinders = null;
            this.textBlock15.Vertical = false;
            this.textBlock15.WhereReturn = ((byte)(0));
            // 
            // textBlock5
            // 
            this.textBlock5.BackColor = System.Drawing.Color.Transparent;
            this.textBlock5.BorderColor = System.Drawing.Color.Empty;
            this.textBlock5.BorderThickness = -1;
            this.textBlock5.DataBindingInfo = null;
            this.textBlock5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock5.LEDStyle = false;
            this.textBlock5.Location = new System.Drawing.Point(32, 198);
            this.textBlock5.Name = "textBlock5";
            this.textBlock5.Radius = -1;
            this.textBlock5.Size = new System.Drawing.Size(42, 25);
            this.textBlock5.TabIndex = 6;
            this.textBlock5.Text = "密码";
            this.textBlock5.UIElementBinders = null;
            this.textBlock5.Vertical = false;
            this.textBlock5.WhereReturn = ((byte)(0));
            // 
            // textBlock4
            // 
            this.textBlock4.BackColor = System.Drawing.Color.Transparent;
            this.textBlock4.BorderColor = System.Drawing.Color.Empty;
            this.textBlock4.BorderThickness = -1;
            this.textBlock4.DataBindingInfo = null;
            this.textBlock4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock4.LEDStyle = false;
            this.textBlock4.Location = new System.Drawing.Point(26, 158);
            this.textBlock4.Name = "textBlock4";
            this.textBlock4.Radius = -1;
            this.textBlock4.Size = new System.Drawing.Size(60, 25);
            this.textBlock4.TabIndex = 5;
            this.textBlock4.Text = "用户名";
            this.textBlock4.UIElementBinders = null;
            this.textBlock4.Vertical = false;
            this.textBlock4.WhereReturn = ((byte)(0));
            // 
            // textBlock3
            // 
            this.textBlock3.BackColor = System.Drawing.Color.Transparent;
            this.textBlock3.BorderColor = System.Drawing.Color.Empty;
            this.textBlock3.BorderThickness = -1;
            this.textBlock3.DataBindingInfo = null;
            this.textBlock3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock3.LEDStyle = false;
            this.textBlock3.Location = new System.Drawing.Point(25, 115);
            this.textBlock3.Name = "textBlock3";
            this.textBlock3.Radius = -1;
            this.textBlock3.Size = new System.Drawing.Size(60, 25);
            this.textBlock3.TabIndex = 7;
            this.textBlock3.Text = "端口号";
            this.textBlock3.UIElementBinders = null;
            this.textBlock3.Vertical = false;
            this.textBlock3.WhereReturn = ((byte)(0));
            // 
            // textBlock2
            // 
            this.textBlock2.BackColor = System.Drawing.Color.Transparent;
            this.textBlock2.BorderColor = System.Drawing.Color.Empty;
            this.textBlock2.BorderThickness = -1;
            this.textBlock2.DataBindingInfo = null;
            this.textBlock2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock2.LEDStyle = false;
            this.textBlock2.Location = new System.Drawing.Point(20, 77);
            this.textBlock2.Name = "textBlock2";
            this.textBlock2.Radius = -1;
            this.textBlock2.Size = new System.Drawing.Size(78, 25);
            this.textBlock2.TabIndex = 8;
            this.textBlock2.Text = "主机地址";
            this.textBlock2.UIElementBinders = null;
            this.textBlock2.Vertical = false;
            this.textBlock2.WhereReturn = ((byte)(0));
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.Transparent;
            this.card2.BorderColor = System.Drawing.Color.Gray;
            this.card2.BorderThickness = -1;
            this.card2.Controls.Add(this.tgl_EmbeddedConfig);
            this.card2.Controls.Add(this.textBlock14);
            this.card2.Controls.Add(this.btn_LoadConnCofig);
            this.card2.Controls.Add(this.textBlock13);
            this.card2.Controls.Add(this.textblock20);
            this.card2.Controls.Add(this.btn_SaveConnCofig);
            this.card2.Controls.Add(this.textBlock10);
            this.card2.Controls.Add(this.textBlock7);
            this.card2.Controls.Add(this.textBlock11);
            this.card2.Controls.Add(this.textBlock9);
            this.card2.Controls.Add(this.cmbx_DBEngineNames);
            this.card2.Controls.Add(this.textBlock1);
            this.card2.Controls.Add(this.btn_Connect);
            this.card2.Controls.Add(this.btn_AttempConnecting);
            this.card2.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.card2.DataBindingInfo = null;
            this.card2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.card2.HeadColor = System.Drawing.Color.DodgerBlue;
            this.card2.HeaderAlignment = dotNetLab.Widgets.Alignments.Up;
            this.card2.HeadHeight = 50;
            this.card2.ImagePos = new System.Drawing.Point(0, 0);
            this.card2.ImageSize = new System.Drawing.Size(0, 0);
            this.card2.Location = new System.Drawing.Point(49, 86);
            this.card2.Name = "card2";
            this.card2.NormalColor = System.Drawing.Color.Snow;
            this.card2.Radius = 10;
            this.card2.Size = new System.Drawing.Size(275, 442);
            this.card2.Source = null;
            this.card2.TabIndex = 14;
            this.card2.Text = "card2";
            this.card2.UIElementBinders = null;
            // 
            // tgl_EmbeddedConfig
            // 
            this.tgl_EmbeddedConfig.BackColor = System.Drawing.Color.Transparent;
            this.tgl_EmbeddedConfig.BlockColor = System.Drawing.Color.DarkGray;
            this.tgl_EmbeddedConfig.BorderColor = System.Drawing.Color.DarkGray;
            this.tgl_EmbeddedConfig.BottomColor = System.Drawing.Color.DodgerBlue;
            this.tgl_EmbeddedConfig.Checked = true;
            this.tgl_EmbeddedConfig.DataBindingInfo = null;
            this.tgl_EmbeddedConfig.Location = new System.Drawing.Point(142, 193);
            this.tgl_EmbeddedConfig.Name = "tgl_EmbeddedConfig";
            this.tgl_EmbeddedConfig.Size = new System.Drawing.Size(45, 22);
            this.tgl_EmbeddedConfig.TabIndex = 17;
            this.tgl_EmbeddedConfig.UIElementBinders = null;
            // 
            // textBlock14
            // 
            this.textBlock14.BackColor = System.Drawing.Color.Transparent;
            this.textBlock14.BorderColor = System.Drawing.Color.Empty;
            this.textBlock14.BorderThickness = -1;
            this.textBlock14.DataBindingInfo = null;
            this.textBlock14.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock14.LEDStyle = false;
            this.textBlock14.Location = new System.Drawing.Point(36, 285);
            this.textBlock14.Name = "textBlock14";
            this.textBlock14.Radius = -1;
            this.textBlock14.Size = new System.Drawing.Size(78, 25);
            this.textBlock14.TabIndex = 16;
            this.textBlock14.Text = "保存配置";
            this.textBlock14.UIElementBinders = null;
            this.textBlock14.Vertical = false;
            this.textBlock14.WhereReturn = ((byte)(0));
            // 
            // btn_LoadConnCofig
            // 
            this.btn_LoadConnCofig.ArrowAlignment = dotNetLab.Widgets.Alignments.Left;
            this.btn_LoadConnCofig.BackColor = System.Drawing.Color.Transparent;
            this.btn_LoadConnCofig.BorderColor = System.Drawing.Color.Gray;
            this.btn_LoadConnCofig.BorderThickness = 2F;
            this.btn_LoadConnCofig.CenterImage = true;
            this.btn_LoadConnCofig.ClipCircleRegion = false;
            this.btn_LoadConnCofig.DataBindingInfo = null;
            this.btn_LoadConnCofig.Effect = null;
            this.btn_LoadConnCofig.Fill = false;
            this.btn_LoadConnCofig.FillColor = System.Drawing.Color.Empty;
            this.btn_LoadConnCofig.ImagePostion = new System.Drawing.Point(0, 0);
            this.btn_LoadConnCofig.ImageSize = new System.Drawing.SizeF(25F, 25F);
            this.btn_LoadConnCofig.Location = new System.Drawing.Point(137, 231);
            this.btn_LoadConnCofig.MouseDownColor = System.Drawing.Color.Gray;
            this.btn_LoadConnCofig.Name = "btn_LoadConnCofig";
            this.btn_LoadConnCofig.NeedEffect = false;
            this.btn_LoadConnCofig.Size = new System.Drawing.Size(45, 45);
            this.btn_LoadConnCofig.Source = ((System.Drawing.Image)(resources.GetObject("btn_LoadConnCofig.Source")));
            this.btn_LoadConnCofig.TabIndex = 15;
            this.btn_LoadConnCofig.UIElementBinders = null;
            this.btn_LoadConnCofig.WhichShap = 6;
            this.btn_LoadConnCofig.WhitePattern = false;
            // 
            // textBlock13
            // 
            this.textBlock13.BackColor = System.Drawing.Color.Transparent;
            this.textBlock13.BorderColor = System.Drawing.Color.Empty;
            this.textBlock13.BorderThickness = -1;
            this.textBlock13.DataBindingInfo = null;
            this.textBlock13.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock13.LEDStyle = false;
            this.textBlock13.Location = new System.Drawing.Point(46, 194);
            this.textBlock13.Name = "textBlock13";
            this.textBlock13.Radius = -1;
            this.textBlock13.Size = new System.Drawing.Size(60, 25);
            this.textBlock13.TabIndex = 16;
            this.textBlock13.Text = "嵌入式";
            this.textBlock13.UIElementBinders = null;
            this.textBlock13.Vertical = false;
            this.textBlock13.WhereReturn = ((byte)(0));
            // 
            // textblock20
            // 
            this.textblock20.BackColor = System.Drawing.Color.Transparent;
            this.textblock20.BorderColor = System.Drawing.Color.Empty;
            this.textblock20.BorderThickness = -1;
            this.textblock20.DataBindingInfo = null;
            this.textblock20.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textblock20.LEDStyle = false;
            this.textblock20.Location = new System.Drawing.Point(38, 237);
            this.textblock20.Name = "textblock20";
            this.textblock20.Radius = -1;
            this.textblock20.Size = new System.Drawing.Size(78, 25);
            this.textblock20.TabIndex = 16;
            this.textblock20.Text = "载入配置";
            this.textblock20.UIElementBinders = null;
            this.textblock20.Vertical = false;
            this.textblock20.WhereReturn = ((byte)(0));
            // 
            // btn_SaveConnCofig
            // 
            this.btn_SaveConnCofig.ArrowAlignment = dotNetLab.Widgets.Alignments.Right;
            this.btn_SaveConnCofig.BackColor = System.Drawing.Color.Transparent;
            this.btn_SaveConnCofig.BorderColor = System.Drawing.Color.Gray;
            this.btn_SaveConnCofig.BorderThickness = 2F;
            this.btn_SaveConnCofig.CenterImage = true;
            this.btn_SaveConnCofig.ClipCircleRegion = false;
            this.btn_SaveConnCofig.DataBindingInfo = null;
            this.btn_SaveConnCofig.Effect = null;
            this.btn_SaveConnCofig.Fill = false;
            this.btn_SaveConnCofig.FillColor = System.Drawing.Color.Empty;
            this.btn_SaveConnCofig.ImagePostion = new System.Drawing.Point(0, 0);
            this.btn_SaveConnCofig.ImageSize = new System.Drawing.SizeF(25F, 25F);
            this.btn_SaveConnCofig.Location = new System.Drawing.Point(137, 279);
            this.btn_SaveConnCofig.MouseDownColor = System.Drawing.Color.Gray;
            this.btn_SaveConnCofig.Name = "btn_SaveConnCofig";
            this.btn_SaveConnCofig.NeedEffect = false;
            this.btn_SaveConnCofig.Size = new System.Drawing.Size(45, 45);
            this.btn_SaveConnCofig.Source = ((System.Drawing.Image)(resources.GetObject("btn_SaveConnCofig.Source")));
            this.btn_SaveConnCofig.TabIndex = 15;
            this.btn_SaveConnCofig.UIElementBinders = null;
            this.btn_SaveConnCofig.WhichShap = 6;
            this.btn_SaveConnCofig.WhitePattern = false;
            // 
            // textBlock10
            // 
            this.textBlock10.BackColor = System.Drawing.Color.Transparent;
            this.textBlock10.BorderColor = System.Drawing.Color.Empty;
            this.textBlock10.BorderThickness = -1;
            this.textBlock10.DataBindingInfo = null;
            this.textBlock10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock10.ForeColor = System.Drawing.Color.Maroon;
            this.textBlock10.LEDStyle = false;
            this.textBlock10.Location = new System.Drawing.Point(2, 86);
            this.textBlock10.Name = "textBlock10";
            this.textBlock10.Radius = -1;
            this.textBlock10.Size = new System.Drawing.Size(96, 25);
            this.textBlock10.TabIndex = 14;
            this.textBlock10.Text = "员权限启动";
            this.textBlock10.UIElementBinders = null;
            this.textBlock10.Vertical = false;
            this.textBlock10.WhereReturn = ((byte)(0));
            // 
            // textBlock7
            // 
            this.textBlock7.BackColor = System.Drawing.Color.Transparent;
            this.textBlock7.BorderColor = System.Drawing.Color.Empty;
            this.textBlock7.BorderThickness = -1;
            this.textBlock7.DataBindingInfo = null;
            this.textBlock7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock7.ForeColor = System.Drawing.Color.Maroon;
            this.textBlock7.LEDStyle = false;
            this.textBlock7.Location = new System.Drawing.Point(-10, 61);
            this.textBlock7.Name = "textBlock7";
            this.textBlock7.Radius = -1;
            this.textBlock7.Size = new System.Drawing.Size(294, 25);
            this.textBlock7.TabIndex = 14;
            this.textBlock7.Text = "如果VS安装在C盘，则需要使用管理";
            this.textBlock7.UIElementBinders = null;
            this.textBlock7.Vertical = false;
            this.textBlock7.WhereReturn = ((byte)(0));
            // 
            // textBlock11
            // 
            this.textBlock11.BackColor = System.Drawing.Color.Transparent;
            this.textBlock11.BorderColor = System.Drawing.Color.Empty;
            this.textBlock11.BorderThickness = -1;
            this.textBlock11.DataBindingInfo = null;
            this.textBlock11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock11.ForeColor = System.Drawing.Color.White;
            this.textBlock11.LEDStyle = false;
            this.textBlock11.Location = new System.Drawing.Point(89, 14);
            this.textBlock11.Name = "textBlock11";
            this.textBlock11.Radius = -1;
            this.textBlock11.Size = new System.Drawing.Size(78, 25);
            this.textBlock11.TabIndex = 13;
            this.textBlock11.Text = "控制中心";
            this.textBlock11.UIElementBinders = null;
            this.textBlock11.Vertical = false;
            this.textBlock11.WhereReturn = ((byte)(0));
            // 
            // textBlock9
            // 
            this.textBlock9.BackColor = System.Drawing.Color.Transparent;
            this.textBlock9.BorderColor = System.Drawing.Color.Empty;
            this.textBlock9.BorderThickness = -1;
            this.textBlock9.DataBindingInfo = null;
            this.textBlock9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock9.LEDStyle = false;
            this.textBlock9.Location = new System.Drawing.Point(84, 14);
            this.textBlock9.Name = "textBlock9";
            this.textBlock9.Radius = -1;
            this.textBlock9.Size = new System.Drawing.Size(0, 0);
            this.textBlock9.TabIndex = 12;
            this.textBlock9.Text = null;
            this.textBlock9.UIElementBinders = null;
            this.textBlock9.Vertical = false;
            this.textBlock9.WhereReturn = ((byte)(0));
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.Transparent;
            this.card3.BorderColor = System.Drawing.Color.Gray;
            this.card3.BorderThickness = -1;
            this.card3.Controls.Add(this.btn_ViewDBFilePath);
            this.card3.Controls.Add(this.txb_EmbeddedDBName);
            this.card3.Controls.Add(this.textBlock6);
            this.card3.Controls.Add(this.textBlock12);
            this.card3.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.card3.DataBindingInfo = null;
            this.card3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.card3.HeadColor = System.Drawing.Color.Green;
            this.card3.HeaderAlignment = dotNetLab.Widgets.Alignments.Right;
            this.card3.HeadHeight = 35;
            this.card3.ImagePos = new System.Drawing.Point(0, 0);
            this.card3.ImageSize = new System.Drawing.Size(0, 0);
            this.card3.Location = new System.Drawing.Point(334, 402);
            this.card3.Name = "card3";
            this.card3.NormalColor = System.Drawing.Color.Snow;
            this.card3.Radius = 10;
            this.card3.Size = new System.Drawing.Size(375, 123);
            this.card3.Source = null;
            this.card3.TabIndex = 15;
            this.card3.Text = "card3";
            this.card3.UIElementBinders = null;
            // 
            // textBlock12
            // 
            this.textBlock12.BackColor = System.Drawing.Color.Transparent;
            this.textBlock12.BorderColor = System.Drawing.Color.Empty;
            this.textBlock12.BorderThickness = -1;
            this.textBlock12.DataBindingInfo = null;
            this.textBlock12.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBlock12.ForeColor = System.Drawing.Color.White;
            this.textBlock12.LEDStyle = false;
            this.textBlock12.Location = new System.Drawing.Point(342, 4);
            this.textBlock12.Name = "textBlock12";
            this.textBlock12.Radius = -1;
            this.textBlock12.Size = new System.Drawing.Size(23, 116);
            this.textBlock12.TabIndex = 18;
            this.textBlock12.Text = "嵌入式数据库";
            this.textBlock12.UIElementBinders = null;
            this.textBlock12.Vertical = true;
            this.textBlock12.WhereReturn = ((byte)(0));
            // 
            // btn_ViewDBFilePath
            // 
            this.btn_ViewDBFilePath.BackColor = System.Drawing.Color.Transparent;
            this.btn_ViewDBFilePath.DataBindingInfo = null;
            this.btn_ViewDBFilePath.EnclosingGap = 2F;
            this.btn_ViewDBFilePath.ExternalColor = System.Drawing.Color.Gray;
            this.btn_ViewDBFilePath.ExternalWidth = 2F;
            this.btn_ViewDBFilePath.InnerColor = System.Drawing.Color.LimeGreen;
            this.btn_ViewDBFilePath.InnerWidth = 3.5F;
            this.btn_ViewDBFilePath.Location = new System.Drawing.Point(277, 44);
            this.btn_ViewDBFilePath.Name = "btn_ViewDBFilePath";
            this.btn_ViewDBFilePath.Size = new System.Drawing.Size(40, 40);
            this.btn_ViewDBFilePath.TabIndex = 15;
            this.btn_ViewDBFilePath.UIElementBinders = null;
            // 
            // txb_EmbeddedDBName
            // 
            this.txb_EmbeddedDBName.ActiveColor = System.Drawing.Color.Orange;
            this.txb_EmbeddedDBName.BackColor = System.Drawing.Color.Transparent;
            this.txb_EmbeddedDBName.DataBindingInfo = null;
            this.txb_EmbeddedDBName.DoubleValue = double.NaN;
            this.txb_EmbeddedDBName.EnableMobileRound = true;
            this.txb_EmbeddedDBName.EnableNullValue = false;
            this.txb_EmbeddedDBName.FillColor = System.Drawing.Color.Transparent;
            this.txb_EmbeddedDBName.FloatValue = float.NaN;
            this.txb_EmbeddedDBName.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_EmbeddedDBName.ForeColor = System.Drawing.Color.Black;
            this.txb_EmbeddedDBName.IntValue = -2147483648;
            this.txb_EmbeddedDBName.LineThickness = 2F;
            this.txb_EmbeddedDBName.Location = new System.Drawing.Point(104, 44);
            this.txb_EmbeddedDBName.Name = "txb_EmbeddedDBName";
            this.txb_EmbeddedDBName.Radius = -1;
            this.txb_EmbeddedDBName.Size = new System.Drawing.Size(167, 31);
            this.txb_EmbeddedDBName.StaticColor = System.Drawing.Color.ForestGreen;
            this.txb_EmbeddedDBName.TabIndex = 14;
            this.txb_EmbeddedDBName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb_EmbeddedDBName.TextBackColor = System.Drawing.SystemColors.Window;
            this.txb_EmbeddedDBName.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_EmbeddedDBName.UIElementBinders = null;
            this.txb_EmbeddedDBName.WhitePattern = false;
            // 
            // textBlock6
            // 
            this.textBlock6.BackColor = System.Drawing.Color.Transparent;
            this.textBlock6.BorderColor = System.Drawing.Color.Empty;
            this.textBlock6.BorderThickness = -1;
            this.textBlock6.DataBindingInfo = null;
            this.textBlock6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock6.LEDStyle = false;
            this.textBlock6.Location = new System.Drawing.Point(20, 49);
            this.textBlock6.Name = "textBlock6";
            this.textBlock6.Radius = -1;
            this.textBlock6.Size = new System.Drawing.Size(78, 25);
            this.textBlock6.TabIndex = 13;
            this.textBlock6.Text = "数据库名";
            this.textBlock6.UIElementBinders = null;
            this.textBlock6.Vertical = false;
            this.textBlock6.WhereReturn = ((byte)(0));
            // 
            // DBEngineConnectPage
            // 
            this.ClientSize = new System.Drawing.Size(757, 592);
            this.Controls.Add(this.card2);
            this.Controls.Add(this.card1);
            this.Controls.Add(this.colorDecorator1);
            this.Controls.Add(this.card3);
            this.Name = "DBEngineConnectPage";
            this.Text = "连接数据库";
            this.TitlePos = new System.Drawing.Point(70, 20);
            this.Controls.SetChildIndex(this.card3, 0);
            this.Controls.SetChildIndex(this.colorDecorator1, 0);
            this.Controls.SetChildIndex(this.card1, 0);
            this.Controls.SetChildIndex(this.card2, 0);
            this.card1.ResumeLayout(false);
            this.card2.ResumeLayout(false);
            this.card3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
