using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Forms;
using dotNetLab.Widgets.UIBinding;
using System.IO;

namespace dotNetLab.Widgets
{
   public class DataBindingPropertyEditorDialog : Session
    {
        public MobileTextBox txb_TableName;
        public MobileTextBox txb_DBField;
        public MobileTextBox txb_Filter;
        private TextBlock textBlock1;
        private TextBlock textBlock2;
        private TextBlock textBlock3;
        public Toggle tgl_UpdateToDB;
        private MobileButton btn_BindData;
        public MobileTextBox txb_DBEngineIndex;
        private TextBlock textBlock6;
        private ColorDecorator colorDecorator1;
        private TextBlock textBlock4;
        public Toggle tgl_RealTimeWithDB;
        private MobileButton btn_LookupTable;
        private System.ComponentModel.IContainer components;
        private TextBlock textBlock5;
         
       protected override void prepareCtrls()
       {
           base.prepareCtrls();
           InitializeComponent() ;
           CheckDataCenterDllExists();    
            
       }

       private void CheckDataCenterDllExists()
       {
           String strDevenvPath = Application.ExecutablePath;
           string DataCenterDllPath = Path.GetDirectoryName(strDevenvPath) + "\\shikii.dotNetLab.DataCenter.dll" ;
           if(!File.Exists(DataCenterDllPath))
           {
              //To Do 

           }
           
       }
       
       protected override void prepareAppearance()
       {
           base.prepareAppearance();
           this.EnableDrawUpDownPattern = true;
           this.Img_Up = dotNetLab.UI.RibbonSpring;
           this.Img_Down = dotNetLab.UI.RibbonUnderwater;
       }
       protected override void prepareEvents()
       {
           base.prepareEvents();
           this.btn_LookupTable.Click += new EventHandler(btn_LookupTable_Click);
           this.btn_BindData.Click += new EventHandler(btn_BindData_Click);
          
           this.FormClosing += new FormClosingEventHandler(DataBindingPropertyEditorDialog_FormClosing);
       }

        

       void btn_LookupTable_Click(object sender, EventArgs e)
       {
           DBEngineManagePage DBEngineManager = new DBEngineManagePage();
           this.Hide();
           this.WindowState = FormWindowState.Normal;
           DBEngineManager.DataBindingPropertyEditorDialogObject = this;
           DBEngineManager.ShowDialog();
           DBEngineManager.Close();
           
           DBEngineManager.Dispose();
           Show();
         
       }

      

       void DataBindingPropertyEditorDialog_FormClosing(object sender, FormClosingEventArgs e)
       {
           if (txb_Filter.Text.Contains("{0}"))
           {
               dotNetLab.Tipper.Tip_Info_Error("接入条件错误");
               e.Cancel = true;
           }
       }

