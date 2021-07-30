namespace Oversteer.Web.Services.Cars
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Oversteer.Models.Cars;
    using Oversteer.Web.Data;
    using Oversteer.Web.Models.Cars;
    using Oversteer.Web.Models.Cars.Enumerations;
    using Oversteer.Web.Models.Home;
    using Oversteer.Web.Services.Contracts;

    public class CarsService : ICarsService
    {
        private readonly string[] AllowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CarsService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void CreateCar(CarFormModel carModel, int companyId, string imagePath)
        {
            var car = new Car()
            {
                CarBrandId = carModel.BrandId,
                CarModelId = carModel.ModelId,
                ColorId = carModel.ColorId,
                CarTypeId = carModel.CarTypeId,
                FuelId = carModel.FuelId,
                TransmissionId = carModel.TransmissionId,
                ModelYear = (int)carModel.Year,
                DailyPrice = (decimal)carModel.DailyPrice,
                SeatsCount = (int)carModel.SeatsCount,
                Description = carModel.Description,
                CompanyId = companyId
            };

            Directory.CreateDirectory($"{imagePath}/cars/");

            foreach (var image in carModel.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!AllowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new CarImage()
                {
                    CompanyId = companyId,
                    Extension = extension
                };

                car.CarImages.Add(dbImage);

                var physicalPath = $"{imagePath}/cars/{dbImage.Id}.{extension}";

                using Stream stream = new FileStream(physicalPath, FileMode.Create);
                image.CopyTo(stream);
            }

            this.data.Cars.Add(car);
            this.data.SaveChanges();
        }

        public void DeleteCar(int companyId, int carId)
        {
            var car = this.data.Cars
                               .Where(x => x.Id == carId && x.CompanyId == companyId)
                               .FirstOrDefault();
            car.IsDeleted = true;

            this.data.SaveChanges();
        }

        public bool EditCar(int carId, int brandId, int modelId, int colorId, int carTypeId, int fuelId, int transmissionId, int? year,
            decimal? dailyPrice, int? seatsCount, string imageUrl, string description)
        {
            var carData = this.data.Cars.Find(carId);

            if (carData == null)
            {
                return false;
            }

            carData.CarBrandId = brandId;
            carData.CarModelId = modelId;
            carData.ColorId = colorId;
            carData.CarTypeId = carTypeId;
            carData.FuelId = fuelId;
            carData.TransmissionId = transmissionId;
            carData.ModelYear = (int)year;
            carData.DailyPrice = (decimal)dailyPrice;
            carData.SeatsCount = (int)seatsCount;
            carData.Description = description;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<string> GetAddedCarBrands()
            => this.data.Cars
                    .Where(x => !x.IsDeleted)
                    .Select(c => c.Brand.Name)
                    .OrderBy(c => c)
                    .Distinct()
                    .ToList();

        public IEnumerable<string> GetAddedByCompanyCarBrands(int companyId)
            => this.data.Cars
            .Where(x => x.CompanyId == companyId && !x.IsDeleted)
            .Select(c => c.Brand.Name)
            .OrderBy(c => c)
            .Distinct()
            .ToList();

        public IEnumerable<ListCarFormModel> GetAllCars(CarsSearchQueryModel query)
            => this.SearchCar(query)
                .ToList();

        public IEnumerable<Car> GetCompanyCars(int companyId)
            => this.data.Cars
                .Where(x => x.CompanyId == companyId && !x.IsDeleted)
                .ToList();

        public CarDetailsFormModel GetCarDetails(int carId)
            => this.data.Cars
                .Where(x => x.Id == carId && !x.IsDeleted)
                .ProjectTo<CarDetailsFormModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public IEnumerable<CarIndexViewModel> GetThreeNewestCars()
            => this.data.Cars
                .Where(x => !x.IsDeleted)
                .ProjectTo<CarIndexViewModel>(this.mapper.ConfigurationProvider)
                .Take(3)
                .ToList();

        public IEnumerable<CarBrandFormModel> GetCarBrands()
        {
            var carBrands = this.data.CarBrands
                               .ToList();
            var brandsViewModel = this.mapper.Map<List<CarBrandFormModel>>(carBrands);

            return brandsViewModel;
        }

        public IEnumerable<CarModelFormModel> GetCarModels()
        {
            var cars = this.data.CarModels
                .ToList();
            var models = this.mapper.Map<List<CarModelFormModel>>(cars);

            return models;
        }

        public IEnumerable<ColorFormModel> GetCarColors()
        {
            var colors = this.data.Colors
                               .ToList();
            var colorModels = this.mapper.Map<List<ColorFormModel>>(colors);

            return colorModels;
        }

        public IEnumerable<FuelTypeFormModel> GetFuelTypes()
        {
            var fuels = this.data.Fuels
                               .ToList();
            var fuelModels = this.mapper.Map<List<FuelTypeFormModel>>(fuels);

            return fuelModels;
        }

        public IEnumerable<TransmissionTypeFormModel> GetTransmissionTypes()
        {
            var transmissions = this.data.Transmissions
                                        .ToList();

            var transmissionsModels = this.mapper.Map<List<TransmissionTypeFormModel>>(transmissions);

            return transmissionsModels;
        }

        public IEnumerable<CarTypeFormModel> GetCarTypes()
        {
            var carTypes = this.data.CarTypes
                               .ToList();

            var carTypesModels = this.mapper.Map<List<CarTypeFormModel>>(carTypes);

            return carTypesModels;
        }

        public int GetQueryCarsCount(CarsSearchQueryModel query)
            => this.QueryCars(query)
                    .Where(x => !x.IsDeleted)
                    .Count();

        public bool GetBrandId(int id)
        {
            if (!this.data.CarBrands.Any(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public bool GetModelId(int? id)
        {
            if (!this.data.CarModels.Any(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public bool GetColorId(int id)
        {
            if (!this.data.Colors.Any(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public bool GetFuelTypeId(int id)
        {
            if (!this.data.CarBrands.Any(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public bool GetTransmissionId(int id)
        {
            if (!this.data.Transmissions.Any(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public bool GetCarTypeId(int id)
        {
            if (!this.data.CarTypes.Any(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public bool IsCarFromCompany(int carId, int companyId)
            => this.data.Cars
                    .Any(x => x.Id == carId && x.CompanyId == companyId);

        private IEnumerable<ListCarFormModel> SearchCar(CarsSearchQueryModel query)
        {
            var carsQuery = this.QueryCars(query);

            var returnQuery = carsQuery
                .Skip((query.CurrentPage - 1) * CarsSearchQueryModel.CarsPerPage)
                .Take(CarsSearchQueryModel.CarsPerPage)
                .Where(x => !x.IsDeleted)
                .Select(x => new ListCarFormModel
                {
                    Id = x.Id,
                    BrandName = x.Brand.Name,
                    ModelName = x.Model.Name,
                    ModelYear = x.ModelYear,
                    CarTypeName = x.CarType.Name,
                    ColorName = x.Color.Name,
                    FuelName = x.Fuel.Name,
                    DailyPrice = x.DailyPrice,
                    TransmissionName = x.Transmission.Name,
                    Url = x.CarImages.FirstOrDefault().RemoteImageUrl ??
                          "/images/cars/" + x.CarImages.FirstOrDefault().Id + "." + x.CarImages.FirstOrDefault().Extension,
                    CompanyId = x.CompanyId
                }).ToList();

            return returnQuery;
        }

        private IQueryable<Car> QueryCars(CarsSearchQueryModel query)
        {
            var carsQuery = this.data.Cars
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                carsQuery = carsQuery.Where(x => x.Brand.Name == query.Brand);
            }

            var arg = string.Empty;

            if (!string.IsNullOrWhiteSpace(query.SearchTerm) && query.SearchTerm.Contains(' '))
            {
                var searchArgs = query.SearchTerm.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
                arg = searchArgs.Length >= 1 ? searchArgs[0] : query.SearchTerm;
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                carsQuery = carsQuery.Where(c =>
                       (c.Brand.Name + " " + c.Model.Name).ToLower().Contains(arg.ToLower()) ||
                       c.Description.ToLower().Contains(arg.ToLower()));
            }

            carsQuery = query.CarSorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(x => x.ModelYear),
                CarSorting.Brand => carsQuery.OrderBy(x => x.Brand.Name).ThenBy(x => x.Model.Name),
                CarSorting.DateCreated or _ => carsQuery.OrderByDescending(x => x.Id)
            };

            return carsQuery;
        }
    }
}
