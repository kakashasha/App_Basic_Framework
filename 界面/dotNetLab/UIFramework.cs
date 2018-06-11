using dotNetLab.Appearance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab
{
    namespace Framework
    {
        public struct DrageMoveArgs
        {
            public Control ctrlParent;
            public int X, Y;
            public int nSelfWidth;
            public int nSelfHight;
            public int nParentWidth;
            public int nParentHeight;

        }

        public enum Orientations
        {
            Axis_X, Axis_Y, None
        }

      

        [ToolboxItem(false)]
        public class View : UserControl
        {
            public delegate void DragCallback(ref DrageMoveArgs e);
            public event DragCallback DragMove;
            public delegate void DoTaskCallback();
            public event DoTaskCallback DoTask;
            protected SizeF szf_Text;
            protected Graphics g, g_Text;
            protected String strCaption;
            protected SolidBrush sbrh_Text;
            protected float x_Text;
            protected float y_Text;
            protected Font fnt_cmp;
            protected DrageMoveArgs dma;
            private Orientations enu_Orientation;
            protected Point pnt_Modify;
            protected GraphicsPath gp;
            public View()
            {

                this.BackColor = Color.Transparent;
                g_Text = this.CreateGraphics();
                pnt_Modify = new Point();
                Prepare();
            }



            protected virtual void Prepare()
            {
                prepareData();
                prepareAppearance();
                prepareCtrls();
                prepareEvents();
                CustomWork();
                UnitCtrls();
            }
            protected virtual void CustomWork() { }
            public void AddControl(Control ctrl)
            {
                this.Controls.Add(ctrl);
            }
            protected virtual void UnitCtrls() { }
            protected virtual void prepareEvents() { }
            protected virtual void prepareAppearance() { }
            protected virtual void prepareData() { }
            protected virtual void prepareCtrls() { }
            protected virtual void OnDraw() { }
            protected override void OnPaint(PaintEventArgs e)
            {

                g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                OnDraw();
                base.OnPaint(e);
            }
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle = 0x20;
                    return cp;
                }
            }
            public void YaHeiFont(int nFontSize)
            {
                this.Font = new Font(Fonts.YAHEI, nFontSize);
            }
            public Font DengXianFont(int nFontSize)
            {
                return new Font(Fonts.DENGXIANG, nFontSize);
            }
            protected virtual void CenterText(SolidBrush slbh_Text)
            {
                slbh_Text.Color = this.ForeColor;
                MessureText();

                g.DrawString(Text, Font, slbh_Text,
                x_Text, y_Text);
                this.fnt_cmp = Font;
            }
            protected void MessureText()
            {

                szf_Text = g_Text.MeasureString(strCaption, Font);
                x_Text = this.Width / 2 - szf_Text.Width / 2;
                y_Text = this.Height / 2 - szf_Text.Height / 2;
            }
            protected Color PressEffect(Color _c, int nValue, bool bIsAdd)
            {
                int _Red = _c.R;
                int _Green = _c.G;
                int _Blue = _c.B;
                if (bIsAdd)
                {
                    if (_c.R + nValue >= 0 && _c.R + nValue < 255)
                        _Red = _c.R + nValue;

                    if (_c.G + nValue >= 0 && _c.G + nValue < 255)

                        _Green = _c.G + nValue;

                    if (_c.B + nValue >= 0 && _c.B + nValue < 255)
                        _Blue = _c.B + nValue;
                }
                else
                {
                    if (_c.R - nValue >= 0)
                        _Red = _c.R - nValue;

                    if (_c.G - nValue >= 0)

                        _Green = _c.G - nValue;

                    if (_c.B - nValue >= 0)
                        _Blue = _c.B - nValue;
                }

                return Color.FromArgb(_Red, _Green, _c.B);
            }
            [BrowsableAttribute(false)]
            public override RightToLeft RightToLeft
            {
                get
                {
                    return base.RightToLeft;
                }

                set
                {
                    base.RightToLeft = value;
                }
            }
            [Category("外观")]
            public Orientations Orientation
            {
                get
                {
                    return enu_Orientation;
                }

                set
                {
                    enu_Orientation = value;
                    Refresh();
                }
            }

            public virtual void PrepareDragable()
            {

                this.MouseMove += this.Me_MouseMove;
            }

            protected virtual void HandleDragMove(DrageMoveArgs e)
            {
                this.DragMove(ref e);
                pnt_Modify.X = e.X;
                pnt_Modify.Y = e.Y;
                this.Location = pnt_Modify;

            }

            void CollectDragMoveInfo(Point pnt)
            {
                dma.X = pnt.X;
                dma.Y = pnt.Y;
                dma.nParentWidth = Parent.Width;
                dma.nParentHeight = Parent.Height;
                dma.nSelfWidth = this.Width;
                dma.nSelfHight = this.Height;
            }
            protected void Me_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    switch (enu_Orientation)
                    {
                        case Orientations.Axis_X:
                            pnt_Modify.X = Location.X + e.X;
                            pnt_Modify.X -= this.Width / 2;
                            pnt_Modify.Y = Location.Y;
                            CollectDragMoveInfo(pnt_Modify);
                            HandleDragMove(dma);
                            if (DoTask != null)
                                DoTask();
                            break;
                        case Orientations.Axis_Y:
                            pnt_Modify.X = Location.X;
                            pnt_Modify.Y = Location.Y + e.Y;
                            pnt_Modify.Y -= this.Height / 2;
                            CollectDragMoveInfo(pnt_Modify);
                            HandleDragMove(dma);
                            if (DoTask != null)
                                DoTask();
                            break;
                        case Orientations.None:
                            pnt_Modify.X = Location.X + e.X;
                            pnt_Modify.Y = Location.Y + e.Y;
                            CollectDragMoveInfo(pnt_Modify);
                            HandleDragMove(dma);

                            break;

                    }

                }
            }
            public new bool Enabled
            {
                get
                {
                    return base.Enabled;
                }
                set
                {
                    base.Enabled = value;
                    Refresh();
                }
            }

        }
    }
     
}
