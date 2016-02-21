using DNSPod.Api.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Core {

    public interface IResponse {
        Status Status { get; set; }
    }
}
