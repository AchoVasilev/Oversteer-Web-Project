namespace Oversteer.Services.Companies
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Locations;

    public interface ILocationService
    {
        Task AddLocationAsync(int companyId, CreateLocationFormModel model);

        Task<int> GetLocationIdByNameAsync(string name);

        Task DeleteLocationAsync(int locationId);

        Task<LocationFormModel> GetCurrentLocationAsync(int locationId);

        Task<bool> EditLocationAsync(int locationId, int countryId, string cityName, string addressName);

        Task<bool> LocationIsFromCompanyAsync(int companyId, int locationId);

        ICollection<string> GetAllLocationNames();

        IEnumerable<LocationFormModel> GetCompanyLocations(int companyId);

        ICollection<LocationFormModel> GetAllLocations();
    }
}
