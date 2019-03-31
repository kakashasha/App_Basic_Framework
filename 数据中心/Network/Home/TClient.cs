using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetLab.Network
{
  public class TClient : TCPClient
    {
        //防止数据未传递完毕
        protected override void RecieveFormServerMethod()
        {
            int nRecievedLen = Client.Receive(MainBuffer , 0, 5, System.Net.Sockets.SocketFlags.None);
            int nLen = (int)TCPBase.FetchDataLenByts(MainBuffer);
            int nCount = TCPBase.MARKPOSITION;

            int nTotalLen = (int)nLen + TCPBase.MARKPOSITION;
            while (true)
            {
                if (nCount < nTotalLen)
                {
                    nCount += Client.Receive(MainBuffer, nCount, nTotalLen - nCount, System.Net.Sockets.SocketFlags.None);
                }
                else
                    break;
            }
            nRecievedNum =  (uint)nRecievedLen;
        }

        
    }
}
