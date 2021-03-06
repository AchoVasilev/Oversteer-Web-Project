namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;

    public class CarBrandsController : AdministrationController
    {
        private readonly ICarItemsService carItemsService;

        public CarBrandsController(ICarItemsService carItemsService)
        {
            this.carItemsService = carItemsService;
        }

        public IActionResult All()
        {
            var models = this.carItemsService.GetAllBrandsAsync();

            return View(models);
        }

        public IActionResult Edit(int id)
        {
            var model = new CarBrandFormModel()
            {
                Id = id
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarBrandFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var isEdited = await this.carItemsService.EditBrandAsync(model);

            if (!isEdited)
            {
                return BadRequest();
            }

            this.TempData["Message"] = "The brand was edited successfully.";

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Add() => this.View();

        [HttpPost]
        public async Task<IActionResult> Add(CarBrandFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.carItemsService.AddBrandAsync(model);

            this.TempData["Message"] = "The brand was added successfully.";

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await this.carItemsService.DeleteBrandAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            this.TempData["Message"] = "The brand was removed successfully.";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}