namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Rentals;
    using Oversteer.Web.ViewModels.Rents;

    using static Oversteer.Data.Common.Constants.WebConstants;

    [Authorize(Roles = AdministratorRoleName)]
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
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await this.rentingService.DeleteAsync(id);

            if (!isDeleted)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var rent = await this.rentingService.GetRentByIdAsync(id);

            if (rent is null)
            {
                return NotFound();
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
                this.TempData["Message"] = "The rent was an error editing the rent. Please try again.";

                return RedirectToAction(nameof(this.All));
            }

            await this.rentingService
                .EditRentAsync(inputModel.Id, inputModel.ClientFirstName, inputModel.ClientLastName,
                                                       inputModel.ClientUserEmail, inputModel.Price);

            this.TempData["Message"] = "The rent was edited successfully.";

            return RedirectToAction(nameof(this.All), "Rentals", new { area = "Company" });
        }

        public IActionResult All()
        {
            var orders = this.rentingService.GetAllCompanyRents();

            var viewModels = this.mapper.Map<List<MyRentsViewModel>>(orders);

            return this.View(viewModels);
        }
    }
}
