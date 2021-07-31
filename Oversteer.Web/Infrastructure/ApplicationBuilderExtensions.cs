namespace Oversteer.Web.Infrastructure
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using Oversteer.Models.Cars;
    using Oversteer.Models.Users;
    using Oversteer.Web.Data;
    using Oversteer.Web.Data.Cars;
    using Oversteer.Web.Dto;

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
            SeedCountries(data);
            SeedCitiesInBulgaria(data);

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

        private static void SeedCountries(ApplicationDbContext data)
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

            data.Countries.AddRange(countries);
            data.SaveChanges();
        }

        private static void SeedCitiesInBulgaria(ApplicationDbContext data)
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

            data.Cities.AddRange(cities);
            data.SaveChanges();
        }
    }
}