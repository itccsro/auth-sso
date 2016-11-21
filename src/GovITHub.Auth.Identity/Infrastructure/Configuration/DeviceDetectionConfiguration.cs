using GovITHub.Auth.Identity.Services.DeviceDetection;
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
            return services.AddTransient<IDeviceDetector, DeviceDetector>();
        }
    }
}
