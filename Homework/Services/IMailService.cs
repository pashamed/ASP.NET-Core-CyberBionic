using System.Threading.Tasks;
using Homework.Models;

namespace Homework.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
