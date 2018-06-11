using dotNetLab.Appearance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Widgets.Design;
using System.Windows.Forms.Design;

namespace dotNetLab.Widgets
{
    [ToolboxItem(true), DefaultEvent("Click"), DefaultProperty("Text"),
        Designer(typeof(TextBlockDesigner)), ToolboxBitmap(typeof(Label))]
    public class TextBlock : RoundElement
    {
        protected bool bEnableResize = false;
        private byte nWhereReturn = 0;
        StringBuilder sb;
        SolidBrush sbr_Flag;
        Graphics g = null;
        private bool bVertical = false;
        bool bLEDStyle = false;
        Font fnt_backup;
        FontFamily ff;
        bool bEnableFlag = false;
        Color clrFlagColor = Color.DodgerBlue;
        int nGapBetweenTextFlag = 10;
        bool bEnableTextRenderHint = false;
        Alignments alg_FlagAlign = Alignments.Left;
        int nFlagThickness = 5;
        bool bWithUnderLine = false;
        Pen pen_UnderLine;
        Color clrUnderLine = Color.DarkGray;
        float fUnderLineThickness = 2;
        public void MakeReturns()
        {
            if (sb == null)
                sb = new StringBuilder();
            if (nWhereReturn > 0 && Text != null)
            {
                int nLen = Text.Length;
                int nIndex = 0;
                sb.Remove(0, sb.Length);


                if (Text.Length >= nWhereReturn)
                {

                    for (int i = 0; i < nLen; i++)
                    {
                        if (nIndex == nWhereReturn)
                        {

                            sb.Append(Text.Substring(i - nWhereReturn, nWhereReturn));
                            sb.Append("\r\n");
                            nIndex = 0;
                        }
                        nIndex++;
                    }
                    if (nIndex != 0)
                        sb.Append(Text.Substring(nLen - nIndex));

                    this.Text = sb.ToString();
                }
            }
            Refresh();
        }
        protected void AutoCmpText()
        {
            if (g == null)
                g = this.CreateGraphics();
            szf_Text = g.MeasureString(strCaption, Font);
            //if (!bEnableResize)
            //{
            //    this.Width = (int)(szf_Text.Width / 10 + szf_Text.Width);
            //    this.Height = (int)(szf_Text.Height / 10 + szf_Text.Height);
            //}
        }
        protected override void prepareData()
        {

            this.Font = new System.Drawing.Font(Fonts.YAHEI, 12);
            this.fnt_backup = Font;
            base.prepareData();
        }
        protected override void prepareAppearance()
        {
            this.sbrh_Text = new
                System.Drawing.SolidBrush
                (System.Drawing.Color.Black);

        }


