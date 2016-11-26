using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Reflection;
using System.IO;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using System.Text.RegularExpressions;
using GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders.Regexes;

namespace GovITHub.Auth.Identity.Services.DeviceDetection.DeviceInfoBuilders
{
    public class DeviceTypeBuilder : DeviceInfoBuilderBase<DeviceRegex>, IDeviceInfoBuilder
    {
        public DeviceTypeBuilder(IDeviceInfoRegexLoader<DeviceRegex> regexLoader) : base(regexLoader)
        {
        }

        public void Build(DeviceInfo deviceInfo, string userAgent)
        {
            BuildInternal(deviceInfo, userAgent, (di, value) =>
            {
                di.DeviceType = value;
            });
        }

        protected override string BuildDeviceInfo(DeviceRegex regex, Match match, string userAgent)
        {
            var model = regex.Models.Select(m => new
            {
                Match = Regex.Match(userAgent, m.Regex),
                Model = m
            })
            .FirstOrDefault(m => m.Match.Success);
            if (model == null)
            {
                return match.Value;
            }
            var deviceModelName = GetDeviceModelName(model.Model, model.Match);
            return $"{match.Value} {deviceModelName}";
        }

        private static string GetDeviceModelName(DeviceModel deviceModel, Match deviceModelMatch)
        {
            if (deviceModel.Model.Contains("$"))
            {
                var capturingGroups = Regex.Matches(deviceModel.Model, @"\$\d")
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .Aggregate(deviceModel.Model, (modelName, capturingGroup) =>
                    {
                        return modelName.Replace(capturingGroup, deviceModelMatch.GetCapturingGroupValue(capturingGroup));
                    });
                return capturingGroups;
            }
            return deviceModel.Model;
        }
    }
}
