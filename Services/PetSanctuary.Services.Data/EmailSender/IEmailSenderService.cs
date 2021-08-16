namespace PetSanctuary.Services.Data.EmailSender
{
    using System.Threading.Tasks;

    public interface IEmailSenderService
    {
        Task SendEmailAsync(string fromEmail, string toEmail, string subject, string content);
    }
}
