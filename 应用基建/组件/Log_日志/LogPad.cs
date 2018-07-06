using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotNetLab.Widgets;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace dotNetLab.Common
{
    public static class MobileListBoxExtension
    {
       public static void AppendLog(this MobileListBox mlistbox,String strText,bool isError=false )
       {
           LogItem lg = new LogItem();
           if (isError)
               lg.ErrorText = strText;
           else
               lg.InfoText = strText;
           
            mlistbox.AddItem(lg);

       }
       public static void DisplayLogMessage( this Form frm,MobileListBox mlistbox,String strText,bool bisError)
       {

           mlistbox.AppendLog(strText, bisError);
       }
    }
    public class LogItem : UIElement
    {

        TextBox txb;
        public LogItem()
        {
            prepareCtrls();
            prepareEvents();

        }
        public String ErrorText
        {
            set
            {
                this.Text = value;
                this.ForeColor = Color.Red;
                this.Font = new Font(this.Font, FontStyle.Bold);
            }

        }
        public String InfoText
        {
            set
            {
                this.Text = value;
                this.ForeColor = Color.Green;
            }

        }
        protected override void prepareCtrls()
        {

            base.prepareCtrls();
            this.Font = new System.Drawing.Font("微软雅黑", 9);
            this.Height = 16;
            txb = new TextBox();
            txb.ReadOnly = true;
            txb.Location = this.Location;
            txb.Font = this.Font;
            txb.KeyUp += new KeyEventHandler(txb_KeyUp);
            txb.Visible = false;
            this.Controls.Add(txb);
        }

        void txb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txb.Visible = false;
            }
        }
        protected override void prepareEvents()
        {
            base.prepareCtrls();
            this.DoubleClick += new EventHandler(label1_DoubleClick);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (sbrh_Text == null)
                sbrh_Text = new SolidBrush(Color.DimGray);
            sbrh_Text.Color = ForeColor;
            e.Graphics.DrawString(Text, Font, sbrh_Text, 0, 0);

        }

        void label1_DoubleClick(object sender, EventArgs e)
        {
            if (txb.Visible == false)
            {

                txb.Visible = true;
                txb.Size = txb.Size;
                txb.Text = this.Text;
                txb.BringToFront();
            }



        }
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {


                base.Font = value;

                Refresh();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override String Text
        {
            get
            {
                return strCaption;
            }

            set
            {

                this.strCaption = value;

                Graphics g = this.CreateGraphics();
                SizeF sz = g.MeasureString(strCaption, Font);
                this.Width = (int)sz.Width;
                Refresh();
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {


                base.ForeColor = value;
                Refresh();
            }
        }

    }
}
