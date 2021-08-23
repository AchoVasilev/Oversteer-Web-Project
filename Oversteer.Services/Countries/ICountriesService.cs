namespace Oversteer.Services.Countries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Countries;

    public interface ICountriesService
    {
        IEnumerable<CountryViewModel> GetCountries();

        Task AddCityToCountryAsync(CountryFormModel model);

        Task AddCountryAsync(CountryViewModel model);

        Task<bool> EditCountryAsync(int id, string name);

        Task<bool> DeleteCountryAsync(int id);

        Task<string> GetCountryName(int countryId);
    }
}
