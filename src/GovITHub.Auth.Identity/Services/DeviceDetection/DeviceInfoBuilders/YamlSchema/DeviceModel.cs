using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema
{
    public class DeviceModel
    {
        public string Regex { get; set; }

        public string Model { get; set; }

        public string Device { get; set; }
    }
}
