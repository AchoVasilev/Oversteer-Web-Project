namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Caches;
    using Oversteer.Services.Cars;
    using Oversteer.Services.Companies;
    using Oversteer.Web.Extensions;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.ViewModels.Cars;
    using static Oversteer.Data.Common.Constants.WebConstants.Caching;
    using static Oversteer.Data.Common.Constants.ErrorMessages;

    public class CarsController : AdministrationController
    {
        private readonly ICarsService carsService;
        private readonly ICarCacheService carCacheService;
        private readonly ILocationService locationService;
        private readonly ICompaniesService companiesService;

        public CarsController(
            ICarsService carsService, 
            ICarCacheService carCacheService, 
            ILocationService locationService, 
            ICompaniesService companiesService)
        {
            this.carsService = carsService;
            this.carCacheService = carCacheService;
            this.locationService = locationService;
            this.companiesService = companiesService;
        }

        public async Task<IActionResult> All([FromQuery] CarsSearchQueryModel query)
            => this.View(new CarsSearchQueryModel()
            {
                Brand = query.Brand,
                Brands = this.carsService.GetAddedCarBrands(),
                Cars = this.carsService.GetAllCars(query),
                CarSorting = query.CarSorting,
                SearchTerm = query.SearchTerm,
                CurrentPage = query.CurrentPage,
                TotalCars = await this.carsService.GetQueryCarsCounAsync(query)
            });

        public IActionResult Edit(int id, string information)
        {
            var car = this.carsService.GetCarDetails(id);

            if (information != car.ToFriendlyUrl())
            {
                return NotFound();
            }

            var companyId = car.CompanyId;

            var carForm = new CarFormModel()
            {
                Brands = this.carCacheService.CacheCarBrands(CarBrandsCacheKey),
                CarModels = this.carCacheService.CacheCarModels(CarModelsCacheKey),
                Colors = this.carCacheService.CacheCarColors(CarColorsCacheKey),
                FuelTypes = this.carsService.GetFuelTypes(),
                Transmissions = this.carsService.GetTransmissionTypes(),
                CarTypes = this.carsService.GetCarTypes(),
                Locations = this.locationService.GetCompanyLocations(companyId)
            };

            return this.View(carForm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CarFormModel carModel)
        {
            var currentUserId = this.User.GetId();

            if (!this.carsService.GetBrandId(carModel.BrandId))
            {
                this.ModelState.AddModelError(nameof(carModel.BrandId), CarBrandDoesntExist);
            }

            if (!this.carsService.GetModelId(carModel.ModelId))
            {
                this.ModelState.AddModelError(nameof(carModel.ModelId), CarModelDoesntExist);
            }

            if (!this.carsService.GetCarTypeId(carModel.CarTypeId))
            {
                this.ModelState.AddModelError(nameof(carModel.CarTypeId), CarTypeDoesntExist);
            }

            if (!this.carsService.GetFuelTypeId(carModel.FuelId))
            {
                this.ModelState.AddModelError(nameof(carModel.FuelId), CarFuelTypeDoesntExist);
            }

            if (!this.carsService.GetTransmissionId(carModel.TransmissionId))
            {
                this.ModelState.AddModelError(nameof(carModel.TransmissionId), CarTransmissionTypeDoesntExist);
            }

            if (!this.carsService.GetColorId(carModel.ColorId))
            {
                this.ModelState.AddModelError(nameof(carModel.ColorId), CarColorDoesntExist);
            }

            var companyId = this.companiesService.GetCurrentCompanyId(currentUserId);

            if (!ModelState.IsValid)
            {
                carModel.Brands = this.carCacheService.CacheCarBrands(CarBrandsCacheKey);
                carModel.CarModels = this.carCacheService.CacheCarModels(CarModelsCacheKey);
                carModel.Colors = this.carCacheService.CacheCarColors(CarColorsCacheKey);
                carModel.FuelTypes = this.carsService.GetFuelTypes();
                carModel.Transmissions = this.carsService.GetTransmissionTypes();
                carModel.CarTypes = this.carsService.GetCarTypes();
                carModel.Locations = this.locationService.GetCompanyLocations(companyId);

                return this.View(carModel);
            }

            if (!this.carsService.IsCarFromCompany(id, companyId) && !User.IsAdmin())
            {
                return this.NotFound();
            }

            try
            {
                await this.carsService.EditCarAsync(
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

            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var companyId = await this.carsService.GetCompanyByCarAsync(id);
            await this.carsService.DeleteCarAsync(companyId, id);

            this.TempData["Message"] = "The car was removed successfully.";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
