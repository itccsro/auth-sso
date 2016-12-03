using GovITHub.Auth.Common.Data.Models;
using GovITHub.Auth.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GovITHub.Auth.Common.Data
{
    public class ApplicationDataInitializer
    {
        private ApplicationDbContext context;

        public ApplicationDataInitializer(ApplicationDbContext ctx)
        {
            context = ctx;
            context.Database.Migrate();
        }

        public async void InitializeData(UserManager<ApplicationUser> userManager)
        {
            var mainOrg = context.Organizations.FirstOrDefault(t => t.ParentId == null);
            if (mainOrg == null)
            {
                mainOrg = new Organization()
                {
                    Name = Config.MAIN_ORG_NAME,
                    Website = Config.MAIN_ORG_WEBSITE
                };
                context.Organizations.Add(mainOrg);
                context.SaveChanges();
            }

            string adminUserId;
            var adminUser = await userManager.FindByEmailAsync(Config.MAIN_ORG_ADMIN_USERNAME);
            if(adminUser == null)
            {
                adminUser = new ApplicationUser()
                {
                    Email = Config.MAIN_ORG_ADMIN_USERNAME,
                    UserName = Config.MAIN_ORG_ADMIN_USERNAME
                };
                var identityResult = await userManager.CreateAsync(adminUser, Config.MAIN_ORG_ADMIN_FIRST_PASSWORD);
            }
            adminUserId = adminUser.Id;
            var orgUser = context.OrganizationUsers.
                FirstOrDefault(t => t.OrganizationId == mainOrg.Id && t.UserId == adminUserId);
            if (orgUser == null)
            {
                orgUser = new OrganizationUser()
                {
                    Level = OrganizationUserLevel.Admin,
                    OrganizationId = mainOrg.Id,
                    UserId = adminUserId
                };
                context.OrganizationUsers.Add(orgUser);
                context.SaveChanges();
            }
        }
    }
}
