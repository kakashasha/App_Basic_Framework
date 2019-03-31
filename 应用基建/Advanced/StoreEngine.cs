
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace dotNetLab.Vision.Halcon
{
    //将数据存储于文件中
    public class StoreEngine
    {
        //用于描述数据类型的列表

        public List<StoreItemInfo> StoreItemInfoSet;
        //存储的文件名
        private String fileName;
        //用于操作存储文件的流对象
        public FileStream ThisFileStream;
        public BinaryReader ThisBr;
        public BinaryWriter ThisWr;
        //记录当前存储项目的索引。
        int StoreItemIndex = 0;
        //赋予文件名即已经打开了文件并初始化了 ThisFileStream; ThisBr; ThisWr;
        public string FileName { get { return fileName; } set { fileName = value; LoadNewFile(value); } }

        public StoreEngine()
        {
            StoreItemInfoSet = new List<StoreItemInfo>();
        }
        //添加一个数据描述项
        public void AddStoreItem(Type type)
        {
            StoreItemInfoSet.Add(new StoreItemInfo(type));
        }
        //存储一个数据
        public void StoreItem(BinaryWriter bw, Object obj)
        {
            try
            {  //以使再次存储不报错，将索引置0
                if (StoreItemIndex == StoreItemInfoSet.Count)
                    StoreItemIndex = 0;
                StoreItemInfoSet[StoreItemIndex].xvalue = obj;
                StoreItemInfoSet[StoreItemIndex++].Store(bw);
            }
            catch (Exception ex)
            {
                Tipper.Error = "At StoreEngine.StoreItem :" + ex.Message;
            }

        }

        public void StoreItem(Object obj)
        {
            StoreItem(ThisWr, obj);
        }
        public Object FetchItem()
        {
            return FetchItem(ThisBr);
        }
        public Object FetchItem(BinaryReader br)
        {
            try
            {
                StoreItemInfoSet[StoreItemIndex].Fetch(br);
                return StoreItemInfoSet[StoreItemIndex++].xvalue;
            }
            catch (Exception ex)
            {
                Tipper.Error = "At StoreEngine.FetchItem :" + ex.Message;
                return null;
            }
        }
        void LoadNewFile(String strFileName)
        {
            fileName = strFileName;

            Action<Object> DisposeStream = (Object obj) =>
            {
                if (obj != null)
                {
                    if (obj is Stream)
                    {
                        Stream sr = obj as Stream;
                        sr.Close();
                        sr.Dispose();
                    }
                    else
                    {
                        obj.GetType().GetMethod("Close").Invoke(obj, null);
                        IDisposable disposable = obj as IDisposable;
                        disposable.Dispose();
                    }

                }

            };
            DisposeStream(ThisFileStream);
            DisposeStream(ThisBr);
            DisposeStream(ThisWr);
            ThisFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            ThisBr = new BinaryReader(ThisFileStream);
            ThisWr = new BinaryWriter(ThisFileStream);

        }

        public void SaveStreamBy(BinaryWriter bw)
        {

            ThisWr = bw;
        }
        public void ReadStreamBy(BinaryReader br)
        {
            ThisBr = br;
    

        }
    }
    public class StoreItemInfo
    {

        public Type type;
        public Object xvalue;
        double lf;
        public static Type intType = typeof(int);
        public static Type doubleType = typeof(double);
        public static Type longType = typeof(long);
        public static Type stringType = typeof(string);
        public static Type boolType = typeof(bool);
        public static Type floatType = typeof(float);
        public static Type byteArrayType = typeof(byte[]);

        public StoreItemInfo(Type type)
        {
            this.type = type;

        }

        public void Store(BinaryWriter bw)
        {
            if (intType == type)
            {
                bw.Write((int)xvalue);
            }
            if (doubleType == type)
            {
                bw.Write((double)(xvalue));
            }
            if (longType == type)
            {
                bw.Write(Convert.ToInt64(xvalue));
            }
            if (stringType == type)
            {
                bw.Write(Convert.ToString(xvalue));
            }
            if (boolType == type)
            {
                bw.Write(Convert.ToBoolean(xvalue));
            }
            if (floatType == type)
            {
                bw.Write(Convert.ToSingle(xvalue));
            }
            if (byteArrayType == type)
            {
                byte[] buffer = (byte[])xvalue;
                bw.Write(buffer.Count());
                bw.Write(buffer, 0, buffer.Count());
            }
        }
        public void Fetch(BinaryReader br)
        {
            if (intType == type)
            {
                xvalue = br.ReadInt32();
            }
            if (doubleType == type)
            {
                xvalue = br.ReadDouble();
                lf = (double)xvalue;
            }
            if (longType == type)
            {
                xvalue = br.ReadInt64();
            }
            if (stringType == type)
            {
                xvalue = br.ReadString();
            }
            if (boolType == type)
            {
                xvalue = br.ReadBoolean();
            }
            if (floatType == type)
            {
                xvalue = br.ReadSingle();
            }
            if (byteArrayType == type)
            {
                int nCount = br.ReadInt32();
                xvalue = new byte[nCount];
                byte[] buf = (byte[])xvalue;
                br.Read(buf, 0, nCount);

            }
        }
    }
}
