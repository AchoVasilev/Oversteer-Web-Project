namespace Oversteer.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using Moq;

    using Oversteer.Services.Companies;
    using Oversteer.Services.Companies.OfferedService;
    using Oversteer.Tests.Extensions;
    using Oversteer.Web.Areas.Company.Controllers;
    using Oversteer.Web.ViewModels.Companies;

    using Xunit;

    public class CompanyServiceControllerTest
    {
        [Fact]
        public void MyServicesShouldReturnViewAndCorrectModel()
        {
            var model = new List<CompanyServiceFormModel>();
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var servicesMock = new Mock<IOfferedServicesService>();
            servicesMock.Setup(x => x.GetAll(1))
                .Returns(model);

            var serviceController = new ServicesController(servicesMock.Object, companiesMock.Object);
            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");

            var result = serviceController.MyServices();

            Assert.NotNull(result);

            var view = Assert.IsType<ViewResult>(result);

            Assert.IsType<List<CompanyServiceFormModel>>(view.Model);
        }

        [Fact]
        public void MyServicesShouldReturnRedirectToActionToCompaniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var serviceController = new ServicesController(null, companiesMock.Object);
            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");

            var result = serviceController.MyServices();

            Assert.NotNull(result);

            var routes = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", routes.ActionName);
            Assert.Equal("Companies", routes.ControllerName);
        }

        [Fact]
        public async Task DeleteShouldReturnRedirectToActionToMyServicesIfDeleteIsSuccessful()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var serviceMock = new Mock<IOfferedServicesService>();
            serviceMock.Setup(x => x.CompanyHasServiceAsync(1, 1))
                .ReturnsAsync(true);
            serviceMock.Setup(x => x.DeleteServiceAsync(1))
                .ReturnsAsync(true);

            var serviceController = new ServicesController(serviceMock.Object, companiesMock.Object);
            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            serviceController.TempData = tempData;

            var result = await serviceController.Delete(1);

            Assert.NotNull(result);

            var routes = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("MyServices", routes.ActionName);
            Assert.Equal("Services", routes.ControllerName);
        }

        [Fact]
        public async Task DeleteShouldRedirectToActionCompaniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var serviceController = new ServicesController(null, companiesMock.Object);

            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");

            var result = await serviceController.Delete(1);

            Assert.NotNull(result);

            var model = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", model.ActionName);
            Assert.Equal("Companies", model.ControllerName);
        }

        [Fact]
        public async Task DeleteShouldReturnBadRequestIfCompanyDoesNotHaveTheService()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var serviceMock = new Mock<IOfferedServicesService>();
            serviceMock.Setup(x => x.CompanyHasServiceAsync(1, 1))
                .ReturnsAsync(false);

            var serviceController = new ServicesController(serviceMock.Object, companiesMock.Object);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            serviceController.TempData = tempData;

            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");

            var result = await serviceController.Delete(1);

            Assert.NotNull(result);

            var model = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteShouldReturnRedirectToActionToMyServicesIfDeleteIsNotSuccessful()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var serviceMock = new Mock<IOfferedServicesService>();
            serviceMock.Setup(x => x.CompanyHasServiceAsync(1, 1))
                .ReturnsAsync(true);
            serviceMock.Setup(x => x.DeleteServiceAsync(1))
                .ReturnsAsync(false);

            var serviceController = new ServicesController(serviceMock.Object, companiesMock.Object);

            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            serviceController.TempData = tempData;

            var result = await serviceController.Delete(1);

            Assert.NotNull(result);

            var model = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("MyServices", model.ActionName);
            Assert.Equal("Services", model.ControllerName);
        }

        [Fact]
        public void AddShouldReturnViewIfSuccessful()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var serviceController = new ServicesController(null, companiesMock.Object);
            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");

            var result = serviceController.Add();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AddShouldRedirectToActionCompaniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var serviceController = new ServicesController(null, companiesMock.Object);

            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");

            var result = serviceController.Add();

            Assert.NotNull(result);

            var model = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", model.ActionName);
            Assert.Equal("Companies", model.ControllerName);
        }

        [Fact]
        public async Task AddShouldReturnRedirectToActionToMyServicesIfAddIsSuccessful()
        {
            var serviceModel = new CompanyServiceFormModel();
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var serviceMock = new Mock<IOfferedServicesService>();

            var controller = new ServicesController(serviceMock.Object, companiesMock.Object);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho");

            var result = await controller.Add(serviceModel);

            Assert.NotNull(result);

            var model = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("MyServices", model.ActionName);
            Assert.Equal("Services", model.ControllerName);
        }

        [Fact]
        public async Task AddShouldReturnRedirectToActionToCompaniesCreateIfUserIsNotCompany()
        {
            var serviceModel = new CompanyServiceFormModel();
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var serviceMock = new Mock<IOfferedServicesService>();

            var controller = new ServicesController(serviceMock.Object, companiesMock.Object);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho");

            var result = await controller.Add(serviceModel);

            Assert.NotNull(result);

            var model = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", model.ActionName);
            Assert.Equal("Companies", model.ControllerName);
        }

        [Fact]
        public async Task EditShouldReturnViewIfSuccessful()
        {
            var expected = new CompanyServiceFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var serviceMock = new Mock<IOfferedServicesService>();
            serviceMock.Setup(x => x.GetServiceAsync(1, 1))
                .ReturnsAsync(expected);

            var serviceController = new ServicesController(serviceMock.Object, companiesMock.Object);
            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            serviceController.TempData = tempData;

            var result = await serviceController.Edit(1);

            Assert.NotNull(result);
            var model = Assert.IsType<ViewResult>(result);
            Assert.IsType<CompanyServiceFormModel>(model.Model);
        }

        [Fact]
        public async Task EditShouldReturnRedirectToActionToCompaniesCreateIfUserIsNotCompany()
        {
            var serviceModel = new CompanyServiceFormModel();
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var serviceMock = new Mock<IOfferedServicesService>();

            var controller = new ServicesController(serviceMock.Object, companiesMock.Object);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho");

            var result = await controller.Edit(1);

            Assert.NotNull(result);

            var model = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", model.ActionName);
            Assert.Equal("Companies", model.ControllerName);
        }

        [Fact]
        public async Task EditShouldReturnRedirectToActionToMyServices()
        {
            var expected = new CompanyServiceFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var serviceMock = new Mock<IOfferedServicesService>();
            serviceMock.Setup(x => x.GetServiceAsync(1, 1));

            var serviceController = new ServicesController(serviceMock.Object, companiesMock.Object);
            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            serviceController.TempData = tempData;

            var result = await serviceController.Edit(1);

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyServices", route.ActionName);
            Assert.Equal("Services", route.ControllerName);
        }

        [Fact]
        public async Task EditShouldReturnRedirectToActionToMyServicesWhenSuccessful()
        {
            var serviceModel = new CompanyServiceFormModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var serviceMock = new Mock<IOfferedServicesService>();

            var serviceController = new ServicesController(serviceMock.Object, companiesMock.Object);
            ControllerExtensions.WithIdentity(serviceController, "gosho", "pesho");

            var result = await serviceController.Edit(serviceModel);

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("MyServices", route.ActionName);
            Assert.Equal("Services", route.ControllerName);
        }

        [Fact]
        public async Task EditWithModelShouldReturnRedirectToActionToCompaniesCreateIfUserIsNotCompany()
        {
            var serviceModel = new CompanyServiceFormModel();
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var serviceMock = new Mock<IOfferedServicesService>();

            var controller = new ServicesController(serviceMock.Object, companiesMock.Object);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho");

            var result = await controller.Edit(serviceModel);

            Assert.NotNull(result);

            var model = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", model.ActionName);
            Assert.Equal("Companies", model.ControllerName);
        }
    }
}
