namespace Oversteer.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Feedbacks;
    using Oversteer.Services.Rentals;
    using Oversteer.Web.ViewModels.Feedbacks;

    public class FeedbacksController : Controller
    {
        private readonly IFeedbackService feedbackService;
        private readonly IRentingService rentingService;

        public FeedbacksController(IFeedbackService feedbackService, IRentingService rentingService)
        {
            this.feedbackService = feedbackService;
            this.rentingService = rentingService;
        }

        [Authorize]
        public IActionResult Create(string orderId)
        {
            bool isValidRequest = this.rentingService
                .IsValidReviewRequestAsync(orderId, this.User.Identity.Name.ToLower())
                .GetAwaiter()
                .GetResult();

            if (!isValidRequest)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ViewData["Order"] = orderId;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(FeedbackInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create), new { rentId = inputModel.RentId });
            }

            await this.feedbackService.CreateFeedbackAsync(inputModel.RentId, inputModel.Rating, inputModel.Comment);

            return RedirectToAction(nameof(RentalsController.MyRents), "Rentals");
        }
    }
}
