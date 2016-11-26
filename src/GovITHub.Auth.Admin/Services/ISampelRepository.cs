using GovITHub.Auth.Admin.Models;
using GovITHub.Auth.Common.Data;

namespace GovITHub.Auth.Admin.Services
{
    public interface ISampleRepository
    {
        void Add(Sample item);
        ModelQuery<Sample> GetAll(ModelQueryFilter filter);
        Sample Find(string key);
        Sample Remove(string key);
        void Update(Sample item);
    }
}
