using DevCommerce.Core.CrossCuttingConcerns.Email;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DevCommerce.WebApi.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string displayName, string fromEmail, string toEmail, string subject, string message )
        {
            return emailSender.SendEmailAsync(displayName, fromEmail, toEmail, subject, $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(message)}'>link</a>");
        }
    }
}
