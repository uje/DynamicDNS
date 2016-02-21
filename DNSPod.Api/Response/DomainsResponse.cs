using DNSPod.Api.Content;
using DNSPod.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Response {

    /// <summary>
    /// 域名列表，Info没啥用，所以没实现
    /// </summary>
    public class DomainsResponse : ResponseBase {

        public Domain[] Domains { get; set; }
    }
}
