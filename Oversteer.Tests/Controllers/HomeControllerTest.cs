namespace Oversteer.Tests.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using Oversteer.Data.Models.Users;
    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Services.Home;
    using Oversteer.Services.Rentals;
    using Oversteer.Tests.Data;
    using Oversteer.Tests.Extensions;
    using Oversteer.Tests.Mock;
    using Oversteer.Web.Controllers;
    using Oversteer.Web.ViewModels.Home;

    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void Error404ShouldReturnView()
        {
            var homeController = new HomeController(null, null, null, null);

            var result = homeController.Error404();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy()
        {
            var homeController = new HomeController(null, null, null, null);

            var result = homeController.Privacy();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task IndexShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Cars.AddRange(Cars.TenPublicCars);
            data.Users.Add(new ApplicationUser());

            data.SaveChanges();

            var carsService = new CarsService(data, mapper, null, null, null);
            var homeService = new HomeService(data);
            var locationService = new LocationService(data, null, null, mapper);

            var rents = new Mock<IRentingService>();
            rents.Setup(x => x.UserFinishedRentsAsync("pesho"))
                .ReturnsAsync(true);

            var homeController = new HomeController(carsService, homeService, locationService, rents.Object);
            ControllerExtensions.WithIdentity(homeController, "gosho", "pesho", "");

            var result = await homeController.Index();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(10, indexModel.TotalCars);
        }
    }
}
