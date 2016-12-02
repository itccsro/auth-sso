using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes;
using System.IO;
using System.Reflection;

namespace GovITHub.Auth.Common.Tests.DeviceDetection.DeviceInfoBuilders.Regexes
{
    public class RegexResourceFileStreamReader : IRegexStreamReader
    {
        private readonly string _resourceName;

        public RegexResourceFileStreamReader(string resourceName)
        {
            _resourceName = resourceName;
        }

        public Stream GetRegexStream()
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream(_resourceName);
        }
    }
}
