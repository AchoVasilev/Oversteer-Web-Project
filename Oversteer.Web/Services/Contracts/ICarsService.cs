namespace Oversteer.Web.Services.Contracts
{
    using System.Collections.Generic;

    using Oversteer.Models.Cars;
    using Oversteer.Web.Models.Cars;
    using Oversteer.Web.Models.Home;

    public interface ICarsService
    {
        IEnumerable<Car> GetCompanyCars(int companyId);

        void CreateCar(AddCarFormModel carModel, int companyId);

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
    }
}
