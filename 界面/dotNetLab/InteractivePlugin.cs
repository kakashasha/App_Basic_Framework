using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{
   public class InteractivePlugin
    {
        int ox, oy;
       protected Control Host;

       public virtual Control Subject
        {
            get
            {
                return Host;
            }
            set
            {
                Host = value;
                InitInteractiveControl();
            }
        }

        private void InitInteractiveControl()
        {
            Host.MouseDown -= Control_MouseDown;
            Host.MouseMove -= Control_MouseMove;
            Host.MouseDown += Control_MouseDown;
            Host.MouseMove += Control_MouseMove;
        }
        protected virtual void Control_MouseDown(object o, MouseEventArgs e)
        {
            ox = e.X;
            oy = e.Y;
             
        }
        protected virtual void Control_MouseMove(object o, MouseEventArgs e)
        {

            ((Control)o).Cursor = Cursors.Arrow;//设置拖动时鼠标箭头
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(ox, oy);//设置偏移
                ((Control)o).Location = ((Control)o).Parent.PointToClient(mousePos);
                ((Control)o).Parent.Invalidate();
            }
        }
    }
}
