using GovITHub.Auth.Common.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using Xunit;

namespace GovITHub.Auth.Identity.Tests.Services.DeviceDetection.DeviceInfoBuilders
{
    public class OsInfoBuilderTests
    {
        private readonly OsInfoBuilder _builder;

        public OsInfoBuilderTests()
        {
            _builder = new OsInfoBuilder(new SimpleResourceFileRegexLoader<OsRegex>("GovITHub.Auth.Identity.oss.yml"));
        }

        [Theory]
        [InlineData(
           @"Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.87 Safari/537.36 OPR/41.0.2353.56",
           "Windows 8.1")]
        [InlineData(
           @"Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36",
           "Windows 8.1")]
        [InlineData(
           @"Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko",
           "Windows 8.1")]
        public void ShoulProperlyParseClientInfo(string userAgent, string expectedOutput)
        {
            var deviceInfo = new DeviceInfo
            {
                UserAgent = userAgent
            };
            _builder.Build(deviceInfo, userAgent);
            Assert.Equal(expectedOutput, deviceInfo.OperatingSystem);
        }
    }
}
