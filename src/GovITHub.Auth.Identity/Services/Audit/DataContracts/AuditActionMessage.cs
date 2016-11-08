using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Services.Audit.DataContracts
{
    public class AuditActionMessage
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string IpV4 { get; set; }

        public string IpV6 { get; set; }

        public string ActionUrl { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}
