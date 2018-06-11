using dotNetLab.Appearance;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Widgets;
using System.ComponentModel;
using dotNetLab.Widgets.UIBinding;

namespace dotNetLab.Forms
{
    public class Wnd : Form
    {
        Point mPoint_Drag = new Point();
        
        protected MobileButton clickDownMenu;
        bool bModernUI = true;
        Object UIElementBinderObject;
        [Browsable(false)]
        public Object UIElementBinders
        {
            get { return UIElementBinderObject; }
            set { UIElementBinderObject = value; }

        }
        public Wnd(bool bModernUI)
        {

            if (bModernUI)
            {
                this.bModernUI = bModernUI;
                PrepareModernUI();
            }

            //  PrepareClickDownMenu();
            Prepare();
        }

        //private void PrepareClickDownMenu()
        //{
        //    clickDownMenu = new Flat();
        //    clickDownMenu.Text = null;
        //    clickDownMenu.PressColor=clickDownMenu.NormalColor = Color.DarkGray;
        //    clickDownMenu.Radius = 6;
        //    clickDownMenu.Visible = false;
           
            
        //    AddClickDownMenuItem("最近提示消息", 170, 20, 0, null, true);
        //   // flt.Click +=  onClickShowLastRecordMenuItem;
        //}

        public Wnd()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            FindAllBindableCtrls(this);
        }
        void FindAllBindableCtrls(Control c)
        {
            Control.ControlCollection arr = c.Controls;
            foreach (Control item in arr)
            {
                UIElement element = item as UIElement;
                    if(element != null)
                {
                    if (element.DataBindingInfo != null)
                    {
                        UIElementDataBinding XBinders = (UIElementBinders as UIElementDataBinding);
                        if(XBinders.BindedCtrls.IndexOf(item)==-1)
                      XBinders.AddBindItem(element.DataBindingInfo);
                    }

                    else
                    {
                        FindAllBindableCtrls(item);
                    }
                }
            }
        }
        protected virtual void Prepare()
        {

            prepareData();
            prepareAppearance();
            prepareCtrls();
            UnitCtrls();
            prepareEvents();
        }
        protected void PrepareModernUI()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MouseDown += this.Me_MouseDown;
            this.MouseMove += this.Me_MouseMove;
        }
        
        public void AddControl(Control ctrl)
        {
            this.Controls.Add(ctrl);
        }
        protected virtual void prepareEvents() {   }

        //protected override void OnMouseUp(MouseEventArgs e)
        //{
        //    base.OnMouseUp(e);
        //    if (e.Button.Equals(MouseButtons.Right))
        //    {
               

        //        clickDownMenu.Location = new Point(e.X, e.Y);
                
        //        clickDownMenu.Visible = true;
        //        BringToFront();

        //    }
        //    else if(e.Button.Equals(MouseButtons.Left))
        //        clickDownMenu.Visible = false;
        //}

        protected virtual void prepareAppearance() { }
        protected virtual void prepareData() { }
        protected virtual void prepareCtrls() { }
        
        protected virtual void UnitCtrls() { }
         
        public void YaHeiFont(int nFontSize)
        {
            this.Font = new Font(Fonts.YAHEI, nFontSize);
        }
        protected void Me_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint_Drag.X = e.X;
            mPoint_Drag.Y = e.Y;
        }
        protected void Me_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint_Drag.X, -mPoint_Drag.Y);
                Location = myPosittion;
            }
        }


    }
    [ToolboxItem(false)]
    public class Switcher : UserControl
    {
        private bool _EnableEx = false;

        private int _Switch_Feature = 0;

        private float _CirclePenSize = 2f;

        private float _InnerPenSize = 2f;

        private Color _CircleColor = Color.Black;

        private Color _InnerColor = Color.Red;

        private PointF Point_CircleLocation;

        private PointF Point_Left_Up;

        private PointF Point_Left_Down;

        private PointF Point_Right_Up;

        private PointF Point_Right_Down;

        private Pen _Pen_Circle;

        private Pen _Pen_Inner;

        public Color MouseUpColor = Color.Transparent;

        public Color MouseLeaveColor = Color.Transparent;

        public Color MouseDownColor = Color.DarkGray;

        public Color MouseEnterColor = Color.LightGray;

        private float Radius = 0f;

        public Color InnerPen_Down_Enter = Color.White;

        public Color InnerPen_Leave = Color.Black;

        public float fOffset_X = 1f;

        public float fOffset_Y = 4f;

        [Category("个性化")]
        public bool EnableEx
        {
            get
            {
                return this._EnableEx;
            }
            set
            {
                this._EnableEx = value;
                this.Refresh();
            }
        }

        [Category("个性化")]
        public int Switch_Feature
        {
            get
            {
                return this._Switch_Feature;
            }
            set
            {
                this._Switch_Feature = value;
                this.Refresh();
            }
        }

        [Category("个性化")]
        public float CirclePenSize
        {
            get
            {
                return this._CirclePenSize;
            }
            set
            {
                this._CirclePenSize = value;
                this._Pen_Circle.Width = this._CirclePenSize;
            }
        }

        [Category("个性化")]
        public float InnerPenSize
        {
            get
            {
                return this._InnerPenSize;
            }
            set
            {
                this._InnerPenSize = value;
                this._Pen_Inner.Width = value;
            }
        }

        [Category("个性化")]
        public Color CircleColor
        {
            get
            {
                return this._CircleColor;
            }
            set
            {
                this._CircleColor = value;
                this._Pen_Circle.Color = value;
            }
        }

        [Category("个性化")]
        public Color InnerColor
        {
            get
            {
                return this._InnerColor;
            }
            set
            {
                this._InnerColor = value;
                this._Pen_Inner.Color = value;
                this.InnerPen_Leave = value;
            }
        }

        public Switcher()
        {
            this.BackColor = Color.Transparent;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this._Pen_Circle = new Pen(this._CircleColor, this._CirclePenSize);
            this._Pen_Inner = new Pen(this._InnerColor, this._InnerPenSize);
            base.Size = new Size(20, 20);
            this.Point_CircleLocation = default(PointF);
            this.Point_Left_Down = default(PointF);
            this.Point_Left_Up = default(PointF);
            this.Point_Right_Down = default(PointF);
            this.Point_Right_Up = default(PointF);
        }

        private void DrawCross(Graphics Canvas)
        {
            this.Point_Left_Up.X = 0.6f * (float)base.Width / 2f;
            this.Point_Left_Up.Y = this.Point_Left_Up.X;
            this.Point_Right_Down.X = 0.7f * (float)base.Width;
            this.Point_Right_Down.Y = this.Point_Right_Down.X;
            this.Point_Left_Down.X = this.Point_Left_Up.X;
            this.Point_Left_Down.Y = this.Point_Right_Down.Y;
            this.Point_Right_Up.X = this.Point_Left_Down.Y;
            this.Point_Right_Up.Y = this.Point_Left_Down.X;
            Canvas.DrawLine(this._Pen_Inner, this.Point_Left_Up, this.Point_Right_Down);
            Canvas.DrawLine(this._Pen_Inner, this.Point_Left_Down, this.Point_Right_Up);
        }

        private void DrawMini(Graphics Canvas)
        {
            this.Point_Left_Up.X = (float)(base.Width / 4);
            this.Point_Left_Up.Y = (float)(base.Height / 2 + base.Height / 8);
            this.Point_Right_Up.X = 0.75f * (float)base.Width;
            this.Point_Right_Up.Y = this.Point_Left_Up.Y;
            Canvas.SmoothingMode = SmoothingMode.HighQuality;
            Canvas.DrawLine(this._Pen_Inner, this.Point_Left_Up, this.Point_Right_Up);
        }

        private void DrawMax(Graphics Canvas)
        {
            this.Point_Left_Up.X = (float)base.Width / 4f + this.fOffset_X;
            this.Point_Left_Up.Y = (float)base.Height / 6f + this.fOffset_Y;
            this.Point_Right_Up.X = this.Point_Left_Up.X + (float)base.Width / 2f;
            this.Point_Right_Up.Y = this.Point_Left_Up.Y;
            this.Point_Left_Down.X = (float)base.Width / 4f;
            this.Point_Left_Down.Y = (float)base.Width - (float)base.Width / 6f - this.fOffset_Y;
            this.Point_Right_Down.X = this.Point_Right_Up.X;
            this.Point_Right_Down.Y = this.Point_Left_Down.Y;
            Canvas.DrawRectangle(this._Pen_Inner, this.Point_Left_Up.X, this.Point_Left_Up.Y, this.Point_Right_Up.X - this.Point_Left_Up.X, this.Point_Left_Down.Y - this.Point_Left_Up.Y);
            Canvas.DrawLine(this._Pen_Inner, this.Point_Left_Up.X, this.Point_Left_Up.Y + 1f, this.Point_Right_Up.X, this.Point_Right_Up.Y + 1f);
            Canvas.DrawLine(this._Pen_Inner, this.Point_Left_Up.X, this.Point_Left_Up.Y + 2f, this.Point_Right_Up.X, this.Point_Right_Up.Y + 2f);
        }

        private void DrawCommonPart(Graphics Canvas)
        {
            float temp = 10f;
            this.Point_CircleLocation.X = 0.2f * (float)base.Width / 2f;
            this.Point_CircleLocation.Y = 0.2f * (float)base.Width / 2f;
            this.Radius = 8f / temp * (float)base.Width / 2f;
            Canvas.DrawEllipse(this._Pen_Circle, this.Point_CircleLocation.X, this.Point_CircleLocation.Y, this.Radius * 2f, this.Radius * 2f);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
             
            this.DrawCommonPart(e.Graphics);
            switch (this._Switch_Feature)
            {
                case 0:
                    this.DrawMini(e.Graphics);
                    break;
                case 1:
                    this.DrawMax(e.Graphics);
                    break;
                case 2:
                    this.DrawCross(e.Graphics);
                    break;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.OnMouseEnter(e);
            this.Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            bool flag = !this._EnableEx;
            if (flag)
            {
                this.BackColor = this.MouseDownColor;
            }
            else
            {
                this._Pen_Inner.Color = this.MouseDownColor;
            }
            this.Refresh();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            bool flag = !this._EnableEx;
            if (flag)
            {
                this.BackColor = this.MouseEnterColor;
                this._Pen_Inner.Color = this.InnerPen_Down_Enter;
            }
            else
            {
                this._Pen_Inner.Color = this.MouseEnterColor;
            }
            this.Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            bool flag = !this._EnableEx;
            if (flag)
            {
                this.BackColor = this.MouseLeaveColor;
                this._Pen_Inner.Color = this.InnerPen_Leave;
            }
            else
            {
                this._Pen_Inner.Color = this.MouseLeaveColor;
            }
            this.Refresh();
        }
    }
}
/*
 //未写完
        public Flat AddClickDownMenuItem(String str,int nWidth,int nHeight,int nGap,Image img, bool isLeft)
        {
            Flat flt = new Flat();
            flt.Text = str;
            flt.NormalColor = flt.PressColor = Color.Transparent;
            
            int X = 1;
            int Y = -1;
            if (clickDownMenu.Controls.Count == 0)
            {
                X = clickDownMenu.Radius + 1;
                Y = clickDownMenu.Radius + 1;
            }
            else
            {
                X = clickDownMenu.Controls[0].Location.X;
                int lastChildIndex = clickDownMenu.Controls.Count-1;
                Control ctrl  = clickDownMenu.Controls[lastChildIndex];
                Y =ctrl.Location.Y + ctrl.Height + nGap;
            }
            flt.Location = new Point(X,Y);
            flt.Size = new Size(clickDownMenu.Size.Width-2*X,nHeight);
            flt.Source = img;
            clickDownMenu.Width = flt.Width + 2 * clickDownMenu.Radius;
            clickDownMenu.Height = flt.Location.Y + flt.Height + clickDownMenu.Radius;
            clickDownMenu.AddControl(flt);
            return flt;
        }
     
     */
