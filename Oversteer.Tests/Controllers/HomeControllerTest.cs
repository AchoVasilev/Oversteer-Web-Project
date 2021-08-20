namespace Oversteer.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Data.Models.Users;
    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Services.Home;
    using Oversteer.Tests.Data;
    using Oversteer.Tests.Mocks;
    using Oversteer.Web.Controllers;
    using Oversteer.Web.ViewModels.Home;

    using Xunit;

    public class HomeControllerTest
    {
       [Fact]
       public void ErrorShouldReturnView()
        {
            var homeController = new HomeController(null, null, null);

            var result = homeController.Error();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error404ShouldReturnView()
        {
            var homeController = new HomeController(null, null, null);

            var result = homeController.Error404();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy()
        {
            var homeController = new HomeController(null, null, null);

            var result = homeController.Privacy();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Cars.AddRange(Cars.TenPublicCars);
            data.Users.Add(new ApplicationUser());

            data.SaveChanges();

            var carsService = new CarsService(data, mapper, null, null, null);
            var homeService = new HomeService(data);
            var locationService = new LocationService(data, null, null, mapper);
            var homeController = new HomeController(carsService, homeService, locationService);

            var result = homeController.Index();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(10, indexModel.TotalCars);
        }
    }
}
