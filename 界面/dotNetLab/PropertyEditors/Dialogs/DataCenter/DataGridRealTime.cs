using System;
using System.Collections.Generic;
 
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.ComponentModel;

namespace dotNetLab.Widgets
{
   public class DataGridRealTime
    {
        DataGridView dgv;
        DBEngineInvoker dbInvoker;
        String cellTempValue = null;
        DataGridViewCellEventArgs CurrentCellIndex = null;
        String strTableName;
        TextBox txb;
        ContextMenuStrip cts;
        private int nIndexRowSelected = -1;
       public DataTable dt;
        private List<String> lst_TempRowData = null;
       public DataBindingPropertyEditorDialog dataBindingPropertyEditorDialog;
       System.ComponentModel.Container components; 
        public DataGridRealTime(ref DataGridView dgv 
            )
        {
            this.dgv = dgv;
            Control ctrl;
            components = new System.ComponentModel.Container();
            lst_TempRowData = new List<string>();

            cts = new ContextMenuStrip(components);
            dgv.ContextMenuStrip = cts;
           
          //  cts.Items.Add("新增行");
            cts.Items.Add("删除行");
            cts.Items.Add("选取该字段");
            cts.Items.Add("接入条件");
            cts.Items[0].Click += DeleteRow_ContextMenuStrip_Click;
           // cts.Items[0].Click += NewRow_ContextMenuStrip_Click;
            cts.Items[2].Click += ConnRequire_ContextMenuStrip_Click;
            cts.Items[1].Click += SelectField_ContextMenuStrip_Click;
            dgv.ReadOnly = true;
            txb = new TextBox();
            dgv.Controls.Add(txb);
            txb.Visible = false;
            txb.KeyUp += new KeyEventHandler(txb_KeyUp);
            
            dgv.CellDoubleClick += new DataGridViewCellEventHandler(dgv_CellDoubleClick);
            dgv.CellClick += new DataGridViewCellEventHandler(dgv_CellClick);
            dgv.RowHeaderMouseClick += DgvOnRowHeaderMouseClick;
             
        }

