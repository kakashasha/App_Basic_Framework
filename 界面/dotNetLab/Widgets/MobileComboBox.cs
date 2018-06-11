using dotNetLab.Appearance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Animation;
using dotNetLab.Widgets.Design;
using System.Windows.Forms.Design;

namespace dotNetLab.Widgets
{
    [ToolboxItem(true)]
    [Designer(typeof(SpinnerDesigner)), ToolboxBitmap(typeof(ComboBox))]
    public class MobileComboBox : UIElement
    {
        public event EventHandler SelectedItemChanged;
        Color clrBorder = Color.Gray;
        Color clrText = Color.Black;
        int nHeight, nOrigionHeight;
        Point pnt_Origin_Location, pnt_Clicked;
        protected Animation.AnimationBase _animationBase;
        public SpinnerItem[] spn_Items;
        public String[] strItems;
        public String strSelectItem;
        int nSelectItem;
        int nSpns = 0;
        Pen pen_Border_Inner, pen_Border_External;
        int nPen_BorderWidth = 1;
        SpinnerItem spnitem;
        string strHeadItem;
        SpinnerItem spn_Item_Head;
        private bool bEnableAnimation = false;
        private int nDisplayItems = 0;
        float fValue_Before = 0;
      //  float f_Cum = 0;
        [Category("外观")]
        public bool EnableAnimation
        {
            get { return bEnableAnimation; }
            set { bEnableAnimation = value; }
        }

        [Category("外观")]
        public int DisplayItems
        {
            get { return nDisplayItems; }
            set { nDisplayItems = value; }
        }
        [Category("外观")]
        public String SelectedItem
        {
            get
            {
                if (spn_Items != null)
                    return spn_Items[0].Text;
                else
                    return null;
            }
            set
            {
                if (spn_Items!= null)
                spn_Items[0].Text = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Color BorderColor
        {
            get { return clrBorder; }
            set
            {
                clrBorder = value;
                Refresh();
            }
        }
        [Category("数据")]
        public string[]
        Items
        {
            get { return strItems; }
            set
            {
                strItems = value;
                if (strItems != null)
                {
                    nSpns = value.Length;
                    prepareViews();
                }
                Refresh();
            }
        }

      
        protected override void prepareAppearance()
        {
            this.BackColor = Color.White;
            pen_Border_Inner = new Pen(Color.DarkGray, nPen_BorderWidth);
            pen_Border_External = new Pen(Color.LightGray, nPen_BorderWidth);
            spn_Item_Head = new SpinnerItem();
            this.Width = 100;
            this.Height = spn_Item_Head.Height;
            this.pnt_Origin_Location = new Point();
            spn_Item_Head = new SpinnerItem();
            _animationBase = new AnimationBase(); 
            _animationBase.Connect(this,15,150);
            _animationBase.objects = new object[1];
            //是否处于打开状态
            _animationBase.objects[0] = true;
        }
     
 
        protected override void prepareEvents()
        {
            base.prepareEvents();
            this.Resize += Spinner_Resize;
 
        }
        void ItemsMove(int n, bool bdown)
        {
            if (bdown)
                for (int i = 0; i < nSpns; i++)
                {
                    spn_Items[i].Width = this.Width - 27;
                    spn_Items[i].Location = new Point(spn_Items[i].Location.X, spn_Items[i].Location.Y - n * spn_Items[0].Height);
                }
            else
            {
                for (int i = 0; i < nSpns; i++)
                {
                    spn_Items[i].Width = this.Width - 27;
                    spn_Items[i].Location = new Point(spn_Items[i].Location.X, spn_Items[i].Location.Y + n * spn_Items[0].Height);
                }
            }
        }
        private void Vbar_Seek(float fvalue)
        {
            float f = 1.0f / spn_Items.Length;
            if (fvalue - fValue_Before > 0)
            {
                int n = 0;
                if (fValue_Before != 0)
                {
                    n = (int)((fvalue - fValue_Before) / f);
                }
                else
                    n = (int)(fvalue / f);
                ItemsMove(n, true);
            }
            else if (fvalue - fValue_Before < 0)
            {
                int n = 0;
                if (fValue_Before != 0)
                {
                    n = (int)((fvalue - fValue_Before) / f);
                }
                else
                    n = (int)(fvalue / f);
                ItemsMove(n, false);
            }

            this.fValue_Before = fvalue;
        }

        private void Spinner_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < nSpns; i++)
            {
                spn_Items[i].Width = this.Width - 4;
            }
            Refresh();
        }

      
        private void prepareViews()
        {
            this.spn_Item_Head.Text = "";
            spn_Items = new SpinnerItem[nSpns];

            for (int i = 0; i < nSpns; i++)
            {

                spn_Items[i] = new SpinnerItem();
                nHeight = nSpns * spn_Items[0].Height + 4;
                spn_Items[i].Text = strItems[i];
                spn_Items[i].Location = new Point(2, i * spn_Items[i].Height + 2);
                spn_Items[i].Width = this.Width - 5;
                spn_Items[i].TextColor = clrText;
                spn_Items[i].Click += new EventHandler(SpinnerX_Click);
                spn_Items[i].nIndex = i;
                this.Controls.Add(spn_Items[i]);
            }

            this.Height = spn_Items[0].Height + 4;
            strHeadItem = spn_Items[0].Text;
            this.spn_Item_Head.Text = strHeadItem;
            spn_Item_Head.Refresh();
            spn_Items[0].Refresh();
        }
        
