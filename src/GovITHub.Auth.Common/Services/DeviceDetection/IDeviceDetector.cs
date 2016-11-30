using GovITHub.Auth.Common.Services.DeviceDetection.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Common.Services.DeviceDetection
{
    public interface IDeviceDetector
    {
        DeviceInfo GetDeviceInfo(string userAgentString);
    }
}
