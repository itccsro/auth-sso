using System.Diagnostics;

namespace GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema
{
    [DebuggerDisplay("{Name}")]
    public class OsRegex : IRegex
    {
        public string Regex { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }
    }
}
