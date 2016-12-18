using GovITHub.Auth.Common.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes;
using Moq;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace GovITHub.Auth.Common.Tests.Services.DeviceDetection.DeviceInfoBuilders
{
    public class MobileDeviceInfoBuilderTests : IDisposable
    {
        private const string regexFilePath = "GovITHub.Auth.Common.Tests.mobiles.yml";

        private readonly Mock<Microsoft.Extensions.FileProviders.IFileProvider> fileProviderMock;
        private readonly Mock<Microsoft.Extensions.FileProviders.IFileInfo> fileInfoMock;
        private readonly Mock<Microsoft.Extensions.Logging.ILoggerFactory> loggerFactoryMock;
        private readonly Mock<Microsoft.Extensions.Logging.ILogger> loggerMock;

        private readonly MobileDeviceInfoBuilder mobileDeviceInfoBuilder;

        public MobileDeviceInfoBuilderTests()
        {
            fileProviderMock = new Mock<Microsoft.Extensions.FileProviders.IFileProvider>(MockBehavior.Strict);
            fileInfoMock = new Mock<Microsoft.Extensions.FileProviders.IFileInfo>(MockBehavior.Strict);
            loggerFactoryMock = new Mock<Microsoft.Extensions.Logging.ILoggerFactory>(MockBehavior.Strict);
            loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger>(MockBehavior.Strict);

            loggerFactoryMock.Setup(x => x.CreateLogger("GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes.MobileDevicesResourceFileRegexLoader")).Returns(loggerMock.Object);

            mobileDeviceInfoBuilder = new MobileDeviceInfoBuilder(new MobileDevicesResourceFileRegexLoader(regexFilePath, fileProviderMock.Object, loggerFactoryMock.Object));
        }

        [Fact]
        public void Build_WhenFileDoesNotExist_ThenErrorIsLogged()
        {
            fileProviderMock.Setup(x => x.GetFileInfo(regexFilePath)).Returns(fileInfoMock.Object);

            fileInfoMock.SetupGet(x => x.Exists).Returns(false);

            loggerMock
                .Setup(x => x.Log(It.IsAny<Microsoft.Extensions.Logging.LogLevel>(), It.IsAny<Microsoft.Extensions.Logging.EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback<Microsoft.Extensions.Logging.LogLevel, Microsoft.Extensions.Logging.EventId, object, Exception, Func<object, Exception, string>>((logLevelCallback, eventIdCallback, objectCallback, exceptionCallback, formatterCallback) =>
                {
                    Assert.Equal(Microsoft.Extensions.Logging.LogLevel.Error, logLevelCallback);
                    Assert.Equal("Device detector files missing!", formatterCallback(objectCallback, exceptionCallback));
                });

            DeviceInfo deviceInfo = new DeviceInfo
            {
                UserAgent = "Mozilla/5.0 (Linux; Android 4.1.1; HTC One X Build/JRO03C) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19"
            };

            mobileDeviceInfoBuilder.Build(deviceInfo, deviceInfo.UserAgent);

            Assert.Equal(null, deviceInfo.MobileDevice);
        }

        [Theory]
        [InlineData(
            @"Mozilla/5.0 (Linux; Android 4.1.1; HTC One X Build/JRO03C) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19",
            "HTC One X")]
        [InlineData(
            @"Mozilla/5.0 (Linux; Android 6.0; HTC One M9 Build/MRA58K) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.98 Mobile Safari/537.36",
            "HTC One M9")]
        [InlineData(
            @"Mozilla/5.0 (Linux; Android 6.0.1; E6653 Build/32.2.A.0.253) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.98 Mobile Safari/537.36",
            "Sony Xperia Z5")]
        [InlineData(
            @"Mozilla/5.0 (Linux; Android 6.0.1; Nexus 6P Build/MMB29P) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.83 Mobile Safari/537.36",
            "Google Nexus 6P")]
        [InlineData(
            @"Mozilla/5.0 (Windows Phone 10.0; Android 4.2.1; Microsoft; Lumia 950) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Mobile Safari/537.36 Edge/13.10586",
            "Nokia Lumia 950")]
        [InlineData(
            @"Mozilla/5.0 (Linux; Android 6.0.1; SGP771 Build/32.2.A.0.253; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/52.0.2743.98 Safari/537.36",
            "Sony SGP771")]
        [InlineData(
            @"Mozilla/5.0 (Linux; Android 5.0.2; SAMSUNG SM-T550 Build/LRX22G) AppleWebKit/537.36 (KHTML, like Gecko) SamsungBrowser/3.3 Chrome/38.0.2125.102 Safari/537.36",
            "Samsung SM-T550")]
        public void ShouldRun(string userAgent, string expectedMobileDevice)
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream(regexFilePath);

            fileProviderMock.Setup(x => x.GetFileInfo(regexFilePath)).Returns(fileInfoMock.Object);

            fileInfoMock.SetupGet(x => x.Exists).Returns(true);

            fileInfoMock.Setup(x => x.CreateReadStream()).Returns(stream);

            DeviceInfo deviceInfo = new DeviceInfo
            {
                UserAgent = userAgent
            };

            mobileDeviceInfoBuilder.Build(deviceInfo, userAgent);

            Assert.Equal(expectedMobileDevice, deviceInfo.MobileDevice);
        }


        public void Dispose()
        {
            fileProviderMock.VerifyAll();
            fileInfoMock.VerifyAll();
            loggerFactoryMock.VerifyAll();
            loggerMock.VerifyAll();
        }
    }
}