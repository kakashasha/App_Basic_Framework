using System;
using System.Collections.Generic;
using System.Text;
using dotNetLab.Network;
using System.Data;
using System.Threading;
using dotNetLab.Data.Uniting;
namespace dotNetLab.Data.Embedded
{
    //不需要注册DBDiagnoseHandler
   public abstract class TCPServerDB:UnitDB
    {
        TServer SignalTower;
        private int nIndex_CurrentClient = -1;
        //非设置远程连接数据（TCP的 IP,port）
        public override void Connect(DBEngineNames EngineName, string IP, int Port, string UserName, string Pwd, string dbName)
        {
            String strConn = null;
            switch (EngineName)
            {
                case DBEngineNames.SQLITE:
                    Connect(EngineName,dbName);
                    break;
                case DBEngineNames.SQLCE:
                    Connect(EngineName,dbName);
                    break;
                case DBEngineNames.FireBird:
                    Connect(EngineName,dbName);
                    break;
                case DBEngineNames.LOCALDB:
                    Connect(EngineName,dbName);
                    break;
                case DBEngineNames.SQLSERVER:
                   PrepareSQLServerDB();
                  strConn = String.Format(
                            "Uid={0};Pwd={1};Initial Catalog={2};Data Source=LocalHost;",UserName,Pwd,
                            dbName);
                    base.Connect(IP, Port, dbName, UserName, Pwd, strConn, ref this.conn, ref this.cmd);
                    break;
                case DBEngineNames.POSTGRESQL:
                    PreparePostgresqlDB();
                    //默认为用户名为postgres
                        strConn = String.Format(
                            "Host=127.0.0.1;Username={0};Password={1};Database={2}",UserName,Pwd,
                            dbName);
                    base.Connect(IP, Port, dbName, UserName, Pwd, strConn, ref this.conn, ref this.cmd);
                    break;
                case DBEngineNames.MySQL:
                    PrepareMySQLDB();
                        strConn = String.Format(
                            "server=localhost;Database ={0};uid={1};pwd={2};charset=utf8",dbName,UserName,Pwd 
                            );
                    base.Connect(IP, Port, dbName, UserName, Pwd, strConn, ref this.conn, ref this.cmd);
                    break;
            }

            this.strIP = IP;
            this.nPort = Port;
            
            
            PrepareEvents();
        }

        public  void SQLiteAsServerDBConnect(string IP,int Port,string DBFilePath)
        {
            Connect(DBEngineNames.SQLITE, IP, Port, "", "", DBFilePath);
        }
        
        private void PrepareEvents()
        {
            this.SignalTower = new TServer();
            this.DBDiagnoseHandler += RemoteServer_DBDiagnoseHandler;
            SignalTower.ClientConnected += FactoryServer_Socket_ClientConnected;
            SignalTower.ClientDisconnected += FactoryServer_Socket_ClientDisconnected;
            SignalTower.Route = RouteMessage;
            
            SignalTower.IP = strIP;
            SignalTower.Boot();
        }

