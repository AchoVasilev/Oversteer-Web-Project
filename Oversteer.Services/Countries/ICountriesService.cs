namespace Oversteer.Services.Countries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Countries;

    public interface ICountriesService
    {
        IEnumerable<CountryViewModel> GetCountries();

        Task AddCitiesToCountry(CountryFormModel model);

        int GetCountryId(string countryName);

        string GetCountryName(int countryId);
    }
}
