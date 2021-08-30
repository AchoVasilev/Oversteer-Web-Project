namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Rentals;
    using Oversteer.Web.ViewModels.Rents;

    public class RentalsController : AdministrationController
    {
        private readonly IRentingService rentingService;
        private readonly IMapper mapper;

        public RentalsController(IRentingService rentingService, IMapper mapper)
        {
            this.rentingService = rentingService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Cancel(string id)
        {
            var isCanceled = await this.rentingService.CancelAsync(id);

            if (!isCanceled)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await this.rentingService.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var rent = await this.rentingService.GetRentByIdAsync(id);

            if (rent is null)
            {
                return BadRequest();
            }

            var dto = this.mapper.Map<RentsDto>(rent);
            var model = this.mapper.Map<EditRentFormModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRentFormModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var isEdited = await this.rentingService
                .EditRentAsync(inputModel.Id, inputModel.ClientFirstName, inputModel.ClientLastName,
                                                       inputModel.ClientUserEmail, inputModel.Price);

            if (!isEdited)
            {
                return BadRequest();
            }

            this.TempData["Message"] = "The rent was edited successfully.";

            return RedirectToAction(nameof(this.All), "Rentals", new { area = "Company" });
        }

        public IActionResult All()
        {
            var rents = this.rentingService.GetAllCompaniesRents();

            var viewModels = this.mapper.Map<List<MyRentsViewModel>>(rents);

            return this.View(viewModels);
        }
    }
}
