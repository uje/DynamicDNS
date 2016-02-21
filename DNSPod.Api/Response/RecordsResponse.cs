using DNSPod.Api.Content;
using DNSPod.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Response {

    /// <summary>
    /// 记录列表信息
    /// </summary>
    public class RecordsResponse : ResponseBase {
        public Domain Domain { get; set; }
        public Record[] Records { get; set; }
    }
}
