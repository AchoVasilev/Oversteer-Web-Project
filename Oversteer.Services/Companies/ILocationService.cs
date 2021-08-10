namespace Oversteer.Services.Companies
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Locations;

    public interface ILocationService
    {
        Task AddLocationAsync(int companyId, CreateLocationFormModel model);

        IEnumerable<LocationFormModel> GetCompanyLocations(int companyId);

        Task<int> GetLocationIdByNameAsync(string name);

        ICollection<string> GetAllLocationNames();
    }
}
