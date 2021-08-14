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

        ICollection<string> GetAllLocationNames();

        IEnumerable<LocationFormModel> GetCompanyLocations(int companyId);

        IEnumerable<LocationFormModel> AllLocations(int companyId);
    }
}
