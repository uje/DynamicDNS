using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicDNS.Api.Helper {

    /// <summary>
    /// 转换处理类
    /// </summary>
    public static class ConvertHelper {

        /// <summary>
        /// 将当前时间转换为时间戳
        /// </summary>
        public static long Timestamp() {
            return Timestamp(DateTime.Now);
        }

        /// <summary>
        /// 将时间转换为时间戳
        /// </summary>
        public static long Timestamp(DateTime time) {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
