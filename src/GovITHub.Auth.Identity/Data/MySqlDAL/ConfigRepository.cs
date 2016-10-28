using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Data.MySqlDAL
{
    public class ConfigRepository : IConfigRepository
    {
        IScopeStore scopeStore;
        IClientStore clientStore;
        ILogger logger;
        public ConfigRepository(IScopeStore scopeStore, IClientStore clientStore)
        {
            this.scopeStore = scopeStore;
            this.clientStore = clientStore;
        }
        public async Task<Client> GetClient(string id)
        {
            try
            {
                var client = await clientStore.FindClientByIdAsync(id);
                return client;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error retrieving client {0}. Reason {1}", id, ex);
            }
            return null;
        }

        public async Task<IEnumerable<Scope>> GetScopesAsync()
        {
            var  scopes  = await scopeStore.GetScopesAsync();
            return scopes;
        }
    }
}
