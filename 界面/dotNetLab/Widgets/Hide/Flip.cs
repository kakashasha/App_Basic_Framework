using dotNetLab.Appearance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets
{
    [ToolboxItem(false)]
    public class Flip : MobilePictureBox
    {
        int nStartClientHeight = 0;
        int nOffset = 0;
        float fPenWidth = 1;
        SizeF szf_MessuredText;

        protected SolidBrush sldb_Caption, sldb_Background;
        Point[] ArrPnt_Border;
        Point[] arrPnt_Border_Draw;
        GraphicsPath gp;

        void GetSuitStr()
        {
            Graphics e = this.CreateGraphics();

            //Limited Ctrl Width       
            float temp_DiffWidth = this.Width - 10;
            StringBuilder strTemp = new StringBuilder();
            string strResult = "";
            szf_MessuredText = e.MeasureString(strCaption, Font);
            if (szf_MessuredText.Width > this.Width - 10)
            {
                for (int i = 0; i < this.strCaption.Length; i++)
                {
                    strTemp.Append(strCaption[i]);
                    szf_MessuredText = e.MeasureString(strTemp.ToString(), Font);
                    if (szf_MessuredText.Width > temp_DiffWidth)
                    {
                        strTemp.Append("\r\n");
                        strResult += strTemp;
                        strTemp.Remove(0, strTemp.Length);
                    }
                }
                if (strTemp != null)
                    strResult += strTemp;
                strCaption = strResult;
            }






        }
        protected override void prepareAppearance()
        {
            this.BackColor = Color.White;
            this.Font = new Font("微软雅黑", 12);
            sldb_Caption = new SolidBrush(Color.Red);
        }
        protected override void prepareData()
        {
            base.prepareData();
            ArrPnt_Border = new Point[7];
            arrPnt_Border_Draw = new Point[7];
            gp = new GraphicsPath();
            GetDrawPoints();
        }
        void PreparePoints(Point[] pnt)
        {
            pnt[0] = new Point(this.Width / 2, 0);
            // Top Left_Center Point
            pnt[1] = new Point(this.Width / 2 - this.Width / 6 - nOffset, this.Height / 8);
            // Top Left Point
            pnt[2] = new Point(1, pnt[1].Y);
            //Down Left Point
            pnt[3] = new Point(1, this.Height - 2);
            //Down Right Point
            pnt[4] = new Point(this.Width - 2, this.Height - 2);
            //Top Right Point
            pnt[5] = new Point(this.Width - 2, pnt[1].Y);
            // Top Right_Center
            pnt[6] = new Point(this.Width / 2 - (int)(this.Height / 8 / Math.Sqrt(3.0)), this.Height / 8);
        }
        void GetDrawPoints()
        {
            //Top Point
            PreparePoints(ArrPnt_Border);
            PreparePoints(arrPnt_Border_Draw);
            arrPnt_Border_Draw[3].Y -= 1;
            //Down Right Point
            arrPnt_Border_Draw[4] = new Point(this.Width - 3, this.Height - 3);
            //Top Right Point
            arrPnt_Border_Draw[5] = new Point(this.Width - 3, arrPnt_Border_Draw[1].Y);
            nStartClientHeight = ArrPnt_Border[6].Y;
            if (gp.PointCount > 0)
                gp.Reset();
            gp.AddPolygon(ArrPnt_Border);

            this.Region = new Region(gp);
        }
        protected override void prepareEvents()
        {
            base.prepareEvents();
            this.Resize += new EventHandler(MessageShow_Resize);

        }
        void MessageShow_Resize(object sender, EventArgs e)
        {
            GetDrawPoints();
            Refresh();

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.FillPolygon(sldb_Background, ArrPnt_Border);
            if (pen_Border == null)
                BorderColor = Color.Gray;
            g.DrawPolygon(pen_Border, arrPnt_Border_Draw);
            if (!string.IsNullOrEmpty(strCaption))
                g.DrawString(strCaption, Font, sldb_Caption, 5, this.nStartClientHeight + 5);
        }
       
        public int GetPlacePoint_Y()
        {
            return nStartClientHeight;
        }
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {

                sldb_Caption.Color = value;
                base.ForeColor = value;
                Refresh();
            }
        }
        public override Color BackColor
        {
            get
            {
                return sldb_Background.Color;

            }

            set
            {
                if (sldb_Background == null)
                    sldb_Background = new SolidBrush(Color.White);
                sldb_Background.Color = value;
                base.BackColor = value;
                this.Refresh();
            }
        }
        [Category("外观")]
        public  float BorderThickness
        {
            get { return fPenWidth; }
            set
            {
                fPenWidth = value;
                Refresh();
            }
        }
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                Refresh();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility
            (DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
                strCaption = value;
                GetSuitStr();
                Refresh();
            }
        }
        [Category("外观")]
        public int Offset
        {
            get { return nOffset; }
            set
            {
                nOffset = value;
                Refresh();
            }
        }

    }
}
