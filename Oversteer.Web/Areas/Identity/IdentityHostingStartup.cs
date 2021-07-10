using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Oversteer.Web.Areas.Identity.IdentityHostingStartup))]

namespace Oversteer.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
            => builder.ConfigureServices((context, services) =>
        {
        });
    }
}