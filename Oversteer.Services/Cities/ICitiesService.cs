namespace Oversteer.Services.Cities
{
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Cities;

    public interface ICitiesService
    {
        Task<int> CreateAsync(CityFormModel model, int countryId);

        Task<int> AddAddress(AddressFormModel model);

        bool CityIsInCountry(int countryId, string cityName);

        int GetCityIdByCountry(int countryId, string cityName);

        bool CityHasUser(string userId);
    }
}