       void btn_BindData_Click(object sender, EventArgs e)
       {
           this.Close();
       }
       private void InitializeComponent()
       {
           this.txb_TableName = new dotNetLab.Widgets.MobileTextBox();
           this.txb_DBField = new dotNetLab.Widgets.MobileTextBox();
           this.txb_Filter = new dotNetLab.Widgets.MobileTextBox();
           this.textBlock1 = new dotNetLab.Widgets.TextBlock();
           this.textBlock2 = new dotNetLab.Widgets.TextBlock();
           this.textBlock3 = new dotNetLab.Widgets.TextBlock();
           this.tgl_UpdateToDB = new dotNetLab.Widgets.Toggle();
           this.textBlock5 = new dotNetLab.Widgets.TextBlock();
           this.btn_BindData = new dotNetLab.Widgets.MobileButton();
           this.txb_DBEngineIndex = new dotNetLab.Widgets.MobileTextBox();
           this.textBlock6 = new dotNetLab.Widgets.TextBlock();
           this.colorDecorator1 = new dotNetLab.Widgets.ColorDecorator();
           this.textBlock4 = new dotNetLab.Widgets.TextBlock();
           this.tgl_RealTimeWithDB = new dotNetLab.Widgets.Toggle();
           this.btn_LookupTable = new dotNetLab.Widgets.MobileButton();
           this.SuspendLayout();
           // 
           // tipper
           // 
           this.tipper.Location = new System.Drawing.Point(385, 426);
           // 
           // txb_TableName
           // 
           this.txb_TableName.ActiveColor = System.Drawing.Color.Orange;
           this.txb_TableName.BackColor = System.Drawing.Color.Transparent;
           this.txb_TableName.DataBindingInfo = null;
           this.txb_TableName.DoubleValue = double.NaN;
           this.txb_TableName.EnableMobileRound = true;
           this.txb_TableName.EnableNullValue = false;
           this.txb_TableName.FillColor = System.Drawing.Color.Transparent;
           this.txb_TableName.FloatValue = float.NaN;
           this.txb_TableName.Font = new System.Drawing.Font("微软雅黑", 13F);
           this.txb_TableName.ForeColor = System.Drawing.Color.DimGray;
           this.txb_TableName.IntValue = -2147483648;
           this.txb_TableName.LineThickness = 2F;
           this.txb_TableName.Location = new System.Drawing.Point(277, 120);
           this.txb_TableName.Name = "txb_TableName";
           this.txb_TableName.Radius = -1;
           this.txb_TableName.Size = new System.Drawing.Size(240, 31);
           this.txb_TableName.StaticColor = System.Drawing.Color.DodgerBlue;
           this.txb_TableName.TabIndex = 1;
           this.txb_TableName.Text = "App_Extension_Data_Table";
           this.txb_TableName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
           this.txb_TableName.TextBackColor = System.Drawing.SystemColors.Window;
           this.txb_TableName.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
           this.txb_TableName.UIElementBinders = null;
           this.txb_TableName.WhitePattern = false;
           // 
           // txb_DBField
           // 
           this.txb_DBField.ActiveColor = System.Drawing.Color.Orange;
           this.txb_DBField.BackColor = System.Drawing.Color.Transparent;
           this.txb_DBField.DataBindingInfo = null;
           this.txb_DBField.DoubleValue = double.NaN;
           this.txb_DBField.EnableMobileRound = true;
           this.txb_DBField.EnableNullValue = false;
           this.txb_DBField.FillColor = System.Drawing.Color.Transparent;
           this.txb_DBField.FloatValue = float.NaN;
           this.txb_DBField.Font = new System.Drawing.Font("微软雅黑", 13F);
           this.txb_DBField.ForeColor = System.Drawing.Color.DimGray;
           this.txb_DBField.IntValue = -2147483648;
           this.txb_DBField.LineThickness = 2F;
           this.txb_DBField.Location = new System.Drawing.Point(277, 175);
           this.txb_DBField.Name = "txb_DBField";
           this.txb_DBField.Radius = -1;
           this.txb_DBField.Size = new System.Drawing.Size(240, 31);
           this.txb_DBField.StaticColor = System.Drawing.Color.DodgerBlue;
           this.txb_DBField.TabIndex = 1;
           this.txb_DBField.Text = "Val";
           this.txb_DBField.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
           this.txb_DBField.TextBackColor = System.Drawing.SystemColors.Window;
           this.txb_DBField.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
           this.txb_DBField.UIElementBinders = null;
           this.txb_DBField.WhitePattern = false;
           // 
           // txb_Filter
           // 
           this.txb_Filter.ActiveColor = System.Drawing.Color.Orange;
           this.txb_Filter.BackColor = System.Drawing.Color.Transparent;
           this.txb_Filter.DataBindingInfo = null;
           this.txb_Filter.DoubleValue = double.NaN;
           this.txb_Filter.EnableMobileRound = true;
           this.txb_Filter.EnableNullValue = false;
           this.txb_Filter.FillColor = System.Drawing.Color.Transparent;
           this.txb_Filter.FloatValue = float.NaN;
           this.txb_Filter.Font = new System.Drawing.Font("微软雅黑", 13F);
           this.txb_Filter.ForeColor = System.Drawing.Color.DimGray;
           this.txb_Filter.IntValue = -2147483648;
           this.txb_Filter.LineThickness = 2F;
           this.txb_Filter.Location = new System.Drawing.Point(277, 226);
           this.txb_Filter.Name = "txb_Filter";
           this.txb_Filter.Radius = -1;
           this.txb_Filter.Size = new System.Drawing.Size(240, 31);
           this.txb_Filter.StaticColor = System.Drawing.Color.DodgerBlue;
           this.txb_Filter.TabIndex = 1;
           this.txb_Filter.Text = "Name=\'{0}\'";
           this.txb_Filter.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
           this.txb_Filter.TextBackColor = System.Drawing.SystemColors.Window;
           this.txb_Filter.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
           this.txb_Filter.UIElementBinders = null;
           this.txb_Filter.WhitePattern = false;
           // 
           // textBlock1
           // 
           this.textBlock1.BackColor = System.Drawing.Color.Transparent;
           this.textBlock1.BorderColor = System.Drawing.Color.Empty;
           this.textBlock1.BorderThickness = -1;
           this.textBlock1.DataBindingInfo = null;
           this.textBlock1.Font = new System.Drawing.Font("微软雅黑", 12F);
           this.textBlock1.LEDStyle = false;
           this.textBlock1.Location = new System.Drawing.Point(155, 126);
           this.textBlock1.Name = "textBlock1";
           this.textBlock1.Radius = -1;
           this.textBlock1.Size = new System.Drawing.Size(42, 25);
           this.textBlock1.TabIndex = 2;
           this.textBlock1.Text = "表名";
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
           this.textBlock2.Location = new System.Drawing.Point(147, 175);
           this.textBlock2.Name = "textBlock2";
           this.textBlock2.Radius = -1;
           this.textBlock2.Size = new System.Drawing.Size(60, 25);
           this.textBlock2.TabIndex = 2;
           this.textBlock2.Text = "字段名";
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
           this.textBlock3.Location = new System.Drawing.Point(147, 226);
           this.textBlock3.Name = "textBlock3";
           this.textBlock3.Radius = -1;
           this.textBlock3.Size = new System.Drawing.Size(78, 25);
           this.textBlock3.TabIndex = 2;
           this.textBlock3.Text = "接入条件";
           this.textBlock3.UIElementBinders = null;
           this.textBlock3.Vertical = false;
           this.textBlock3.WhereReturn = ((byte)(0));
           // 
           // tgl_UpdateToDB
           // 
           this.tgl_UpdateToDB.BackColor = System.Drawing.Color.Transparent;
           this.tgl_UpdateToDB.BlockColor = System.Drawing.Color.DarkGray;
           this.tgl_UpdateToDB.BorderColor = System.Drawing.Color.DarkGray;
           this.tgl_UpdateToDB.BottomColor = System.Drawing.Color.DodgerBlue;
           this.tgl_UpdateToDB.Checked = true;
           this.tgl_UpdateToDB.DataBindingInfo = null;
           this.tgl_UpdateToDB.Location = new System.Drawing.Point(277, 285);
           this.tgl_UpdateToDB.Name = "tgl_UpdateToDB";
           this.tgl_UpdateToDB.Size = new System.Drawing.Size(45, 22);
           this.tgl_UpdateToDB.TabIndex = 3;
           this.tgl_UpdateToDB.UIElementBinders = null;
           // 
           // textBlock5
           // 
           this.textBlock5.BackColor = System.Drawing.Color.Transparent;
           this.textBlock5.BorderColor = System.Drawing.Color.Empty;
           this.textBlock5.BorderThickness = -1;
           this.textBlock5.DataBindingInfo = null;
           this.textBlock5.Font = new System.Drawing.Font("微软雅黑", 12F);
           this.textBlock5.LEDStyle = false;
           this.textBlock5.Location = new System.Drawing.Point(130, 285);
           this.textBlock5.Name = "textBlock5";
           this.textBlock5.Radius = -1;
           this.textBlock5.Size = new System.Drawing.Size(114, 25);
           this.textBlock5.TabIndex = 2;
           this.textBlock5.Text = "更新到数据库";
           this.textBlock5.UIElementBinders = null;
           this.textBlock5.Vertical = false;
           this.textBlock5.WhereReturn = ((byte)(0));
           // 
           // btn_BindData
           // 
           this.btn_BindData.BackColor = System.Drawing.Color.Transparent;
           this.btn_BindData.BorderColor = System.Drawing.Color.Empty;
           this.btn_BindData.BorderThickness = -1;
           this.btn_BindData.CornerAligment = dotNetLab.Widgets.Alignments.All;
           this.btn_BindData.DataBindingInfo = null;
           this.btn_BindData.EnableMobileRound = false;
           this.btn_BindData.Font = new System.Drawing.Font("微软雅黑", 12F);
           this.btn_BindData.ForeColor = System.Drawing.Color.White;
           this.btn_BindData.GapBetweenTextImage = 8;
           this.btn_BindData.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
           this.btn_BindData.ImageSize = new System.Drawing.Size(0, 0);
           this.btn_BindData.LEDStyle = false;
           this.btn_BindData.Location = new System.Drawing.Point(473, 398);
           this.btn_BindData.Name = "btn_BindData";
           this.btn_BindData.NeedAnimation = true;
           this.btn_BindData.NormalColor = System.Drawing.Color.DodgerBlue;
           this.btn_BindData.PressColor = System.Drawing.Color.Cyan;
           this.btn_BindData.Radius = 10;
           this.btn_BindData.Size = new System.Drawing.Size(132, 47);
           this.btn_BindData.Source = null;
           this.btn_BindData.TabIndex = 4;
           this.btn_BindData.Text = "接入";
           this.btn_BindData.UIElementBinders = null;
           this.btn_BindData.Vertical = false;
           this.btn_BindData.WhereReturn = ((byte)(0));
           // 
           // txb_DBEngineIndex
           // 
           this.txb_DBEngineIndex.ActiveColor = System.Drawing.Color.Orange;
           this.txb_DBEngineIndex.BackColor = System.Drawing.Color.Transparent;
           this.txb_DBEngineIndex.DataBindingInfo = null;
           this.txb_DBEngineIndex.DoubleValue = 0D;
           this.txb_DBEngineIndex.EnableMobileRound = true;
           this.txb_DBEngineIndex.EnableNullValue = false;
           this.txb_DBEngineIndex.FillColor = System.Drawing.Color.Transparent;
           this.txb_DBEngineIndex.FloatValue = 0F;
           this.txb_DBEngineIndex.Font = new System.Drawing.Font("微软雅黑", 13F);
           this.txb_DBEngineIndex.ForeColor = System.Drawing.Color.DimGray;
           this.txb_DBEngineIndex.IntValue = 0;
           this.txb_DBEngineIndex.LineThickness = 2F;
           this.txb_DBEngineIndex.Location = new System.Drawing.Point(277, 332);
           this.txb_DBEngineIndex.Name = "txb_DBEngineIndex";
           this.txb_DBEngineIndex.Radius = -1;
           this.txb_DBEngineIndex.Size = new System.Drawing.Size(240, 31);
           this.txb_DBEngineIndex.StaticColor = System.Drawing.Color.DodgerBlue;
           this.txb_DBEngineIndex.TabIndex = 1;
           this.txb_DBEngineIndex.Text = "0";
           this.txb_DBEngineIndex.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
           this.txb_DBEngineIndex.TextBackColor = System.Drawing.SystemColors.Window;
           this.txb_DBEngineIndex.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
           this.txb_DBEngineIndex.UIElementBinders = null;
           this.txb_DBEngineIndex.WhitePattern = false;
           // 
           // textBlock6
           // 
           this.textBlock6.BackColor = System.Drawing.Color.Transparent;
           this.textBlock6.BorderColor = System.Drawing.Color.Empty;
           this.textBlock6.BorderThickness = -1;
           this.textBlock6.DataBindingInfo = null;
           this.textBlock6.Font = new System.Drawing.Font("微软雅黑", 12F);
           this.textBlock6.LEDStyle = false;
           this.textBlock6.Location = new System.Drawing.Point(130, 338);
           this.textBlock6.Name = "textBlock6";
           this.textBlock6.Radius = -1;
           this.textBlock6.Size = new System.Drawing.Size(132, 25);
           this.textBlock6.TabIndex = 2;
           this.textBlock6.Text = "数据库引擎索引";
           this.textBlock6.UIElementBinders = null;
           this.textBlock6.Vertical = false;
           this.textBlock6.WhereReturn = ((byte)(0));
           // 
           // colorDecorator1
           // 
           this.colorDecorator1.BackColor = System.Drawing.Color.White;
           this.colorDecorator1.DataBindingInfo = null;
           this.colorDecorator1.Location = new System.Drawing.Point(5, 453);
           this.colorDecorator1.Name = "colorDecorator1";
           this.colorDecorator1.Size = new System.Drawing.Size(150, 53);
           this.colorDecorator1.TabIndex = 6;
           this.colorDecorator1.UIElementBinders = null;
           // 
           // textBlock4
           // 
           this.textBlock4.BackColor = System.Drawing.Color.Transparent;
           this.textBlock4.BorderColor = System.Drawing.Color.Empty;
           this.textBlock4.BorderThickness = -1;
           this.textBlock4.DataBindingInfo = null;
           this.textBlock4.Font = new System.Drawing.Font("微软雅黑", 12F);
           this.textBlock4.LEDStyle = false;
           this.textBlock4.Location = new System.Drawing.Point(353, 285);
           this.textBlock4.Name = "textBlock4";
           this.textBlock4.Radius = -1;
           this.textBlock4.Size = new System.Drawing.Size(150, 25);
           this.textBlock4.TabIndex = 2;
           this.textBlock4.Text = "实时更新到数据库";
           this.textBlock4.UIElementBinders = null;
           this.textBlock4.Vertical = false;
           this.textBlock4.WhereReturn = ((byte)(0));
           // 
           // tgl_RealTimeWithDB
           // 
           this.tgl_RealTimeWithDB.BackColor = System.Drawing.Color.Transparent;
           this.tgl_RealTimeWithDB.BlockColor = System.Drawing.Color.DarkGray;
           this.tgl_RealTimeWithDB.BorderColor = System.Drawing.Color.DarkGray;
           this.tgl_RealTimeWithDB.BottomColor = System.Drawing.Color.DodgerBlue;
           this.tgl_RealTimeWithDB.Checked = true;
           this.tgl_RealTimeWithDB.DataBindingInfo = null;
           this.tgl_RealTimeWithDB.Location = new System.Drawing.Point(533, 285);
           this.tgl_RealTimeWithDB.Name = "tgl_RealTimeWithDB";
           this.tgl_RealTimeWithDB.Size = new System.Drawing.Size(45, 22);
           this.tgl_RealTimeWithDB.TabIndex = 3;
           this.tgl_RealTimeWithDB.UIElementBinders = null;
           // 
           // btn_LookupTable
           // 
           this.btn_LookupTable.BackColor = System.Drawing.Color.Transparent;
           this.btn_LookupTable.BorderColor = System.Drawing.Color.Empty;
           this.btn_LookupTable.BorderThickness = -1;
           this.btn_LookupTable.CornerAligment = dotNetLab.Widgets.Alignments.All;
           this.btn_LookupTable.DataBindingInfo = null;
           this.btn_LookupTable.EnableMobileRound = false;
           this.btn_LookupTable.Font = new System.Drawing.Font("微软雅黑", 12F);
           this.btn_LookupTable.ForeColor = System.Drawing.Color.White;
           this.btn_LookupTable.GapBetweenTextImage = 8;
           this.btn_LookupTable.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
           this.btn_LookupTable.ImageSize = new System.Drawing.Size(0, 0);
           this.btn_LookupTable.LEDStyle = false;
           this.btn_LookupTable.Location = new System.Drawing.Point(334, 398);
           this.btn_LookupTable.Name = "btn_LookupTable";
           this.btn_LookupTable.NeedAnimation = true;
           this.btn_LookupTable.NormalColor = System.Drawing.Color.DodgerBlue;
           this.btn_LookupTable.PressColor = System.Drawing.Color.Cyan;
           this.btn_LookupTable.Radius = 10;
           this.btn_LookupTable.Size = new System.Drawing.Size(133, 47);
           this.btn_LookupTable.Source = null;
           this.btn_LookupTable.TabIndex = 4;
           this.btn_LookupTable.Text = "查询";
           this.btn_LookupTable.UIElementBinders = null;
           this.btn_LookupTable.Vertical = false;
           this.btn_LookupTable.WhereReturn = ((byte)(0));
           // 
           // DataBindingPropertyEditorDialog
           // 
           this.ClientSize = new System.Drawing.Size(688, 511);
           this.Controls.Add(this.colorDecorator1);
           this.Controls.Add(this.btn_LookupTable);
           this.Controls.Add(this.btn_BindData);
           this.Controls.Add(this.tgl_RealTimeWithDB);
           this.Controls.Add(this.tgl_UpdateToDB);
           this.Controls.Add(this.textBlock4);
           this.Controls.Add(this.textBlock5);
           this.Controls.Add(this.textBlock6);
           this.Controls.Add(this.textBlock3);
           this.Controls.Add(this.textBlock2);
           this.Controls.Add(this.txb_DBEngineIndex);
           this.Controls.Add(this.textBlock1);
           this.Controls.Add(this.txb_Filter);
           this.Controls.Add(this.txb_DBField);
           this.Controls.Add(this.txb_TableName);
           this.Name = "DataBindingPropertyEditorDialog";
           this.Text = "接入数据中心";
           this.TitlePos = new System.Drawing.Point(70, 20);
           this.Controls.SetChildIndex(this.txb_TableName, 0);
           this.Controls.SetChildIndex(this.txb_DBField, 0);
           this.Controls.SetChildIndex(this.txb_Filter, 0);
           this.Controls.SetChildIndex(this.textBlock1, 0);
           this.Controls.SetChildIndex(this.txb_DBEngineIndex, 0);
           this.Controls.SetChildIndex(this.textBlock2, 0);
           this.Controls.SetChildIndex(this.textBlock3, 0);
           this.Controls.SetChildIndex(this.textBlock6, 0);
           this.Controls.SetChildIndex(this.textBlock5, 0);
           this.Controls.SetChildIndex(this.textBlock4, 0);
           this.Controls.SetChildIndex(this.tgl_UpdateToDB, 0);
           this.Controls.SetChildIndex(this.tgl_RealTimeWithDB, 0);
           this.Controls.SetChildIndex(this.btn_BindData, 0);
           this.Controls.SetChildIndex(this.btn_LookupTable, 0);
           this.Controls.SetChildIndex(this.colorDecorator1, 0);
           this.ResumeLayout(false);

       }
       public Object Value
       {
           get {
               UIElementBinderInfo dif = new UIElementBinderInfo();
               dif.DBEngineIndex = txb_DBEngineIndex.IntValue;
               dif.StoreInDB = tgl_UpdateToDB.Checked;
               dif.FieldName = txb_DBField.Text;
               dif.Filter = txb_Filter.Text;
               dif.TableName = txb_TableName.Text;
               return dif;
           }
       }
        
   }
}
