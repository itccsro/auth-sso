using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders
{
    public abstract class DeviceInfoBuilderBase<TRegex> where TRegex : IRegex
    {
        public DeviceInfoBuilderBase()
        {
            Regexes = LoadResourceStream<TRegex>(ResourceName);
        }

        public IEnumerable<TRegex> Regexes { get; private set; }

        protected abstract string ResourceName { get; }

        protected IEnumerable<T> LoadResourceStream<T>(string resourceName)
        {
            var serializer = new DeserializerBuilder()
                .WithNamingConvention(namingConvention: new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();
            var assembly = typeof(DeviceInfoBuilderBase<>).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var collection = serializer.Deserialize<List<T>>(reader);
                return collection;
            }
        }

        protected void BuildInternal(DeviceInfo deviceInfo, string userAgent, Action<DeviceInfo, string> buildFunction)
        {
            Debug.Assert(deviceInfo != null);
            Debug.Assert(!String.IsNullOrEmpty(userAgent));
            Debug.Assert(buildFunction != null);

            var matchingItem = Regexes.Select(r => new
            {
                Match = Regex.Match(userAgent, r.Regex),
                Regex = r
            })
           .FirstOrDefault(tuple => tuple.Match.Success);
            if (true)
            {
                var regex = matchingItem.Regex;
                var match = matchingItem.Match;
                var version = IsFixedVersion(regex) ? regex.Version : match.GetCapturingGroupValue(regex.Version);
                var value = String.IsNullOrEmpty(version) ? regex.Name : $"{regex.Name} {version}";
                buildFunction(deviceInfo, value);
            }
        }

        protected bool IsFixedVersion(TRegex regex)
        {
            return String.IsNullOrEmpty(regex.Version) || !regex.Version.StartsWith("$");
        }
    }
}
