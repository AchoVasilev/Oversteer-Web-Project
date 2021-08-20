namespace Oversteer.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using Moq;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Services.Companies;
    using Oversteer.Services.Rentals;
    using Oversteer.Tests.Extensions;
    using Oversteer.Tests.Mocks;
    using Oversteer.Web.Areas.Company.Controllers;
    using Oversteer.Web.ViewModels.Rents;

    using Xunit;

    public class CompanyRentalsControllerTest
    {
        [Fact]
        public async Task CancelShouldRedirectToRentalsAllIfResultIsSuccessful()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentingMock = new Mock<IRentingService>();
            rentingMock.Setup(x => x.CancelAsync("asd"))
                .ReturnsAsync(true);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var rentalsController = new RentalsController(rentingMock.Object, null, companiesMock.Object);

            rentalsController.TempData = tempData;

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Cancel("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Cancel("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("All", routes.ActionName);
            Assert.Equal("Rentals", routes.ControllerName);
        }

        [Fact]
        public async Task CancelShouldRedirectToComapniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var rentalsController = new RentalsController(null, null, companiesMock.Object);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";
            rentalsController.TempData = tempData;

            var result = await rentalsController.Cancel("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Cancel("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public async Task CancelShouldToRentalsAllIfRentCancelIsNotSuccessful()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.CancelAsync("asd"))
                .ReturnsAsync(false);

            var rentalsController = new RentalsController(rentsMock.Object, null, companiesMock.Object);
            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessianVariable"] = "admin";
            rentalsController.TempData = tempData;

            var result = await rentalsController.Cancel("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Cancel("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("All", routes.ActionName);
            Assert.Equal("Rentals", routes.ControllerName);
        }

        [Fact]
        public async Task DeleteShouldRedirectToRentalsAllIfResultIsSuccessful()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentingMock = new Mock<IRentingService>();
            rentingMock.Setup(x => x.DeleteAsync("asd"))
                .ReturnsAsync(true);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var rentalsController = new RentalsController(rentingMock.Object, null, companiesMock.Object);

            rentalsController.TempData = tempData;

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Delete("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Delete("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("All", routes.ActionName);
            Assert.Equal("Rentals", routes.ControllerName);
        }

        [Fact]
        public async Task DeleteShouldRedirectToComapniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var rentalsController = new RentalsController(null, null, companiesMock.Object);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Delete("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Finish("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public async Task DeleteShouldToRentalsAllIfRentCancelIsNotSuccessful()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.DeleteAsync("asd"))
                .ReturnsAsync(false);

            var rentalsController = new RentalsController(rentsMock.Object, null, companiesMock.Object);
            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessianVariable"] = "admin";
            rentalsController.TempData = tempData;

            var result = await rentalsController.Delete("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Delete("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("All", routes.ActionName);
            Assert.Equal("Rentals", routes.ControllerName);
        }

        [Fact]
        public async Task FinishShouldRedirectToRentalsAllIfResultIsSuccessful()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentingMock = new Mock<IRentingService>();
            rentingMock.Setup(x => x.FinishAsync("asd"))
                .ReturnsAsync(true);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var rentalsController = new RentalsController(rentingMock.Object, null, companiesMock.Object);

            rentalsController.TempData = tempData;

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Finish("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Finish("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("All", routes.ActionName);
            Assert.Equal("Rentals", routes.ControllerName);
        }

        [Fact]
        public async Task FinishShouldRedirectToComapniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var rentalsController = new RentalsController(null, null, companiesMock.Object);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Finish("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Finish("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public async Task FinishShouldToRentalsAllIfRentCancelIsNotSuccessful()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.FinishAsync("asd"))
                .ReturnsAsync(false);

            var rentalsController = new RentalsController(rentsMock.Object, null, companiesMock.Object);
            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessianVariable"] = "admin";
            rentalsController.TempData = tempData;

            var result = await rentalsController.Finish("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Finish("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("All", routes.ActionName);
            Assert.Equal("Rentals", routes.ControllerName);
        }

        [Fact]
        public async Task EditShouldReturnViewAndCorrectModel()
        {
            var model = new EditRentFormModel();
            var rentModel = new Rental();
            var mapperMock = MapperMock.Instance;

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentingMock = new Mock<IRentingService>();
            rentingMock.Setup(x => x.GetRentByIdAsync("asd"))
                .ReturnsAsync(rentModel);

            var rentalsController = new RentalsController(rentingMock.Object, mapperMock, companiesMock.Object);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Edit("asd");

            Assert.NotNull(result);

            var returnModel = Assert.IsType<ViewResult>(result);

            Assert.IsType<EditRentFormModel>(returnModel.Model);
        }

        [Fact]
        public async Task EditShouldReturnRedirectToActionToCompaniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var rentalsController = new RentalsController(null, null, companiesMock.Object);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Edit("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Edit("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public async Task EditShouldReturnRedirectToActionIndexInHomeControllerIfRentIsNull()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.GetRentByIdAsync("asd"));

            var rentalsController = new RentalsController(rentsMock.Object, null, companiesMock.Object);
            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Edit("asd");

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Edit("asd");

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", routes.ActionName);
            Assert.Equal("Home", routes.ControllerName);
        }

        [Fact]
        public async Task EditRentShouldReturnRedirectToActionAllInRentalsWhenEditIsSuccesfull()
        {
            var model = new EditRentFormModel();

            var rentingMock = new Mock<IRentingService>();
            rentingMock.Setup(x => x.EditRentAsync(model.Id, model.ClientFirstName, model.ClientLastName, model.ClientUserEmail, model.Price))
                .ReturnsAsync(true);

            var rentalsController = new RentalsController(rentingMock.Object, null, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessianVariable"] = "admin";
            rentalsController.TempData = tempData;

            var result = await rentalsController.Edit(model);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Edit(model);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("All", routes.ActionName);
            Assert.Equal("Rentals", routes.ControllerName);
        }

        [Fact]
        public async Task EditShouldReturnRedirectToActionRentalsAllIfEditIsNotSuccessfull()
        {
            var model = new EditRentFormModel();

            var rentingMock = new Mock<IRentingService>();
            rentingMock.Setup(x => x.EditRentAsync(model.Id, model.ClientFirstName, model.ClientLastName, model.ClientUserEmail, model.Price))
                .ReturnsAsync(false);

            var rentalsController = new RentalsController(rentingMock.Object, null, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessianVariable"] = "admin";
            rentalsController.TempData = tempData;

            var result = await rentalsController.Edit(model);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Edit(model);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("All", routes.ActionName);
            Assert.Equal("Rentals", routes.ControllerName);
        }

        [Fact]
        public void AllShouldReturnCorrectViewModelAndViewResult()
        {
            var rent = new List<RentsDto>();
            var model = new List<MyRentsViewModel>();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentingMock = new Mock<IRentingService>();
            rentingMock.Setup(x => x.GetAllCompanyRents())
                .Returns(rent);

            var mapperMock = MapperMock.Instance;

            var rentalsController = new RentalsController(rentingMock.Object, mapperMock, companiesMock.Object);
            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = rentalsController.All();

            Assert.NotNull(result);

            var returnModel = Assert.IsType<ViewResult>(result);

            Assert.IsType<List<MyRentsViewModel>>(returnModel.Model);
        }

        [Fact]
        public void AllShouldRedirectToCompaniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var rentalsController = new RentalsController(null, null, companiesMock.Object);
            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = rentalsController.All();

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)result;

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }
    }
}
