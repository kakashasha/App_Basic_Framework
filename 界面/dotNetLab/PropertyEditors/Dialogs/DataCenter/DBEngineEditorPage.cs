using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using dotNetLab.Forms;
using dotNetLab.Widgets.Parts;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{
    public class DBEngineEditorPage : MetroListPage
    {
        private Widgets.Container.CanvasPanel canvasPanel1;
        TableProvidePart tableProvidePart;
        public Object DataBindingPropertyEditorDialogObject;
        SQL_Query_Part sqlQueryPart;
        private ConveyDataToOtherDBPart db2DbPart;
        public DBEngineInvoker dbInvoker;
        protected override void prepareCtrls()
        {

            base.prepareCtrls();
            InitializeComponent();
            tableProvidePart = new TableProvidePart();
             sqlQueryPart = new SQL_Query_Part();
             db2DbPart = new ConveyDataToOtherDBPart();

        }
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            EnableDrawUpDownPattern = true;
            Img_Down = dotNetLab.UI.RibbonUnderwater;
            Img_Up = dotNetLab.UI.RibbonTreeRings;
            this.EnableDialog = true;
        }
        protected override void prepareEvents()
        {
            base.prepareEvents();
            this.MenuItemClicked += new HandleMenuClickedCallback(DBEngineEditorPage_MenuItemClicked);
        }
        void DBEngineEditorPage_MenuItemClicked(string MenuItemText)
        {
            switch (MenuItemText)
            {
                case "表查询": EnterTableQueryPart(); break;
                case "SQL查询": EnterSQLQueryPart(); break;
                case "数据交接": EnterDB2DBPart(); break;
            }
            this.Text = MenuItemText;
        }

        private void EnterDB2DBPart()
        {
            this.canvasPanel1.Controls.Clear();
            db2DbPart.dbInvoker = dbInvoker;
            db2DbPart.txb_DBName.Text = dbInvoker.Database;
            db2DbPart.dataBindingPropertyEditorDialog = (DataBindingPropertyEditorDialog)DataBindingPropertyEditorDialogObject;
            canvasPanel1.Controls.Add(db2DbPart);
        }

        void EnterTableQueryPart()
        {
            this.canvasPanel1.Controls.Clear();
            tableProvidePart.dbInvoker = dbInvoker;
            tableProvidePart.dataBindingPropertyEditorDialog = (DataBindingPropertyEditorDialog)DataBindingPropertyEditorDialogObject;
            canvasPanel1.Controls.Add(tableProvidePart);

        }
        void EnterSQLQueryPart()
        {
            this.canvasPanel1.Controls.Clear();
            sqlQueryPart.dbInvoker = dbInvoker;
            sqlQueryPart.dataBindingPropertyEditorDialog = (DataBindingPropertyEditorDialog)DataBindingPropertyEditorDialogObject;
            canvasPanel1.Controls.Add(sqlQueryPart);

        }
        private void InitializeComponent()
        {
            this.canvasPanel1 = new dotNetLab.Widgets.Container.CanvasPanel();
            this.SuspendLayout();
            // 
            // tipper
            // 
            this.tipper.Location = new System.Drawing.Point(445, 491);
            // 
            // canvasPanel1
            // 
            this.canvasPanel1.BackColor = System.Drawing.Color.Transparent;
            this.canvasPanel1.BorderColor = System.Drawing.Color.Empty;
            this.canvasPanel1.BorderThickness = -1;
            this.canvasPanel1.CornerAlignment = dotNetLab.Widgets.Alignments.All;
            this.canvasPanel1.DataBindingInfo = null;
            this.canvasPanel1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.canvasPanel1.ImagePos = new System.Drawing.Point(0, 0);
            this.canvasPanel1.ImageSize = new System.Drawing.Size(0, 0);
            this.canvasPanel1.Location = new System.Drawing.Point(124, 56);
            this.canvasPanel1.Name = "canvasPanel1";
            this.canvasPanel1.NormalColor = System.Drawing.Color.Transparent;
            this.canvasPanel1.Radius = -1;
            this.canvasPanel1.Size = new System.Drawing.Size(605, 508);
            this.canvasPanel1.Source = null;
            this.canvasPanel1.TabIndex = 2;
            this.canvasPanel1.Text = "canvasPanel1";
            this.canvasPanel1.UIElementBinders = null;
            this.MenuColor = Color.DodgerBlue;
            // 
            // DBEngineEditorPage
            // 
            this.ClientSize = new System.Drawing.Size(748, 576);
            this.Controls.Add(this.canvasPanel1);
            this.MenuItems.AddRange(new string[] {
            "表查询",
            "SQL查询",
            "Entity操作",
             "数据交接"
           });
            this.Name = "DBEngineEditorPage";
            this.TitlePos = new System.Drawing.Point(374, 2);
            this.Controls.SetChildIndex(this.canvasPanel1, 0);
            this.ResumeLayout(false);

        }
    }
}
