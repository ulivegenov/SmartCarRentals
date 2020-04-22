namespace SmartCarRentals.Services.Data.Tests.AppServices
{
    using System.Threading.Tasks;

    using SendGrid;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Data.AppServices;
    using SmartCarRentals.Services.Data.Tests.Common;

    using Xunit;

    public class SendGridServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Fact]
        public async Task SendEmailAsync_ShouldReturnCorrectResponse()
        {
            var configuration = Configuration.InitConfiguration();
            var sendgridApiKey = configuration["Sendgrid:ApiKey"];
            var options = new SendGridClientOptions();
            options.ApiKey = sendgridApiKey;
            var sendGridClient = new SendGridClient(options.ApiKey);
            var sendGridService = new SendGridService(sendGridClient);

            var email = GlobalConstants.ApplicationEmail;
            var name = "Name";
            var subject = "subject";
            var content = "Some content";
            var response = await sendGridService.SendEmailAsync(email, name, subject, content);

            Assert.True(response.StatusCode.ToString() == "Accepted", ErrorMessage);
        }

        [Fact]
        public async Task ReceiveEmailAsync_ShouldReturnCorrectResponse()
        {
            var configuration = Configuration.InitConfiguration();
            var sendgridApiKey = configuration["Sendgrid:ApiKey"];
            var options = new SendGridClientOptions();
            options.ApiKey = sendgridApiKey;
            var sendGridClient = new SendGridClient(options.ApiKey);
            var sendGridService = new SendGridService(sendGridClient);

            var email = GlobalConstants.ApplicationEmail;
            var name = "Name";
            var subject = "subject";
            var content = "Some content";
            var response = await sendGridService.ReceiveEmailAsync(email, name, subject, content);

            Assert.True(response.StatusCode.ToString() == "Accepted", ErrorMessage);
        }
    }
}
