using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.Regexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GovITHub.Auth.Identity.Tests.Services.DeviceDetection.DeviceInfoBuilders
{
    public class DeviceTypeBuilderTests
    {
        private readonly DeviceTypeBuilder _builder;

        public DeviceTypeBuilderTests()
        {
            _builder = new DeviceTypeBuilder(new MobileDevicesResourceFileRegexLoader("GovITHub.Auth.Identity.mobiles.yml"));
        }

        [Theory]
        [InlineData(
            @"Mozilla/5.0 (Linux; Android 4.1.1; HTC One X Build/JRO03C) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19",
            "HTC One X")]
        public void ShouldRun(string userAgent, string expectedDeviceType)
        {
            var deviceInfo = new DeviceInfo { UserAgent = userAgent };
            _builder.Build(deviceInfo, userAgent);
            Assert.Equal(expectedDeviceType, deviceInfo.DeviceType);
        }
    }
}
