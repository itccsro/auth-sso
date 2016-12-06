using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes
{
    public class RegexFileStreamReader : IRegexStreamReader
    {
        private readonly string _fileName;
        private readonly IHostingEnvironment _hostingEnvironment;

        public RegexFileStreamReader(string fileName, IHostingEnvironment hostingEnvironment)
        {
            _fileName = fileName;
            _hostingEnvironment = hostingEnvironment;
        }

        public Stream GetRegexStream()
        {
            var fileInfo = _hostingEnvironment.WebRootFileProvider.GetFileInfo(_fileName);
            return fileInfo.CreateReadStream();
        }
    }
}
