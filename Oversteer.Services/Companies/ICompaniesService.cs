namespace Oversteer.Services.Companies
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Data.Models.Users;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Companies;

    public interface ICompaniesService
    {
        Task<Company> CreateCompanyAsync(CreateCompanyFormModel companyFormModel, string userId);

        Task<CompanyDetailsFormModel> DetailsAsync(int companyId);

        IEnumerable<ListCarFormModel> AllCompanyCars(int page, int itemsPerPage, int companyId);

        Task<List<string>> GetCompanyLocations(int companyId);

        Task<string> GetCompanyName(int companyId);

        bool UserIsCompany(string id);

        int GetCurrentCompanyId(string userId);

        string GetCompanyPhoneNumber(string userId);
    }
}
