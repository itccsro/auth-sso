using System.Threading.Tasks;

namespace GovITHub.Auth.Common.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
