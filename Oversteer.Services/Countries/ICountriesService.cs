namespace Oversteer.Services.Countries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Countries;

    public interface ICountriesService
    {
        IEnumerable<CountryViewModel> GetCountries();

        Task AddCitiesToCountry(CountryFormModel model);

        Task AddCountry(CountryViewModel model);

        Task<bool> EditCountry(int id, string name);

        Task<bool> DeleteCountry(int id);

        int GetCountryId(string countryName);

        string GetCountryName(int countryId);
    }
}
