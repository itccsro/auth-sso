using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes
{
    public class SimpleResourceFileRegexLoader<TRegex> : IDeviceInfoRegexLoader<TRegex> where TRegex : IRegex
    {
        private readonly string _resourceFile;

        public SimpleResourceFileRegexLoader(string resourceFile)
        {
            _resourceFile = resourceFile;
        }

        public IEnumerable<TRegex> LoadRegularExpressions()
        {
            var serializer = new DeserializerBuilder()
               .WithNamingConvention(namingConvention: new CamelCaseNamingConvention())
               .IgnoreUnmatchedProperties()
               .Build();
            var assembly = typeof(DeviceInfoBuilderBase<>).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream(_resourceFile))
            using (var reader = new StreamReader(stream))
            {
                var collection = serializer.Deserialize<List<TRegex>>(reader);
                return collection;
            }
        }
    }
}
