using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicDNS.Api.Core {
    public interface ICacheClient {
        T GetCacheData<T>(GetCacheDataDelegate<T> getData, string key, TimeSpan expiry);
        void Remove(string key);

        void Clear(string startWidth);
    }

    public delegate T GetCacheDataDelegate<T>();
}