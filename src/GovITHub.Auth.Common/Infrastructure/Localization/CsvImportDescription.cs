using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GovITHub.Auth.Common.Infrastructure.Localization
{
    public class CsvImportDescription
    {
        public string Information { get; set; }

        public ICollection<IFormFile> File { get; set; }
    }
}
