namespace Oversteer.Services.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using CloudinaryDotNet;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;
    using Oversteer.Data.Models.Cars;
    using Oversteer.Services.DateTime;
    using Oversteer.Services.Images;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;
    using Oversteer.Web.ViewModels.Cars.Enumerations;
    using Oversteer.Web.ViewModels.Home;

    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;
        private readonly IImageService imageService;
        private readonly IDateTimeParserService dateTimeParserService;
        private readonly Cloudinary cloudinary;

        public CarsService(
            ApplicationDbContext data, 
            IMapper mapper, 
            IImageService imageService, 
            Cloudinary cloudinary, 
            IDateTimeParserService dateTimeParserService)
        {
            this.data = data;
            this.mapper = mapper;
            this.imageService = imageService;
            this.cloudinary = cloudinary;
            this.dateTimeParserService = dateTimeParserService;
        }

        public async Task CreateCarAsync(CarFormModel carModel, int companyId)
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
                CompanyId = companyId,
                LocationId = carModel.LocationId,
                CarFeatures = carModel.CarFeatures.Select(x => new CarFeature()
                {
                    Name = x.Name
                }).ToList()
            };

            await this.imageService.UploadImage(cloudinary, carModel.Images, companyId, car);

            await this.data.Cars.AddAsync(car);
            await this.data.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int companyId, int carId)
        {
            var car = this.data.Cars
                               .Where(x => x.Id == carId && x.CompanyId == companyId)
                               .FirstOrDefault();

            car.IsDeleted = true;
            car.DeleteDate = DateTime.UtcNow;

            await this.data.SaveChangesAsync();
        }

        public async Task<bool> EditCarAsync(int carId, int brandId, int modelId, int colorId, int carTypeId, int fuelId, int transmissionId, int? year,
            decimal? dailyPrice, int? seatsCount, string imageUrl, string description, IEnumerable<IFormFile> images, int companyId, IEnumerable<CarFeatureFormModel> features)
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

            foreach (var image in carData.CarImages)
            {
                DeleteImage(image);
            }

            foreach (var feature in carData.CarFeatures)
            {
                DeleteFeature(feature);
            }

            carData.CarFeatures = features.Select(x => new CarFeature()
            {
                Name = x.Name
            }).ToList();

            await this.imageService.UploadImage(cloudinary, images, companyId, carData);

            await this.data.SaveChangesAsync();

            return true;
        }

        public IEnumerable<CarDto> GetAvailableCars(string startDate, string endDate, string location)
        {
            var dates = new List<DateTime>();

            var startRentDate = this.dateTimeParserService.TryParseExact(startDate);
            var returnRentDate = this.dateTimeParserService.TryParseExact(endDate);

            for (var date = startRentDate.Date; date <= returnRentDate.Date; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            var cars = this.data.Cars
                            .Where(x => x.Location.Name == location)
                            .Where(x => x.RentDays.Any(y => dates.Contains(y.RentDate)) == false)
                            .Where(x => !x.IsDeleted)
                            .Select(x => new CarDto()
                            {
                                Id = x.Id,
                                Image = x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().RemoteImageUrl ??
                                        x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Url ??
                                        "/images/cars/" + x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Id + "." + x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Extension,
                                Description = x.Description,
                                TransmissionName = x.Transmission.Name,
                                DailyPrice = x.DailyPrice,
                                BrandName = x.Brand.Name,
                                ModelName = x.Model.Name,
                                ModelYear = x.ModelYear,
                                CompanyId = x.CompanyId,
                                CompanyName = x.Company.Name,
                                Days = dates.Count,
                                StartRent = startDate,
                                EndRent = endDate
                            }).ToList();

            return cars;
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

        public CarDetailsFormModel GetCarDetails(int carId)
        {
            var details = new CarDetailsFormModel();

            var images = this.data.Cars
                .Where(x => x.Id == carId)
                .Select(x => x.CarImages.Where(x => !x.IsDeleted).ToList())
                .ToList();

            var car = this.data.Cars
                           .Where(x => x.Id == carId && !x.IsDeleted)
                           .ProjectTo<CarDetailsFormModel>(this.mapper.ConfigurationProvider)
                           .FirstOrDefault();

            foreach (var image in images)
            {
                foreach (var img in image)
                {
                    var imagePath = img.Url ?? $"/images/cars/{img.Id}.{img.Extension}";

                    car.Urls.Add(imagePath);
                }
            }

            return car;
        }

        public async Task<bool> RentCarAsync(DateTime startRent, DateTime endRent, int carId)
        {
            var dates = new List<CarRentDays>();
            for (var dt = startRent; dt <= endRent; dt = dt.AddDays(1))
            {
                dates.Add(new CarRentDays
                {
                    CarId = carId,
                    RentDate = dt,
                });
            }

            var car = await this.GetCarByIdAsync(carId);

            if (car == null)
            {
                return false;
            }

            car.IsAvailable = false;

            await this.data.CarRentDays.AddRangeAsync(dates);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsRentedAsync(DateTime startDate, DateTime endDate, int carId)
        {
            var dates = new List<DateTime>();

            for (var i = startDate; i <= endDate; i = i.AddDays(1))
            {
                dates.Add(i);
            }

            foreach (var date in dates)
            {
                if (await this.data.CarRentDays.AnyAsync(x => x.CarId == carId && x.RentDate == date))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<string> GetCarLocationAsync(int carId)
            => await this.data.Cars
                        .Where(x => x.Id == carId)
                        .Select(x => x.Location.Name)
                        .FirstOrDefaultAsync();

        public List<CarIndexViewModel> GetThreeNewestCars()
            => this.data.Cars
                .Where(x => !x.IsDeleted)
                .ProjectTo<CarIndexViewModel>(this.mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Id)
                .Take(3)
                .ToList();

        public IEnumerable<CarBrandFormModel> GetCarBrands()
        {
            var carBrands = this.data.CarBrands
                               .Where(x => !x.IsDeleted)
                               .ToList();

            var brandsViewModel = this.mapper.Map<List<CarBrandFormModel>>(carBrands);

            return brandsViewModel;
        }

        public async Task<int> GetCompanyIdByCarIdAsync(int carId)
            => await this.data.Cars
                            .Where(x => x.Id == carId)
                            .Select(x => x.CompanyId)
                            .FirstOrDefaultAsync();

        public IEnumerable<CarModelFormModel> GetCarModels()
        {
            var cars = this.data.CarModels
                .Where(x => !x.IsDeleted)
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

        public async Task<int> GetQueryCarsCounAsync(CarsSearchQueryModel query)
            => await this.QueryCars(query)
                        .Where(x => !x.IsDeleted)
                        .CountAsync();

        public async Task<int> GetCompanyCarsCount(int companyId)
            => await this.data.Cars
                .Where(x => x.CompanyId == companyId && !x.IsDeleted)
                .CountAsync();

        public async Task<CarDto> GetCarByIdAsync(int carId)
            => await this.data.Cars
                        .Where(x => x.Id == carId && !x.IsDeleted)
                        .ProjectTo<CarDto>(this.mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

        public async Task<bool> BrandExistsAsync(int id)
        {
            if (!await this.data.CarBrands.AnyAsync(x => x.Id == id && !x.IsDeleted))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ModelExistsAsync(int? id)
        {
            if (!await this.data.CarModels.AnyAsync(x => x.Id == id && !x.IsDeleted))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ColorExistsAsync(int id)
        {
            if (!await this.data.Colors.AnyAsync(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> FuelTypeExistsAsync(int id)
        {
            if (!await this.data.Fuels.AnyAsync(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> TransmissionExistsAsync(int id)
        {
            if (!await this.data.Transmissions.AnyAsync(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CarTypeExistsAsync(int id)
        {
            if (!await this.data.CarTypes.AnyAsync(x => x.Id == id))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsCarFromCompanyAsync(int carId, int companyId)
            => await this.data.Cars
                        .AnyAsync(x => x.Id == carId && x.CompanyId == companyId);

        public async Task<bool> ChangeLocationAsync(int id, int returnLocationId)
        {
            var car = this.data.Cars.Find(id);

            if (car is null)
            {
                return false;
            }

            car.LocationId = returnLocationId;

            await this.data.SaveChangesAsync();

            return true;
        }

        private IEnumerable<ListCarFormModel> SearchCar(CarsSearchQueryModel query)
        {
            var carsQuery = this.QueryCars(query);

            var returnQuery = carsQuery
                .Skip((query.CurrentPage - 1) * CarsSearchQueryModel.CarsPerPage)
                .Take(CarsSearchQueryModel.CarsPerPage)
                .Where(x => !x.IsDeleted)
                .ProjectTo<ListCarFormModel>(this.mapper.ConfigurationProvider).ToList();

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
                var searchArgs = query.SearchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries);
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

        private void DeleteImage(CarImage image)
        {
            image.IsDeleted = true;
            image.ModifiedOn = DateTime.UtcNow;
        }

        private void DeleteFeature(CarFeature feature)
        {
            feature.IsDeleted = true;
            feature.DeletedOn = DateTime.UtcNow;
        }
    }
}