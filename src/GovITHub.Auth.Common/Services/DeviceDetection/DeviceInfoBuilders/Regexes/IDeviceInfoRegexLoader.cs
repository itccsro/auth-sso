using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using System.Collections.Generic;

namespace GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes
{
    public interface IDeviceInfoRegexLoader<TRegex> where TRegex : IRegex
    {
        IEnumerable<TRegex> LoadRegularExpressions();
    }
}
