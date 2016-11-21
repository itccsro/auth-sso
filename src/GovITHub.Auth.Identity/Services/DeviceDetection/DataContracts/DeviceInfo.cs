using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts
{
    public class DeviceInfo
    {
        public string UserAgent { get; set; }

        public string OperatingSystem { get; set; }

        public string DeviceType { get; set; }

        public string Client { get; set; }
    }
}
