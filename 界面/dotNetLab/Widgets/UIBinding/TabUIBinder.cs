using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Widgets.UIBinding
{
   public  class  TabUIBinder : UIBinder
    {
         bool Vertial = false;
      
        public event  EventHandler TabItemClickRule = null;
        public event EventHandler AttachRule = null;

        public TabUIBinder(Control Container)
        {
            this.ctrlWhichContainer = Container;
        }

        

        protected override void AlignmentRule()
        {
            if (ctrlWhichContainer.Width > ctrlWhichContainer.Height)
                Vertial = false;
            else if(ctrlWhichContainer.Width< ctrlWhichContainer.Height)
                Vertial = true;
            
           if (!Vertial)
            {
                if (controlItems.Count > 1)
                {
                    //When use Round Corner
                  //  bool bUsedRadius = false;
                    Tap thi  =  controlItems[0] as Tap;
                    TabHeader thd = thi.Parent as TabHeader;
                    //thi.NormalColor = thd.ItemNormalColor;
                    //thi.PressColor = thd.ItemNormalColor;
                    if (thi.UnderLine)
                        UIAlignmentGap = thi.UITabHeaderItemGap;
                    //if (thi.Radius > 0)
                    //{
                    //    bUsedRadius = true;

                    //}
                    int nCount = controlItems.Count;
                    for (int i = 1; i < nCount; i++)
                    {
                        //thi = controlItems[i] as Tap;
                        //thi.NormalColor = thd.ItemNormalColor;
                        //thi.PressColor = thd.ItemNormalColor;
                        controlItems[i].Location = new System.Drawing.Point(controlItems[i - 1].Location.X + controlItems[i - 1].Width + UIAlignmentGap,
                                      controlItems[0].Location.Y);
                        //if (bUsedRadius)
                        //{

                        //    if (i != nCount - 1)
                        //    {
                        //        thi.CornerAlignment = Alignments.Up;
                        //        thi.Radius = 1;
                        //    }
                        //    else
                        //    {
                        //        thi.CornerAlignment = Alignments.Right_UP;
                        //        thi.Radius = (controlItems[0] as Tap).Radius;
                        //    }
                        //}
                        
                      
                            
                    }
                }
                this.ctrlWhichContainer.Size = new Size(ctrlWhichContainer.Width, controlItems[0].Height + 1);
                Control c = controlItems[controlItems.Count - 1];
                ctrlWhichContainer.Width = c.Location.X + c.Width;
            }
           else
            {
                if (controlItems.Count > 1)
                {
                    bool bNeedAdjustWidth = false;
                    Tap thi = controlItems[0] as Tap;
                    if (thi.Radius > 0)
                    {
                        bNeedAdjustWidth = true;
                      
                    }
                    for (int i = 1; i < controlItems.Count; i++)
                    {
                        if (bNeedAdjustWidth)
                        {
                            thi = controlItems[i] as Tap;

                            thi.CornerAlignment = Alignments.Up;

                            thi.Radius = 1;
                           
                        }

                        controlItems[i].Location = new System.Drawing.Point(controlItems[i - 1].Location.X  ,
                            controlItems[i - 1].Location.Y + controlItems[i - 1].Height + UIAlignmentGap);

                    }
                }
                this.ctrlWhichContainer.Size = new Size(controlItems[0].Width + 1,controlItems[0].Height);
                Control c = controlItems[controlItems.Count - 1];
                ctrlWhichContainer.Height = c.Location.X + c.Height;
            }

            for (int i = 0; i < controlItems.Count; i++)
            {
                try
                {
                    controlItems[i].Click -= TabItemClickRule;
                }
                catch (Exception e)
                {

                }
                controlItems[i].Click += TabItemClickRule;

            }
        }

       
         
        public virtual void DefaultClickRule(Object sender,Color clrNormal,Color clrActivateColor)
        {
            Tap btn = lastChild as Tap;
            if(lastChild!=null)
                btn.NormalColor = clrNormal;
            ;
            this.lastChild = sender as Control;
            (this.lastChild as Tap).NormalColor =clrActivateColor;
            if (AttachRule != null)
                AttachRule(sender, null);
        }
        public virtual void DefaultClickRule(Object sender )
        {
            TabHeader thd = (sender as Control).Parent as TabHeader;
            DefaultClickRule(sender, thd.ItemNormalColor,thd.ItemPressColor);
        }
    }
}
