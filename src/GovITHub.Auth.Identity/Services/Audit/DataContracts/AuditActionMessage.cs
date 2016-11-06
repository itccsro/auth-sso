using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Services.Audit.DataContracts
{
    public class AuditActionMessage
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string IpV4 { get; set; }

        public string IpV6 { get; set; }

        public string ActionUrl { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}
