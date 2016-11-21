using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Diagnostics;

namespace GovITHub.Auth.Identity.Services.DeviceDetection
{
    public class DeviceDetector : IDeviceDetector
    {
        private readonly Lazy<IEnumerable<BrowserRegex>> _browserRegexes = new Lazy<IEnumerable<BrowserRegex>>(() => LoadResourceStream<BrowserRegex>("GovITHub.Auth.Identity.browsers.yml"), LazyThreadSafetyMode.ExecutionAndPublication);

        public IEnumerable<dynamic> BrowserRegexes
        {
            get { return _browserRegexes.Value; }
        }

        [DebuggerDisplay("Name")]
        public class BrowserRegex
        {
            public string Regex { get; set; }
            public string Name { get; set; }
            public string Version { get; set; }
            public Engine Engine { get; set; }
        }

        [DebuggerDisplay("{Default}")]
        public class Engine
        {
            public string Default { get; set; }
        }

        private static IEnumerable<T> LoadResourceStream<T>(string resourceName)
        {
            var serializer = new DeserializerBuilder()
                .WithNamingConvention(namingConvention: new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();
            var assembly = Assembly.GetEntryAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var collection = serializer.Deserialize<List<T>>(reader);
                return collection;
            }
        }

        public DeviceInfo GetDeviceInfo(string userAgentString)
        {
            var result = new DeviceInfo
            {
                UserAgent = userAgentString
            };

            var match = BrowserRegexes.Cast<BrowserRegex>().Select(r => new
            {
                Match = Regex.Match(userAgentString, r.Regex),
                Regex = r
            })
            .FirstOrDefault(tuple => tuple.Match.Success);
            if (match != null)
            {
                var browserName = match.Regex.Name;
                var groupIndex = int.Parse(match.Regex.Version.TrimStart('$'));
                var version = match.Match.Groups[groupIndex];
                result.Client = $"{browserName} {version}";
            }


            return result;
        }
    }
}
