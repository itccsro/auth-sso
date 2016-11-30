using GovITHub.Auth.Common.Services.DeviceDetection;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using Microsoft.Extensions.DependencyInjection;

namespace GovITHub.Auth.Common.Infrastructure.Configuration
{
    public static class DeviceDetectionConfiguration
    {
        public static IServiceCollection ConfigureDeviceDetection(this IServiceCollection services)
        {
            return services.AddTransient<IDeviceDetector, DeviceDetector>()
                .AddTransient<ILoginDeviceManagementService, LoginDeviceManagementService>()
                .AddTransient<IDeviceInfoBuilder, BrowserInfoBuilder>(_ => new BrowserInfoBuilder(new SimpleResourceFileRegexLoader<BrowserRegex>("GovITHub.Auth.Identity.browsers.yml")))
                .AddTransient<IDeviceInfoBuilder, OsInfoBuilder>(_ => new OsInfoBuilder(new SimpleResourceFileRegexLoader<OsRegex>("GovITHub.Auth.Identity.oss.yml")))
                .AddTransient<IDeviceInfoBuilder, MobileDeviceInfoBuilder>(_ => new MobileDeviceInfoBuilder(new MobileDevicesResourceFileRegexLoader("GovITHub.Auth.Identity.mobiles.yml")));
        }
    }
}
