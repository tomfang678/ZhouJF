using MB.Framework.Common.Caching;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using YHPT.Management.WebUI.Models;

namespace YHPT.Management.WebUI
{
    public class ProcessBarHandler
    {
        private readonly static Lazy<ProcessBarHandler> _instance = new Lazy<ProcessBarHandler>(() =>
        {
            return new ProcessBarHandler();
        });

        private ProcessBarHandler()
        {
        }

        public static ProcessBarHandler Instance
        {
            get
            {
                return _instance.Value;
            }
        }


        public void SendProcessMessage(string processBarKey, string message)
        {
            //var data = CacheManager.Instance.Get<List<string>>(processBarKey) ?? new List<string>();
            //data.Add(message);
            //CacheManager.Instance.Add(processBarKey, data, TimeSpan.FromMinutes(10));
        }

        public void SendFinishMessage(string processBarKey, string message)
        {
            //var data = CacheManager.Instance.Get<List<string>>(processBarKey) ?? new List<string>();
            //data.Add(":finish" + message);
            //CacheManager.Instance.Add(processBarKey, data, TimeSpan.FromMinutes(10));
        }

        public List<string> GetData(string processBarKey)
        {
            //var data = CacheManager.Instance.Get<List<string>>(processBarKey) ?? new List<string>();
            //CacheManager.Instance.Remove(processBarKey);
            //return data;
            return null;
        }
        /// <summary>
        /// 添加未导入成功的商品款编码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prodClsCode"></param>
        public void AddUnFinishProdClsCode(string key, string prodClsCode)
        {
            //var data = CacheManager.Instance.Get<List<string>>(key) ?? new List<string>();
            //data.Add(prodClsCode);
            //CacheManager.Instance.Add(key, data, TimeSpan.FromMinutes(300));
        }
        /// <summary>
        /// 获取未导入成功的商品款编码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetUnFinishProdClsCode(string key)
        {
            //var data = CacheManager.Instance.Get<List<string>>(key) ?? new List<string>();
            //CacheManager.Instance.Remove(key);
            //return data;
            return null;
        }

        public void AddUnFinishErrorModel(string key, SyncErrorModel model)
        {
            //var data = CacheManager.Instance.Get<List<SyncErrorModel>>(key) ?? new List<SyncErrorModel>();
            //data.Add(model);
            //CacheManager.Instance.Add(key, data, TimeSpan.FromMinutes(300));
        }

        public List<SyncErrorModel> GetUnFinishErrorModels(string key,bool removeKey)
        {
            //var data = CacheManager.Instance.Get<List<SyncErrorModel>>(key) ?? new List<SyncErrorModel>();
            //if (removeKey)
            //    CacheManager.Instance.Remove(key);
            //return data;
            return null;
        }
    }
}