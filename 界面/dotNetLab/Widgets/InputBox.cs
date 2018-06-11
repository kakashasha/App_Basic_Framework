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

namespace dotNetLab.Widgets
{

    [ToolboxItem(true)]
     [Designer(typeof(InputBoxDesigner)),ToolboxBitmap(typeof(TextBox))]
    public class InputBox: UIElement
    {
        public System.Windows.Forms.TextBox txb;
        Pen Pen_Border;
        Color clrBorderActive = Color.Orange,
        clrBorderStatic = Color.Gray;
        bool bIsTxbClicked = false;
        bool bEnableNullValue = false;
        int nValue = 0;
        float fValue = 0;
        double lfValue = 0;
        //GraphicsPath gp;
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
                txb.Text = value;
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
        }
        protected override void prepareEvents()
        {
            this.Resize += new EventHandler(EditText_Resize);
            txb.Click += new EventHandler(txb_Click);
            txb.LostFocus += new EventHandler(txb_LostFocus);
            txb.GotFocus += new EventHandler(txb_GotFocus);

        }

        void txb_GotFocus(object sender, EventArgs e)
        {
            bIsTxbClicked = true;
            Refresh();
        }

        void txb_LostFocus(object sender, EventArgs e)
        {
            bIsTxbClicked = false;
            Pen_Border.Color = clrBorderStatic;
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
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
             
            if (bIsTxbClicked)
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


    }

    internal class InputBoxDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(InputBoxDesignerActionList);
        }
    }

    internal class InputBoxDesignerActionList : VisualActionList
    {
        private InputBox txb;
        public InputBoxDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            txb = this.TargetControl as InputBox;
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            AddDesignerActionHeaderItem(APPEARANCE);
            AddDesignerActionPropertyItem("Text","文本");
            AddDesignerActionPropertyItem("Font","字体");
            AddDesignerActionPropertyItem("ForeColor","字体颜色");
            AddDesignerActionPropertyItem("TextBackColor","字体背景颜色");
            AddDesignerActionPropertyItem("StaticColor","未获得焦点时颜色");
            AddDesignerActionPropertyItem("ActiveColor","获得焦点时颜色");
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
