namespace Oversteer.Tests
{
    using CloudinaryDotNet;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MyTested.AspNetCore.Mvc;

    using Oversteer.Web;

    public class TestStartUp : Startup
    {
        public TestStartUp(IConfiguration configuration)
             : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            var cloud = this.Configuration["Cloudinary:CloudifyName"];
            var apiKey = this.Configuration["Cloudinary:CloudifyAPI"];
            var apiSecret = this.Configuration["Cloudinary:CloudifyKey"];
            var cloudinaryAccount = new Account(cloud, apiKey, apiSecret);
            var cloudinary = new Cloudinary(cloudinaryAccount);

            services.ReplaceSingleton<Cloudinary>(cloudinary);
            base.ConfigureServices(services);
        }
    }
}
