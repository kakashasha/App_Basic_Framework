using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotNetLab.Data.Network
{
   public class NormalPLC : PLCBase
    {

        public Action<byte[]> Route;
        public NormalPLC()
        {
            Route = null;
        }

        public override bool Close()
        {
            try
            {

                if (_rs232 != null)
                {
                    _rs232.DiscardInBuffer();
                    _rs232.DiscardOutBuffer();
                    _rs232.Close();
                    _rs232.Dispose();
                    _rs232 = null;
                }
                Console.WriteLine("已经关闭串口：模式为通用。");
                return true;
            }
            catch (Exception e)
            {

                _rs232 = null;
                Tipper.Error = ("关闭通用作用口时失败：" + e.Message);
                return false;
            }
        }
        public override bool Open()
        {
            Close();
            try
            {
                _rs232 = new System.IO.Ports.SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
                _rs232.Encoding = ThisEncoding;
                //this.BufferSize = 128;
                buffer = new byte[this.BufferSize];
                //_rs232.DtrEnable = true;
                //_rs232.RtsEnable = true;
                //_rs232.ReceivedBytesThreshold = 64;
                _rs232.Open();
                _rs232.DiscardOutBuffer();
                _rs232.DiscardInBuffer();
                //_rs232.Handshake = System.IO.Ports.Handshake.None;
                _rs232.DataReceived += (sender, e) =>
                {
                    try
                    {

                        ClearBuffer();

                        int by = 0;
                        int z = 0;
                        do
                        {
                            by = _rs232.ReadByte();
                            buffer[z++] = (byte)by;
                        } while (-1 != by);

                        if (Route != null)
                            Route(buffer);

                    }
                    catch (Exception ex)
                    {


                    }
                  

                };

                Console.WriteLine("已经开启串口：模式为通用。");
                return true;
            }
            catch (Exception e)
            {

                Tipper.Error = "通用作用串口打开失败：" + e.Message;
                return false;
            }

        }
        void ClearBuffer()
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)'\0';
            }
        }
        public void Send(byte[] buf)
        {
            if (null == _rs232)
            {
                Tipper.Error = ("串口不可用");
            }

            if (!_rs232.IsOpen)
            {
                Tipper.Error = (_rs232.PortName + " 未打开");
            }

            try
            {
                _rs232.Write(buf, 0, buf.Length);
                // Tipper.Info = ("串口发送数据成功。");
            }
            catch (Exception ex)
            {
                Tipper.Error = "SerialPortSend错误:" + ex.Message;
            }
        }
    }
}
