namespace SmartCarRentals.Services.Data.AppServices
{
    using System;
    using System.Threading.Tasks;

    using SendGrid;

    using SendGrid.Helpers.Mail;
    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Data.AppServices.Contracts;

    public class SendGridService : IMailService
    {
        private readonly SendGridClient sendGridClient;

        public SendGridService(SendGridClient sendGridClient)
        {
            this.sendGridClient = sendGridClient;
        }

        public async Task<Response> SendEmailAsync(string toEmail, string name, string subject, string content)
        {
            var from = new EmailAddress(GlobalConstants.ApplicationEmail, GlobalConstants.SystemName);
            var to = new EmailAddress(toEmail, name);
            var plainTextContent = content;
            var htmlContent = $"<strong>{content}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await this.sendGridClient.SendEmailAsync(msg);

            return response;
        }

        public async Task<Response> ReceiveEmailAsync(string fromEmail, string name, string subject, string content)
        {
            var from = new EmailAddress(GlobalConstants.ApplicationEmail, name);
            var to = new EmailAddress(GlobalConstants.ApplicationEmail, GlobalConstants.SystemName);
            var plainTextContent = $"Email of sender: {fromEmail} {Environment.NewLine}" +
                                   $"Name: {name}{Environment.NewLine}" +
                                   $"Message:{Environment.NewLine}{content}";
            var htmlContent = $"<strong>{plainTextContent}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await this.sendGridClient.SendEmailAsync(msg);

            return response;
        }
    }
}
