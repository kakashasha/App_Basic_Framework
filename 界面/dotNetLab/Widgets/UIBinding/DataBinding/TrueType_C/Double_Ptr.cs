using System;
using System.Collections.Generic;
using System.Reflection;

namespace dotNetLab.Widgets.UIBinding
{
    public class Double_Ptr : Pointer
    {
        public Double_Ptr()
        {
            Init(8);
            
        }
        public override void Value<T>(T t)
        {
            Update(Convert.ToDouble(t));
        }
        public override T Value<T>()
        {
           return (T)Convert.ChangeType(FetchDouble(), typeof(T));
        }
        
    
    }
}