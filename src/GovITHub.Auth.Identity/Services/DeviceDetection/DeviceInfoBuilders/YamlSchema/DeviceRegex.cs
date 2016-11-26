using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema
{
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
