namespace Oversteer.Web.Infrastructure
{
    using System.Linq;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Oversteer.Models.Cars;
    using Oversteer.Web.Data;
    using Oversteer.Web.Data.Cars;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            data.Database.Migrate();

            SeedCarTypes(data);
            SeedColors(data);
            SeedFuel(data);
            SeedTransmission(data);

            return app;
        }

        private static void SeedCarTypes(ApplicationDbContext data)
        {
            if (data.CarTypes.Any())
            {
                return;
            }

            data.CarTypes.AddRange(new[]
            {
                new CarType() { Name = "Mini" },
                new CarType() { Name = "Economy" },
                new CarType() { Name = "SUV" },
                new CarType() { Name = "Van" },
                new CarType() { Name = "Midsize" },
                new CarType() { Name = "Large" },
                new CarType() { Name = "Luxury" },
                new CarType() { Name = "Cabrio" },
            });

            data.SaveChanges();
        }

        private static void SeedColors(ApplicationDbContext data)
        {
            if (data.Colors.Any())
            {
                return;
            }

            data.Colors.AddRange(new[]
            {
                new Color() { Name = "White" },
                new Color() { Name = "Black" },
                new Color() { Name = "Gray" },
                new Color() { Name = "Silver" },
                new Color() { Name = "Red" },
                new Color() { Name = "Blue" },
                new Color() { Name = "Brown" },
                new Color() { Name = "Green" },
                new Color() { Name = "Beige" },
                new Color() { Name = "Orange" },
                new Color() { Name = "Gold" },
                new Color() { Name = "Yellow" },
                new Color() { Name = "Purple" },
                new Color() { Name = "Pink" },
            });

            data.SaveChanges();
        }

        private static void SeedFuel(ApplicationDbContext data)
        {
            if (data.Fuels.Any())
            {
                return;
            }

            data.Fuels.AddRange(new[]
            {
                new Fuel() { Name = "Petrol" },
                new Fuel() { Name = "Diesel" },
                new Fuel() { Name = "Methane" },
                new Fuel() { Name = "LPG" },
                new Fuel() { Name = "Electricity" },
                new Fuel() { Name = "Hybrid" },
            });

            data.SaveChanges();
        }

        private static void SeedTransmission(ApplicationDbContext data)
        {
            if (data.Transmissions.Any())
            {
                return;
            }

            data.Transmissions.AddRange(new[]
            {
                new Transmission() { Name = "Manual" },
                new Transmission() { Name = "Automatic" },
                new Transmission() { Name = "Continuously variable transmission" },
                new Transmission() { Name = "Semi-automatic" },
            });

            data.SaveChanges();
        }
    }
}