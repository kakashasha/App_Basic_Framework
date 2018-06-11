using System;
using System.Text;
using System.Runtime.InteropServices;
namespace dotNetLab.Widgets.UIBinding
{
    public abstract class Pointer : IDisposable
    {
        public delegate void EndModifyCallback();
        public EndModifyCallback EndModify = null;
        int nBufferSize = -1;
        IntPtr ptr;
        byte[] bytArrTemp, bytArrBuf;
        /* Type type_Int = null;
         Type type_Float = 
         Type type_Double = null;
         Type type_Long = null;*/

        protected void Init(int nBufferSize)
        {
            this.nBufferSize = nBufferSize;
            ptr = Marshal.AllocHGlobal(nBufferSize);
            bytArrBuf = new byte[nBufferSize];
            /* type_Int = typeof(int);
             type_Long = typeof(long);
             type_Float = typeof(float);
             type_Double = typeof(double);*/

        }
        protected void Update(double t)
        {
            bytArrTemp = BitConverter.GetBytes(t);
            for (int i = 0; i < bytArrTemp.Length; i++)
            {

                Marshal.WriteByte(ptr, i, bytArrTemp[i]);
            }

        }
        protected void Update(float t)
        {
            bytArrTemp = BitConverter.GetBytes(t);
            for (int i = 0; i < bytArrTemp.Length; i++)
            {
                Marshal.WriteByte(ptr, i, bytArrTemp[i]);
            }

        }
        protected void Update(int t)
        {
            bytArrTemp = BitConverter.GetBytes(t);
            for (int i = 0; i < bytArrTemp.Length; i++)
            {
                Marshal.WriteByte(ptr, i, bytArrTemp[i]);
            }
        }
        protected void Update(long t)
        {
            bytArrTemp = BitConverter.GetBytes(t);
            for (int i = 0; i < bytArrTemp.Length; i++)
            {
                Marshal.WriteByte(ptr, i, bytArrTemp[i]);
            }
        }
        protected double FetchDouble()
        {
            for (int i = 0; i < bytArrBuf.Length; i++)
            {
                this.bytArrBuf[i] = Marshal.ReadByte(this.ptr, i);
            }
            return BitConverter.ToDouble(bytArrBuf, 0);
        }
        protected float FetchFloat()
        {
            for (int i = 0; i < bytArrBuf.Length; i++)
            {
                this.bytArrBuf[i] = Marshal.ReadByte(this.ptr, i);
            }
            return BitConverter.ToSingle(bytArrBuf, 0);
        }
        protected int FetchInt()
        {
            for (int i = 0; i < bytArrBuf.Length; i++)
            {
                this.bytArrBuf[i] = Marshal.ReadByte(this.ptr, i);
            }
            return BitConverter.ToInt32(bytArrBuf, 0);
        }
        protected long FetchLong()
        {
            for (int i = 0; i < bytArrBuf.Length; i++)
            {
                this.bytArrBuf[i] = Marshal.ReadByte(this.ptr, i);
            }
            return BitConverter.ToInt64(bytArrBuf, 0);
        }

        public abstract void Value<T>(T t);

        public abstract T Value<T>();

        ~Pointer()
        {
            Dispose();
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(ptr);
        }


    }

}
