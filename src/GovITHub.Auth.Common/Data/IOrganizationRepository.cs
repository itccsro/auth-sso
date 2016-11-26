using GovITHub.Auth.Common.Data.Models;
using System.Collections.Generic;

namespace GovITHub.Auth.Common.Data
{
    public interface IOrganizationRepository
    {
        Organization Find(long id);
        void Update(Organization item);
        Organization Remove(long id);
        void Add(Organization item);
        ModelQuery<Organization> GetAll(ModelQueryFilter filter);
    }
}
