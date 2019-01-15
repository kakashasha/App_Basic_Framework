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
        public override bool Close()
        {
            throw new NotImplementedException();
        }

        public override bool Open()
        {
            throw new NotImplementedException();
        }

        public void Writeword(string add, long Num)
        {


            try
            {

                char[] chArr = new char[8];
                for (int i = 0; i < 8; i++)
                {
                    chArr[i] = '0';
                }
                int n = 7;
                StringBuilder str = new StringBuilder();
                str.Append(Num.ToString("X4"));
                if (str.Length < 8)
                {
                    for (int i = str.Length - 1; i > 0; i--)
                    {
                        chArr[n--] = str[i];
                    }
                }
                String s = chArr.ToString();
                string B = s.Substring(s.Length - 7, 4);
                string C = s.Substring(s.Length - 3, 4);
                this._rs232.Write(add + C + B);
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
