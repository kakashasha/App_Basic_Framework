using System;
using System.ComponentModel;
using System.ComponentModel.Design;
 
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace dotNetLab.Widgets.Design
{
    public abstract  class VisualDesigner : ControlDesigner
    {
        protected Type type_ActionList = null;
    public bool EableUIGlyph = false;
    protected UIGlyph uiGlyph;
    DesignerActionListCollection actionList;
    protected abstract void ProvideType();
    private Adorner myAdorner;


    public void AddCtrlForParent(Type type_Control)
    {
        IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
        Control ctrl = (Control)designerHost.CreateComponent(type_Control);
        Control.Parent.Controls.Add(ctrl);
    }
    public void AddChildCtrl(Type type_Control)
    {
        IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
        Control ctrl = (Control)designerHost.CreateComponent(type_Control);
        Control.Controls.Add(ctrl);
    }
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            if (actionList == null)
            {
              
                if (type_ActionList == null)
                    ProvideType();
                actionList = new DesignerActionListCollection();
                actionList.Add((DesignerActionList)System.Activator.CreateInstance(type_ActionList, this.Component, this));
            }

            return actionList;
        }
    }
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);
        PrepareGlyph(component);
        this.ProvideType();
    }
    public EventHandler ClickHandler
    {
        get
        {
            return uiGlyph.uiBehavior.Click;
        }
        set
        {
            uiGlyph.uiBehavior.Click += value;
        }
    }
    protected virtual void PrepareGlyph(IComponent component)
    {
        if (EableUIGlyph)
        {
            myAdorner = new Adorner();
            this.BehaviorService.Adorners.Add(myAdorner);
            this.uiGlyph = new UIGlyph(BehaviorService, (Control)component);
            myAdorner.Glyphs.Add(uiGlyph);
        }
    }
}
}
