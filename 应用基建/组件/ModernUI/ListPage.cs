using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using dotNetLab.Data;
using dotNetLab.Data.DBEngines.SQLite;
using dotNetLab.Data.Embedded;
using dotNetLab.Data.Uniting;
using dotNetLab.Forms;

namespace dotNetLab.Common.ModernUI
{
   public class ListPage:MetroListPage
    {
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
        public String ClipboardText
        {
            get { return Clipboard.GetText();}
            set { Clipboard.SetText(value); }
        }
        protected override void Prepare()
        {
            InitDB();
            prepareAppearance();
            prepareCtrls();
            prepareData();
            UnitCtrls();
            prepareEvents();
             
        }
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
                R.CompactDB.DBDiagnoseHandler += CompactDbOnDbDiagnoseHandler;
                R.CompactDB.Connect(DBEngineNames.SQLITE,"shikii.db");
                if (R.DataBindingManager == null)
                    R.DataBindingManager = new dotNetLab.Widgets.UIBinding.UIElementDataBinding();
                R.DataBindingManager.DbCommandList.Add(R.CompactDB.ThisDbCommand);
                UIElementBinders = R.DataBindingManager;
            }
            else
                UIElementBinders = R.DataBindingManager;
        }
     
    }
}
