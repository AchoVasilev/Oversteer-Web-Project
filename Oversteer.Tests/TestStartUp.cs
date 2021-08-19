namespace Oversteer.Tests
{
    using CloudinaryDotNet;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MyTested.AspNetCore.Mvc;

    using Oversteer.Web;

    class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            var cloud = "dpo3vbxnl";
            var apiKey = "713733769727835";
            var apiSecret = "AxitLgW4XE_LhDgvwBWGRSsSjv0";
            var cloudinaryAccount = new Account(cloud, apiKey, apiSecret);
            var cloudinary = new Cloudinary(cloudinaryAccount);

            base.ConfigureServices(services);
            services.ReplaceSingleton<Cloudinary>(cloudinary);
        }
    }
}
