namespace Oversteer.Web.Services.Contracts
{
    using System.Threading.Tasks;

    using Oversteer.Web.Models.Companies;

    public interface ICompaniesService
    {
        Task CreateCompanyAsync(CreateCompanyFormModel companyFormModel, string userId);

        bool UserIsCompany(string id);

        int GetCurrentCompanyId(string userId);

        string GetCompanyPhoneNumber(string userId);
    }
}
