using GovITHub.Auth.Common.Data;
using GovITHub.Auth.Common.Data.Models;
using IdentityModel;
using System;
using System.Security.Claims;

namespace GovITHub.Auth.Admin.Services.Impl
{
    public class UserClaimsExtender : IUserClaimsExtender
    {
        IOrganizationRepository _repository;
        public UserClaimsExtender(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public ClaimsPrincipal TransformClaims(ClaimsPrincipal incoming)
        {
            var identity = (ClaimsIdentity)incoming.Identity;
            var orgUser = _repository.GetOrganizationUser(identity.Claims.GetClaim(JwtClaimTypes.Name));
            if (orgUser != null)
            {
                identity.AddClaim(new Claim(JwtClaimTypes.Role, Convert.ToString(orgUser.Level)));
                identity.AddClaim(new Claim("OrganizationId", Convert.ToString(orgUser.OrganizationId)));
            }
            return incoming;
        }

    }
}
