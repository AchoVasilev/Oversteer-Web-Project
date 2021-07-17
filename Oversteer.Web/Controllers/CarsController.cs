namespace Oversteer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Web.Models.Cars;
    using Oversteer.Web.Models.Cars.Enumerations;
    using Oversteer.Web.Services.Contracts;

    using static Oversteer.Web.Data.Constants.ErrorMessages;

    public class CarsController : Controller
    {
        private readonly ICarsService carService;

        public CarsController(ICarsService carService)
        {
            this.carService = carService;
        }

        public IActionResult Add() => this.View(new AddCarFormModel()
        {
            Brands = this.carService.GetCarBrands(),
            CarModels = this.carService.GetCarModels(),
            Colors = this.carService.GetCarColors(),
            FuelTypes = this.carService.GetFuelTypes(),
            Transmissions = this.carService.GetTransmissionTypes(),
            CarTypes = this.carService.GetCarTypes(),
        });

        [HttpPost]
        public IActionResult Add(AddCarFormModel carModel)
        {
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
                carModel.Colors = this.carService.GetCarColors();
                carModel.FuelTypes = this.carService.GetFuelTypes();
                carModel.Transmissions = this.carService.GetTransmissionTypes();
                carModel.CarTypes = this.carService.GetCarTypes();

                return this.View(carModel);
            }

            this.carService.CreateCar(carModel);

            return RedirectToAction(nameof(All));
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
    }
}
