using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Content {

    /// <summary>
    /// 返回的状态
    /// </summary>
    public class Status {
        public int Code { get; set; }
        public string Message { get; set; }

        [JsonProperty("create_at")]
        public DateTime CreateAt { get; set; }
    }
}
