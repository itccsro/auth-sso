using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes
{
    public class MobileDevicesResourceFileRegexLoader : IDeviceInfoRegexLoader<MobileDeviceRegex>
    {
        private readonly IRegexStreamReader _reader;

        public MobileDevicesResourceFileRegexLoader(IRegexStreamReader reader)
        {
            _reader = reader;
        }

        public IEnumerable<MobileDeviceRegex> LoadRegularExpressions()
        {
            var serializer = new DeserializerBuilder()
                .WithNamingConvention(namingConvention: new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();
            using (var stream = _reader.GetRegexStream())
            using (var input = new StreamReader(stream))
            {
                dynamic collection = serializer.Deserialize(input);
                IEnumerable<MobileDeviceRegex> deviceRegexes = ConvertToDeviceRegex(collection);
                /// HACK: The regex for MicroMax is not well formed and .net framework throws an error
                /// As a workaround, exclude the MicroMax regex from the result until the regex is replaced.
                return deviceRegexes.Where(r => r.Name != "MicroMax" && r.Name != "Symphony");
            }
        }

        private IEnumerable<MobileDeviceRegex> ConvertToDeviceRegex(dynamic collection)
        {
            foreach (dynamic item in collection)
            {
                var value = item.Value;
                var regex = new MobileDeviceRegex();
                regex.Name = item.Key;
                regex.DeviceType = value.ContainsKey("device") ? value["device"] : String.Empty;
                regex.Regex = value["regex"];
                regex.Models = new List<DeviceModel>();
                if (value.ContainsKey("models"))
                {
                    var models = value["models"];
                    foreach (var model in models)
                    {
                        regex.Models.Add(new DeviceModel
                        {
                            Model = model["model"],
                            Regex = model["regex"],
                            Device = model.ContainsKey("device") ? model["device"] : String.Empty
                        });
                    }
                }
                regex.Model = value.ContainsKey("model") ? value["model"] : String.Empty;
                yield return regex;
            }
        }
    }
}
