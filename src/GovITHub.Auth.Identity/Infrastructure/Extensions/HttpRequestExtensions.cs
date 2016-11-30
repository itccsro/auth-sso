using Microsoft.AspNetCore.Http;
using System;

namespace GovITHub.Auth.Identity.Infrastructure.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetUserAgent(this HttpRequest request)
        {
            const string userAgentKey = "User-Agent";
            if (request.Headers == null || !request.Headers.ContainsKey(userAgentKey))
            {
                return String.Empty;
            }
            var userAgent = request.Headers[userAgentKey];
            return Convert.ToString(userAgent);
        }
    }
}
