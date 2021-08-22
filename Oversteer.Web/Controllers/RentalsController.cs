namespace Oversteer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Services.DateTime;
    using Oversteer.Services.Rentals;
    using Oversteer.Web.Hubs;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.ViewModels.Rents;

    using static Oversteer.Data.Common.Constants.ErrorMessages;
    using static Oversteer.Data.Common.Constants.WebConstants;
    using static Oversteer.Data.Common.Constants.WebConstants.SignalR;

    public class RentalsController : Controller
    {
        private readonly IRentingService rentingService;
        private readonly ICarsService carsService;
        private readonly ICompaniesService companiesService;
        private readonly ILocationService locationService;
        private readonly IMapper mapper;
        private readonly IHubContext<NotificationHub> notificationHub;
        private readonly IDateTimeParserService dateTimeParserService;

        public RentalsController(
            IRentingService rentingService,
            ICarsService carsService,
            ICompaniesService companiesService,
            ILocationService locationService,
            IMapper mapper,
            IHubContext<NotificationHub> notificationHub, 
            IDateTimeParserService dateTimeParserService)
        {
            this.rentingService = rentingService;
            this.carsService = carsService;
            this.companiesService = companiesService;
            this.locationService = locationService;
            this.mapper = mapper;
            this.notificationHub = notificationHub;
            this.dateTimeParserService = dateTimeParserService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Order(RentFormModel model)
        {
            var userId = this.User.GetId();

            var isCompany = this.companiesService.UserIsCompany(userId);

            if (isCompany)
            {
                return RedirectToAction("Register", "Account", new { area = "Identity" });
            }

            var carModel = await this.carsService.GetCarByIdAsync(model.CarId);

            if (carModel == null)
            {
                this.ModelState.AddModelError(nameof(model.CarId), CarDoesntExist);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.rentingService.CreateOrderAsync(model, userId);

            if (!result)
            {
                return RedirectToAction(nameof(Invalid));
            }

            var pickUpDate = this.dateTimeParserService.Parse(model.StartDate);

            var returnDate = this.dateTimeParserService.Parse(model.EndDate);

            var days = (returnDate - pickUpDate).Days;

            var message = string.Format(SignalRMessageForNewOrder, carModel.ModelName, days);

            await this.notificationHub.Clients.All.SendAsync(SignalRMethodNewOrder, message);

            return RedirectToAction(nameof(MyRents));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Preview(RentPreviewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return this.View(inputModel);
        }

        [Authorize]
        public IActionResult MyRents()
        {
            var userId = this.User.GetId();

            var orders = this.rentingService.GetAllUserRents(userId);
            var viewModels = this.mapper.Map<List<MyRentsViewModel>>(orders);

            return this.View(viewModels);
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var userId = this.User.GetId();
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (!this.User.IsInRole(AdministratorRoleName) && userId == null && companyId == 0)
            {
                return this.RedirectToAction(nameof(CarsController.All), "Cars");
            }

            var model = await this.rentingService.GetDetailsAsync(id);

            if (model is null)
            {
                return this.RedirectToAction(nameof(this.MyRents));
            }

            return this.View(model);
        }

        [Authorize]
        public IActionResult Invalid()
        {
            return this.View();
        }
    }
}
