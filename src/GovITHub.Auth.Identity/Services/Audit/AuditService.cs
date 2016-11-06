using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using GovITHub.Auth.Identity.Services.Audit.DataContracts;
using Microsoft.AspNetCore.Hosting;

namespace GovITHub.Auth.Identity.Services.Audit
{
    public class AuditService : IAuditService
    {
        private readonly string _path;

        public AuditService(IHostingEnvironment hostingEnvironment)
        {
            _path = Path.Combine(hostingEnvironment.ContentRootPath, "audit-logs");
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }
        
        public Task LogActionExecutingAsync(AuditActionMessage message)
        {
            return Task.Factory.StartNew(() =>
            {
                var filePath = Path.Combine(_path, message.Id.ToString());
                File.WriteAllText(filePath, String.Concat(
                    String.Format("Id: {0}", message.Id), Environment.NewLine,
                    String.Format("IpV4: {0}", message.IpV4), Environment.NewLine,
                    String.Format("IpV6: {0}", message.IpV6), Environment.NewLine,
                    String.Format("UserName: {0}", message.UserName), Environment.NewLine,
                    String.Format("ActionUrl: {0}", message.ActionUrl), Environment.NewLine,
                    String.Format("Timestamp: {0}", message.Timestamp), Environment.NewLine));
            });
        }
    }
}
