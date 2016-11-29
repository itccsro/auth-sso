using System.Collections.Generic;
using System.Diagnostics;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema
{
    [DebuggerDisplay("{Name}")]
    public class DeviceRegex : IRegex
    {
        public string Regex { get; set; }

        public string DeviceType { get; set; }

        public string Model { get; set; }

        public List<DeviceModel> Models { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }
    }
}
