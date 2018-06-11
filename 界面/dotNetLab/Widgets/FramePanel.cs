using System;
 
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
 
using System.Windows.Forms;
using System.Windows.Forms.Design;
using dotNetLab.Widgets.Container;
using dotNetLab.Widgets.Design;

namespace dotNetLab.Widgets
{
    [Designer(typeof(FramePanelDesigner)),
        ToolboxBitmap(typeof(System.DirectoryServices.DirectoryEntry))]
    public class FramePanel : CanvasPanel
    {
        int nWhichNeedToEdit = 0;
        protected override void prepareAppearance()
        {
            base.prepareAppearance();
            this.NormalColor  = Color.Transparent;
        }
       
        [Category("外观")]
        public int WhichNeedToEdit
        {
            get { return nWhichNeedToEdit; }
            set { nWhichNeedToEdit = value;
                GetEditPanel(value);
                Invalidate(); }
        }

        public void GetEditPanel(int nIndex)
        {
            
            foreach (Control control in Controls)
            {
                if(control.Tag!= null)
                { 
                if (control.Tag.Equals(nIndex))
                {
                    control.BringToFront();
                    break;
                }
                    }
            }


        }

    }

    internal class FramePanelDesigner : VisualPanelDesigner
    {
        protected override void ProvideType()
        {
            type_ActionList = typeof(FramePanelDesignerActionList);
        }
    }

    internal class FramePanelDesignerActionList : CanvasPanelDesignerActionList
    {
        private FramePanel fp;
        private VisualPanelDesigner pcd;
        public FramePanelDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            fp = TargetControl as FramePanel;
            pcd = this.designer as VisualPanelDesigner;

        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            AddDesignerActionMethodItem("GenerateItem", "添加项");
            AddDesignerActionPropertyItem("WhichNeedToEdit", "需要编辑的容器");
            base.PrepareDesignerActionItemCollection(DIC);
            
            
        }

       

      
        public int WhichNeedToEdit
        {
            get { return fp.WhichNeedToEdit; }
            set
            {
                //SetPropertyValue(pcd.Control.Controls[value],"BringToFront",true);
                AssignPropertyValue("WhichNeedToEdit", value);
            }
        }
        public void GenerateItem()
        {
            Control c = pcd.AddChildCtrl(typeof(CanvasPanel));

            c.Dock = DockStyle.Fill;
            int nCount = pcd.Control.Controls.Count;
            c.Tag = nCount - 1;
            //SetPropertyValue(c, "Tag", (nCount - 1).ToString());

        }


    }
     
}
