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
    [DefaultEvent("Click"), DefaultProperty("Text"),Designer(typeof(BlockDesigner)),ToolboxItem(true),ToolboxBitmap(typeof(FlowLayoutPanel))]
    public class Block : UIElement
    {
        public Font Font_Title;
        public Pen Pen_Border_Effect, Pen_Mark_Right;
        public SolidBrush TextBrush;
        private Point Point_Text_Start;
        bool bCanSelect = false;
        bool bCenterImage = true;
        private Point Brick_Picture_Pos;
        public int TitlePos_X = 30, TitlePos_Y = 10;
        public SolidBrush sbrush_BrickBK, sldb_Selected;
        //Top Text
        bool bNeedTitle = true;
        bool bNeedTip = false;
        Color clrSelected;
        string strBrickTitle;
        string strBrickTip;
        SizeF szf_MessureStr;
        bool bSelected = false;
        private Pen pen_Border = null ;
        private Image source;
        private Size imageSize;
        PointF[] pnt_Triangle, pnt_Mark_Right;
        private Color clrBackground;
    
        public override Font Font
        {
            get
            {
                return Font_Title;
            }
            set
            {
                this.Font_Title = value;
                base.Font = value;
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
                TextBrush.Color = value;
                Refresh();
            }
        }
        [Category("外观")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color SelectedColor
        {
            get { return clrSelected; }
            set
            {
                clrSelected = value;
                sldb_Selected.Color = clrSelected;
                Refresh();
            }
        }
        [Category("外观")]
        public bool Selected
        {
            get { return bSelected; }
            set { bSelected = value; Refresh();}
        }
        public bool NeedTip
        {
            get { return bNeedTip; }
            set
            {
                bNeedTip = value;
                Refresh();
            }
        }
        [Category("外观")]
        public bool EnableSelect
        {
            get { return bCanSelect; }
            set
            {
                bCanSelect = value;
                Refresh();
            }
        }
        [Category("外观")]
        public string BrickTip
        {
            get { return strBrickTip; }
            set
            {
                strBrickTip = value;
                this.bNeedTip = true;
                Refresh();
            }
        }
        [Category("外观")]
        public bool CenterImage
        {
            get { return bCenterImage; }
            set
            {
                bCenterImage = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Point TitlePos
        {
            get { return Point_Text_Start; }
            set
            {
                Point_Text_Start = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Point Picture_Pos
        {
            get { return Brick_Picture_Pos; }
            set
            {
                Brick_Picture_Pos = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Color BorderColor
        {
            get { return pen_Border.Color; }
            set { pen_Border.Color = value; this.Invalidate(); }
        }
        [Category("外观")]
        public Image Source
        {
            get { return source; }
            set
            {
                source = value;
                Refresh();
            }
        }
        [Category("外观")]
        public bool NeedTitle
        {
            get { return bNeedTitle; }
            set { bNeedTitle = value; Refresh(); }
        }
        [Category("外观")]
        public Size ImageSize
        {
            get { return imageSize; }
            set
            {
                imageSize = value;
                Refresh();
            }
        }
        //Top Text
        [BrowsableAttribute(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
                strBrickTitle = value;
                Refresh();
            }
        }
        [BrowsableAttribute(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
                Refresh();
            }
        }
        [Category("外观")]
        public Color BrickColor
        {
            get { return clrBackground; }
            set
            {
                this.clrBackground = value;
                sbrush_BrickBK.Color = clrBackground;
                Refresh();
            }
        }
        [Category("外观")]
        protected override void prepareEvents()
        {
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(Brick_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(Brick_MouseUp);
            this.KeyPress += Brick_KeyPress;
        }
        protected override void prepareData()
        {
            base.prepareData();

        }
        private void Brick_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

        }
        protected override void prepareAppearance()
        {
            this.Size = new Size(70, 35);
            Font_Title = new Font("微软雅黑", 12);
            Pen_Mark_Right = new Pen(Color.White, 3);
            clrSelected = Color.FromArgb(0, 122, 204);
            sbrush_BrickBK = new SolidBrush(clrBackground);
            sldb_Selected = new SolidBrush(clrSelected);
            TextBrush = new SolidBrush(Color.White);
            Point_Text_Start = new Point(TitlePos_X, TitlePos_Y);
            strBrickTitle = "未命名";
            this.strBrickTip = "提示";
            Pen_Border_Effect = new Pen(Color.Transparent, 1);
            imageSize = new Size(64, 64);
            Brick_Picture_Pos = new Point(ClientSize.Width / 8, ClientRectangle.Height / 4);
            pen_Border = new Pen(Color.LightGray, 2);
            ForeColor = Color.White;
            this.BorderColor = Color.Transparent;
           

        }
        protected override void OnResize(EventArgs e)
        {
            if (pnt_Triangle == null)
            {
                pnt_Triangle = new PointF[3];
                pnt_Mark_Right = new PointF[3];
            }
            pnt_Triangle[0] = new PointF(this.Width - 42, 4);
            pnt_Triangle[1] = new PointF(this.Width-4, 4);
            pnt_Triangle[2] = new PointF(this.Width-4, 42);

            Refresh();
            base.OnResize(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
             
            Graphics g = e.Graphics;
            Pen_Border_Effect.Color = PressEffect(sbrush_BrickBK.Color, 10, true);
            //基本框架
            if (bCanSelect && bSelected)
                g.FillRectangle(sldb_Selected, 4, 4, this.ClientSize.Width - 9, this.ClientSize.Height - 9);
            g.FillRectangle(sbrush_BrickBK, 4, 4, this.ClientSize.Width - 9, this.ClientSize.Height - 9);
            g.DrawRectangle(Pen_Border_Effect, 4, 4, this.ClientSize.Width - 9, this.ClientSize.Height - 9);

            if (bNeedTitle)
                g.DrawString(strBrickTitle, Font_Title, TextBrush, Point_Text_Start);
            if (bNeedTip)
            {
                szf_MessureStr = g.MeasureString(strBrickTip, Font_Title);
                g.DrawString(strBrickTip, Font_Title, TextBrush, this.Width - szf_MessureStr.Width - 6, this.Height - szf_MessureStr.Height - 6);

            }
            if (bCanSelect && bSelected)
            {
                g.FillPolygon(sldb_Selected, pnt_Triangle);
                g.DrawImage(global::dotNetLab.UI.check_brick,
                   this.Width - 25, 5, 22, 16);
            }
            if (Source != null)
            {
                if (!bCenterImage)
                    g.DrawImage(Source, Brick_Picture_Pos.X, Brick_Picture_Pos.Y, imageSize.Width, imageSize.Height);
                else
                {
                    int x = (int)(this.Width / 2 - imageSize.Width / 2);
                    int y = (int)(this.Height / 2 - imageSize.Height / 2);
                    g.DrawImage(Source, x, y, imageSize.Width, imageSize.Height);
                }

            }
            
        }
        
        void Brick_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            sbrush_BrickBK.Color = clrBackground;
            this.Refresh();
        }
        void Brick_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            sbrush_BrickBK.Color = PressEffect(clrBackground, 10, false);
            this.Refresh();
        }
    }

    internal class BlockDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(BlockActionListDesigner);
        }
    }

    internal class BlockActionListDesigner : VisualActionList
    {
        private Block blk;
        public BlockActionListDesigner(IComponent component,ControlDesigner controlDesigner) : base(component,controlDesigner)
        {
            blk = TargetControl as Block;
            
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
             AddDesignerActionHeaderItem(APPEARANCE);
            AddDesignerActionPropertyItem("BrickColor","磁贴的颜色");
            AddDesignerActionPropertyItem("Text","磁贴标题");
            AddDesignerActionPropertyItem("Source","磁贴图片");
            AddDesignerActionPropertyItem("ImageSize","磁贴图片大小");
            AddDesignerActionPropertyItem("ForeColor","字体颜色");
            AddDesignerActionPropertyItem("Font","字体");
            AddDesignerActionPropertyItem("NeedTitle","显示磁贴标题");
            AddDesignerActionPropertyItem("SelectedColor","选中时的颜色");
            AddDesignerActionPropertyItem("BrickTip","提示文本");
            AddDesignerActionPropertyItem("NeedTip","显示提示文本");
            AddDesignerActionPropertyItem("EnableSelect","允许被选中");
            AddDesignerActionPropertyItem("Selected","选中");
            AddDesignerActionPropertyItem("CenterImage","图片居中");
            AddDesignerActionPropertyItem("TitlePos","标题的坐标");
            AddDesignerActionPropertyItem("Picture_Pos","图片的坐标(禁用图片居中)"); 
        }

        
          public   Font Font
        {
            get
            {
                return blk.Font;
            }
            set
            {
                AssignPropertyValue("Font",value);
            }
        }
        public   Color ForeColor
        {
            get
            {
                return blk. ForeColor;
            }

            set
            {
                AssignPropertyValue("ForeColor",value);
            }
        }
   
        public Color SelectedColor
        {
            get { return blk.SelectedColor; }
            set
            {
                AssignPropertyValue("SelectedColor",value);
            }
        }
     
        public bool Selected
        {
            get { return blk.Selected; }
            set {  AssignPropertyValue("Selected",value); }
        }
        public bool NeedTip
        {
            get { return blk.NeedTip; }
            set
            {
                AssignPropertyValue("NeedTip",value);
            }
        }
  
        public bool EnableSelect
        {
            get { return blk.EnableSelect; }
            set
            {
                AssignPropertyValue("EnableSelect",value);
            }
        }
      
        public string BrickTip
        {
            get { return blk.BrickTip; }
            set
            {
                AssignPropertyValue("BrickTip",value);
            }
        }
  
        public bool CenterImage
        {
            get { return blk.CenterImage; }
            set
            {
                AssignPropertyValue("CenterImage",value);
            }
        }
     
        public Point TitlePos
        {
            get { return blk.TitlePos; }
            set
            {
                AssignPropertyValue("TitlePos",value);
            }
        }
    
        public Point Picture_Pos
        {
            get { return blk.Picture_Pos; }
            set
            {
                AssignPropertyValue("Picture_Pos",value);
            }
        }

       
        public bool NeedTitle
        {
            get { return blk.NeedTitle; }
            set { AssignPropertyValue("NeedTitle",value); }
        }
   
        public   string Text
        {
            get
            {
                return blk.Text;
            }

            set
            {
                AssignPropertyValue("Text",value);
            }
        }
        
  
        public Color BrickColor
        {
            get { return blk.BrickColor; }
            set
            {
                AssignPropertyValue("BrickColor",value);
            }
        }
        public Size ImageSize
        {
            get { return blk.ImageSize; }
            set
            {
                AssignPropertyValue("ImageSize",value);
            }
        }
        public Image Source
        {
            get { return blk.Source; }
           set{ AssignPropertyValue("Source",value);} 
        }
    }
}
