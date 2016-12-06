using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovITHub.Auth.Common.Services.DeviceDetection.DataContracts;

namespace GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders
{
    public interface IDeviceInfoBuilder
    {
        void Build(DeviceInfo deviceInfo, string userAgent);
    }
}
