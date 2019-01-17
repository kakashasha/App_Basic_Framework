using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace dotNetLab.Data.Network
{
   public  class AddressPLC : PLCBase
    {

        
       public void Send2Plc(String Head,String Adrr,String End)
        {

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
               

                Console.WriteLine("已经开启串口：模式为通用。");
                return true;
            }
            catch (Exception e)
            {

                Tipper.Error = "通用作用串口打开失败：" + e.Message;
                return false;
            }

        }


    }
}
