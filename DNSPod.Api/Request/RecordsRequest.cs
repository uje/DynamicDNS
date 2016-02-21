using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNSPod.Api.Core;
using DNSPod.Api.Response;

namespace DNSPod.Api.Request {

    /// <summary>
    /// 记录列表获取请求
    /// </summary>
    public class RecordsRequest : RequestBase<RecordsResponse> {

        /// <summary>
        /// 域名ID, 必选
        /// </summary>
        [Parameter("domain_id")]
        public string DomainId { get; set; }

        protected override string Url {
            get { return "https://dnsapi.cn/Record.List"; }
        }
    }
}