        void DrawTextBlockWithFlag(Graphics g)
        {
            SizeF sf;
            sf = g.MeasureString(Text, this.Font);
            if (sbr_Flag == null)
                sbr_Flag = new SolidBrush(FlagColor);
            sbr_Flag.Color = FlagColor;
            System.Drawing.Drawing2D.SmoothingMode orginMode = g.SmoothingMode;
            if (UnderLine)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                if (pen_UnderLine == null)
                {
                    pen_UnderLine = new Pen(UnderLineColor, UnderLineThickness);
                }
                pen_UnderLine.Color = UnderLineColor;
                pen_UnderLine.Width = UnderLineThickness;
                g.DrawLine(pen_UnderLine, FlagThickness + 1, this.Height - 1, this.Width, this.Height  - 1);
            }
            g.SmoothingMode = orginMode;
            switch (FlagAlign)
            {
               
                case Alignments.Up:
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                    g.FillRectangle(sbr_Flag, 0, 0, this.Width, FlagThickness);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawString(Text, Font, sbrh_Text, this.Width / 2 - sf.Width / 2, GapBetweenTextFlag+FlagThickness);
                    break;
                case Alignments.Down:
                     
                    
                    g.DrawString(Text, Font, sbrh_Text, this.Width / 2 - sf.Width / 2, 0);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                    g.FillRectangle(sbr_Flag, 0, sf.Height+GapBetweenTextFlag, this.Width, FlagThickness);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    break;
                case Alignments.Left:
                    

                    g.DrawString(Text, Font, sbrh_Text, GapBetweenTextFlag + FlagThickness, this.Height / 2 - sf.Height / 2);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                    g.FillRectangle(sbr_Flag, 0, 0, FlagThickness, this.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    break;
                case Alignments.Right:
                    
                    g.DrawString(Text, Font, sbrh_Text, GapBetweenTextFlag + FlagThickness, this.Height / 2 - sf.Height / 2);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                    g.FillRectangle(sbr_Flag, this.Width- GapBetweenTextFlag - FlagThickness, 0, FlagThickness, this.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    break;
              
            }
            
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (EnableTextRenderHint)
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            else
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            if (sbrh_Text == null)
                this.sbrh_Text = new
                System.Drawing.SolidBrush
                (System.Drawing.Color.Black);
            if (!Vertical)
            {
                if (!EnableFlag)
                    this.CenterText(e.Graphics, sbrh_Text);
                else
                    DrawTextBlockWithFlag(e.Graphics);
            }
            else
            {

                MessureText(e.Graphics);
                this.Size = new Size((int)(szf_Text.Height + 4), 2 + (int)szf_Text.Height * Text.Length);
                if (sb == null)
                    sb = new StringBuilder();
                else
                {
                    sb.Remove(0, sb.Length);
                }

                for (int i = 0; i < Text.Length; i++)
                {
                    sb.AppendFormat("{0}\r\n", Text[i]);
                }
                e.Graphics.DrawString(sb.ToString(), Font, sbrh_Text, 2, 2);
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

                if (bLEDStyle)
                    base.Font = new Font(ff, value.SizeInPoints);
                else
                    base.Font = value;
                AutoCmpText();
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

                AutoCmpText();

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
                if (sbrh_Text == null)
                    sbrh_Text = new SolidBrush(Color.Transparent);
                this.sbrh_Text.Color = value;
                base.ForeColor = value;
                Refresh();
            }
        }
        [Category("外观")]
        public byte WhereReturn
        {
            get
            {
                return nWhereReturn;
            }

            set
            {
                nWhereReturn = value;
                MakeReturns();

            }
        }
        [Category("外观")]
        public virtual bool Vertical
        {
            get { return bVertical; }
            set { bVertical = value;

                Invalidate(); }
        }
        [Category("外观")]
        public Alignments FlagAlign { get { return alg_FlagAlign; } set { alg_FlagAlign = value; Invalidate(); } }
        [Category("外观")]
        public Color FlagColor { get { return clrFlagColor; } set { clrFlagColor = value; Invalidate(); } }
        [Category("外观")]
        public bool EnableFlag { get { return bEnableFlag; } set { bEnableFlag = value;   this.bEnableResize = value; Invalidate(); } }

        [Category("外观")]
        public int GapBetweenTextFlag { get { return nGapBetweenTextFlag; } set { nGapBetweenTextFlag = value; Invalidate(); } }
        [Category("外观")]
        public virtual bool EnableTextRenderHint
        {
            get { return bEnableTextRenderHint; }
            set
            {
                bEnableTextRenderHint = value;

                Invalidate();
            }
        }
        [Category("外观")]
        public bool LEDStyle
        {
            get { return bLEDStyle; }
            set
            {
                bLEDStyle = value;
                if (ff == null)
                {
                    ff = dotNetLabUISurport.DynamicFont(UI.DS_DIGI);
                    base.Font = new Font(ff, Font.SizeInPoints);
                }
                if (value)

                    base.Font = new Font(ff, Font.SizeInPoints);
                else
                    base.Font = new Font("微软雅黑", this.Font.Size);

                Invalidate();
            }
        }
        [Category("外观")]
        public int FlagThickness
        {
            get { return nFlagThickness; }
            set { nFlagThickness = value; Invalidate(); }
        }
        [Category("外观")]
        public bool UnderLine
        {
            get
            {
                return bWithUnderLine;

            }
            set
            {
                bWithUnderLine = value;
                Invalidate();
            }
        }
        [Category("外观")]
        public Color  UnderLineColor
        {
            get { return clrUnderLine; }
            set
            {
                clrUnderLine = value;
                Invalidate();
            }
        }
        [Category("外观")]
        public float UnderLineThickness
        {
            get { return fUnderLineThickness; }
            set
            {
                fUnderLineThickness = value;
                Invalidate();
            }
        }

    }

