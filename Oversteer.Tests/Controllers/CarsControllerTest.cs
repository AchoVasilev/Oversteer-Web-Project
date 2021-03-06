namespace Oversteer.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using Moq;

    using Oversteer.Data.Models.Cars;
    using Oversteer.Data.Models.Users;
    using Oversteer.Services.Caches;
    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Services.DateTime;
    using Oversteer.Tests.Extensions;
    using Oversteer.Tests.Mock;
    using Oversteer.Web.Controllers;
    using Oversteer.Web.Extensions;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;
    using Oversteer.Web.ViewModels.Home;
    using Oversteer.Web.ViewModels.Locations;

    using Xunit;

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

            var carsController = new CarsController(carsMock.Object, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho", "pipi");

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

            var carsController = new CarsController(carsMock.Object, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho", "pipi");

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

            var carsController = new CarsController(carsMock.Object, null, null, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho", "pipi");

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

            var carsController = new CarsController(carsMock.Object, null, null, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho", "pipi");

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

            var carsController = new CarsController(carsMock.Object, null, null, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho", "pipi");

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
            carsMock.Setup(x => x.IsCarFromCompanyAsync(5, 1))
                .ReturnsAsync(true);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var carsController = new CarsController(carsMock.Object, companyMock.Object, null, null, null, null);

            carsController.TempData = tempData;

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho", "pipi");

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

            var carsController = new CarsController(null, companyMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho", "pipi");

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
            carsMock.Setup(x => x.IsCarFromCompanyAsync(5, 1))
                .ReturnsAsync(false);

            var carsController = new CarsController(carsMock.Object, companyMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(carsController, "gosho", "pesho", "pipi");

            var result = await carsController.Delete(1);

            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddShouldReturnViewAndModelIfSuccessful()
        {
            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();
            var locations = new List<LocationFormModel>();
            var company = new Company { Id = 1 };

            var locationsString = new List<string>();
            locationsString.Add("1");
            locationsString.Add("2");
            locationsString.Add("3");

            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);
            companiesMock.Setup(x => x.GetCompanyLocations(1))
                .ReturnsAsync(locationsString);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(company.Id))
                .Returns(locations);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(null, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";
            controller.TempData = tempData;

            var result = await controller.Add();

            Assert.NotNull(result);

            var model = Assert.IsType<ViewResult>(result);

            Assert.IsType<CarFormModel>(model.Model);
        }

        [Fact]
        public async Task AddShouldRedirectToCompaniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var controller = new CarsController(null, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";
            controller.TempData = tempData;

            var result = await controller.Add();

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", route.ActionName);
            Assert.Equal("Companies", route.ControllerName);
        }

        [Fact]
        public async Task AddShouldRedirectToCompaniesAddLocationIfCompanyHasNoLocations()
        {
            var locationsString = new List<string>();

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            companiesMock.Setup(x => x.GetCompanyLocations(1))
                .ReturnsAsync(locationsString);

            var controller = new CarsController(null, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";
            controller.TempData = tempData;

            var result = await controller.Add();

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("AddLocation", route.ActionName);
            Assert.Equal("Companies", route.ControllerName);
        }

        [Fact]
        public async Task AddShouldRedirectToMyCarsIfAddingCarIsSuccessful()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);
            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";
            controller.TempData = tempData;

            var result = await controller.Add(expected);

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("MyCars", route.ActionName);
        }

        [Fact]
        public async Task AddWithModelShouldRedirectToCompaniesCreateIfUserIsNotCompany()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var controller = new CarsController(null, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";
            controller.TempData = tempData;

            var result = await controller.Add(expected);

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", route.ActionName);
            Assert.Equal("Companies", route.ControllerName);
        }

        [Fact]
        public async Task AddShouldAddErrorToModelStateIfBrandIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Add(expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task AddShouldAddErrorToModelStateIfModelIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Add(expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task AddShouldAddErrorToModelStateIfCarTypeIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Add(expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task AddShouldAddErrorToModelStateIfFuelTypeIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Add(expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task AddShouldAddErrorToModelStateIfTransmissionIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Add(expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task AddShouldAddErrorToModelStateIfColorIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(false);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);
            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Add(expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public void EditShouldReturnViewAndModelIfSuccessful()
        {
            var carModel = new CarDetailsFormModel { BrandName = "VW", ModelName = "Passat", ModelYear = 2012, CompanyUserId = "gosho" };

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetCarDetails(1))
                .Returns(carModel);

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();
            var locations = new List<LocationFormModel>();
            var company = new Company { Id = 1 };

            var locationsString = new List<string>();
            locationsString.Add("1");
            locationsString.Add("2");
            locationsString.Add("3");

            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);
            companiesMock.Setup(x => x.GetCompanyLocations(1))
                .ReturnsAsync(locationsString);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(company.Id))
                .Returns(locations);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = controller.Edit(1, carModel.ToFriendlyUrl());

            Assert.NotNull(result);

            var model = Assert.IsType<ViewResult>(result);

            Assert.IsType<CarFormModel>(model.Model);
        }

        [Fact]
        public void EditShouldRedirectToCompaniesCreateIfUserIsNotCompany()
        {
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var controller = new CarsController(null, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = controller.Edit(1, "asd");

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", route.ActionName);
            Assert.Equal("Companies", route.ControllerName);
        }

        [Fact]
        public void EditShouldNotFoundIfInformationIsDifferentFromModelFriendlyUrl()
        {
            var carModel = new CarDetailsFormModel { BrandName = "VW", ModelName = "Passat", ModelYear = 2012, CompanyUserId = "gosho" };

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetCarDetails(1))
                .Returns(carModel);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = controller.Edit(1, "asd");

            Assert.NotNull(result);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditShouldUnauthorizedIfUserIdIsDifferentFromModelId()
        {
            var carModel = new CarDetailsFormModel { BrandName = "VW", ModelName = "Passat", ModelYear = 2012, CompanyUserId = "pipi" };

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetCarDetails(1))
                .Returns(carModel);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = controller.Edit(1, carModel.ToFriendlyUrl());

            Assert.NotNull(result);

            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task EditShouldRedirectToMyCarsIfEditingCarIsSuccessful()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var car = new Car { Id = 1, CompanyId = 1 };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.IsCarFromCompanyAsync(car.Id, car.CompanyId))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);
            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";
            controller.TempData = tempData;

            var result = await controller.Edit(car.Id, expected);

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("MyCars", route.ActionName);
        }

        [Fact]
        public async Task EditWithDataShouldRedirectToCompaniesCreateIfUserIsNotCompany()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(0);

            var controller = new CarsController(null, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Edit(1, expected);

            Assert.NotNull(result);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Create", route.ActionName);
            Assert.Equal("Companies", route.ControllerName);
        }

        [Fact]
        public async Task EditWithDataShouldRedirectToNotFoundIfCarIsNotFromCompany()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.IsCarFromCompanyAsync(1, 1))
                .ReturnsAsync(false);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, null, null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Edit(1, expected);

            Assert.NotNull(result);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditShouldAddErrorToModelStateIfBrandIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.IsCarFromCompanyAsync(1, 1))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Edit(1, expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task EditShouldAddErrorToModelStateIfModelIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.IsCarFromCompanyAsync(1, 1))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Edit(1, expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task EditShouldAddErrorToModelStateIfCarTypeIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.IsCarFromCompanyAsync(1, 1))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Edit(1, expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task EditShouldAddErrorToModelStateIfFuelTypeIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.IsCarFromCompanyAsync(1, 1))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);
            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Edit(1, expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task EditShouldAddErrorToModelStateIfTransmissionIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.IsCarFromCompanyAsync(1, 1))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Edit(1, expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task EditShouldAddErrorToModelStateIfColorIdIsInvalid()
        {
            var expected = new CarFormModel
            {
                BrandId = 1,
                ModelId = 1,
                CarTypeId = 1,
                TransmissionId = 1,
                ColorId = 1,
                FuelId = 1,
                LocationId = 1
            };

            var carBrands = new List<CarBrandFormModel>();
            var carModels = new List<CarModelFormModel>();
            var colors = new List<ColorFormModel>();
            var fuel = new List<FuelTypeFormModel>();
            var transmissions = new List<TransmissionTypeFormModel>();
            var types = new List<CarTypeFormModel>();

            var locations = new List<LocationFormModel>();
            locations.Add(new LocationFormModel { Id = 1 });
            locations.Add(new LocationFormModel { Id = 2 });
            locations.Add(new LocationFormModel { Id = 3 });

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var locationsMock = new Mock<ILocationService>();
            locationsMock.Setup(x => x.GetCompanyLocations(1))
                .Returns(locations);

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.BrandExistsAsync(expected.BrandId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ModelExistsAsync(expected.ModelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.CarTypeExistsAsync(expected.CarTypeId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.FuelTypeExistsAsync(expected.FuelId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.TransmissionExistsAsync(expected.TransmissionId))
                .ReturnsAsync(true);
            carsMock.Setup(x => x.ColorExistsAsync(expected.ColorId))
                .ReturnsAsync(false);
            carsMock.Setup(x => x.IsCarFromCompanyAsync(1, 1))
                .ReturnsAsync(true);

            var cacheMock = new Mock<ICarCacheService>();
            cacheMock.Setup(x => x.CacheCarBrands("brand"))
                .Returns(carBrands);
            cacheMock.Setup(x => x.CacheCarModels("model"))
                .Returns(carModels);
            cacheMock.Setup(x => x.CacheCarColors("color"))
               .Returns(colors);
            cacheMock.Setup(x => x.CacheCarFuelTypes("fuel"))
                .Returns(fuel);
            cacheMock.Setup(x => x.CacheCarTransmissionTypes("transmission"))
               .Returns(transmissions);
            cacheMock.Setup(x => x.CacheCarTypes("types"))
                .Returns(types);

            var controller = new CarsController(carsMock.Object, companiesMock.Object, null, locationsMock.Object, cacheMock.Object, null);
            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = await controller.Edit(1, expected);

            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public void AvailableShouldReturnCorrectViewAndModel()
        {
            var searchRentModel = new SearchRentCarModel { PickUpDate = "05/12/2021", ReturnDate = "10/12/2021" };
            var availableCars = new List<CarDto>();

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetAvailableCars("05/12/2021", "10/12/2021", "asd"))
                .Returns(availableCars);

            var dateTimeMock = new Mock<IDateTimeParserService>();

            var controller = new CarsController(carsMock.Object, null, null, null, null, dateTimeMock.Object);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            var result = controller.Available(searchRentModel);

            var model = Assert.IsType<ViewResult>(result);

            Assert.IsType<AvailableCarModel>(model.Model);
        }

        [Fact]
        public void AvailableShouldRedirectToHomeIfModelstateIsNotValid()
        {
            var model = new SearchRentCarModel();

            var controller = new CarsController(null, null, null, null, null, null);

            ControllerExtensions.WithIdentity(controller, "gosho", "pesho", "pipi");

            controller.ModelState.AddModelError("test", "test");

            var result = controller.Available(model);

            var route = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", route.ActionName);
            Assert.Equal("Home", route.ControllerName);
        }
    }
}
