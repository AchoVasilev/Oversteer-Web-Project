namespace Oversteer.Web.ViewComponents.Home
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Rentals;
    using Oversteer.Web.ViewModels.Home;

    [ViewComponent(Name = "UserOrderBar")]
    public class UserFinishedOrdersBarViewComponent : ViewComponent
    {
        private readonly IRentingService rentingService;

        public UserFinishedOrdersBarViewComponent(IRentingService rentingService)
        {
            this.rentingService = rentingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var orders = await HasOrders();
            return View(orders);
        }

        private async Task<UserFinishedOrdersBarViewModel> HasOrders()
        {
            var model = new UserFinishedOrdersBarViewModel
            {
                HasFinished = await this.rentingService.UserFinishedRentsAsync(this.User.Identity.Name)
            };

            return await Task.FromResult(model);
        }
    }
}
