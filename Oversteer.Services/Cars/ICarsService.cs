namespace Oversteer.Services.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using Oversteer.Data.Models.Cars;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;
    using Oversteer.Web.ViewModels.Home;

    public interface ICarsService
    {
        Task CreateCarAsync(CarFormModel carModel, int companyId, string path);

        Task DeleteCarAsync(int companyId, int carId);

        Task<bool> EditCarAsync(int carId, int brandId, int modelId, int colorId, int carTypeId, int fuelId, int transmissionId, int? year,
            decimal? dailyPrice, int? seatsCount, string imageUrl, string description, IEnumerable<IFormFile> images, string imagePath, int companyId);

        Task<bool> IsRentedAsync(DateTime startDate, DateTime endDate, int carId);

        IEnumerable<CarModelFormModel> GetCarModels();

        IEnumerable<CarBrandFormModel> GetCarBrands();

        IEnumerable<ColorFormModel> GetCarColors();

        IEnumerable<FuelTypeFormModel> GetFuelTypes();

        IEnumerable<TransmissionTypeFormModel> GetTransmissionTypes();

        IEnumerable<CarTypeFormModel> GetCarTypes();

        IEnumerable<ListCarFormModel> GetAllCars(CarsSearchQueryModel query);

        IEnumerable<CarIndexViewModel> GetThreeNewestCars();

        IEnumerable<CarDto> GetAvailableCars(string startDate, string endDate, string location);

        IEnumerable<string> GetAddedCarBrands();

        IEnumerable<string> GetAddedByCompanyCarBrands(int companyId);

        CarDetailsFormModel GetCarDetails(int carId);

        int GetQueryCarsCount(CarsSearchQueryModel query);

        Task<int> GetCompanyByCar(int carId);

        Task<bool> RentCarAsync(DateTime startRent, DateTime endRent, int carId);

        Task<string> GetCarLocationAsync(int carId);

        Task<Car> GetCarById(int carId);

        int GetCompanyCarsCount(int companyId);

        bool GetBrandId(int id);

        bool GetModelId(int? id);

        bool GetColorId(int id);

        bool GetFuelTypeId(int id);

        bool GetTransmissionId(int id);

        bool GetCarTypeId(int id);

        bool IsCarFromCompany(int carId, int companyId);
    }
}
