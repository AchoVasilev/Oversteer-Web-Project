namespace Oversteer.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using Moq;

    using Oversteer.Services.Companies;
    using Oversteer.Tests.Extensions;
    using Oversteer.Web.Areas.Company.Controllers;
    using Oversteer.Web.ViewModels.Companies;
    using Oversteer.Web.ViewModels.Locations;

    using Xunit;

    public class CompanyCompaniesControllerTest
    {
        [Fact]
        public void CreateShouldReturnView()
        {
            var controller = new CompaniesController(null, null, null);

            var result = controller.Create();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CreateShouldRedirectToActionWhenUserIsCompany()
        {
            var expected = new CreateCompanyFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var companiesController = new CompaniesController(companiesMock.Object, null, null);

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.Create(expected);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await companiesController.Create(expected);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Add", routes.ActionName);
            Assert.Equal("Cars", routes.ControllerName);
        }

        [Fact]
        public async Task CreateShouldRedirectToActionWhenSuccessfull()
        {
            var expected = new CreateCompanyFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var rentalsController = new CompaniesController(companiesMock.Object, null, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho", "pipi");

            var result = await rentalsController.Create(expected);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await rentalsController.Create(expected);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Add", routes.ActionName);
            Assert.Equal("Cars", routes.ControllerName);
        }

        [Fact]
        public void AddLocationReturnView()
        {
            var controller = new CompaniesController(null, null, null);

            var result = controller.AddLocation();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AddLocationShouldReturnRedirectToMyLocationsWhenResultIsSuccessful()
        {
            var expected = new CreateLocationFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var companiesController = new CompaniesController(companiesMock.Object, locationsMock.Object, null);

            companiesController.TempData = tempData;

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.AddLocation(expected);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await companiesController.AddLocation(expected);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("MyLocations", routes.ActionName);
        }

        [Fact]
        public async Task AddLocationShouldRedirectToCompaniesCreateIfUserIsNotCompany()
        {
            var expected = new CreateLocationFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var companiesController = new CompaniesController(companiesMock.Object, null, null);

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.AddLocation(expected);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await companiesController.AddLocation(expected);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public void MyLocationsShouldReturnViewWithCorrectModel()
        {
            var locationMock = new Mock<ILocationService>();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var companiesController = new CompaniesController(companiesMock.Object, locationMock.Object, null);

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = companiesController.MyLocations();

            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsAssignableFrom<IEnumerable<LocationFormModel>>(model);
        }

        [Fact]
        public void MyLocationsShouldRedirectToCompaniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);

            var companiesController = new CompaniesController(companiesMock.Object, null, null);

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = companiesController.MyLocations();

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)companiesController.MyLocations();

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public async Task DeleteLocationShouldReturnRedirectToMyLocationsWhenResultIsSuccessful()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var companiesController = new CompaniesController(companiesMock.Object, locationsMock.Object, null);

            companiesController.TempData = tempData;

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.DeleteLocation(1);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await companiesController.DeleteLocation(1);

            Assert.IsType<RedirectToActionResult>(result);
            
            Assert.Equal("MyLocations", routes.ActionName);
        }

        [Fact]
        public async Task DeleteLocationsShouldRedirectToCompaniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var companiesController = new CompaniesController(companiesMock.Object, null, null);

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.DeleteLocation(1);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await companiesController.DeleteLocation(1);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public async Task EditLocationsShouldRedirectToCompaniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var companiesController = new CompaniesController(companiesMock.Object, null, null);

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.EditLocation(1);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await companiesController.EditLocation(1);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public async Task EditLocationsShouldReturnNotFoundIfLocationIsNotFromCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.LocationIsFromCompanyAsync(1, 1))
                .ReturnsAsync(false);

            var companiesController = new CompaniesController(companiesMock.Object, locationsMock.Object, null);

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.EditLocation(1);

            Assert.NotNull(result);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditLocationShouldReturnCorrectViewAndModel()
        {
            var companyDetails = new CompanyDetailsFormModel();
            var location = new LocationFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCurrentLocationAsync(1))
                .ReturnsAsync(location);
            locationsMock.Setup(x => x.LocationIsFromCompanyAsync(1, 1))
                .ReturnsAsync(true);

            var companiesController = new CompaniesController(companiesMock.Object, locationsMock.Object, null);

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.EditLocation(1);

            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<LocationFormModel>(model);
        }

        [Fact]
        public async Task EditLocationShouldReturnRedirectToMyLocationsWhenResultIsSuccessful()
        {
            var expected = new LocationFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.EditLocationAsync(expected.Id, expected.CountryId, expected.CityName, expected.AddressName))
                .ReturnsAsync(true);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var companiesController = new CompaniesController(companiesMock.Object, locationsMock.Object, null);

            companiesController.TempData = tempData;

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.EditLocation(expected);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await companiesController.EditLocation(expected);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("MyLocations", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public async Task EditLocationShouldReturnRedirectToCompaniesCreateIfUserIsNotCompany()
        {
            var expected = new LocationFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var locationsMock = new Mock<ILocationService>();

            var companiesController = new CompaniesController(companiesMock.Object, locationsMock.Object, null);

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.EditLocation(expected);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await companiesController.EditLocation(expected);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public async Task EditLocationShouldReturnRedirectToIndexOfHomeControllerIfLocationIsNotEditedSuccessfully()
        {
            var expected = new LocationFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.EditLocationAsync(expected.Id, expected.CountryId, expected.CityName, expected.AddressName))
                .ReturnsAsync(false);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var companiesController = new CompaniesController(companiesMock.Object, locationsMock.Object, null);

            companiesController.TempData = tempData;

            ControllerExtensions.WithIdentity(companiesController, "gosho", "pesho", "pipi");

            var result = await companiesController.EditLocation(expected);

            Assert.NotNull(result);

            var routes = (RedirectToActionResult)await companiesController.EditLocation(expected);

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", routes.ActionName);
            Assert.Equal("Home", routes.ControllerName);
        }

        [Fact]
        public async Task DetailsShouldReturnCorrectViewAndModel()
        {
            var model = new CompanyDetailsFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.DetailsAsync(2))
                .ReturnsAsync(model);

            var companyController = new CompaniesController(companiesMock.Object, null, null);

            var result = await companyController.Details(2);

            Assert.NotNull(result);

            var returnModel = Assert.IsType<ViewResult>(result);

            Assert.IsType<CompanyDetailsFormModel>(returnModel.Model);
        }
    }
}
