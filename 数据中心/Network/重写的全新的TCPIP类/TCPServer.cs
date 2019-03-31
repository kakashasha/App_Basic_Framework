using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace dotNetLab.Networking
{
   public  class TServer : TCPBase
    {
        public Action<int> ClientConnected;
        public Action<String> ClientDisconnected;
     
        //Each SubLoop Thread Content Buffer To Recieve Client Message
        protected List<Byte[]> clientBuffers;
        protected List<bool> ClientSwitchers;
        public bool MainLoopSwitcher = true;
        public List<Byte[]> ClientsBuffer
        {
            get { return clientBuffers; }
            set { clientBuffers = value; }
        }
        // Deal More Clients Threads
        protected List<Thread> clientThread;
        // Record Connected Client IDs ;
        public List<String> clientIDs;

        protected List<Socket> clientSockets;

        protected Socket ServerSocket;
        public List<Socket> SocketClients
        {
            get { return clientSockets; }
        }

        public TServer()
        {
            InitCollections();
           
        }
        bool Boot(String strIP)
        {
            try
            {
                this.bEndNetwork = false;
                ServerIP = IPAddress.Parse(strIP);
                IPEndPoint ServerEndPoint = new IPEndPoint(ServerIP, nPort);

                ServerSocket =
                new Socket(
                   AddressFamily.InterNetwork,
                   SocketType.Stream, ProtocolType.IP);
                ServerSocket.Bind(ServerEndPoint);
                ServerSocket.Listen(3);
                thd_Main = new Thread(Loop);
                thd_Main.Start();
                return true;
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(String.Format("未能成功构建TCP服务器，IP或者端口错误: {0}。", ex.Message)
                                , "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        //请确保为IP属性赋值了
        public bool Boot()
        {
            return Boot(IP);
        }
        
        private void InitCollections()
        {
            ClientsBuffer = new List<Byte[]>();
            clientSockets = new List<Socket>();
            clientThread = new List<Thread>();
            clientIDs = new List<string>();
            ClientSwitchers = new List<bool>();

        }
        protected override void Loop()
        {
            while (true)
            {

                try
                {
                    clientSockets.Add(ServerSocket.Accept());

                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("At TCPServer.Loop:" + ex.Message);
                    continue;
                   
                }
                
                //取得 client IP(with itself port)
                 String rawIP = clientSockets[clientSockets.Count -1].RemoteEndPoint.ToString();
                String strID = rawIP.Split(new char[] { ':' })[0];
                this.clientIDs.Add(strID);


                if (ClientConnected != null)
                    this.ClientConnected(clientSockets.Count - 1);
                else
                    Console.WriteLine("未实现‘ ClientConnected 事件 ’");
                ClientSwitchers.Add(true);
                clientThread.Add(new Thread(SubLoop));
                clientThread[clientThread.Count - 1].Start();

            }
        }
        protected void SubLoop()
        {
            ClientsBuffer.Add(new byte[BufferSize]);

            RecieveAndParse(ClientsBuffer.Count - 1);
        }
        

        protected virtual int ClientRecieveMethod(int nIndex)
        {
            //读取数据的长度
            int nCount = 0;
            int nLen = 0;
            int nTotalLen = 0;
            nCount = GetConentStartIndex();
            int nRecievedLen = clientSockets[nIndex].Receive(ClientsBuffer[nIndex], 0, nCount, System.Net.Sockets.SocketFlags.None);

            nLen = FetchDataLen(ClientsBuffer[nIndex]);
            nTotalLen = nLen + nCount;
            //然后循环读取，确保没有少读
            while (true)
            {
                if (nCount < nTotalLen)
                {
                    nCount += clientSockets[nIndex].Receive(ClientsBuffer[nIndex], nCount, nTotalLen - nCount, System.Net.Sockets.SocketFlags.None);
                }
                else
                    break;
            }
            return nRecievedLen;
        }
        void RecieveAndParse(int nIndex_ArrByt)
        {
            string strClientID = clientIDs[nIndex_ArrByt];
            while (true)
            {

                try
                {
                    if (!ClientSwitchers[nIndex_ArrByt])
                        return;
                    if (bEndNetwork)
                        return;
                    int nRecievedLen = ClientRecieveMethod(nIndex_ArrByt);
                    if (nRecievedLen == 0)
                        throw new Exception("客户端断开");
                 
                    if (Route != null)
                        Route(nIndex_ArrByt, clientBuffers[nIndex_ArrByt]);
                    Thread.Sleep(nLoopGapTime);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    this.strErrorInfo = e.ToString();
                    nIndex_ArrByt = clientIDs.IndexOf(strClientID);
                    if (ClientDisconnected != null)
                        this.ClientDisconnected(clientIDs[nIndex_ArrByt]);
                    else
                        Console.WriteLine("ClientDisconnected 事件未实现");

                    try
                    {
                        clientSockets[nIndex_ArrByt].Shutdown(SocketShutdown.Both);
                    }
                    catch (System.Exception exx)
                    {

                    }

                    clientSockets[nIndex_ArrByt].Close();
                    clientSockets.RemoveAt(nIndex_ArrByt);
                    this.ClientsBuffer.RemoveAt(nIndex_ArrByt);
                    this.clientIDs.RemoveAt(nIndex_ArrByt);
                    ClientSwitchers.RemoveAt(nIndex_ArrByt);

                    Thread it = clientThread[nIndex_ArrByt];
                    clientThread.RemoveAt(nIndex_ArrByt);
                    int nIndex_NewOne = clientSockets.Count - 1;
                    it.Abort();
                    return;
                }


            }
        }
        protected override bool Close()
        {
            try
            {
                this.bEndNetwork = true;

                // Thread.Sleep(200);

                try
                {
                    ServerSocket.Shutdown(SocketShutdown.Both);
                }
                catch (System.Exception ex)
                {

                    //  ServerSocket.Shutdown(SocketShutdown.Receive);
                }
                ServerSocket.Close();



                foreach (var item in clientSockets)
                {
                    item.Shutdown(SocketShutdown.Both);
                    item.Close();
                }
                foreach (var item in clientThread)
                {
                    item.Abort();
                }
                thd_Main.Abort();
                return true;
            }
            catch (System.Exception ex)
            {
                this.strErrorInfo = ex.ToString();
                return false;
            }
        }
        public void Dispose(bool bForceClose = true)
        {
            Close();
            if (bForceClose)
                ForceClose();
        }
        ~TServer()
        {
            Close();
        }

        protected virtual void GetClientInfo(Socket skt) { }
        public string GetClientIP(int nIndex)
        {
            return this.clientSockets[nIndex].RemoteEndPoint.ToString();
        }
       
        int GetClientIndex(String strClientID)
        {
            return this.clientIDs.IndexOf(strClientID);
        }
        byte[] GetClientBuffer(String strClientID)
        {
            int n = GetClientIndex(strClientID);
            return ClientsBuffer[n];
        }
        byte[] GetClientBuffer(int nClientIndex)
        {
            return ClientsBuffer[nClientIndex];
        }
        public void KillClientThread(int nClientIndex)
        {
            this.ClientSwitchers[nClientIndex] = false;
        }


    }
 
}
