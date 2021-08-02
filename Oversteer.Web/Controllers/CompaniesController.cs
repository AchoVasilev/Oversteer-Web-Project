﻿namespace Oversteer.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.Models.Companies;
    using Oversteer.Web.Services.Contracts;

    public class CompaniesController : Controller
    {
        private readonly ICompaniesService companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            this.companiesService = companiesService;
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

            return RedirectToAction("All", "Cars");
        }
    }
}
