using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema
{
    public interface IRegex
    {
        string Name { get; set; }
        string Regex { get; set; }
        string Version { get; set; }
    }
}
