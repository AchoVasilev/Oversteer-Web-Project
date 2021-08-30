namespace Oversteer.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    using Moq;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Services.DateTime;
    using Oversteer.Services.Rentals;
    using Oversteer.Tests.Extensions;
    using Oversteer.Tests.Mock;
    using Oversteer.Web.Controllers;
    using Oversteer.Web.Hubs;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Rents;

    using Xunit;

    public class RentalsControllerTest
    {
        [Fact]
        public void PreviewShouldReturnViewWithCorrectModelWhenModelStateIsValid()
        {
            var rentalsController = new RentalsController(null, null, null, null, null, null, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho", "pipi");

            var previewModel = new RentPreviewModel()
            {
                CarId = 1,
                CompanyId = 1,
                CompanyName = "Penko",
                Days = 10,
                PricePerDay = 100m,
                Image = "pesho",
                Model = "VW",
                PickUpPlace = "Sofia",
                ReturnPlace = "Kaspichan",
                RentStart = "24/08/2021 23:26",
                RentEnd = "29/08/2021 23:26"
            };

            var result = rentalsController.Preview(previewModel);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<RentPreviewModel>(model);
        }

        [Fact]
        public void PreviewShouldRedirectToHomeIfModelStateIsInvalid()
        {
            var rentalsController = new RentalsController(null, null, null, null, null, null, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho", "pipi");
            rentalsController.ModelState.AddModelError("test", "test");
            var result = rentalsController.Preview(null);

            Assert.NotNull(result);
            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", route.ActionName);
            Assert.Equal("Home", route.ControllerName);
        }

        [Fact]
        public void MyRentsShouldReturnAllUserRents()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.GetAllUserRents("gosho"));

            var rentalsController = new RentalsController(rentsMock.Object, null, null, null, mapper, null, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho", "pipi");

            var result = rentalsController.MyRents();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<List<MyRentsViewModel>>(model);
        }

        [Fact]
        public void InvalidShouldReturnView()
        {
            var rentalsController = new RentalsController(null, null, null, null, null, null, null);

            var result = rentalsController.Invalid();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task DetailsShouldReturnCorrectViewAndModel()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var rent = new Rental()
            {
                Id = "51s",
            };

            data.Rentals.Add(rent);
            data.SaveChanges();

            var expected = new RentDetailsModel();

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.GetDetailsAsync("51s"))
                .ReturnsAsync(expected);
                
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var rentalsController = new RentalsController(rentsMock.Object, null, companiesMock.Object, null, mapper, null, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho", "pipi");

            var result = await rentalsController.Details("51s");

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<RentDetailsModel>(model);
        }

        [Fact]
        public async Task DetailsShouldReturnRedirectToActionIfCompanyIdIsWrong()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var expected = new RentDetailsModel();

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.GetDetailsAsync("51s"));

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"));

            var rentalsController = new RentalsController(rentsMock.Object, null, companiesMock.Object, null, mapper, null, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho", "pipi");

            var result = await rentalsController.Details("51s");

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task OrderShouldRedirectIfUserIsCompany()
        {
            var model = new RentFormModel
            {
                CompanyId = 1,
            };

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentalsController = new RentalsController(null, null, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho", "pipi");

            var result = await rentalsController.Order(model);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task OrderShouldReturnRedirectToActionToMyRentsWhenSuccessful()
        {
            var carModel = new CarDto();
            var model = new RentFormModel
            {
                CompanyId = 1,
                CarId = 1
            };

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetCarByIdAsync(model.CarId))
                .ReturnsAsync(carModel);

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.CreateRentAsync(model, "gosho"))
                .ReturnsAsync(true);

            var hubMock = new Mock<IHubContext<NotificationHub>>();

            Mock<IHubClients> mockClients = new Mock<IHubClients>();
            Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);

            hubMock.Setup(x => x.Clients)
                .Returns(() => mockClients.Object);

            var dateMock = new Mock<IDateTimeParserService>();

            var controller = new RentalsController(rentsMock.Object, carsMock.Object, companiesMock.Object, null, null, hubMock.Object, dateMock.Object);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Order(model);

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("MyRents", route.ActionName);
        }

        [Fact]
        public async Task OrderShouldReturnViewModelIfCarModelIsNull()
        {
            var carModel = new CarDto();
            var model = new RentFormModel
            {
                CompanyId = 1,
                CarId = 1
            };

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetCarByIdAsync(model.CarId));

            var controller = new RentalsController(null, carsMock.Object, companiesMock.Object, null, null, null, null);
            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Order(model);

            Assert.False(controller.ModelState.IsValid);

            var resultModel = Assert.IsType<ViewResult>(result);

            Assert.IsType<RentFormModel>(resultModel.Model);
        }

        [Fact]
        public async Task OrderShouldReturnRedirectToInvalidIfRentIsNotSuccessful()
        {
            var carModel = new CarDto();
            var model = new RentFormModel
            {
                CompanyId = 1,
                CarId = 1
            };

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetCarByIdAsync(model.CarId))
                .ReturnsAsync(carModel);

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.CreateRentAsync(model, "gosho"))
                .ReturnsAsync(false);

            var controller = new RentalsController(rentsMock.Object, carsMock.Object, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Order(model);

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Invalid", route.ActionName);
        }
    }
}
