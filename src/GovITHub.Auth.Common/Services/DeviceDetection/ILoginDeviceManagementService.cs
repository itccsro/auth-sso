using GovITHub.Auth.Common.Services.DeviceDetection.DataContracts;
using System.Threading.Tasks;

namespace GovITHub.Auth.Common.Services.DeviceDetection
{
    public interface ILoginDeviceManagementService
    {
        Task RegisterDeviceLoginAsync(string userId, DeviceInfo deviceInfo);
    }
}
