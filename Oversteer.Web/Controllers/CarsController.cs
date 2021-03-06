namespace Oversteer.Web.Controllers
{
    using System;
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
    using static Oversteer.Data.Common.Constants.WebConstants.Caching;

    public class CarsController : Controller
    {
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
            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (companyId == 0)
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

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
                FuelTypes = this.carCacheService.CacheCarFuelTypes(CarFuelTypesCacheKey),
                Transmissions = this.carCacheService.CacheCarTransmissionTypes(CarTransmissionTypesCacheKey),
                CarTypes = this.carCacheService.CacheCarTypes(CarTypesCacheKey),
                Locations = this.locationService.GetCompanyLocations(companyId)
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CarFormModel carModel)
        {
            var currentUserId = this.User.GetId();

            var companyId = this.companiesService.GetCurrentCompanyId(currentUserId);

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            if (!await this.carService.BrandExistsAsync(carModel.BrandId))
            {
                this.ModelState.AddModelError(nameof(carModel.BrandId), CarBrandDoesntExist);
            }

            if (!await this.carService.ModelExistsAsync(carModel.ModelId))
            {
                this.ModelState.AddModelError(nameof(carModel.ModelId), CarModelDoesntExist);
            }

            if (!await this.carService.CarTypeExistsAsync(carModel.CarTypeId))
            {
                this.ModelState.AddModelError(nameof(carModel.CarTypeId), CarTypeDoesntExist);
            }

            if (!await this.carService.FuelTypeExistsAsync(carModel.FuelId))
            {
                this.ModelState.AddModelError(nameof(carModel.FuelId), CarFuelTypeDoesntExist);
            }

            if (!await this.carService.TransmissionExistsAsync(carModel.TransmissionId))
            {
                this.ModelState.AddModelError(nameof(carModel.TransmissionId), CarTransmissionTypeDoesntExist);
            }

            if (!await this.carService.ColorExistsAsync(carModel.ColorId))
            {
                this.ModelState.AddModelError(nameof(carModel.ColorId), CarColorDoesntExist);
            }

            if (!ModelState.IsValid)
            {
                carModel.Brands = this.carCacheService.CacheCarBrands(CarBrandsCacheKey);
                carModel.CarModels = this.carCacheService.CacheCarModels(CarModelsCacheKey);
                carModel.Colors = this.carCacheService.CacheCarColors(CarColorsCacheKey);
                carModel.FuelTypes = this.carCacheService.CacheCarFuelTypes(CarFuelTypesCacheKey);
                carModel.Transmissions = this.carCacheService.CacheCarTransmissionTypes(CarTransmissionTypesCacheKey);
                carModel.CarTypes = this.carCacheService.CacheCarTypes(CarTypesCacheKey);
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

            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            if (companyId == 0 && !User.IsAdmin())
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

            var carForm = new CarFormModel()
            {
                Brands = this.carCacheService.CacheCarBrands(CarBrandsCacheKey),
                CarModels = this.carCacheService.CacheCarModels(CarModelsCacheKey),
                Colors = this.carCacheService.CacheCarColors(CarColorsCacheKey),
                FuelTypes = this.carCacheService.CacheCarFuelTypes(CarFuelTypesCacheKey),
                Transmissions = this.carCacheService.CacheCarTransmissionTypes(CarTransmissionTypesCacheKey),
                CarTypes = this.carCacheService.CacheCarTypes(CarTypesCacheKey),
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

            if (companyId == 0 && !this.User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            if (!await this.carService.IsCarFromCompanyAsync(id, companyId))
            {
                return NotFound();
            }

            if (!await this.carService.BrandExistsAsync(carModel.BrandId))
            {
                this.ModelState.AddModelError(nameof(carModel.BrandId), CarBrandDoesntExist);
            }

            if (!await this.carService.ModelExistsAsync(carModel.ModelId))
            {
                this.ModelState.AddModelError(nameof(carModel.ModelId), CarModelDoesntExist);
            }

            if (!await this.carService.CarTypeExistsAsync(carModel.CarTypeId))
            {
                this.ModelState.AddModelError(nameof(carModel.CarTypeId), CarTypeDoesntExist);
            }

            if (!await this.carService.FuelTypeExistsAsync(carModel.FuelId))
            {
                this.ModelState.AddModelError(nameof(carModel.FuelId), CarFuelTypeDoesntExist);
            }

            if (!await this.carService.TransmissionExistsAsync(carModel.TransmissionId))
            {
                this.ModelState.AddModelError(nameof(carModel.TransmissionId), CarTransmissionTypeDoesntExist);
            }

            if (!await this.carService.ColorExistsAsync(carModel.ColorId))
            {
                this.ModelState.AddModelError(nameof(carModel.ColorId), CarColorDoesntExist);
            }

            if (!ModelState.IsValid)
            {
                carModel.Brands = this.carCacheService.CacheCarBrands(CarBrandsCacheKey);
                carModel.CarModels = this.carCacheService.CacheCarModels(CarModelsCacheKey);
                carModel.Colors = this.carCacheService.CacheCarColors(CarColorsCacheKey);
                carModel.FuelTypes = this.carCacheService.CacheCarFuelTypes(CarFuelTypesCacheKey);
                carModel.Transmissions = this.carCacheService.CacheCarTransmissionTypes(CarTransmissionTypesCacheKey);
                carModel.CarTypes = this.carCacheService.CacheCarTypes(CarTypesCacheKey);
                carModel.Locations = this.locationService.GetCompanyLocations(companyId);

                return this.View(carModel);
            }

            if (!await this.carService.IsCarFromCompanyAsync(id, companyId) && !this.User.IsAdmin())
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

            if (!await this.carService.IsCarFromCompanyAsync(id, companyId))
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
            const int ItemsPerPage = 12;

            if (!this.companiesService.UserIsCompany(currentUserId))
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var model = new CarsSearchQueryModel()
            {
                Brand = query.Brand,
                Brands = this.carService.GetAddedByCompanyCarBrands(companyId),
                Cars = this.companiesService.AllCompanyCars(query.CurrentPage, ItemsPerPage, companyId),
                CarSorting = query.CarSorting,
                SearchTerm = query.SearchTerm,
                CurrentPage = query.CurrentPage,
                TotalCars = await this.carService.GetCompanyCarsCount(companyId),
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

            var startDate = this.dateTimeParserService.TryParseExact(model.PickUpDate);

            var endDate = this.dateTimeParserService.TryParseExact(model.ReturnDate);

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