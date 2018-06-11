using System;
using System.Collections.Generic;
using System.Text;
using dotNetLab.Widgets.Container;
using System.Data.Common;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace dotNetLab.Widgets.Parts 
{
    [ToolboxItem(false)]
   public partial class TableProvidePart : CanvasPanel
   {
       DataTable dt;
       string CurrentTableName;
       public DBEngineInvoker dbInvoker;
       List<String> AllTableNames;
       List<String> KeyValueTableNams, NormalTableNames;
       DataGridRealTime dataGridRealTime,dataRealTime_MaxWindow;
       DbCommand cmd;
       public DataBindingPropertyEditorDialog dataBindingPropertyEditorDialog;
       protected override void prepareData()
       {
           base.prepareData();
           if (KeyValueTableNams != null)
               KeyValueTableNams.Clear();
           if (NormalTableNames != null)
               NormalTableNames.Clear();
           this.KeyValueTableNams = new List<string>();
           NormalTableNames = new List<string>();
           
       }
       protected override void prepareEvents()
       {
           base.prepareEvents();
           this.FramePannel_Header.GetEditPanel(0);
           FramePanel_Content.GetEditPanel(0);
           dataGridRealTime = new DataGridRealTime(ref dgv_PreviewTable);
          
           txb_PatternSearch.txb.KeyUp += new System.Windows.Forms.KeyEventHandler(txb_PatternSearch_KeyUp);
           btn_GoBackFromTablePreviewToMainPart.Click += new EventHandler(btn_GoBackFromTablePreviewToMainPart_Click);
           lnk_ShowTableContent_Full.Click += new EventHandler(btn_ShowTableContent_Full_Click);
           btn_ShowColumnNames.Click += new EventHandler(btn_ShowColumnNames_Click);
           btn_ShowColumnName_1.Click += new EventHandler(btn_ShowColumnNames_Click);
           btn_Goback_2.Click += new EventHandler(btn_Goback_2_Click);
           lnk_PreviewTableContent.Click += new EventHandler(btn_ShowTableContent_Full_Click);
           btn_GainColumnNames.Click += new EventHandler(btn_GainColumnNames_Click);
           btn_CreateKeyValueView.Click += new EventHandler(btn_CreateKeyValueView_Click);
           btn_PutContentIntoClipboard.Click += new EventHandler(btn_PutContentIntoClipboard_Click);
       }

       void btn_PutContentIntoClipboard_Click(object sender, EventArgs e)
       {
           Clipboard.Clear();
           String[] temp = GetSelectedColumnNames();
           StringBuilder sb = new StringBuilder();
           for (int i = 0; i < temp.Length; i++)
           {
               sb.AppendFormat("{0} ", temp[i]);
           }
           Clipboard.SetText(sb.ToString());
       }

       void btn_CreateKeyValueView_Click(object sender, EventArgs e)
       {
           
           if (!String.IsNullOrEmpty(txb_ViewColumnNames.Text) && txb_ViewColumnNames.Text.Trim().Contains(" "))
           {
               String[] temp = txb_ViewColumnNames.Text.Trim().Split(' ');
               if(temp.Length >1)
               dbInvoker.NewKeyValueView(txb_KeyValueViewName.Text, CurrentTableName, temp[0], temp[1]);
               else
                   dotNetLab.Tipper.Tip_Info_Error("视图列数不能少于1");
           }
           else
               dotNetLab.Tipper.Tip_Info_Error("视图列名不能为空或视图列数不能少于1");
       }

        String []  GetSelectedColumnNames( )
       {
            List<String> lst = new List<String>() ;
           for (int i = 0; i < ltb_ColumnNames.Items.Count; i++)
           {
               if ((ltb_ColumnNames.Items[i] as Choice).Checked)
               {
                   lst.Add(ltb_ColumnNames.Items[i].Text);

               } 
           }
           return lst.ToArray();
       }
       void btn_GainColumnNames_Click(object sender, EventArgs e)
       {
          
           String[] temp = GetSelectedColumnNames();
           if (temp.Length ==0)
           {
               dotNetLab.Tipper.Tip_Info_Error("未选择任何列名");
               return;
           }
           StringBuilder sb = new StringBuilder();
           for (int i = 0; i < temp.Length; i++)
           {
               sb.AppendFormat("{0} ", temp[i]);
           }
           txb_SimpleColumnNames.Text = sb.ToString();
           txb_ViewColumnNames.Text = sb.ToString();
           sb.Remove(0, sb .Length - 1);
           
       }

       //void lnk_PreviewTableContent_Click(object sender, EventArgs e)
       //{
       //    dgv_PreviewTable.DataSource = dbInvoker.ProvideTable(String.Format("select * from {0}", CurrentTableName), DBOperator.OPERATOR_QUERY_TABLE);
       //    FramePanel_Content.GetEditPanel(1);
       //    FramePannel_Header.GetEditPanel(1);
         
       //}

       void btn_Goback_2_Click(object sender, EventArgs e)
       {
           FramePanel_Content.GetEditPanel(0);
           FramePannel_Header.GetEditPanel(0);
          
       }

       public void GetCheckedTableName()
       {
           CurrentTableName = null;
           for (int i = 0; i < ltb_NormalTableNames.Items.Count; i++)
           {
               if ((ltb_NormalTableNames.Items[i] as ListBoxItem_TableName).Checked  )
               {
                   CurrentTableName = ltb_NormalTableNames.Items[i].Text;
                   return; 
               }
           }
            
           for (int i = 0; i < ltb_KeyValueTableNames.Items.Count; i++)
           {
               if ((ltb_KeyValueTableNames.Items[i] as ListBoxItem_TableName).Checked)
               {
                   CurrentTableName = ltb_KeyValueTableNames.Items[i].Text;
                   return; 
               }
           }

           if (CurrentTableName == null)
           {
               
               dotNetLab.Tipper.Tip_Info_Error("必须指定表名！");
           }
       }

       void btn_ShowColumnNames_Click(object sender, EventArgs e)
       {
           FramePanel_Content.GetEditPanel(2);
           FramePannel_Header.GetEditPanel(2);
           lnk_ShowTableContent_Full.Text = CurrentTableName;
           lnk_PreviewTableContent.Text = CurrentTableName;
           GetCheckedTableName();
           DataTable dt;
           if (CurrentTableName != null)
           {
               List<String> lst = dbInvoker.GetTableColumnNames(CurrentTableName);
               ltb_ColumnNames.Clear();
               if (lst.Count >0 )
               {
                   for (int i = 0; i < lst.Count; i++)
                   {
                       ListBoxItem_ColumnName btn = new ListBoxItem_ColumnName();
                       btn.Text = lst[i];
                       btn.Radius = 0;
                       btn.PressColor = (ltb_ColumnNames.Parent as Card).HeadColor;
                       btn.Radius = 6;
                       if (btn.Width < ltb_ColumnNames.UIAdapter.Width)
                           btn.Width = ltb_ColumnNames.UIAdapter.Width;
                       ltb_ColumnNames.UIAdapter.ElementVerticalGap = 3;
                       ltb_ColumnNames.AddItem(btn);
                       
                   }
               }
               else
               {
                   dotNetLab.Tipper.Tip_Info_Error("未能列举列名，出现未知错误");
               }
           }
       }

       void btn_ShowTableContent_Full_Click(object sender, EventArgs e)
       {
           
           Form frm = new Form();
           frm.Text = CurrentTableName;
           DataGridView dgv = new DataGridView();
           dgv.Dock = DockStyle.Fill;
           dgv.DataSource = dbInvoker.
               ProvideTable(String.Format("select * from {0}", 
               CurrentTableName), DBOperator.OPERATOR_QUERY_TABLE);
           dgv_PreviewTable.DataSource = dgv.DataSource;
           frm.Controls.Add(dgv);
           frm.WindowState = FormWindowState.Maximized;
           frm.Font = new System.Drawing.Font("微软雅黑", 11);
           this.dataRealTime_MaxWindow = new DataGridRealTime(ref dgv);
           this.dataRealTime_MaxWindow.dataBindingPropertyEditorDialog = dataBindingPropertyEditorDialog;
           dataRealTime_MaxWindow.Connect(ref dgv, ref dbInvoker, CurrentTableName);
           frm.FormClosing +=   Maxfrm_FormClosing;
           frm.Show();

       }

       void Maxfrm_FormClosing(object sender, FormClosingEventArgs e)
       {
           dgv_PreviewTable.DataSource = dataRealTime_MaxWindow.dt;
       }

       void btn_GoBackFromTablePreviewToMainPart_Click(object sender, EventArgs e)
       {
           FramePanel_Content.GetEditPanel(0);
           FramePannel_Header.GetEditPanel(0);
       }

       void txb_PatternSearch_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
       {
           if (e.KeyCode == System.Windows.Forms.Keys.Enter)
           {
               if (String.IsNullOrEmpty(txb_PatternSearch.Text))
                   ShowTableNames();
               else
                   ShowPatternSearchTableNames();

            }
           
       }
       public void FetchClassifiedTableNamesFromDB()
       {
           dbInvoker.AllTableNames.Clear();
           dbInvoker.GetAllTableNames();
           if (KeyValueTableNams.Count > 0)
               KeyValueTableNams.Clear();
           if (NormalTableNames.Count > 0)
               NormalTableNames.Clear();
           InferKeyValueTable();
       }
       private void ShowPatternSearchTableNames()
       {
           FetchClassifiedTableNamesFromDB();
           String strPattern = txb_PatternSearch.Text.Trim().ToLower();
           for (int i = 0; i < this.KeyValueTableNams.Count; i++)
           {
               if (!KeyValueTableNams[i].ToLower().Contains(strPattern))
               {
                   KeyValueTableNams.RemoveAt(i);
                   i--;
               }

           }


           for (int i = 0; i < this.NormalTableNames.Count; i++)
           {
               if (!NormalTableNames[i].ToLower().Contains(strPattern))
               {
                   NormalTableNames.RemoveAt(i);
                   i--;
               }

           }
               

            
           ShowKeyValueTableNames();
           ShowNormalTableNames();
       }
       void AddTableName(String strTableName)
       {

           List<String> lst = dbInvoker.GetTableColumnNames(strTableName);
           if (lst.Count > 0)
           {
               if (lst.Count == 1)
               {
                   NormalTableNames.Add(strTableName);

               } 
               else if (lst.Count > 2)
               {
                   NormalTableNames.Add(strTableName);
               }
               else
               {
                   if (lst[0].ToLower() == "name" && lst[1].ToLower() == "val")
                       KeyValueTableNams.Add(strTableName);
                   else
                       NormalTableNames.Add(strTableName);
               }
           }
       }
       void ShowTableNames()
       {
           FetchClassifiedTableNamesFromDB();
            ShowKeyValueTableNames();
            ShowNormalTableNames();
       }

       private void ShowNormalTableNames()
       {
           ltb_NormalTableNames.Clear();
           
           if (NormalTableNames.Count > 0)
           {
               for (int i = 0; i < NormalTableNames.Count; i++)
               {
                   ListBoxItem_TableName btn = new ListBoxItem_TableName();
                   btn.PressColor = pnl_NormalTableNames.HeadColor;
                   btn.Text = NormalTableNames[i];
                   btn.Radius = 6;
                   btn.OtherObj = ltb_KeyValueTableNames ;
                    btn.ExternalCall +=  UniqueSelectedTableName;
                   if (btn.Width < ltb_NormalTableNames.UIAdapter.Width)
                       btn.Width = ltb_NormalTableNames.UIAdapter.Width;
                     
                   ltb_NormalTableNames.AddItem(btn);
                   ltb_NormalTableNames.Items[ltb_NormalTableNames.Items.Count - 1].DoubleClick += btn_TableName_Click;
               }
               
               
           }
       }

       private void UniqueSelectedTableName(object sender, EventArgs eventArgs)
       {
           MobileListBox c = (sender as ListBoxItem_TableName).OtherObj as MobileListBox;
           for (int i = 0; i < c.Items.Count; i++)
           {
               (c.Items[i] as ListBoxItem_TableName).LostFocusAppearance();
           }
       }

       void btn_TableName_Click(object sender, EventArgs e)
       {
           

           dgv_PreviewTable.DataSource = dbInvoker.ProvideTable(String.Format("select * from {0}", (sender as Control).Text), DBOperator.OPERATOR_QUERY_TABLE);
           FramePanel_Content.GetEditPanel(1);
           FramePannel_Header.GetEditPanel(1);
           CurrentTableName = (sender as Control).Text;
           lnk_ShowTableContent_Full.Text = CurrentTableName;
           lnk_PreviewTableContent.Text = CurrentTableName;
           dataGridRealTime.dataBindingPropertyEditorDialog = dataBindingPropertyEditorDialog;
           dataGridRealTime.Connect(ref dgv_PreviewTable,ref dbInvoker, CurrentTableName);

           if (dataBindingPropertyEditorDialog != null)
           {
               dataBindingPropertyEditorDialog.txb_TableName.Text = CurrentTableName;
           }
       }

       private void ShowKeyValueTableNames()
       {
           ltb_KeyValueTableNames.Clear();
           this.ltb_KeyValueTableNames.HBar.NormalThumbColor = pnl_KeyValueTableNames.HeadColor;
           this.ltb_KeyValueTableNames.HBar.PressThumbColor = Color.Firebrick;
           this.ltb_KeyValueTableNames.VBar.NormalThumbColor = pnl_KeyValueTableNames.HeadColor;
           this.ltb_KeyValueTableNames.VBar.PressThumbColor = Color.Firebrick;

           if (KeyValueTableNams.Count > 0)
           {
               
               for (int i = 0; i < KeyValueTableNams.Count; i++)
                   {
                       ListBoxItem_TableName btn = new ListBoxItem_TableName();
                       btn.PressColor = pnl_KeyValueTableNames.HeadColor;
                       btn.Text = KeyValueTableNams[i];
                       btn.Radius = 6;
                       if (btn.Width < ltb_KeyValueTableNames.UIAdapter.Width)
                           btn.Width = ltb_KeyValueTableNames.UIAdapter.Width;
                      
                       btn.OtherObj = ltb_NormalTableNames;
                       btn.ExternalCall +=  UniqueSelectedTableName;
                       ltb_KeyValueTableNames.AddItem(btn);
                       ltb_KeyValueTableNames.Items[ltb_KeyValueTableNames.Items.Count - 1].DoubleClick += btn_TableName_Click; 
                   }

 
           }
       }
       void InferKeyValueTable()
       {
           
           AllTableNames = dbInvoker.AllTableNames;
           for (int i = 0; i < AllTableNames.Count; i++)
           {

               AddTableName(AllTableNames[i]);
           }
       }
       private void TableProvidePart_Load(object sender, EventArgs e)
       {

       }

   }
}
