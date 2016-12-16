using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GovITHub.Auth.Common.Data
{
    public class ConfigurationDataInitializer
    {
        private ConfigurationDbContext cfgDbContext;
        private PersistedGrantDbContext prstDbContext;

        public ConfigurationDataInitializer(ConfigurationDbContext configContext, PersistedGrantDbContext prstContext)
        {
            cfgDbContext = configContext;
            prstDbContext = prstContext;
        }

        public void InitializeData()
        {
            cfgDbContext.Database.Migrate();
            prstDbContext.Database.Migrate();
            InitializeClientsAndScopes();
        }

        private void InitializeClientsAndScopes()
        {
            if (cfgDbContext.Clients.FirstOrDefault() == null)
            {
                foreach (var client in Config.GetClients())
                {
                    cfgDbContext.Clients.Add(client.ToEntity());
                }
                cfgDbContext.SaveChanges();
            }

            if (cfgDbContext.ApiResources.FirstOrDefault() == null)
            {
                foreach (var apiResource in Config.GetApiResources())
                {
                    cfgDbContext.ApiResources.Add(apiResource.ToEntity());
                }
                cfgDbContext.SaveChanges();
            }

            if (cfgDbContext.IdentityResources.FirstOrDefault() == null)
            {
                foreach (var identityResource in Config.GetIdentityResources())
                {
                    cfgDbContext.IdentityResources.Add(identityResource.ToEntity());
                }
                cfgDbContext.SaveChanges();
            }
        }
    }
}
