using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace YHPT.Management.WebUI
{
    public static class FileNPOI
    {
        #region 导出数据为Excel
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fileName"></param>
        /// <param name="Dt"></param>
        public static void Export(HttpContext context, string fileName, DataTable Dt)
        {
            MemoryStream ms = RenderToExcel(Dt, fileName);
            RenderToBrowser(ms, context, fileName);
        }

        /// <summary>
        /// 从浏览器上下载
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="context"></param>
        /// <param name="fileName"></param>
        public static void RenderToBrowser(MemoryStream ms, HttpContext context, string fileName)
        {
            if (context.Request.Browser.Browser == "IE")
                fileName = HttpUtility.UrlEncode(fileName);
            context.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
            context.Response.BinaryWrite(ms.ToArray());
        }

        /// <summary>
        /// 生成excel文件流
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel(DataTable table, string fileName)
        {
            int pageSize = 100000;

            MemoryStream ms = new MemoryStream();
            using (table)
            {
                IWorkbook workbook = null;
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook();
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook();

                var pageCount = (int)Math.Ceiling(table.Rows.Count * 1.0 / pageSize);

                for (var p = 0; p < pageCount; p++)
                {

                    ISheet sheet = workbook.CreateSheet();
                    IRow headerRow = sheet.CreateRow(0);
                    foreach (DataColumn column in table.Columns)
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
                    int rowIndex = 1;

                    for (int i = p * pageSize; i < table.Rows.Count && i < (1 + p) * pageSize; i++)
                    {
                        var row = table.Rows[i];
                        IRow dataRow = sheet.CreateRow(rowIndex);

                        foreach (DataColumn column in table.Columns)
                        {
                            Type type = column.DataType;
                            if (type == typeof(int))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((int)row[column]);
                            }
                            else if (type == typeof(Int64))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((Int64)row[column]);
                            }
                            else if (type == typeof(Int16))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((Int16)row[column]);
                            }
                            else if (type == typeof(double))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((double)row[column]);
                            }
                            else if (type == typeof(decimal))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDouble(row[column]));
                            }
                            else if (type == typeof(float))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((float)row[column]);
                            }
                            else if (type == typeof(Single))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((Single)row[column]);
                            }
                            else if (type == typeof(bool))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((bool)row[column]);
                            }
                            else
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                            }
                        }
                        rowIndex++;
                    }

                }
                workbook.Write(ms);
                ms.Flush();
                //ms.Position = 0;
            }
            return ms;
        }

        /// <summary>
        /// 生成本地文件，并返回流
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] DataTableToExcel(DataTable table, string fileName)
        {
            int pageSize = 100000;
            byte[] bytes = null;
            using (table)
            {
                IWorkbook workbook = null;
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook();
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook();

                var pageCount = (int)Math.Ceiling(table.Rows.Count * 1.0 / pageSize);

                for (var p = 0; p < pageCount; p++)
                {

                    ISheet sheet = workbook.CreateSheet();
                    IRow headerRow = sheet.CreateRow(0);
                    // handling header.
                    foreach (DataColumn column in table.Columns)
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
                    int rowIndex = 1;

                    for (int i = p * pageSize; i < table.Rows.Count && i < (1 + p) * pageSize; i++)
                    {
                        var row = table.Rows[i];
                        IRow dataRow = sheet.CreateRow(rowIndex);

                        foreach (DataColumn column in table.Columns)
                        {
                            Type type = column.DataType;
                            if (type == typeof(int))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((int)row[column]);
                            }
                            else if (type == typeof(Int64))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((Int64)row[column]);
                            }
                            else if (type == typeof(Int16))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((Int16)row[column]);
                            }
                            else if (type == typeof(double))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((double)row[column]);
                            }
                            else if (type == typeof(decimal))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDouble(row[column]));
                            }
                            else if (type == typeof(float))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((float)row[column]);
                            }
                            else if (type == typeof(Single))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((Single)row[column]);
                            }
                            else if (type == typeof(bool))
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue((bool)row[column]);
                            }
                            else
                            {
                                dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                            }
                        }
                        rowIndex++;
                    }

                }
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var ext = Path.GetExtension(fileName);
                var name = "DFSDownLoad" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                path = Path.Combine(path, "DownLoadFiles", name);

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)) 
                {
                    workbook.Write(fs);
                }
                bytes = File.ReadAllBytes(path);
            }
            return bytes;
        }
        #endregion
    }
}
