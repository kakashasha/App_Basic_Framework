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
    [ToolboxItem(true),Designer(typeof(ImageViewDesigner)), ToolboxBitmap(typeof(PictureBox))]
    public class MobilePictureBox : UIElement
    {
        protected Image img;
        protected SizeF imageSize;
        protected Color clrBorder;
        protected Pen pen_Border;
        private bool bCenterImage;
        protected float borderThickness = 2;
        [Category("外观")]
        public Image Source
        {
            get
            {
                return img;
            }

            set
            {
                if (img != null)
                    img.Dispose();
                img = value;
                Refresh();
            }
        }
        [Category("外观")]
        public SizeF ImageSize
        {
            get
            {
                return imageSize;
            }

            set
            {
                imageSize = value;

                Refresh();
            }
        }
        [Category("外观")]
        public Color BorderColor
        {
            get
            {
                return clrBorder;
            }

            set
            {
                clrBorder = value;
                if (pen_Border == null)
                    pen_Border = new Pen(Color.LightGray, 2);
                pen_Border.Color = value;
                Refresh();
            }
        }
        [Category("外观")]
        public virtual bool CenterImage
        {
            get
            {
                return bCenterImage;
            }

            set
            {
                bCenterImage = value;
                Refresh();
            }
        }
        [Category("外观")]
        public float BorderThickness
        {
            get { return borderThickness; }
            set
            {
                borderThickness = value; 
                Refresh();
            }
        }

        void ResizeMe()
        {
            this.Size =
                new Size
                ((int)imageSize.Width + 4, (int)imageSize.Height + 4);
        }
        protected void CenterPicture(Graphics _g)
        {
            int x = (int)(this.Width / 2 - imageSize.Width / 2);
            int y = (int)(this.Height / 2 - imageSize.Height / 2);
            _g.DrawImage(Source, x, y, imageSize.Width, imageSize.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if(pen_Border==null)
                pen_Border = new Pen(Color.DodgerBlue, 1); 
            g.DrawRectangle(pen_Border, 0, 0, this.Width - 1, this.Height - 1);
            if (!bCenterImage)
            {

                if (img != null)
                    g.DrawImage(img, 2, 2, imageSize.Width, imageSize.Height);
            }
            else
                    if (img != null)
                CenterPicture(g);
        }

        protected override void prepareAppearance()
        {
            pen_Border = new Pen(Color.DodgerBlue, 1);
            base.prepareAppearance();
        }
    }
 internal class ImageViewDesigner : VisualDesigner
    {
      

        protected override void ProvideType()
        {
            this.type_ActionList = typeof(ImageViewActionListDesigner);
        }
    }

    internal class ImageViewActionListDesigner : VisualActionList
    {
        private MobilePictureBox blk;
        public ImageViewActionListDesigner(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            blk = TargetControl as MobilePictureBox;
            
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            AddDesignerActionHeaderItem(APPEARANCE);
      
            AddDesignerActionPropertyItem("Source", "图片");
            AddDesignerActionPropertyItem("CenterImage", "图片居中");
            AddDesignerActionPropertyItem("BorderColor", "边框颜色");
            AddDesignerActionPropertyItem("ImageSize", "图片大小");
        }

        public Color BorderColor
        {
            get { return blk.BorderColor; }
            set{  AssignPropertyValue("BorderColor",value); }
        }
        public SizeF ImageSize
        {
            get { return blk.ImageSize; }
            set
            {
                AssignPropertyValue("ImageSize",value);
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
        public Image Source
        {
            get { return blk.Source; }
           set{ AssignPropertyValue("Source",value);} 
        }
    }

}
