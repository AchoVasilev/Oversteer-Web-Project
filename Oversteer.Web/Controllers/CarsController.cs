using Microsoft.AspNetCore.Mvc;
namespace Oversteer.Web.Controllers
{
    using Oversteer.Web.Models.Cars;
    public class CarsController : Controller
    {
        public IActionResult Add() => this.View();

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            return this.View();
        }
    }
}
