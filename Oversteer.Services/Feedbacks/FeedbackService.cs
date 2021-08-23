namespace Oversteer.Services.Feedbacks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Oversteer.Data;
    using Oversteer.Data.Models.Others;
    using Oversteer.Services.Rentals;
    using Oversteer.Web.ViewModels.Feedbacks;

    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext data;
        private readonly IRentingService rentingService;
        private readonly IMapper mapper;

        public FeedbackService(ApplicationDbContext data, IRentingService rentingService, IMapper mapper)
        {
            this.data = data;
            this.rentingService = rentingService;
            this.mapper = mapper;
        }

        public async Task<bool> CreateFeedbackAsync(string orderId, int rating, string comment)
        {
            var rental = await this.data.Rentals.FindAsync(orderId);

            if (rental is null)
            {
                return false;
            }

            rental.Feedback = new Feedback
            {
                UserId = rental.UserId,
                CompanyId = rental.CompanyId,
                Comment = comment,
                Rating = rating
            };

            await this.data.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFeedbackAsync(int feedbackId)
        {
            var feedback = this.data.Feedbacks.Find(feedbackId);

            if (feedback is null)
            {
                return false;
            }

            await this.rentingService.DeleteFeedbackFromRentAsync(feedbackId);

            this.data.Feedbacks.Remove(feedback);

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<ListFeedbackModel> GetAllFeedbacks() 
            => this.data.Feedbacks
                .Where(x => !x.IsDeleted)
                .ProjectTo<ListFeedbackModel>(this.mapper.ConfigurationProvider)
                .ToList();
    }
}
