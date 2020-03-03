using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SmartCarRentals.Web.Areas.Identity.IdentityHostingStartup))]

namespace SmartCarRentals.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}