    internal class TextBlockDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            type_ActionList = typeof(TextBlockDesignerActionList);
        }
    }

    internal class TextBlockDesignerActionList : VisualActionList
    {
        private TextBlock tb;
        public TextBlockDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            tb = TargetControl as TextBlock;
             
        }
        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            AddDesignerActionHeaderItem(APPEARANCE);
            AddDesignerActionPropertyItem("EnableTextRenderHint", "字体平滑");
            AddDesignerActionPropertyItem("Text", "文本");
            AddDesignerActionPropertyItem("LEDStyle", "LCD风格");
            AddDesignerActionPropertyItem("Vertical","垂直文本");
            AddDesignerActionPropertyItem("ForeColor", "字体颜色");
           
            AddDesignerActionPropertyItem("Font", "字体");
            AddDesignerActionPropertyItem("LEDFontSize", "LCD字体大小");
            AddDesignerActionHeaderItem("装饰器");
            AddDesignerActionPropertyItem("EnableFlag", "启用装饰器", "装饰器");
            AddDesignerActionPropertyItem("FlagAlign", "装饰器布局", "装饰器");
            AddDesignerActionPropertyItem("FlagColor", "装饰器颜色", "装饰器");
            AddDesignerActionPropertyItem("FlagThickness", "装饰器厚度", "装饰器");
            AddDesignerActionPropertyItem("GapBetweenTextFlag", "装饰器与文本间距", "装饰器");



        }
        public String Text
        {
            get { return tb.Text; }
            set
            {
                AssignPropertyValue("Text", value);
            }
        }
        public bool LEDStyle
        {
            get { return tb.LEDStyle; }
            set
            {
                AssignPropertyValue("LEDStyle", value);
            }
        }
        public float LEDFontSize
        {
            get { return tb.Font.Size; }
            set
            {
                Font fnt = new Font(tb.Font.FontFamily, value);
                AssignPropertyValue("Font", fnt);
            }
        }
        public Font Font
        {
            get { return tb.Font; }
            set { AssignPropertyValue("Font", value); }
        }
        public Color ForeColor
        {
            get { return tb.ForeColor; }
            set { AssignPropertyValue("ForeColor", value); }
        }
        public bool Vertical
        {
            get { return tb.Vertical; }
            set{AssignPropertyValue("Vertical",value);}
        }
        public Alignments FlagAlign { get { return tb.FlagAlign; } set { AssignPropertyValue("FlagAlign", value); } }
      
        public Color FlagColor { get { return tb.FlagColor; } set { AssignPropertyValue("FlagColor", value); } }
       
        public bool EnableFlag { get { return tb.EnableFlag; } set { AssignPropertyValue("EnableFlag", value); } }

       
        public int GapBetweenTextFlag { get { return tb.GapBetweenTextFlag; } set { AssignPropertyValue("GapBetweenTextFlag", value); } }
      
        public virtual bool EnableTextRenderHint
        {
            get { return tb.EnableTextRenderHint; }
            set
            {
                AssignPropertyValue("EnableTextRenderHint", value);
            }
        }
       
     
        public int FlagThickness
        {
            get { return tb.FlagThickness; }
            set { AssignPropertyValue("FlagThickness", value); }
        }
    }

}
