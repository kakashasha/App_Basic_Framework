using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace dotNetLab.Data.Network
{

    //  baseSerialPort.ReadBufferSize = baseSerialPort.WriteBufferSize = (int)BufferSize;
    public abstract class PLCBase
    {

        protected System.IO.Ports.SerialPort _rs232;
        protected Encoding en = Encoding.ASCII;
        protected Thread thd_Main;     
        public byte[] buffer;
        public System.IO.Ports.SerialPort ThisSerialPort
        {
            get
            {
                return _rs232;
            }
            set
            {
                _rs232 = value;

            }
        }
        public PLCBase()
        {
            BaudRate = 9600;
            this.BufferSize = 128;
            PortName = "COM1";
            Parity = System.IO.Ports.Parity.None;
            DataBits = 8;
            StopBits = System.IO.Ports.StopBits.One;

        }
        public abstract bool Open();
        public abstract bool Close();

       
        public string PortName { get; set; }
      
        public int BaudRate { get; set; }
        
        public System.IO.Ports.Parity Parity { get; set; }
       
        public int DataBits { get; set; }
    
        public System.IO.Ports.StopBits StopBits { get; set; }        
        public int BufferSize { get; set; }
        public Encoding ThisEncoding { get { return en; } set { en = value; } }
       public byte [] DecodeHexString(String str)
        {
            String[] temp = str.Split(new char[] { ' ' });
            int nIndex = 0;
            byte[] bytArr = new byte[temp.Length];
            foreach (var item in temp)
            {
                bytArr[nIndex++] =
                    byte.Parse(item,
                    System.Globalization.NumberStyles.HexNumber);
            }
            return bytArr;
        }
        protected void DiscardIOBuffer()
        {

            _rs232.DiscardOutBuffer();
            _rs232.DiscardInBuffer();
        }

        public byte[] ProvideBytes(ushort ust)
        {
            return BitConverter.GetBytes(ust);
        }
        public byte[] ProvideBytes(String str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        public byte[] ProvideBytes(short srt)
        {
            return BitConverter.GetBytes(srt);
        }
        public byte[] ProvideBytes(uint un)
        {
            return BitConverter.GetBytes(un);
        }
        public byte[] ProvideBytes(int n)
        {
            return BitConverter.GetBytes(n);
        }
        public object ProvideNum(byte[] bytArr, int nStartIndex, int nBytesConsist, bool isU)
        {

            switch (nBytesConsist)
            {
                case 2:
                    if (isU)
                        return BitConverter.ToUInt16(bytArr, nStartIndex);
                    else
                        return BitConverter.ToInt16(bytArr, nStartIndex);
                case 4:
                    if (isU)
                        return BitConverter.ToUInt32(bytArr, nStartIndex);
                    else
                        return BitConverter.ToInt32(bytArr, nStartIndex);
            }
            return null;
        }
        public String ProvideString(byte[] bytArr, int nIndex, int nCount)
        {

            return Encoding.ASCII.GetString(bytArr, nIndex, nCount);
        }
        ~PLCBase()
        {
            this.Close();
        }
    }
}
