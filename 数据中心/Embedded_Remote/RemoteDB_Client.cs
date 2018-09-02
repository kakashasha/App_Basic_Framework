using System;
using System.Collections.Generic;
using System.Text;
using dotNetLab.Data.Embedded;
using dotNetLab.Network;
using System.Threading;
using dotNetLab.Data.Uniting;
namespace dotNetLab.Data.Embedded
{
  
    //默认RemoteEnable=true 
   public   class RemoteDB : UnitDB
    {
        TCPClient clientSocket;
        bool bRemoteHandled = false;
        byte[] buffer;
        public bool RemoteEnable
        {
            get { return bRemoteMode; }
            set { bRemoteMode = value; }
        }
       public override void Connect(DBEngineNames EngineName, string IP, int Port, string UserName, string Pwd, string dbName)
        {
            String strConn = null;
            this.RemoteEnable = true;
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
            Initializer();
        }
       private void Initializer()
       {
            clientSocket = new TCPClient();
            clientSocket.Connect(clientSocket.IP);
            clientSocket.Route = ClientSocket_Route;
            this.RemoteHandle += RemoteDB_RemoteHandle;
       }
        void Send(string sql, DBOperator byt_Operator)
        {
            clientSocket.Send(sql,(byte)byt_Operator);
            while (true)
            {
                Thread.Sleep(clientSocket.LoopGapTime);
                if (bRemoteHandled)
                {
                    this.bRemoteHandled = false;
                    break;
                    
                }
            }
             
        }
        private object RemoteDB_RemoteHandle(string strSQL, DBOperator byt_Operator)
        {
            object obj = null;
             byte byt = 255;
            switch (byt_Operator)
            {
                case  DBOperator.OPERATOR_QUERY_UNIQUE: Send(strSQL, byt_Operator);
                    obj = clientSocket.GetWords();
                    break;
                case  DBOperator.OPERATOR_QUERY_ALL_DBNAMES:
                    Send(strSQL, byt_Operator);
                    obj = clientSocket.BytesToObject(buffer, TCPBase.MARKPOSITION, (int)TCPBase.FetchDataLenByts(buffer));
                     break;
                case  DBOperator.OPERATOR_QUERY_ALL_TABLENAMES:
                    Send(strSQL, byt_Operator);
                    obj = clientSocket.BytesToObject(buffer, TCPBase.MARKPOSITION, (int)TCPBase.FetchDataLenByts(buffer));  break;
                case  DBOperator.OPERATOR_QUERY_TABLE:
                    Send(strSQL, byt_Operator);
                    obj = clientSocket.BytesToObject(buffer, TCPBase.MARKPOSITION, (int)TCPBase.FetchDataLenByts(buffer));   break;
                default:
                    Send(strSQL, byt_Operator);
                    byt = TCPBase.FetchMSGMark(buffer);
                    if (byt == (byte)DBOperator.OPERATOR_SUCCEED)
                        obj = true;
                    if (byt == (byte)DBOperator.OPERATOR_FAILED)
                        obj = false;
                      break;
            }
            return obj;
        }
        void PrepareHandleRecievedData(byte dboperator)
        {
            if(dboperator== Signals.BUFFER_SIZE)
            {

                clientSocket.MainBuffer = new Byte[TCPBase.FetchDataLenByts(clientSocket.MainBuffer)+TCPBase.MARKPOSITION];
               
                return;
            }
            this.bRemoteHandled = true;
            this.buffer = clientSocket.MainBuffer;
            if ((DBOperator)dboperator == DBOperator.OPERATOR_FAILED)
            {
                 
                HandleError(new Exception(clientSocket.GetWords()), this.cmd, (DBOperator)dboperator);
            }
        }
        private void ClientSocket_Route(int nWhichClient, byte[] byts)
        {
          //  this.buffer = byts;
            switch (TCPBase.FetchMSGMark(byts))
            {

                case (Byte)DBOperator.OPERATOR_SUCCEED: PrepareHandleRecievedData( TCPBase.FetchMSGMark(byts)); break;
                case (Byte)DBOperator.OPERATOR_FAILED: PrepareHandleRecievedData( TCPBase.FetchMSGMark(byts)); break;
                case (Byte)DBOperator.OPERATOR_QUERY_UNIQUE: PrepareHandleRecievedData( TCPBase.FetchMSGMark(byts)); break;
                case (Byte)DBOperator.OPERATOR_QUERY_ALL_DBNAMES: PrepareHandleRecievedData(TCPBase.FetchMSGMark(byts)); break;
                case (Byte)DBOperator.OPERATOR_QUERY_ALL_TABLENAMES: PrepareHandleRecievedData( TCPBase.FetchMSGMark(byts)); break;
                case (byte)DBOperator.OPERATOR_QUERY_TABLE: PrepareHandleRecievedData( TCPBase.FetchMSGMark(byts)); break;
                case Signals.BUFFER_SIZE: PrepareHandleRecievedData(TCPBase.FetchMSGMark(byts));  break;
            }
        }
    }
}
