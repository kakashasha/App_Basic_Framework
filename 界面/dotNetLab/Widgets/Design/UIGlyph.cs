using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;

namespace dotNetLab.Widgets.Design
{
    public class UIGlyph : Glyph
    {
        Control control;
        BehaviorService behaviorSvc;
        public EventHandler Click = null;
       public UIBehavior uiBehavior;
        public UIGlyph(BehaviorService behaviorSvc, Control control)
            : base(new UIBehavior())
        {
            this.behaviorSvc = behaviorSvc;
            this.control = control;
            uiBehavior = base.Behavior as UIBehavior;
           // uiBehavior.Click = Click;
            
        }

        public override Rectangle Bounds
        {
            get
            {
                Point edge = behaviorSvc.ControlToAdornerWindow(control);
                Size size = control.Size;
                Point center = new Point(edge.X + (size.Width / 2),
                        edge.Y + (size.Height / 2));

                Rectangle bounds = new Rectangle(
                    center.X - control.Width / 2,
                    center.Y - control.Height / 2,
                    control.Width,
                    control.Height);
                //  Rectangle bounds = control.ClientRectangle;
                return bounds;
            }
        }

        public override Cursor GetHitTest(Point p)
        {
            if (Bounds.Contains(p))
            {
                return Cursors.Hand;
            }
            return null;
        }

        public override void Paint(PaintEventArgs pe)
        {
            //    pe.Graphics.FillEllipse(Brushes.Red, Bounds);
        }

      
    }
}
