using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders;
using System.Collections.Generic;

namespace GovITHub.Auth.Identity.Services.DeviceDetection
{
    public class DeviceDetector : IDeviceDetector
    {
        private readonly IEnumerable<IDeviceInfoBuilder> _builders;

        public DeviceDetector(IEnumerable<IDeviceInfoBuilder> builders)
        {
            _builders = builders;
        }


        public DeviceInfo GetDeviceInfo(string userAgentString)
        {
            var result = new DeviceInfo
            {
                UserAgent = userAgentString
            };
            foreach (var builder in _builders)
            {
                builder.Build(result, userAgentString);
            }
            return result;
        }
    }
}
