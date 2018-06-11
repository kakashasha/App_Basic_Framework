using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace dotNetLab
{
    namespace Data
    {
    public partial class ErrorWnd : Form
    {
        public ErrorWnd()
        {
            InitializeComponent();
        }

        public void ErrorRecord(String str)
         {
           this.txb_Description.Text += str;
          }
    }
    }
}
