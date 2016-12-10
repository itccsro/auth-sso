using GovITHub.Auth.Admin.Services;
using GovITHub.Auth.Common.Data;
using GovITHub.Auth.Common.Data.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GovITHub.Auth.Admin.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserIdentityController : Controller
    {
        IOrganizationRepository _organizationRepository;
        public UserIdentityController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            string userName = User.Claims.GetClaim(JwtClaimTypes.Name);
            var level = User.Claims.GetClaim<OrganizationUserLevel>(JwtClaimTypes.Role);
            long organizationId = User.Claims.GetClaim<long>("OrganizationId");
            string organizationName = string.Empty;
            if (organizationId > 0)
            {
                var organization = _organizationRepository.Find(organizationId);
                organizationName = organization?.Name;
            }
            return Json(new { username = userName, level = level, organizationId = organizationId, organizationName = organizationName});
        }
    }
}
