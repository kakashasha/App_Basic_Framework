using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.ComponentModel;
using System.Drawing.Design;
using System.Threading;

namespace dotNetLab.Widgets.UIBinding
{

    public delegate void UpdateTextPropertyCallback(Control ctrl, string strTable);
    public delegate void FillTextPropertyCallback(Control ctrl, string strField, string strFilter);
    public enum NotifyMessages { Read, Write, NoControlRead, NoControlWrite }
    [Editor(typeof(DataBindingVisualPropertyEditor),typeof(UITypeEditor))]
    public class UIElementBinderInfo
    {
        public delegate void DBDataNotifyCallback(Control c, NotifyMessages nmgs, params string[] strArr);
        public DBDataNotifyCallback DBDataNotify = null;
        Control ctrl;
        string strFieldName;
        string strFilter;
        string strTableName;
        bool bStoreInDB;
        bool bStoreIntoDBRealTime = true;
    
     
       
        public bool StoreIntoDBRealTime
        {
            get { return bStoreIntoDBRealTime; }
            set { bStoreIntoDBRealTime = value; }
        }
        //默认为嵌入式数据库
        int nIndex_DBEngine = 0;
        //True Type C
        Pointer ptr;
        bool bEnable_Bool_One_Zero = false;
        string strSQL_Query = "select {0} from {1} where {2} ;";
        string strSQLEx_Query = "select count(*) from {1} ;";
        string strSQL_Update = "Update {0} set {1}='{2}' where {3} ;";
        public string TableName
        {
            get { return strTableName; }
            set { strTableName = value; }
        }
        public string QuerySQLProvider
        {
            get
            {
                if (!string.IsNullOrEmpty(strFilter))
                {
                    return string.Format(strSQL_Query, FieldName, strTableName, Filter);
                }
                else
                    return string.Format(strSQLEx_Query, strTableName);
            }

        }
        public string UpdateSQLProvider
        {
            get
            {
                CheckBox cbx = ctrl as CheckBox;
                String strF = null;
                if (cbx != null)
                {

                    strF = string.Format(strSQL_Update, TableName, FieldName, cbx.Checked.ToString(), this.Filter);
                }
                else
                {
                    strF = string.Format(strSQL_Update, TableName, FieldName, (ctrl as UIElement).MainBindableProperty, this.Filter);
                }
                return strF;

            }
        }
        public string Filter
        {
            get { return strFilter; }
            set { strFilter = value; }
        }
        public string FieldName
        {
            get { return strFieldName; }
            set
            {
                strFieldName = value;
            }
        }
        public bool StoreInDB
        {
            get
            {
                return bStoreInDB;
            }
            set
            {
                bStoreInDB = value;
            }
        }
        public Pointer Ptr
        {
            get { return ptr; }
            set { ptr = value; }
        }
        public bool EnableCheckBox_One_Zero
        {
            get { return bEnable_Bool_One_Zero; }
            set { bEnable_Bool_One_Zero = value; } 
        }
        public Control ThisControl
        {
            get { return ctrl; }
            set { ctrl = value; }

        }
        public int DBEngineIndex
        {
            get
            {
                return this.nIndex_DBEngine;
            }
            set
            {
                nIndex_DBEngine = value;
            }
        }
   
       
 


    }
    public class UIElementDataBinding
    {
            List<DbCommand>  cmd_List;
            List<Control> lst_Ctrls = null;
            List<UIElementBinderInfo> lst_UIElementBinderInfo;
            public bool bIsUpateAllCtrls = true;
    
            public UpdateTextPropertyCallback UpdateTextPropertyDelegator;
            public FillTextPropertyCallback FillTextPropertyDelegator;
            public bool bIsSynchronDBData = true;
            public List<Control> BindedCtrls
            {
                get
                {
                    return lst_Ctrls;
                }
            }
            public List<UIElementBinderInfo> UIElementBinderCollection
            {

                get
                {
                    return lst_UIElementBinderInfo;
                }
                set
                {
                    lst_UIElementBinderInfo = value;
                }

            }
            public List<DbCommand> DbCommandList
            {

                get { return cmd_List; }
                set { cmd_List = value; }
            }
            public UIElementDataBinding( )
            {
           
                lst_Ctrls = new List<Control>();
                lst_UIElementBinderInfo = new List<UIElementBinderInfo>();
                DbCommandList = new List<DbCommand>();
                this.FillTextPropertyDelegator = Fill;
                this.UpdateTextPropertyDelegator = Synchron;

            }
         
