namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;
    using Oversteer.Data.Models.Cars;
    using Oversteer.Services.Cars;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;

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
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.carItemsService.AddModelAsync(model);

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

        public IActionResult Edit() => this.View();

        [HttpPost]
        public async Task<IActionResult> Edit(CarModelFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.carItemsService.EditAsync(model);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
