using Reservea.Common.Mails.Models;
using System.Threading.Tasks;

namespace Reservea.Common.Interfaces
{
    public interface IMailSendingService
    {
        Task SendMailAsync(string to, string toAddress, string subject, string messageContent);
        Task SendMailFromTemplateAsync(string template, BaseMailModel baseMailModel);
    }
}
