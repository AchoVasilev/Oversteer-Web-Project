namespace Oversteer.Services.Cars
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Data.Models.Cars;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;

    public interface ICarItemsService
    {
        Task AddBrandAsync(CarBrandFormModel brand);

        Task<bool> BrandExistsAsync(int brandId);

        Task<bool> DeleteBrandAsync(int brandId);

        Task<bool> EditBrandAsync(CarBrandFormModel model);

        Task AddModelAsync(CarModelFormModel model);

        Task<string> GetBrandNameAsync(int brandId);

        Task<CarModel> GetCarModelAsync(int modelId);

        Task<bool> EditModelAsync(CarModelFormModel model);

        Task<bool> DeleteModelAsync(int modelId);

        IEnumerable<CarModelFormModel> GetAllModelsAsync(int brandId);

        IEnumerable<CarBrandFormModel> GetAllBrandsAsync();
    }
}
