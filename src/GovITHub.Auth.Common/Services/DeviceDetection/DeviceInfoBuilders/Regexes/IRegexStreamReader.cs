using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes
{
    public interface IRegexStreamReader
    {
        Stream GetRegexStream();
    }
}
