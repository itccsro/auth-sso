using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using GovITHub.Auth.Identity.Services.Audit.DataContracts;
using GovITHub.Auth.Identity.Data;

namespace GovITHub.Auth.Identity.Services.Audit
{
    public class AuditService : IAuditService
    {
        private readonly ApplicationDbContext _dbContext;

        public AuditService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogActionExecuting(AuditActionMessage message)
        {
            _dbContext.Add(message);
            _dbContext.SaveChanges();
        }
    }
}
