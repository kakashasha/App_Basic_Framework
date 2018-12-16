using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetLab.Data 
{
    public abstract class   DB 
    {
        protected String DBName;
        protected int nPort;
        protected String strIP;
        public String Database
        {
            get { return DBName; }
            set { DBName = value; }
        }
    }
}
