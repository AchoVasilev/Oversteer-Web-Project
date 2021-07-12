namespace Oversteer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Oversteer.Web.Models.Cars;
    using Oversteer.Web.Services.Contracts;

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
            Colors = this.carService.GetCarColors(),
            FuelTypes = this.carService.GetFuelTypes()
        });

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            return this.View();
        }
    }
}
