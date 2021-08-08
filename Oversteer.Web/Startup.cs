namespace Oversteer.Web
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Oversteer.Models.Users;
    using Oversteer.Web.Areas.Company.Services.Companies;
    using Oversteer.Web.Data;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.Models.Email;
    using Oversteer.Web.Services;
    using Oversteer.Web.Services.Cars;
    using Oversteer.Web.Services.Cities;
    using Oversteer.Web.Services.Clients;
    using Oversteer.Web.Services.Countries;
    using Oversteer.Web.Services.EmailSenders;
    using Oversteer.Web.Services.Home;
    using Oversteer.Web.Services.Rentals;
    using Oversteer.Web.Services.Statistics;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = true;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequiredUniqueChars = 0;
                    })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<ICarsScraperService, CarsScraperService>()
                .AddTransient<ICarsService, CarsService>()
                .AddTransient<IHomeService, HomeService>()
                .AddTransient<ICompaniesService, CompaniesService>()
                .AddTransient<IStatisticsService, StatisticsService>()
                .AddTransient<ICountriesService, CountriesService>()
                .AddTransient<ICitiesService, CitiesService>()
                .AddTransient<IZipCodesService, ZipCodesService>()
                .AddTransient<IRentingService, RentingService>()
                .AddTransient<ILocationService, LocationService>()
                .AddTransient<IClientsService, ClientsService>();

            services.AddTransient<IEmailSender, MailKitSender>();
            services.Configure<MailKitEmailSenderOptions>(options =>
            {
                options.HostAddress = Configuration["SmtpSettings:Server"];
                options.HostPort = Convert.ToInt32(Configuration["SmtpSettings:Port"]);
                options.HostUsername = Configuration["SmtpSettings:Username"];
                options.HostPassword = Configuration["SmtpSettings:Password"];
                options.SenderEmail = Configuration["SmtpSettings:SenderEmail"];
                options.SenderName = Configuration["SmtpSettings:SenderName"];
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews(configure =>
            {
                configure.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase()
                .GetAwaiter()
                .GetResult();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
        }
    }
}
