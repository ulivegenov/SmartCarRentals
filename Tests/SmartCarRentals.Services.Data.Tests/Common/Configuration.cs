namespace SmartCarRentals.Services.Data.Tests.Common
{
    using Microsoft.Extensions.Configuration;

    public class Configuration
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}
