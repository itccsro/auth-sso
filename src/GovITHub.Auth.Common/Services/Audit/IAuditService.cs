using GovITHub.Auth.Common.Services.Audit.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Common.Services.Audit
{
    public interface IAuditService
    {
        void LogActionExecuting(AuditActionMessage message);
    }
}
