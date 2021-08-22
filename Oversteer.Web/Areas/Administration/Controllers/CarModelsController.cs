namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Cars;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.ViewModels.Cars;

    public class CarModelsController : AdministrationController
    {
        private readonly ICarItemsService carItemsService;

        public CarModelsController(ICarItemsService carItemsService)
        {
            this.carItemsService = carItemsService;
        }

        public IActionResult All(int id)
        {
            var models = this.carItemsService.GetAllModelsAsync(id);

            return View(models);
        }

        public async Task<IActionResult> Add(int id)
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var brandName = await this.carItemsService.GetBrandNameAsync(id);
            var model = new CarModelFormModel()
            {
                CarBrandName = brandName,
                CarBrandId = id
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarModelFormModel model)
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.carItemsService.AddModelAsync(model);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var isDeleted = await this.carItemsService.DeleteModelAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            this.TempData["Message"] = "The model was removed successfully.";

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Edit(int id)
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var model = new CarModelFormModel()
            {
                Id = id
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarModelFormModel model)
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var isEdited = await this.carItemsService.EditModelAsync(model);

            if (!isEdited)
            {
                return BadRequest();
            }

            this.TempData["Message"] = "The model was edited successfully.";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
