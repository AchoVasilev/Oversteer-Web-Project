namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Feedbacks;

    using static Oversteer.Data.Common.Constants.WebConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class FeedbacksController : AdministrationController
    {
        private readonly IFeedbackService feedbackService;
        private readonly IMapper mapper;

        public FeedbacksController(IFeedbackService feedbackService, IMapper mapper)
        {
            this.feedbackService = feedbackService;
            this.mapper = mapper;
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
                return RedirectToAction(nameof(this.All), "Feedbacks");
            }

            return RedirectToAction(nameof(All));
        }
    }
}
