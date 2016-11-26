using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using System.Collections.Generic;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.Regexes
{
    public interface IDeviceInfoRegexLoader<TRegex> where TRegex : IRegex
    {
        IEnumerable<TRegex> LoadRegularExpressions();
    }
}
