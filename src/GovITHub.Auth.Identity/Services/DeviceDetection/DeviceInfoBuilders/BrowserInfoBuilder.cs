using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.Regexes;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders
{
    public class BrowserInfoBuilder : DeviceInfoBuilderBase<BrowserRegex>, IDeviceInfoBuilder
    {
        public BrowserInfoBuilder(IDeviceInfoRegexLoader<BrowserRegex> regexLoader) : base(regexLoader)
        {
        }

        public void Build(DeviceInfo deviceInfo, string userAgent)
        {
            BuildInternal(deviceInfo, userAgent, (info, value) =>
            {
                deviceInfo.Client = value;
            });

        }
    }
}
