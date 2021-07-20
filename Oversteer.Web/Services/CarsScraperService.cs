namespace Oversteer.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using AngleSharp;

    using Oversteer.Models.Cars;
    using Oversteer.Web.Data;
    using Oversteer.Web.Data.Cars;
    using Oversteer.Web.Dto;
    using Oversteer.Web.Services.Contracts;

    public class CarsScraperService : ICarsScraperService
    {
        private readonly IConfiguration config;
        private readonly IBrowsingContext browsingContext;
        private readonly ApplicationDbContext data;

        public CarsScraperService(ApplicationDbContext data)
        {
            this.config = Configuration.Default.WithDefaultLoader();
            this.browsingContext = BrowsingContext.New(this.config);
            this.data = data;
        }

        public async Task PopulateDatabaseWithCarBrandsAndModels()
        {
            if (this.data.CarBrands.Any())
            {
                return;
            }

            var carBrands = new List<CarBrandDto>();

            try
            {
                carBrands = this.GetBrand().ToList();
            }
            catch
            { }
            
            Parallel.For(1, carBrands.Count, (i) =>
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

                await this.data.CarBrands.AddAsync(carBrand);
                await this.data.SaveChangesAsync();
            }
        }

        private IEnumerable<CarBrandDto> GetBrand()
        {
            var brandsUrl = "https://www.carmodelslist.com/car-manufacturers/";

            var brandsDocument = this.browsingContext.OpenAsync(brandsUrl)
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

                var modelsDocument = this.browsingContext.OpenAsync(modelsUrl)
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