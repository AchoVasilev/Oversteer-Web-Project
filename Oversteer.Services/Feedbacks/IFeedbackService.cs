namespace Oversteer.Services.Feedbacks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Feedbacks;

    public interface IFeedbackService
    {
        Task<bool> CreateFeedbackAsync(string orderId, int rating, string comment);

        Task<bool> DeleteFeedbackAsync(int feedbackId);

        ICollection<ListFeedbackModel> GetAllFeedbacks();
    }
}
