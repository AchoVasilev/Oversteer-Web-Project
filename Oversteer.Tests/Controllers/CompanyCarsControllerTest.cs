namespace Oversteer.Tests.Controllers
{
    using System.Collections.Generic;

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
        public void CarsShouldReturnCorrectViewAndModel()
        {
            var model = new CompanyCar();
            var cars = new List<ListCarFormModel>();

            var carsMock = new Mock<ICarsService>();
            carsMock.Setup(x => x.GetCompanyCarsCount(3))
                .Returns(5);

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.AllCompanyCars(5, 5, 3))
                .Returns(cars);

            var carsController = new CarsController(carsMock.Object, companiesMock.Object);

            var result = carsController.Cars(3);

            Assert.NotNull(result);

            var returnModel = Assert.IsType<ViewResult>(result);
            Assert.IsType<CompanyCar>(returnModel.Model);
        }

        [Fact]
        public void CarsShouldReturnNotFoundIfCarsIdIsWrong()
        {
            var carsController = new CarsController(null, null);

            var result = carsController.Cars(0);

            Assert.NotNull(result);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
