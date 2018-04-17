using SendGrid;
using System.Threading.Tasks;

namespace DevCommerce.Core.CrossCuttingConcerns.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string displayName, string fromEmail, string toEmail, string subject, string message);
    }
}
