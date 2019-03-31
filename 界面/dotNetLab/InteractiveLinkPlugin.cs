//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;

//namespace dotNetLab.Widgets
//{
//   public class InteractiveLinkPlugin : InteractivePlugin
//    {
        
//        public bool StopMove_UseLink = false;
//        bool EnhancedLine = false;
//        Pen pen_DrawLine;

//        public InteractiveLinkPlugin()
//        {
             
//            pen_DrawLine = new Pen(Brushes.Blue, 2); ;

//        }

//        public override Control Subject { get => base.Subject;

//            set {  base.Subject = value;
//                value.Tag = new List<Control>();
//                value.ContextMenuStrip = new ContextMenuStrip();
//                value.ContextMenuStrip.Items.Add("连接到").Click +=(s,e)=>
//                {
//                    this.StopMove_UseLink = true;
//                };
//                value.ContextMenuStrip.Items.Add("移动").Click +=(s,e)=>
//                {
//                    StopMove_UseLink = false;
//                };
//                value.Parent.Paint += (s, e) =>
//                {
//                    Graphics g = e.Graphics;
//                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
//                    for (int i = 0; i <  Host.Parent.Controls.Count; i++)
//                    {
//                        if (!EnhancedLine)
//                        {
//                            pen_DrawLine.Width = 2;
//                            g.DrawLine(pen_DrawLine, Host.Location.X + Host.ClientRectangle.Right, Host.Location.Y + Host.ClientRectangle.Bottom, LinkedPoints[i].Location.X, LinkedPoints[i].Location.Y);
                            
//                        }
//                        else
//                        {
//                            pen_DrawLine.Width = 4;
//                            g.DrawLine(pen_DrawLine, Host.Location.X + Host.ClientRectangle.Right, Host.Location.Y + Host.ClientRectangle.Bottom, LinkedPoints[i].Location.X, LinkedPoints[i].Location.Y);
//                        }
//                    }

//                };
//                value.Parent.MouseClick += (s, e) =>
//                  {
//                     // e.Location
//                  };
//            }
//        }



//        protected override void Control_MouseMove(object o, MouseEventArgs e)
//        {
//            if(!StopMove_UseLink)
//            base.Control_MouseMove(o, e);
//            else
//            {
//                if (e.Button == MouseButtons.Left)
//                {
//                    Point mousePos = Control.MousePosition;
                    
//                     Point pnt = ((Control)o).Parent.PointToClient(mousePos);
//                    foreach (Control item in Subject.Parent.Controls)
//                    {
//                        if (item == Host)
//                            continue;
//                        Rectangle rct = new Rectangle(item.Location.X, item.Location.Y,  item.Width, item.Height);
//                       if(rct.Contains(pnt))
//                        {
                            
//                            ((List<Control>)Host.Tag).Add(item);


//                            break;
//                        }
//                    }
//                    ((Control)o).Parent.Invalidate();
//                }
               
//            }

//        }
//        public   bool PointIsInLine(PointF pf, PointF p1, PointF p2, double range = 0)
//        {

//            //range 判断的的误差，不需要误差则赋值0
//            //点在线段首尾两端之外则return false

//            double cross = (p2.X - p1.X) * (pf.X - p1.X) + (p2.Y - p1.Y) * (pf.Y - p1.Y);
//            if (cross <= 0) return false;
//            double d2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
//            if (cross >= d2) return false;
//            double r = cross / d2;
//            double px = p1.X + (p2.X - p1.X) * r;
//            double py = p1.Y + (p2.Y - p1.Y) * r;

//            //判断距离是否小于误差
//            return Math.Sqrt((pf.X - px) * (pf.X - px) + (py - pf.Y) * (py - pf.Y)) <= range;
//        }

         
//    }
//}
