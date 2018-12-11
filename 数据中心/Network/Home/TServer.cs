using System ;
using System.Windows.Forms ;
namespace dotNetLab.Network
{
    public abstract class XTCPServer :TCPServer
    {
       
        protected override void GetClientInfo(System.Net.Sockets.Socket skt)
        {
         base.GetClientInfo(skt);
         String strID = GetClientID(skt).Split(new char[] { ':' })[0];
         this.lstStrArr_ClientID.Add(strID);
         // ImplementClientCon_DisCon_Delegate();
         }  
    }
    //防止数据没有传递完
    public   class TServer: XTCPServer
    {
        protected override void ImplementClientCon_DisCon_Delegate()
        {
           
        }

       //防止数据没有传递完
        protected override int ClientRecieveMethod(int nIndex)
        {
             //读取数据的长度
             int nRecievedLen  =  lst_Clients[nIndex].Receive(ClientsBuffer[nIndex],0,5,System.Net.Sockets.SocketFlags.None);
             int nLen = (int)TCPBase.FetchDataLenByts(ClientsBuffer[nIndex]);
            int nCount = TCPBase.MARKPOSITION;
             int nTotalLen = (int)nLen + TCPBase.MARKPOSITION;
            //然后循环读取，确保没有少读
            while (true)
            {
                if (nCount < nTotalLen)
                {
                    nCount += lst_Clients[nIndex].Receive(ClientsBuffer[nIndex], nCount, nTotalLen - nCount,System.Net.Sockets.SocketFlags.None);
                }
                else
                    break;
            }
            return nRecievedLen;

        }
    }
}
//  in.read(MainBuffer,0, TCPBase.MARKPOSITION);

//int nCount = TCPBase.MARKPOSITION;
//int nLen = TCPBase.FetchDataLenByts(MainBuffer);
//int nTotalLen = nLen + TCPBase.MARKPOSITION;
/*            nRecievedNum = in.read(MainBuffer,TCPBase.MARKPOSITION,nLen);
            nCount += nRecievedNum ;*/
