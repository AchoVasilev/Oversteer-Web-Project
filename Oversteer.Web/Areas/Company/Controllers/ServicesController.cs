namespace Oversteer.Web.Areas.Company.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Companies;
    using Oversteer.Services.Companies.OfferedService;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.ViewModels.Companies;

    public class ServicesController : BaseController
    {
        private readonly IOfferedServicesService offeredServices;
        private readonly ICompaniesService companiesService;

        public ServicesController(IOfferedServicesService offeredServices, ICompaniesService companiesService)
        {
            this.offeredServices = offeredServices;
            this.companiesService = companiesService;
        }

        [Authorize]
        public IActionResult MyServices()
        {
            var userId = this.User.GetId();
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var services = this.offeredServices.GetAll(companyId);

            return this.View(services);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.GetId();
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var hasService = await this.offeredServices.CompanyHasServiceAsync(id, companyId);

            if (!hasService)
            {
                return BadRequest();
            }

            var isDeleted = await this.offeredServices.DeleteServiceAsync(id);

            if (!isDeleted)
            {
                this.TempData["Message"] = "The service was not deleted. Please try again.";
                return RedirectToAction(nameof(this.MyServices), "Services", new { area = "Company" });
            }

            this.TempData["Message"] = "The service was deleted successfully.";

            return RedirectToAction(nameof(this.MyServices), "Services", new { area = "Company" });
        }

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.GetId();
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CompanyServiceFormModel model)
        {
            var userId = this.User.GetId();
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            await this.offeredServices.AddServicesAsync(model, companyId);

            return RedirectToAction(nameof(this.MyServices), "Services", new { area = "Company" });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.User.GetId();
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var service = await this.offeredServices.GetServiceAsync(id, companyId);

            if (service == null)
            {
                this.TempData["Message"] = $"The service was not found.";

                return RedirectToAction(nameof(this.MyServices), "Services", new { area = "Company" });
            }

            return this.View(service);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CompanyServiceFormModel model)
        {
            var userId = this.User.GetId();
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            await this.offeredServices.AddServicesAsync(model, companyId);

            return RedirectToAction(nameof(this.MyServices), "Services", new { area = "Company" });
        }
    }
}
