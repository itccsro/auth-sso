using IdentityServer4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Data
{
    public interface IConfigRepository
    {
        Task<IEnumerable<Scope>> GetScopesAsync();
        Task<Client> GetClient(string id);

    }
}
