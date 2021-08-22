namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Countries;
    using Oversteer.Web.ViewModels.Countries;

    public class CountriesController : AdministrationController
    {
        private readonly ICountriesService countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IActionResult All()
        {
            var models = this.countriesService.GetCountries();

            return View(models);
        }

        public IActionResult Edit(int id)
        {
            var model = new CountryViewModel()
            {
                Id = id
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var isEdited = await this.countriesService.EditCountry(model.Id, model.Name);

            if (!isEdited)
            {
                return NotFound();
            }

            this.TempData["Message"] = "The country was edited successfully.";

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Add() => this.View();

        [HttpPost]
        public async Task<IActionResult> Add(CountryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.countriesService.AddCountry(model);

            this.TempData["Message"] = "The country was added successfully.";

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await this.countriesService.DeleteCountry(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            this.TempData["Message"] = "The country was removed successfully.";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
