namespace Oversteer.Services.Companies.OfferedService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Web.ViewModels.Companies;

    public interface IOfferedServicesService
    {
        Task<bool> CompanyHasServiceAsync(int serviceId, int companyId);

        Task<bool> DeleteServiceAsync(int serviceId);

        Task AddServicesAsync(CompanyServiceFormModel services, int companyId);

        Task<CompanyServiceFormModel> GetServiceAsync(int serviceId, int companyId);

        IEnumerable<CompanyServiceFormModel> GetAll(int companyId);
    }
}
