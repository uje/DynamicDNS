using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Content {

    /// <summary>
    /// 域名相关信息
    /// </summary>
    public class Domain {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Grade { get; set; }

        [JsonProperty("grade_title")]
        public string GradeTitle { get; set; }

        public string Status { get; set; }

        [JsonProperty("ext_status")]
        public string ExtStatus { get; set; }

        public string Records { get; set; }

        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("is_mark")]
        public string IsMark { get; set; }

        public string Remark { get; set; }

        [JsonProperty("is_vip")]
        public string IsVip { get; set; }

        [JsonProperty("searchengine_push")]
        public string SearchEnginePush { get; set; }

        public string Beian { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string TTL { get; set; }

        public string Owner { get; set; }
    }
}
