using GovITHub.Auth.Common.Services.DeviceDetection;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GovITHub.Auth.Common.Infrastructure.Configuration
{
    public static class DeviceDetectionConfiguration
    {
        public static IServiceCollection ConfigureDeviceDetection(this IServiceCollection services)
        {
            return services.AddTransient<IDeviceDetector, DeviceDetector>()
                .AddTransient<ILoginDeviceManagementService, LoginDeviceManagementService>()
                .AddBrowserInfoBuilder()
                .AddOsInfoBuilder()
                .AddMobileDeviceInfoBuilder();
        }

        private static IServiceCollection AddMobileDeviceInfoBuilder(this IServiceCollection services)
        {
            return services.AddDeviceInfoBuilder((config, hostingEnvironment) =>
            {
                var reader = new RegexFileStreamReader(config.GetDeviceDetectionResourceFile("mobiles"), hostingEnvironment);
                var loader = new MobileDevicesResourceFileRegexLoader(reader);
                return new MobileDeviceInfoBuilder(loader);
            });
        }

        private static IServiceCollection AddOsInfoBuilder(this IServiceCollection services)
        {
            return services.AddDeviceInfoBuilder((config, hostingEnvironment) =>
            {
                var reader = new RegexFileStreamReader(config.GetDeviceDetectionResourceFile("oss"), hostingEnvironment);
                var loader = new SimpleResourceFileRegexLoader<OsRegex>(reader);
                return new OsInfoBuilder(loader);
            });
        }

        private static IServiceCollection AddBrowserInfoBuilder(this IServiceCollection services)
        {
            return services.AddDeviceInfoBuilder((config, hostingEnvironment) =>
            {
                var reader = new RegexFileStreamReader(config.GetDeviceDetectionResourceFile("browsers"), hostingEnvironment);
                var loader = new SimpleResourceFileRegexLoader<BrowserRegex>(reader);
                return new BrowserInfoBuilder(loader);
            });
        }

        private static IServiceCollection AddDeviceInfoBuilder<T>(this IServiceCollection services, Func<IConfigurationRoot, IHostingEnvironment, T> factoryFunc) where T : class, IDeviceInfoBuilder
        {
            return services.AddTransient<IDeviceInfoBuilder, T>(serviceProvider =>
            {
                var config = serviceProvider.GetService<IConfigurationRoot>();
                var hostingEnvironment = serviceProvider.GetService<IHostingEnvironment>();
                return factoryFunc(config, hostingEnvironment);
            });
        }

        private static string GetDeviceDetectionResourceFile(this IConfigurationRoot configuration, string key)
        {
            var section = configuration.GetSection("DeviceDetection");
            if (section == null)
            {
                throw new ArgumentException("There is no configuration section pointing to the regex files for device detection.");
            }
            var filePath = section[key];
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException($"No value provided for key '{key}' in section 'DeviceDetection'.");
            }
            return filePath;
        }
    }
}
