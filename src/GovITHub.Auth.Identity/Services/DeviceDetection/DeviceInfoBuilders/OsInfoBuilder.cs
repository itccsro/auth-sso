using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders
{
    public class OsInfoBuilder : DeviceInfoBuilderBase<OsRegex>, IDeviceInfoBuilder
    {
        protected override string ResourceName
        {
            get
            {
                return "GovITHub.Auth.Identity.oss.yml";
            }
        }

        public void Build(DeviceInfo deviceInfo, string userAgent)
        {
            BuildInternal(deviceInfo, userAgent, (match, regex) =>
            {
                var version = IsFixedVersion(regex) ? regex.Version : match.GetCapturingGroupValue(regex.Version);
                deviceInfo.OperatingSystem = $"{regex.Name} {version}";
            });
        }

        private bool IsFixedVersion(OsRegex regex)
        {
            int versionNumber;
            return Int32.TryParse(regex.Version, out versionNumber);
        }
    }
}
