using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using dotNetLab.Widgets.Container;
using dotNetLab.Widgets.Design;
using dotNetLab.Widgets.UIBinding;

namespace dotNetLab.Widgets
{
    [ToolboxItem(true),Designer(typeof(TabHeaderDesigner))]
    [Browsable(false), ToolboxBitmap(typeof(TabControl))]
   public class TabHeader :dotNetLab.Widgets.Container.Container 
   {
        TabUIBinder tabUIBinder;
      
        string strTabFramePanelName = null;
        Control ctrlBody = null;
        Color clrItemNormalColor = Color.DarkGray;
        Color clrItemPressColor = Color.DodgerBlue;
        bool bUnderLine = false;
        bool bPureText = false;
       
        protected override void ProvideUIBinder()
       {
           tabUIBinder = new TabUIBinder(this);
           this.UIBinder = tabUIBinder;
       }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            UIBinder.BindUI();
        }
        protected override void prepareCtrls()
       {
           base.prepareCtrls();
           this.Size = new Size(200,35);
        }
    
        protected override void prepareEvents()
        {
            base.prepareEvents();
            tabUIBinder.TabItemClickRule += TabUIBinder_TabItemClickRule;
            tabUIBinder.AttachRule += TabUIBinder_AttachRule;
        }

        private void TabUIBinder_AttachRule(object sender, EventArgs e)
        {
            if (ctrlBody == null)
                GetBody();
            Control control = sender as Control;
            if ( (control).Tag == null)
            {
                if(ctrlBody!= null)
                {
                    int n = Controls.IndexOf((Tap)control);
                    foreach (Control item in ctrlBody.Controls)
                    {
                        if (item.Tag.Equals( n))
                        {
                            control.Tag = item;
                            break;
                        }
                    }
                    
                    Control ctrl = control.Tag as Control;
                    ctrl.BringToFront();
                }
            }
            else
            {
                Control ctrl = control.Tag as Control;
                ctrl.BringToFront();
            }
        }
        void GetBody()
        {
            if (this.Parent != null)
            {
                foreach (Control item in this.Parent.Controls)
                {
                    if (item.Name.Equals(TabFramePanelName))
                    {
                        ctrlBody = item;
                        break;
                    }
                }
            }
        }
        private void TabUIBinder_TabItemClickRule(object sender, EventArgs e)
        {
            tabUIBinder.DefaultClickRule(sender);
        }
        
        public bool ItemPureText
        {
            get { return bPureText; }
            set { bPureText = value; }
        }
      
