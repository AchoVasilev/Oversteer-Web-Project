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

    public class HomeController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IHomeService homeService;
        private readonly ILocationService locationService;

        public HomeController(ICarsService carsService, IHomeService homeService, ILocationService locationService)
        {
            this.carsService = carsService;
            this.homeService = homeService;
            this.locationService = locationService;
        }

        public IActionResult Index()
        {
            var cars = this.carsService.GetThreeNewestCars();
            var totalCars = this.homeService.GetTotalCarsCount();

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
    }
}
