namespace Oversteer.Tests.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

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
    }
}
