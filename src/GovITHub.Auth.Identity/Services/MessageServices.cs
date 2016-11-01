using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PostmarkDotNet;
using System.Threading.Tasks;

namespace GovITHub.Auth.Identity.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        IConfigurationRoot configurationRootService;
        private readonly ILogger<AuthMessageSender> logger;
        public AuthMessageSender(IConfigurationRoot configurationRootService, ILogger<AuthMessageSender> logger)
        {
            this.configurationRootService = configurationRootService;
            this.logger = logger;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var postmarkServerToken = configurationRootService[Config.POSTMARK_SERVER_TOKEN];
            var originEmailAddress = configurationRootService[Config.EMAIL_FROM_ADDRESS];
            if (!string.IsNullOrWhiteSpace(postmarkServerToken))
            {
                var emailMessage = new PostmarkMessage()
                {
                    From = originEmailAddress,
                    To = email,
                    Subject = subject,
                    TextBody = message
                };

                var client = new PostmarkClient("server_token");
                return client.SendMessageAsync(emailMessage);
            }
            else
            {
                logger.LogWarning("Postmark server token is not configured, so we're not able to send emails.");
                return Task.FromResult(0);
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
