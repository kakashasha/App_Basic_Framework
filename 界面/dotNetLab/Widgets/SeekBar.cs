using dotNetLab.Widgets.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{
   [ToolboxItem(true)]
   public class SeekBar : RoundElement

    {
        
        public delegate void ScrollValueChangedDelegate(object sender, float nDelta, float newValue, DragableDirection direction);
        public event ScrollValueChangedDelegate ValueChanged;
        public DragableElement p;
        float nLastValue = 0;
        
        private float fLineThickness = 1f;

        private Pen pen_Line;

        private int fBallSize = 22;

        private Color clr_Line = Color.Gray;

        private float fValue;

        private int LineLen;
        Orientation orientation ;

        protected virtual void PrepareDrableElement()
        {
            this.p = new Perl();
            this.p.Size = new Size(22, 22);
            base.Height = this.p.Height;
            this.p.Location = new Point(0, base.Height / 2 - this.p.Height / 2);
        }
        
        protected override void prepareCtrls()
        {
            base.prepareCtrls();
            PrepareDrableElement();
            this.p.PrepareDragable();
            this.p.Orientation = orientation;
            this.p.DragMove += this.perl_DragMove;
        }

        protected override void prepareEvents()
        {
            base.prepareEvents();
            base.Resize += new EventHandler(this.HSeekBar_Resize);
        }

        private void HSeekBar_Resize(object sender, EventArgs e)
        {
            NewPerl();
            
                if (orientation == Orientation.Horizontal)
                {
                    base.Height = this.p.Height + 5;
                    this.p.Location = new Point(0, base.Height / 2 - this.p.Height / 2);
                }
                else
                {
                    base.Width = this.p.Width + 5;
                    this.p.Location = new Point(base.Width / 2 - this.p.Width / 2, 0);
                }

            Invalidate();
        }

        protected override void UnitCtrls()
        {
            base.UnitCtrls();
            base.AddControl(this.p);
        }

        private void perl_DragMove(ref DrageMoveArgs e)
        {
            if (orientation == Orientation.Horizontal)
            {
                if (e.X <= 0)
                {
                    e.X = 0;
                    this.Value = 0f;
                }
                else
                {
                     
                    if (e.X + e.nSelfWidth >= base.Width)
                    {
                        e.X = base.Width - e.nSelfWidth;
                        this.Value = (float)this.LineLen;
                    }
                    else
                    {
                        this.Value = (float)(e.X - this.p.Width / 2);
                    }
                }
            }
            else
            {
                if (p.Location.Y <= 0)
                {
                     
                    this.Value = 0f;
                }
                else
                {
                    
                    if (p.Location.Y + p.Height >= base.Height)
                    {
                        e.Y = base.Height - p.Height;
                        this.Value = 100;
                    }
                    else
                    {
                        if(e.DragableDirection  == DrageMoveArgs.DragableDirections.Down)
                            this.Value += (this.)
                      //  this.Value = (e.Y  - this.p.Height / 2.0f)/(LineLen-p.Height) * 100.0f;
                    }
                }
            }
        }

        
        protected override void OnPaint(PaintEventArgs g)
        {
            
            if (this.pen_Line == null)
            {
                this.pen_Line = new Pen(this.LineColor, this.LineThickness);
            }
            if (this.orientation == Orientation.Horizontal)
            {
                this.LineLen = base.Width - this.p.Width;
                g.Graphics.DrawLine(this.pen_Line, this.p.Width / 2, base.Height / 2, base.Width - this.p.Width / 2, base.Height / 2);
            }
            else
            {
                LineLen = base.Height - p.Height;
                g.Graphics.DrawLine(this.pen_Line, p.Location.X + p.Width/2, this.p.Height / 2, p.Location.X + p.Width / 2, this.Height- p.Height/2);
            }
           
        }
        void NewPerl()
        {
            if (p == null)
            {
                p = new Perl();
               
            }
        }

        [Category("外观")]
        public Orientation Orientation
        {
            get { return orientation; }
            set
            {
                orientation = value;
                NewPerl();
                p.Orientation = value;
                Invalidate();
            }
        }
        [Category("外观")]
        public Color LineColor
        {
            get
            {
                return this.clr_Line;
            }
            set
            {
                this.clr_Line = value;
                bool flag = this.pen_Line == null;
                if (flag)
                {
                    this.pen_Line = new Pen(this.clr_Line, this.LineThickness);
                }
                this.pen_Line.Color = value;
                this.Refresh();
            }
        }

        [Category("外观")]
        public float LineThickness
        {
            get
            {
                return this.fLineThickness;
            }
            set
            {
                this.fLineThickness = value;
                bool flag = this.pen_Line == null;
                if (flag)
                {
                    this.pen_Line = new Pen(this.clr_Line, value);
                }
                this.pen_Line.Width = value;
            }
        }

        [Category("外观")]
        public int BallSize
        {
            get
            {
                return this.fBallSize;
            }
            set
            {
                this.fBallSize = value;

                if (this.p != null)
                {
                    this.p.Size = new Size(value, value);
                }

                this.Refresh();
            }
        }


        [Browsable(false)]
        public float Value
        {
            get
            {
                 
                return fValue;
            }
            set
            {
                this.fValue = value;
                if (ValueChanged != null)
                {
                    
                    if (orientation == Orientation.Vertical)
                    {

                        if (value - nLastValue > 0)

                            this.dragableDirection = DragableDirection.Down;

                        else
                            dragableDirection = DragableDirection.Top;
                    }
                    else
                    {
                        if (value - nLastValue > 0)

                            dragableDirection = DragableDirection.Right;

                        else
                            dragableDirection = DragableDirection.Left;
                    }
                    if (value - nLastValue != 0)
                    {
                        if (value != 100)
                            ValueChanged(this, Math.Abs(value - nLastValue), value, dragableDirection);
                        else
                            ValueChanged(this, 0, value, dragableDirection);
                    }
                }
                this.nLastValue = value;
            }
        }
    }
}

