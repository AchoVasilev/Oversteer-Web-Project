namespace Oversteer.Web.Areas.Company.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Web.ViewModels.Companies;

    public class CarsController : BaseController
    {
        private readonly ICarsService carsService;
        private readonly ICompaniesService companiesService;

        public CarsController(ICarsService carsService, ICompaniesService companiesService)
        {
            this.carsService = carsService;
            this.companiesService = companiesService;
        }

        public IActionResult Cars(int id)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;
            var pageNumber = 1;

            var viewModel = new CompanyCar()
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageNumber,
                Cars = this.companiesService.AllCompanyCars(pageNumber, ItemsPerPage, id),
                CompanyId = id,
                ItemsCount = this.carsService.GetCompanyCarsCount(id)
            };

            return this.View(viewModel);
        }
    }
}
