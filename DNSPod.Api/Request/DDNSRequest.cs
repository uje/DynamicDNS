using DNSPod.Api.Core;
using DNSPod.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Request {

    /// <summary>
    /// 动态域名创建请求
    /// </summary>
    public class DDNSRequest : RequestBase<RecordResponse> {

        /// <summary>
        /// 域名ID, 必选
        /// </summary>
        [Parameter("domain_id")]
        public string DomainId { get; set; }

        /// <summary>
        /// 记录ID，必选
        /// </summary>
        [Parameter("record_id")]
        public string RecordId { get; set; }

        /// <summary>
        /// 记录线路，通过API记录线路获得，中文，比如：默认，必选
        /// </summary>
        [Parameter("record_line")]
        public string RecordLine { get; set; }

        /// <summary>
        /// 主机记录，如 www
        /// </summary>
        [Parameter("sub_domain")]
        public string SubDomain { get; set; }

        /// <summary>
        /// IP地址，例如：6.6.6.6，可选
        /// </summary>
        [Parameter("value")]
        public string Value { get; set; }

        protected override string Url {
            get { return "https://dnsapi.cn/Record.Ddns"; }
        }
    }
}
