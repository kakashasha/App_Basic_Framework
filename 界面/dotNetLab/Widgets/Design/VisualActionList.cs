using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace dotNetLab.Widgets.Design
{
    public abstract  class VisualActionList/*VisualActionList */: DesignerActionList
    {
        protected Object TargetControl;
       protected ControlDesigner designer = null ;

        protected readonly string APPEARANCE = "外观";

        private DesignerActionItemCollection DesignerActionItems;
       // Flat flt;
        public DesignerActionUIService UIActionservice;

        public void AssignPropertyValue(Control c,string propertyName, object obj)
        {
            TypeDescriptor.GetProperties(c)[propertyName].SetValue(c, obj);
            UIActionservice.Refresh(this.Component);
        }
        public VisualActionList(IComponent component,ControlDesigner controlDesigner) :base(component)
        {
           // EditorServiceContext.EditValue(this._designer, base.Component, "Image");
            TargetControl = component;
            UIActionservice = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService; 
            this.designer = controlDesigner;
             
        }
        
        private PropertyDescriptor GetPropertyByName(string name)
        {
            return TypeDescriptor.GetProperties(TargetControl)[name];
        }
        //添加分类头(显示‘Catgory’)
        protected void AddDesignerActionHeaderItem(String name)
        {
            DesignerActionItems.Add(new DesignerActionHeaderItem(name));
        }

        protected void AddDesignerActionPropertyItem(String PropertyName, String Display, String Catgroy)
        {
            DesignerActionItems.Add(new DesignerActionPropertyItem(PropertyName,Display, Catgroy));
        }
        // Catgroy == '外观'
        protected void AddDesignerActionPropertyItem(String PropertyName, String Display)
        {
            AddDesignerActionPropertyItem(PropertyName, Display,APPEARANCE);
        }

        protected void AddDesignerActionMethodItem(String MethodName,String Display ,string Catgroy  )
        {
            DesignerActionItems.Add(new DesignerActionMethodItem(this,MethodName,Display, Catgroy));
        }
        // Catgroy == '外观'
        protected void AddDesignerActionMethodItem(String MethodName,String Display    )
        {
            AddDesignerActionMethodItem(MethodName, Display, APPEARANCE);
        }

        // protected Method
        protected void AssignPropertyValue(string name,Object Value)
        {
            GetPropertyByName(name).SetValue(TargetControl,Value);
            UIActionservice.Refresh(this.Component);

        }
        public abstract  void PrepareDesignerActionItemCollection(DesignerActionItemCollection DIC);
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            if(DesignerActionItems !=null)
                DesignerActionItems.Clear();
       
            DesignerActionItems = new DesignerActionItemCollection();
            PrepareDesignerActionItemCollection(DesignerActionItems);
            return DesignerActionItems;
        }
    }
}