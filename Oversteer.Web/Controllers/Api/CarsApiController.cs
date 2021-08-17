namespace Oversteer.Web.Controllers.Api
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Cars;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;

    using static Oversteer.Data.Common.Constants.WebConstants.ApiRoutes;

    [ApiController]
    [Route(CarsApiControllerRoute)]
    public class CarsApiController : ControllerBase
    {
        private readonly ICarsService carsService;

        public CarsApiController(ICarsService carsService)
            => this.carsService = carsService;

        [HttpGet]
        [Route(CarsApiControllerBrandsRoute)]
        public IEnumerable<CarBrandFormModel> GetAllBrands()
            => this.carsService.GetCarBrands();

        [HttpGet]
        [Route(CarsApiControllerModelsRoute)]
        public IEnumerable<CarModelFormModel> GetAllModels()
            => this.carsService.GetCarModels();
    }
}
