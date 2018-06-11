using dotNetLab.Widgets.UIBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms.Design;
using dotNetLab.Widgets.Design;
using System.Windows.Forms;

namespace dotNetLab.Widgets.Container
{
    [ToolboxItem(true),Designer(typeof(BlockContainerDesigner))]
   public class BlockContainer : Container
    {
      protected  Color clrSelected = Color.DodgerBlue;
        BlockUIBinder bbd;
      
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.Size = new System.Drawing.Size(300, 300);
           
        }

        protected override void ProvideUIBinder()
        {
            this.UIBinder = new BlockUIBinder(this);
            bbd = uIBinder as BlockUIBinder;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            UIBinder.BindUI();
        }



        [Category("外观")]
        
        public Color SelectedColor
        {
            get { return clrSelected; }
            set {
                clrSelected = value;
                UIBinder.BindUI();
                Invalidate(); }
        }
        
    }
    internal class BlockContainerDesigner : VisualPanelDesigner
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(BlockContainerDesignerActionList);
        }
    }

    internal class BlockContainerDesignerActionList : ContainerDesignerActionList
    {
        private BlockContainer bc;
       // VisualPanelDesigner vpd;
        public BlockContainerDesignerActionList(IComponent component, ControlDesigner controlDesigner) :
            base(component, controlDesigner)
        {
               bc = this.TargetControl as BlockContainer;
           // vpd = controlDesigner as VisualPanelDesigner;
        }


        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            base.PrepareDesignerActionItemCollection(DIC);
            foreach (DesignerActionItem item in DIC)
            {
                if (item.DisplayName.Equals("刷新布局"))
                {
                    DIC.Remove(item);
                    break;
                }

            }
            AddDesignerActionPropertyItem("SelectedColor","选中时颜色");
        }
        protected override void AddChild()
        {
              AddDesignerChild(typeof(Block));
           // vpd.AddChildCtrl(typeof(Block));
        }
        public Color SelectedColor
        {
            get { return bc.SelectedColor; }
            set
            {
                AssignPropertyValue("SelectedColor",value);
                for (int i = 0; i < vpcd.Control.Controls.Count; i++)
                {
                    AssignPropertyValue(vpcd.Control.Controls[i],"SelectedColor", value);

                }
            }
        }
    }

}
