#define SOCKERTTEST
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace dotNetLab
{
    namespace Network
    {
        public abstract class TCPServer : TCPBase
        {
            public delegate void ClientConnectedCallback(int nIndex);
            public delegate void ClientDisconnectedCallback(String ClientInfo);
            public event ClientConnectedCallback ClientConnected;
            public event ClientDisconnectedCallback ClientDisconnected;
           
            public RouteMessageCallback Route;
            //Each SubLoop Thread Content Buffer To Recieve Client Message
            protected List<Byte[]> lstBytArr_Content;
            protected List<bool> lst_Thd_Ctrls;
          public  bool MainLoopCtrl = true;
            public List<Byte[]> ClientsBuffer
            {
                get { return lstBytArr_Content; }
                set { lstBytArr_Content = value; }
            }
            // Deal More Clients Threads
            protected List<Thread> lstThdArr_SubContent;
            // Record Connected Client IDs ;
            public List<String> lstStrArr_ClientID;
            protected List<Socket> lst_Clients;

            protected Socket ServerSocket;
            public List<Socket> SocketClients {

                get { return lst_Clients; }
            }

            public TCPServer()
            {

                DefaultConfig();
                ServerSocket =
                     new Socket(
                         AddressFamily.InterNetwork,
                         SocketType.Stream, ProtocolType.IP);

                InitCollections();
                ImplementClientCon_DisCon_Delegate();
            }
            public bool Boot(String strIP)
            {
                try
                {
                    ServerIP = IPAddress.Parse(strIP);
                    IPEndPoint ServerEndPoint = new IPEndPoint(ServerIP, nPort);
                    ServerSocket.Bind(ServerEndPoint);
                    ServerSocket.Listen(3);
                    thd_Main = new Thread(Loop);
                    thd_Main.Start();
                    return true ;
                }
                catch (System.Exception ex)
                {
                      
                	MessageBox.Show(String.Format("未能成功构建TCP服务器，IP或者端口错误: {0}。",ex.Message) 
                	                ,"提示", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false ;
                }

            }
            //请确保为IP属性赋值了
            public bool Boot()
            {
                return Boot(IP);
            }
            protected abstract void ImplementClientCon_DisCon_Delegate();
            private void InitCollections()
            {
                ClientsBuffer = new List<Byte[]>();
                lst_Clients = new List<Socket>();
                lstThdArr_SubContent = new List<Thread>();
                lstStrArr_ClientID = new List<string>();
                lst_Thd_Ctrls = new List<bool>();
                this.lst_FileTransferInfo = new List<FileTransferInfo>();
            }
            protected override void Loop()
            {
                while (true)
                {
                     
                    lst_Clients.Add(ServerSocket.Accept());
                    GetClientInfo(lst_Clients[lst_Clients.Count - 1]);
                    if (ClientConnected != null)
                        this.ClientConnected(lst_Clients.Count - 1);
                    else
                        Console.WriteLine("未实现‘ ClientConnected 事件 ’");
                    lst_Thd_Ctrls.Add(true);
                    lstThdArr_SubContent.Add(new Thread(SubLoop));
                    
                    lstThdArr_SubContent[lstThdArr_SubContent.Count - 1].Start();

                }
            }
            protected void SubLoop()
            {
                ClientsBuffer.Add(new byte[BufferSize]);
                AddFileTransferInfo(lstStrArr_ClientID[lst_Clients.Count - 1]);
                RecieveAndParse(ClientsBuffer.Count - 1);
            }
            protected virtual void ParseAndHandle(byte[] bytArr,int nIndex)
            {
                byte byt_MSG_Mark = bytArr[0];
                int n=-1;
                switch (byt_MSG_Mark)
                {
                    case Signals.CLIENT_ID: OnClientID(); break;
                    case Signals.FILE_BEGIN:   n = GetFileTransferTarget(nIndex);
                        lst_FileTransferInfo[n].OnFileBegine();break;
                    case Signals.FILE_TRANSFER:
                          n = GetFileTransferTarget(nIndex);
                        lst_FileTransferInfo[n].OnFileTransfer(); break;
                    case Signals.FILE_END:
                        n = GetFileTransferTarget(nIndex);
                        lst_FileTransferInfo[n].OnFileEnd(); break;
                    default:
                        if (Route != null)
                            Route(nIndex,bytArr);
                        else
                            Console.WriteLine("未实现自定义消息 Route,自定义消息无法处理");
                        break;
                }
            }
            
             protected virtual int ClientRecieveMethod(int nIndex)
            {
               return   lst_Clients[nIndex].Receive(ClientsBuffer[nIndex]);

              
            }
              void RecieveAndParse(int nIndex_ArrByt)
            {
                string strClientID = lstStrArr_ClientID[nIndex_ArrByt];
                while (true)
                {

                    try
                    {
                        if (!lst_Thd_Ctrls[nIndex_ArrByt])
                            return;
                        int nRecievedLen =  ClientRecieveMethod(nIndex_ArrByt);
                        if (nRecievedLen == 0)
                            throw new Exception("客户端断开");
                        ParseAndHandle(ClientsBuffer[nIndex_ArrByt],nIndex_ArrByt);
                        Thread.Sleep(nLoopGapTime);
                    }
                    catch (Exception e)
                    {
                       
                        Console.WriteLine(e.Message);
                        this.strErrorInfo = e.ToString();
                        nIndex_ArrByt = lstStrArr_ClientID.IndexOf(strClientID);
                        if (ClientDisconnected!=null)
                            this.ClientDisconnected(lstStrArr_ClientID[nIndex_ArrByt]);
                        else
                            Console.WriteLine("ClientDisconnected 事件未实现");
                        if (lst_FileTransferInfo.Count > 0)
                        {
                            try
                            {
                                int n = GetFileTransferTarget(nIndex_ArrByt);
                                lst_FileTransferInfo.RemoveAt(n);

                            }
                            catch (Exception ex)
                            {

                               // throw;
                            }
                        }
                        lst_Clients[nIndex_ArrByt].Shutdown(SocketShutdown.Both);
                        lst_Clients[nIndex_ArrByt].Close();
                        lst_Clients.RemoveAt(nIndex_ArrByt);
                        this.ClientsBuffer.RemoveAt(nIndex_ArrByt);
                        this.lstStrArr_ClientID.RemoveAt(nIndex_ArrByt);
                        lst_Thd_Ctrls.RemoveAt(nIndex_ArrByt);

                        Thread it = lstThdArr_SubContent[nIndex_ArrByt];
                        lstThdArr_SubContent.RemoveAt(nIndex_ArrByt);
                        int nIndex_NewOne = lst_Clients.Count - 1;
                        it.Abort();
                        return;
                    }


                }
            }
          
            public virtual bool Send(String strClientID, byte byt_Msg, byte[] byt_Arr_Content)
            {
                int nIndex = GetClientIndex(strClientID);
                byte [] bytArr =  GetClientBuffer(nIndex); 

                StoreMSGMark(bytArr, byt_Msg);
                StoreDataLenByts((uint)byt_Arr_Content.Length, bytArr);
                for (int i = 0; i < byt_Arr_Content.Length; i++)
                {
                    bytArr[i + 5] = byt_Arr_Content[i];
                }
                int nResult = lst_Clients[nIndex].Send(bytArr);
                if (nResult > 0)
                    return true;
                else
                    return false;

            }
            public virtual bool Send(int nIndex_Client, byte byt_Msg, byte[] byt_Arr_Content)
            {
                 
                byte[] bytArr = GetClientBuffer(nIndex_Client);

                StoreMSGMark(bytArr, byt_Msg);
                StoreDataLenByts((uint)byt_Arr_Content.Length, bytArr);
                for (int i = 0; i < byt_Arr_Content.Length; i++)
                {
                    bytArr[i + 5] = byt_Arr_Content[i];
                }
                int nResult = -1;
                //  Console.WriteLine("Begine Send DataTable");
                try
                {
                    nResult = lst_Clients[nIndex_Client].Send(bytArr);
                    //  Console.WriteLine("Sended ! ");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Error");
                }
                if (nResult > 0)
                    return true;
                else
                    return false;

            }
            //The Method To Handle Client Connected Message
            protected void OnClientID()
            {
                uint byteNum = FetchDataLenByts(ClientsBuffer[ClientsBuffer.Count - 1]);
                byte[] buf = ClientsBuffer[ClientsBuffer.Count - 1];
                lstStrArr_ClientID.Add(TextEncode.GetString
                    (buf, 5, (int)byteNum)
                    );
                AddFileTransferInfo(lstStrArr_ClientID[lst_Clients.Count - 1]);
#if SOCKERTTEST
                Console.WriteLine(lstStrArr_ClientID[lstStrArr_ClientID.Count - 1]);
#endif
            }
            protected override bool Close()
            {
                try
                {
                    this.bEndNetwork = true;
                    ServerSocket.Shutdown(SocketShutdown.Both);
                    ServerSocket.Close();
                    thd_Main.Abort();
                    foreach (var item in lst_Clients)
                    {
                        item.Shutdown(SocketShutdown.Both);
                        item.Close();
                    }
                    foreach (var item in lstThdArr_SubContent)
                    {
                        item.Abort();
                    }

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
                ForceClose();
            }
            ~TCPServer()
            {
                Close();
            }
            
            protected virtual void GetClientInfo(Socket skt) { }
            public string GetClientIP(int nIndex)
            {
                return this.lst_Clients[nIndex].RemoteEndPoint.ToString();
            }
            protected String GetClientID(Socket skt_ClientSocket)
            {
                return skt_ClientSocket.RemoteEndPoint.ToString();
                 
            }
            int  GetClientIndex(String strClientID)
            {
                return this.lstStrArr_ClientID.IndexOf(strClientID);
            }
            byte[]  GetClientBuffer(String strClientID)
            {
                int n = GetClientIndex(strClientID);
                return ClientsBuffer[n];
            }
            byte[] GetClientBuffer(int nClientIndex)
            {
                return ClientsBuffer[nClientIndex];
            }
           public  void KillClientThread(int nClientIndex)
            {
                this.lst_Thd_Ctrls[nClientIndex] = false;
            }
            
            #region File_Operation

            int GetFileTransferTarget(int n)
            {
                for (int i = 0; i < lst_FileTransferInfo.Count; i++)
                {
                    if (lst_FileTransferInfo[i].sct == lst_Clients[n])
                        return i;
                }
                return -1;
            }
            void AddFileTransferInfo(string strClientID)
            {
                int n = this.lstStrArr_ClientID.IndexOf(strClientID);
                this.lst_FileTransferInfo.Add(
                    new FileTransferInfo(this.TextEncode,ClientsBuffer[n], lst_Clients[n]));
            }

            #endregion
        }
    }
}
