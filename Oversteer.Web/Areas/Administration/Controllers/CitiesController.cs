namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Cities;
    using Oversteer.Web.ViewModels.Cities;

    public class CitiesController : AdministrationController
    {
        private readonly ICitiesService citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            this.citiesService = citiesService;
        }

        public IActionResult Edit(int id)
        {
            var model = new CityFormModel()
            {
                Id = id
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CityFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var isEdited = await this.citiesService.EditCityAsync(model.Id, model.Name);

            if (!isEdited)
            {
                return NotFound();
            }

            this.TempData["Message"] = "The address was edited successfully.";

            return this.RedirectToAction(nameof(LocationsController.All), "Locations", new { area = "Administration" });
        }
    }
}
