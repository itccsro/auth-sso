using GovITHub.Auth.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Admin.Services
{
    public interface ISampleRepository
    {
        void Add(Sample item);
        IEnumerable<Sample> GetAll();
        Sample Find(string key);
        Sample Remove(string key);
        void Update(Sample item);
    }
}
