using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using System.Linq;
using System.Text.RegularExpressions;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders
{
    public class ClientInfoBuilder : DeviceInfoBuilderBase<BrowserRegex>, IDeviceInfoBuilder
    {
        protected override string ResourceName
        {
            get
            {
                return "GovITHub.Auth.Identity.browsers.yml";
            }
        }

        public void Build(DeviceInfo deviceInfo, string userAgent)
        {
            BuildInternal(deviceInfo, userAgent, (match, regex) =>
            {
                var browserName = regex.Name;
                var version = match.GetCapturingGroupValue(regex.Version);
                deviceInfo.Client = $"{browserName} {version}";
            });

        }
    }
}
