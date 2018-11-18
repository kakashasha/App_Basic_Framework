using System;
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