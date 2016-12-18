using GovITHub.Auth.Common.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using Moq;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace GovITHub.Auth.Common.Tests.Services.DeviceDetection.DeviceInfoBuilders
{
    public class OsInfoBuilderTests : IDisposable
    {
        private const string regexFilePath = "GovITHub.Auth.Common.Tests.oss.yml";

        private readonly Mock<Microsoft.Extensions.FileProviders.IFileProvider> fileProviderMock;
        private readonly Mock<Microsoft.Extensions.FileProviders.IFileInfo> fileInfoMock;
        private readonly Mock<Microsoft.Extensions.Logging.ILoggerFactory> loggerFactoryMock;
        private readonly Mock<Microsoft.Extensions.Logging.ILogger> loggerMock;

        private readonly OsInfoBuilder osInfoBuilder;

        public OsInfoBuilderTests()
        {
            fileProviderMock = new Mock<Microsoft.Extensions.FileProviders.IFileProvider>(MockBehavior.Strict);
            fileInfoMock = new Mock<Microsoft.Extensions.FileProviders.IFileInfo>(MockBehavior.Strict);
            loggerFactoryMock = new Mock<Microsoft.Extensions.Logging.ILoggerFactory>(MockBehavior.Strict);
            loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger>(MockBehavior.Strict);

            loggerFactoryMock.Setup(x => x.CreateLogger("GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes.SimpleResourceFileRegexLoader")).Returns(loggerMock.Object);

            osInfoBuilder = new OsInfoBuilder(new SimpleResourceFileRegexLoader<OsRegex>(regexFilePath, fileProviderMock.Object, loggerFactoryMock.Object));
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
                UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.87 Safari/537.36 OPR/41.0.2353.56"
            };

            osInfoBuilder.Build(deviceInfo, deviceInfo.UserAgent);

            Assert.Equal(null, deviceInfo.OperatingSystem);
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
        public void ShoulProperlyParseClientInfo(string userAgent, string expectedOperatingSystem)
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

            osInfoBuilder.Build(deviceInfo, userAgent);

            Assert.Equal(expectedOperatingSystem, deviceInfo.OperatingSystem);
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