using dotNetLab.Appearance;
using dotNetLab.Widgets;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Forms
{
    public class MetroListPage : ModernPage
    {
        public delegate void HandleMenuClickedCallback(string MenuItemText);
        public event HandleMenuClickedCallback MenuItemClicked;
        private Control.ControlCollection lst_Flats;
        protected ItemList lst_DataSource;
        private int nItemWidth = 100;
        private int nItemHeight = 40;
        MobileButton flt_Previous;
       dotNetLab.Widgets.Container.ListContainer.CanvasControl ctrMenu;
        int nAddIndex = 0;
        Revert rvt;
        protected Font fnt_Item;
        Color clrMenuItemActived = Color.Transparent;
        protected override void prepareCtrls()
        {
            base.prepareCtrls();
            PrepareMenuContainer();
            PrepareRevert();
            lst_Flats = ctrMenu.Controls;

        }

        private void PrepareRevert()
        {
            this.rvt = new Revert();
            rvt.Size = new System.Drawing.Size(45, 45);
            rvt.MouseLeaveColor = Color.White;
            rvt.MouseEnterColor = Color.LightGray;
            rvt.MouseDownColor = Color.Gray;
            rvt.Location = new Point((ItemWidth - rvt.Width) / 2, 10);
        }

        void PrepareMenuContainer()
        {
            ctrMenu = new dotNetLab.Widgets.Container.ListContainer.CanvasControl();
            ctrMenu.BackColor = Color.FromArgb(150, Color.RoyalBlue);
            ctrMenu.Location = new Point(3, 3);
            ctrMenu.Size = new Size(ItemWidth - 2, this.Height - 6);
        }

        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.FontX = new Font(Fonts.YAHEI, 16);
            this.ForeColor = Color.RoyalBlue;
            fnt_Item = new Font(Fonts.YAHEI, 12);
             
            clrMenuItemActived = Color.CornflowerBlue;
            //  ItemFont = new Font(Fonts.YAHEI, 12);
            // this.Img_Down = UI.Underworking;
        }
        
        protected override void prepareEvents()
        {
            base.prepareEvents();
            this.Resize += MetroForm_Resize;
            this.rvt.Click += Rvt_Click;

        }

        private void Rvt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void DefineItemControl(MobileButton flt, string ItemText)
        {
            flt.Location = new Point(0, 70 + nAddIndex * ItemHeight);
            flt.Width = ItemWidth + 4;
            flt.Height = ItemHeight;
            flt.Text = ItemText;
            flt.Font = this.ItemFont;
            flt.NormalColor = Color.Transparent;
            flt.PressColor = flt.NormalColor;
            flt.Click += Flt_Click;
            nAddIndex++;

        }

        private void Flt_Click(object sender, EventArgs e)
        {
            MobileButton flt = sender as MobileButton;
            if (flt != flt_Previous)
                if (flt_Previous != null)
                    flt_Previous.NormalColor = Color.Transparent;
            flt.NormalColor = MenuItemActivedColor;
            flt.PressColor = flt.NormalColor;
            MenuItemClicked(flt.Text);
            flt_Previous = flt;

        }
        private void Lst_Source_ItemChanged(string ItemText, bool bAdd)
        {
            if (bAdd)
            {

                lst_Flats.Add(new MobileButton());
                DefineItemControl(lst_Flats[lst_Flats.Count - 1] as MobileButton, ItemText);

            }
            else
            {
                nAddIndex = 0;
                for (int i = 0; i < lst_Flats.Count; i++)
                {
                    lst_Flats.Remove(lst_Flats[i]);
                }

                for (int i = 0; i < lst_Flats.Count; i++)
                {
                    lst_Flats.Add(new MobileButton());
                    DefineItemControl(lst_Flats[lst_Flats.Count - 1] as MobileButton, ItemText);
                }

            }
            ctrMenu.Refresh();
        }
        void ChangeItemsSize()
        {
            for (int i = 0; i < lst_Flats.Count; i++)
            {
                lst_Flats[i].Size = new Size(ItemWidth, ItemHeight);
            }
            this.ctrMenu.Width = ItemWidth - 2;
            rvt.Size = new Size(45, 45);
            rvt.Location = new Point((ItemWidth - rvt.Width) / 2, 10);

            rvt.Refresh();
        }
        void ChangeItemFont()
        {
            lst_Flats = this.ctrMenu.Controls;
            for (int i = 0; i < lst_Flats.Count; i++)
            {
                lst_Flats[i].Font = ItemFont;
            }

        }
        protected override void prepareData()
        {
            base.prepareData();
            lst_DataSource = new ItemList();
            lst_DataSource.ItemChanged += Lst_Source_ItemChanged;

        }

        void CenterTitle()
        {
            Graphics g_Text = this.CreateGraphics();
            SizeF sz = g_Text.MeasureString(this.Text, this.FontX);
            pnt_TitlePos.X = (int)((this.Width - sz.Width) / 2);
            Refresh();
        }
        private void MetroForm_Resize(object sender, EventArgs e)
        {
            CenterTitle();
            ctrMenu.Size = new Size(ItemWidth + 4, this.Height - 6);
        }
        protected override void UnitCtrls()
        {
            base.UnitCtrls();
            this.Controls.Remove(DecoratedTriangle);
            this.pnt_TitlePos.Y = 2;
            Controls.Add(ctrMenu);
            ctrMenu.Controls.Add(rvt);
            ChangeItemFont();
          //  MenuColor = Color.DodgerBlue;
           // clrMenuItemActived = Color.CornflowerBlue;
        }
        public class ItemList : StringCollection
        {

            public delegate void ItemChangedCallback(string ItemText, bool bAdd);
            public event ItemChangedCallback ItemChanged;
            public new void Add(string item)
            {
                base.Add(item);
                ItemChanged(item, true);
            }
            public new void AddRange(string[] items)
            {
                base.AddRange(items);
                for (int i = 0; i < items.Length; i++)
                {
                    ItemChanged(items[i], true);
                }

            }
            public new void Clear()
            {
                base.Clear();
                ItemChanged("", false);
            }
            public new bool Remove(string item)
            {
                ItemChanged(item, false);
                base.Remove(item);
                return true;
            }
            public new void RemoveAt(int index)
            {
                ItemChanged(this[index], false);
                base.RemoveAt(index);
            }
        }
        [Category("数据")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design",
    "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public ItemList MenuItems
        {
            get
            {

                return lst_DataSource;
            }
        }
        [Category("外观")]
        public int ItemWidth
        {
            get
            {
                return nItemWidth;
            }

            set
            {
                nItemWidth = value;
                ChangeItemsSize();
                Refresh();
            }
        }
        [Category("外观")]
        public int ItemHeight
        {
            get
            {
                return nItemHeight;
            }

            set
            {
                nItemHeight = value;
                ChangeItemsSize();
                Refresh();
            }
        }
        [Category("外观")]
        public Color MenuColor
        {
            get
            {
                return ctrMenu.BackColor;
            }
            set
            {
                ctrMenu.BackColor = value;
                ctrMenu.Refresh();
            }
        }
        [Category("外观")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font ItemFont
        {
            set
            {
                fnt_Item = value;
                ChangeItemFont();
            }
            get
            {
                return fnt_Item;

            }
        }
        [Category("外观")]
        public Color MenuItemActivedColor { get { return clrMenuItemActived; } set {
                clrMenuItemActived = value;
                if (this.ctrMenu.Controls.Count > 0)
                {
                    lst_Flats = this.ctrMenu.Controls;
                    for (int i = 0; i < lst_Flats.Count; i++)
                    {
                        MobileButton flt = lst_Flats[i] as MobileButton;
                         if(flt!=null)
                        flt.PressColor = flt.NormalColor = value;
                    }
                }
                Invalidate(); } }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MetroListPage
            // 
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Name = "MetroListPage";
            this.ResumeLayout(false);

        }
    }

}
