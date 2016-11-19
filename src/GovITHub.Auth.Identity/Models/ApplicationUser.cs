using GovITHub.Auth.Identity.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GovITHub.Auth.Identity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public OrganizationUser OrganizationUser { get; set; }
    }
}
