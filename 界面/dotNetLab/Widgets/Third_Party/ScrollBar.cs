
using dotNetLab.Widgets.Third_Party;
using MetroFramework.Native;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace dotNetLab.Widgets.Third_Party
{
   

    [DefaultEvent("Scroll"), DefaultProperty("Value"), 
        Designer(typeof(MetroScrollBarDesigner)), ToolboxBitmap(typeof(VScrollBar))]
    public class MobileScrollBar : Control 
    {
        public enum MetroScrollOrientation
        {
            Horizontal,
            Vertical
        }
        public enum ScrollDirection
        {
            Left,Right,Top,Down
        }
        public ScrollDirection scrollDirection =  ScrollDirection.Down; 
        Color clrBottomColor = Color.Silver;
        public delegate void ScrollValueChangedDelegate(object sender, int nDelta,int newValue, ScrollDirection direction);
        public event ScrollValueChangedDelegate ValueChanged;
        int nLastValue = 0;

        private bool isFirstScrollEventVertical = true;

        private bool isFirstScrollEventHorizontal = true;

        private bool inUpdate;

        private Rectangle clickedBarRectangle;
        private Color clrNormalThumb = Color.DodgerBlue;

        private Color clrPressThumb = Color.RoyalBlue;

      
        private Rectangle thumbRectangle;
        private bool topBarClicked;
        private bool bottomBarClicked;
        private bool thumbClicked;
        private int thumbWidth = 6;
        private int thumbHeight;
        private int thumbBottomLimitBottom;
        private int thumbBottomLimitTop;
        private int thumbTopLimit;
        private int thumbPosition;
        private int trackPosition;
        private int mouseWheelBarPartitions = 10;
        private bool isPressed;
        private MetroScrollOrientation metroOrientation = MetroScrollOrientation.Vertical;

        private ScrollOrientation scrollOrientation = ScrollOrientation.VerticalScroll;

        private int minimum;

        private int maximum = 100;

        private int smallChange = 1;

        private int largeChange = 10;

        private int curValue;

        private bool dontUpdateColor;
        public int MouseWheelBarPartitions
        {
            get
            {
                return this.mouseWheelBarPartitions;
            }
            set
            {
                if (value > 0)
                {
                    this.mouseWheelBarPartitions = value;
                    return;
                }
                throw new ArgumentOutOfRangeException("value", "MouseWheelBarPartitions has to be greather than zero");
            }
        }
     
        public int ScrollbarSize
        {
            get
            {
                if (this.Orientation != MetroScrollOrientation.Vertical)
                {
                    return base.Height;
                }
                return base.Width;
            }
            set
            {
                if (this.Orientation == MetroScrollOrientation.Vertical)
                {
                    base.Width = value;
                    return;
                }
                base.Height = value;
            }
        }
        public MetroScrollOrientation Orientation
        {
            get
            {
                return this.metroOrientation;
            }
            set
            {
                if (value == this.metroOrientation)
                {
                    return;
                }
                this.metroOrientation = value;
                if (value == MetroScrollOrientation.Vertical)
                {
                    this.scrollOrientation = ScrollOrientation.VerticalScroll;
                }
                else
                {
                    this.scrollOrientation = ScrollOrientation.HorizontalScroll;
                }
                base.Size = new Size(base.Height, base.Width);
                this.SetupScrollBar();
            }
        }

        public int Minimum
        {
            get
            {
                return this.minimum;
            }
            set
            {
                if (this.minimum == value || value < 0 || value >= this.maximum)
                {
                    return;
                }
                this.minimum = value;
                if (this.curValue < value)
                {
                    this.curValue = value;
                }
                if (this.largeChange > this.maximum - this.minimum)
                {
                    this.largeChange = this.maximum - this.minimum;
                }
                this.SetupScrollBar();
                if (this.curValue < value)
                {
                    this.dontUpdateColor = true;
                    this.Value = value;
                    return;
                }
                this.ChangeThumbPosition(this.GetThumbPosition());
                this.Refresh();
            }
        }

        public int Maximum
        {
            get
            {
                return this.maximum;
            }
            set
            {
                if (value == this.maximum || value < 1 || value <= this.minimum)
                {
                    return;
                }
                this.maximum = value;
                if (this.largeChange > this.maximum - this.minimum)
                {
                    this.largeChange = this.maximum - this.minimum;
                }
                this.SetupScrollBar();
                if (this.curValue > value)
                {
                    this.dontUpdateColor = true;
                    this.Value = this.maximum;
                    return;
                }
                this.ChangeThumbPosition(this.GetThumbPosition());
                this.Refresh();
            }
        }

        [DefaultValue(1)]
        public int SmallChange
        {
            get
            {
                return this.smallChange;
            }
            set
            {
                if (value == this.smallChange || value < 1 || value >= this.largeChange)
                {
                    return;
                }
                this.smallChange = value;
                this.SetupScrollBar();
            }
        }

        [DefaultValue(5)]
        public int LargeChange
        {
            get
            {
                return this.largeChange;
            }
            set
            {
                if (value == this.largeChange || value < this.smallChange || value < 2)
                {
                    return;
                }
                if (value > this.maximum - this.minimum)
                {
                    this.largeChange = this.maximum - this.minimum;
                }
                else
                {
                    this.largeChange = value;
                }
                this.SetupScrollBar();
            }
        }

        [Browsable(true), DefaultValue(0)]
        public int Value
        {
            get
            {
                return this.curValue;
            }
            set
            {
                if (this.curValue == value || value < this.minimum || value > this.maximum)
                {
                    return;
                }
                this.curValue = value;
                this.ChangeThumbPosition(this.GetThumbPosition());
                this.OnScroll(ScrollEventType.ThumbPosition, -1, value, this.scrollOrientation);
                if (!this.dontUpdateColor  )
                {
                    ;
                     
                }
                else
                {
                    this.dontUpdateColor = false;
                }
                if (ValueChanged != null)
                {
                    if(metroOrientation== MetroScrollOrientation.Vertical)
                    {
                        if(value -nLastValue >0)
                      
                            scrollDirection = ScrollDirection.Down;
                       
                        else
                            scrollDirection = ScrollDirection.Top;
                    }
                    else
                    {
                        if (value - nLastValue > 0)

                            scrollDirection = ScrollDirection.Right;

                        else
                            scrollDirection = ScrollDirection.Left;
                    }
                    if (value - nLastValue != 0)
                    {
                        if(ValueChanged != null)
                        {

                        
                        if(value != 100)
                        ValueChanged(this, Math.Abs(value - nLastValue), value, scrollDirection);
                        else
                            ValueChanged(this, 0, value, scrollDirection);
                        }
                    }
                }
                this.nLastValue = value;
                this.Refresh();
            }
        }
        
        public Color  BottomColor { get { return clrBottomColor; }  set { clrBottomColor = value;Invalidate(); } }

        private void OnScroll(ScrollEventType type, int oldValue, int newValue, ScrollOrientation orientation)
        {
            
            if (orientation == ScrollOrientation.HorizontalScroll)
            {
                if (type != ScrollEventType.EndScroll && this.isFirstScrollEventHorizontal)
                {
                    type = ScrollEventType.First;
                }
                else if (!this.isFirstScrollEventHorizontal && type == ScrollEventType.EndScroll)
                {
                    this.isFirstScrollEventHorizontal = true;
                }
            }
            else if (type != ScrollEventType.EndScroll && this.isFirstScrollEventVertical)
            {
                type = ScrollEventType.First;
            }
            else if (!this.isFirstScrollEventHorizontal && type == ScrollEventType.EndScroll)
            {
                this.isFirstScrollEventVertical = true;
            }

            if (metroOrientation == MetroScrollOrientation.Vertical)
            {
                if (Value - nLastValue > 0)

                    scrollDirection = ScrollDirection.Down;

                else
                    scrollDirection = ScrollDirection.Top;
            }
            else
            {
                if (Value - nLastValue > 0)

                    scrollDirection = ScrollDirection.Right;

                else
                    scrollDirection = ScrollDirection.Left;
            }
            if (Value - nLastValue != 0)
            {
                if (ValueChanged != null)
                {


                    if (Value != 100)
                        ValueChanged(this, Math.Abs(Value - nLastValue), Value, scrollDirection);
                    else
                        ValueChanged(this, 0, Value, scrollDirection);
                }
            }

            this.nLastValue = Value;


        }

        public MobileScrollBar()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            base.Width = 10;
            base.Height = 200;
            
           
            this.SetupScrollBar();
            
        }

        public MobileScrollBar(MetroScrollOrientation orientation) : this()
        {
            this.Orientation = orientation;
        }

        public MobileScrollBar(MetroScrollOrientation orientation, int width) : this(orientation)
        {
            base.Width = width;
        }

        public bool HitTest(Point point)
        {
            return this.thumbRectangle.Contains(point);
        }

        [SecuritySafeCritical]
        public void BeginUpdate()
        {
            WinApi.SendMessage(base.Handle, 11, false, 0);
            this.inUpdate = true;
        }

        [SecuritySafeCritical]
        public void EndUpdate()
        {
            WinApi.SendMessage(base.Handle, 11, true, 0);
            this.inUpdate = false;
            this.SetupScrollBar();
            this.Refresh();
        }

        

        protected override void OnPaint(PaintEventArgs e)
        {
            Color clrThumb = Color.Transparent;
            if (!this.isPressed && base.Enabled)
            {
                clrThumb = NormalThumbColor;

            }
            else if (this.isPressed && base.Enabled)
            {
                clrThumb = PressThumbColor;

            }

            // bottom Color
            using (SolidBrush sbr_Bottom = new SolidBrush(clrBottomColor))
            {
                e.Graphics.FillRectangle(sbr_Bottom, base.ClientRectangle);
            }

            using (SolidBrush solidBrush3 = new SolidBrush(clrThumb))
            {
                e.Graphics.FillRectangle(solidBrush3, this.thumbRectangle);
            }
        }

        protected override void OnLeave(EventArgs e)
        {
          
            this.isPressed = false;
            base.Invalidate();
            base.OnLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int num = e.Delta / 120 * (this.maximum - this.minimum) / this.mouseWheelBarPartitions;
            if (this.Orientation == MetroScrollOrientation.Vertical)
            {
                this.Value -= num;
                return;
            }
            this.Value += num;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.isPressed = true;
                base.Invalidate();
            }
            base.OnMouseDown(e);
            base.Focus();
            if (e.Button != MouseButtons.Left)
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.trackPosition = ((this.metroOrientation == MetroScrollOrientation.Vertical) ? e.Y : e.X);
                }
                return;
            }
            Point location = e.Location;
            if (this.thumbRectangle.Contains(location))
            {
                this.thumbClicked = true;
                this.thumbPosition = ((this.metroOrientation == MetroScrollOrientation.Vertical) ? (location.Y - this.thumbRectangle.Y) : (location.X - this.thumbRectangle.X));
                base.Invalidate(this.thumbRectangle);
                return;
            }
            this.trackPosition = ((this.metroOrientation == MetroScrollOrientation.Vertical) ? location.Y : location.X);
            if (this.trackPosition < ((this.metroOrientation == MetroScrollOrientation.Vertical) ? this.thumbRectangle.Y : this.thumbRectangle.X))
            {
                this.topBarClicked = true;
            }
            else
            {
                this.bottomBarClicked = true;
            }
            this.ProgressThumb(true);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.isPressed = false;
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                if (this.thumbClicked)
                {
                    this.thumbClicked = false;
                    this.OnScroll(ScrollEventType.EndScroll, -1, this.curValue, this.scrollOrientation);
                }
                else if (this.topBarClicked)
                {
                    this.topBarClicked = false;
                    
                }
                else if (this.bottomBarClicked)
                {
                    this.bottomBarClicked = false;
                    
                }
                base.Invalidate();
            }
        }

     

      

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                if (this.thumbClicked)
                {
                    int num = this.curValue;
                    int num2 = (this.metroOrientation == MetroScrollOrientation.Vertical) ? e.Location.Y : e.Location.X;
                    int num3 = (this.metroOrientation == MetroScrollOrientation.Vertical) ? (num2 / base.Height / this.thumbHeight) : (num2 / base.Width / this.thumbWidth);
                    if (num2 <= this.thumbTopLimit + this.thumbPosition)
                    {
                        this.ChangeThumbPosition(this.thumbTopLimit);
                        this.curValue = this.minimum;
                        base.Invalidate();
                    }
                    else if (num2 >= this.thumbBottomLimitTop + this.thumbPosition)
                    {
                        this.ChangeThumbPosition(this.thumbBottomLimitTop);
                        this.curValue = this.maximum;
                        base.Invalidate();
                    }
                    else
                    {
                        this.ChangeThumbPosition(num2 - this.thumbPosition);
                        int num4;
                        int num5;
                        if (this.Orientation == MetroScrollOrientation.Vertical)
                        {
                            num4 = base.Height - num3;
                            num5 = this.thumbRectangle.Y;
                        }
                        else
                        {
                            num4 = base.Width - num3;
                            num5 = this.thumbRectangle.X;
                        }
                        float num6 = 0f;
                        if (num4 != 0)
                        {
                            num6 = (float)num5 / (float)num4;
                        }
                        this.curValue = Convert.ToInt32(num6 * (float)(this.maximum - this.minimum) + (float)this.minimum);
                    }
                    if (num != this.curValue)
                    {
                        this.OnScroll(ScrollEventType.ThumbTrack, num, this.curValue, this.scrollOrientation);
                        this.Refresh();
                        return;
                    }
                }
            }
            else
            {
                if (!base.ClientRectangle.Contains(e.Location))
                {
                  
                    return;
                }
                if (e.Button == MouseButtons.None)
                {
                    if (this.thumbRectangle.Contains(e.Location))
                    {
                        base.Invalidate(this.thumbRectangle);
                        return;
                    }
                    if (base.ClientRectangle.Contains(e.Location))
                    {
                        base.Invalidate();
                    }
                }
            }
        }

        

      
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
            if (base.DesignMode)
            {
                this.SetupScrollBar();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.SetupScrollBar();
        }

         
      

        private void SetupScrollBar()
        {
            if (this.inUpdate)
            {
                return;
            }
            if (this.Orientation == MetroScrollOrientation.Vertical)
            {
                this.thumbWidth = ((base.Width > 0) ? base.Width : 10);
                this.thumbHeight = ThumbHeight;
                this.clickedBarRectangle = base.ClientRectangle;
                this.clickedBarRectangle.Inflate(-1, -1);
                this.thumbRectangle = new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, this.thumbWidth, this.thumbHeight);
                this.thumbPosition = this.thumbRectangle.Height / 2;
                this.thumbBottomLimitBottom = base.ClientRectangle.Bottom;
                this.thumbBottomLimitTop = this.thumbBottomLimitBottom - this.thumbRectangle.Height;
                this.thumbTopLimit = base.ClientRectangle.Y;
            }
            else
            {
                this.thumbHeight = ((base.Height > 0) ? base.Height : 10);
                this.thumbWidth = ThumbWidth;
                this.clickedBarRectangle = base.ClientRectangle;
                this.clickedBarRectangle.Inflate(-1, -1);
                this.thumbRectangle = new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, this.thumbWidth, this.thumbHeight);
                this.thumbPosition = this.thumbRectangle.Width / 2;
                this.thumbBottomLimitBottom = base.ClientRectangle.Right;
                this.thumbBottomLimitTop = this.thumbBottomLimitBottom - this.thumbRectangle.Width;
                this.thumbTopLimit = base.ClientRectangle.X;
            }
            this.ChangeThumbPosition(this.GetThumbPosition());
            this.Refresh();
        }

         
      

        private int GetValue(bool smallIncrement, bool up)
        {
            int num;
            if (up)
            {
                num = this.curValue - (smallIncrement ? this.smallChange : this.largeChange);
                if (num < this.minimum)
                {
                    num = this.minimum;
                }
            }
            else
            {
                num = this.curValue + (smallIncrement ? this.smallChange : this.largeChange);
                if (num > this.maximum)
                {
                    num = this.maximum;
                }
            }
            return num;
        }

        private int GetThumbPosition()
        {
            if (this.thumbHeight == 0 || this.thumbWidth == 0)
            {
                return 0;
            }
            int num = 1;
            try
            {
              num = (this.metroOrientation == MetroScrollOrientation.Vertical) ? (this.thumbPosition / base.Height / this.thumbHeight) : (this.thumbPosition / base.Width / this.thumbWidth);

            }
            catch (Exception ex)
            {

      
            }
            int num2;
            if (this.Orientation == MetroScrollOrientation.Vertical)
            {
                num2 = base.Height - num;
            }
            else
            {
                num2 = base.Width - num;
            }
            int num3 = this.maximum - this.minimum;
            float num4 = 0f;
            if (num3 != 0)
            {
                num4 = ((float)this.curValue - (float)this.minimum) / (float)num3;
            }
            return Math.Max(this.thumbTopLimit, Math.Min(this.thumbBottomLimitTop, Convert.ToInt32(num4 * (float)num2)));
        }

        private int GetThumbSize()
        {
            int num = (this.metroOrientation == MetroScrollOrientation.Vertical) ? base.Height : base.Width;
            if (this.maximum == 0 || this.largeChange == 0)
            {
                return num;
            }
            //10 * height /100
            float val = (float)this.largeChange * (float)num / (float)this.maximum;

           return Convert.ToInt32(Math.Min((float)num, Math.Max(val, 10f)));
           // return 100;
        }
        private void ChangeThumbPosition(int position)
        {
            if (this.Orientation == MetroScrollOrientation.Vertical)
            {
                this.thumbRectangle.Y = position;
                return;
            }
            this.thumbRectangle.X = position;
        }

        private void ProgressThumb(bool enableTimer)
        {
            int num = this.curValue;
            ScrollEventType type = ScrollEventType.First;
            int num2;
            int num3;
            if (this.Orientation == MetroScrollOrientation.Vertical)
            {
                num2 = this.thumbRectangle.Y;
                num3 = this.thumbRectangle.Height;
            }
            else
            {
                num2 = this.thumbRectangle.X;
                num3 = this.thumbRectangle.Width;
            }
            if (this.bottomBarClicked && num2 + num3 < this.trackPosition)
            {
                type = ScrollEventType.LargeIncrement;
                this.curValue = this.GetValue(false, false);
                if (this.curValue == this.maximum)
                {
                    this.ChangeThumbPosition(this.thumbBottomLimitTop);
                    type = ScrollEventType.Last;
                }
                else
                {
                    this.ChangeThumbPosition(Math.Min(this.thumbBottomLimitTop, this.GetThumbPosition()));
                }
            }
            else if (this.topBarClicked && num2 > this.trackPosition)
            {
                type = ScrollEventType.LargeDecrement;
                this.curValue = this.GetValue(false, true);
                if (this.curValue == this.minimum)
                {
                    this.ChangeThumbPosition(this.thumbTopLimit);
                    type = ScrollEventType.First;
                }
                else
                {
                    this.ChangeThumbPosition(Math.Max(this.thumbTopLimit, this.GetThumbPosition()));
                }
            }
            if (num != this.curValue)
            {
                this.OnScroll(type, num, this.curValue, this.scrollOrientation);
                base.Invalidate();
                
            }
        }
       [Category("外观")]
        public int ThumbHeight
        {
            get { return thumbHeight; }
            set { 
                thumbHeight = value;
                SetupScrollBar();
                Refresh(); }
        }
         [Category("外观")]
       public Color NormalThumbColor
       {
           get { return clrNormalThumb; }
           set { clrNormalThumb = value; Refresh(); }
       }
         [Category("外观")]
       public Color PressThumbColor
       {
           get { return clrPressThumb; }
           set { clrPressThumb = value; Refresh(); }
       }
         [Category("外观")]
         public int ThumbWidth
         {
             get
             {
                 return thumbWidth;
             }
             set
             {
                 thumbWidth = value;
                 SetupScrollBar();
                 Refresh();
             }
         }
    }
    [Designer(typeof(ScrollableControlDesigner), typeof(ParentControlDesigner))]
    internal class MetroScrollBarDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Orientation"];
                if (propertyDescriptor == null)
                {
                    return base.SelectionRules;
                }
                MobileScrollBar.MetroScrollOrientation metroScrollOrientation = (MobileScrollBar.MetroScrollOrientation)propertyDescriptor.GetValue(base.Component);
                if (metroScrollOrientation == MobileScrollBar.MetroScrollOrientation.Vertical)
                {
                    return SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.TopSizeable | SelectionRules.BottomSizeable;
                }
                return SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
            }
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("Text");
            properties.Remove("BackgroundImage");
            properties.Remove("ForeColor");
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("BackColor");
            properties.Remove("Font");
            properties.Remove("RightToLeft");
            base.PreFilterProperties(properties);
        }
    }
}

