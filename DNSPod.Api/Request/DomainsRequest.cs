using DNSPod.Api.Core;
using DNSPod.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Request {

    /// <summary>
    /// 域名列表获取请求
    /// </summary>
    public class DomainsRequest : RequestBase<DomainsResponse> {
        protected override string Url {
            get { return "https://dnsapi.cn/Domain.List"; }
        }
    }
}