        public bool ItemUnderLine
        {
            get { return bUnderLine; }
            set { bUnderLine = value;
                
            }
        }
        [Category("外观")]
        public String TabFramePanelName
        {
            get { return strTabFramePanelName; }
            set
            {
                strTabFramePanelName = value;
                
               
                Refresh();
            }
        }
        [Category("外观")]
        public Color ItemNormalColor
        {
            get { return clrItemNormalColor; }
            set { clrItemNormalColor = value;
                UIBinder.BindUI();
                Invalidate(); }
        }
       [Category("外观")]
        public Color ItemPressColor
        {
            get { return clrItemPressColor; }
            set { clrItemPressColor = value; UIBinder.BindUI(); Invalidate(); }
        }
    }
   
    internal class TabHeaderDesigner : VisualPanelDesigner
    {
        protected override void ProvideType()
        {
            type_ActionList = typeof(TabHeaderDesignerActionList);
        }
    }

    internal class TabHeaderDesignerActionList : ContainerDesignerActionList
    {
        private TabHeader th;
      //  private VisualPanelDesigner pcd;
        public TabHeaderDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            th = TargetControl as TabHeader;
          //  pcd = this.designer as VisualPanelDesigner;
                
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {

            AddDesignerActionMethodItem("AddChild", "添加项");
            AddDesignerActionMethodItem("RefreshLayout", "刷新布局");
            AddDesignerActionPropertyItem("BodyName", "Tab控件的主体名");
            AddDesignerActionPropertyItem("ItemNormalColor", "子控件颜色");
            AddDesignerActionPropertyItem("ItemPressColor", "子控件按下时颜色");
            AddDesignerActionPropertyItem("UnderLine", "下划线风格");
            AddDesignerActionPropertyItem("PureText", "纯文本风格");

        }
        public bool  PureText
        {
            get { return th.ItemPureText; }
            set {
                AssignPropertyValue("ItemPureText", value);
                foreach (Control item in vpcd.Control.Controls)
                {
                    if (value)
                    {
                        AssignPropertyValue(item, "Radius", -1);
                    }
                    AssignPropertyValue(item, "PureText", value);
                    
                }
            }
        }
        public bool  UnderLine
        {
            get { return th.ItemUnderLine; }
            set
            {
                AssignPropertyValue("ItemUnderLine", value);
                foreach (Control item in vpcd.Control.Controls)
                {

                    if (value)
                    {
                        AssignPropertyValue(item, "Radius", -1);
                    }
                    AssignPropertyValue(item, "UnderLine", value);
                    
                }
            }
        }
        
        protected override void RefreshLayout()
        {
            base.RefreshLayout();
            MessageBox.Show(vpcd.Control.Controls.Count.ToString());
            System.Windows.Forms.Control.ControlCollection ctrl_Box = vpcd.Control.Controls;
            Tap tp_0 = ctrl_Box[0] as Tap;
            for (int i = 0; i < ctrl_Box.Count; i++)
            {
                
                AssignPropertyValue(ctrl_Box[i], "NormalColor", th.ItemNormalColor);
                AssignPropertyValue(ctrl_Box[i], "PressColor", th.ItemPressColor);
                 

                if (tp_0.Radius > 0)
                {
                    if (ctrl_Box[i] == tp_0 || i== ctrl_Box.Count-1)
                    {
                       
                        ;
                    }
                   else
                    {
                        AssignPropertyValue(ctrl_Box[i], "CornerAlignment", 0);
                        AssignPropertyValue(ctrl_Box[i], "Radius", 1);
                    }
                       
                }
                 
            }
               
             
        }
        public Color ItemNormalColor
        { get { return th.ItemNormalColor; }
            set {
                AssignPropertyValue("ItemNormalColor", value);
                //foreach (Control item in vpcd.Control.Controls)
                //{
                //    AssignPropertyValue(item, "NormalColor", th.ItemNormalColor);
                //}
            }
        }
        public Color ItemPressColor
        {
            get { return th.ItemPressColor; }
            set
            {
                AssignPropertyValue("ItemPressColor", value);
                //foreach (Control item in vpcd.Control.Controls)
                //{
                //    AssignPropertyValue(item, "PressColor", th.ItemNormalColor);
                //}
            }
        }
        public String BodyName
        {
            get { return th.TabFramePanelName; }
            set
            {
                AssignPropertyValue("TabFramePanelName", value);
            }
        }
        protected override void AddChild()
        {
            AddDesignerChild(typeof(Tap));
        }

        
    }
    [ToolboxItem(false),ToolboxBitmap(typeof(Tap), "ICons.TabHeaderItem.bmp")]
   [Designer(typeof(TapDesigner))]
    public class Tap : RoundElement
    {
        
       
        bool bUnderLine = false;
        bool bPureText = false;
        private int nUnderLineThickness = 2;
        private bool isMouseDown = false;
        protected int nUITabHeaderItemGap = 0;
        Image source = null;
        Size szImageSize  ;
        Point imagePos  ;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (gp != null)
            {
                gp.Dispose();
                gp = null;
            }

        }
        protected override void prepareAppearance()
            {
                base.prepareAppearance();
                this.Size = new Size(70, 35);
                YaHeiFont(11);
               
                sbrh_Text = new SolidBrush(Color.White);
                sbr_FillRoundRect = new SolidBrush(Color.Gray);
            BackColor = Color.Transparent;
            PressColor = Color.DodgerBlue;
            NormalColor = Color.DarkGray;
            szImageSize = new Size(0, 0);
            imagePos = new Point(0, 0);
            this.ForeColor = Color.White;
            }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (this.nRadius > -1)
            {
               

                g.SmoothingMode = SmoothingMode.HighQuality;
               
                 
                    switch (CornerAlignment)
                    {
                        case Alignments.Up:
                            if (gp == null)
                            {
                                gp = UpRoundRect(0, 0, Width -1, Height-1, Radius);

                            }


                            break;
                        case Alignments.Down:
                            if (gp == null)
                            {
                                gp = DownRoundRect(this.Height, 0, 0, Width-1 , Height-1 , Radius);
                            }

                            break;
                    case Alignments.Left:

                        if (gp == null)
                        {
                            gp = LeftRoundRect( 0, 0, Width - 1, Height - 1, Radius);
                        }

                        break;
                    case Alignments.Right:
                        if (gp == null)
                        {
                            gp = RightRoundRect(this.Width,0, 0, Width - 1, Height - 1, Radius);
                        }

                        break;
                    case Alignments.All:
                            if (gp == null)
                            {
                                gp = DrawRoundRect(0, 0, Width -1, Height-1 , Radius);
                            }

                            break;
                        case Alignments.Left_UP:
                            if (gp == null)
                                gp = LeftUpRoundRect(0, 0, Width-1 , Height-1 , Radius);
                            break;
                        case Alignments.Left_Down:
                          gp = LeftDownRoundRect(0, 0, Width - 1, Height - 1, Radius);
                         
                            break;
                        case Alignments.Right_UP:
                            if (gp == null)
                                gp = RightUpRoundRect(0, 0, Width-1 , Height-1 , Radius);
                            break;
                        case Alignments.Right_Down:
                          gp = RightDownRoundRect(0, 0, Width - 1, Height - 1, Radius);
                        
                            break;

                    }
                if(gp!= null)
                    g.FillPath(sbr_FillRoundRect, gp);
                
               
               
            }
            else
            {
                if (bUnderLine)
                {
                    g.FillRectangle(sbr_FillRoundRect, 0, Height - nUnderLineThickness, Width, nUnderLineThickness);
                    if (isMouseDown)
                        sbrh_Text.Color = PressColor;
                    else
                    {
                        sbrh_Text.Color = NormalColor;
                    }
                }
                else if (bPureText)
                {
                    if (isMouseDown)
                        sbrh_Text.Color = PressColor;
                    else
                    {
                        sbrh_Text.Color = NormalColor;
                    }
                }
                else
                {
                    sbrh_Text.Color = ForeColor;
                    e.Graphics.FillRectangle(sbr_FillRoundRect, this.ClientRectangle);
                }
            }
            if (Source != null)
            {
                g.DrawImage(Source, ImagePos.X,imagePos.Y, ImageSize.Width, ImageSize.Height);
            }
            
            CenterText(e.Graphics, sbrh_Text);
           
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            sbr_FillRoundRect.Color = PressColor;
            isMouseDown = true;
          Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            sbr_FillRoundRect.Color = NormalColor;
            isMouseDown = false;
            Invalidate();
        }
        [Category("外观")]
        public Size ImageSize
        {
            get { return szImageSize; }
            set { szImageSize = value;Invalidate(); }
        }
        [Category("外观")]
        public  Point ImagePos
        {
            get { return imagePos; }
            set { imagePos = value; Invalidate(); }
        }
        [Category("外观")]
        public Image Source
        {
            get { return source; }
            set
            {
                if (source != null)
                    source.Dispose();
                source = null;
                source = value;
                Invalidate();
            }
        }
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
                return base.Text;
            }

            set
            {

                this.strCaption = value;
                base.Text = value;
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
        public override int Radius
        {
            get { return base.nRadius; }
            set { base.nRadius = value;Invalidate(); }
        }
        [Category("外观")]
        
        public Color PressColor
        {
            get
            {return clr_Press;}
            set{ 
                clr_Press = value;
                         
                Invalidate();
            }
        }
        [Category("外观")]
   
        public Color NormalColor
        {
            get
            {return this.clr_Normal;
            }
            set
            { 
                clr_Normal = value;
                this.sbr_FillRoundRect.Color = value;
                Invalidate();
            }
        }
        [Category("外观")]
   
        public Alignments  CornerAlignment {
            get { return enu_CornerAlignment; }
            set { enu_CornerAlignment = value;  
                if (gp != null)
                {
                    gp.Dispose();
                    gp = null;
                } Invalidate(); }
        }
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set { base.BackColor = value; Invalidate(); }
        }
        [Category("外观")]
     
        public bool UnderLine
        {
            get { return bUnderLine; }
            set { bUnderLine = value;Invalidate(); }
        }
        [Category("外观")]
       
        public bool PureText
        {
            get { return bPureText; }
            set { bPureText = value;
                bUnderLine = false;Invalidate(); }
        }
        [Category("外观")]
        public int UnderLineThickness
        {
            get { return nUnderLineThickness; }
            set { nUnderLineThickness = value;Invalidate(); }
        }
        [Category("外观")]
        public int  UITabHeaderItemGap
        {
            get { return nUITabHeaderItemGap; }
            set { nUITabHeaderItemGap = value; Invalidate(); }
        }
    }

    internal class TapDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            type_ActionList = typeof(TapDesignerActionList);
        }
    }

    internal class TapDesignerActionList : VisualActionList
    {
        private Tap tp = null;
        public TapDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            tp = TargetControl as Tap;
            
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
          AddDesignerActionHeaderItem(APPEARANCE);
            AddDesignerActionPropertyItem("Source","图片");
            AddDesignerActionPropertyItem("ImageSize","图片大小");
            AddDesignerActionPropertyItem("ImagePos","图片坐标");
            AddDesignerActionPropertyItem("Font","字体");
            AddDesignerActionPropertyItem("Text","文本");
            AddDesignerActionPropertyItem("ForeColor","文本颜色");
            AddDesignerActionPropertyItem("Radius","圆角半径");
            AddDesignerActionPropertyItem("PressColor","按下时颜色");
            AddDesignerActionPropertyItem("NormalColor","静态颜色");
            
            AddDesignerActionPropertyItem("CornerAlignment","圆角布置");
            AddDesignerActionPropertyItem("UnderLine","显示下划线");
            AddDesignerActionPropertyItem("PureText","仅显示文本");
            AddDesignerActionPropertyItem("UnderLineThickness","下划线粗细");
            AddDesignerActionPropertyItem("UITabHeaderItemGap","与其它元素间隔");
            
        }
          
        public Size ImageSize
        {
            get { return tp.ImageSize; }
            set {  AssignPropertyValue("ImageSize",value); }
        }
      
        public  Point ImagePos
        {
            get { return tp.ImagePos; }
            set { AssignPropertyValue("ImagePos",value); }
        }
      
        public Image Source
        {
            get { return tp.Source; }
            set
            {
                AssignPropertyValue("Source",value);
            }
        }
        public   Font Font
        {
            get
            {
                return tp.Font;
            }

            set
            {
                AssignPropertyValue("Font",value);
            }
        }

         
       
        public   String Text
        {
            get
            {
                return tp.Text;
            }

            set
            {

                 AssignPropertyValue("Text",value);
            }
        }
        public   Color ForeColor
        {
            get
            {
                return tp.ForeColor;
            }

            set
            {
                AssignPropertyValue("ForeColor",value);
            }
        }
        public   int Radius
        {
            get { return tp.Radius; }
            set {  AssignPropertyValue("Radius",value);}
        }
        
        public Color PressColor
        {
            get
            {return tp.PressColor;}
            set{ 
                AssignPropertyValue("PressColor",value);
            }
        }
       
   
        public Color NormalColor
        {
            get
            {return tp.NormalColor;
            }
            set
            { 
                AssignPropertyValue("NormalColor",value);
            }
        }
        
   
        public Alignments  CornerAlignment {
            get
            {return tp.CornerAlignment;
            }
            set
            { 
                AssignPropertyValue("CornerAlignment",value);
            }
        }
       
      
     
        public bool UnderLine
        {
            get { return tp.UnderLine; }
            set { AssignPropertyValue("Radius", -1); AssignPropertyValue("UnderLine",value);  }
        }
       
       
        public bool PureText
        {
            get { return tp.PureText; }
            set { AssignPropertyValue("Radius", -1); AssignPropertyValue("PureText",value); }
        }
     
        public int UnderLineThickness
        {
            get { return tp.UnderLineThickness; }
            set { AssignPropertyValue("UnderLineThickness",value); }
        }
       
        public int  UITabHeaderItemGap
        {
            get { return tp.UITabHeaderItemGap; }
            set {  AssignPropertyValue("UITabHeaderItemGap",value);  }
        }
    }

}
