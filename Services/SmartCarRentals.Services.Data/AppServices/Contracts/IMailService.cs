namespace SmartCarRentals.Services.Data.AppServices.Contracts
{
    using System.Threading.Tasks;

    using SendGrid;

    public interface IMailService
    {
        Task<Response> SendEmailAsync(string toEmail, string name, string subject, string content);

        Task<Response> ReceiveEmailAsync(string fromEmail, string name, string subject, string content);
    }
}
