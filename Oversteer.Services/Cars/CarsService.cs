﻿namespace Oversteer.Services.Cars
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;
    using Oversteer.Data.Models.Cars;
    using Oversteer.Web.Infrastructure;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;
    using Oversteer.Web.ViewModels.Cars.Enumerations;
    using Oversteer.Web.ViewModels.Home;

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

        public async Task CreateCarAsync(CarFormModel carModel, int companyId, string imagePath)
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
                LocationId = carModel.LocationId
            };

            Directory.CreateDirectory($"{imagePath}/cars/");

            await this.UploadImages(carModel.Images, companyId, imagePath, car);

            await this.data.Cars.AddAsync(car);
            await this.data.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int companyId, int carId)
        {
            var car = this.data.Cars
                               .Where(x => x.Id == carId && x.CompanyId == companyId)
                               .FirstOrDefault();
            car.IsDeleted = true;

            await this.data.SaveChangesAsync();
        }

        public async Task<bool> EditCarAsync(int carId, int brandId, int modelId, int colorId, int carTypeId, int fuelId, int transmissionId, int? year,
            decimal? dailyPrice, int? seatsCount, string imageUrl, string description, IEnumerable<IFormFile> images, string imagePath, int companyId)
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
            carData.CarImages.Clear();

            await this.UploadImages(images, companyId, imagePath, carData);

            await this.data.SaveChangesAsync();

            return true;
        }

        public IEnumerable<CarDto> GetAvailableCars(string startDate, string endDate, string location)
        {
            var dates = new List<DateTime>();

            var startRentDate = DateTimeParser.ParseDate(startDate);
            var returnRentDate = DateTimeParser.ParseDate(endDate);

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
                                Image = x.CarImages.FirstOrDefault().RemoteImageUrl ??
                                        "/images/cars/" + x.CarImages.FirstOrDefault().Id + "." + x.CarImages.FirstOrDefault().Extension,
                                Description = x.Description,
                                TransmissionName = x.Transmission.Name,
                                DailyPrice = x.DailyPrice,
                                BrandName = x.Brand.Name,
                                ModelName = x.Model.Name,
                                ModelYear = x.ModelYear,
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
                .Select(x => x.CarImages)
                .ToList();

            var car = this.data.Cars
                           .Where(x => x.Id == carId && !x.IsDeleted)
                           .ProjectTo<CarDetailsFormModel>(this.mapper.ConfigurationProvider)
                           .FirstOrDefault();

            foreach (var image in images)
            {
                foreach (var img in image)
                {
                    var imagePath = "/images/cars/" + img.Id + "." + img.Extension;

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

            var car = await this.GetCarById(carId);
            car.IsAvailable = false;

            await this.data.CarRentDays.AddRangeAsync(dates);
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsRentedAsync(DateTime startDate, DateTime endDate, int carId)
        {
            var dates = new List<DateTime>();

            for (var i = startDate; i <= endDate; i.AddDays(1))
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

        public async Task<int> GetCompanyByCar(int carId)
            => await this.data.Cars
                           .Where(x => x.Id == carId)
                            .Select(x => x.CompanyId)
                            .FirstOrDefaultAsync();

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

        public int GetCompanyCarsCount(int companyId)
            => this.data.Cars
                .Where(x => x.CompanyId == companyId)
                .Count();

        public async Task<Car> GetCarById(int carId)
            => await this.data.Cars
                        .Where(x => x.Id == carId)
                        .FirstOrDefaultAsync();

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

        private async Task UploadImages(IEnumerable<IFormFile> images, int companyId, string imagePath, Car car)
        {
            foreach (var image in images)
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
                await image.CopyToAsync(stream);
            }
        }
    }
}