        private void DgvOnRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lst_TempRowData.Clear();
            CurrentCellIndex = new DataGridViewCellEventArgs(-1, e.RowIndex);
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                lst_TempRowData.Add(dgv.Rows[e.RowIndex].Cells[i].Value.ToString());
            }
        }



        void NewRow_ContextMenuStrip_Click(Object sender, EventArgs e)
        {

            NewRecord(CurrentCellIndex);

        }
    
         void DeleteRow_ContextMenuStrip_Click(Object sender,EventArgs e)
        {
           
            RemoveRecord(CurrentCellIndex);
        
         }
        void SelectField_ContextMenuStrip_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedCells.Count > 1)
            {
                dotNetLab.Tipper.Tip_Info_Error("用作接入数据中的字段不能多于一");
                return;
            }
            int nIndex = dgv.SelectedCells[0].ColumnIndex;
            List<String> lst_ColumnNames = dbInvoker.GetTableColumnNames(strTableName);
            if(dataBindingPropertyEditorDialog != null)
               dataBindingPropertyEditorDialog .txb_DBField.Text =lst_ColumnNames[nIndex];
        }

        void ConnRequire_ContextMenuStrip_Click(object sender, EventArgs e)
        {
             

            String[] newColunNames = new String[dgv.SelectedCells.Count];
            int[] ColumnIndexes = new int [dgv.SelectedCells.Count]; 
            for (int i = 0; i < dgv.SelectedCells.Count; i++)
            {
                newColunNames[i] = dgv.SelectedCells[i].Value.ToString();
                ColumnIndexes[i] = dgv.SelectedCells[i].ColumnIndex;
            }


            List<String> lst_ColumnNames = dbInvoker.GetTableColumnNames(strTableName);
            List<bool> lst_ColumnTypes = dbInvoker.GetColumnTypes(strTableName);


            StringBuilder sb = new StringBuilder();
            StringBuilder sb_Vice = new StringBuilder();

            for (int i = 0; i < newColunNames.Length; i++)
            {
                       

                      sb.Append(lst_ColumnNames[ColumnIndexes[i]]);
                      if (lst_ColumnTypes[ColumnIndexes[i]])
                    {
                        sb.AppendFormat("='{0}' and ", newColunNames[i]);
                    }
                    else
                    {
                        sb.AppendFormat("={0}  and ", newColunNames[i]);
                    }

            }
            sb_Vice.Clear();

            sb_Vice.Append(sb.ToString().Trim());
            sb_Vice.Remove(sb_Vice.Length - 3, 3);
            Clipboard.Clear();
            Clipboard.SetText(sb_Vice.ToString());
           // dotNetLab.Tipper.Tip_Info_Error(sb_Vice.ToString());
            if (dataBindingPropertyEditorDialog != null)
            {
                
                dataBindingPropertyEditorDialog.txb_Filter.Text = sb_Vice.ToString();
            }
            else
                dotNetLab.Tipper.Tip_Info_Error("Damn");
            sb.Clear();
            sb_Vice.Clear();
            dgv.DataSource = dbInvoker.ProvideTable(String.Format("select * from {0}", strTableName), DBOperator.OPERATOR_QUERY_TABLE);
            this.dt = dgv.DataSource as DataTable;
            
            
        }

        void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txb.Visible == true && e != CurrentCellIndex)
            {
                dgv.Rows[CurrentCellIndex.RowIndex].Cells[CurrentCellIndex.ColumnIndex].Value = txb.Text.Trim();
                txb.Visible = false;
                StoreDataIntoDB(CurrentCellIndex);
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
              
                dgv.Rows[0].Cells[0].Selected = false;
            }
            DataTable dt = dgv.DataSource as DataTable;
            if (e.RowIndex+1 > dt.Rows.Count)
            {
                
                dgv.ReadOnly = false;
                int n = -1;
                   bool b = int.TryParse( dbInvoker.UniqueResult(String.Format("select count(*) from {0}",strTableName) ),out n);
                 
                if (b && e.RowIndex + 1 - n > 1 )
                {

                    CurrentCellIndex = new DataGridViewCellEventArgs(-1, e.RowIndex-1);
                        NewRecord(CurrentCellIndex);
                     
                }
            }
            else
            {
                dgv.ReadOnly = true;
            }
        }


        void txb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                dgv.Rows[CurrentCellIndex.RowIndex].Cells[CurrentCellIndex.ColumnIndex].Value = txb.Text.Trim();
                txb.Visible = false;
                StoreDataIntoDB(CurrentCellIndex);
            }
        }
        void GetCellLocation()
        {
             int cellLeft=0;
              
            for (int i = this.dgv.FirstDisplayedScrollingColumnIndex; i < CurrentCellIndex.ColumnIndex; i++)
            {
                 cellLeft= cellLeft + this.dgv.Columns[i].Width;
 
            }

            cellLeft -= dgv.FirstDisplayedScrollingColumnHiddenWidth;
            int cellTop = 0;
            for (int j=dgv.FirstDisplayedScrollingRowIndex;j<CurrentCellIndex.RowIndex;j++)
            {
                cellTop=cellTop+this.dgv.Rows[j].Height;
            }
             
            //this.txb.Left = this.dgv.Left + this.dgv.RowHeadersWidth + cellLeft;
            //this.txb.Top = this.dgv.Top + this.dataGridView1.ColumnHeadersHeight+cellTop;
            this.txb.Left = this.dgv.RowHeadersWidth + cellLeft;
            // this.txb.Top = this.dgv.Top + this.dgv.ColumnHeadersHeight+cellTop;
            this.txb.Top = this.dgv.ColumnHeadersHeight + cellTop;//- dgv.VerticalScrollingOffset;
        
        }
        void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable dt = dgv.DataSource as DataTable;
                if (e.RowIndex + 1 > dt.Rows.Count)
                    return;
                if (txb.Visible == true)
                {
                    dgv.Rows[CurrentCellIndex.RowIndex].Cells[CurrentCellIndex.ColumnIndex].Value = txb.Text.Trim();
                    txb.Visible = false;
                    StoreDataIntoDB(CurrentCellIndex);
                }
                CurrentCellIndex = e;

                txb.Text = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                cellTempValue = txb.Text;
                GetCellLocation();
                txb.Size = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Size;
                txb.Visible = true;

            }
            catch (System.Exception ex)
            {
            	
            }
            
        }
        public void Connect(ref DataGridView dgv,ref DBEngineInvoker dbInvoker, String  strTableName)
        {
             
             this.strTableName = strTableName;
             this.dbInvoker = dbInvoker;
             this.dt = dgv.DataSource as DataTable;
           
        }
        private void StoreDataIntoDB( DataGridViewCellEventArgs e)
        {
            try
            {
                if (strTableName == null)
                    return;

                String[] newColunNames = new String[dgv.ColumnCount];
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    newColunNames[i] = dgv.Rows[e.RowIndex].Cells[i].Value.ToString();
                }
                List<String> lst_ColumnNames = dbInvoker.GetTableColumnNames(strTableName);
                List<bool> lst_ColumnTypes = dbInvoker.GetColumnTypes(strTableName);


                StringBuilder sb = new StringBuilder();
                StringBuilder sb_Vice = new StringBuilder();
                if (lst_ColumnTypes[e.ColumnIndex])
                    sb.AppendFormat("{0}='{1}' where ", lst_ColumnNames[e.ColumnIndex],

                           newColunNames[e.ColumnIndex]
                        );
                else
                    sb.AppendFormat("{0}={1} where ", lst_ColumnNames[e.ColumnIndex],
                        newColunNames[e.ColumnIndex]
                       );
                for (int i = 0; i < lst_ColumnNames.Count; i++)
                {
                    if (i != e.ColumnIndex)
                    {
                        sb.Append(lst_ColumnNames[i]);
                        if (lst_ColumnTypes[i])
                        {
                            sb.AppendFormat("='{0}' and ", newColunNames[i]);
                        }
                        else
                        {
                            sb.AppendFormat("={0} and ", newColunNames[i]);
                        }
                    }
                    else
                    {
                        sb.Append(lst_ColumnNames[i]);
                        if (lst_ColumnTypes[i])
                        {
                            sb.AppendFormat("='{0}' and ", cellTempValue);
                        }
                        else
                        {
                            sb.AppendFormat("={0} and ", cellTempValue);
                        }
                    }


                }
                sb_Vice.Clear();

                sb_Vice.Append(sb.ToString().Trim());
                sb_Vice.Remove(sb_Vice.Length - 3, 3);
                sb.Clear();
                dbInvoker.Update(strTableName, sb_Vice.ToString());
                sb_Vice.Clear();
                dgv.DataSource = dbInvoker.ProvideTable(String.Format("select * from {0}", strTableName), DBOperator.OPERATOR_QUERY_TABLE);
                this.dt = dgv.DataSource as DataTable;
            }
            catch (System.Exception ex)
            {
            	
            }
          
        }

        void RemoveRecord(DataGridViewCellEventArgs e)
        {
            try
            {
                if (strTableName == null)
                    return;

                String[] newRowValues = new String[dgv.ColumnCount];
                //  DataTable dt_Current_No_Modify =  dgv.DataSource as DataTable;

                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    newRowValues[i] = lst_TempRowData[i];
                }


                List<String> lst_ColumnNames = dbInvoker.GetTableColumnNames(strTableName);
                List<bool> lst_ColumnTypes = dbInvoker.GetColumnTypes(strTableName);


                StringBuilder sb = new StringBuilder();
                StringBuilder sb_Vice = new StringBuilder();

                for (int i = 0; i < lst_ColumnNames.Count; i++)
                {

                    sb.Append(lst_ColumnNames[i]);
                    if (lst_ColumnTypes[i])
                    {
                         
                        sb.AppendFormat("='{0}' and ", newRowValues[i]);
                    }
                    else
                    {
                        sb.AppendFormat("={0} and ", newRowValues[i]);
                    }
                }
                sb_Vice.Clear();
                sb_Vice.Append(sb.ToString().Trim());
                sb_Vice.Remove(sb_Vice.Length - 3, 3);
                sb.Clear();
                dbInvoker.RemoveRecord(strTableName, sb_Vice.ToString());
                sb_Vice.Clear();
                dgv.DataSource = dbInvoker.ProvideTable(String.Format("select * from {0}", strTableName), DBOperator.OPERATOR_QUERY_TABLE);
                this.dt = dgv.DataSource as DataTable;
            }
            catch (System.Exception ex)
            {
            	
            }
       
            
        }
        void NewRecord(DataGridViewCellEventArgs e)
        {
            String[] newColunNames = new String[dgv.ColumnCount];
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                newColunNames[i] = dgv.Rows[e.RowIndex].Cells[i].Value.ToString();
            }
           // List<String> lst_ColumnNames = dbInvoker.GetTableColumnNames(strTableName);
            List<bool> lst_ColumnTypes = dbInvoker.GetColumnTypes(strTableName);


            StringBuilder sb = new StringBuilder();
            StringBuilder sb_Vice = new StringBuilder();
             
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                 
                     
                    if (lst_ColumnTypes[i])
                    {
                        sb.AppendFormat("'{0}',", newColunNames[i]);
                    }
                    else
                    {
                        sb.AppendFormat("{0},", newColunNames[i]);
                    }
               


            }
            if (sb_Vice.Length > 0) ;
            sb_Vice.Clear();

            sb_Vice.Append(sb.ToString().Trim());
            sb_Vice.Remove(sb_Vice.Length - 1, 1);
            sb.Clear();
            dbInvoker.NewRecord(strTableName, sb_Vice.ToString());
            sb_Vice.Clear();
            dgv.DataSource = dbInvoker.ProvideTable(String.Format("select * from {0}", strTableName), DBOperator.OPERATOR_QUERY_TABLE);
            this.dt = dgv.DataSource as DataTable;
        }
       
    }
}
