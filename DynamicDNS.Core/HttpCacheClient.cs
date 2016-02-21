using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace DynamicDNS.Api.Core {
    /// <summary>
    /// Http缓存客户端，此种缓存是缓存在Http服务端的，调用速度快，但是不适用缓存大批量数据，会影响服务器的运行。
    /// </summary>
    public sealed class HttpcacheClient : ICacheClient {
        string name;

        private HttpcacheClient(string name) {
            this.name = name;
        }

        private string GenerateKey(string key) {
            return string.Format("{0}|{1}", this.name, key);
        }
        /// <summary>
        /// 获取HttpcacheClient缓存客户端
        /// </summary>
        /// <param name="name">缓存组关键字</param>
        /// <returns></returns>
        public static HttpcacheClient GetInstance(string name) {
            return new HttpcacheClient(name);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存的类型</typeparam>
        /// <param name="getData">当无缓存或缓存超时，则调用此方法获取数据</param>
        /// <param name="key">缓存关键字</param>
        /// <param name="expiry">超时时间</param>
        /// <returns></returns>
        public T GetCacheData<T>(GetCacheDataDelegate<T> getData, string key, TimeSpan expiry) {
            key = GenerateKey(key);

            T data;
            object cacheData = HttpRuntime.Cache[key];
            if (expiry.TotalMilliseconds == 0 || cacheData == null || !(cacheData is T)) {
                data = getData();
                if (data != null) {
                    HttpRuntime.Cache.Add(key, data, null, Cache.NoAbsoluteExpiration, expiry, CacheItemPriority.Normal, null);
                }
            }
            else {
                data = (T)cacheData;
            }

            return data;
        }


        /// <summary>
        /// 根据键值清除缓存
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key) {
            HttpRuntime.Cache.Remove(GenerateKey(key));
        }

        /// <summary>
        /// 清除指定前缀的缓存
        /// </summary>
        public void Clear(string startWith = "") {
            foreach (DictionaryEntry entry in HttpRuntime.Cache) {
                string cacheKey = (string)entry.Key;
                if (cacheKey.StartsWith(GenerateKey(startWith)))
                    HttpRuntime.Cache.Remove(cacheKey);
            }
        }
    }
}
