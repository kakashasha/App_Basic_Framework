using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace dotNetLab.Networking
{
   public abstract class TCPBase
    {
        
        public Action<int, byte[]> Route, StoreDataLen;
        public Func<byte[], int> FetchDataLen;
        public Func<int> GetConentStartIndex;
        
        protected bool bEndNetwork = false;
        protected int nPort = 8040;
        protected String strErrorInfo = null;
        protected IPAddress ServerIP;
        protected Encoding en = Encoding.UTF8;
        public System.Text.Encoding TextEncode
        {
            get { return en; }
            set { en = value; }
        }
        protected Thread thd_Main;
        protected const byte SPLITMARK = 94;
        protected uint nBufferSize;
        protected int nLoopGapTime;
        private String strIP;

        protected TCPBase()
        {
            FetchDataLen = (byts) =>
            {
                byte[] byt_Len = new byte[4];
                for (int i = 1; i < 4; i++)
                {
                    byt_Len[i - 1] = byts[i];
                }
                int byteNum = BitConverter.
                        ToInt32(byt_Len, 0
                       );
                byt_Len = null;
                return byteNum;
            };
            GetConentStartIndex = () => 4;
            StoreDataLen = (nLen,buffer) => {
                BitConverter.GetBytes(
                     nLen).CopyTo(buffer, 0);
            };
        }

        protected abstract void Loop();
        protected abstract bool Close();
        public IPAddress LoopBack
        {
            get
            {
                return IPAddress.Loopback;
            }

        }
        public virtual uint BufferSize
        {
            get
            {
                return nBufferSize;

            }
            set
            {
                nBufferSize = value;
            }
        }
        public int LoopGapTime
        {
            get
            {
                return nLoopGapTime;
            }

            set
            {
                nLoopGapTime = value;
            }
        }
        public int Port
        {
            get
            {
                return this.nPort;
            }
            set
            {
                this.nPort = value;
            }
        }
        public String LocalIP
        {
            get
            {
                return Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            }
        }

        public string IP { get { return strIP; } set { strIP = value; } }
        protected void ForceClose()
        {
            String strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            Process[] Procs = Process.GetProcessesByName(strProcessName);
            foreach (Process item in Procs)
            {
                item.Kill();
            }
        }

        public byte[] ProvideBytes(ushort ust)
        {
             
            return BitConverter.GetBytes(ust);
        }
        public byte[] ProvideBytes(String str)
        {

            return TextEncode.GetBytes(str);
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
        //Need Convert byte array to a unsigned Num ?
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
        public virtual String ProvideString(byte[] bytArr, int nIndex, int nCount)
        {

            return TextEncode.GetString(bytArr, nIndex, nCount);
        }

        public byte[] ObjectToBytes(object obj)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, obj);
                    return ms.GetBuffer();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }
        public object BytesToObject(byte[] Bytes, int index, int count)
        {
            using (MemoryStream ms = new MemoryStream(Bytes, index, count))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }

    }
}
