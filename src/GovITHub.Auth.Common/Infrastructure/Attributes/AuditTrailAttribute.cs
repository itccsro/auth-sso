using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using GovITHub.Auth.Common.Services.Audit;
using GovITHub.Auth.Common.Services.Audit.DataContracts;

namespace GovITHub.Auth.Common.Infrastructure.Attributes
{
    public class AuditTrailAttribute : ActionFilterAttribute
    {
        private readonly IAuditService auditService;

        public AuditTrailAttribute(IAuditService auditService)
        {
            this.auditService = auditService;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var message = new AuditActionMessage
            {
                ActionUrl = context.HttpContext.Request.Path,
                Id = Guid.NewGuid().ToString(),
                IpV4 = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                IpV6 = context.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString(),
                Timestamp = DateTimeOffset.Now,
                UserName = context.HttpContext.User.Identity.Name
            };
            auditService.LogActionExecuting(message);
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
