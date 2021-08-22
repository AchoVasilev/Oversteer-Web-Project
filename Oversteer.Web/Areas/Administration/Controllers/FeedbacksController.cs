namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Feedbacks;

    public class FeedbacksController : AdministrationController
    {
        private readonly IFeedbackService feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        public IActionResult All()
        {
            var viewModels = this.feedbackService.GetAllFeedbacks();

            return this.View(viewModels);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await this.feedbackService.DeleteFeedbackAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
