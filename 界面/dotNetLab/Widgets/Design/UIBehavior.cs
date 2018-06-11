using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;

namespace dotNetLab.Widgets.Design
{
  public  class UIBehavior : Behavior
    {
        public EventHandler Click = null;
        public override bool OnMouseUp(Glyph g, MouseButtons button)
        {
            if(Click!=null)
                Click(button, null);
            return base.OnMouseUp(g, button);
        }
    }
}
