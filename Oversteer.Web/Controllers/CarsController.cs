namespace Oversteer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Caches;
    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Services.DateTime;
    using Oversteer.Web.Areas.Company.Controllers;
    using Oversteer.Web.Extensions;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Home;

    using static Oversteer.Data.Common.Constants.ErrorMessages;

    public class CarsController : Controller
    {
        private const string CarBrandsCacheKey = "carBrandsCacheKey";
        private const string CarModelsCacheKey = "carModelsCacheKey";
        private const string CarColorsCacheKey = "carColorsCacheKey";
        private const string CarFuelTypesCacheKey = "carFuelTypesCacheKey";
        private const string CarTransmissionTypesCacheKey = "carTransmissionTypesCacheKey";
        private const string CarTypesCacheKey = "carTypesCacheKey";

        private readonly ICarsService carService;
        private readonly ICompaniesService companiesService;
        private readonly IWebHostEnvironment environment;
        private readonly ILocationService locationService;
        private readonly ICarCacheService carCacheService;
        private readonly IDateTimeParserService dateTimeParserService;

        public CarsController(
            ICarsService carService,
            ICompaniesService companiesService,
            IWebHostEnvironment environment,
            ILocationService locationService,
            ICarCacheService carCacheService, 
            IDateTimeParserService dateTimeParserService)
        {
            this.carService = carService;
            this.companiesService = companiesService;
            this.environment = environment;
            this.locationService = locationService;
            this.carCacheService = carCacheService;
            this.dateTimeParserService = dateTimeParserService;
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            var userId = this.User.GetId();

            if (!this.companiesService.UserIsCompany(userId))
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            var locationsCount = (await companiesService.GetCompanyLocations(companyId)).Count;

            if (locationsCount == 0)
            {
                this.TempData["Message"] = "You need to add offered locations to add a car.";

                return this.RedirectToAction(nameof(CompaniesController.AddLocation), "Companies", new { area = "Company" });
            }

            return this.View(new CarFormModel()
            {
                Brands = this.carCacheService.CacheCarBrands(CarBrandsCacheKey),
                CarModels = this.carCacheService.CacheCarModels(CarModelsCacheKey),
                Colors = this.carCacheService.CacheCarColors(CarColorsCacheKey),
                FuelTypes = this.carService.GetFuelTypes(),
                Transmissions = this.carService.GetTransmissionTypes(),
                CarTypes = this.carService.GetCarTypes(),
                Locations = this.locationService.GetCompanyLocations(companyId)
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CarFormModel carModel)
        {
            var currentUserId = this.User.GetId();

            var companyId = this.companiesService.GetCurrentCompanyId(currentUserId);

            if (companyId == 0 && this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
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
                carModel.Locations = this.locationService.GetCompanyLocations(companyId);

                return this.View(carModel);
            }

            try
            {
                await this.carService.CreateCarAsync(carModel, companyId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", ex.Message);
                return this.View(carModel);
            }

            this.TempData["Message"] = "Car was added successfully.";

            return this.RedirectToAction(nameof(this.MyCars));
        }

        [Authorize]
        public IActionResult Edit(int id, string information)
        {
            var userId = this.User.GetId();

            if (!this.companiesService.UserIsCompany(userId) && !User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var car = this.carService.GetCarDetails(id);

            if (information != car.ToFriendlyUrl())
            {
                return NotFound();
            }

            if (car.CompanyUserId != userId && !User.IsAdmin())
            {
                return this.Unauthorized();
            }

            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            var carForm = new CarFormModel()
            {
                Brands = this.carCacheService.CacheCarBrands(CarBrandsCacheKey),
                CarModels = this.carCacheService.CacheCarModels(CarModelsCacheKey),
                Colors = this.carCacheService.CacheCarColors(CarColorsCacheKey),
                FuelTypes = this.carService.GetFuelTypes(),
                Transmissions = this.carService.GetTransmissionTypes(),
                CarTypes = this.carService.GetCarTypes(),
                Locations = this.locationService.GetCompanyLocations(companyId)
            };

            return this.View(carForm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, CarFormModel carModel)
        {
            var currentUserId = this.User.GetId();

            var companyId = this.companiesService.GetCurrentCompanyId(currentUserId);

            if (!this.companiesService.UserIsCompany(currentUserId) && !User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            if (this.carService.IsCarFromCompany(id, companyId))
            {
                return NotFound();
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
                carModel.Brands = this.carCacheService.CacheCarBrands(CarBrandsCacheKey);
                carModel.CarModels = this.carCacheService.CacheCarModels(CarModelsCacheKey);
                carModel.Colors = this.carCacheService.CacheCarColors(CarColorsCacheKey);
                carModel.FuelTypes = this.carService.GetFuelTypes();
                carModel.Transmissions = this.carService.GetTransmissionTypes();
                carModel.CarTypes = this.carService.GetCarTypes();
                carModel.Locations = this.locationService.GetCompanyLocations(companyId);

                return this.View(carModel);
            }

            if (!this.carService.IsCarFromCompany(id, companyId) && !User.IsAdmin())
            {
                return this.NotFound();
            }

            try
            {
                await this.carService.EditCarAsync(
                    id,
                    carModel.BrandId,
                    carModel.ModelId,
                    carModel.ColorId,
                    carModel.CarTypeId,
                    carModel.FuelId,
                    carModel.TransmissionId,
                    carModel.Year,
                    carModel.DailyPrice,
                    carModel.SeatsCount,
                    carModel.ImageUrl,
                    carModel.Description,
                    carModel.Images,
                    companyId,
                    carModel.CarFeatures
                    );
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", ex.Message);
                return this.View(carModel);
            }

            this.TempData["Message"] = "The car was edited successfully.";

            return this.RedirectToAction(nameof(MyCars));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.GetId();
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (companyId == 0 && !User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            if (!this.carService.IsCarFromCompany(id, companyId))
            {
                return NotFound();
            }

            await this.carService.DeleteCarAsync(companyId, id);

            this.TempData["Message"] = "The car was removed successfully.";

            return this.RedirectToAction(nameof(this.MyCars));
        }

        [Authorize]
        public async Task<IActionResult> MyCars([FromQuery] CarsSearchQueryModel query)
        {
            var currentUserId = this.User.GetId();

            var companyId = this.companiesService.GetCurrentCompanyId(currentUserId);

            if (!this.companiesService.UserIsCompany(currentUserId))
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var model = new CarsSearchQueryModel()
            {
                Brand = query.Brand,
                Brands = this.carService.GetAddedByCompanyCarBrands(companyId),
                Cars = this.carService.GetAllCars(query).Where(x => x.CompanyId == companyId),
                CarSorting = query.CarSorting,
                SearchTerm = query.SearchTerm,
                CurrentPage = query.CurrentPage,
                TotalCars = await this.carService.GetQueryCarsCounAsync(query),
                CompanyId = companyId
            };

            return this.View(model);
        }

        public async Task<IActionResult> All([FromQuery] CarsSearchQueryModel query)
            => this.View(new CarsSearchQueryModel()
            {
                Brand = query.Brand,
                Brands = this.carService.GetAddedCarBrands(),
                Cars = this.carService.GetAllCars(query),
                CarSorting = query.CarSorting,
                SearchTerm = query.SearchTerm,
                CurrentPage = query.CurrentPage,
                TotalCars = await this.carService.GetQueryCarsCounAsync(query)
            });

        public IActionResult Details(int id, string information)
        {
            var car = this.carService.GetCarDetails(id);

            if (information != car.ToFriendlyUrl())
            {
                return NotFound();
            }

            return this.View(car);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Available(SearchRentCarModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var cars = this.carService.GetAvailableCars(model.PickUpDate, model.ReturnDate, model.PickUpLocation);

            var startDate = this.dateTimeParserService.ParseDate(model.PickUpDate);

            var endDate = this.dateTimeParserService.ParseDate(model.ReturnDate);

            var daysCount = (endDate - startDate).TotalDays;

            var viewModel = new AvailableCarModel()
            {
                Cars = cars,
                StartDate = model.PickUpDate,
                EndDate = model.ReturnDate,
                Days = daysCount,
                PickUpPlace = model.PickUpLocation,
                ReturnPlace = model.DropOffLocation
            };

            return this.View(viewModel);
        }
    }
}
