using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace YHPT.Management.WebUI
{
    public class CSVFile
    {
        private const string FolderName = "FileUpload";
        public static void Export(DataTable dt, string fileName)
        {
            HttpContext.Current.Response.Clear();

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderName);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string fullfilePath = Path.Combine(path, fileName + ".xls");

            MB.WinEIDrive.Export.ExportXls export = new MB.WinEIDrive.Export.ExportXls(fullfilePath, false);
            export.DataSource = dt;
            export.Commit();

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpContext.Current.Server.UrlEncode(fileName) + ".xls");
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //HttpContext.Current.Response.Write(sw);
            HttpContext.Current.Response.TransmitFile(fullfilePath);
            HttpContext.Current.Response.End();

        }

        public static void Save(DataTable dt, string fileName)
        {
            string newline = "\r\n";
            StringBuilder sb = new StringBuilder();
            int iColCount = dt.Columns.Count;
            for (int i = 0; i < iColCount; i++)
            {
                sb.Append("\"" + dt.Columns[i] + "\"");
                if (i < iColCount - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append(newline);
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                        sb.Append("\"" + dr[i].ToString() + "\"");
                    else
                        sb.Append("\"\"");
                    if (i < iColCount - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append(newline);
            }


            FileStream file = new FileStream(fileName, FileMode.Create);
            StreamWriter sw3 = new StreamWriter(file, System.Text.Encoding.GetEncoding("gb2312"));
            sw3.Write(sb.ToString());
            sw3.Close();

            file.Close();
        }

        public static DataTable Import(HttpRequestBase request)
        {
            DataTable inputdt = null;

            foreach (string upload in request.Files)
            {
                var fileName = request.Files[upload].FileName.ToLower();
                if (!Extentions.HasFile(request.Files[upload]) || (fileName.EndsWith(".xls") == false && fileName.EndsWith(".xlsx") == false))
                    continue;

                string miniType = request.Files[upload].ContentType;
                Stream fileStream = request.Files[upload].InputStream;
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderName);
                string filename = DateTime.Now.ToString("HHmmssfff") + Path.GetFileName(request.Files[upload].FileName);
                string pathCombine = Path.Combine(path, filename);
                request.Files[upload].SaveAs(pathCombine);

                inputdt = ExcelToDataTable(null, true, pathCombine);
            }
            return inputdt;
        }

        public static DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn, string fileName)
        {
            NPOI.SS.UserModel.ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            IWorkbook workbook = null;
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //获取列数，到第一个空的cell为止

                    var emptyCellIndexs = firstRow.Cells.Where(n => string.IsNullOrEmpty(n.ToString())).Select(n => n.ColumnIndex).ToList();
                    int columnCount = firstRow.LastCellNum;
                    if (emptyCellIndexs.Any())
                    {
                        columnCount = emptyCellIndexs.Min();
                    }
                    //int cellCount = firstRow.LastCellNum; 一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < columnCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.ToString();
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        List<int> indexList = new List<int>();
                        if (row == null)
                        {
                            rowCount = i - 1;
                            break;
                        }
                        else
                        {
                            for (var o = 0; o < columnCount; o++)
                            {
                                if (row.GetCell(o) == null || row.GetCell(o).CellType == CellType.Blank)
                                {
                                    indexList.Add(o);
                                }
                            }
                            //检查行里所有的cell是否都是空，录入的数据到第一个全为空的行为止
                            if (indexList.Count == columnCount)
                            {
                                rowCount = row.RowNum - 1;
                                break;
                            }
                        }
                    }
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < columnCount; ++j)
                        {
                            var cell = row.GetCell(j);
                            if (cell != null) //同理，没有数据的单元格都默认是null
                            {
                                //从cell中获取value给datatable的cell赋值
                                dataRow[j] = GetValue(cell.CellType, cell, workbook);
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception: " + ex.Message);
            }
        }


        private static object GetValue(CellType type, ICell cell, IWorkbook workbook)
        {
            var obj = new object();
            switch (type)
            {
                case CellType.Blank: //空数据类型处理
                    obj = "";
                    break;
                case CellType.String: //字符串类型
                    obj = cell.StringCellValue;
                    break;
                case CellType.Numeric: //数字类型                                   
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        obj = cell.DateCellValue;
                    }
                    else
                    {
                        obj = cell.NumericCellValue;
                    }
                    break;
                case CellType.Formula:
                    IFormulaEvaluator e = null;
                    if (workbook.GetType() == typeof(HSSFWorkbook))
                    {
                        e = new HSSFFormulaEvaluator(workbook);
                    }
                    else
                    {
                        e = new XSSFFormulaEvaluator(workbook);
                    }

                    var type1 = e.EvaluateFormulaCell(cell);
                    obj = GetValue(type1, cell, workbook);
                    break;
                default:
                    obj = "";
                    break;
            }

            return obj;
        }
    }

    public static class Extentions
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
    }
}