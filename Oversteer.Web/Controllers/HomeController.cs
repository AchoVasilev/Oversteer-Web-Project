namespace Oversteer.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Services.Home;
    using Oversteer.Services.Rentals;
    using Oversteer.Web.ViewModels;
    using Oversteer.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IHomeService homeService;
        private readonly ILocationService locationService;
        private readonly IRentingService rentingService;

        public HomeController(
            ICarsService carsService,
            IHomeService homeService,
            ILocationService locationService,
            IRentingService rentingService)
        {
            this.carsService = carsService;
            this.homeService = homeService;
            this.locationService = locationService;
            this.rentingService = rentingService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = this.carsService.GetThreeNewestCars();
            var totalCars = this.homeService.GetTotalCarsCount();

            this.ViewData["FinishedOrders"] = await this.rentingService.UserFinishedRentsAsync(this.User.Identity.Name);

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

        [AllowAnonymous]
        public IActionResult Error()
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return this.View(model);
        }

        public IActionResult Error404()
        {
            return this.View();
        }
    }
}
