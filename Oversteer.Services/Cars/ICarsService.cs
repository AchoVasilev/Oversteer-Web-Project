namespace Oversteer.Services.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;
    using Oversteer.Web.ViewModels.Home;

    public interface ICarsService
    {
        Task CreateCarAsync(CarFormModel carModel, int companyId);

        Task DeleteCarAsync(int companyId, int carId);

        Task<bool> EditCarAsync(int carId, int brandId, int modelId, int colorId, int carTypeId, int fuelId, int transmissionId, int? year,
            decimal? dailyPrice, int? seatsCount, string imageUrl, string description, IEnumerable<IFormFile> images, int companyId, IEnumerable<CarFeatureFormModel> features);

        Task<bool> IsRentedAsync(DateTime startDate, DateTime endDate, int carId);

        Task<int> GetCompanyIdByCarIdAsync(int carId);

        Task<bool> RentCarAsync(DateTime startRent, DateTime endRent, int carId);

        Task<string> GetCarLocationAsync(int carId);

        Task<CarDto> GetCarByIdAsync(int carId);

        Task<bool> ChangeLocationAsync(int id, int returnLocationId);

        Task<int> GetQueryCarsCounAsync(CarsSearchQueryModel query);

        IEnumerable<CarModelFormModel> GetCarModels();

        IEnumerable<CarBrandFormModel> GetCarBrands();

        IEnumerable<ColorFormModel> GetCarColors();

        IEnumerable<FuelTypeFormModel> GetFuelTypes();

        IEnumerable<TransmissionTypeFormModel> GetTransmissionTypes();

        IEnumerable<CarTypeFormModel> GetCarTypes();

        IEnumerable<ListCarFormModel> GetAllCars(CarsSearchQueryModel query);

        List<CarIndexViewModel> GetThreeNewestCars();

        IEnumerable<CarDto> GetAvailableCars(string startDate, string endDate, string location);

        IEnumerable<string> GetAddedCarBrands();

        IEnumerable<string> GetAddedByCompanyCarBrands(int companyId);

        CarDetailsFormModel GetCarDetails(int carId);

        Task<int> GetCompanyCarsCount(int companyId);

        Task<bool> BrandExistsAsync(int id);

        Task<bool> ModelExistsAsync(int? id);

        Task<bool> ColorExistsAsync(int id);

        Task<bool> FuelTypeExistsAsync(int id);

        Task<bool> TransmissionExistsAsync(int id);

        Task<bool> CarTypeExistsAsync(int id);

        Task<bool> IsCarFromCompanyAsync(int carId, int companyId);
    }
}
