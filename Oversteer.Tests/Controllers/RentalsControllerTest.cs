using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Oversteer.Data.Models.Cars;
using Oversteer.Data.Models.Rentals;
using Oversteer.Services.Cars;
using Oversteer.Services.Companies;
using Oversteer.Services.Rentals;
using Oversteer.Tests.Extensions;
using Oversteer.Tests.Mocks;
using Oversteer.Web.Controllers;
using Oversteer.Web.ViewModels.Cars;
using Oversteer.Web.ViewModels.Rents;

using Xunit;

using static Oversteer.Tests.ModelConstants.CarConstants;

namespace Oversteer.Tests.Controllers
{
    public class RentalsControllerTest
    {
        [Fact]
        public void PreviewShouldReturnViewWithCorrectModelWhenModelStateIsValid()
        {
            var rentalsController = new RentalsController(null, null, null, null, null, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var previewModel = new RentPreviewModel()
            {
                CarId = 1,
                CompanyId = 1,
                CompanyName = "Penko",
                Days = 10,
                PricePerDay = 100m,
                Image = "pesho",
                Model = "VW",
                PickUpPlace = "Sofia",
                ReturnPlace = "Kaspichan",
                RentStart = DateTime.UtcNow.Date,
                RentEnd = DateTime.UtcNow.Date.AddDays(3)
            };

            var result = rentalsController.Preview(previewModel);

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<RentPreviewModel>(model);
        }

        [Fact]
        public void MyRentsShouldReturnAllUserRents()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.GetAllUserRents("gosho"));

            var rentalsController = new RentalsController(rentsMock.Object, null, null, null, mapper, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = rentalsController.MyRents();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<List<MyRentsViewModel>>(model);
        }

        [Fact]
        public void InvalidShouldReturnView()
        {
            var rentalsController = new RentalsController(null, null, null, null, null, null);

            var result = rentalsController.Invalid();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task DetailsShouldReturnCorrectViewAndModel()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var rent = new Rental()
            {
                Id = "51s",
            };

            data.Rentals.Add(rent);
            data.SaveChanges();

            var expected = new RentDetailsModel();

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.GetDetailsAsync("51s"))
                .ReturnsAsync(expected);
                
            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"))
                .Returns(1);

            var rentalsController = new RentalsController(rentsMock.Object, null, companiesMock.Object, null, mapper, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Details("51s");

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            Assert.IsType<RentDetailsModel>(model);
        }

        [Fact]
        public async Task DetailsShouldReturnRedirectToActionIfCompanyIdIsWrong()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var expected = new RentDetailsModel();

            var rentsMock = new Mock<IRentingService>();
            rentsMock.Setup(x => x.GetDetailsAsync("51s"));

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.GetCurrentCompanyId("gosho"));

            var rentalsController = new RentalsController(rentsMock.Object, null, companiesMock.Object, null, mapper, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Details("51s");

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task OrderShouldRedirectIfUserIsCompany()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var model = new RentFormModel
            {
                CompanyId = 1,
            };

            var companiesMock = new Mock<ICompaniesService>();
            companiesMock.Setup(x => x.UserIsCompany("gosho"))
                .Returns(true);

            var rentalsController = new RentalsController(null, null, companiesMock.Object, null, mapper, null);

            ControllerExtensions.WithIdentity(rentalsController, "gosho", "pesho");

            var result = await rentalsController.Order(model);

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
