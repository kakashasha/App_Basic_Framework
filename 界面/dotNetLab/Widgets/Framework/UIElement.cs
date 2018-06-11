using dotNetLab.Appearance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Widgets.UIBinding;
using System.Reflection;
using dotNetLab.Forms;
using System.Threading;

namespace dotNetLab.Widgets
{
    [ToolboxItem(false)]
    public class UIElement : UserControl
    {

        protected SizeF szf_Text;
   
        public delegate void DoAnimationCall();
        private StringBuilder sbTextOld;
        public delegate void BindablePropertyChangedCallback(Control ctrl,String str);

        public event BindablePropertyChangedCallback BindablePropertyChanged;
        public DoAnimationCall DoAnimation = null;
        protected String strCaption;
        protected SolidBrush sbrh_Text;
        protected float x_Text;
        protected float y_Text;
        protected Font fnt_cmp;
        protected Point pnt_Modify;
        
        public UIElement()
        {

            this.BackColor = Color.Transparent;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
            this.UpdateStyles();
            pnt_Modify = new Point();
            Prepare();
            DoAnimation = InvokeAnimation;
           
        }
        UIElementBinderInfo dataBindingInfo;
        UIElementDataBinding UIElementBinderObject;
        protected virtual void InvokeAnimation()
        {

        }

        protected virtual void Prepare()
        {
            prepareData();
            prepareAppearance();
            prepareCtrls();
            prepareEvents();

            UnitCtrls();
           
               
        }

        public void AddControl(Control ctrl)
        {
            this.Controls.Add(ctrl);
        }
        protected virtual void UnitCtrls() { }
        protected virtual void prepareEvents() { }
        protected virtual void prepareAppearance() { }
        protected virtual void prepareData() { }
        protected virtual void prepareCtrls() { }
        


        public void YaHeiFont(int nFontSize)
        {
            this.Font = new Font(Fonts.YAHEI, nFontSize);
            
        }
        public Font GetYaHeiFont(int nFontSize)
        {
            return new Font(Fonts.YAHEI, nFontSize); 
        }
        public Font DengXianFont(int nFontSize)
        {
            return new Font(Fonts.DENGXIANG, nFontSize);
        }
        protected virtual void CenterText(Graphics xg, SolidBrush sbrh)
        {


            MessureText(xg);

            xg.DrawString(Text, Font, sbrh, x_Text, y_Text);


        }
        protected virtual void CenterText(Graphics xg, SolidBrush sbrh, bool bTextAnti_Alias)
        {

            if (bTextAnti_Alias)
                xg.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            CenterText(xg, sbrh);


        }
        protected void MessureText(Graphics gx)
        {

            szf_Text = gx.MeasureString(Text, Font);
            x_Text = this.Width / 2 - szf_Text.Width / 2;
            y_Text = this.Height / 2 - szf_Text.Height / 2;
        }
        protected Color PressEffect(Color _c, int nValue, bool bIsAdd)
        {
            int _Red = _c.R;
            int _Green = _c.G;
            int _Blue = _c.B;
            if (bIsAdd)
            {
                if (_c.R + nValue >= 0 && _c.R + nValue < 255)
                    _Red = _c.R + nValue;

                if (_c.G + nValue >= 0 && _c.G + nValue < 255)

                    _Green = _c.G + nValue;

                if (_c.B + nValue >= 0 && _c.B + nValue < 255)
                    _Blue = _c.B + nValue;
            }
            else
            {
                if (_c.R - nValue >= 0)
                    _Red = _c.R - nValue;

                if (_c.G - nValue >= 0)

                    _Green = _c.G - nValue;

                if (_c.B - nValue >= 0)
                    _Blue = _c.B - nValue;
            }

            return Color.FromArgb(_Red, _Green, _c.B);
        }
        [BrowsableAttribute(false)]
        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }

            set
            {
                base.RightToLeft = value;
            }
        }

        public new bool Enabled
        {
            get
            {

                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                Refresh();
            }
        }
        [Category("外观")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public UIElementBinderInfo DataBindingInfo
        {
            get
            {

                return dataBindingInfo;
            }
            set
            {
                dataBindingInfo = value;
                if (dataBindingInfo != null)
                    dataBindingInfo.ThisControl = this;
               
                
            }
        }
       
        [Browsable(false)]
        public UIElementDataBinding UIElementBinders
        {
            get { return UIElementBinderObject; }
            set { UIElementBinderObject = value; }

        }
        [Browsable(false)]
        public virtual String MainBindableProperty
        {
            get { return Text; }
            set { 
                if (sbTextOld == null)
                {
                    sbTextOld = new StringBuilder();
                }
                if (!sbTextOld.ToString().Equals(value))
                {
                     Text = value;
                  
                    sbTextOld.Clear();
                    sbTextOld.Append(Text);
                    if (BindablePropertyChanged != null)

                       BindablePropertyChanged(this, Text);
                }
                }
                
        }

    }

}
