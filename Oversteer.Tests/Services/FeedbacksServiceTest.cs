namespace Oversteer.Tests.Services
{
    using System;
    using System.Linq;

    using Moq;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Data.Models.Users;
    using Oversteer.Services.Feedbacks;
    using Oversteer.Services.Rentals;
    using Oversteer.Tests.Mocks;

    using Xunit;

    public class FeedbacksServiceTest
    {
        [Fact]
        public void CreateShouldReturnTrueIfSuccessfull()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new ApplicationUser());
            var order = new Rental { Id = "peshopeshopesho" };
            data.Rentals.Add(order);
            data.SaveChanges();

            var random = new Random();
            var feedbackRating = random.Next(1, 5);
            var feedbackComment = Guid.NewGuid().ToString();

            var ordersServiceMock = new Mock<IRentingService>();
            ordersServiceMock.Setup(x => x.DeleteReviewFromOrderAsync(It.IsAny<int>())).
                                    ReturnsAsync(true);

            var feedbackService = new FeedbackService(data, ordersServiceMock.Object, mapper);

            Assert.False(data.Feedbacks.Any());

            var isCreated = feedbackService
                .CreateFeedbackAsync("peshopeshopesho", feedbackRating, feedbackComment)
                .GetAwaiter()
                .GetResult();

            Assert.True(order.Feedback != null);
        }

        [Fact]
        public void CreateShouldReturnFalseIfOrderIdIsInvalid()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new ApplicationUser());
            var order = new Rental { Id = "peshopeshopesho" };
            data.Rentals.Add(order);
            data.SaveChanges();

            var random = new Random();
            var feedbackRating = random.Next(1, 5);
            var feedbackComment = Guid.NewGuid().ToString();

            var ordersServiceMock = new Mock<IRentingService>();
            ordersServiceMock.Setup(x => x.DeleteReviewFromOrderAsync(It.IsAny<int>())).
                                    ReturnsAsync(true);

            var feedbackService = new FeedbackService(data, ordersServiceMock.Object, mapper);

            Assert.False(data.Feedbacks.Any());

            var isCreated = feedbackService
                .CreateFeedbackAsync("12345", feedbackRating, feedbackComment)
                .GetAwaiter()
                .GetResult();

            Assert.False(isCreated);
        }

        [Fact]
        public void DeleteFeedbackShouldReturnTrueWhenSuccessful()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new ApplicationUser());
            var rent = new Rental { Id = "peshopeshopesho" };
            data.Rentals.Add(rent);

            var random = new Random();
            var feedbackRating = random.Next(1, 5);
            var feedbackComment = Guid.NewGuid().ToString();

            var rentingService = new Mock<IRentingService>();
            rentingService.Setup(x => x.DeleteReviewFromOrderAsync(It.IsAny<int>())).
                                    ReturnsAsync(true);

            var feedbackService = new FeedbackService(data, rentingService.Object, mapper);

            Assert.False(data.Feedbacks.Any());

            var create = feedbackService
                .CreateFeedbackAsync("peshopeshopesho", feedbackRating, feedbackComment)
                .GetAwaiter()
                .GetResult();

            data.SaveChanges();

            Assert.True(rent.Feedback != null);

            feedbackService.DeleteFeedbackAsync((int)rent.FeedbackId).GetAwaiter().GetResult();

            var result = data.Rentals.FirstOrDefault(x => x.Id == rent.Id).Feedback == null;

            Assert.True(result);
        }

        [Fact]
        public void DeleteFeedbackShouldReturnFalseWhenFeedbackIdIsInvalid()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new ApplicationUser());
            var rent = new Rental { Id = "peshopeshopesho" };
            data.Rentals.Add(rent);

            var random = new Random();
            var feedbackRating = random.Next(1, 5);
            var feedbackComment = Guid.NewGuid().ToString();

            var rentingService = new Mock<IRentingService>();
            rentingService.Setup(x => x.DeleteReviewFromOrderAsync(It.IsAny<int>())).
                                    ReturnsAsync(true);

            var feedbackService = new FeedbackService(data, rentingService.Object, mapper);

            Assert.False(data.Feedbacks.Any());

            var create = feedbackService
                .CreateFeedbackAsync("peshopeshopesho", feedbackRating, feedbackComment)
                .GetAwaiter()
                .GetResult();

            data.SaveChanges();

            var result = feedbackService.DeleteFeedbackAsync(12345).GetAwaiter().GetResult();

            Assert.False(result);
        }
    }
}
