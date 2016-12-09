using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace GovITHub.Auth.Admin.Services
{
    public static class ClaimsExtensions
    {
        public static string GetClaim(this IEnumerable<Claim> claims, string key)
        {
            string claimValue = null;
            var claim = claims.FirstOrDefault(t => t.Type == key);
            if (claim != null)
            {
                 claimValue = claim.Value;
            }
            return claimValue;
        }
        public static T GetClaim<T>(this IEnumerable<Claim> claims, string key)
        {
            var claim = claims.FirstOrDefault(t => t.Type == key);
            if (claim != null)
            {
                var val = claim.Value;
                if (typeof(T).GetTypeInfo().IsEnum)
                    return (T)Enum.Parse(typeof(T), val);
                return (T)Convert.ChangeType(val, typeof(T));
            }
            return default(T);
        }
    }
}
