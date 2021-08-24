namespace Oversteer.Services.Caches
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.Caching.Memory;

    using Oversteer.Services.Cars;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;

    public class CarsCacheService : ICarCacheService
    {
        private readonly ICarsService carsService;
        private readonly IMemoryCache memoryCache;

        public CarsCacheService(ICarsService carsService, IMemoryCache memoryCache)
        {
            this.carsService = carsService;
            this.memoryCache = memoryCache;
        }

        public IEnumerable<CarBrandFormModel> CacheCarBrands(string key)
        {
            var carsBrands = this.memoryCache.Get<IEnumerable<CarBrandFormModel>>(key);

            if (carsBrands == null)
            {
                carsBrands = this.carsService.GetCarBrands();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                var cached = this.memoryCache.Set(key, carsBrands, cacheOptions);
            }

            return carsBrands;
        }

        public IEnumerable<CarModelFormModel> CacheCarModels(string key)
        {
            var carsModels = this.memoryCache.Get<IEnumerable<CarModelFormModel>>(key);

            if (carsModels == null)
            {
                carsModels = this.carsService.GetCarModels();

                var chacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.memoryCache.Set(key, carsModels, chacheOptions);
            }

            return carsModels;
        }

        public IEnumerable<ColorFormModel> CacheCarColors(string key)
        {
            var colors = this.memoryCache.Get<IEnumerable<ColorFormModel>>(key);

            if (colors == null)
            {
                colors = this.carsService.GetCarColors();

                var chacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.memoryCache.Set(key, colors, chacheOptions);
            }

            return colors;
        }

        public IEnumerable<FuelTypeFormModel> CacheCarFuelTypes(string key)
        {
            var models = this.memoryCache.Get<IEnumerable<FuelTypeFormModel>>(key);

            if (models == null)
            {
                models = this.carsService.GetFuelTypes();

                var chacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.memoryCache.Set(key, models, chacheOptions);
            }

            return models;
        }

        public IEnumerable<TransmissionTypeFormModel> CacheCarTransmissionTypes(string key)
        {
            var models = this.memoryCache.Get<IEnumerable<TransmissionTypeFormModel>>(key);

            if (models == null)
            {
                models = this.carsService.GetTransmissionTypes();

                var chacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.memoryCache.Set(key, models, chacheOptions);
            }

            return models;
        }

        public IEnumerable<CarTypeFormModel> CacheCarTypes(string key)
        {
            var models = this.memoryCache.Get<IEnumerable<CarTypeFormModel>>(key);

            if (models == null)
            {
                models = this.carsService.GetCarTypes();

                var chacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.memoryCache.Set(key, models, chacheOptions);
            }

            return models;
        }
    }
}
