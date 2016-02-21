using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Content {

    /// <summary>
    /// 记录信息
    /// </summary>
    public class Record {

        public string Id{get;set;}
        public string Name { get; set; }
        public string Line { get; set; }
        public string Type { get; set; }
        public string TTL { get; set; }
        public string Value { get; set; }
        public string MX { get; set; }
        public string Enabeld { get; set; }
        public string Status { get; set; }

        [JsonProperty("monitor_status")]
        public string MonitorStatus { get; set; }
        public string Remark { get; set; }

        [JsonProperty("updated_on")]
        public DateTime UpdateOn { get; set; }
    }
}
