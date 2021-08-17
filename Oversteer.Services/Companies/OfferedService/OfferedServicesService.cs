namespace Oversteer.Services.Companies
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;
    using Oversteer.Data.Models.Users;
    using Oversteer.Services.Companies.OfferedService;
    using Oversteer.Web.ViewModels.Companies;

    public class OfferedServicesService : IOfferedServicesService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public OfferedServicesService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<CompanyServiceFormModel> GetAll(int companyId)
            => this.data.CompanyServices
                .Where(x => x.CompanyId == companyId && !x.IsDeleted)
                .ProjectTo<CompanyServiceFormModel>(this.mapper.ConfigurationProvider)
                .ToListAsync()
                .GetAwaiter()
                .GetResult();

        public async Task<bool> CompanyHasServiceAsync(int serviceId, int companyId)
            => await this.data.CompanyServices
                                .AnyAsync(x => x.Id == serviceId && x.CompanyId == companyId && !x.IsDeleted);

        public async Task<bool> DeleteServiceAsync(int serviceId)
        {
            var service = await this.data.CompanyServices
                                        .FirstOrDefaultAsync(x => x.Id == serviceId);

            if (service is null)
            {
                return false;
            }

            service.IsDeleted = true;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task AddServicesAsync(CompanyServiceFormModel service, int companyId)
        {
            var company = await this.data.Companies.FirstOrDefaultAsync(x => x.Id == companyId);

            company.CompanyServices.Add(new CompanyService()
            {
                Name = service.Name
            });

            await this.data.SaveChangesAsync();
        }

        public async Task<CompanyServiceFormModel> GetServiceAsync(int serviceId, int companyId)
            => await this.data.CompanyServices
                              .Where(x => x.Id == serviceId && x.CompanyId == companyId && !x.IsDeleted)
                              .ProjectTo<CompanyServiceFormModel>(this.mapper.ConfigurationProvider)
                              .FirstOrDefaultAsync();
    }
}
