using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using MB.Framework.Common.Enums;

namespace YHPT.Management.WebUI.Library.Controls
{
    public class JQueryDataTablesModelBinder : IModelBinder
    {
        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }
            _bindingContext = bindingContext;

            //Bind Model
            var dataTablesRequest = new JQueryDataTablesModel();
            dataTablesRequest.GridKey = GetA<int>(sEchoKey);
            if (dataTablesRequest.GridKey <= 0)
            {
                throw new InvalidOperationException("Expected the request to have a sEcho value greater than 0");
            }

            dataTablesRequest.PageSize = GetA<int>(iDisplayLengthKey);
            dataTablesRequest.StartIndex = GetA<int>(iDisplayStartKey);
            dataTablesRequest.PageIndex =
                (int) Math.Ceiling((dataTablesRequest.StartIndex)*1.0/dataTablesRequest.PageSize);
            dataTablesRequest.FieldNames = GetStringList(mDataProp_Key);
            var iSortingCols = GetANullableValue<int>(iSortingColsKey);

            ReadOnlyCollection<int> iSortCol_ = null;
            ReadOnlyCollection<string> sSortDir_ = null;
            if (iSortingCols.HasValue)
            {
                iSortCol_ = GetAList<int>(iSortCol_Key);
                sSortDir_ = GetStringList(sSortDir_Key);

                if (iSortingCols.Value != iSortCol_.Count)
                {
                    throw new InvalidOperationException(
                        string.Format(
                            "Amount of items contained in iSortCol_ {0} do not match the amount specified in iSortingCols which is {1}",
                            iSortCol_.Count, iSortingCols.Value));
                }

                if (iSortingCols.Value != sSortDir_.Count)
                {
                    throw new InvalidOperationException(
                        string.Format(
                            "Amount of items contained in sSortDir_ {0} do not match the amount specified in iSortingCols which is {1}",
                            sSortDir_.Count, iSortingCols.Value));
                }
                dataTablesRequest.SortField = dataTablesRequest.FieldNames[iSortCol_[0]];
                dataTablesRequest.Direction = sSortDir_[0] == "asc" ? SortDirection.Ascending : SortDirection.Descending;
            }
            return dataTablesRequest;
        }

        #endregion

        #region Private Variables (Request Keys)

        private const string iDisplayStartKey = "iDisplayStart";
        private const string iDisplayLengthKey = "iDisplayLength";
        private const string iColumnsKey = "iColumns";
        private const string sSearchKey = "sSearch";
        private const string bEscapeRegexKey = "bRegex";
        private const string bSortable_Key = "bSortable_";
        private const string bSearchable_Key = "bSearchable_";
        private const string sSearch_Key = "sSearch_";
        private const string bEscapeRegex_Key = "bRegex_";
        private const string iSortingColsKey = "iSortingCols";
        private const string iSortCol_Key = "iSortCol_";
        private const string sSortDir_Key = "sSortDir_";
        private const string sEchoKey = "sEcho";
        private const string mDataProp_Key = "mDataProp_";

        private ModelBindingContext _bindingContext;

        #endregion

        #region Methods

        /// <summary>
        ///     Retrieves an IList of strings from the ModelBindingContext based on the key provided.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private ReadOnlyCollection<string> GetStringList(string key)
        {
            var list = new List<string>();
            var hasMore = true;
            var i = 0;
            while (hasMore)
            {
                var newKey = (key + i);

                // No need to use a prefix since data tables will not prefix the request names        
                var valueResult = _bindingContext.ValueProvider.GetValue(newKey);

                if (valueResult == null)
                {
                    // If valueResult is still null then we know the value is not in the ModelBindingContext
                    // cease execution of this forloop
                    hasMore = false;
                    continue;
                }

                list.Add((string) valueResult.ConvertTo(typeof (string)));

                i++;
            }

            return list.AsReadOnly();
        }

        private ReadOnlyCollection<T> GetAList<T>(string key) where T : struct
        {
            var list = new List<T>();
            var hasMore = true;
            var i = 0;
            while (hasMore)
            {
                var newKey = (key + i);

                var valueResult = _bindingContext.ValueProvider.GetValue(newKey);

                if (valueResult == null)
                {
                    // If valueResult is still null then we know the value is not in the ModelBindingContext
                    // cease execution of this forloop
                    hasMore = false;
                    continue;
                }

                list.Add((T) valueResult.ConvertTo(typeof (T)));
                i++;
            }

            return list.AsReadOnly();
        }

        /// <summary>
        ///     Retrieves a string from the ModelBindingContext based on the key provided.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetString(string key)
        {
            var valueResult = _bindingContext.ValueProvider.GetValue(key);

            if (valueResult == null)
            {
                return null;
            }

            return (string) valueResult.ConvertTo(typeof (string));
        }

        private T GetA<T>(string key) where T : struct
        {
            var valueResult = _bindingContext.ValueProvider.GetValue(key);

            if (valueResult == null)
            {
                return new T();
            }

            return (T) valueResult.ConvertTo(typeof (T));
        }

        private T? GetANullableValue<T>(string key) where T : struct
        {
            var valueResult = _bindingContext.ValueProvider.GetValue(key);

            if (valueResult == null)
            {
                return null;
            }

            return (T?) valueResult.ConvertTo(typeof (T));
        }

        #endregion
    }
}