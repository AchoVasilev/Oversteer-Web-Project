namespace Oversteer.Web.Areas.Company.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Web.Controllers;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.ViewModels.Companies;
    using Oversteer.Web.ViewModels.Locations;

    public class CompaniesController : BaseController
    {
        private readonly ICompaniesService companiesService;
        private readonly ILocationService locationService;
        private readonly ICarsService carsService;

        public CompaniesController(ICompaniesService companiesService,
            ILocationService locationService, 
            ICarsService carsService)
        {
            this.companiesService = companiesService;
            this.locationService = locationService;
            this.carsService = carsService;
        }

        [Authorize]
        public IActionResult Create() => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCompanyFormModel model)
        {
            var userId = this.User.GetId();
            var userIsCompany = this.companiesService.UserIsCompany(userId);

            if (userIsCompany)
            {
                return this.RedirectToAction("Add", "Cars", new { area = "" });
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.companiesService.CreateCompanyAsync(model, userId);

            return this.RedirectToAction("Add", "Cars", new { area = "" });
        }

        [Authorize]
        public IActionResult AddLocation() => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddLocation(CreateLocationFormModel model)
        {
            var userId = this.User.GetId();
            var userIsCompany = companiesService.UserIsCompany(userId);
            var companyId = companiesService.GetCurrentCompanyId(userId);

            if (!userIsCompany && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await locationService.AddLocationAsync(companyId, model);

            TempData["Message"] = "Your office/car location was added successfully.";

            return this.RedirectToAction(nameof(this.MyLocations));
        }

        [Authorize]
        public IActionResult MyLocations()
        {
            var companyId = this.companiesService.GetCurrentCompanyId(this.User.GetId());

            if (companyId == 0)
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var locations = this.locationService.GetCompanyLocations(companyId);

            return this.View(locations);
        }

        [Authorize]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var companyId = this.companiesService.GetCurrentCompanyId(this.User.GetId());

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            await this.locationService.DeleteLocationAsync(id);

            TempData["Message"] = "Your office/car location was deleted successfully.";

            return this.RedirectToAction(nameof(MyLocations));
        }

        [Authorize]
        public async Task<IActionResult> EditLocation(int id)
        {
            var companyId = this.companiesService.GetCurrentCompanyId(this.User.GetId());

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var locationIsFromCompany = await this.locationService.LocationIsFromCompanyAsync(companyId, id);

            if (!locationIsFromCompany && !this.User.IsAdmin())
            {
                return this.NotFound();
            }

            var model = await this.locationService.GetCurrentLocationAsync(id);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditLocation(LocationFormModel model)
        {
            var companyId = this.companiesService.GetCurrentCompanyId(this.User.GetId());

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var isEdited = await this.locationService.EditLocationAsync(model.Id, model.CountryId, model.CityName, model.AddressName);

            if (!isEdited)
            {
                TempData["Message"] = "There was an error with editing your office/car location. Please try again";

                return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "" });
            }

            this.TempData["Message"] = "The location/office was edited successfully.";

            return this.RedirectToAction(nameof(this.MyLocations), "Companies", new { area = "Company" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var company = await this.companiesService.DetailsAsync(id);

            return this.View(company);
        }
    }
}
