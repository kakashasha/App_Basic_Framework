using System;
using System.Collections.Generic;
using System.Text;
using dotNetLab.Widgets.Container;
using System.Data.Common;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
 

namespace dotNetLab.Widgets.Parts 
{
    public partial class TableProvidePart : CanvasPanel
    {
        private Widgets.FramePanel FramePanel_Content;
        private CanvasPanel canvasPanel2;
        private CanvasPanel canvasPanel1;
        private Widgets.Card pnl_KeyValueTableNames;
        private Widgets.MobileListBox ltb_KeyValueTableNames;
        private Widgets.TextBlock textBlock3;
        private Widgets.Card pnl_NormalTableNames;
        private Widgets.MobileListBox ltb_NormalTableNames;
        private CanvasPanel canvasPanel3;
        private Widgets.TextBlock textBlock2;

       
        private FramePanel FramePannel_Header;
        private CanvasPanel canvasPanel4;
        private TextBlock textBlock5;
        private TextBlock textBlock4;
        private MobileTextBox txb_PatternSearch;
        private TextBlock textBlock1;
        private Direction btn_ShowColumnNames;
        private CanvasPanel canvasPanel5;
        private TextBlock textBlock6;
        private Direction btn_GoBackFromTablePreviewToMainPart;
        private System.Windows.Forms.DataGridView dgv_PreviewTable;
        private CanvasPanel canvasPanel6;
        private TextBlock textBlock7;
        private Direction btn_ShowColumnName_1;
        private TextBlock textBlock8;
        private Direction btn_Goback_2;
        private Card card1;
        private TextBlock textBlock9;
        private MobileListBox ltb_ColumnNames;
        private Card card2;
        private TextBlock textBlock10;
        private MobileButton btn_PutContentIntoClipboard;
        private TextBlock textBlock11;
        private MobileTextBox txb_SimpleColumnNames;
        private MobileButton btn_GainColumnNames;
        private System.Windows.Forms.LinkLabel lnk_PreviewTableContent;
        private MobileTextBox txb_ViewColumnNames;
        private MobileTextBox txb_KeyValueViewName;
        private TextBlock textBlock12;
        private MobileButton btn_CreateKeyValueView;
        private TextBlock textBlock13;
        private TextBlock textBlock14;
        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;
        private LinkLabel lnk_ShowTableContent_Full;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableProvidePart));
            this.FramePanel_Content = new dotNetLab.Widgets.FramePanel();
            this.canvasPanel1 = new dotNetLab.Widgets.Container.CanvasPanel();
            this.pnl_KeyValueTableNames = new dotNetLab.Widgets.Card();
            this.ltb_KeyValueTableNames = new dotNetLab.Widgets.MobileListBox();
            this.textBlock3 = new dotNetLab.Widgets.TextBlock();
            this.pnl_NormalTableNames = new dotNetLab.Widgets.Card();
            this.ltb_NormalTableNames = new dotNetLab.Widgets.MobileListBox();
            this.textBlock2 = new dotNetLab.Widgets.TextBlock();
            this.canvasPanel2 = new dotNetLab.Widgets.Container.CanvasPanel();
            this.dgv_PreviewTable = new System.Windows.Forms.DataGridView();
            this.canvasPanel3 = new dotNetLab.Widgets.Container.CanvasPanel();
            this.card2 = new dotNetLab.Widgets.Card();
            this.textBlock13 = new dotNetLab.Widgets.TextBlock();
            this.textBlock12 = new dotNetLab.Widgets.TextBlock();
            this.txb_ViewColumnNames = new dotNetLab.Widgets.MobileTextBox();
            this.txb_KeyValueViewName = new dotNetLab.Widgets.MobileTextBox();
            this.txb_SimpleColumnNames = new dotNetLab.Widgets.MobileTextBox();
            this.btn_CreateKeyValueView = new dotNetLab.Widgets.MobileButton();
            this.btn_GainColumnNames = new dotNetLab.Widgets.MobileButton();
            this.btn_PutContentIntoClipboard = new dotNetLab.Widgets.MobileButton();
            this.textBlock11 = new dotNetLab.Widgets.TextBlock();
            this.textBlock10 = new dotNetLab.Widgets.TextBlock();
            this.card1 = new dotNetLab.Widgets.Card();
            this.ltb_ColumnNames = new dotNetLab.Widgets.MobileListBox();
            this.textBlock9 = new dotNetLab.Widgets.TextBlock();
            this.FramePannel_Header = new dotNetLab.Widgets.FramePanel();
            this.canvasPanel4 = new dotNetLab.Widgets.Container.CanvasPanel();
            this.textBlock5 = new dotNetLab.Widgets.TextBlock();
            this.textBlock4 = new dotNetLab.Widgets.TextBlock();
            this.txb_PatternSearch = new dotNetLab.Widgets.MobileTextBox();
            this.textBlock1 = new dotNetLab.Widgets.TextBlock();
            this.btn_ShowColumnNames = new dotNetLab.Widgets.Direction();
            this.canvasPanel6 = new dotNetLab.Widgets.Container.CanvasPanel();
            this.textBlock14 = new dotNetLab.Widgets.TextBlock();
            this.lnk_PreviewTableContent = new System.Windows.Forms.LinkLabel();
            this.textBlock8 = new dotNetLab.Widgets.TextBlock();
            this.btn_Goback_2 = new dotNetLab.Widgets.Direction();
            this.canvasPanel5 = new dotNetLab.Widgets.Container.CanvasPanel();
            this.lnk_ShowTableContent_Full = new System.Windows.Forms.LinkLabel();
            this.textBlock7 = new dotNetLab.Widgets.TextBlock();
            this.btn_ShowColumnName_1 = new dotNetLab.Widgets.Direction();
            this.textBlock6 = new dotNetLab.Widgets.TextBlock();
            this.btn_GoBackFromTablePreviewToMainPart = new dotNetLab.Widgets.Direction();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FramePanel_Content.SuspendLayout();
            this.canvasPanel1.SuspendLayout();
            this.pnl_KeyValueTableNames.SuspendLayout();
            this.pnl_NormalTableNames.SuspendLayout();
            this.canvasPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PreviewTable)).BeginInit();
            this.canvasPanel3.SuspendLayout();
            this.card2.SuspendLayout();
            this.card1.SuspendLayout();
            this.FramePannel_Header.SuspendLayout();
            this.canvasPanel4.SuspendLayout();
            this.canvasPanel6.SuspendLayout();
            this.canvasPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // FramePanel_Content
            // 
            this.FramePanel_Content.BackColor = System.Drawing.Color.Transparent;
            this.FramePanel_Content.BorderColor = System.Drawing.Color.Empty;
            this.FramePanel_Content.BorderThickness = -1;
            this.FramePanel_Content.Controls.Add(this.canvasPanel2);
            this.FramePanel_Content.Controls.Add(this.canvasPanel1);
            this.FramePanel_Content.Controls.Add(this.canvasPanel3);
            this.FramePanel_Content.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.FramePanel_Content.DataBindingInfo = null;
            this.FramePanel_Content.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.FramePanel_Content.ImagePos = new System.Drawing.Point(0, 0);
            this.FramePanel_Content.ImageSize = new System.Drawing.Size(0, 0);
            this.FramePanel_Content.Location = new System.Drawing.Point(15, 68);
            this.FramePanel_Content.Name = "FramePanel_Content";
            this.FramePanel_Content.NormalColor = System.Drawing.Color.Transparent;
            this.FramePanel_Content.Radius = -1;
            this.FramePanel_Content.Size = new System.Drawing.Size(570, 380);
            this.FramePanel_Content.Source = null;
            this.FramePanel_Content.TabIndex = 10;
            this.FramePanel_Content.Text = "framePanel1";
            this.FramePanel_Content.UIElementBinders = null;
            this.FramePanel_Content.WhichNeedToEdit = 1;
            // 
            // canvasPanel1
            // 
            this.canvasPanel1.BackColor = System.Drawing.Color.Transparent;
            this.canvasPanel1.BorderColor = System.Drawing.Color.Empty;
            this.canvasPanel1.BorderThickness = -1;
            this.canvasPanel1.Controls.Add(this.pnl_KeyValueTableNames);
            this.canvasPanel1.Controls.Add(this.pnl_NormalTableNames);
            this.canvasPanel1.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.canvasPanel1.DataBindingInfo = null;
            this.canvasPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.canvasPanel1.ImagePos = new System.Drawing.Point(0, 0);
            this.canvasPanel1.ImageSize = new System.Drawing.Size(0, 0);
            this.canvasPanel1.Location = new System.Drawing.Point(0, 0);
            this.canvasPanel1.Name = "canvasPanel1";
            this.canvasPanel1.NormalColor = System.Drawing.Color.Transparent;
            this.canvasPanel1.Radius = -1;
            this.canvasPanel1.Size = new System.Drawing.Size(570, 380);
            this.canvasPanel1.Source = null;
            this.canvasPanel1.TabIndex = 0;
            this.canvasPanel1.Tag = 0;
            this.canvasPanel1.Text = "0";
            this.canvasPanel1.UIElementBinders = null;
            // 
            // pnl_KeyValueTableNames
            // 
            this.pnl_KeyValueTableNames.BackColor = System.Drawing.Color.Transparent;
            this.pnl_KeyValueTableNames.BorderColor = System.Drawing.Color.Gray;
            this.pnl_KeyValueTableNames.BorderThickness = -1;
            this.pnl_KeyValueTableNames.Controls.Add(this.ltb_KeyValueTableNames);
            this.pnl_KeyValueTableNames.Controls.Add(this.textBlock3);
            this.pnl_KeyValueTableNames.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.pnl_KeyValueTableNames.DataBindingInfo = null;
            this.pnl_KeyValueTableNames.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.pnl_KeyValueTableNames.HeadColor = System.Drawing.Color.Crimson;
            this.pnl_KeyValueTableNames.HeaderAlignment = dotNetLab.Widgets.Alignments.Down;
            this.pnl_KeyValueTableNames.HeadHeight = 40;
            this.pnl_KeyValueTableNames.ImagePos = new System.Drawing.Point(0, 0);
            this.pnl_KeyValueTableNames.ImageSize = new System.Drawing.Size(0, 0);
            this.pnl_KeyValueTableNames.Location = new System.Drawing.Point(290, 30);
            this.pnl_KeyValueTableNames.Name = "pnl_KeyValueTableNames";
            this.pnl_KeyValueTableNames.NormalColor = System.Drawing.Color.Snow;
            this.pnl_KeyValueTableNames.Radius = 10;
            this.pnl_KeyValueTableNames.Size = new System.Drawing.Size(278, 346);
            this.pnl_KeyValueTableNames.Source = null;
            this.pnl_KeyValueTableNames.TabIndex = 9;
            this.pnl_KeyValueTableNames.Text = "card1";
            this.pnl_KeyValueTableNames.UIElementBinders = null;
            // 
            // ltb_KeyValueTableNames
            // 
            this.ltb_KeyValueTableNames.BackColor = System.Drawing.Color.Transparent;
            this.ltb_KeyValueTableNames.BorderColor = System.Drawing.Color.Gray;
            this.ltb_KeyValueTableNames.BorderThickness = 0;
            this.ltb_KeyValueTableNames.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.ltb_KeyValueTableNames.DataBindingInfo = null;
            this.ltb_KeyValueTableNames.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ltb_KeyValueTableNames.ImagePos = new System.Drawing.Point(0, 0);
            this.ltb_KeyValueTableNames.ImageSize = new System.Drawing.Size(0, 0);
            this.ltb_KeyValueTableNames.Location = new System.Drawing.Point(3, 4);
            this.ltb_KeyValueTableNames.Name = "ltb_KeyValueTableNames";
            this.ltb_KeyValueTableNames.NormalColor = System.Drawing.Color.White;
            this.ltb_KeyValueTableNames.Radius = -1;
            this.ltb_KeyValueTableNames.Size = new System.Drawing.Size(271, 295);
            this.ltb_KeyValueTableNames.Source = null;
            this.ltb_KeyValueTableNames.TabIndex = 1;
            this.ltb_KeyValueTableNames.Text = null;
            this.ltb_KeyValueTableNames.UIElementBinders = null;
            // 
            // textBlock3
            // 
            this.textBlock3.BackColor = System.Drawing.Color.Transparent;
            this.textBlock3.BorderColor = System.Drawing.Color.Empty;
            this.textBlock3.BorderThickness = -1;
            this.textBlock3.DataBindingInfo = null;
            this.textBlock3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock3.ForeColor = System.Drawing.Color.White;
            this.textBlock3.LEDStyle = false;
            this.textBlock3.Location = new System.Drawing.Point(103, 311);
            this.textBlock3.Name = "textBlock3";
            this.textBlock3.Radius = -1;
            this.textBlock3.Size = new System.Drawing.Size(78, 25);
            this.textBlock3.TabIndex = 0;
            this.textBlock3.Text = "键值对表";
            this.textBlock3.UIElementBinders = null;
            this.textBlock3.Vertical = false;
            this.textBlock3.WhereReturn = ((byte)(0));
            // 
            // pnl_NormalTableNames
            // 
            this.pnl_NormalTableNames.BackColor = System.Drawing.Color.Transparent;
            this.pnl_NormalTableNames.BorderColor = System.Drawing.Color.Gray;
            this.pnl_NormalTableNames.BorderThickness = -1;
            this.pnl_NormalTableNames.Controls.Add(this.ltb_NormalTableNames);
            this.pnl_NormalTableNames.Controls.Add(this.textBlock2);
            this.pnl_NormalTableNames.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.pnl_NormalTableNames.DataBindingInfo = null;
            this.pnl_NormalTableNames.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.pnl_NormalTableNames.HeadColor = System.Drawing.Color.DodgerBlue;
            this.pnl_NormalTableNames.HeaderAlignment = dotNetLab.Widgets.Alignments.Up;
            this.pnl_NormalTableNames.HeadHeight = 40;
            this.pnl_NormalTableNames.ImagePos = new System.Drawing.Point(0, 0);
            this.pnl_NormalTableNames.ImageSize = new System.Drawing.Size(0, 0);
            this.pnl_NormalTableNames.Location = new System.Drawing.Point(2, 28);
            this.pnl_NormalTableNames.Name = "pnl_NormalTableNames";
            this.pnl_NormalTableNames.NormalColor = System.Drawing.Color.Snow;
            this.pnl_NormalTableNames.Radius = 10;
            this.pnl_NormalTableNames.Size = new System.Drawing.Size(278, 348);
            this.pnl_NormalTableNames.Source = null;
            this.pnl_NormalTableNames.TabIndex = 8;
            this.pnl_NormalTableNames.Text = "card1";
            this.pnl_NormalTableNames.UIElementBinders = null;
            // 
            // ltb_NormalTableNames
            // 
            this.ltb_NormalTableNames.BackColor = System.Drawing.Color.Transparent;
            this.ltb_NormalTableNames.BorderColor = System.Drawing.Color.Gray;
            this.ltb_NormalTableNames.BorderThickness = 0;
            this.ltb_NormalTableNames.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.ltb_NormalTableNames.DataBindingInfo = null;
            this.ltb_NormalTableNames.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ltb_NormalTableNames.ImagePos = new System.Drawing.Point(0, 0);
            this.ltb_NormalTableNames.ImageSize = new System.Drawing.Size(0, 0);
            this.ltb_NormalTableNames.Location = new System.Drawing.Point(3, 42);
            this.ltb_NormalTableNames.Name = "ltb_NormalTableNames";
            this.ltb_NormalTableNames.NormalColor = System.Drawing.Color.White;
            this.ltb_NormalTableNames.Radius = -1;
            this.ltb_NormalTableNames.Size = new System.Drawing.Size(271, 303);
            this.ltb_NormalTableNames.Source = null;
            this.ltb_NormalTableNames.TabIndex = 1;
            this.ltb_NormalTableNames.Text = null;
            this.ltb_NormalTableNames.UIElementBinders = null;
            // 
            // textBlock2
            // 
            this.textBlock2.BackColor = System.Drawing.Color.Transparent;
            this.textBlock2.BorderColor = System.Drawing.Color.Empty;
            this.textBlock2.BorderThickness = -1;
            this.textBlock2.DataBindingInfo = null;
            this.textBlock2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock2.ForeColor = System.Drawing.Color.White;
            this.textBlock2.LEDStyle = false;
            this.textBlock2.Location = new System.Drawing.Point(88, 7);
            this.textBlock2.Name = "textBlock2";
            this.textBlock2.Radius = -1;
            this.textBlock2.Size = new System.Drawing.Size(96, 25);
            this.textBlock2.TabIndex = 0;
            this.textBlock2.Text = "非键值对表";
            this.textBlock2.UIElementBinders = null;
            this.textBlock2.Vertical = false;
            this.textBlock2.WhereReturn = ((byte)(0));
            // 
            // canvasPanel2
            // 
            this.canvasPanel2.BackColor = System.Drawing.Color.Transparent;
            this.canvasPanel2.BorderColor = System.Drawing.Color.Empty;
            this.canvasPanel2.BorderThickness = -1;
            this.canvasPanel2.Controls.Add(this.dgv_PreviewTable);
            this.canvasPanel2.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.canvasPanel2.DataBindingInfo = null;
            this.canvasPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.canvasPanel2.ImagePos = new System.Drawing.Point(0, 0);
            this.canvasPanel2.ImageSize = new System.Drawing.Size(0, 0);
            this.canvasPanel2.Location = new System.Drawing.Point(0, 0);
            this.canvasPanel2.Name = "canvasPanel2";
            this.canvasPanel2.NormalColor = System.Drawing.Color.DarkGray;
            this.canvasPanel2.Radius = -1;
            this.canvasPanel2.Size = new System.Drawing.Size(570, 380);
            this.canvasPanel2.Source = null;
            this.canvasPanel2.TabIndex = 1;
            this.canvasPanel2.Tag = 1;
            this.canvasPanel2.Text = "1";
            this.canvasPanel2.UIElementBinders = null;
            // 
            // dgv_PreviewTable
            // 
            this.dgv_PreviewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_PreviewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_PreviewTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgv_PreviewTable.Location = new System.Drawing.Point(0, 0);
            this.dgv_PreviewTable.Name = "dgv_PreviewTable";
            this.dgv_PreviewTable.RowTemplate.Height = 23;
            this.dgv_PreviewTable.Size = new System.Drawing.Size(570, 380);
            this.dgv_PreviewTable.TabIndex = 0;
            // 
            // canvasPanel3
            // 
            this.canvasPanel3.BackColor = System.Drawing.Color.Transparent;
            this.canvasPanel3.BorderColor = System.Drawing.Color.Transparent;
            this.canvasPanel3.BorderThickness = -1;
            this.canvasPanel3.Controls.Add(this.card2);
            this.canvasPanel3.Controls.Add(this.card1);
            this.canvasPanel3.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.canvasPanel3.DataBindingInfo = null;
            this.canvasPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.canvasPanel3.ImagePos = new System.Drawing.Point(0, 0);
            this.canvasPanel3.ImageSize = new System.Drawing.Size(0, 0);
            this.canvasPanel3.Location = new System.Drawing.Point(0, 0);
            this.canvasPanel3.Name = "canvasPanel3";
            this.canvasPanel3.NormalColor = System.Drawing.Color.Transparent;
            this.canvasPanel3.Radius = -1;
            this.canvasPanel3.Size = new System.Drawing.Size(570, 380);
            this.canvasPanel3.Source = null;
            this.canvasPanel3.TabIndex = 2;
            this.canvasPanel3.Tag = 2;
            this.canvasPanel3.Text = "2";
            this.canvasPanel3.UIElementBinders = null;
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.Transparent;
            this.card2.BorderColor = System.Drawing.Color.Gray;
            this.card2.BorderThickness = -1;
            this.card2.Controls.Add(this.textBlock13);
            this.card2.Controls.Add(this.textBlock12);
            this.card2.Controls.Add(this.txb_ViewColumnNames);
            this.card2.Controls.Add(this.txb_KeyValueViewName);
            this.card2.Controls.Add(this.txb_SimpleColumnNames);
            this.card2.Controls.Add(this.btn_CreateKeyValueView);
            this.card2.Controls.Add(this.btn_GainColumnNames);
            this.card2.Controls.Add(this.btn_PutContentIntoClipboard);
            this.card2.Controls.Add(this.textBlock11);
            this.card2.Controls.Add(this.textBlock10);
            this.card2.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.card2.DataBindingInfo = null;
            this.card2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.card2.HeadColor = System.Drawing.Color.LightSeaGreen;
            this.card2.HeaderAlignment = dotNetLab.Widgets.Alignments.Right;
            this.card2.HeadHeight = 40;
            this.card2.ImagePos = new System.Drawing.Point(0, 0);
            this.card2.ImageSize = new System.Drawing.Size(0, 0);
            this.card2.Location = new System.Drawing.Point(265, 6);
            this.card2.Name = "card2";
            this.card2.NormalColor = System.Drawing.Color.Snow;
            this.card2.Radius = 10;
            this.card2.Size = new System.Drawing.Size(296, 371);
            this.card2.Source = null;
            this.card2.TabIndex = 1;
            this.card2.Text = "card2";
            this.card2.UIElementBinders = null;
            // 
            // textBlock13
            // 
            this.textBlock13.BackColor = System.Drawing.Color.Transparent;
            this.textBlock13.BorderColor = System.Drawing.Color.Empty;
            this.textBlock13.BorderThickness = -1;
            this.textBlock13.DataBindingInfo = null;
            this.textBlock13.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock13.LEDStyle = false;
            this.textBlock13.Location = new System.Drawing.Point(19, 188);
            this.textBlock13.Name = "textBlock13";
            this.textBlock13.Radius = -1;
            this.textBlock13.Size = new System.Drawing.Size(42, 25);
            this.textBlock13.TabIndex = 3;
            this.textBlock13.Text = "列名";
            this.textBlock13.UIElementBinders = null;
            this.textBlock13.Vertical = false;
            this.textBlock13.WhereReturn = ((byte)(0));
            // 
            // textBlock12
            // 
            this.textBlock12.BackColor = System.Drawing.Color.Transparent;
            this.textBlock12.BorderColor = System.Drawing.Color.Empty;
            this.textBlock12.BorderThickness = -1;
            this.textBlock12.DataBindingInfo = null;
            this.textBlock12.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock12.LEDStyle = false;
            this.textBlock12.Location = new System.Drawing.Point(17, 125);
            this.textBlock12.Name = "textBlock12";
            this.textBlock12.Radius = -1;
            this.textBlock12.Size = new System.Drawing.Size(60, 25);
            this.textBlock12.TabIndex = 3;
            this.textBlock12.Text = "视图名";
            this.textBlock12.UIElementBinders = null;
            this.textBlock12.Vertical = false;
            this.textBlock12.WhereReturn = ((byte)(0));
            // 
            // txb_ViewColumnNames
            // 
            this.txb_ViewColumnNames.ActiveColor = System.Drawing.Color.Orange;
            this.txb_ViewColumnNames.BackColor = System.Drawing.Color.Transparent;
            this.txb_ViewColumnNames.DataBindingInfo = null;
            this.txb_ViewColumnNames.DoubleValue = double.NaN;
            this.txb_ViewColumnNames.EnableMobileRound = true;
            this.txb_ViewColumnNames.EnableNullValue = false;
            this.txb_ViewColumnNames.FillColor = System.Drawing.Color.Transparent;
            this.txb_ViewColumnNames.FloatValue = float.NaN;
            this.txb_ViewColumnNames.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_ViewColumnNames.ForeColor = System.Drawing.Color.Black;
            this.txb_ViewColumnNames.IntValue = -2147483648;
            this.txb_ViewColumnNames.LineThickness = 2F;
            this.txb_ViewColumnNames.Location = new System.Drawing.Point(17, 215);
            this.txb_ViewColumnNames.Name = "txb_ViewColumnNames";
            this.txb_ViewColumnNames.Radius = -1;
            this.txb_ViewColumnNames.Size = new System.Drawing.Size(219, 31);
            this.txb_ViewColumnNames.StaticColor = System.Drawing.Color.DodgerBlue;
            this.txb_ViewColumnNames.TabIndex = 2;
            this.txb_ViewColumnNames.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txb_ViewColumnNames.TextBackColor = System.Drawing.SystemColors.Window;
            this.txb_ViewColumnNames.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_ViewColumnNames.UIElementBinders = null;
            this.txb_ViewColumnNames.WhitePattern = false;
            // 
            // txb_KeyValueViewName
            // 
            this.txb_KeyValueViewName.ActiveColor = System.Drawing.Color.Orange;
            this.txb_KeyValueViewName.BackColor = System.Drawing.Color.Transparent;
            this.txb_KeyValueViewName.DataBindingInfo = null;
            this.txb_KeyValueViewName.DoubleValue = double.NaN;
            this.txb_KeyValueViewName.EnableMobileRound = true;
            this.txb_KeyValueViewName.EnableNullValue = false;
            this.txb_KeyValueViewName.FillColor = System.Drawing.Color.Transparent;
            this.txb_KeyValueViewName.FloatValue = float.NaN;
            this.txb_KeyValueViewName.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_KeyValueViewName.ForeColor = System.Drawing.Color.Black;
            this.txb_KeyValueViewName.IntValue = -2147483648;
            this.txb_KeyValueViewName.LineThickness = 2F;
            this.txb_KeyValueViewName.Location = new System.Drawing.Point(17, 151);
            this.txb_KeyValueViewName.Name = "txb_KeyValueViewName";
            this.txb_KeyValueViewName.Radius = -1;
            this.txb_KeyValueViewName.Size = new System.Drawing.Size(219, 31);
            this.txb_KeyValueViewName.StaticColor = System.Drawing.Color.DodgerBlue;
            this.txb_KeyValueViewName.TabIndex = 2;
            this.txb_KeyValueViewName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txb_KeyValueViewName.TextBackColor = System.Drawing.SystemColors.Window;
            this.txb_KeyValueViewName.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_KeyValueViewName.UIElementBinders = null;
            this.txb_KeyValueViewName.WhitePattern = false;
            // 
            // txb_SimpleColumnNames
            // 
            this.txb_SimpleColumnNames.ActiveColor = System.Drawing.Color.Orange;
            this.txb_SimpleColumnNames.BackColor = System.Drawing.Color.Transparent;
            this.txb_SimpleColumnNames.DataBindingInfo = null;
            this.txb_SimpleColumnNames.DoubleValue = double.NaN;
            this.txb_SimpleColumnNames.EnableMobileRound = true;
            this.txb_SimpleColumnNames.EnableNullValue = false;
            this.txb_SimpleColumnNames.FillColor = System.Drawing.Color.Transparent;
            this.txb_SimpleColumnNames.FloatValue = float.NaN;
            this.txb_SimpleColumnNames.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txb_SimpleColumnNames.ForeColor = System.Drawing.Color.Black;
            this.txb_SimpleColumnNames.IntValue = -2147483648;
            this.txb_SimpleColumnNames.LineThickness = 2F;
            this.txb_SimpleColumnNames.Location = new System.Drawing.Point(19, 16);
            this.txb_SimpleColumnNames.Name = "txb_SimpleColumnNames";
            this.txb_SimpleColumnNames.Radius = -1;
            this.txb_SimpleColumnNames.Size = new System.Drawing.Size(219, 31);
            this.txb_SimpleColumnNames.StaticColor = System.Drawing.Color.DodgerBlue;
            this.txb_SimpleColumnNames.TabIndex = 2;
            this.txb_SimpleColumnNames.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txb_SimpleColumnNames.TextBackColor = System.Drawing.SystemColors.Window;
            this.txb_SimpleColumnNames.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_SimpleColumnNames.UIElementBinders = null;
            this.txb_SimpleColumnNames.WhitePattern = false;
            // 
            // btn_CreateKeyValueView
            // 
            this.btn_CreateKeyValueView.BackColor = System.Drawing.Color.Transparent;
            this.btn_CreateKeyValueView.BorderColor = System.Drawing.Color.Empty;
            this.btn_CreateKeyValueView.BorderThickness = -1;
            this.btn_CreateKeyValueView.CornerAligment = dotNetLab.Widgets.Alignments.All;
            this.btn_CreateKeyValueView.DataBindingInfo = null;
            this.btn_CreateKeyValueView.EnableMobileRound = true;
            this.btn_CreateKeyValueView.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_CreateKeyValueView.ForeColor = System.Drawing.Color.White;
            this.btn_CreateKeyValueView.GapBetweenTextImage = 8;
            this.btn_CreateKeyValueView.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
            this.btn_CreateKeyValueView.ImageSize = new System.Drawing.Size(0, 0);
            this.btn_CreateKeyValueView.LEDStyle = false;
            this.btn_CreateKeyValueView.Location = new System.Drawing.Point(14, 264);
            this.btn_CreateKeyValueView.Name = "btn_CreateKeyValueView";
            this.btn_CreateKeyValueView.NeedAnimation = true;
            this.btn_CreateKeyValueView.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_CreateKeyValueView.PressColor = System.Drawing.Color.Cyan;
            this.btn_CreateKeyValueView.Radius = 37;
            this.btn_CreateKeyValueView.Size = new System.Drawing.Size(224, 38);
            this.btn_CreateKeyValueView.Source = null;
            this.btn_CreateKeyValueView.TabIndex = 1;
            this.btn_CreateKeyValueView.Text = "创建键值视图";
            this.btn_CreateKeyValueView.UIElementBinders = null;
            this.btn_CreateKeyValueView.Vertical = false;
            this.btn_CreateKeyValueView.WhereReturn = ((byte)(0));
            // 
            // btn_GainColumnNames
            // 
            this.btn_GainColumnNames.BackColor = System.Drawing.Color.Transparent;
            this.btn_GainColumnNames.BorderColor = System.Drawing.Color.Empty;
            this.btn_GainColumnNames.BorderThickness = -1;
            this.btn_GainColumnNames.CornerAligment = dotNetLab.Widgets.Alignments.All;
            this.btn_GainColumnNames.DataBindingInfo = null;
            this.btn_GainColumnNames.EnableMobileRound = true;
            this.btn_GainColumnNames.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_GainColumnNames.ForeColor = System.Drawing.Color.White;
            this.btn_GainColumnNames.GapBetweenTextImage = 8;
            this.btn_GainColumnNames.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
            this.btn_GainColumnNames.ImageSize = new System.Drawing.Size(0, 0);
            this.btn_GainColumnNames.LEDStyle = false;
            this.btn_GainColumnNames.Location = new System.Drawing.Point(17, 57);
            this.btn_GainColumnNames.Name = "btn_GainColumnNames";
            this.btn_GainColumnNames.NeedAnimation = true;
            this.btn_GainColumnNames.NormalColor = System.Drawing.Color.YellowGreen;
            this.btn_GainColumnNames.PressColor = System.Drawing.Color.Cyan;
            this.btn_GainColumnNames.Radius = 37;
            this.btn_GainColumnNames.Size = new System.Drawing.Size(224, 38);
            this.btn_GainColumnNames.Source = null;
            this.btn_GainColumnNames.TabIndex = 1;
            this.btn_GainColumnNames.Text = "追加到文本框";
            this.btn_GainColumnNames.UIElementBinders = null;
            this.btn_GainColumnNames.Vertical = false;
            this.btn_GainColumnNames.WhereReturn = ((byte)(0));
            // 
            // btn_PutContentIntoClipboard
            // 
            this.btn_PutContentIntoClipboard.BackColor = System.Drawing.Color.Transparent;
            this.btn_PutContentIntoClipboard.BorderColor = System.Drawing.Color.Empty;
            this.btn_PutContentIntoClipboard.BorderThickness = -1;
            this.btn_PutContentIntoClipboard.CornerAligment = dotNetLab.Widgets.Alignments.All;
            this.btn_PutContentIntoClipboard.DataBindingInfo = null;
            this.btn_PutContentIntoClipboard.EnableMobileRound = true;
            this.btn_PutContentIntoClipboard.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_PutContentIntoClipboard.ForeColor = System.Drawing.Color.White;
            this.btn_PutContentIntoClipboard.GapBetweenTextImage = 8;
            this.btn_PutContentIntoClipboard.IConAlignment = System.Windows.Forms.LeftRightAlignment.Left;
            this.btn_PutContentIntoClipboard.ImageSize = new System.Drawing.Size(0, 0);
            this.btn_PutContentIntoClipboard.LEDStyle = false;
            this.btn_PutContentIntoClipboard.Location = new System.Drawing.Point(18, 318);
            this.btn_PutContentIntoClipboard.Name = "btn_PutContentIntoClipboard";
            this.btn_PutContentIntoClipboard.NeedAnimation = true;
            this.btn_PutContentIntoClipboard.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_PutContentIntoClipboard.PressColor = System.Drawing.Color.Cyan;
            this.btn_PutContentIntoClipboard.Radius = 37;
            this.btn_PutContentIntoClipboard.Size = new System.Drawing.Size(224, 38);
            this.btn_PutContentIntoClipboard.Source = null;
            this.btn_PutContentIntoClipboard.TabIndex = 1;
            this.btn_PutContentIntoClipboard.Text = "放于剪贴板中";
            this.btn_PutContentIntoClipboard.UIElementBinders = null;
            this.btn_PutContentIntoClipboard.Vertical = false;
            this.btn_PutContentIntoClipboard.WhereReturn = ((byte)(0));
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
            this.textBlock11.Location = new System.Drawing.Point(258, 151);
            this.textBlock11.Name = "textBlock11";
            this.textBlock11.Radius = -1;
            this.textBlock11.Size = new System.Drawing.Size(42, 25);
            this.textBlock11.TabIndex = 0;
            this.textBlock11.Text = "操作";
            this.textBlock11.UIElementBinders = null;
            this.textBlock11.Vertical = true;
            this.textBlock11.WhereReturn = ((byte)(0));
            // 
            // textBlock10
            // 
            this.textBlock10.BackColor = System.Drawing.Color.Transparent;
            this.textBlock10.BorderColor = System.Drawing.Color.Empty;
            this.textBlock10.BorderThickness = -1;
            this.textBlock10.DataBindingInfo = null;
            this.textBlock10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock10.ForeColor = System.Drawing.Color.White;
            this.textBlock10.LEDStyle = false;
            this.textBlock10.Location = new System.Drawing.Point(115, 9);
            this.textBlock10.Name = "textBlock10";
            this.textBlock10.Radius = -1;
            this.textBlock10.Size = new System.Drawing.Size(42, 25);
            this.textBlock10.TabIndex = 0;
            this.textBlock10.Text = "操作";
            this.textBlock10.UIElementBinders = null;
            this.textBlock10.Vertical = false;
            this.textBlock10.WhereReturn = ((byte)(0));
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.Transparent;
            this.card1.BorderColor = System.Drawing.Color.Gray;
            this.card1.BorderThickness = -1;
            this.card1.Controls.Add(this.ltb_ColumnNames);
            this.card1.Controls.Add(this.textBlock9);
            this.card1.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.card1.DataBindingInfo = null;
            this.card1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.card1.HeadColor = System.Drawing.Color.DodgerBlue;
            this.card1.HeaderAlignment = dotNetLab.Widgets.Alignments.Up;
            this.card1.HeadHeight = 40;
            this.card1.ImagePos = new System.Drawing.Point(0, 0);
            this.card1.ImageSize = new System.Drawing.Size(0, 0);
            this.card1.Location = new System.Drawing.Point(3, 6);
            this.card1.Name = "card1";
            this.card1.NormalColor = System.Drawing.Color.Snow;
            this.card1.Radius = 10;
            this.card1.Size = new System.Drawing.Size(258, 370);
            this.card1.Source = null;
            this.card1.TabIndex = 0;
            this.card1.Text = "card1";
            this.card1.UIElementBinders = null;
            // 
            // ltb_ColumnNames
            // 
            this.ltb_ColumnNames.BackColor = System.Drawing.Color.Transparent;
            this.ltb_ColumnNames.BorderColor = System.Drawing.Color.Transparent;
            this.ltb_ColumnNames.BorderThickness = 1;
            this.ltb_ColumnNames.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.ltb_ColumnNames.DataBindingInfo = null;
            this.ltb_ColumnNames.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ltb_ColumnNames.ImagePos = new System.Drawing.Point(0, 0);
            this.ltb_ColumnNames.ImageSize = new System.Drawing.Size(0, 0);
            this.ltb_ColumnNames.Location = new System.Drawing.Point(1, 40);
            this.ltb_ColumnNames.Name = "ltb_ColumnNames";
            this.ltb_ColumnNames.NormalColor = System.Drawing.Color.White;
            this.ltb_ColumnNames.Radius = -1;
            this.ltb_ColumnNames.Size = new System.Drawing.Size(255, 320);
            this.ltb_ColumnNames.Source = null;
            this.ltb_ColumnNames.TabIndex = 1;
            this.ltb_ColumnNames.Text = "ltb_ColumnNames";
            this.ltb_ColumnNames.UIElementBinders = null;
            // 
            // textBlock9
            // 
            this.textBlock9.BackColor = System.Drawing.Color.Transparent;
            this.textBlock9.BorderColor = System.Drawing.Color.Empty;
            this.textBlock9.BorderThickness = -1;
            this.textBlock9.DataBindingInfo = null;
            this.textBlock9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock9.ForeColor = System.Drawing.Color.White;
            this.textBlock9.LEDStyle = false;
            this.textBlock9.Location = new System.Drawing.Point(97, 9);
            this.textBlock9.Name = "textBlock9";
            this.textBlock9.Radius = -1;
            this.textBlock9.Size = new System.Drawing.Size(42, 25);
            this.textBlock9.TabIndex = 0;
            this.textBlock9.Text = "列名";
            this.textBlock9.UIElementBinders = null;
            this.textBlock9.Vertical = false;
            this.textBlock9.WhereReturn = ((byte)(0));
            // 
            // FramePannel_Header
            // 
            this.FramePannel_Header.BackColor = System.Drawing.Color.Transparent;
            this.FramePannel_Header.BorderColor = System.Drawing.Color.Empty;
            this.FramePannel_Header.BorderThickness = -1;
            this.FramePannel_Header.Controls.Add(this.canvasPanel4);
            this.FramePannel_Header.Controls.Add(this.canvasPanel6);
            this.FramePannel_Header.Controls.Add(this.canvasPanel5);
            this.FramePannel_Header.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.FramePannel_Header.DataBindingInfo = null;
            this.FramePannel_Header.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.FramePannel_Header.ImagePos = new System.Drawing.Point(0, 0);
            this.FramePannel_Header.ImageSize = new System.Drawing.Size(0, 0);
            this.FramePannel_Header.Location = new System.Drawing.Point(17, 3);
            this.FramePannel_Header.Name = "FramePannel_Header";
            this.FramePannel_Header.NormalColor = System.Drawing.Color.Transparent;
            this.FramePannel_Header.Radius = -1;
            this.FramePannel_Header.Size = new System.Drawing.Size(568, 65);
            this.FramePannel_Header.Source = null;
            this.FramePannel_Header.TabIndex = 12;
            this.FramePannel_Header.Text = "framePanel2";
            this.FramePannel_Header.UIElementBinders = null;
            this.FramePannel_Header.WhichNeedToEdit = 0;
            // 
            // canvasPanel4
            // 
            this.canvasPanel4.BackColor = System.Drawing.Color.Transparent;
            this.canvasPanel4.BorderColor = System.Drawing.Color.Empty;
            this.canvasPanel4.BorderThickness = -1;
            this.canvasPanel4.Controls.Add(this.textBlock5);
            this.canvasPanel4.Controls.Add(this.textBlock4);
            this.canvasPanel4.Controls.Add(this.txb_PatternSearch);
            this.canvasPanel4.Controls.Add(this.textBlock1);
            this.canvasPanel4.Controls.Add(this.btn_ShowColumnNames);
            this.canvasPanel4.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.canvasPanel4.DataBindingInfo = null;
            this.canvasPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.canvasPanel4.ImagePos = new System.Drawing.Point(0, 0);
            this.canvasPanel4.ImageSize = new System.Drawing.Size(0, 0);
            this.canvasPanel4.Location = new System.Drawing.Point(0, 0);
            this.canvasPanel4.Name = "canvasPanel4";
            this.canvasPanel4.NormalColor = System.Drawing.Color.Transparent;
            this.canvasPanel4.Radius = -1;
            this.canvasPanel4.Size = new System.Drawing.Size(568, 65);
            this.canvasPanel4.Source = null;
            this.canvasPanel4.TabIndex = 0;
            this.canvasPanel4.Tag = 0;
            this.canvasPanel4.Text = null;
            this.canvasPanel4.UIElementBinders = null;
            // 
            // textBlock5
            // 
            this.textBlock5.BackColor = System.Drawing.Color.Transparent;
            this.textBlock5.BorderColor = System.Drawing.Color.Empty;
            this.textBlock5.BorderThickness = -1;
            this.textBlock5.DataBindingInfo = null;
            this.textBlock5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBlock5.ForeColor = System.Drawing.Color.Green;
            this.textBlock5.LEDStyle = false;
            this.textBlock5.Location = new System.Drawing.Point(110, -1);
            this.textBlock5.Name = "textBlock5";
            this.textBlock5.Radius = -1;
            this.textBlock5.Size = new System.Drawing.Size(235, 21);
            this.textBlock5.TabIndex = 16;
            this.textBlock5.Text = "查看所有表请保留文本框为空,回车";
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
            this.textBlock4.ForeColor = System.Drawing.Color.DimGray;
            this.textBlock4.LEDStyle = false;
            this.textBlock4.Location = new System.Drawing.Point(408, 24);
            this.textBlock4.Name = "textBlock4";
            this.textBlock4.Radius = -1;
            this.textBlock4.Size = new System.Drawing.Size(96, 25);
            this.textBlock4.TabIndex = 15;
            this.textBlock4.Text = "列举表列名";
            this.textBlock4.UIElementBinders = null;
            this.textBlock4.Vertical = false;
            this.textBlock4.WhereReturn = ((byte)(0));
            // 
            // txb_PatternSearch
            // 
            this.txb_PatternSearch.ActiveColor = System.Drawing.Color.Cyan;
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
            this.txb_PatternSearch.Location = new System.Drawing.Point(79, 23);
            this.txb_PatternSearch.Name = "txb_PatternSearch";
            this.txb_PatternSearch.Radius = 30;
            this.txb_PatternSearch.Size = new System.Drawing.Size(290, 31);
            this.txb_PatternSearch.StaticColor = System.Drawing.Color.Gray;
            this.txb_PatternSearch.TabIndex = 14;
            this.txb_PatternSearch.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txb_PatternSearch.TextBackColor = System.Drawing.Color.White;
            this.txb_PatternSearch.TextBoxStyle = dotNetLab.Widgets.MobileTextBox.TextBoxStyles.Mobile;
            this.txb_PatternSearch.UIElementBinders = null;
            this.txb_PatternSearch.WhitePattern = false;
            // 
            // textBlock1
            // 
            this.textBlock1.BackColor = System.Drawing.Color.Transparent;
            this.textBlock1.BorderColor = System.Drawing.Color.Empty;
            this.textBlock1.BorderThickness = -1;
            this.textBlock1.DataBindingInfo = null;
            this.textBlock1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock1.LEDStyle = false;
            this.textBlock1.Location = new System.Drawing.Point(-3, 28);
            this.textBlock1.Name = "textBlock1";
            this.textBlock1.Radius = -1;
            this.textBlock1.Size = new System.Drawing.Size(78, 25);
            this.textBlock1.TabIndex = 13;
            this.textBlock1.Text = "模糊查寻";
            this.textBlock1.UIElementBinders = null;
            this.textBlock1.Vertical = false;
            this.textBlock1.WhereReturn = ((byte)(0));
            // 
            // btn_ShowColumnNames
            // 
            this.btn_ShowColumnNames.ArrowAlignment = dotNetLab.Widgets.Alignments.Right;
            this.btn_ShowColumnNames.BackColor = System.Drawing.Color.Transparent;
            this.btn_ShowColumnNames.BorderColor = System.Drawing.Color.Gray;
            this.btn_ShowColumnNames.BorderThickness = 2F;
            this.btn_ShowColumnNames.CenterImage = true;
            this.btn_ShowColumnNames.ClipCircleRegion = false;
            this.btn_ShowColumnNames.DataBindingInfo = null;
            this.btn_ShowColumnNames.Effect = null;
            this.btn_ShowColumnNames.Fill = false;
            this.btn_ShowColumnNames.FillColor = System.Drawing.Color.Empty;
            this.btn_ShowColumnNames.ImagePostion = new System.Drawing.Point(0, 0);
            this.btn_ShowColumnNames.ImageSize = new System.Drawing.SizeF(25F, 25F);
            this.btn_ShowColumnNames.Location = new System.Drawing.Point(515, 8);
            this.btn_ShowColumnNames.MouseDownColor = System.Drawing.Color.Gray;
            this.btn_ShowColumnNames.Name = "btn_ShowColumnNames";
            this.btn_ShowColumnNames.NeedEffect = false;
            this.btn_ShowColumnNames.Size = new System.Drawing.Size(50, 50);
            this.btn_ShowColumnNames.Source = ((System.Drawing.Image)(resources.GetObject("btn_ShowColumnNames.Source")));
            this.btn_ShowColumnNames.TabIndex = 12;
            this.btn_ShowColumnNames.UIElementBinders = null;
            this.btn_ShowColumnNames.WhichShap = 6;
            this.btn_ShowColumnNames.WhitePattern = false;
            // 
            // canvasPanel6
            // 
            this.canvasPanel6.BackColor = System.Drawing.Color.Transparent;
            this.canvasPanel6.BorderColor = System.Drawing.Color.Empty;
            this.canvasPanel6.BorderThickness = -1;
            this.canvasPanel6.Controls.Add(this.textBlock14);
            this.canvasPanel6.Controls.Add(this.lnk_PreviewTableContent);
            this.canvasPanel6.Controls.Add(this.textBlock8);
            this.canvasPanel6.Controls.Add(this.btn_Goback_2);
            this.canvasPanel6.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.canvasPanel6.DataBindingInfo = null;
            this.canvasPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel6.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.canvasPanel6.ImagePos = new System.Drawing.Point(0, 0);
            this.canvasPanel6.ImageSize = new System.Drawing.Size(0, 0);
            this.canvasPanel6.Location = new System.Drawing.Point(0, 0);
            this.canvasPanel6.Name = "canvasPanel6";
            this.canvasPanel6.NormalColor = System.Drawing.Color.Transparent;
            this.canvasPanel6.Radius = -1;
            this.canvasPanel6.Size = new System.Drawing.Size(568, 65);
            this.canvasPanel6.Source = null;
            this.canvasPanel6.TabIndex = 2;
            this.canvasPanel6.Tag = 2;
            this.canvasPanel6.Text = null;
            this.canvasPanel6.UIElementBinders = null;
            // 
            // textBlock14
            // 
            this.textBlock14.BackColor = System.Drawing.Color.Transparent;
            this.textBlock14.BorderColor = System.Drawing.Color.Empty;
            this.textBlock14.BorderThickness = -1;
            this.textBlock14.DataBindingInfo = null;
            this.textBlock14.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock14.LEDStyle = false;
            this.textBlock14.Location = new System.Drawing.Point(328, 21);
            this.textBlock14.Name = "textBlock14";
            this.textBlock14.Radius = -1;
            this.textBlock14.Size = new System.Drawing.Size(96, 25);
            this.textBlock14.TabIndex = 5;
            this.textBlock14.Text = "预览表内容";
            this.textBlock14.UIElementBinders = null;
            this.textBlock14.Vertical = false;
            this.textBlock14.WhereReturn = ((byte)(0));
            // 
            // lnk_PreviewTableContent
            // 
            this.lnk_PreviewTableContent.AutoSize = true;
            this.lnk_PreviewTableContent.Location = new System.Drawing.Point(443, 23);
            this.lnk_PreviewTableContent.Name = "lnk_PreviewTableContent";
            this.lnk_PreviewTableContent.Size = new System.Drawing.Size(0, 20);
            this.lnk_PreviewTableContent.TabIndex = 4;
            // 
            // textBlock8
            // 
            this.textBlock8.BackColor = System.Drawing.Color.Transparent;
            this.textBlock8.BorderColor = System.Drawing.Color.Empty;
            this.textBlock8.BorderThickness = -1;
            this.textBlock8.DataBindingInfo = null;
            this.textBlock8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock8.ForeColor = System.Drawing.Color.DimGray;
            this.textBlock8.LEDStyle = false;
            this.textBlock8.Location = new System.Drawing.Point(58, 22);
            this.textBlock8.Name = "textBlock8";
            this.textBlock8.Radius = -1;
            this.textBlock8.Size = new System.Drawing.Size(42, 25);
            this.textBlock8.TabIndex = 3;
            this.textBlock8.Text = "返回";
            this.textBlock8.UIElementBinders = null;
            this.textBlock8.Vertical = false;
            this.textBlock8.WhereReturn = ((byte)(0));
            // 
            // btn_Goback_2
            // 
            this.btn_Goback_2.ArrowAlignment = dotNetLab.Widgets.Alignments.Left;
            this.btn_Goback_2.BackColor = System.Drawing.Color.Transparent;
            this.btn_Goback_2.BorderColor = System.Drawing.Color.Gray;
            this.btn_Goback_2.BorderThickness = 2F;
            this.btn_Goback_2.CenterImage = true;
            this.btn_Goback_2.ClipCircleRegion = false;
            this.btn_Goback_2.DataBindingInfo = null;
            this.btn_Goback_2.Effect = null;
            this.btn_Goback_2.Fill = false;
            this.btn_Goback_2.FillColor = System.Drawing.Color.Empty;
            this.btn_Goback_2.ImagePostion = new System.Drawing.Point(0, 0);
            this.btn_Goback_2.ImageSize = new System.Drawing.SizeF(25F, 25F);
            this.btn_Goback_2.Location = new System.Drawing.Point(3, 7);
            this.btn_Goback_2.MouseDownColor = System.Drawing.Color.Gray;
            this.btn_Goback_2.Name = "btn_Goback_2";
            this.btn_Goback_2.NeedEffect = false;
            this.btn_Goback_2.Size = new System.Drawing.Size(50, 50);
            this.btn_Goback_2.Source = ((System.Drawing.Image)(resources.GetObject("btn_Goback_2.Source")));
            this.btn_Goback_2.TabIndex = 2;
            this.btn_Goback_2.UIElementBinders = null;
            this.btn_Goback_2.WhichShap = 6;
            this.btn_Goback_2.WhitePattern = false;
            // 
            // canvasPanel5
            // 
            this.canvasPanel5.BackColor = System.Drawing.Color.Transparent;
            this.canvasPanel5.BorderColor = System.Drawing.Color.Empty;
            this.canvasPanel5.BorderThickness = -1;
            this.canvasPanel5.Controls.Add(this.lnk_ShowTableContent_Full);
            this.canvasPanel5.Controls.Add(this.textBlock7);
            this.canvasPanel5.Controls.Add(this.btn_ShowColumnName_1);
            this.canvasPanel5.Controls.Add(this.textBlock6);
            this.canvasPanel5.Controls.Add(this.btn_GoBackFromTablePreviewToMainPart);
            this.canvasPanel5.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.canvasPanel5.DataBindingInfo = null;
            this.canvasPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel5.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.canvasPanel5.ImagePos = new System.Drawing.Point(0, 0);
            this.canvasPanel5.ImageSize = new System.Drawing.Size(0, 0);
            this.canvasPanel5.Location = new System.Drawing.Point(0, 0);
            this.canvasPanel5.Name = "canvasPanel5";
            this.canvasPanel5.NormalColor = System.Drawing.Color.Transparent;
            this.canvasPanel5.Radius = -1;
            this.canvasPanel5.Size = new System.Drawing.Size(568, 65);
            this.canvasPanel5.Source = null;
            this.canvasPanel5.TabIndex = 1;
            this.canvasPanel5.Tag = 1;
            this.canvasPanel5.Text = null;
            this.canvasPanel5.UIElementBinders = null;
            // 
            // lnk_ShowTableContent_Full
            // 
            this.lnk_ShowTableContent_Full.AutoSize = true;
            this.lnk_ShowTableContent_Full.Location = new System.Drawing.Point(246, 24);
            this.lnk_ShowTableContent_Full.Name = "lnk_ShowTableContent_Full";
            this.lnk_ShowTableContent_Full.Size = new System.Drawing.Size(133, 20);
            this.lnk_ShowTableContent_Full.TabIndex = 18;
            this.lnk_ShowTableContent_Full.TabStop = true;
            this.lnk_ShowTableContent_Full.Text = "在独立窗口中显示";
            // 
            // textBlock7
            // 
            this.textBlock7.BackColor = System.Drawing.Color.Transparent;
            this.textBlock7.BorderColor = System.Drawing.Color.Empty;
            this.textBlock7.BorderThickness = -1;
            this.textBlock7.DataBindingInfo = null;
            this.textBlock7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock7.ForeColor = System.Drawing.Color.DimGray;
            this.textBlock7.LEDStyle = false;
            this.textBlock7.Location = new System.Drawing.Point(391, 24);
            this.textBlock7.Name = "textBlock7";
            this.textBlock7.Radius = -1;
            this.textBlock7.Size = new System.Drawing.Size(96, 25);
            this.textBlock7.TabIndex = 17;
            this.textBlock7.Text = "列举表列名";
            this.textBlock7.UIElementBinders = null;
            this.textBlock7.Vertical = false;
            this.textBlock7.WhereReturn = ((byte)(0));
            // 
            // btn_ShowColumnName_1
            // 
            this.btn_ShowColumnName_1.ArrowAlignment = dotNetLab.Widgets.Alignments.Right;
            this.btn_ShowColumnName_1.BackColor = System.Drawing.Color.Transparent;
            this.btn_ShowColumnName_1.BorderColor = System.Drawing.Color.Gray;
            this.btn_ShowColumnName_1.BorderThickness = 2F;
            this.btn_ShowColumnName_1.CenterImage = true;
            this.btn_ShowColumnName_1.ClipCircleRegion = false;
            this.btn_ShowColumnName_1.DataBindingInfo = null;
            this.btn_ShowColumnName_1.Effect = null;
            this.btn_ShowColumnName_1.Fill = false;
            this.btn_ShowColumnName_1.FillColor = System.Drawing.Color.Empty;
            this.btn_ShowColumnName_1.ImagePostion = new System.Drawing.Point(0, 0);
            this.btn_ShowColumnName_1.ImageSize = new System.Drawing.SizeF(25F, 25F);
            this.btn_ShowColumnName_1.Location = new System.Drawing.Point(498, 8);
            this.btn_ShowColumnName_1.MouseDownColor = System.Drawing.Color.Gray;
            this.btn_ShowColumnName_1.Name = "btn_ShowColumnName_1";
            this.btn_ShowColumnName_1.NeedEffect = false;
            this.btn_ShowColumnName_1.Size = new System.Drawing.Size(50, 50);
            this.btn_ShowColumnName_1.Source = ((System.Drawing.Image)(resources.GetObject("btn_ShowColumnName_1.Source")));
            this.btn_ShowColumnName_1.TabIndex = 16;
            this.btn_ShowColumnName_1.UIElementBinders = null;
            this.btn_ShowColumnName_1.WhichShap = 6;
            this.btn_ShowColumnName_1.WhitePattern = false;
            // 
            // textBlock6
            // 
            this.textBlock6.BackColor = System.Drawing.Color.Transparent;
            this.textBlock6.BorderColor = System.Drawing.Color.Empty;
            this.textBlock6.BorderThickness = -1;
            this.textBlock6.DataBindingInfo = null;
            this.textBlock6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBlock6.ForeColor = System.Drawing.Color.DimGray;
            this.textBlock6.LEDStyle = false;
            this.textBlock6.Location = new System.Drawing.Point(58, 22);
            this.textBlock6.Name = "textBlock6";
            this.textBlock6.Radius = -1;
            this.textBlock6.Size = new System.Drawing.Size(42, 25);
            this.textBlock6.TabIndex = 1;
            this.textBlock6.Text = "返回";
            this.textBlock6.UIElementBinders = null;
            this.textBlock6.Vertical = false;
            this.textBlock6.WhereReturn = ((byte)(0));
            // 
            // btn_GoBackFromTablePreviewToMainPart
            // 
            this.btn_GoBackFromTablePreviewToMainPart.ArrowAlignment = dotNetLab.Widgets.Alignments.Left;
            this.btn_GoBackFromTablePreviewToMainPart.BackColor = System.Drawing.Color.Transparent;
            this.btn_GoBackFromTablePreviewToMainPart.BorderColor = System.Drawing.Color.Gray;
            this.btn_GoBackFromTablePreviewToMainPart.BorderThickness = 2F;
            this.btn_GoBackFromTablePreviewToMainPart.CenterImage = true;
            this.btn_GoBackFromTablePreviewToMainPart.ClipCircleRegion = false;
            this.btn_GoBackFromTablePreviewToMainPart.DataBindingInfo = null;
            this.btn_GoBackFromTablePreviewToMainPart.Effect = null;
            this.btn_GoBackFromTablePreviewToMainPart.Fill = false;
            this.btn_GoBackFromTablePreviewToMainPart.FillColor = System.Drawing.Color.Empty;
            this.btn_GoBackFromTablePreviewToMainPart.ImagePostion = new System.Drawing.Point(0, 0);
            this.btn_GoBackFromTablePreviewToMainPart.ImageSize = new System.Drawing.SizeF(25F, 25F);
            this.btn_GoBackFromTablePreviewToMainPart.Location = new System.Drawing.Point(3, 7);
            this.btn_GoBackFromTablePreviewToMainPart.MouseDownColor = System.Drawing.Color.Gray;
            this.btn_GoBackFromTablePreviewToMainPart.Name = "btn_GoBackFromTablePreviewToMainPart";
            this.btn_GoBackFromTablePreviewToMainPart.NeedEffect = false;
            this.btn_GoBackFromTablePreviewToMainPart.Size = new System.Drawing.Size(50, 50);
            this.btn_GoBackFromTablePreviewToMainPart.Source = ((System.Drawing.Image)(resources.GetObject("btn_GoBackFromTablePreviewToMainPart.Source")));
            this.btn_GoBackFromTablePreviewToMainPart.TabIndex = 0;
            this.btn_GoBackFromTablePreviewToMainPart.UIElementBinders = null;
            this.btn_GoBackFromTablePreviewToMainPart.WhichShap = 6;
            this.btn_GoBackFromTablePreviewToMainPart.WhitePattern = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TableProvidePart
            // 
            this.Controls.Add(this.FramePannel_Header);
            this.Controls.Add(this.FramePanel_Content);
            this.Name = "TableProvidePart";
            this.NormalColor = System.Drawing.Color.Transparent;
            this.Radius = 10;
            this.Size = new System.Drawing.Size(605, 480);
            this.Load += new System.EventHandler(this.TableProvidePart_Load);
            this.FramePanel_Content.ResumeLayout(false);
            this.canvasPanel1.ResumeLayout(false);
            this.pnl_KeyValueTableNames.ResumeLayout(false);
            this.pnl_NormalTableNames.ResumeLayout(false);
            this.canvasPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PreviewTable)).EndInit();
            this.canvasPanel3.ResumeLayout(false);
            this.card2.ResumeLayout(false);
            this.card1.ResumeLayout(false);
            this.FramePannel_Header.ResumeLayout(false);
            this.canvasPanel4.ResumeLayout(false);
            this.canvasPanel6.ResumeLayout(false);
            this.canvasPanel6.PerformLayout();
            this.canvasPanel5.ResumeLayout(false);
            this.canvasPanel5.PerformLayout();
            this.ResumeLayout(false);

        }
        protected override void prepareCtrls()
        {

            base.prepareCtrls();
            InitializeComponent();

        }

       
    }
}