        void SpinnerX_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            spnitem = sender as SpinnerItem;
            
            strSelectItem = spnitem.Text;
            nSelectItem = spnitem.nIndex;
           
            if (nDisplayItems > 0)
                this.nHeight = nDisplayItems * spn_Items[0].Height;
                    
            if (this.Height < nHeight)
            {
                this.pnt_Origin_Location = this.Location;
                nOrigionHeight = this.Height;
                if (bEnableAnimation)
                {
                    _animationBase.objects[0] = true;
                    _animationBase.Play();
                }
                else
                {
                    Height = nHeight;
                   
                      
                    pnt_Clicked = new Point(this.Location.X,
                        this.Location.Y - nHeight / 2 + spnitem.Height);
                    if (pnt_Clicked.Y < 0 || pnt_Clicked.Y + nHeight > this.Parent.Height)
                    {
                        if (pnt_Clicked.Y < 0)
                        {
                            pnt_Clicked = new Point(this.Location.X, 4);
                        }
                        if (pnt_Clicked.Y + nHeight > this.Parent.Height)
                        {
                            int nGap = pnt_Clicked.Y + nHeight - this.Parent.Height;
                            pnt_Clicked = new Point(this.Location.X, pnt_Clicked.Y -nGap);
                        }
                    }
                    this.Location = pnt_Clicked;
                    spn_Items[0].Text = strHeadItem;
                    pen_Border_Inner.Color = Color.DodgerBlue;
                    spn_Items[0].TextColor = Color.Blue;
                }
            }
            else
            {
             //Close 
                if (bEnableAnimation)
                {
                    _animationBase.objects[0] = false;
                    _animationBase.Play();
                }
                else
                {
                    this.Height = nOrigionHeight;
                    this.Location = this.pnt_Origin_Location;
                    spn_Items[0].Text = spn_Items[nSelectItem].Text;
                    pen_Border_Inner.Color = clrBorder;
                    spn_Items[0].TextColor = clrText;
                }

            }
            if (SelectedItemChanged != null)
                this.SelectedItemChanged(sender, e);
            Refresh();
        }

        protected override void InvokeAnimation( )
        {
            bool b =Convert.ToBoolean(_animationBase.objects[0]);
            //opening
            if (b)
            {
               
                int n = nHeight / _animationBase.nTimes;
                this.Location = new Point(this.Location.X,this.Location.Y-n/8);
                this.Height += n;
                if (Height >= nHeight)
                {
                    Height = nHeight;
                    this._animationBase.Stop();
                     //this.pnt_Origin_Location = this.Location;
                  /*  pnt_Clicked = new Point(this.Location.X,
                        this.Location.Y - nHeight / 2 + spnitem.Height);*/
                    pnt_Clicked = new Point(this.Location.X,
                         nHeight / 2 - spnitem.Height/2  );
                    this.Location = pnt_Clicked;
                    spn_Items[0].Text = strHeadItem;
                    pen_Border_Inner.Color = Color.DodgerBlue;
                    spn_Items[0].TextColor = Color.Blue;
                }
            }
            else
            {
                int n = nHeight / _animationBase.nTimes;
                this.Location = new Point(this.Location.X,this.Location.Y+n/4);
                this.Height -= n;
           
                if (Height <= nOrigionHeight)
                {
                    this._animationBase.Stop();
                    this.Height = nOrigionHeight;
                    this.Location = this.pnt_Origin_Location;
                    spn_Items[0].Text = spn_Items[nSelectItem].Text;
                    pen_Border_Inner.Color = clrBorder;
                    spn_Items[0].TextColor = clrText;
                }
            }
            
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // e.Graphics.FillRectangle(Brushes.White, 2, 2, this.Width - 4, this.Height - 4);
            g.DrawRectangle(pen_Border_Inner, 1, 1, this.Width - 3, this.Height - 3);
            g.DrawRectangle(pen_Border_External, 0, 0, this.Width - 1, this.Height - 1);
        }
        [ToolboxItem(false)]
        public class SpinnerItem : System.Windows.Forms.UserControl
        {
            Color clrFocus = Color.DodgerBlue,
                clrText = Color.Black;

