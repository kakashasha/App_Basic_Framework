using System;
using System.Collections.Generic;
using System.Text;
using dotNetLab.Forms;

namespace dotNetLab.Widgets
{
   public  class DBEngineManagePage : ModernPage 
    {
        private Block btn_MicrosoftHugeDB;
        private Block btn_SQLite;
        private Block btn_Firebird;
        private Block btn_MySQL;
        private ColorDecorator colorDecorator1;
        private Block btn_Postgresql;
        private Block btn_SQLCE;
       private Container.BlockContainer blockContainer1;
       public Object DataBindingPropertyEditorDialogObject;
       protected override void prepareCtrls()
       {
           base.prepareCtrls();
           InitializeComponent();
            for (int i = 0; i < this.blockContainer1.Controls.Count; i++)
          {
              blockContainer1.Controls[i].Click += new EventHandler(DBEngineManagePage_Click);
          }
       }
       void DBEngineManagePage_Click(object sender, EventArgs e)
       {
           UIElement ctrl = sender as UIElement;
           String [] str = ctrl.Name.Split('_');
            
           DBEngineConnectPage dcp = new DBEngineConnectPage();
           String strx = str[1].ToLower();
           if (strx.Equals("mysql")
               || strx.Equals("postgresql") || str[1].Contains("Microsoft"))
           {
               dcp.tgl_EmbeddedConfig.Checked = false;
               if (strx.Equals("mysql"))
               {
                   dcp.cmbx_DBEngineNames.SelectedItem = "MySQL";

                }
               if (strx.Equals("postgresql"))
               {
                   dcp.cmbx_DBEngineNames.SelectedItem = "POSTGRESQL";
               }
           }
           else
           {
               dcp.tgl_EmbeddedConfig.Checked = true;
               if (strx.Equals("sqlite"))
               {
                   dcp.cmbx_DBEngineNames.SelectedItem = "SQLITE";

               }
               if (strx.Equals("firebird"))
               {
                   dcp.cmbx_DBEngineNames.SelectedItem = "FireBird";
               }
               if (strx.Equals("sqlce"))
               {
                   dcp.cmbx_DBEngineNames.SelectedItem = "SQLCE";
               }

           }
           dcp.DataBindingPropertyEditorDialogObject = DataBindingPropertyEditorDialogObject;
           this.Hide();
           dcp.ShowDialog();
           dcp.Close();
           dcp.Dispose();
           this.Show();
          // this.WindowState = System.Windows.Forms.FormWindowState.Normal;
       }
       protected override void prepareAppearance()
       {
           base.prepareAppearance();
           this.EnableDrawUpDownPattern = true;
           this.Img_Up = dotNetLab.UI.RibbonSpring;
           Img_Down = dotNetLab.UI.RibbonUnderwater;
           this.EnableDialog = true;
       }
       private void InitializeComponent()
       {
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBEngineManagePage));
           this.blockContainer1 = new dotNetLab.Widgets.Container.BlockContainer();
           this.btn_MicrosoftHugeDB = new dotNetLab.Widgets.Block();
           this.btn_SQLite = new dotNetLab.Widgets.Block();
           this.btn_Firebird = new dotNetLab.Widgets.Block();
           this.btn_MySQL = new dotNetLab.Widgets.Block();
           this.btn_Postgresql = new dotNetLab.Widgets.Block();
           this.btn_SQLCE = new dotNetLab.Widgets.Block();
           this.colorDecorator1 = new dotNetLab.Widgets.ColorDecorator();
           this.blockContainer1.SuspendLayout();
           this.SuspendLayout();
           // 
           // tipper
           // 
           this.tipper.Location = new System.Drawing.Point(388, 415);
           // 
           // blockContainer1
           // 
           this.blockContainer1.BackColor = System.Drawing.Color.Transparent;
           this.blockContainer1.BorderColor = System.Drawing.Color.Empty;
           this.blockContainer1.BorderThickness = -1;
           this.blockContainer1.Controls.Add(this.btn_MicrosoftHugeDB);
           this.blockContainer1.Controls.Add(this.btn_SQLite);
           this.blockContainer1.Controls.Add(this.btn_Firebird);
           this.blockContainer1.Controls.Add(this.btn_MySQL);
           this.blockContainer1.Controls.Add(this.btn_Postgresql);
           this.blockContainer1.Controls.Add(this.btn_SQLCE);
           this.blockContainer1.CornerAlignment = dotNetLab.Widgets.Alignments.All;
           this.blockContainer1.DataBindingInfo = null;
           this.blockContainer1.Font = new System.Drawing.Font("微软雅黑", 11F);
           this.blockContainer1.ImagePos = new System.Drawing.Point(0, 0);
           this.blockContainer1.ImageSize = new System.Drawing.Size(0, 0);
           this.blockContainer1.Location = new System.Drawing.Point(31, 101);
           this.blockContainer1.Name = "blockContainer1";
           this.blockContainer1.NormalColor = System.Drawing.Color.Azure;
           this.blockContainer1.Radius = 20;
           this.blockContainer1.SelectedColor = System.Drawing.Color.DodgerBlue;
           this.blockContainer1.Size = new System.Drawing.Size(637, 332);
           this.blockContainer1.Source = null;
           this.blockContainer1.TabIndex = 2;
           this.blockContainer1.Text = "blockContainer1";
           this.blockContainer1.UIElementBinders = null;
           // 
           // btn_MicrosoftHugeDB
           // 
           this.btn_MicrosoftHugeDB.BackColor = System.Drawing.Color.Transparent;
           this.btn_MicrosoftHugeDB.BorderColor = System.Drawing.Color.Transparent;
           this.btn_MicrosoftHugeDB.BrickColor = System.Drawing.Color.Crimson;
           this.btn_MicrosoftHugeDB.BrickTip = "提示";
           this.btn_MicrosoftHugeDB.CenterImage = false;
           this.btn_MicrosoftHugeDB.DataBindingInfo = null;
           this.btn_MicrosoftHugeDB.EnableSelect = true;
           this.btn_MicrosoftHugeDB.ForeColor = System.Drawing.Color.White;
           this.btn_MicrosoftHugeDB.ImageSize = new System.Drawing.Size(100, 100);
           this.btn_MicrosoftHugeDB.Location = new System.Drawing.Point(242, 27);
           this.btn_MicrosoftHugeDB.Name = "btn_MicrosoftHugeDB";
           this.btn_MicrosoftHugeDB.NeedTip = false;
           this.btn_MicrosoftHugeDB.NeedTitle = true;
           this.btn_MicrosoftHugeDB.Picture_Pos = new System.Drawing.Point(150, 25);
           this.btn_MicrosoftHugeDB.Selected = false;
           this.btn_MicrosoftHugeDB.SelectedColor = System.Drawing.Color.DodgerBlue;
           this.btn_MicrosoftHugeDB.Size = new System.Drawing.Size(380, 126);
           this.btn_MicrosoftHugeDB.Source = ((System.Drawing.Image)(resources.GetObject("btn_MicrosoftHugeDB.Source")));
           this.btn_MicrosoftHugeDB.TabIndex = 0;
           this.btn_MicrosoftHugeDB.Text = "SQL Server / LocalDB";
           this.btn_MicrosoftHugeDB.TitlePos = new System.Drawing.Point(15, 10);
           this.btn_MicrosoftHugeDB.UIElementBinders = null;
           // 
           // btn_SQLite
           // 
           this.btn_SQLite.BackColor = System.Drawing.Color.Transparent;
           this.btn_SQLite.BorderColor = System.Drawing.Color.Transparent;
           this.btn_SQLite.BrickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
           this.btn_SQLite.BrickTip = "提示";
           this.btn_SQLite.CenterImage = true;
           this.btn_SQLite.DataBindingInfo = null;
           this.btn_SQLite.EnableSelect = true;
           this.btn_SQLite.ForeColor = System.Drawing.Color.White;
           this.btn_SQLite.ImageSize = new System.Drawing.Size(100, 100);
           this.btn_SQLite.Location = new System.Drawing.Point(13, 27);
           this.btn_SQLite.Name = "btn_SQLite";
           this.btn_SQLite.NeedTip = false;
           this.btn_SQLite.NeedTitle = true;
           this.btn_SQLite.Picture_Pos = new System.Drawing.Point(8, 8);
           this.btn_SQLite.Selected = false;
           this.btn_SQLite.SelectedColor = System.Drawing.Color.DodgerBlue;
           this.btn_SQLite.Size = new System.Drawing.Size(233, 288);
           this.btn_SQLite.Source = ((System.Drawing.Image)(resources.GetObject("btn_SQLite.Source")));
           this.btn_SQLite.TabIndex = 0;
           this.btn_SQLite.Text = "SQLite";
           this.btn_SQLite.TitlePos = new System.Drawing.Point(30, 10);
           this.btn_SQLite.UIElementBinders = null;
           // 
           // btn_Firebird
           // 
           this.btn_Firebird.BackColor = System.Drawing.Color.Transparent;
           this.btn_Firebird.BorderColor = System.Drawing.Color.Transparent;
           this.btn_Firebird.BrickColor = System.Drawing.Color.Violet;
           this.btn_Firebird.BrickTip = "提示";
           this.btn_Firebird.CenterImage = true;
           this.btn_Firebird.DataBindingInfo = null;
           this.btn_Firebird.EnableSelect = true;
           this.btn_Firebird.ForeColor = System.Drawing.Color.White;
           this.btn_Firebird.ImageSize = new System.Drawing.Size(72, 72);
           this.btn_Firebird.Location = new System.Drawing.Point(241, 147);
           this.btn_Firebird.Name = "btn_Firebird";
           this.btn_Firebird.NeedTip = false;
           this.btn_Firebird.NeedTitle = true;
           this.btn_Firebird.Picture_Pos = new System.Drawing.Point(8, 8);
           this.btn_Firebird.Selected = false;
           this.btn_Firebird.SelectedColor = System.Drawing.Color.DodgerBlue;
           this.btn_Firebird.Size = new System.Drawing.Size(147, 168);
           this.btn_Firebird.Source = ((System.Drawing.Image)(resources.GetObject("btn_Firebird.Source")));
           this.btn_Firebird.TabIndex = 1;
           this.btn_Firebird.Text = "FireBird";
           this.btn_Firebird.TitlePos = new System.Drawing.Point(30, 10);
           this.btn_Firebird.UIElementBinders = null;
           // 
           // btn_MySQL
           // 
           this.btn_MySQL.BackColor = System.Drawing.Color.Transparent;
           this.btn_MySQL.BorderColor = System.Drawing.Color.Transparent;
           this.btn_MySQL.BrickColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
           this.btn_MySQL.BrickTip = "提示";
           this.btn_MySQL.CenterImage = true;
           this.btn_MySQL.DataBindingInfo = null;
           this.btn_MySQL.EnableSelect = true;
           this.btn_MySQL.ForeColor = System.Drawing.Color.White;
           this.btn_MySQL.ImageSize = new System.Drawing.Size(128, 64);
           this.btn_MySQL.Location = new System.Drawing.Point(385, 147);
           this.btn_MySQL.Name = "btn_MySQL";
           this.btn_MySQL.NeedTip = false;
           this.btn_MySQL.NeedTitle = true;
           this.btn_MySQL.Picture_Pos = new System.Drawing.Point(8, 8);
           this.btn_MySQL.Selected = false;
           this.btn_MySQL.SelectedColor = System.Drawing.Color.DodgerBlue;
           this.btn_MySQL.Size = new System.Drawing.Size(238, 87);
           this.btn_MySQL.Source = ((System.Drawing.Image)(resources.GetObject("btn_MySQL.Source")));
           this.btn_MySQL.TabIndex = 2;
           this.btn_MySQL.Text = "MySQL";
           this.btn_MySQL.TitlePos = new System.Drawing.Point(30, 10);
           this.btn_MySQL.UIElementBinders = null;
           // 
           // btn_Postgresql
           // 
           this.btn_Postgresql.BackColor = System.Drawing.Color.Transparent;
           this.btn_Postgresql.BorderColor = System.Drawing.Color.Transparent;
           this.btn_Postgresql.BrickColor = System.Drawing.Color.CornflowerBlue;
           this.btn_Postgresql.BrickTip = "提示";
           this.btn_Postgresql.CenterImage = false;
           this.btn_Postgresql.DataBindingInfo = null;
           this.btn_Postgresql.EnableSelect = true;
           this.btn_Postgresql.ForeColor = System.Drawing.Color.White;
           this.btn_Postgresql.ImageSize = new System.Drawing.Size(50, 50);
           this.btn_Postgresql.Location = new System.Drawing.Point(385, 228);
           this.btn_Postgresql.Name = "btn_Postgresql";
           this.btn_Postgresql.NeedTip = false;
           this.btn_Postgresql.NeedTitle = true;
           this.btn_Postgresql.Picture_Pos = new System.Drawing.Point(60, 30);
           this.btn_Postgresql.Selected = false;
           this.btn_Postgresql.SelectedColor = System.Drawing.Color.DodgerBlue;
           this.btn_Postgresql.Size = new System.Drawing.Size(151, 87);
           this.btn_Postgresql.Source = ((System.Drawing.Image)(resources.GetObject("btn_Postgresql.Source")));
           this.btn_Postgresql.TabIndex = 3;
           this.btn_Postgresql.Text = "Postgresql";
           this.btn_Postgresql.TitlePos = new System.Drawing.Point(10, 5);
           this.btn_Postgresql.UIElementBinders = null;
           // 
           // btn_SQLCE
           // 
           this.btn_SQLCE.BackColor = System.Drawing.Color.Transparent;
           this.btn_SQLCE.BorderColor = System.Drawing.Color.Transparent;
           this.btn_SQLCE.BrickColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
           this.btn_SQLCE.BrickTip = "提示";
           this.btn_SQLCE.CenterImage = true;
           this.btn_SQLCE.DataBindingInfo = null;
           this.btn_SQLCE.EnableSelect = true;
           this.btn_SQLCE.ForeColor = System.Drawing.Color.White;
           this.btn_SQLCE.ImageSize = new System.Drawing.Size(64, 64);
           this.btn_SQLCE.Location = new System.Drawing.Point(532, 228);
           this.btn_SQLCE.Name = "btn_SQLCE";
           this.btn_SQLCE.NeedTip = false;
           this.btn_SQLCE.NeedTitle = true;
           this.btn_SQLCE.Picture_Pos = new System.Drawing.Point(8, 8);
           this.btn_SQLCE.Selected = false;
           this.btn_SQLCE.SelectedColor = System.Drawing.Color.DodgerBlue;
           this.btn_SQLCE.Size = new System.Drawing.Size(91, 87);
           this.btn_SQLCE.Source = null;
           this.btn_SQLCE.TabIndex = 4;
           this.btn_SQLCE.Text = "SQLCE";
           this.btn_SQLCE.TitlePos = new System.Drawing.Point(10, 10);
           this.btn_SQLCE.UIElementBinders = null;
           // 
           // colorDecorator1
           // 
           this.colorDecorator1.BackColor = System.Drawing.Color.White;
           this.colorDecorator1.DataBindingInfo = null;
           this.colorDecorator1.Location = new System.Drawing.Point(5, 439);
           this.colorDecorator1.Name = "colorDecorator1";
           this.colorDecorator1.Size = new System.Drawing.Size(150, 53);
           this.colorDecorator1.TabIndex = 3;
           this.colorDecorator1.UIElementBinders = null;
           // 
           // DBEngineManagePage
           // 
           this.ClientSize = new System.Drawing.Size(691, 500);
           this.Controls.Add(this.colorDecorator1);
           this.Controls.Add(this.blockContainer1);
           this.Name = "DBEngineManagePage";
           this.Text = "数据中心";
           this.TitlePos = new System.Drawing.Point(45, 25);
           this.Controls.SetChildIndex(this.blockContainer1, 0);
           this.Controls.SetChildIndex(this.colorDecorator1, 0);
           this.blockContainer1.ResumeLayout(false);
           this.ResumeLayout(false);

       }
    }
}
