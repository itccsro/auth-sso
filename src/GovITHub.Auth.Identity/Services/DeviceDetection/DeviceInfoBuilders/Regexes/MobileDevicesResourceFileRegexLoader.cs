using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Reflection;
using System.IO;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.Regexes
{
    public class MobileDevicesResourceFileRegexLoader : IDeviceInfoRegexLoader<MobileDeviceRegex>
    {
        private readonly string _resourceName;

        public MobileDevicesResourceFileRegexLoader(string resourceName)
        {
            _resourceName = resourceName;
        }

        public IEnumerable<MobileDeviceRegex> LoadRegularExpressions()
        {
            var serializer = new DeserializerBuilder()
                .WithNamingConvention(namingConvention: new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();
            var assembly = typeof(DeviceInfoBuilderBase<>).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream(_resourceName))
            using (var input = new StreamReader(stream))
            {
                dynamic collection = serializer.Deserialize(input);
                IEnumerable<MobileDeviceRegex> deviceRegexes = ConvertToDeviceRegex(collection);
                /// HACK: The regex for MicroMax is not well formed and .net framework throws an error
                /// As a workaround, exclude the MicroMax regex from the result until the regex is replaced.
                return deviceRegexes.Where(r => r.Name != "MicroMax");
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
