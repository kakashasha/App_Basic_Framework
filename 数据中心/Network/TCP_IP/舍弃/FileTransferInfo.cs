using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotNetLab.Data.Network.TCP_IP.舍弃
{
    //public class FileTransferInfo
    //{

    //    public byte[] byt_MainChannel;
    //    public Socket sct;
    //    public FileStream fs;
    //    public Encoding en;
    //    public byte MARKPOSITION = 5;
    //    public FileTransferInfo() { }
    //    public FileTransferInfo(Encoding en, byte[] byt_MainChannel, Socket sct)
    //    {
    //        this.en = en;
    //        this.byt_MainChannel = byt_MainChannel;
    //        this.sct = sct;
    //    }
    //    void StartFileTransfer(string strFileName, bool isRead)
    //    {
    //        if (!isRead)
    //            this.fs = new FileStream(strFileName, FileMode.Create);
    //        else
    //            this.fs = new FileStream(strFileName, FileMode.Open);
    //    }
    //    void DisposeFileStream()
    //    {
    //        fs.Close();
    //        fs.Dispose();
    //    }
    //    public virtual bool SendFile(string FilePath)
    //    {
    //        try
    //        {

    //            StartFileTransfer(FilePath, true);
    //            long FileLength = fs.Length;
    //            int BufferContentLen = byt_MainChannel.Length;
    //            long SendTimes = 0;
    //            SendTimes = (FileLength / (BufferContentLen - MARKPOSITION));
    //            //Short File Path ;
    //            string MereFileName = Path.GetFileName(FilePath);
    //            //Get Short File Path Bytes ;
    //            byte[] temp = en.GetBytes(MereFileName);
    //            temp.CopyTo(byt_MainChannel, MARKPOSITION);
    //            byte[] bytArr_FileLen = BitConverter.GetBytes((int)FileLength);
    //            bytArr_FileLen.CopyTo(byt_MainChannel, temp.Length + 5);
    //            byte[] bytArr_SendTimes = BitConverter.GetBytes((int)SendTimes + 1);
    //            bytArr_SendTimes.CopyTo(byt_MainChannel, temp.Length + 5 + 4);
    //            // Get Short File Path Bytes Length ;
    //            //And Store The Length Into DataBytesNum [1025] [1026] ;
    //            TCPBase.StoreDataLenByts((uint)temp.Length, byt_MainChannel);
    //            // Content Mark  Is File Name Mark ;
    //            TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_BEGIN);

    //            //Send File Name To Someone ;
    //            sct.Send(byt_MainChannel);

    //            //Change Content Mark To Be File Data Mark  ;
    //            //  StoreMSGMark(byt_MainChannel,);

    //            // Create FileStream To Read File  Data Into Array byt_MainChannel ;

    //            //Gain File Length ;

    //            //Record How Times To Send ;

    //            //if File Length  Is Less Than 1024 
    //            //Read  All File Data Into byt_MainChannel ; 
    //            if (FileLength <= BufferContentLen - MARKPOSITION)
    //            {
    //                fs.Read(byt_MainChannel, MARKPOSITION, (int)FileLength);

    //                TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_TRANSFER);
    //                //Identify The Number Of File Data To Send ;
    //                TCPBase.StoreDataLenByts((uint)FileLength, byt_MainChannel);
    //                sct.Send(byt_MainChannel);
    //                TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_END);
    //                //Send The Last Time ;
    //                sct.Send(byt_MainChannel);
    //                DisposeFileStream();
    //            }
    //            //else 
    //            else
    //            {
    //                // Gain The Times To Send ;

    //                //Gain The Last Bytes To Send  ;
    //                int Spare = (int)(FileLength - (BufferContentLen - MARKPOSITION) * SendTimes);
    //                //Loop And Send File Data ;
    //                for (; SendTimes > 0; SendTimes--)
    //                {
    //                    fs.Read(byt_MainChannel, MARKPOSITION, (int)(BufferContentLen - MARKPOSITION));

    //                    //Identify The Number Of File Data To Send ;
    //                    TCPBase.StoreDataLenByts((uint)(BufferContentLen - MARKPOSITION), byt_MainChannel);
    //                    TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_TRANSFER);

    //                    //Send 1024 Bytes File Data To 
    //                    sct.Send(byt_MainChannel);
    //                }
    //                if (Spare != 0)
    //                {
    //                    //Read The Last Bytes 
    //                    fs.Read(byt_MainChannel, MARKPOSITION, Spare);

    //                    TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_TRANSFER);
    //                    //Identify The Number Of File Data To Send ;
    //                    TCPBase.StoreDataLenByts((uint)Spare, byt_MainChannel);
    //                    // Send The Bytes Of The File 
    //                    sct.Send(byt_MainChannel);
    //                }
    //                // Content Mark 
    //                //Info The File To Send Is Finished ! ;
    //                // File End Mark 
    //                TCPBase.StoreMSGMark(byt_MainChannel, Signals.FILE_END);
    //                //Send The Last Time ;
    //                sct.Send(byt_MainChannel);
    //                DisposeFileStream();
    //            }

    //            return true;
    //        }
    //        catch
    //        {
    //            if (fs != null)
    //            {
    //                DisposeFileStream();
    //            }
    //            return false;
    //        }
    //    }
    //    void PrepareFile(bool isRead)
    //    {

    //    }
    //    public virtual void OnFileBegine()
    //    {
    //        uint nFileNameLen = TCPBase.FetchDataLenByts(byt_MainChannel);
    //        string strFileName = en.GetString(byt_MainChannel, MARKPOSITION, (int)nFileNameLen);
    //        int FileLen = BitConverter.ToInt32(byt_MainChannel, (int)nFileNameLen + 5);
    //        int SendTimes = BitConverter.ToInt32(byt_MainChannel, (int)nFileNameLen + 5 + 4);
    //        StartFileTransfer(strFileName, false);
    //    }
    //    public virtual String RecieveFileName()
    //    {
    //        uint nFileNameLen = TCPBase.FetchDataLenByts(byt_MainChannel);
    //        return en.GetString(byt_MainChannel, MARKPOSITION, (int)nFileNameLen);
    //    }
    //    public virtual void OnFileTransfer()
    //    {
    //        uint nLen = TCPBase.FetchDataLenByts(byt_MainChannel);
    //        fs.Write(byt_MainChannel, MARKPOSITION, (int)nLen);
    //        fs.Flush();
    //    }
    //    public virtual void OnFileEnd()
    //    {
    //        fs.Flush();
    //        DisposeFileStream();
    //    }
    //    public virtual void SendDownloadCommand(string strFileName)
    //    {
    //        byte[] bytArr = en.GetBytes(strFileName);
    //        TCPBase.StoreMSGMark(byt_MainChannel, Signals.DOWNLOAD_FILE);
    //        TCPBase.StoreDataLenByts((uint)bytArr.Length, byt_MainChannel);
    //        bytArr.CopyTo(byt_MainChannel, MARKPOSITION);
    //        sct.Send(this.byt_MainChannel);
    //    }
    //}
}
