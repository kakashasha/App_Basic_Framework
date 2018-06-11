using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
namespace dotNetLab
{
    namespace Network
    {
        public class FileTransferInfo
        {
           
            public byte[] byt_MainChannel;
            public Socket sct; 
            public FileStream fs;
            public Encoding en;
            public const byte  MARKPOSITION = 5;
            public FileTransferInfo() { }
            public FileTransferInfo(Encoding en, byte[] byt_MainChannel, Socket sct)
            {
                this.en = en;
                this.byt_MainChannel = byt_MainChannel;
                this.sct = sct;
            }
            void StartFileTransfer(string strFileName,bool isRead)
            {
                if(!isRead)
                this.fs = new FileStream(strFileName,FileMode.Create);
                else
                this.fs = new FileStream(strFileName, FileMode.Open);
            }
            void DisposeFileStream()
            {
                fs.Close();
                fs.Dispose();
            }
            public virtual bool SendFile(string FilePath)
            {
                try
                {
                   
                    StartFileTransfer(FilePath, true);
                    long FileLength =  fs.Length;
                    int BufferContentLen = byt_MainChannel.Length;
                    long SendTimes = 0;
                    SendTimes = (FileLength / (BufferContentLen - MARKPOSITION));
                    //Short File Path ;
                    string MereFileName = Path.GetFileName(FilePath);
                    //Get Short File Path Bytes ;
                    byte[] temp = en.GetBytes(MereFileName);
                    temp.CopyTo(byt_MainChannel, MARKPOSITION);
                     byte [] bytArr_FileLen= BitConverter.GetBytes((int)FileLength);
                    bytArr_FileLen.CopyTo(byt_MainChannel, temp.Length + 5);
                    byte[] bytArr_SendTimes = BitConverter.GetBytes((int)SendTimes+1) ;
                    bytArr_SendTimes.CopyTo(byt_MainChannel,  temp.Length + 5+4);
                    // Get Short File Path Bytes Length ;
                    //And Store The Length Into DataBytesNum [1025] [1026] ;
                    TCPBase.StoreDataLenByts((uint)temp.Length, byt_MainChannel);
                    // Content Mark  Is File Name Mark ;
                    TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_BEGIN);

                    //Send File Name To Someone ;
                    sct.Send(byt_MainChannel);
                   
                    //Change Content Mark To Be File Data Mark  ;
                    //  StoreMSGMark(byt_MainChannel,);

                    // Create FileStream To Read File  Data Into Array byt_MainChannel ;

                    //Gain File Length ;

                    //Record How Times To Send ;
                   
                    //if File Length  Is Less Than 1024 
                    //Read  All File Data Into byt_MainChannel ; 
                    if (FileLength <= BufferContentLen - MARKPOSITION)
                    {
                        fs.Read(byt_MainChannel, MARKPOSITION, (int)FileLength);
                       
                        TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_TRANSFER);
                        //Identify The Number Of File Data To Send ;
                        TCPBase.StoreDataLenByts((uint)FileLength, byt_MainChannel);
                        sct.Send(byt_MainChannel);
                        TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_END);
                        //Send The Last Time ;
                        sct.Send(byt_MainChannel);
                        DisposeFileStream();
                    }
                    //else 
                    else
                    {
                        // Gain The Times To Send ;
                      
                        //Gain The Last Bytes To Send  ;
                        int Spare = (int)(FileLength - (BufferContentLen - MARKPOSITION) * SendTimes);
                        //Loop And Send File Data ;
                        for (; SendTimes > 0; SendTimes--)
                        {
                            fs.Read(byt_MainChannel, MARKPOSITION, (int)(BufferContentLen - MARKPOSITION));

                            //Identify The Number Of File Data To Send ;
                            TCPBase.StoreDataLenByts((uint)(BufferContentLen - MARKPOSITION), byt_MainChannel);
                            TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_TRANSFER);

                            //Send 1024 Bytes File Data To 
                            sct.Send(byt_MainChannel);
                        }
                        if (Spare != 0)
                        {
                            //Read The Last Bytes 
                            fs.Read(byt_MainChannel, MARKPOSITION, Spare);

                            TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_TRANSFER);
                            //Identify The Number Of File Data To Send ;
                            TCPBase.StoreDataLenByts((uint)Spare, byt_MainChannel);
                            // Send The Bytes Of The File 
                            sct.Send(byt_MainChannel);
                        }
                        // Content Mark 
                        //Info The File To Send Is Finished ! ;
                        // File End Mark 
                        TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_END);
                        //Send The Last Time ;
                        sct.Send(byt_MainChannel);
                        DisposeFileStream();
                    }
                 
                    return true;
                }
                catch
                {
                    if(fs != null)
                    {
                        DisposeFileStream();
                    }
                    return false;
                }
            }
            void PrepareFile (bool isRead)
            {
                
            }
            public virtual void OnFileBegine() {
                uint nFileNameLen = TCPBase.FetchDataLenByts(byt_MainChannel);
                string strFileName = en.GetString(byt_MainChannel, MARKPOSITION, (int)nFileNameLen);
                int FileLen = BitConverter.ToInt32(byt_MainChannel, (int)nFileNameLen + 5);
                int SendTimes = BitConverter.ToInt32(byt_MainChannel, (int)nFileNameLen + 5 + 4);
                StartFileTransfer(strFileName, false);
            }
            public virtual String RecieveFileName()
            {
                uint nFileNameLen = TCPBase.FetchDataLenByts(byt_MainChannel);
               return en.GetString(byt_MainChannel, MARKPOSITION, (int)nFileNameLen);
            }
            public virtual void OnFileTransfer() {
                uint nLen = TCPBase.FetchDataLenByts(byt_MainChannel);
                fs.Write(byt_MainChannel, MARKPOSITION, (int)nLen);
                fs.Flush();
            }
            public virtual void OnFileEnd() {
                fs.Flush();
                DisposeFileStream();
            }
            public virtual void SendDownloadCommand(string strFileName)
            {
                byte[] bytArr = en.GetBytes(strFileName);
                TCPBase.StoreMSGMark(byt_MainChannel, Signals.DOWNLOAD_FILE);
                TCPBase.StoreDataLenByts((uint)bytArr.Length, byt_MainChannel);
                bytArr.CopyTo(byt_MainChannel, MARKPOSITION);
                sct.Send(this.byt_MainChannel);
            }
        }
        public abstract class TCPBase
        {
            public delegate void RouteMessageCallback(int nWhichClient,byte[] byts);
            public delegate void FileDownloadBeginCallback(string strFileName,int nFileSize,int nSendTimes);
            public delegate void FileDownloadingCallback(int nRecievedTimes);
            public delegate void FileDownEndCallback();
            public delegate void FileUploadBegin(string strFileName, int nFileSize, int nSendTimes);
            public delegate void FileUploading(int nSentTimes);
            public delegate void FileUploadEnd();
            protected bool bEndNetwork =false ;
            protected int nPort = 8040;
            protected String strErrorInfo = null;
            protected IPAddress ServerIP;
            protected Encoding en =  Encoding.UTF8;
            public System.Text.Encoding TextEncode
            {
                get { return en; }
                set { en = value; }
            }
            protected Thread thd_Main;
            protected const byte SPLITMARK = 94;
            protected uint nBufferSize ;
            protected int nLoopGapTime ;
            private String strIP;
            public List<FileTransferInfo> lst_FileTransferInfo;
            public const byte MARKPOSITION = 5;
            public static void StoreDataLenByts(uint nLen,
            byte[] buffer)
            {
                BitConverter.GetBytes(
                      nLen).CopyTo(buffer, 1);
            }
            public static uint FetchDataLenByts(byte[] buffer)
            {
                byte[] byt_Len = new byte[4];
                for (int i = 1; i < 5; i++)
                {
                    byt_Len[i - 1] = buffer[i];
                }
                uint byteNum = BitConverter.
                        ToUInt32(byt_Len, 0
                       );
                byt_Len = null;
                return byteNum;
            }
            public static void StoreMSGMark(byte[] buffer, byte byt)
            {
                buffer[0] = byt;
            }
            public static byte FetchMSGMark(byte[] buffer)
            {
                return buffer[0];
            }
            public virtual void DefaultConfig()
            {
                TextEncode = Encoding.UTF8;
                 BufferSize = 1029;
                LoopGapTime = 100;
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
                get {
                     return Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString() ;
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
               // Encoding.Unicode
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

            public  byte[] ObjectToBytes(object obj)
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
            public   object BytesToObject(byte[] Bytes, int index, int count)
            {
                using (MemoryStream ms = new MemoryStream(Bytes,index,count))
                {
                    IFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(ms);
                }
            }

        }
    }
}
