namespace Oversteer.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Cities;
    using Oversteer.Services.Countries;
    using Oversteer.Web.ViewModels.Countries;

    public class CountriesController : Controller
    {
        private readonly ICountriesService countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        [Authorize]
        public IActionResult Add() 
            => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CountryFormModel model)
        {
            await this.countriesService.AddCitiesToCountry(model);

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
