using GovITHub.Auth.Identity.Services.Audit.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Services.Audit
{
    public interface IAuditService
    {
        Task LogActionExecutingAsync(AuditActionMessage message);
    }
}
