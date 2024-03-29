﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using dotNetLab.Data;
using dotNetLab.Data.DBEngines.SQLite;
using dotNetLab.Data.Embedded;
using dotNetLab.Data.Uniting;
using dotNetLab.Forms;

namespace dotNetLab.Common.Ribbon
{
    public  class PageBase:RibbonPage
    {
        public delegate Object __WinForm_Internal_Delegate_Callback(params object[] objArr);
         private __WinForm_Internal_Delegate_Callback __WinForm_Internal_Delegate;
       protected MethodInfo[] MethodArray;
        protected  Type ThisType;
        protected List<String> MethodNames;
        private String CurrentDelegateMethodName = null;
        public UnitDB CompactDB
        {
            get { return R.CompactDB; }
        }
        public LogPipe LogPipe
        {
            get
            {
                return R.LogConsolePipe;
            }
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
        public PageBase()
        {
            Prepare();
            this.__WinForm_Internal_Delegate = __Invoke_Internal;
        }

        public virtual Object Invoke(String MethodName,params object[] objArr)
        {
            this.CurrentDelegateMethodName = MethodName;
           return this.Invoke(__WinForm_Internal_Delegate, objArr);
        }
        /*
         获取类型信息
            Type  t  =  Type.GetType("TestSpace.TestClass");
//构造器的参数
        object[]  constuctParms  =  new  object[]{"timmy"};
//根据类型创建对象
        object  dObj  =  Activator.CreateInstance(t,constuctParms);
//获取方法的信息
        MethodInfo  method  =  t.GetMethod("GetValue");
//调用方法的一些标志位，这里的含义是Public并且是实例方法，这也是默认的值
        BindingFlags  flag  =  BindingFlags.Public  |  BindingFlags.Instance;
//GetValue方法的参数
        object[]  parameters  =  new  object[]{"Hello"};
//调用方法，用一个object接收返回值
        object  returnValue  =  method.Invoke(dObj,flag,Type.DefaultBinder,parameters,null);
         * 
         */
        protected virtual Object __Invoke_Internal(params object[] objArr)
        {
            if(CurrentDelegateMethodName==null)
            return null;
            else
            {
                try
                {
                    int nIndex = this.MethodNames.IndexOf(CurrentDelegateMethodName);
                  return  MethodArray[nIndex].Invoke(this, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static,
                        Type.DefaultBinder, objArr, null);
                }
                catch (Exception e)
                {
                  ConsolePipe.Error(e);
                    return null;
                }
               
                
            }
        }
        protected virtual void Prepare()
        {
            InitDB();
            PrepareCtrls();
            PrepareData();
            UnitCtrls();
            PrepareEvents();
            PrepareRefection();
        }

        protected virtual void PrepareRefection()
        {
            try
            {
                ThisType = this.GetType();

                Assembly ass = ThisType.Assembly;
                Type[] types = ass.GetTypes();
                int n = types.Length;
                for (int i = 0; i < n; i++)
                {
                    if (types[i].BaseType == ThisType)
                        ThisType = types[i];
                }
                MethodArray = ThisType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (MethodNames == null)
                    MethodNames = new List<string>();
                else
                    MethodNames.Clear();
                for (int i = 0; i < MethodArray.Length; i++)
                {
                    MethodNames.Add(MethodArray[i].Name);
                }
            }
            catch(Exception e)
            {

            }
            
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
                this.UIElementBinders = R.DataBindingManager;
            }
        }
    }
}
