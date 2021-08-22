namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Cities;
    using Oversteer.Web.ViewModels.Cities;

    public class AddressesController : Controller
    {
        private readonly ICitiesService citiesService;

        public AddressesController(ICitiesService citiesService)
        {
            this.citiesService = citiesService;
        }

        public IActionResult Edit(int id)
        {
            var model = new AddressFormModel()
            {
                Id = id
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddressFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var isEdited = await this.citiesService.EditAddressAsync(model.Id, model.Name);

            if (!isEdited)
            {
                return NotFound();
            }

            this.TempData["Message"] = "The city was edited successfully.";

            return this.RedirectToAction(nameof(LocationsController.All), "Locations", new { area = "Administration" });
        }
    }
}
