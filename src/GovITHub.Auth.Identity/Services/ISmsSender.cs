using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