        private void RemoteServer_DBDiagnoseHandler(ErrorInfo e, DBOperator byt_Operator)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.CommandText);
            Console.WriteLine(e.ExceptionInfo.ToString());
            String str = string.Format("{0} Xand {1}", e.Message, e.CommandText);
            SignalTower.Send(nIndex_CurrentClient, (byte)DBOperator.OPERATOR_FAILED,
                SignalTower.ProvideBytes(str)
                 );
        }
        void SendFeedback(int nIndex_CurrentClient ,bool bIsTrue)
        {
            SignalTower.Send(nIndex_CurrentClient, (byte)DBOperator.OPERATOR_SUCCEED,
               SignalTower.ProvideBytes("Accepted !") 
                );
        }
        void SendFeedback(int nIndex_CurrentClient,byte []byt_Content,byte [] byteArr)
        {
            SignalTower.Send(nIndex_CurrentClient,TCPBase.FetchMSGMark(byteArr) ,
                byt_Content
                );
        }
        string FetchSQLStatment(byte[] byts)
        {
             uint Num= TCPBase.FetchDataLenByts(byts);
           return SignalTower.ProvideString(byts, TCPBase.MARKPOSITION, (int)Num);
        }
        void GetTableData(byte [] bytArr,int nIndex_Client)
        {

            byte byt_MSG = bytArr[0];
           Object obj = this.ProvideTable(this.FetchSQLStatment(bytArr), (DBOperator)bytArr[0]);
            
            if (this.Status)
            {

                byte[] bytArr_Obj  = SignalTower.ObjectToBytes(obj);
                if(bytArr_Obj.Length>bytArr.Length)
                {
                    TCPBase.StoreMSGMark(bytArr, Signals.BUFFER_SIZE);
                    TCPBase.StoreDataLenByts((uint)bytArr_Obj.Length, bytArr);

                    int nNum = SignalTower.SocketClients[nIndex_Client].Send(bytArr);
                    if(nNum<1)
                    Console.WriteLine("未能发送变更的buffer大小");
                    // SendFeedback(nIndex_Client, this.SignalTower.factoryServer_Socket.ProvideBytes("buffersize"), bytArr);
                    Thread.Sleep(500);
                    SignalTower.ClientsBuffer[nIndex_Client] = null;
                    SignalTower.ClientsBuffer[nIndex_Client] = new byte[bytArr_Obj.Length+TCPBase.MARKPOSITION];
                }
                SignalTower.ClientsBuffer[nIndex_Client][0] = byt_MSG;
               // DataTable dt = (DataTable)SignalTower.factoryServer_Socket.BytesToObject(bytArr_,0,bytArr_.Length);
                SendFeedback(nIndex_Client,bytArr_Obj, SignalTower.ClientsBuffer[nIndex_Client]) ;
            }
            else
            {
                Console.WriteLine("未能得到DataTable");
            }

               
        }
        void GetTableArrayData(byte[] bytArr, int nIndex_Client)
        {

            byte byt_MSG = bytArr[0];
            char ch_enQ = (Char)5;
            DataTable dt = this.ProvideTable(this.FetchSQLStatment(bytArr), (DBOperator)bytArr[0]);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                   sb.Append(String.Format( "{0}{1}",dt.Rows[i][j].ToString(),ch_enQ));
                }
            }
            sb.Append(String.Format("{0}{1}", dt.Rows.Count,ch_enQ)) ;
            sb.Append( dt.Columns.Count);

            if (this.Status)
            {

                byte[] bytArr_Obj = SignalTower.ProvideBytes(sb.ToString());
                if (bytArr_Obj.Length > bytArr.Length)
                {
                    TCPBase.StoreMSGMark(bytArr, Signals.BUFFER_SIZE);
                    TCPBase.StoreDataLenByts((uint)bytArr_Obj.Length, bytArr);

                    int nNum = SignalTower.SocketClients[nIndex_Client].Send(bytArr);
                    if (nNum < 1)
                        Console.WriteLine("未能发送变更的buffer大小");
                    // SendFeedback(nIndex_Client, this.SignalTower.factoryServer_Socket.ProvideBytes("buffersize"), bytArr);
                    Thread.Sleep(500);
                    SignalTower.ClientsBuffer[nIndex_Client] = null;
                    SignalTower.ClientsBuffer[nIndex_Client] = new byte[bytArr_Obj.Length + TCPBase.MARKPOSITION];
                }
                SignalTower.ClientsBuffer[nIndex_Client][0] = byt_MSG;
                // DataTable dt = (DataTable)SignalTower.factoryServer_Socket.BytesToObject(bytArr_,0,bytArr_.Length);
                SendFeedback(nIndex_Client, bytArr_Obj, SignalTower.ClientsBuffer[nIndex_Client]);
            }
            else
            {
                Console.WriteLine("未能得到DataTable");
            }

        }
        void GetUniqueData(int nIndexClient,byte[] bytArr )
        {
            string str = this.UniqueResult(this.FetchSQLStatment(bytArr) );
            if (this.Status)
            {
                SendFeedback(nIndexClient, SignalTower.ProvideBytes(str),bytArr);
            }
        }
        void ExtendChannalWidth(int nIndexClient,byte [] byts)
        {
             SignalTower.ClientsBuffer[nIndexClient] = 
                new byte[TCPBase.FetchDataLenByts(byts)+TCPBase.MARKPOSITION];
        }
        // To Do
        void DatabaseOperate(int nIndexClient, byte[] byts)
        {

        }
        void  RouteMessage(int nIndex_Client,byte[] byts)
        {
            // Only Available For One Thread To Access  This Method At The Same Time .
            lock (SignalTower)
            {
                this.nIndex_CurrentClient = nIndex_Client;
                switch (TCPBase.FetchMSGMark(byts))
                {
                    case (byte)DBOperator.OPERATOR_QUERY_ALL_TABLENAMES:
                        GetTableData(byts,nIndex_Client);
                        break;
                    case (byte)DBOperator.OPERATOR_QUERY_ALL_DBNAMES:
                        GetTableData(byts, nIndex_Client);
                        break;
                    case (byte)DBOperator.OPERATOR_QUERY_TABLE:
                        GetTableData(byts, nIndex_Client);
                        break;
                    case (byte)DBOperator.OPERATOR_QUERY_UNIQUE:
                        GetUniqueData(nIndex_Client, byts  );
                        break;
                    case Signals.BUFFER_SIZE:
                         ExtendChannalWidth(nIndex_Client,byts);
                        break;
                    case (byte)DBOperator.OPERATOR_NEW_DB:
                        DatabaseOperate(nIndex_Client, byts);
                        break;
                    case  (Byte)DBOperator.OPERATOR_QUERY_ALL_TABLENAMES_ARRAY:
                        GetTableArrayData(byts, nIndex_Client);break;
                    default: 
                        this.ExecuteNonQuery(FetchSQLStatment(byts), (DBOperator)byts[0]);
                        if(this.Status)
                        SendFeedback(nIndex_Client,true);
                         break;
                }

            }
        }
        private void FactoryServer_Socket_ClientDisconnected(string ClientInfo)
        {
            Console.WriteLine(string.Format("[{0}] 客户端:{1} 失去连接！",DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),ClientInfo));
        }
        private void FactoryServer_Socket_ClientConnected(int nIndex)
        {
            Console.WriteLine(string.Format("[{0}] 客户端:{1} 已经连接！", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),SignalTower.GetClientIP(nIndex)));
        }

         

    }
}
