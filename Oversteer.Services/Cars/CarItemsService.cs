namespace Oversteer.Services.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;
    using Oversteer.Data.Models.Cars;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;

    public class CarItemsService : ICarItemsService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CarItemsService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task AddBrandAsync(CarBrandFormModel brand)
        {
            var carBrand = new CarBrand()
            {
                Name = brand.Name
            };

            await this.data.CarBrands.AddAsync(carBrand);
            await this.data.SaveChangesAsync();
        }

        public IEnumerable<CarBrandFormModel> GetAllBrandsAsync()
            => this.data.CarBrands
            .ProjectTo<CarBrandFormModel>(this.mapper.ConfigurationProvider)
            .ToListAsync()
            .GetAwaiter()
            .GetResult();

        public async Task<bool> BrandExistsAsync(int brandId)
            => await this.data.CarBrands
                            .AnyAsync(x => x.Id == brandId);

        public async Task<CarBrand> GetCarBrandAsync(int brandId)
            => await this.data.CarBrands
                              .Where(x => x.Id == brandId && !x.IsDeleted)
                              .FirstOrDefaultAsync();

        public async Task<bool> DeleteBrandAsync(int brandId)
        {
            var brand = await this.GetCarBrandAsync(brandId);

            if (brand == null)
            {
                return false;
            }

            brand.IsDeleted = true;
            brand.DeletedOn = DateTime.UtcNow;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditBrandAsync(CarBrandFormModel model)
        {
            var brand = await this.GetCarBrandAsync(model.Id);

            if (brand == null)
            {
                return false;
            }

            brand.Name = model.Name;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<string> GetBrandNameAsync(int brandId)
            => await this.data.CarBrands
                        .Where(x => x.Id == brandId && !x.IsDeleted)
                        .Select(x => x.Name)
                        .FirstOrDefaultAsync();

        //Car Models

        public async Task AddModelAsync(CarModelFormModel model)
        {
            var carModel = new CarModel()
            {
                Name = model.Name,
                CarBrandId = model.CarBrandId
            };

            await this.data.CarModels.AddAsync(carModel);
            await this.data.SaveChangesAsync();
        }

        public IEnumerable<CarModelFormModel> GetAllModelsAsync(int brandId)
            => this.data.CarModels
                        .Where(x => x.CarBrandId == brandId && !x.IsDeleted)
                        .ProjectTo<CarModelFormModel>(this.mapper.ConfigurationProvider)
                        .ToListAsync()
                        .GetAwaiter()
                        .GetResult();

        public async Task<CarModel> GetCarModelAsync(int modelId)
            => await this.data.CarModels
                        .Where(x => x.Id == modelId && !x.IsDeleted)
                        .FirstOrDefaultAsync();

        public async Task<bool> EditModelAsync(CarModelFormModel model)
        {
            var carModel = await this.GetCarModelAsync(model.Id);

            if (carModel == null)
            {
                return false;
            }

            carModel.Name = model.Name;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteModelAsync(int modelId)
        {
            var model = await this.GetCarModelAsync(modelId);

            if (model == null)
            {
                return false;
            }

            model.IsDeleted = true;
            model.DeletedOn = DateTime.UtcNow;

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
