using DevCommerce.Core.Entities.AppSettingsModels;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace DevCommerce.Core.CrossCuttingConcerns.Email
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; }

        public Task SendEmailAsync(string displayName, string fromEmail, string toEmail, string subject, string message)
        {
            return Execute(Options.SendGridKey, displayName, fromEmail, toEmail, subject, message);
        }

        public async Task<Response> Execute(string apiKey, string displayName, string fromEmail, string toEmail, string subject, string message)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, displayName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));
            var result = await client.SendEmailAsync(msg);
            return result;
        }
    }
}
