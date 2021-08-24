namespace Oversteer.Services.Cities
{
    using System.Threading.Tasks;

    using Oversteer.Data.Models.Users;
    using Oversteer.Web.ViewModels.Cities;

    public interface ICitiesService
    {
        Task<int> CreateAsync(CityFormModel model, int countryId);

        Task<int> AddAddressAsync(AddressFormModel model);

        Task<City> GetCityByIdAsync(int cityId);

        Task<bool> EditCityAsync(int cityId, string name);

        Task<Address> GetAddressByIdAsync(int addressId);

        Task<bool> EditAddressAsync(int addressId, string name);

        Task<bool> CityIsInCountryAsync(int countryId, string cityName);

        Task<int> GetCityIdByCountryAsync(int countryId, string cityName);
    }
}
