namespace Oversteer.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.Models.Companies;
    using Oversteer.Web.Models.Locations;
    using Oversteer.Web.Services.Contracts;

    public class CompaniesController : Controller
    {
        private readonly ICompaniesService companiesService;
        private readonly ILocationService locationService;

        public CompaniesController(ICompaniesService companiesService, ILocationService locationService)
        {
            this.companiesService = companiesService;
            this.locationService = locationService;
        }

        [Authorize]
        public IActionResult Create() => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCompanyFormModel companyModel)
        {
            var userId = this.User.GetId();
            var userIsCompany = this.companiesService.UserIsCompany(userId);

            if (userIsCompany)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return this.View(companyModel);
            }

            await this.companiesService.CreateCompanyAsync(companyModel, userId);

            return RedirectToAction("Add", "Cars");
        }

        [Authorize]
        public IActionResult AddLocation() => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddLocation(CreateLocationFormModel model)
        {
            var userId = this.User.GetId();
            var userIsCompany = this.companiesService.UserIsCompany(userId);
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (!userIsCompany)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.locationService.AddLocationAsync(companyId, model);

            this.TempData["Message"] = "Your office/car location was added successfully.";

            return RedirectToAction(nameof(this.AddLocation));
        }
    }
}
