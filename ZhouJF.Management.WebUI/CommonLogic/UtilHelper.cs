using MB.Framework.Common;
using MB.Framework.Common.Attrs;
using YHPT.Management.WebUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YHPT.Management.WebUI.CommonLogic
{
    public class UtilHelper
    {
        public static List<T> GetDuplicateElementInList<T>(List<T> list)
        {
            var set = new HashSet<T>();
            var result = new List<T>();
            list.ForEach(
            n =>
            {
                if (!set.Add(n))
                {
                    result.Add(n);
                }
            });
            return result;
        }


        /// <summary>
        /// 校验参数是否为多少位的int
        /// </summary>
        /// <returns></returns>
        public static bool CheckIntCount(string values, int count = 0)
        {
            string regStr = "";
            if (count == 0)
            {
                regStr = @"^[0-9]+$";
            }
            else
            {
                regStr = "^[0-9]{" + count + "}$";
            }
            Regex r = new Regex(regStr);
            if (!string.IsNullOrEmpty(values))
            {
                if (!r.Match(values).Success)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else return false;
        }
        /// <summary>
        /// 校验参数是否为最多为多少位小数的decimal
        /// </summary>
        /// <returns></returns>
        public static bool CheckDecimal(string values, int digitCount)
        {
            var str = "^\\d+(\\.\\d{1," + digitCount + "})?$";
            if (digitCount == 0)
            {
                str = "^\\d+$";
            }
            if (new Regex(str).Match(values).Success)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 校验导入的excel行的值是否有空值
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool CheckExcelRows(DataRow row, int columnTotalNum)
        {
            for (var i = 0; i < columnTotalNum; i++)
            {
                if (string.IsNullOrEmpty(row[i].ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取枚举描述和枚举整数值的Keyvaluepairs
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> GetPairsFromEnum<T>()
        {
            var result = new List<KeyValuePair<int, string>>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var key = Convert.ToInt32(Enum.Parse(typeof(T), item.ToString()));
                var value = GetEnumDescription<T>(item.ToString());
                KeyValuePair<int, string> pair = new KeyValuePair<int, string>(key, value);
                result.Add(pair);
            }
            return result;
        }

        /// <summary>
        /// 获取枚举描述和枚举名称的Keyvaluepairs
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GetNameWithDescPairsFromEnum<T>()
        {
            var result = new List<KeyValuePair<string, string>>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var key = item.ToString();
                var value = GetEnumDescription<T>(item.ToString());
                KeyValuePair<string, string> pair = new KeyValuePair<string, string>(key, value);
                result.Add(pair);
            }
            return result;
        }
        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type)
                           .Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                           .Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
        public static string GetEnumDescription<T>(int value)
        {
            var result = "";
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var key = Convert.ToInt32(Enum.Parse(typeof(T), item.ToString()));
                if (value == key)
                {
                    result = GetEnumDescription<T>(item.ToString());
                    break;
                }
            }
            return result;
        }
        
        /// <summary>
        /// 获取枚举整数和枚举名称的Keyvaluepairs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> GetEnumValueNamePairs<T>()
        {
            var result = new List<KeyValuePair<int, string>>();
            foreach (string name in Enum.GetNames(typeof(T)))
            {
                var value = (int)Enum.Parse(typeof(T), name);
                result.Add(new KeyValuePair<int, string>(value, name));
            }
            return result;
        }
        /// <summary>
        /// 获取枚举描述和枚举值的Dic
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Dictionary<string, int></returns>

        public static Dictionary<string, int> GetDicFromEnumDescWithValue<T>()
        {
            var result = new Dictionary<string, int>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var key = item.ToString();
                var value = GetEnumDescription<T>(item.ToString());
                result.Add(value, (int)item);
            }
            return result;
        }
        /// <summary>
        /// 获取枚举描述和枚举名称的Dic
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Dictionary<string, string></returns>
        public static Dictionary<string, string> GetDicFromEnumDescWithName<T>()
        {
            var result = new Dictionary<string, string>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var key = item.ToString();
                var value = GetEnumDescription<T>(item.ToString());
                result.Add(value, item.ToString());
            }
            return result;
        }

        public static object GetValueFromGridStage(string name, HttpRequestBase Request)
        {
            var gridStageStr = Request["gridStage"];
            var gridStage = JsonConvert.DeserializeObject<GridStage>(gridStageStr);
            var paramDic = gridStage.paramArray.ToDictionary(n => n.Name, n => n.Value);

            if (paramDic.ContainsKey(name) && paramDic[name] != null && !string.IsNullOrEmpty(paramDic[name].ToString()))
            {
                return paramDic[name];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据List里面实体的ExportColumn特性导出Excel
        /// </summary>
        /// <typeparam name="T">要导出的实体类型</typeparam>
        /// <param name="source">要导出的数据源</param>
        /// <param name="fileName">Excel名字</param>
        /// <param name="tableAction">生成DataTable后的特殊操作</param>
        public static void ExportExcel<T>(List<T> source, string fileName, Action<DataTable> tableAction = null) where T : class
        {
            var dataTable = new DataTable();
            var properties = typeof(T).GetProperties();
            var fileds = typeof(T).GetFields();
            //从attr中取出导出列的相关信息
            Dictionary<int, ColumnDefines> dic = new Dictionary<int, ColumnDefines>();
            foreach (var prop in properties)
            {
                var attrs = prop.GetCustomAttributes(true).ToList();
                var attr = attrs.Find(n => n.GetType() == typeof(ExportColumn));
                if (attr != null)
                {
                    var columnAtt = attr as ExportColumn;
                    dic.Add(columnAtt.ExportOrder, new ColumnDefines(columnAtt.NameInExcel, prop.Name, columnAtt.TypeInDT));
                }
            }
            foreach (var field in fileds)
            {
                var attrs = field.GetCustomAttributes(true).ToList();
                var attr = attrs.Find(n => n.GetType() == typeof(ExportColumn));
                if (attr != null)
                {
                    var columnAtt = attr as ExportColumn;
                    dic.Add(columnAtt.ExportOrder, new ColumnDefines(columnAtt.NameInExcel, field.Name, columnAtt.TypeInDT));
                }
            }
            // = (Dictionary<int, ColumnDefines>)
            dic = dic.OrderBy(n => n.Key).ToDictionary(n => n.Key, n => n.Value);
            //添加数据表列
            foreach (var obj in dic)
            {
                if (obj.Value.TypeInDT != null)
                {
                    dataTable.Columns.Add(obj.Value.ColumnNameInDT, obj.Value.TypeInDT);
                }
                else
                {
                    dataTable.Columns.Add(obj.Value.ColumnNameInDT);
                }
            }
            //赋值
            var dicProperty = typeof(T).GetProperties().ToList().ToDictionary(n => n.Name, n => n);
            var dicField = typeof(T).GetFields().ToList().ToDictionary(n => n.Name, n => n);
            source.ForEach(n =>
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (var obj in dic)
                {
                    if (dicProperty.ContainsKey(obj.Value.ColumnNameInDT))
                    {
                        dataRow[obj.Value.ColumnNameInDT] = dicProperty[obj.Value.ColumnNameInDT].GetValue(n, null);
                    }
                    else
                    {
                        dataRow[obj.Value.ColumnNameInDT] = dicField[obj.Value.ColumnNameInDT].GetValue(n);
                    }
                }
                dataTable.Rows.Add(dataRow);
            });

            //生成Excel的列名
            foreach (var obj in dic)
            {
                dataTable.Columns[obj.Value.ColumnNameInDT].ColumnName = obj.Value.ColumnNameInExcel;
            }

            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            if (System.Web.HttpContext.Current.Request.Browser.Browser != "Firefox")
            {
                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            }
            if (tableAction != null)
            {
                tableAction(dataTable);
            }

            FileNPOI.Export(System.Web.HttpContext.Current, fileName, dataTable);
        }

        /// <summary>
        /// 并行获取分页数据，并合并返回实体结果集，查询实体的PageSize为0，会根据结果总数自动确定合适的PageSize
        /// </summary>
        /// <typeparam name="T">返回的实体类型</typeparam>
        /// <typeparam name="G">查询参数dto</typeparam>
        /// <param name="queryParams">查询参数DTO</param>
        /// <param name="funcGetTotalCount">获取查询结果总数的方法</param>
        /// <param name="funcGetEntity">分页查询的方法</param>
        /// <returns>所有实体的list</returns>
        //public static List<T> GetObejctByPage<T, G>(G queryParams, Func<G, int> funcGetTotalCount, Func<G, ServiceResult<PagedList<T>>> funcGetEntity)
        //    where T : new()
        //    where G : PagedQueryParam, new()
        //{
        //    var datas = new List<T>();
        //    var total = funcGetTotalCount(queryParams);
        //    var pageSize = queryParams.PageSize;
        //    if (pageSize == 0)
        //    {
        //        if (total > 0 && total <= 100)
        //        {
        //            pageSize = 20;
        //        }
        //        else if (total > 100 && total <= 500)
        //        {
        //            pageSize = 100;
        //        }
        //        else if (total > 500 && total <= 1000)
        //        {
        //            pageSize = 200;
        //        }
        //        else if (total > 1000 && total <= 5000)
        //        {
        //            pageSize = 1000;
        //        }
        //        else if (total > 5000 && total <= 10000)
        //        {
        //            pageSize = 2000;
        //        }
        //        else if (total > 10001)
        //        {
        //            pageSize = 3000;
        //        }
        //    }
        //    queryParams.PageSize = pageSize;
        //    var list = new List<G>();
        //    if (total > 0)
        //    {
        //        if (total > pageSize)
        //        {
        //            var i = total % pageSize > 0 ? total / pageSize + 1 : total / pageSize;
        //            for (var m = 0; m < i; m++)
        //            {
        //                G dto = (G)queryParams.Clone();
        //                dto.PageIndex = m;
        //                list.Add(dto);
        //            }
        //            //Parallel.ForEach(list,
        //            list.ForEach(
        //                n =>
        //                {
        //                    var response = funcGetEntity(n);
        //                    if (response.IsSucess && response.DataContext != null)
        //                    {
        //                        datas.AddRange(response.DataContext.Data);
        //                    }
        //                    else
        //                    {
        //                        throw new Exception(response.ErrorMessage);
        //                    }
        //                });
        //        }
        //        else
        //        {
        //            datas.AddRange(funcGetEntity(queryParams).DataContext.Data);
        //        }
        //    }
        //    return datas;
        //}

        public static void ExportTxtFile(List<string> list)
        {
            StringBuilder sb = new StringBuilder();
            if (list.Any())
            {
                list.ForEach(
                    n =>
                    {
                        sb.Append(n);
                        sb.Append("\r\n");
                    });
            }
            var fileName = "Synonyms.txt";
            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            if (System.Web.HttpContext.Current.Request.Browser.Browser != "Firefox")
                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            System.Web.HttpContext.Current.Response.ContentType = "text/plain";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(sb);
            System.Web.HttpContext.Current.Response.Write(oStringWriter);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 获取中英文混排字符串的实际长度(字节数)
        /// </summary>
        /// <param name="str">要获取长度的字符串</param>
        /// <returns>字符串的实际长度值（字节数）</returns>
        public static int GetStringLength(string str)
        {
            if (str.Equals(string.Empty))
                return 0;
            int strlen = 0;
            ASCIIEncoding strData = new ASCIIEncoding();
            //将字符串转换为ASCII编码的字节数字
            byte[] strBytes = strData.GetBytes(str);
            for (int i = 0; i <= strBytes.Length - 1; i++)
            {
                if (strBytes[i] == 63)  //中文都将编码为ASCII编码63,即"?"号
                    strlen++;
                strlen++;
            }
            return strlen;
        }

        /// <summary>
        /// 隐藏银行卡号
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static string HiddenCardNo(string cardNo)
        {
            string replaceStr = "";
            if (string.IsNullOrEmpty(cardNo)) return cardNo;
            int cardLength = cardNo.Length;
            replaceStr = replaceStr.PadLeft(cardLength - 8, '*');

            return string.Format("{0}{1}{2}", cardNo.Substring(0, 4), replaceStr, cardNo.Substring(cardLength - 4));
        }
    }
}