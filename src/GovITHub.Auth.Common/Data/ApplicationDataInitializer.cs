using GovITHub.Auth.Common.Data.Models;
using GovITHub.Auth.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace GovITHub.Auth.Common.Data
{
    public class ApplicationDataInitializer
    {
        private ApplicationDbContext context;

        public ApplicationDataInitializer(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task InitializeDataAsync(UserManager<ApplicationUser> userManager, IConfigurationRoot configurationService)
        {
            context.Database.Migrate();
            var mainOrg = context.Organizations.FirstOrDefault(t => t.ParentId == null);
            if (mainOrg == null)
            {
                mainOrg = CreateRootOrganization(configurationService);
            }
            string adminUserId = await GetRootOrganizationAdminIdAsync(userManager, configurationService);
            AttachAdminUserToOrganization(mainOrg.Id, adminUserId);
        }

        private Organization CreateRootOrganization(IConfigurationRoot configurationService)
        {
            string orgName = configurationService[Config.MAIN_ORG_NAME_KEY];
            if (string.IsNullOrWhiteSpace(orgName))
                throw new ArgumentException("Root organization name missing from configuration!");
            var mainOrg = new Organization()
            {
                Name = orgName,
                Website = configurationService[Config.MAIN_ORG_WEBSITE_KEY]
            };
            context.Organizations.Add(mainOrg);
            context.SaveChanges();
            return mainOrg;
        }

        /// <summary>
        /// Returns the Id of the user with admin role attached to root organization.
        /// It creates the user if the user is not present, based on configuration.
        /// </summary>
        /// <param name="configurationService">App configuration service</param>
        /// <returns>Id of the admin user, as string.</returns>
        private async Task<string> GetRootOrganizationAdminIdAsync(UserManager<ApplicationUser> userManager, IConfigurationRoot configurationService)
        {
            var defaultAdminUsername = configurationService[Config.MAIN_ORG_ADMIN_USERNAME_KEY];
            if (string.IsNullOrWhiteSpace(defaultAdminUsername))
                throw new ArgumentNullException("Root organization's admin username (email) missing from configuration!");
            var adminUser = await userManager.FindByEmailAsync(defaultAdminUsername);
            if (adminUser == null)
            {
                var defaultAdminPassword = configurationService[Config.MAIN_ORG_ADMIN_FIRST_PASSWORD_KEY];
                if (string.IsNullOrWhiteSpace(defaultAdminPassword))
                    throw new ArgumentNullException("Root organization's admin password missing from configuration!");
                // maybe validate the password strength also ?
                adminUser = new ApplicationUser()
                {
                    Email = defaultAdminUsername,
                    UserName = defaultAdminUsername
                };
                var identityResult = await userManager.CreateAsync(adminUser,
                    defaultAdminPassword);
            }
            return adminUser.Id;
        }

        private void AttachAdminUserToOrganization(long organizationId, string adminUserId)
        {
            var orgUser = context.OrganizationUsers.
                FirstOrDefault(t => t.OrganizationId == organizationId && t.UserId == adminUserId);
            // if not already attached, attach it
            if (orgUser == null)
            {
                orgUser = new OrganizationUser()
                {
                    Level = OrganizationUserLevel.Admin,
                    OrganizationId = organizationId,
                    UserId = adminUserId
                };
                context.OrganizationUsers.Add(orgUser);
                context.SaveChanges();
            }
        }
    }
}
