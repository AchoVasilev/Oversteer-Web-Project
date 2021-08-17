namespace Oversteer.Web.Areas.Company.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Companies;
    using Oversteer.Services.Rentals;
    using Oversteer.Web.Controllers;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.ViewModels.Rents;

    public class RentalsController : BaseController
    {
        private readonly IRentingService rentingService;
        private readonly ICompaniesService companiesService;
        private readonly IMapper mapper;

        public RentalsController(IRentingService rentingService, IMapper mapper, ICompaniesService companiesService)
        {
            this.rentingService = rentingService;
            this.mapper = mapper;
            this.companiesService = companiesService;
        }

        [Authorize]
        public async Task<IActionResult> Cancel(string id)
        {
            var userIsCompany = this.companiesService.UserIsCompany(this.User.GetId());

            if (!userIsCompany && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var isCanceled = await this.rentingService.CancelAsync(id);

            if (!isCanceled)
            {
                this.TempData["Message"] = "There was an error and the rent was not canceled successfully. Please try again.";

                return RedirectToAction(nameof(this.All), "Rentals", new { area = "Company" });
            }

            this.TempData["Message"] = "The rent was canceled successfully.";

            return RedirectToAction(nameof(this.All), "Rentals", new { area = "Company" });
        }

        [Authorize]
        public async Task<IActionResult> Finish(string id)
        {
            var userIsCompany = this.companiesService.UserIsCompany(this.User.GetId());

            if (!userIsCompany && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var isFinished = await this.rentingService.FinishAsync(id);

            if (!isFinished)
            {
                this.TempData["Message"] = "There was an error and the rent was not finished successfully. Please try again.";
                return RedirectToAction(nameof(this.All), "Rentals", new { area = "Company" });
            }

            this.TempData["Message"] = "The rent was finished successfully.";

            return RedirectToAction(nameof(this.All), "Rentals", new { area = "Company" });
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var userIsCompany = this.companiesService.UserIsCompany(this.User.GetId());

            if (!userIsCompany && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var isDeleted = await this.rentingService.DeleteAsync(id);

            if (!isDeleted)
            {
                this.TempData["Message"] = "There was an error and the rent was not deleted successfully. Please try again.";

                return RedirectToAction(nameof(this.All), "Rentals", new { area = "Company" });
            }

            this.TempData["Message"] = "The rent was deleted successfully.";

            return RedirectToAction(nameof(this.All), "Rentals", new { area = "Company" });
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var userIsCompany = this.companiesService.UserIsCompany(this.User.GetId());

            if (!userIsCompany && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var rent = await this.rentingService.GetRentByIdAsync(id);

            if (rent is null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "" });
            }

            var dto = this.mapper.Map<RentsDto>(rent);
            var model = this.mapper.Map<EditRentFormModel>(dto);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditRentFormModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                this.TempData["Message"] = "The rent was an error editing the rent. Please try again.";

                return RedirectToAction(nameof(this.All), "Rentals", new { area = "Company" });
            }

            await this.rentingService
                .EditRentAsync(inputModel.Id, inputModel.ClientFirstName, inputModel.ClientLastName,
                                                       inputModel.ClientUserEmail, inputModel.Price);

            this.TempData["Message"] = "The rent was edited successfully.";

            return RedirectToAction(nameof(this.All), "Rentals", new { area = "Company" });
        }

        [Authorize]
        public IActionResult All()
        {
            var userIsCompany = this.companiesService.UserIsCompany(this.User.GetId());

            if (!userIsCompany && this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var orders = this.rentingService.GetAllCompanyRents();

            var viewModels = this.mapper.Map<List<MyRentsViewModel>>(orders);

            return this.View(viewModels);
        }
    }
}
