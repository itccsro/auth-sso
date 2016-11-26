using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.Regexes;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders
{
    public class OsInfoBuilder : DeviceInfoBuilderBase<OsRegex>, IDeviceInfoBuilder
    {
        public OsInfoBuilder(IDeviceInfoRegexLoader<OsRegex> regexLoader) : base(regexLoader)
        {
        }

        public void Build(DeviceInfo deviceInfo, string userAgent)
        {
            BuildInternal(deviceInfo, userAgent, (info, value) =>
            {
                deviceInfo.OperatingSystem = value;
            });
        }
    }
}
