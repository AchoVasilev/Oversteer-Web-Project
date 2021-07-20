namespace Oversteer.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Web.Services.Contracts;

    public class GatherCarsController : Controller
    {
        private readonly ICarsScraperService carsScraper;

        public GatherCarsController(ICarsScraperService carsScraper)
        {
            this.carsScraper = carsScraper;
        }

        public async Task<IActionResult> Add()
        {
            await this.carsScraper.PopulateDatabaseWithCarBrandsAndModels();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
