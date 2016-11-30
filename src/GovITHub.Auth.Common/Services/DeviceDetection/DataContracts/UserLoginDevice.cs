using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Common.Services.DeviceDetection.DataContracts
{
    public class UserLoginDevice
    {
        public string UserId { get; set; }

        public string DeviceId { get; set; }

        public DateTime RegistrationTimeUtc { get; set; }

        public DateTime LastLoginTimeUtc { get; set; }
    }
}
