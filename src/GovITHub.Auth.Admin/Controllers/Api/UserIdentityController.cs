using GovITHub.Auth.Admin.Services;
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
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            string userName = User.Claims.GetClaim(JwtClaimTypes.Name);
            var level = User.Claims.GetClaim<OrganizationUserLevel>(JwtClaimTypes.Role);
            long organizationId = User.Claims.GetClaim<long>("OrganizationId");
            return Json(new { username = userName, level = level, organizationId = organizationId});
        }
    }
}
