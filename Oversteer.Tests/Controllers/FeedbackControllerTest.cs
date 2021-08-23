namespace Oversteer.Tests.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Data.Models.Users;
    using Oversteer.Services.Feedbacks;
    using Oversteer.Services.Rentals;
    using Oversteer.Tests.Data;
    using Oversteer.Tests.Extensions;
    using Oversteer.Tests.Mock;
    using Oversteer.Web.Controllers;
    using Oversteer.Web.ViewModels.Feedbacks;

    using Xunit;

    public class FeedbackControllerTest
    {
        [Fact]
        public async Task CreateShouldReturnView()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Cars.AddRange(Cars.TenPublicCars);
            data.Users.Add(new ApplicationUser { Id = "12345", UserName = "pesho@pesho.bg" });
            data.Rentals.Add(new Rental { Id = "gosho", UserId = "12345" });

            data.SaveChanges();

            var mockRent = new Mock<IRentingService>();

            var user = GetUser("someValue", "pesho@pesho.bg");

            mockRent.Setup(x => x.IsValidFeedbackRequestAsync("gosho", "pesho@pesho.bg"))
                .ReturnsAsync(true);

            var feedbackService = new FeedbackService(data, mockRent.Object, mapper);

            var feedBackController = new FeedbacksController(feedbackService, mockRent.Object);

            feedBackController.ControllerContext = new ControllerContext();

            feedBackController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var result = await feedBackController.Create("gosho");

            Assert.NotNull(result);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CreateShouldReturnRedirectToActionIfRequestIsFalse()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new ApplicationUser { Id = "12345", UserName = "pesho@pesho.bg" });
            data.Rentals.Add(new Rental { Id = "gosho", UserId = "12345" });

            data.SaveChanges();

            var mockRent = new Mock<IRentingService>();

            mockRent.Setup(x => x.IsValidFeedbackRequestAsync("gosho", "5868"))
                .ReturnsAsync(false);

            var feedbackService = new FeedbackService(data, mockRent.Object, mapper);

            var feedBackController = new FeedbacksController(feedbackService, mockRent.Object);

            var user = GetUser("asd", "pesho");

            feedBackController.ControllerContext = new ControllerContext();

            feedBackController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var result = await feedBackController.Create("gosho");

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task CreateShouldReturnRedirectToActionIfRequestIsValid()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var mockRent = new Mock<IRentingService>();

            mockRent.Setup(x => x.IsValidFeedbackRequestAsync("gosho", "5868"))
                .ReturnsAsync(false);

            var model = new FeedbackInputModel
            {
                RentId = "goshoghosho",
                Comment = "peshopeshopesho",
                Rating = 5
            };

            var mockFeedback = new Mock<IFeedbackService>();

            mockFeedback.Setup(x => x.CreateFeedbackAsync(model.RentId, model.Rating, model.Comment))
                .ReturnsAsync(true);

            var feedBackController = new FeedbacksController(mockFeedback.Object, mockRent.Object);

            var result = await feedBackController.Create(model);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task CreateShouldRedirectToCreateeWithInputId()
        {
            var model = new FeedbackInputModel
            {
                RentId = "goshoghosho",
                Comment = "peshopeshopesho",
                Rating = 5
            };

            var controller = new FeedbacksController(null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");
            controller.ModelState.AddModelError("test", "test");
            var result = await controller.Create(model);

            Assert.NotNull(result);
            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", route.ActionName);
        }

        private static ClaimsPrincipal GetUser(string identifier, string name)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, identifier),
                                        new Claim(ClaimTypes.Name, name)
                                        // other required and custom claims
                                   }, "TestAuthentication"));
            return user;
        }
    }
}
