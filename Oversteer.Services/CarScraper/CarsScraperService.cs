namespace Oversteer.Services.CarScraper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using AngleSharp;

    using Oversteer.Data;
    using Oversteer.Data.Models.Cars;
    using Oversteer.Data.Seeding.Dto;

    public class CarsScraperService : ICarsScraperService
    {
        private readonly IConfiguration config;
        private readonly IBrowsingContext browsingContext;
        private readonly ApplicationDbContext data;

        public CarsScraperService(ApplicationDbContext data)
        {
            config = Configuration.Default.WithDefaultLoader();
            browsingContext = BrowsingContext.New(config);
            this.data = data;
        }

        public async Task PopulateDatabaseWithCarBrandsAndModels()
        {
            if (data.CarBrands.Any())
            {
                return;
            }

            var carBrands = new List<CarBrandDto>();

            var carCount = 1;
            Parallel.For(1, carCount, (i) =>
            {
                try
                {
                    carBrands = this.GetBrand().ToList();
                }
                catch
                { }
            });

            foreach (var brand in carBrands)
            {
                var carBrand = new CarBrand()
                {
                    Name = brand.Name,
                    CarModels = brand.Models.Select(x => new CarModel()
                    {
                        Name = x.Name
                    })
                    .ToList()
                };

                await data.CarBrands.AddAsync(carBrand);
                await data.SaveChangesAsync();
            }
        }

        private IEnumerable<CarBrandDto> GetBrand()
        {
            var brandsUrl = "https://www.carmodelslist.com/car-manufacturers/";

            var brandsDocument = browsingContext.OpenAsync(brandsUrl)
                .GetAwaiter()
                .GetResult();

            if (brandsDocument.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException();
            }

            var allCarBrands = brandsDocument.QuerySelectorAll("#genesis-content > ul > li > a.manulink")
                .Select(x => x.TextContent)
                .ToList();

            var brands = new List<CarBrandDto>();

            var getBrand = "";

            foreach (var documentBrand in allCarBrands)
            {
                var parsedBrand = documentBrand.Contains(" ") ? documentBrand.Replace(" ", "-") : documentBrand;

                if (parsedBrand.Contains("-"))
                {
                    var brandArgs = parsedBrand.Split("-", StringSplitOptions.RemoveEmptyEntries);

                    if (brandArgs[0].Length == 1)
                    {
                        getBrand = string.Join(" ", brandArgs[0], brandArgs[1]);
                    }
                    else if (brandArgs[0] == "The")
                    {
                        getBrand = brandArgs[1];
                    }
                    else
                    {
                        getBrand = brandArgs[0].Contains(":") ? brandArgs[0].Replace(":", "") : brandArgs[0];
                    }
                }

                var modelsUrl = $"https://www.carmodelslist.com/{getBrand}/";

                var modelsDocument = browsingContext.OpenAsync(modelsUrl)
                    .GetAwaiter()
                    .GetResult();

                if (modelsDocument.StatusCode == HttpStatusCode.NotFound)
                {
                    continue;
                }

                var allBrandModels = modelsDocument.QuerySelectorAll("div.godhelp");
                var carModels = new List<CarModelDto>();

                if (allBrandModels == null)
                {
                    allBrandModels = modelsDocument.QuerySelectorAll("#genesis-content > article > div > h");
                }

                foreach (var model in allBrandModels)
                {
                    var parseModel = model.TextContent.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length <= 3;

                    if (!parseModel)
                    {
                        continue;
                    }

                    var carModel = new CarModelDto
                    {
                        Name = model.TextContent
                    };

                    carModels.Add(carModel);
                }

                brands.Add(new CarBrandDto
                {
                    Name = getBrand,
                    Models = carModels
                });
            }

            return brands;
        }
    }
}