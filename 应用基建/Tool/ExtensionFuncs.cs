using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Common
{
    public static class ExtensionFuncs
    {
        public static void SetIndexByString(this ComboBox cmbx, String str)
        {
            int nIndex = cmbx.Items.IndexOf(str);
            if (nIndex > -1)
            {
                cmbx.SelectedIndex = nIndex;
            }
            //string[] strarr = new string[5] ;
            //strarr.IndexOf("");
              
        }
        public static int IndexOf<T>(this Array array, T obj)
        {
          return Array.IndexOf<T>((T[])array,  obj);
        }

        
        public static String ConnectAll<T>(this IEnumerable<T> array,String gapStr)
        {
           int n =  array.Count();
            StringBuilder sb = new StringBuilder();
            if (n > 0)
            {
                IEnumerator en = array.GetEnumerator();
                while (en.MoveNext())
                {
                    String str = en.Current.ToString();
                    sb.AppendFormat("{0}{1}", str, gapStr);
                }
                String s = sb.ToString().Replace(gapStr,"");
                return s;
            } 
            return null;
        }
        public static Binding AddBinding(this Control c, String thisCtrlProperty
            , Object AttachingObject, String AttachingObjectProperty, bool formattingEnabled = false,
            DataSourceUpdateMode dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            )
        {
            c.DataBindings.Add(new Binding(thisCtrlProperty, AttachingObject,
                AttachingObjectProperty, formattingEnabled, dataSourceUpdateMode));
            return c.DataBindings[c.DataBindings.Count - 1];
        }
        
        public static Binding AddTextBinding(this Control c, Object AttachingObject, String AttachingObjectProperty,
             bool formattingEnabled = false, DataSourceUpdateMode dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged)
        {
            return AddBinding(c, "Text", AttachingObject, AttachingObjectProperty, formattingEnabled, dataSourceUpdateMode);
        }

        public static Form GetParentForm(this Control c)
        {

          return   InternalFindParentForm(c);
        }

        static Form  InternalFindParentForm(Control c)
        {
            if (c  is Form)
                return c as Form;
            else
            {
               return InternalFindParentForm( c.Parent);
            }
        }
        public static Binding AddTextBinding(this Control c , String AttachingObjectProperty,
            bool formattingEnabled = false, DataSourceUpdateMode dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged)
        {

           Form frm =  InternalFindParentForm(c);
            return AddBinding(c, "Text", frm, AttachingObjectProperty, formattingEnabled ,dataSourceUpdateMode);
        }

        public static  void FactoryUse(this NumericUpDown c,int DecimalPlaces = 2, HorizontalAlignment alignment = HorizontalAlignment.Center  , bool Hexadecimal = false)
        {
            c.Minimum = decimal.MinValue;
            c.Maximum = decimal.MaxValue;
            c.DecimalPlaces = DecimalPlaces;
            c.Hexadecimal = Hexadecimal;
            c.TextAlign = alignment;
        }

        public static Binding AddValueBinding(this NumericUpDown c, Object AttachingObject, String AttachingObjectProperty,
             bool formattingEnabled = false)
        {
           return  AddBinding(c, "Value", AttachingObject, AttachingObjectProperty, formattingEnabled);
        }

        public static Binding AddValueBinding(this ProgressBar c, Object AttachingObject, String AttachingObjectProperty,
             bool formattingEnabled = false)
        {
            return AddBinding(c, "Value", AttachingObject, AttachingObjectProperty, formattingEnabled);
        }
        public static String  TrimText(this Control c)
        {
            return c.Text.Trim();
        }
        public static int TrimIntText(this Control c)
        {
            int n = 0;
            int.TryParse(c.Text.Trim(),out n);
            return n;
        }

        public static int ToInt(this String str)
        {

            try
            {
                return int.Parse(str);
            }
            catch (Exception ex)
            {

                throw;
              
            }
        }
        public static float ToFloat(this String str)
        {

            try
            {
                return float.Parse(str);
            }
            catch (Exception ex)
            {

                throw;

            }
        }
        public static double ToDouble(this String str)
        {

            try
            {
                return double.Parse(str);
            }
            catch (Exception ex)
            {

                throw;

            }
        }

    }

   
}


// public static int IndexOf(this Array array,Object obj)
// {
//    Type type = obj.GetType();
//    int nIndex = -1;
//    if (type == typeof(int))
//    {
//        nIndex =  Array.IndexOf<int>( (int[]) array,  (int) obj );
//    }
//    else if (type == typeof(double))
//    {
//        nIndex = Array.IndexOf<double>((double[])array, (double)obj);
//    }
//    else if (type == typeof(String))
//    {
//        nIndex = Array.IndexOf<String>((String[])array, obj.ToString());
//    }
//    else if (type == typeof(bool))
//    {
//        nIndex = Array.IndexOf<bool>((bool[])array, (bool)obj);
//    }
//    else if (type == typeof(float))
//    {
//        nIndex = Array.IndexOf<float>((float[])array, (float)obj);
//    }
//    else if (type == typeof(uint))
//    {
//        nIndex = Array.IndexOf<uint>((uint[])array, (uint)obj);
//    }
//    else if (type == typeof(long))
//    {
//        nIndex = Array.IndexOf<long>((long[])array, (long)obj);
//    }
//    return nIndex;
//}