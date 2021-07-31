using System;
using System.Threading.Tasks;
using Service.Helpers;

namespace Service.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailMessage message);
    }
}
