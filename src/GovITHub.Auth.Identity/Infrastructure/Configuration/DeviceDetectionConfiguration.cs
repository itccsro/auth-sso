using GovITHub.Auth.Identity.Services.DeviceDetection;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.Regexes;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Infrastructure.Configuration
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
