using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GovITHub.Auth.Identity.Tests.Services.DeviceDetection.DeviceInfoBuilders
{
    public class ClientInfoBuilderTests
    {
        private readonly ClientInfoBuilder _builder;

        public ClientInfoBuilderTests()
        {
            _builder = new ClientInfoBuilder();
        }

        [Theory]
        [InlineData(
            @"Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.87 Safari/537.36 OPR/41.0.2353.56",
            "Opera 41.0.2353.56")]
        [InlineData(
            @"Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36",
            "Chrome 54.0.2840.99")]
        [InlineData(
            @"Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko",
            "Internet Explorer 11.0")]
        public void ShoulProperlyParseClientInfo(string userAgent, string expectedOutput)
        {
            var deviceInfo = new DeviceInfo
            {
                UserAgent = userAgent
            };
            _builder.Build(deviceInfo, userAgent);
            Assert.Equal(expectedOutput, deviceInfo.Client);
        }
    }
}
