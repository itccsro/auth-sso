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
        private readonly IRegexStreamReader _reader;

        public SimpleResourceFileRegexLoader(IRegexStreamReader reader)
        {
            _reader = reader;
        }

        public IEnumerable<TRegex> LoadRegularExpressions()
        {
            var serializer = new DeserializerBuilder()
               .WithNamingConvention(namingConvention: new CamelCaseNamingConvention())
               .IgnoreUnmatchedProperties()
               .Build();
            var assembly = typeof(DeviceInfoBuilderBase<>).GetTypeInfo().Assembly;
            using (var stream = _reader.GetRegexStream())
            using (var reader = new StreamReader(stream))
            {
                var collection = serializer.Deserialize<List<TRegex>>(reader);
                return collection;
            }
        }
    }
}
