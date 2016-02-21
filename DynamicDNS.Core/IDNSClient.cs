using DNSPod.Api.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDNS.Core {
    public interface IDNSClient {

        Domain GetDomain(string domainName);

        Record GetRecord(string domainId, string subDomain);

        Record CreateRecord(string domainId, string recordId, string value);

        void DDNS(string domainId, string subDomain, string recordId);
    }
}