            public int nIndex = -1;
            Font fnt_Text;
            bool bCenter = false;
            SizeF strSizeF;
            Graphics g;

            string strCaption;
            int nHeight = 0, nWidth = 0;
            [Browsable(true)]
           [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public override string Text
            {
                get { return base.Text; }
                set
                {
                    strCaption = value;
                    CenterTextHeight();
                    base.Text = value;
                    Refresh();
                }
            }
            public Font Fnt_Text
            {
                get { return fnt_Text; }
                set
                {
                    fnt_Text = value;
                    CenterTextHeight();
                    Refresh();
                }
            }
            SolidBrush sldbrhText;
            SolidBrush sldbrhBkg;
            public Color TextColor
            {
                get { return clrText; }
                set
                {
                    clrText = value;
                    sldbrhText.Color = value;
                    Refresh();
                }
            }
            public SpinnerItem()
            {
                this.BringToFront();
                prepareData();
                prepareEvent();
            }
            private void prepareData()
            {
                fnt_Text = new Font("微软雅黑", 13);
                this.Size = new Size(150, 20);
                this.BackColor = Color.Transparent;
                sldbrhText = new SolidBrush(clrText);
                sldbrhBkg = new SolidBrush(Color.Transparent);
                this.Text = "Item";
            }
            private void prepareEvent()
            {
                this.Paint += SpinnerItem_Paint;
                this.MouseDown += SpinnerItem_MouseDown;
                this.MouseUp += SpinnerItem_MouseUp;
                this.Resize += SpinnerItem_Resize;
            }
            void CenterTextHeight()
            {
                Graphics g = this.CreateGraphics();
                strSizeF = g.MeasureString(strCaption, fnt_Text);
                nHeight = (int)(this.Height / 2.0 - strSizeF.Height / 2.0);
                nWidth = (int)(this.Width / 2.0 - strSizeF.Width / 2.0);
                this.Size = new Size(
                   this.Width
                    ,
                    (int)strSizeF.Height + 6);
            }
            void SpinnerItem_Resize(object sender, EventArgs e)
            {
                CenterTextHeight();
                Refresh();
            }

            void SpinnerItem_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                sldbrhText.Color = clrText;
                sldbrhBkg.Color = Color.Transparent;
                Refresh();
            }

            void SpinnerItem_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                sldbrhText.Color = Color.White;
                sldbrhBkg.Color = clrFocus;
                Refresh();
            }

            void SpinnerItem_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
            {
                g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.FillRectangle(sldbrhBkg, 0, 0, this.Width - 1, this.Height - 1);
                if (!bCenter)
                {

                    g.DrawString(strCaption, fnt_Text, sldbrhText, nWidth, nHeight);
                }
                else
                {

                }
            }
        }
    }

    internal class SpinnerDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {

            type_ActionList = typeof(SpinnerDesignerActionList);
        }
    }

    internal class SpinnerDesignerActionList : VisualActionList
    {
        private MobileComboBox spn = null;
        public SpinnerDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            spn = TargetControl as MobileComboBox;
            
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
             AddDesignerActionHeaderItem("数据");
            AddDesignerActionPropertyItem("Items","下拉列表数据");
        }
        
        public string[] Items
        {
            get { return spn.Items; }
            set
            {
                 AssignPropertyValue("Items",value);
            }
        }
    }
}
