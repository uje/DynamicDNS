using DNSPod.Api.Content;
using DNSPod.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Response {
    public class RecordResponse : ResponseBase {
        public Record Record { get; set; }
    }
}
