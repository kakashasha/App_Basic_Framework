using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using dotNetLab.Widgets.Design;

namespace dotNetLab.Widgets
{
    [Designer(typeof(DirectionDesigner)), ToolboxBitmap(typeof(Cursor))]
    public class Direction :Circle
    {
        bool bWhitePattern = false;
        Alignments enm_ArrowAlignment = Alignments.Right;
        
        void AssignImageSource(Alignments alg,bool bWhitePattern)
        {
            switch (alg)
            {
                case Alignments.Right: 
                    if(!bWhitePattern)
                    Source = UI.RightArrow_DodgerBlue_Thin;
                    else
                        Source = UI.RightArrow_White_Thin;
                    break;
                case Alignments.Left: Source = UI.LeftArrow_DodgerBlue_Thin;
                    if(!bWhitePattern)
                        Source = UI.LeftArrow_DodgerBlue_Thin;
                    else
                        Source = UI.LeftArrow_White_Thin;
                    break;
                    case Alignments.Down:
                        if(!bWhitePattern)
                            Source = UI.DownArrow_DodgerBlue_Thin;
                        else
                            Source = UI.DownArrow_White_Thin;
                        break;
                        case Alignments.Up: 
                            if(!bWhitePattern)
                                Source = UI.UpArrow_DodgerBlue_Thin;
                            else
                                Source = UI.UpArrow_White_Thin;
                            break;
            }
        }
        

        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.Source = UI.RightArrow_DodgerBlue_Thin;
            this.Size = new System.Drawing.Size(50, 50);
            this.Fill = false;
            this.BorderColor = Color.Gray;
            BorderThickness = 2;
            this.WhichShap = 6;
            imageSize = new SizeF(25, 25);
            this.CenterImage = true;
            NeedEffect = false;
            MouseDownColor = Color.Gray;
        }
        
        [Category("外观")]
        public bool WhitePattern
        {
            get
            {
                return bWhitePattern;
            }
            set
            {
                bWhitePattern = value;
                
                AssignImageSource(ArrowAlignment,value);
            }
        }
        [Category("外观")]
        public Alignments ArrowAlignment
        {
            get { return enm_ArrowAlignment; }
            set
            {
                enm_ArrowAlignment = value;
                AssignImageSource(value, WhitePattern);
                Invalidate();
            }
        }
    }
     internal class DirectionDesigner : VisualDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(DirectionDesignerActionList);
        }
    }

    internal class DirectionDesignerActionList : VisualActionList
    {
        private Direction txb;
        public DirectionDesignerActionList (IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            txb = this.TargetControl as Direction;
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            for (int i = 0; i < DIC.Count; i++)
            {
                DIC.RemoveAt(i);
            } 
              AddDesignerActionHeaderItem(APPEARANCE);
            
            AddDesignerActionPropertyItem("WhitePattern","白色主题");
            AddDesignerActionPropertyItem("BorderColor","边框颜色");
            AddDesignerActionPropertyItem("ArrowAlignment","指向");
           
        }

        public Color BorderColor
        {
            get { return txb.BorderColor; }
            set{AssignPropertyValue("BorderColor",value);}
        }
      
        public bool WhitePattern
        {
            get
            {
                return  txb.WhitePattern;
            }
            set
            {
                 
                AssignPropertyValue("WhitePattern",value);
            }
        }
        
        public Alignments ArrowAlignment
        {
            get { return txb.ArrowAlignment; }
            set
            {
                AssignPropertyValue("ArrowAlignment",value);
            }
        }
       
        
    }
}
