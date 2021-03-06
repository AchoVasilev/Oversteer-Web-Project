namespace Oversteer.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Web.Areas.Company.Controllers;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Companies;

    using Xunit;

    public class CompanyCarsControllerTest
    {
        [Fact]
        public async Task CarsShouldReturnCorrectViewAndModel()
        {
            var model = new CompanyCar();
            var cars = new List<ListCarFormModel>();

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetCompanyCarsCount(3))
                .ReturnsAsync(5);

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.AllCompanyCars(5, 5, 3))
                .Returns(cars);

            var carsController = new CarsController(carsMock.Object, companiesMock.Object);

            var result = await carsController.Cars(3);

            Assert.NotNull(result);

            var returnModel = Assert.IsType<ViewResult>(result);
            Assert.IsType<CompanyCar>(returnModel.Model);
        }

        [Fact]
        public async Task CarsShouldReturnNotFoundIfCarsIdIsWrong()
        {
            var carsController = new CarsController(null, null);

            var result = await carsController.Cars(0);

            Assert.NotNull(result);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
