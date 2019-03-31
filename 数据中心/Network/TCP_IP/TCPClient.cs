using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace dotNetLab
{
    namespace Network
    {
      public  class TCPClient:TCPBase
        {
            public   RouteMessageCallback Route;
            protected Socket Client;
            protected Byte[] bytArr_MainChannel;
            public Byte[] MainBuffer
            {
                get { return bytArr_MainChannel; }
                set { bytArr_MainChannel = value; }
            }
           protected uint nRecievedNum = 0;
            public bool bIsConnected = false;
           
            public TCPClient()
            {
                DefaultConfig();
               
                
            }
            public bool Connected
            {
                get
                {
                    return bIsConnected;
                }
            }
            public bool Connect(String strIP, String strClientID)
            {
                try
                {
                     
                    ServerIP = IPAddress.Parse(strIP);
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
                    byte[] bytArr = TextEncode.GetBytes(strClientID);
                    bytArr.CopyTo(MainBuffer,5);
                    StoreDataLenByts((uint)bytArr.Length, MainBuffer);
                    StoreMSGMark(MainBuffer, Signals.CLIENT_ID);
                    int nNum = this.Client.Send(MainBuffer);
                    if (nNum > 0)
                    {
                        this.bIsConnected = true;
                        return true;
                    }
                    else
                    {
                        this.bIsConnected = false;
                        return false;
                    }
                }
                catch (System.Exception ex)
                {
                    this.strErrorInfo = ex.ToString();
                    return false;
                }

            }
            //Client ID Is Client IP
            public bool Connect( )
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
                    this.bIsConnected = true;
                    return true;
                }
                catch (System.Exception ex)
                {
                    this.strErrorInfo = ex.ToString();
                    this.bIsConnected = false ;
                    return false;
                }

            }
            // Message Loop

            protected override void Loop()
            {
                while (true)
                {
                    if (bEndNetwork)
                        return;
                    RecieveAndParse();
                    Thread.Sleep(nLoopGapTime);
                }
                Console.WriteLine("消息接收线程已经退出");
            }

            protected virtual void RecieveFormServerMethod()
            {
                nRecievedNum = (uint)Client.Receive(MainBuffer);
            }
            protected void RecieveAndParse()
            {
                try
                {
                    RecieveFormServerMethod();
                    if (nRecievedNum == 0)
                    {
                        bIsConnected = false;
                        return;
                    }
                    byte byt_MSG_Mark = MainBuffer[0];
                     
                    
                    if (Route!=null)
                    Route(-1,MainBuffer);
                    Thread.Sleep(nLoopGapTime);
                }
                catch (Exception e)
                {
                    this.bIsConnected = false;
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
                    this.bIsConnected = false;
                    
                    return true;
                }
                catch (System.Exception ex)
                {
                    this.strErrorInfo = ex.ToString();
                    this.bIsConnected = false;
                    return false;
                }


            }
            public void Dispose ()
            {
                Close();
                ForceClose();
            }
            //Send Message To Server
            public bool Send(String strMsg)
            {
                byte[] bytArr = TextEncode.GetBytes(strMsg);
                bytArr.CopyTo(MainBuffer, 5);
                StoreDataLenByts((uint)bytArr.Length, MainBuffer);
                StoreMSGMark(MainBuffer, Signals.WORDS_CTS);
                int nNum = this.Client.Send(MainBuffer);
                if (nNum > 0)
                    return true;
                else
                    return false;
            }
            public bool Send(String strMsg,byte MSG)
            {
                byte[] bytArr = TextEncode.GetBytes(strMsg);
                int nLen = bytArr.Length;
                if(nLen > this.MainBuffer.Length)
                {
                    StoreMSGMark(MainBuffer, Signals.BUFFER_SIZE);
                    StoreDataLenByts((uint)nLen, MainBuffer);
                    int nNum = this.Client.Send(MainBuffer);
                    MainBuffer = new Byte[nLen +MARKPOSITION];
                    if (nNum > 0)
                    {
                        //  TCPBase.StoreMSGMark(bytArr_MainChannel, (byte)0);
                        return true;
                    }
                    else
                        return false;
                }
                Thread.Sleep(500);
                bytArr.CopyTo(MainBuffer, 5);
                StoreDataLenByts((uint)nLen , MainBuffer);
                StoreMSGMark(MainBuffer, MSG);
                  
                try
                {
                int nNum = this.Client.Send(MainBuffer);
                if (nNum > 0)
                {
                  //  TCPBase.StoreMSGMark(bytArr_MainChannel, (byte)0);
                    return true;
                }
                else
                    return false;
                }
                catch (System.Exception ex)
                {
                    return false;
                }
            }
            //Get Message Which Contains Words
            public String GetWords()
            {

                this.nRecievedNum = FetchDataLenByts(MainBuffer);
                return TextEncode.GetString(this.MainBuffer, 5,
                    (int)this.nRecievedNum);
            }
            ~TCPClient()
            {
                Close();
            }
        }
    }
}
