using DNSPod.Api.Core;
using DNSPod.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Request {

    /// <summary>
    /// 记录创建请求
    /// </summary>
    public class RecordCreateRequest : RequestBase<RecordResponse> {

        /// <summary>
        /// 域名ID, 必选
        /// </summary>
        [Parameter("domain_id")]
        public string DomainId { get; set; }

        /// <summary>
        /// 主机记录, 如 www, 默认@，可选
        /// </summary>
        [Parameter("sub_domain")]
        public string SubDomain { get; set; }

        /// <summary>
        /// 记录类型，通过API记录类型获得，大写英文，比如：A, 必选
        /// </summary>
        [Parameter("record_type")]
        public string RecordType { get; set; }

        /// <summary>
        /// 记录线路，通过API记录线路获得，中文，比如：默认, 必选
        /// </summary>
        [Parameter("record_line")]
        public string RecordLine { get; set; }

        /// <summary>
        /// 记录值, 如 IP:200.200.200.200, CNAME: cname.dnspod.com., MX: mail.dnspod.com., 必选
        /// </summary>
        [Parameter("value")]
        public string Value { get; set; }

        /// <summary>
        /// {1-20} MX优先级, 当记录类型是 MX 时有效，范围1-20, MX记录必选
        /// </summary>
        [Parameter("mx")]
        public string MX { get; set; }

        /// <summary>
        /// {1-604800} TTL，范围1-604800，不同等级域名最小值不同, 可选
        /// </summary>
        [Parameter("ttl")]
        public string TTL { get; set; }

        protected override string Url {
            get { return "https://dnsapi.cn/Record.Create"; }
        }
    }
}
