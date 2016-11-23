using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema
{
    [DebuggerDisplay("Name")]
    public class BrowserRegex : IRegex
    {
        public string Regex { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public Engine Engine { get; set; }
    }
}
