using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Common
{
    public class PortableOffice
    {
        /// <summary>
        /// 默认sheetName 为“sheet1”
        /// </summary>
        /// <param name="dataGridView1"></param>
        public static void ExportToExcel(DataGridView dataGridView1 )
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Excel(97-2003)|*.xls";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            InternalExecel(dataGridView1, fileDialog.FileName);
        }
        /// <summary>
        /// 默认sheetName 为“sheet1”
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// /// <param name="FileName">Execel文件名</param>
        public static void InternalExecel(DataGridView dataGridView1,string FileName)
        {
            //不允许dataGridView显示添加行，负责导出时会报最后一行未实例化错误
            dataGridView1.AllowUserToAddRows = false;
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("sheet1");
            IRow rowHead = sheet.CreateRow(0);

            //填写表头
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                rowHead.CreateCell(i, CellType.String).SetCellValue(dataGridView1.Columns[i].HeaderText.ToString());
            }
            //填写内容
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    row.CreateCell(j, CellType.String).SetCellValue(dataGridView1.Rows[i].Cells[j].Value.ToString());
                }
            }

            using (FileStream stream = File.OpenWrite(FileName))
            {
                workbook.Write(stream);
                stream.Close();
            }
            MessageBox.Show("导出数据成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GC.Collect();
        }
        /// <summary>
        /// 将Execel文件内容读取到DataTable
        /// </summary>
        /// 默认sheetName 为“sheet1”
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable ExcelSheetImportToDataTable(string filePath)
        {

            string sheetName = "sheet1";
            DataTable dt = new DataTable();
             

            if (Path.GetExtension(filePath).ToLower() == ".xls".ToLower())
            {//.xls

                HSSFWorkbook hssfworkbook = null;
                
                #region .xls文件处理:HSSFWorkbook
                try
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                          hssfworkbook = new HSSFWorkbook(file);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                ISheet sheet = hssfworkbook.GetSheet(sheetName);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);

                //一行最后一个方格的编号 即总的列数 
                for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
                {
                    //SET EVERY COLUMN NAME
                    HSSFCell cell = (HSSFCell)headerRow.GetCell(j);

                    dt.Columns.Add(cell.ToString());
                }

                while (rows.MoveNext())
                {
                    IRow row = (HSSFRow)rows.Current;
                    DataRow dr = dt.NewRow();

                    if (row.RowNum == 0) continue;//The firt row is title,no need import

                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        if (i >= dt.Columns.Count)//cell count>column count,then break //每条记录的单元格数量不能大于表格栏位数量 20140213
                        {
                            break;
                        }

                        ICell cell = row.GetCell(i);

                        if ((i == 0) && (string.IsNullOrEmpty(cell.ToString()) == true))//每行第一个cell为空,break
                        {
                            break;
                        }

                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }

                    dt.Rows.Add(dr);
                }
                #endregion
            }
            else
            {//.xlsx

                XSSFWorkbook hssfworkbook = null;
                #region .xlsx文件处理:XSSFWorkbook
                try
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {

                        hssfworkbook = new XSSFWorkbook(file);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                ISheet sheet = hssfworkbook.GetSheet(sheetName);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                XSSFRow headerRow = (XSSFRow)sheet.GetRow(0);



                //一行最后一个方格的编号 即总的列数 
                for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
                {
                    //SET EVERY COLUMN NAME
                    XSSFCell cell = (XSSFCell)headerRow.GetCell(j);

                    dt.Columns.Add(cell.ToString());

                }

                while (rows.MoveNext())
                {
                    IRow row = (XSSFRow)rows.Current;
                    DataRow dr = dt.NewRow();

                    if (row.RowNum == 0) continue;//The firt row is title,no need import

                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        if (i >= dt.Columns.Count)//cell count>column count,then break //每条记录的单元格数量不能大于表格栏位数量 20140213
                        {
                            break;
                        }

                        ICell cell = row.GetCell(i);

                        if ((i == 0) && (string.IsNullOrEmpty(cell.ToString()) == true))//每行第一个cell为空,break
                        {
                            break;
                        }

                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
                #endregion
            }
            return dt;
        }
    }
}
