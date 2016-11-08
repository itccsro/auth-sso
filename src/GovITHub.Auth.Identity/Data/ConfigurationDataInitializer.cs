using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GovITHub.Auth.Identity.Data
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

            if (cfgDbContext.Scopes.FirstOrDefault() == null)
            {
                foreach (var client in Config.GetScopes())
                {
                    cfgDbContext.Scopes.Add(client.ToEntity());
                }
                cfgDbContext.SaveChanges();
            }
        }
    }
}
