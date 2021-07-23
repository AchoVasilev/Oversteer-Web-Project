namespace Oversteer.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.Models.Cars;
    using Oversteer.Web.Services.Contracts;

    using static Oversteer.Web.Data.Constants.ErrorMessages;

    public class CarsController : Controller
    {
        private readonly ICarsService carService;
        private readonly ICompaniesService companiesService;

        public CarsController(ICarsService carService, ICompaniesService companiesService)
        {
            this.carService = carService;
            this.companiesService = companiesService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.GetId();

            if (!this.companiesService.UserIsCompany(userId))
            {
                return RedirectToAction(nameof(CompaniesController.Create), "Companies");
            }

            return this.View(new AddCarFormModel()
            {
                Brands = this.carService.GetCarBrands(),
                CarModels = this.carService.GetCarModels(),
                Colors = this.carService.GetCarColors(),
                FuelTypes = this.carService.GetFuelTypes(),
                Transmissions = this.carService.GetTransmissionTypes(),
                CarTypes = this.carService.GetCarTypes(),
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCarFormModel carModel)
        {
            var currentUserId = this.User.GetId();

            var companyId = this.companiesService.GetCurrentCompanyId(currentUserId);

            if (companyId == 0)
            {
                return RedirectToAction(nameof(CompaniesController.Create), "Companies");
            }

            if (!this.carService.GetBrandId(carModel.BrandId))
            {
                this.ModelState.AddModelError(nameof(carModel.BrandId), CarBrandDoesntExist);
            }

            if (!this.carService.GetModelId(carModel.ModelId))
            {
                this.ModelState.AddModelError(nameof(carModel.ModelId), CarModelDoesntExist);
            }

            if (!this.carService.GetCarTypeId(carModel.CarTypeId))
            {
                this.ModelState.AddModelError(nameof(carModel.CarTypeId), CarTypeDoesntExist);
            }

            if (!this.carService.GetFuelTypeId(carModel.FuelId))
            {
                this.ModelState.AddModelError(nameof(carModel.FuelId), CarFuelTypeDoesntExist);
            }

            if (!this.carService.GetTransmissionId(carModel.TransmissionId))
            {
                this.ModelState.AddModelError(nameof(carModel.TransmissionId), CarTransmissionTypeDoesntExist);
            }

            if (!this.carService.GetColorId(carModel.ColorId))
            {
                this.ModelState.AddModelError(nameof(carModel.ColorId), CarColorDoesntExist);
            }

            if (!ModelState.IsValid)
            {
                carModel.Brands = this.carService.GetCarBrands();
                carModel.CarModels = this.carService.GetCarModels();
                carModel.Colors = this.carService.GetCarColors();
                carModel.FuelTypes = this.carService.GetFuelTypes();
                carModel.Transmissions = this.carService.GetTransmissionTypes();
                carModel.CarTypes = this.carService.GetCarTypes();

                return this.View(carModel);
            }

            this.carService.CreateCar(carModel, companyId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult MyCars([FromQuery] CarsSearchQueryModel query)
        {
            var currentUserId = this.User.GetId();

            var companyId = this.companiesService.GetCurrentCompanyId(currentUserId);

            if (companyId == 0)
            {
                return RedirectToAction(nameof(CompaniesController.Create), "Companies");
            }

            return this.View(new CarsSearchQueryModel()
            {
                Brand = query.Brand,
                Brands = this.carService.GetAddedByCompanyCarBrands(companyId),
                Cars = this.carService.GetAllCars(query).Where(x => x.CompanyId == companyId),
                CarSorting = query.CarSorting,
                SearchTerm = query.SearchTerm,
                CurrentPage = query.CurrentPage,
                TotalCars = this.carService.GetQueryCarsCount(query),
                CompanyId = companyId
            });
        }

        public IActionResult All([FromQuery] CarsSearchQueryModel query)
            => this.View(new CarsSearchQueryModel()
            {
                Brand = query.Brand,
                Brands = this.carService.GetAddedCarBrands(),
                Cars = this.carService.GetAllCars(query),
                CarSorting = query.CarSorting,
                SearchTerm = query.SearchTerm,
                CurrentPage = query.CurrentPage,
                TotalCars = this.carService.GetQueryCarsCount(query)
            });

        public IActionResult Details(int id)
        {
            var car = this.carService.GetCarDetails(id);

            return this.View(car);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = this.User.GetId();
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            this.carService.DeleteCar(companyId, id);

            return RedirectToAction(nameof(CarsController.MyCars), "Cars");
        }
    }
}
