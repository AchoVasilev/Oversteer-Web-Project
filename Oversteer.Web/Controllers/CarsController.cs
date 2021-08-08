﻿namespace Oversteer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Web.Areas.Company.Controllers;
    using Oversteer.Web.Areas.Company.Services.Companies;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.Models.Cars;
    using Oversteer.Web.Services.Cars;

    using static Oversteer.Web.Data.Constants.ErrorMessages;

    public class CarsController : Controller
    {
        private readonly ICarsService carService;
        private readonly ICompaniesService companiesService;
        private readonly IWebHostEnvironment environment;

        public CarsController(ICarsService carService, ICompaniesService companiesService, IWebHostEnvironment environment)
        {
            this.carService = carService;
            this.companiesService = companiesService;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.GetId();

            if (!this.companiesService.UserIsCompany(userId))
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            return this.View(new CarFormModel()
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
        public async Task<IActionResult> Add(CarFormModel carModel)
        {
            var currentUserId = this.User.GetId();

            var companyId = this.companiesService.GetCurrentCompanyId(currentUserId);

            if (companyId == 0)
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

                return this.View(carModel);
            }

            try
            {
                await this.carService.CreateCarAsync(carModel, companyId, $"{environment.WebRootPath}/images");
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
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.companiesService.UserIsCompany(userId) && !User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var car = this.carService.GetCarDetails(id);

            if (car.CompanyUserId != userId && !User.IsAdmin())
            {
                return this.Unauthorized();
            }

            var carForm = new CarFormModel()
            {
                Brands = this.carService.GetCarBrands(),
                CarModels = this.carService.GetCarModels(),
                Colors = this.carService.GetCarColors(),
                FuelTypes = this.carService.GetFuelTypes(),
                Transmissions = this.carService.GetTransmissionTypes(),
                CarTypes = this.carService.GetCarTypes(),
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

            if (!this.carService.IsCarFromCompany(id, companyId) && !User.IsAdmin())
            {
                return this.BadRequest();
            }

            var imagePath = $"{environment.WebRootPath}/images";

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
                    imagePath,
                    companyId
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

            if (!this.companiesService.UserIsCompany(userId) && !User.IsAdmin())
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
            }

            var companyId = this.companiesService.GetCurrentCompanyId(userId);

            await this.carService.DeleteCarAsync(companyId, id);

            this.TempData["Message"] = "The car was removed successfully.";

            return this.RedirectToAction(nameof(this.MyCars));
        }

        [Authorize]
        public IActionResult MyCars([FromQuery] CarsSearchQueryModel query)
        {
            var currentUserId = this.User.GetId();

            var companyId = this.companiesService.GetCurrentCompanyId(currentUserId);

            if (!this.companiesService.UserIsCompany(currentUserId))
            {
                return this.RedirectToAction(nameof(CompaniesController.Create), "Companies", new { area = "Company" });
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

        public IActionResult ById()
        {
            return this.View();
        }
    }
}
