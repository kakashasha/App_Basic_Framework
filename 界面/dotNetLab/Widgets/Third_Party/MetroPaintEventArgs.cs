using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace dotNetLab.Widgets.Third_Party
{
     
        public class MetroPaintEventArgs : EventArgs
        {
            public Color BackColor
            {
                get;
                private set;
            }

            public Color ForeColor
            {
                get;
                private set;
            }

            public Graphics Graphics
            {
                get;
                private set;
            }

            public MetroPaintEventArgs(Color backColor, Color foreColor, Graphics g)
            {
                this.BackColor = backColor;
                this.ForeColor = foreColor;
                this.Graphics = g;
            }
        }
     
}
