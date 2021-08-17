namespace PetSanctuary.Services.Data.EmailSender
{
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string fromEmail, string toEmail, string subject, string content)
        {
            var apiKey = this.configuration["SendGridApi:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail);
            var to = new EmailAddress(toEmail);
            var htmlContent = new StringBuilder();

            htmlContent.AppendLine($"<h1>{subject} 🐾</h1>");
            htmlContent.AppendLine($"<p>{content}</p>");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent.ToString());
            await client.SendEmailAsync(msg);
        }
    }
}
