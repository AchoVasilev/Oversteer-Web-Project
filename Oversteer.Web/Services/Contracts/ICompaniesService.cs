namespace Oversteer.Web.Services.Contracts
{
    using Oversteer.Web.Models.Companies;

    public interface ICompaniesService
    {
        void CreateCompany(CreateCompanyFormModel companyFormModel, string userId);

        bool UserIsCompany(string id);

        int GetCurrentCompanyId(string userId);
    }
}
