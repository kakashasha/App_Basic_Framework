using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using dotNetLab.Data;
using dotNetLab.Data.DBEngines.SQLite;
using dotNetLab.Data.Embedded;
using dotNetLab.Forms;
using System.ComponentModel;
using dotNetLab.Data.Uniting;
using dotNetLab.Widgets.UIBinding;

namespace dotNetLab.Common.Normal
{
    public class PageBase :Form
    {
        UIElementDataBinding UIElementBinderObject;
        [Browsable(false)]
        public UIElementDataBinding UIElementBinders
        {
            get { return UIElementBinderObject; }
            set { UIElementBinderObject = value; }

        }
   
        public UnitDB CompactDB
        {
            get { return R.CompactDB; }
        }

        public UIConsole ConsolePipe
        {
            get
            {
                return  R.Pipe;
            }
        }
        public LogPipe LogPipe
        {
            get
            {
                return R.LogConsolePipe;
            }
        }
     
        public PageBase()
        {
            Prepare();
       
        }

    
      
        protected virtual void Prepare()
        {
            InitDB();
            PrepareCtrls();
            PrepareData();
            UnitCtrls();
            PrepareEvents();
         
        }


        protected virtual void UnitCtrls(){}

        protected virtual void PrepareCtrls(){}
        protected virtual void PrepareEvents(){}
        protected virtual void PrepareData(){}
        protected  virtual  void CompactDbOnDbDiagnoseHandler(ErrorInfo errorInfo, DBOperator bytOperator)
        {
            R.Pipe.Error(errorInfo.ExceptionInfo);
        }

        protected virtual void InitSubForm<T>(out T frm){WinFormApp._manager.InitSubForm(out frm);}
        private void InitDB()
        {
            if (R.CompactDB == null)
            {
                R.CompactDB = new UnitDB();
                R.Pipe = new UIConsole();
                R.LogConsolePipe = new LogPipe();
                R.CompactDB.DBDiagnoseHandler += CompactDbOnDbDiagnoseHandler;
                R.CompactDB.Connect(DBEngineNames.SQLITE,"shikii.db");
                R.LogConsolePipe.ThisDB = R.CompactDB;
                if (R.DataBindingManager == null)
                    R.DataBindingManager = new dotNetLab.Widgets.UIBinding.UIElementDataBinding();
                R.DataBindingManager.DbCommandList.Add(R.CompactDB.ThisDbCommand);
                UIElementBinders = R.DataBindingManager;
            }
        }
    }
}

