namespace Oversteer.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Services.Home;
    using Oversteer.Web.ViewModels.Home;
    using Oversteer.Web.ViewModels;
    using Microsoft.Extensions.Caching.Memory;
    using System.Collections.Generic;
    using System;

    public class HomeController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IHomeService homeService;
        private readonly ILocationService locationService;
        private readonly IMemoryCache memoryCache;

        public HomeController(
            ICarsService carsService,
            IHomeService homeService,
            ILocationService locationService,
            IMemoryCache memoryCache
            )
        {
            this.carsService = carsService;
            this.homeService = homeService;
            this.locationService = locationService;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            const string latestCarCacheKey = "LatestCarsCacheKey";
            const string totalCarsCacheKey = "TotalCarsCacheKey";

            var cars = this.memoryCache.Get<IEnumerable<CarIndexViewModel>>(latestCarCacheKey);

            if (cars == null)
            {
                cars = this.carsService.GetThreeNewestCars();

                var chacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.memoryCache.Set(latestCarCacheKey, cars, chacheOptions);
            }

            var totalCars = this.memoryCache.Get<int>(totalCarsCacheKey);

            if (totalCars == 0)
            {
                totalCars = this.homeService.GetTotalCarsCount();

                var chacheOptions = new MemoryCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.memoryCache.Set(totalCarsCacheKey, totalCars, chacheOptions);
            }

            return this.View(new IndexViewModel()
            {
                TotalCars = totalCars,
                Cars = cars.ToList(),
                SearchModel = new SearchRentCarModel()
                {
                    Locations = this.locationService.GetAllLocationNames()
                }
            });
        }

        public IActionResult Privacy()
            => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });

        public IActionResult Error404()
        {
            return this.View();
        }
    }
}
