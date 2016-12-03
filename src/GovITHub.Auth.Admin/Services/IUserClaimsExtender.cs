using System.Security.Claims;

namespace GovITHub.Auth.Admin.Services
{
    public interface IUserClaimsExtender
    {
        ClaimsPrincipal TransformClaims(ClaimsPrincipal incoming);
    }
}