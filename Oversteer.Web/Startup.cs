namespace Oversteer.Web
{
    using System;

    using CloudinaryDotNet;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Oversteer.Data;
    using Oversteer.Data.Models.Users;
    using Oversteer.Data.Seeding;
    using Oversteer.Services.Caches;
    using Oversteer.Services.Cars;
    using Oversteer.Services.CarScraper;
    using Oversteer.Services.Cities;
    using Oversteer.Services.Companies;
    using Oversteer.Services.Companies.Account;
    using Oversteer.Services.Companies.OfferedService;
    using Oversteer.Services.Countries;
    using Oversteer.Services.DateTime;
    using Oversteer.Services.EmailSenders;
    using Oversteer.Services.Feedbacks;
    using Oversteer.Services.Home;
    using Oversteer.Services.Images;
    using Oversteer.Services.Rentals;
    using Oversteer.Services.Statistics;
    using Oversteer.Services.Users;
    using Oversteer.Web.Hubs;
    using Oversteer.Web.ViewModels.Email;

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

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opts.Lockout.MaxFailedAccessAttempts = 3;
            });

            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                });

            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddTransient<ICarsScraperService, CarsScraperService>()
                .AddTransient<ICarsService, CarsService>()
                .AddTransient<IHomeService, HomeService>()
                .AddTransient<ICompaniesService, CompaniesService>()
                .AddTransient<IStatisticsService, StatisticsService>()
                .AddTransient<ICountriesService, CountriesService>()
                .AddTransient<ICitiesService, CitiesService>()
                .AddTransient<IRentingService, RentingService>()
                .AddTransient<ILocationService, LocationService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IImageService, ImageService>()
                .AddTransient<ICompanyAccountService, CompanyAccountService>()
                .AddTransient<IOfferedServicesService, OfferedServicesService>()
                .AddTransient<IFeedbackService, FeedbackService>()
                .AddTransient<ICarCacheService, CarsCacheService>()
                .AddTransient<ICarItemsService, CarItemsService>()
                .AddTransient<IDateTimeParserService, DateTimeParserService>();
            
            //Configure MailKit
            services.AddTransient<IEmailSender, MailKitSender>();
            services.Configure<MailKitEmailSenderOptions>(options =>
            {
                options.HostAddress = this.Configuration["SmtpSettings:Server"];
                options.HostPort = Convert.ToInt32(this.Configuration["SmtpSettings:Port"]);
                options.HostUsername = this.Configuration["SmtpSettings:Username"];
                options.HostPassword = this.Configuration["SmtpSettings:Password"];
                options.SenderEmail = this.Configuration["SmtpSettings:SenderEmail"];
                options.SenderName = this.Configuration["SmtpSettings:SenderName"];
            });

            //Configure Cloudinary
            var cloud = this.Configuration["Cloudinary:CloudifyName"];
            var apiKey = this.Configuration["Cloudinary:CloudifyAPI"];
            var apiSecret = this.Configuration["Cloudinary:CloudifyKey"];
            var cloudinaryAccount = new Account(cloud, apiKey, apiSecret);
            var cloudinary = new Cloudinary(cloudinaryAccount);

            services.AddSingleton(cloudinary);

            services.AddMemoryCache();

            services.AddAutoMapper(typeof(Startup));

            //Configure External Providers
            services.AddAuthentication().AddFacebook(fbOptions =>
            {
                fbOptions.AppId = this.Configuration["Facebook:AppId"];
                fbOptions.AppSecret = this.Configuration["Facebook:AppSecret"];
                fbOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
            });

            services.AddAuthentication().AddGoogle(google =>
            {
                google.ClientId = this.Configuration["Google:ClientId"];
                google.ClientSecret = this.Configuration["Google:ClientSecret"];
            });

            services.AddControllersWithViews(configure =>
            {
                configure.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.AddRazorPages();

            services.AddSignalR();
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

            app.UseStatusCodePagesWithRedirects("/Home/Error{0}");

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "Car Details",
                        pattern: "/Cars/Details/{id}/{information}",
                        defaults: new { controller = "Cars", action = "Details" });

                    endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");

                    endpoints.MapRazorPages();

                    endpoints.MapHub<NotificationHub>("/notify");
                });
        }
    }
}
