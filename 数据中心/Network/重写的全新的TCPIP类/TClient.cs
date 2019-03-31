using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace dotNetLab.Networking
{
   public  class TClient : TCPBase
    {
        
        protected Socket Client;
        protected Byte[] bytArr_MainChannel;
        public Byte[] MainBuffer
        {
            get { return bytArr_MainChannel; }
            set { bytArr_MainChannel = value; }
        }
        protected int nRecievedNum = 0;
        public bool bIsConnected = false;
  
        //Client ID Is Client IP
        public bool Connect()
        {
            try
            {
                MainBuffer = new byte[BufferSize];
                bEndNetwork = false;

                ServerIP = IPAddress.Parse(IP);
                IPEndPoint ClientEndPoint =
                    new IPEndPoint(this.ServerIP, nPort);
                Client = new
             Socket
             (
             AddressFamily.InterNetwork,
             SocketType.Stream, ProtocolType.IP);
                Client.Connect(ClientEndPoint);
                thd_Main = new Thread(Loop);
                thd_Main.Start();
                return true;
            }
            catch (System.Exception ex)
            {
                this.strErrorInfo = ex.ToString();
                return false;
            }

        }
        protected override void Loop()
        {
            while (true)
            {
                if (bEndNetwork)
                    return;
                RecieveAndParse();
                Thread.Sleep(nLoopGapTime);
            }
           
        }

        protected virtual void RecieveFormServerMethod()
        {
            int nCount = GetConentStartIndex();
            int nRecievedLen = Client.Receive(MainBuffer, 0, nCount, System.Net.Sockets.SocketFlags.None);
            int nLen =  FetchDataLen(MainBuffer);
            

            int nTotalLen =  nLen + nCount;
            while (true)
            {
                if (nCount < nTotalLen)
                {
                    nCount += Client.Receive(MainBuffer, nCount, nTotalLen - nCount, System.Net.Sockets.SocketFlags.None);
                }
                else
                    break;
            }
            nRecievedNum = nRecievedLen;
        }
        protected void RecieveAndParse()
        {
            try
            {
                RecieveFormServerMethod();
                if (nRecievedNum == 0)
                    return;
                byte byt_MSG_Mark = MainBuffer[0];


                if (Route != null)
                    Route(-1, MainBuffer);
                Thread.Sleep(nLoopGapTime);
            }
            catch (Exception e)
            {
                this.strErrorInfo = e.ToString();
            }

        }
        //Close Socket
        protected override bool Close()
        {
            try
            {
                bEndNetwork = true;
                thd_Main.Abort();
                Client.Disconnect(false);
                Client.Shutdown(SocketShutdown.Both);
                Client.Close();

                return true;
            }
            catch (System.Exception ex)
            {
                this.strErrorInfo = ex.ToString();
                return false;
            }


        }
        public void Dispose()
        {
            Close();
         
        }
        //Send Message To Server
        public bool Send(String strMsg)
        {
            byte[] bytArr = TextEncode.GetBytes(strMsg);
            bytArr.CopyTo(MainBuffer, GetConentStartIndex());
            StoreDataLen( bytArr.Length, MainBuffer);
            
            int nNum = this.Client.Send(MainBuffer);
            if (nNum > 0)
                return true;
            else
                return false;
        }
     
        public int Send(byte [] buf)
        {
            try
            {
                return Client.Send(buf);
            }
            catch (Exception ex) 
            {

                throw;
            }
          
        }
        //Get Message Which Contains Words
        public String GetWords()
        {

            this.nRecievedNum = FetchDataLen(MainBuffer);
            return TextEncode.GetString(this.MainBuffer, GetConentStartIndex(),
                this.nRecievedNum);
        }
        ~TClient()
        {
            Close();
        }
    }
}
