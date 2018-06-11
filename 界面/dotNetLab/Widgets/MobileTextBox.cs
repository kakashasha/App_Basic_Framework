using dotNetLab.Appearance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Widgets.Design;
using System.Windows.Forms.Design;
using dotNetLab.Widgets.UIBinding;

namespace dotNetLab.Widgets
{

    [ToolboxItem(true)]
     [Designer(typeof(MobileTextBoxDesigner)),ToolboxBitmap(typeof(TextBox))]
    public class MobileTextBox: UIElement
    {
        public enum TextBoxStyles
        {
            Mobile,Traditional
        }
        TextBoxStyles textBoxStyle = TextBoxStyles.Mobile;
        public System.Windows.Forms.TextBox txb;
        Pen Pen_Border;
        Color clrBorderActive = Color.Orange,
        clrBorderStatic = Color.DodgerBlue;

        
        bool bIsTxbClicked = false;
        bool bEnableNullValue = false;
        int nValue = 0;
        float fValue = 0;
        double lfValue = 0;
        private Pen pen_Line;
        private float fLineThickness = 2;
        int nRadius = -1;
        Color clrFill;
        protected SolidBrush sbr_Fill;
        bool bEnableRound = false;
        private bool bWhitePattern = false;
        bool bGreyPattern = false;
        GraphicsPath gp;
        [Category("外观")]
        public TextBoxStyles TextBoxStyle
        {
            get { return textBoxStyle; }
            set { textBoxStyle = value; Refresh(); }
        }
        [Category("外观")]
        public HorizontalAlignment TextAlignment
        {

            get { return txb.TextAlign; }
            set { txb.TextAlign = value; Refresh(); }
        }
        [BrowsableAttribute(true)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
       
        public override string Text
        {
            get
            {

                return txb.Text;
            }

            set
            {
                lock (this)
                {
                    txb.Text = value; 
                }
               
                Refresh();
            }
        }
        [BrowsableAttribute(true)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        public override Font Font
        {
            get { return txb.Font; }
            set
            {
                base.Font = value;
                txb.Font = value;
                txb.Location = new Point(txb.Location.X, txb.Location.Y + 1);
                this.Size = new System.Drawing.Size(this.Width, txb.Height + 8);
                Refresh();
            }
        }
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                base.ForeColor = value;
                if(txb != null)
                this.txb.ForeColor = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Color FillColor
        {
            get { return clrFill; }
            set
            {
                clrFill = value;
                if (clrFill == Color.Transparent)
                    ;
                //EnableMobileRound = false;
                else
                    EnableMobileRound = true;
                Invalidate();
            }
        }
        [Category("外观")]
        public float LineThickness
        {
            get { return fLineThickness; }
            set
            {
                fLineThickness = value;
                pen_Line.Width = value;
                Invalidate();
            }
        }
        [Category("外观")]
        public bool EnableMobileRound
        {
            get { return bEnableRound; }
            set
            {
                bEnableRound = value;
                if (value)
                {
                    nRadius = this.Height - 1;
                    StaticColor = Color.Gray;
                }
                else
                {
                    nRadius = -1;
                    StaticColor = Color.DodgerBlue;
                }

                this.Invalidate();
            }
        }

        [Category("外观")]
        public int Radius
        {
            get { return nRadius; }
            set
            {
                nRadius = value;
                Refresh();
            }
        }

        [Category("外观")]
        public Color TextBackColor
        {
            get { return txb.BackColor; }
            set { txb.BackColor = value; }
        }
        [Category("外观")]
        public virtual Color StaticColor
        {
            get { return clrBorderStatic; }
            set
            {
                clrBorderStatic = value;
                this.pen_Line.Color = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Color ActiveColor
        {
            get { return clrBorderActive; }
            set
            {
                clrBorderActive = value;
                Refresh();
            }
        }

        [BrowsableAttribute(false)]
        public double DoubleValue
        {
            get
            {
                try
                {
                    lfValue = Convert.ToDouble(txb.Text);
                    return lfValue;
                }
                catch
                {
                    return double.NaN;
                }
                
            }
            set { lfValue = value; }
        }
        [BrowsableAttribute(false)]
        public float FloatValue
        {
            get
            {
                try
                {
                    fValue = Convert.ToSingle(txb.Text);
                    return fValue;
                }
                catch
                {
                    return float.NaN; ;
                }
               
            }
            set { fValue = value; }
        }
        [BrowsableAttribute(false)]
        public int IntValue
        {
            get
            {
                try
                {
                    nValue = Convert.ToInt32(txb.Text);
                    return nValue;
                }
                catch
                {
                    return Int32.MinValue;
                }

                
            }
            set { nValue = value; }
        }
        [Category("外观")]
        public bool EnableNullValue
        {
            get
            {


                return bEnableNullValue;
            }

            set
            {
                bEnableNullValue = value;
            }
        }
        [Category("外观")]
        public bool WhitePattern
        {
            get { return bWhitePattern; }
            set
            {
                bWhitePattern = value;
                if (value)
                {
                    FillColor = Color.White;
                    StaticColor = ActiveColor = FillColor;
                }
                else if(! GreyPattern)
                {
                    FillColor = Color.Transparent;
                }
            }
        }

        [Category("外观")]
        public bool GreyPattern
        {
            get { return bGreyPattern; }
            set
            {
                bGreyPattern = value;
                if (value)
                {
                    FillColor = Color.Silver;
                    StaticColor = ActiveColor = FillColor;
                }
                else
                {
                    FillColor = Color.Transparent;
                }
            }
        }

        protected override void Prepare()
        {
            prepareAppearance();
            prepareCtrls();
            prepareEvents();
            UnitCtrls();
        }
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.ForeColor = Color.Black;
            this.Size = new Size(150, 30);

            Pen_Border = new Pen(clrBorderStatic, 1);
            //StaticColor = Color.DodgerBlue;
            //ActiveColor = Color.RoyalBlue;
            pen_Line = new Pen(StaticColor, fLineThickness);
            sbr_Fill = new SolidBrush(clrFill);
        }
        protected override void prepareCtrls()
        {
            txb = new System.Windows.Forms.TextBox();
            txb.Location = new Point(4, 2);
            txb.Size = new System.Drawing.Size(this.Width - 8, this.Height - 4);
            txb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txb.Font = new System.Drawing.Font(Fonts.YAHEI, 12);
            YaHeiFont(13);
        }
        protected override void UnitCtrls()
        {

            this.AddControl(txb);
            Text = null;
            txb.Text = null;
        }
        protected override void prepareEvents()
        {
            this.Resize += new EventHandler(EditText_Resize);
            txb.Click += new EventHandler(txb_Click);
            txb.LostFocus += new EventHandler(txb_LostFocus);
            txb.GotFocus += new EventHandler(txb_GotFocus);
            this.txb.TextChanged +=new EventHandler(MobileTextBox_TextChanged);
             
        }

        private void MobileTextBox_TextChanged(object sender, EventArgs e)
        {
            
            MainBindableProperty = Text;
        }

        void txb_GotFocus(object sender, EventArgs e)
        {
            bIsTxbClicked = true;
            this.pen_Line.Color = ActiveColor;
            Refresh();
        }

        void txb_LostFocus(object sender, EventArgs e)
        {
            bIsTxbClicked = false;
            Pen_Border.Color = clrBorderStatic;
            pen_Line.Color = StaticColor;
            Refresh();
        }

        void txb_Click(object sender, EventArgs e)
        {
            bIsTxbClicked = true;
            Pen_Border.Color = clrBorderActive;
            this.Refresh();
        }
        void ResizeMainPanel()
        {
            txb.Size = new System.Drawing.Size(this.Width - 8, this.Height - 4);
            //this.Size = new System.Drawing.Size(txb.Width + 4, txb.Height + 4);
           
            Refresh();
        }
        void EditText_Resize(object sender, EventArgs e)
        {
            ResizeMainPanel();
            if (gp != null)
                gp.Dispose();
            gp = null;

        }
        void DrawCircleRoundTextBox()
        {
            if (gp != null)
                gp.Dispose();
            gp = new GraphicsPath();
            if (bGreyPattern || bWhitePattern)
            {
                nRadius = this.Height - 1;
            }
            int radius = Radius;
            int height = this.Height - 1;
            int width = this.Width - 2;
            gp.AddArc(0, 0, radius, radius, 180, 90);
            gp.AddArc(width - radius, 0, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(0, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            this.txb.Width = this.Width - this.Height;
            int ncenterY = Height / 2 - txb.Height / 2;
            txb.Location = new Point(0 + Height / 2, ncenterY);

        }
        void MobileStyleTextBox(Graphics g)
        {

            
            if (Radius < 0)
            {
                int ncenterY = Height / 2 - txb.Height / 2;
                txb.Location = new Point(7, ncenterY);
                g.DrawLine(pen_Line, 0, this.Height - fLineThickness, this.Width, Height - fLineThickness);
            }
              
            else
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                DrawCircleRoundTextBox();
                if (FillColor != Color.Transparent)
                    txb.BackColor = FillColor;
                sbr_Fill.Color = FillColor;
                g.FillPath(sbr_Fill, gp);
                g.DrawPath(pen_Line, gp);

            }
        }
        void TraditionalTextBox(Graphics g)
        {

            int ncenterY = Height / 2 - txb.Height / 2;
            txb.Location = new Point(6, ncenterY);

            if (   bIsTxbClicked)
            {
                Pen_Border.Color = PressEffect(clrBorderActive, 70, true);
                //Pen_Border.Color = Color.DarkRed;
                g.DrawRectangle(Pen_Border, 0, 0, this.Width - 1, this.Height - 1);
                Pen_Border.Color = clrBorderActive;
                g.DrawRectangle(Pen_Border, 1, 1, this.Width - 3, this.Height - 3);
            }

            else
            {
                Pen_Border.Color = Color.GhostWhite;
                g.DrawRectangle(Pen_Border, 0, 0, this.Width - 1, this.Height - 1);
                Pen_Border.Color = clrBorderStatic;
                g.DrawRectangle(Pen_Border, 1, 1, this.Width - 3, this.Height - 3);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (textBoxStyle == TextBoxStyles.Mobile)
                MobileStyleTextBox(e.Graphics);
            else
                TraditionalTextBox(e.Graphics);
        }


    }

    internal class MobileTextBoxDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(MobileTextBoxDesignerActionList);
        }
    }

    internal class MobileTextBoxDesignerActionList : VisualActionList
    {
        private MobileTextBox txb;
        public MobileTextBoxDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            txb = this.TargetControl as MobileTextBox;
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            AddDesignerActionPropertyItem("DataBindingInfo", "接入数据中心");
            AddDesignerActionPropertyItem("TextBoxStyle", "文本框风格");
            AddDesignerActionHeaderItem(APPEARANCE);
            AddDesignerActionPropertyItem("Text","文本");
            AddDesignerActionPropertyItem("Font","字体");
            AddDesignerActionPropertyItem("TextAlignment", "文本排列方式");
            AddDesignerActionPropertyItem("ForeColor","字体颜色");
            AddDesignerActionPropertyItem("TextBackColor","字体背景颜色");
            AddDesignerActionPropertyItem("StaticColor","未获得焦点时颜色");
            AddDesignerActionPropertyItem("ActiveColor","获得焦点时颜色");
            AddDesignerActionPropertyItem("EnableMobileRound", "手机风格");
            AddDesignerActionPropertyItem("Radius", "圆角半径");
            AddDesignerActionPropertyItem("WhitePattern", "白色主题");
            AddDesignerActionPropertyItem("GreyPattern", "灰色主题");


        }
        public UIElementBinderInfo DataBindingInfo
        {
            get { return txb.DataBindingInfo; }
            set { AssignPropertyValue("DataBindingInfo", value); }
        }
        public HorizontalAlignment TextAlignment
        {
            get { return txb.TextAlignment; }
            set { AssignPropertyValue("TextAlignment", value); }

        }
        public MobileTextBox.TextBoxStyles TextBoxStyle
        {
            get { return txb.TextBoxStyle; }
            set { AssignPropertyValue("TextBoxStyle", value); }
        }
        public bool EnableMobileRound
        {
            get { return txb.EnableMobileRound; }
            set { AssignPropertyValue("EnableMobileRound", value); AssignPropertyValue("ActiveColor", Color.Cyan); }
        }
        public bool WhitePattern
        {
            get { return txb.WhitePattern; }
            set
            {
                AssignPropertyValue("WhitePattern", value);
            }
        }
        public bool GreyPattern
        {
            get { return txb.GreyPattern; }
            set
            {
                AssignPropertyValue("GreyPattern", value);
            }
        }

        public int Radius
        {
            get { return txb.Radius; }
            set { AssignPropertyValue("Radius", value); }
        }
        public Color TextBackColor
        {
            get { return txb.TextBackColor; }
            set
            {
                AssignPropertyValue("TextBackColor",value);
            }
        }
        public   string Text
        {
            get
            {

                return txb.Text;
            }

            set
            {
                AssignPropertyValue("Text",value);
                
               
            }
        }
       
        public   Font Font
        {
            get { return txb.Font; }
            set
            {
                AssignPropertyValue("Font",value);
            }
        }
        public   Color ForeColor
        {
            get
            {
                return txb.ForeColor;
            }

            set
            {
                AssignPropertyValue("ForeColor",value);
            }
        }
       
        public   Color StaticColor
        {
            get { return txb.StaticColor; }
            set
            {
                AssignPropertyValue("StaticColor",value);
            }
        }
        
        public Color ActiveColor
        {
            get { return txb.ActiveColor; }
            set
            {
                AssignPropertyValue("ActiveColor",value);
            }
        }
    }
}