            public void AddBindItem(UIElementBinderInfo bndInfo)
            {
                this.UIElementBinderCollection.Add(bndInfo);
              //  new Thread(FindFormContainer).Start(bndInfo);
           
                UIElement element = bndInfo.ThisControl as UIElement;
                lst_Ctrls.Add(element);

                Fill(element, bndInfo.FieldName, bndInfo.Filter);
               
                //FillTextPropertyDelegator(element, bndInfo.FieldName, bndInfo.Filter);
           
            if (bndInfo.StoreIntoDBRealTime)
                {
                     // element = bndInfo.ThisControl as UIElement;
                    element.BindablePropertyChanged += BindingCtrlPropertyChanged;


                }

                
                
            }
            private void Cbx_CheckedChanged(object sender, EventArgs e)
            {
                Control ctrl = sender as Control;
               
                int n = lst_Ctrls.IndexOf(ctrl);
                if (lst_UIElementBinderInfo[n].StoreInDB)
                    UpdateTextPropertyDelegator(ctrl, lst_UIElementBinderInfo[n].TableName);
            }
            private void  BindingCtrlPropertyChanged(Control ctrl,String strValue )
            {
                
                if (bIsUpateAllCtrls)
                    UpdateAllCtrl(ctrl);
                int n = lst_Ctrls.IndexOf(ctrl);
                if (lst_UIElementBinderInfo[n].StoreInDB)
                    UpdateTextPropertyDelegator(ctrl, lst_UIElementBinderInfo[n].TableName);

                
            }
           public static Form GetForm(Control thisParentForm )
        {
            Control ctrlbackup = thisParentForm;
            Form frm = null;
            while (true)
            {
                try
                {

                      frm = thisParentForm as Form;
                    if (frm == null)

                        thisParentForm = thisParentForm.Parent;
                    else

                        break;

                }
                catch (Exception e)
                {

                    thisParentForm = ctrlbackup;
                    continue;

                }


            }
            return frm;
        }
            private void UpdateAllCtrl(Control ctrl)
            {
                int n = lst_Ctrls.IndexOf(ctrl);
                string strInfer = lst_UIElementBinderInfo[n].Filter;
                string strTableName_ = lst_UIElementBinderInfo[n].TableName;
                for (int i = 0; i < lst_Ctrls.Count; i++)
                {
                    if (lst_UIElementBinderInfo[i].Filter == strInfer)
                    {
                    if (i != n)
                    {

                        if (strTableName_ == lst_UIElementBinderInfo[i].TableName)
                        {
                            (lst_Ctrls[i] as UIElement).MainBindableProperty = (ctrl as UIElement).MainBindableProperty;

                        }
                    }
                    }

                }
            }
            public int WhichControl(Control ctrl)
            {
                int n = lst_Ctrls.IndexOf(ctrl);
                return n;
            }
            public UIElementBinderInfo WhichUIElementBinderInfo(Control ctrl)
            {
                int n = WhichControl(ctrl);
                return this.lst_UIElementBinderInfo[n];
            }
            public string GetQuerySQL(Control ctrl)
            {
                 int n = lst_Ctrls.IndexOf(ctrl);

                 return lst_UIElementBinderInfo[n].QuerySQLProvider;
            }
            public string GetUpdateSQL(Control ctrl )
            {
                int n =lst_Ctrls.IndexOf(ctrl);
                return lst_UIElementBinderInfo[n].UpdateSQLProvider;
            }
            protected void Synchron(Control ctrl, string strTable)
            {
                int nDbCommandIndex = this.WhichUIElementBinderInfo(ctrl).DBEngineIndex;

                DbCommand cmd = DbCommandList[nDbCommandIndex];
                cmd.CommandText = this.GetUpdateSQL(ctrl);
                CheckBox cbx = ctrl as CheckBox;
                if (cbx == null)
                {
                UIElement ele = (ctrl as UIElement);
                cmd.CommandText = this.GetQuerySQL(ctrl);
                String str =  cmd.ExecuteScalar().ToString();
                if (!str.Equals(ele.MainBindableProperty))
                {
                    cmd.CommandText = this.GetUpdateSQL(ctrl);
                    cmd.ExecuteNonQuery();
                }

                }
                else
                {
                    UIElementBinderInfo dat = this.WhichUIElementBinderInfo(ctrl);
                    // String strValue = cmd.ExecuteScalar().ToString();
                    if (!dat.EnableCheckBox_One_Zero)
                         cmd.ExecuteNonQuery();
                    else
                    {
                        String str = "1";
                        if (!cbx.Checked)
                        {
                            str = "0";
                        }
                        cmd.CommandText = string.Format("Update {0} set {1}='{2}' where {3} ;", dat.TableName, dat.FieldName, str, dat.Filter);
                        cmd.ExecuteNonQuery();
                    }
                }

                UIElementBinderInfo data = this.WhichUIElementBinderInfo(ctrl);
                if (data.Ptr != null)
                {
                    char ch = '\'';
                    int nStart = cmd.CommandText.IndexOf(ch) + 1;
                    int nEnd = cmd.CommandText.IndexOf(" where") - 1;
                    try
                    {
                        data.Ptr.Value(
                            Convert.ToDouble(
                                cmd.CommandText.Substring(nStart, nEnd - nStart))
                        );
                        if (data.Ptr.EndModify != null)
                        {
                            data.Ptr.EndModify();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }

                }
                if (data.DBDataNotify != null)
                    data.DBDataNotify(ctrl,  NotifyMessages.Write, strTable);

            }
            protected void Fill(Control ctrl,
                string strField,
                string strFilter)
            {
                int nDbCommandIndex = this.WhichUIElementBinderInfo(ctrl).DBEngineIndex;

                DbCommand cmd = DbCommandList[nDbCommandIndex];
                cmd.CommandText = this.GetQuerySQL(ctrl);

                CheckBox cbx = ctrl as CheckBox;
                if (cbx == null)
                {

                lock (this)
                {
                    try
                    {
                        String str = cmd.ExecuteScalar().ToString();
                        UIElement ele = (ctrl as UIElement);
                        if (!ele.MainBindableProperty.Equals(str))
                            ele.MainBindableProperty = cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception e)
                    {

                        Tipper.Error = e.Message;
                    }
                   
                }


            }
            else
                {
                    UIElementBinderInfo dat = this.WhichUIElementBinderInfo(ctrl);
                    String strValue = cmd.ExecuteScalar().ToString();
                    if (!dat.EnableCheckBox_One_Zero)
                        try
                        {
                            cbx.Checked = Convert.ToBoolean(strValue);

                        }
                        catch (Exception e)
                        {
                            if (strValue == "0")
                                cbx.Checked = false;
                            else

                                cbx.Checked = true;

                        }
                    else
                    {
                        if (strValue.Equals("0"))
                            cbx.Checked = false;
                        else
                            cbx.Checked = true;

                    }
                }
                UIElementBinderInfo data = this.WhichUIElementBinderInfo(ctrl);
                if (data.Ptr != null)
                {
                    try
                    {
                        data.Ptr.Value(
                            Convert.ToDouble(
                                (ctrl as UIElement).MainBindableProperty
                            )
                        );
                        if (data.Ptr.EndModify != null)
                        {
                            data.Ptr.EndModify();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }

                }
                if (data.DBDataNotify != null)
                    data.DBDataNotify(ctrl,  NotifyMessages.Read);

            }
    }
}

/*
    void FindFormContainer(Object obj)
        {
            UIElementBinderInfo bndInfo = obj as UIElementBinderInfo;


            while (true)
            {
                Form frm = GetForm(bndInfo.ThisControl);
                Form1 form1 = frm as frm;
                IContainer container = frm.Container;
                if (container != null)
                {
                    bndInfo.ReadDataFromDbTimer = new System.Windows.Forms.Timer(container);
                    bndInfo.ReadDataFromDbTimer.Tick += ReadDataFromDbTimer_Tick;
                    bndInfo.ReadDataFromDbTimer.Tag = UIElementBinderCollection.IndexOf(bndInfo);
                    bndInfo.ReadDataFromDbTimer.Interval = 1000;
                    bndInfo.ReadDataFromDbTimer.Interval = bndInfo.IntervalTime;
                    bndInfo.ReadDataFromDbTimer.Enabled = true;
                    bndInfo.ReadDataFromDbTimer.Start();
                    break;
                }

            }
           
        }
            private void ReadDataFromDbTimer_Tick(object sender, EventArgs e)
        {
             System.Windows.Forms.Timer tmr = sender as System.Windows.Forms.Timer;
            int nIndex = Convert.ToInt32(tmr.Tag);
            Control ctrl = lst_Ctrls[nIndex];
            int nDbCommandIndex = this.WhichUIElementBinderInfo(ctrl).DBEngineIndex;

            DbCommand cmd = DbCommandList[nDbCommandIndex];
            cmd.CommandText = this.GetQuerySQL(ctrl);
                lock (this)
                {
                    String str =  cmd.ExecuteScalar().ToString();
                    UIElement ele  =(ctrl as UIElement);
                  if (!ele.MainBindableProperty.Equals(str))
                     ele.MainBindableProperty = cmd.ExecuteScalar().ToString();
                }

            UIElementBinderInfo data = this.WhichUIElementBinderInfo(ctrl);
            if (data.Ptr != null)
            {
                try
                {
                    data.Ptr.Value(
                        Convert.ToDouble(
                            (ctrl as UIElement).MainBindableProperty
                        )
                    );
                    if (data.Ptr.EndModify != null)
                    {
                        data.Ptr.EndModify();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                }

            }
            if (data.DBDataNotify != null)
                data.DBDataNotify(ctrl, NotifyMessages.Read);

        }
    */
