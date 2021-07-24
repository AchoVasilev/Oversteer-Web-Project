﻿namespace Oversteer.Web.Services.Contracts
{
    using System.Collections.Generic;

    using Oversteer.Models.Cars;
    using Oversteer.Web.Models.Cars;
    using Oversteer.Web.Models.Home;

    public interface ICarsService
    {
        void CreateCar(CarFormModel carModel, int companyId);

        void DeleteCar(int companyId, int carId);

        bool EditCar(int carId, int brandId, int modelId, int colorId, int carTypeId, int fuelId, int transmissionId, int? year,
            decimal? dailyPrice, int? seatsCount, string imageUrl, string description);

        IEnumerable<Car> GetCompanyCars(int companyId);

        IEnumerable<CarModelFormModel> GetCarModels();

        IEnumerable<CarBrandFormModel> GetCarBrands();

        IEnumerable<ColorFormModel> GetCarColors();

        IEnumerable<FuelTypeFormModel> GetFuelTypes();

        IEnumerable<TransmissionTypeFormModel> GetTransmissionTypes();

        IEnumerable<CarTypeFormModel> GetCarTypes();

        IEnumerable<ListCarFormModel> GetAllCars(CarsSearchQueryModel query);

        IEnumerable<CarIndexViewModel> GetThreeNewestCars();

        IEnumerable<string> GetAddedCarBrands();

        IEnumerable<string> GetAddedByCompanyCarBrands(int companyId);

        CarDetailsFormModel GetCarDetails(int carId);

        int GetQueryCarsCount(CarsSearchQueryModel query);

        bool GetBrandId(int id);

        bool GetModelId(int? id);

        bool GetColorId(int id);

        bool GetFuelTypeId(int id);

        bool GetTransmissionId(int id);

        bool GetCarTypeId(int id);

        bool IsCarFromCompany(int carId, int companyId);
    }
}
