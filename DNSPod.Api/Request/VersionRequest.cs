using DNSPod.Api.Core;
using DNSPod.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Request {
    public class VersionRequest : RequestBase<VersionResponse> {
        protected override string Url {
            get { return "https://dnsapi.cn/Info.Version"; }
        }
    }
}
