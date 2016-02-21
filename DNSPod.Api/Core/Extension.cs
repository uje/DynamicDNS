using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Core {
    public static class Extension {

        /// <summary>
        /// 检测文本是否为null或者空白符
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string input) {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// 如果当前文本为空则检测参数直到找到不为空的一项并返回
        /// </summary>
        public static string Or(this string input, params object[] others) {

            if (!string.IsNullOrWhiteSpace(input)) return input;

            foreach (var value in others) {
                if (value != null && !value.ToString().IsNullOrWhiteSpace()) return value.ToString();
            }

            return string.Empty;
        }
    }
}
