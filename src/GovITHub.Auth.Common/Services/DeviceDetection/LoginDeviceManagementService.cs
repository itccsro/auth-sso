using System;
using System.Linq;
using System.Threading.Tasks;
using GovITHub.Auth.Common.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Common.Data;

namespace GovITHub.Auth.Common.Services.DeviceDetection
{
    public class LoginDeviceManagementService : ILoginDeviceManagementService
    {
        private readonly ApplicationDbContext _dbContext;

        public LoginDeviceManagementService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RegisterDeviceLoginAsync(string userId, DeviceInfo deviceInfo)
        {
            var device = _dbContext.Set<LoginDevice>().SingleOrDefault(d => d.UserAgent == deviceInfo.UserAgent);
            if (device == null)
            {
                device = new LoginDevice
                {
                    Browser = deviceInfo.Browser,
                    Id = Guid.NewGuid().ToString(),
                    MobileDevice = deviceInfo.MobileDevice,
                    OperatingSystem = deviceInfo.OperatingSystem,
                    UserAgent = deviceInfo.UserAgent
                };
                _dbContext.Set<LoginDevice>().Add(device);
            }
            var userDevice = _dbContext.Set<UserLoginDevice>().SingleOrDefault(d => d.UserId == userId && d.DeviceId == device.Id);
            if (userDevice == null)
            {
                userDevice = new UserLoginDevice
                {
                    DeviceId = device.Id,
                    RegistrationTimeUtc = DateTime.UtcNow,
                    UserId = userId
                };
                _dbContext.Add(userDevice);
            }
            userDevice.LastLoginTimeUtc = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }
    }
}
