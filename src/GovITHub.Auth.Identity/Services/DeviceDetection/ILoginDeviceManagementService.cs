using GovITHub.Auth.Identity.Services.DeviceDetection.DataContracts;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Services.DeviceDetection
{
    public interface ILoginDeviceManagementService
    {
        Task RegisterDeviceLoginAsync(string userId, DeviceInfo deviceInfo);
    }
}
