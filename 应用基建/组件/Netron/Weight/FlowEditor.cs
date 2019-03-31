using Netron.GraphLib;
using Netron.GraphLib.BasicShapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace dotNetLab.Common
{

    public class FlowEditor : UserControl
    {
        private SplitContainer splitContainer2;
        private SplitContainer MainContainer;
      public  GraphControlEx graphControlEx;
        PropertyGrid grid;
        public Control CustomControlContainer
        {
            get
            {
                return splitContainer2.Panel2;
            }

        }

        public FlowEditor()
        {
            InitializeComponent();
            grid = new PropertyGrid();
            grid.Dock = DockStyle.Fill;
            graphControlEx = new GraphControlEx();
            graphControlEx.Dock = DockStyle.Fill;
            MainContainer.Panel1.Controls.Add(graphControlEx);

            splitContainer2.Panel1.Controls.Add(grid);
            graphControlEx.grid = this.grid;
            grid.SelectedObject = graphControlEx;
            graphControlEx.Click += (s, e) =>
             {
                 grid.SelectedObject = s;
             };
        }
        public void Open(String fileName)
        {
            this.graphControlEx.Open(fileName);
            foreach (Shape item in graphControlEx.Shapes)
            {
                if (item is NRectangle)
                {
                    (item as NRectangle).grid = this.grid;
                    item.OnMouseUp += (s, ex) =>
                    {
                        grid.SelectedObject = s;
                    };
                }
            }
        }
        public void Save(String fileName)
        {
            graphControlEx.SaveAs(fileName);
        }
        private void InitializeComponent()
        {
            this.MainContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).BeginInit();
            this.MainContainer.Panel2.SuspendLayout();
            this.MainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainContainer
            // 
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(0, 0);
            this.MainContainer.Name = "MainContainer";
            // 
            // MainContainer.Panel2
            // 
            this.MainContainer.Panel2.Controls.Add(this.splitContainer2);
            this.MainContainer.Size = new System.Drawing.Size(853, 594);
            this.MainContainer.SplitterDistance = 630;
            this.MainContainer.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer2.Size = new System.Drawing.Size(219, 594);
            this.splitContainer2.SplitterDistance = 336;
            this.splitContainer2.TabIndex = 0;
            // 
            // FlowEditor
            // 
            this.Controls.Add(this.MainContainer);
            this.Name = "FlowEditor";
            this.Size = new System.Drawing.Size(853, 594);
            this.MainContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).EndInit();
            this.MainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
    public class GraphControlEx : Netron.GraphLib.UI.GraphControl
    {
        public PropertyGrid grid;
       
         
        public GraphControlEx()
        {
            AutoScroll = true;
            RestrictToCanvas = false;
            this.ShowGrid = false;

            ThisMenuStrip.Items.Add("重命名").Click += (s, e) =>
            {
                if(this.SelectedShapes.Count ==1)
                {
                    NRectangle rct = (SelectedShapes[0] as NRectangle);

                    TextBox m_tb = null;
                    m_tb = new TextBox();
                    m_tb.Font = new Font("微软雅黑",9);
                    m_tb.Width = (int)rct.Rectangle.Width - 2;
                    m_tb.Location = new Point((int)rct.Rectangle.X + 2, (int)rct.Rectangle.Y + (int)rct.Rectangle.Height / 2 - m_tb.Height / 2);
                   
                    m_tb.Multiline = true;
                    m_tb.Text = Text;
                    m_tb.SelectionLength = Text.Length;
                    m_tb.KeyDown += (sx, ex) =>
                    {
                        if (ex.KeyCode == Keys.Enter)
                        {
                            rct.Text = m_tb.Text;

                            this.Controls.Remove(m_tb);
                        }
                    };

                    m_tb.LostFocus += (sx, ex) =>
                     {
                         rct.Text = m_tb.Text;

                         this.Controls.Remove(m_tb);
                     };
                    this.Controls.Add(m_tb);
                    m_tb.Show();
                    m_tb.Focus();
                    m_tb.ScrollToCaret();
                }
            };
            ThisMenuStrip.Items.Add("添加通用模块").Click += (s, e) =>
             {
                 AddNRectangleShape();
             };
            ThisMenuStrip.Items.Add("添加描述").Click += (s, e) =>
            {
                NDescriptor r = new NDescriptor();
                r.Rectangle = new RectangleF(0,0,60f, 30f);
                r.grid = grid;
                r.OnMouseUp += (sx, ex) =>
                {
                    grid.SelectedObject = sx;
                };
                Point p1 = MousePosition;//鼠标相对于屏幕的坐标
                Point p2 = this.PointToClient(MousePosition);//鼠标相对于窗体的坐标
                this.AddShape(r, p2);
            };
            ThisMenuStrip.Items.Add("缩放").Click += (s, e) =>
            {
                Form form = new Form();
                form.Font = new Font("微软雅黑",10);
                form.MaximizeBox = false;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.Size = new Size(200, 90);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Controls.Add(new Button());
                form.Controls.Add(new Button());
                
                form.Controls[0].Dock = DockStyle.Top;
                form.Controls[0].Height = 35;
                form.Controls[0].Margin = new Padding(2, 2, 2, 2);
              form.Controls[0].Text = "缩小";
                form.Controls[1].Dock = DockStyle.Bottom;
                form.Controls[1].Text = "放大";
                form.Controls[1].Height = 35;

                form.Controls[0].Click += (sx, ex) =>
                 {
                     this.Zoom -= 0.1f;
                 };
                form.Controls[1].Click += (sx, ex) =>
                {
                    this.Zoom += 0.1f;
                };
                form.Show();
                form.FormClosing += (sx, ex) =>
                 {
                     form.Dispose();
                 };
            };

             
        }
        public void AddNRectangleShape()
        {
            NRectangle r = new NRectangle();
            r.grid = grid;
            r.OnMouseUp += (sx, ex) =>
            {
                grid.SelectedObject = sx;
            };
            Point p1 = MousePosition;//鼠标相对于屏幕的坐标
            Point p2 = this.PointToClient(MousePosition);//鼠标相对于窗体的坐标
            this.AddShape(r, p2);
        }

    }


  
    [Serializable]
    public class NRectangle : Shape
    {
        private Connector m_leftConnector;
        private Connector m_rightConnector;
        private Connector m_upConnector;
        private Connector m_bottomConnector;
        public PropertyGrid grid;
        MouseEventHandler handler_LinkLine;

        public override string Text
        {
            get => base.Text; set
            {
                base.Text = value;

                Invalidate();
            }
        }


        protected override internal void InitEntity()
        {
            base.InitEntity();

            Pen = new Pen(Brushes.Transparent, 2);
            ShapeColor = Color.DarkGray;
            Rectangle = new RectangleF(100, 10, 100, 50);

            m_leftConnector = new Connector(this, "Left", true);

            m_rightConnector = new Connector(this, "Right", true);
            m_upConnector = new Connector(this, "Up", true);
            m_bottomConnector = new Connector(this, "Bottom", true);

            UnitConnectors();

        }

        void UnitConnectors()
        {
            base.Connectors.Add(m_leftConnector);
            base.Connectors.Add(m_rightConnector);
            base.Connectors.Add(m_upConnector);
            base.Connectors.Add(m_bottomConnector);
            
          TextBox   m_tb = new TextBox();
            m_tb.Font = new Font("微软雅黑", 10);
            
            handler_LinkLine = (s, ex) =>
            {
                grid.SelectedObject = s;
            };
        }


        public override PointF ConnectionPoint(Connector c)
        {
            if (c == m_leftConnector)
                return new PointF(Rectangle.Left, Rectangle.Top + Rectangle.Height / 2);
            if (c == m_rightConnector)
                return new PointF(Rectangle.Right, Rectangle.Top + Rectangle.Height / 2);
            if (c == m_upConnector)
                return new PointF(Rectangle.Left + Rectangle.Width / 2, Rectangle.Top);
            if (c == m_bottomConnector)
                return new PointF(Rectangle.Right - Rectangle.Width / 2, Rectangle.Bottom);
            return new PointF(0, 0);
        }

        public override void Paint(Graphics g)
        {

            base.Paint(g);
            g.FillRectangle(new SolidBrush(ShapeColor), Rectangle);
           // g.DrawRectangle(Pen, System.Drawing.Rectangle.Round(Rectangle));

            foreach (Connector item in Connectors)
            {

                foreach (Connection chd in item.Connections)
                {

                    chd.OnMouseUp -= handler_LinkLine;
                    chd.OnMouseUp += handler_LinkLine;
                }

            }

            if (!String.IsNullOrEmpty(Text))
            {
                SizeF sf = g.MeasureString(Text, Font);
                float x = Rectangle.X + Rectangle.Width / 2 - sf.Width / 2;
                float y = Rectangle.Y + Rectangle.Height / 2 - sf.Height / 2;

                g.DrawString(Text, Font, TextBrush, x, y);


            }
        }


        protected NRectangle(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {

            m_leftConnector = InitConnectors(info, "m_leftConnector", typeof(Connector));
            m_rightConnector = InitConnectors(info, "m_rightConnector", typeof(Connector));
            m_upConnector = InitConnectors(info, "m_upConnector", typeof(Connector));
            m_bottomConnector = InitConnectors(info, "m_bottomConnector", typeof(Connector));
            UnitConnectors();
        }

        public NRectangle()
        {
        }

        Connector InitConnectors(SerializationInfo info, String ConnectorName, Type ConnectorType)
        {
            Connector connector = (Connector)info.GetValue(ConnectorName, typeof(Connector));
            connector.BelongsTo = this;

            return connector;
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("m_leftConnector", m_leftConnector);
            info.AddValue("m_rightConnector", m_rightConnector);
            info.AddValue("m_upConnector", m_upConnector);
            info.AddValue("m_bottomConnector", m_bottomConnector);

        }
    }


    internal class NDescriptor: NRectangle
    {
        public override void Paint(Graphics g)
        {
            if (!String.IsNullOrEmpty(Text))
            {
                SizeF sf = g.MeasureString(Text, Font);
                float x = Rectangle.X + Rectangle.Width / 2 - sf.Width / 2;
                float y = Rectangle.Y + Rectangle.Height / 2 - sf.Height / 2;
                ((SolidBrush)TextBrush).Color = this.TextColor;
                g.DrawString(Text, Font, TextBrush, x, y);
            }

        }
    }
}
