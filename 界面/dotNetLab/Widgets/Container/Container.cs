using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using dotNetLab.Widgets.Design;
using dotNetLab.Widgets.UIBinding;

namespace dotNetLab.Widgets.Container
{
    [ToolboxItem(false),Designer(typeof(ContainerDesigner))]
    public abstract class  Container :CanvasPanel
    {

 
       protected UIBinder uIBinder;
        public  UIBinder UIBinder
        {
            get { return uIBinder; }
            set { uIBinder = value; Refresh(); }
        }
        protected abstract void ProvideUIBinder();
        protected override void prepareCtrls()
        {
            base.prepareCtrls();
            this.Size = new Size(200, 200);
            this.NormalColor =  Color.Transparent;
            ProvideUIBinder();
        }
      
    }
    internal class  ContainerDesigner : VisualPanelDesigner 
    {
        protected override void ProvideType()
        {
            this.type_ActionList = typeof(VisualActionList);
        }
    }

    internal class  ContainerDesignerActionList : CanvasPanelDesignerActionList
    {
        protected VisualPanelDesigner vpcd;
        public ContainerDesignerActionList(IComponent component, ControlDesigner controlDesigner) : base(component, controlDesigner)
        {
            
            vpcd = this.designer as VisualPanelDesigner;
        }

        public override void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC)
        {
            base.PrepareDesignerActionItemCollection(DIC);
            AddDesignerActionMethodItem("AddChild", "添加项");
            AddDesignerActionMethodItem("RefreshLayout", "刷新布局");
        }
        protected void AddDesignerChild(Type typeChild)
        {
            vpcd.AddChildCtrl(typeChild);
        }
        protected virtual void AddChild()
        {
            
        }
        protected virtual void RefreshLayout()
        {
            Container thr = vpcd.Control as Container;
            thr.UIBinder.BindUI();
        }
    }
  }