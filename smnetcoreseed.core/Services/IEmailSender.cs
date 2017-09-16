using System.Threading.Tasks;

namespace smnetcoreseed.core.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}