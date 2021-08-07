namespace Oversteer.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Web.Models;
    using Oversteer.Web.Models.Home;
    using Oversteer.Web.Services.Contracts;

    public class HomeController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IHomeService homeService;

        public HomeController(ICarsService carsService, IHomeService homeService)
        {
            this.carsService = carsService;
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            var cars = this.carsService.GetThreeNewestCars();
            var totalCars = this.homeService.GetTotalCarsCount();

            return this.View(new IndexViewModel()
            {
                TotalCars = totalCars,
                Cars = cars.ToList()
            });
        }

        public IActionResult Privacy()
            => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
