namespace Oversteer.Services.Caches
{
    using System.Collections.Generic;

    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;

    public interface ICarCacheService
    {
        IEnumerable<CarBrandFormModel> CacheCarBrands(string key);

        IEnumerable<CarModelFormModel> CacheCarModels(string key);

        IEnumerable<ColorFormModel> CacheCarColors(string key);

        IEnumerable<FuelTypeFormModel> CacheCarFuelTypes(string key);

        IEnumerable<TransmissionTypeFormModel> CacheCarTransmissionTypes(string key);

        IEnumerable<CarTypeFormModel> CacheCarTypes(string key);
    }
}
