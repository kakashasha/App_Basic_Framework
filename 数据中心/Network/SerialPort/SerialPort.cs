using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace dotNetLab
{
    namespace Network
    {
       
        public  class SerialPortEx
        {
            public delegate void SerialPortCallback(byte[] bytArr);
            public delegate void SerialPortConnectedCallback();
            public event SerialPortConnectedCallback  SerialPortConnected;
            protected Encoding en;
            protected Thread thd_Main;
            protected const byte SPLITMARK = 94;
            protected uint nBufferSize;
            SerialPort baseSerialPort;
            private string strPortName;
            private int nBaudRate;
            private string strEnuParity;
            private Parity EnuParity;
            private int nDataBit;
            private StopBits enuStopBits;
            public byte[] buffer;
            bool bConnected = false ;
            public event SerialPortCallback SerialPortDataRecieved;
            protected void DiscardIOBuffer()
            {
                 
                baseSerialPort.DiscardOutBuffer();
                baseSerialPort.DiscardInBuffer();
            }
            public void Prepare()
            {
                try
                {
                    baseSerialPort = new SerialPort(PortName, BaudRate, EnuParity, DataBit, enuStopBits);
                    baseSerialPort.Encoding = Encoding.ASCII;
                    baseSerialPort.DtrEnable = true;
                    baseSerialPort.RtsEnable = true;
                    baseSerialPort.ReadBufferSize = baseSerialPort.WriteBufferSize = (int)BufferSize;
                    buffer = new byte[this.baseSerialPort.ReadBufferSize];
             
                }
                catch (Exception e)
                {
                    MessageBox.Show("初始化串口失败！\r\n"+e.Message,"提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                
                
            }
            public bool  Open()
            {
                try
                {
                    baseSerialPort.Open();
                    baseSerialPort.DataReceived += BaseSerialPort_DataReceived
                        ;
                    DiscardIOBuffer();
                    SerialPortConnected();
                    return true;
                }
                catch (Exception e )
                {

                    return false;
                }
              
            }
            public void  Close()
            {
                if (baseSerialPort.IsOpen)
                {
                    baseSerialPort.Close();
                    baseSerialPort.Dispose();
                }
            }
            private void BaseSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
            {
                 baseSerialPort.Read(buffer, 0, buffer.Length);
                 baseSerialPort.DiscardInBuffer();
                 SerialPortDataRecieved(buffer);
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
            public  void Send(byte [] bytArr)
            {
                baseSerialPort.Write(bytArr, 0, bytArr.Length);
            }
            public byte [] ProvideBytes (ushort ust)
            {
                return BitConverter.GetBytes(ust);
            }
            public byte[] ProvideBytes(String str)
            {
                return Encoding.ASCII.GetBytes(str);
            }
            public byte [] ProvideBytes(short srt)
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
            public object ProvideNum (byte [] bytArr,int nStartIndex,int nBytesConsist,bool isU)
            {
               
                switch (nBytesConsist)
                {
                 case 2:
                        if(isU)
                            return   BitConverter.ToUInt16(bytArr, nStartIndex);
                        else
                            return BitConverter.ToInt16(bytArr, nStartIndex);
                    case 4:
                        if(isU)
                            return BitConverter.ToUInt32(bytArr, nStartIndex);
                        else
                            return BitConverter.ToInt32(bytArr, nStartIndex);
                }
                return null;
            }
            public String ProvideString(byte [] bytArr,int nIndex,int nCount)
            {
              
              return  Encoding.ASCII.GetString(bytArr, nIndex, nCount);
            }
            public String PortName { get { return strPortName; } set { strPortName = value; } }
            public int BaudRate{get { return nBaudRate; }set { this.nBaudRate = value; }}
            public string _Parity
            {
            
                set
                {
                    switch (value)
                    {
                        case "NONE": this.  EnuParity = Parity.None; break;
                        case "ODD": this.  EnuParity = Parity.Odd; break;
                        case "EVEN": this. EnuParity = Parity.Even; break;
                        case "MARK": this. EnuParity = Parity.Mark; break;
                        case "SPACE": this. EnuParity = Parity.Space; break;
                    }

                }

            }
            public int DataBit { get { return nDataBit; } set { nDataBit = value; } }
            public int StopBit
            {
                get
                {
                    return (int)enuStopBits;
                }
                set
                {
                    switch (value)
                    {
                        case 0:
                            this.enuStopBits = StopBits.None; break;
                        case 1: this.enuStopBits = StopBits.One; break;
                        case 2: this.enuStopBits = StopBits.Two; break;
                        case 3: this.enuStopBits = StopBits.OnePointFive; break;
                    }
                }
            }

            public bool Connected { get { return bConnected; } set { bConnected = value; } }
            public void Writeword(string add, long Num)
            {

                
                try
                {

                    char[] chArr = new char[8];
                    for (int i = 0; i < 8; i++)
                    {
                        chArr[i] = '0';
                    }
                    int n = 7  ;
                    StringBuilder str = new StringBuilder();
                    str.Append(Num.ToString("X4"));
                      if(str.Length<8)
                      {
                          for (int i = str.Length-1; i > 0; i--)
                          {
                              chArr[n--] = str[i];
                          }
                      }
                      String s = chArr.ToString();
                    string B = s.Substring(s.Length - 7, 4) ;
                    string C =  s.Substring( s.Length - 3, 4);
                    this.baseSerialPort.Write(add + C + B);
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
               
            }
        }
    }
}
