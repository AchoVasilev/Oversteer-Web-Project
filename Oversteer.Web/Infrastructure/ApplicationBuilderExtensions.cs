namespace Oversteer.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using Oversteer.Models.Cars;
    using Oversteer.Models.Users;
    using Oversteer.Web.Data;
    using Oversteer.Web.Data.Cars;
    using Oversteer.Web.Dto;

    using static Oversteer.Web.Data.Constants.WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedServices.ServiceProvider;
            var data = serviceProvider.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();

            await SeedCarTypesAsync(data);
            await SeedColorsAsync(data);
            await SeedFuelAsync(data);
            await SeedTransmissionAsync(data);

            await SeedCountriesAsync(data);
            await SeedCitiesInBulgariaAsync(data);

            await SeedAdministratorAsync(serviceProvider);

            return app;
        }

        private static async Task SeedCarTypesAsync(ApplicationDbContext data)
        {
            if (data.CarTypes.Any())
            {
                return;
            }

            await data.CarTypes.AddRangeAsync(new[]
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

            await data.SaveChangesAsync();
        }

        private static async Task SeedColorsAsync(ApplicationDbContext data)
        {
            if (data.Colors.Any())
            {
                return;
            }

            await data.Colors.AddRangeAsync(new[]
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

            await data.SaveChangesAsync();
        }

        private static async Task SeedFuelAsync(ApplicationDbContext data)
        {
            if (data.Fuels.Any())
            {
                return;
            }

            await data.Fuels.AddRangeAsync(new[]
            {
                new Fuel() { Name = "Petrol" },
                new Fuel() { Name = "Diesel" },
                new Fuel() { Name = "Methane" },
                new Fuel() { Name = "LPG" },
                new Fuel() { Name = "Electricity" },
                new Fuel() { Name = "Hybrid" },
            });

            await data.SaveChangesAsync();
        }

        private static async Task SeedTransmissionAsync(ApplicationDbContext data)
        {
            if (data.Transmissions.Any())
            {
                return;
            }

            await data.Transmissions.AddRangeAsync(new[]
            {
                new Transmission() { Name = "Manual" },
                new Transmission() { Name = "Automatic" },
                new Transmission() { Name = "Continuously variable transmission" },
                new Transmission() { Name = "Semi-automatic" },
            });

            await data.SaveChangesAsync();
        }

        private static async Task SeedCountriesAsync(ApplicationDbContext data)
        {
            if (data.Countries.Any())
            {
                return;
            }

            var countries = new List<Country>();
            var countriesJson = File.ReadAllText("Datasets/Countries.json");
            var countriesDto = JsonConvert.DeserializeObject<CountryDto[]>(countriesJson);

            foreach (var countryDto in countriesDto)
            {
                var country = new Country()
                {
                    Name = countryDto.Name
                };

                countries.Add(country);
            }

            await data.Countries.AddRangeAsync(countries);
            await data.SaveChangesAsync();
        }

        private static async Task SeedCitiesInBulgariaAsync(ApplicationDbContext data)
        {
            var countryBulgariaId = data.Countries
                .Where(x => x.Name == "Bulgaria")
                .Select(x => x.Id)
                .FirstOrDefault();

            var citiesJson = File.ReadAllText("Datasets/Cities.json");
            var citiesDto = JsonConvert.DeserializeObject<CityDto[]>(citiesJson);
            var cities = new List<City>();

            foreach (var cityDto in citiesDto)
            {
                var city = new City()
                {
                    Name = cityDto.Town,
                    CountryId = countryBulgariaId
                };

                cities.Add(city);
            }

            await data .Cities.AddRangeAsync(cities);
            await data.SaveChangesAsync();
        }

        private static async Task SeedAdministratorAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (await roleManager.RoleExistsAsync(AdministratorRoleName))
            {
                return;
            }

            var identityRole = new IdentityRole()
            {
                Name = AdministratorRoleName
            };

            await roleManager.CreateAsync(identityRole);

            const string adminEmail = "oversteer@abv.bg";
            const string adminPassword = "administrator123";

            var adminUser = new ApplicationUser()
            {
                Email = adminEmail,
                UserName = adminEmail,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRoleAsync(adminUser, identityRole.Name);
        }
    }
}