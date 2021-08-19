using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Moq;

using Oversteer.Data.Models.Cars;
using Oversteer.Services.Cars;
using Oversteer.Services.Companies;
using Oversteer.Tests.Extensions;
using Oversteer.Tests.Mocks;
using Oversteer.Web.Controllers;
using Oversteer.Web.Extensions;
using Oversteer.Web.ViewModels.Cars;

using Xunit;

namespace Oversteer.Tests.Controllers
{
    public class CarsControllerTest
    {
        [Fact]
        public async Task MyCarsShouldReturCorrectViewAndModel()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var model = new CarsSearchQueryModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetAddedByCompanyCarBrands(1))
                .Returns(model.Brands);
            carsMock.Setup(x => x.GetQueryCarsCounAsync(model))
                .ReturnsAsync(5);

            var carsController = new CarsController(carsMock.Object, companiesMock.Object, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho");

            var result = await carsController.MyCars(model);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var modelType = viewResult.Model;

            Assert.IsType<CarsSearchQueryModel>(modelType);
        }

        [Fact]
        public async Task MyCarsShouldReturRedirectIfCompanyIsNull()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var model = new CarsSearchQueryModel();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(false);
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetAddedByCompanyCarBrands(1))
                .Returns(model.Brands);
            carsMock.Setup(x => x.GetQueryCarsCounAsync(model))
                .ReturnsAsync(5);

            var carsController = new CarsController(carsMock.Object, companiesMock.Object, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho");

            var result = await carsController.MyCars(model);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task CarsAllShouldReturnView()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var model = new CarsSearchQueryModel();

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetAddedCarBrands())
                .Returns(model.Brands);
            carsMock.Setup(x => x.GetAllCars(model))
                .Returns(model.Cars);
            carsMock.Setup(x => x.GetQueryCarsCounAsync(model))
                .ReturnsAsync(model.TotalCars);

            var carsController = new CarsController(carsMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho");

            var result = await carsController.All(model);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var modelType = viewResult.Model;

            Assert.IsType<CarsSearchQueryModel>(modelType);
        }

        [Fact]
        public void CarsDetailsShouldReturnCorrectViewAndModel()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var model = new CarDetailsFormModel();

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetCarDetails(1))
                .Returns(model);

            var carsController = new CarsController(carsMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho");

            var result = carsController.Details(1, model.ToFriendlyUrl());

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var modelType = viewResult.Model;

            Assert.IsType<CarDetailsFormModel>(modelType);
        }

        [Fact]
        public void CarsDetailsShouldReturnNotFoundIfInformationIsDifferent()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var model = new CarDetailsFormModel();

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetCarDetails(1))
                .Returns(model);

            var carsController = new CarsController(carsMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho");

            var result = carsController.Details(1, "gogo");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CarsDeleteShouldWorkAndRedirectSuccessfully()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var model = new Car
            {
                Id = 5,
                IsDeleted = false
            };

            data.Cars.Add(model);
            data.SaveChanges();

            var companyMock = new Mock<ICompaniesService>();
            companyMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.IsCarFromCompany(5, 1))
                .Returns(true);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var carsController = new CarsController(carsMock.Object, companyMock.Object, null, null, null);

            carsController.TempData = tempData;

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho");

            var result = await carsController.Delete(model.Id);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task CarsDeleteShouldRedirectIfUserIsNotCompany()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var companyMock = new Mock<ICompaniesService>();
            companyMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var carsController = new CarsController(null, companyMock.Object, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho");

            var result = await carsController.Delete(1);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task CarsDeleteShouldReturnNotFoundIfCarIsNotFromCompany()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var companyMock = new Mock<ICompaniesService>();
            companyMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.IsCarFromCompany(5, 1))
                .Returns(false);

            var carsController = new CarsController(carsMock.Object, companyMock.Object, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho");

            var result = await carsController.Delete(1);

            var viewResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
