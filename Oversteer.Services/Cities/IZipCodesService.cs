namespace Oversteer.Services.Cities
{
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Cities;

    public interface IZipCodesService
    {
        Task<int> CreateAsync(ZipCodeFormModel model, int cityId);

        bool ZipCodeIsInCity(int cityId, int zipCode);

        int GetZipCodeIdByCity(int cityId, int? zipCode);

        bool ZipHasUser(string userId);
    }
}
