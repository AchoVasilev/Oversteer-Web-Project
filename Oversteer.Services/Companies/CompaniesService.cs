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
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Companies;

    public class CompaniesService : ICompaniesService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;
        public CompaniesService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<Company> CreateCompanyAsync(CreateCompanyFormModel companyFormModel, string userId)
        {
            var company = new Company()
            {
                Name = companyFormModel.CompanyName,
                PhoneNumber = companyFormModel.PhoneNumber,
                UserId = userId,
                Description = companyFormModel.Description,
                CompanyServices = companyFormModel.CompanyServices.Select(x => new CompanyService()
                {
                    Name = x.Name
                }).ToList()
            };

            await data.Companies.AddAsync(company);
            await data.SaveChangesAsync();

            return company;
        }

        public async Task<CompanyDetailsFormModel> DetailsAsync(int companyId)
            => await this.data.Companies
                        .Where(x => x.Id == companyId)
                        .ProjectTo<CompanyDetailsFormModel>(this.mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

        public IEnumerable<ListCarFormModel> AllCompanyCars(int page, int itemsPerPage, int companyId)
        {
            return this.data.Cars
                                .Where(x => !x.IsDeleted && x.CompanyId == companyId)
                                .AsQueryable()
                                .OrderBy(x => x.Id)
                                .Skip((page - 1) * itemsPerPage)
                                .Take(itemsPerPage)
                                .ProjectTo<ListCarFormModel>(this.mapper.ConfigurationProvider)
                                .ToList();
        }

        public async Task<List<string>> GetCompanyLocations(int companyId)
            => await this.data.Locations
                            .Where(x => x.CompanyId == companyId)
                            .Select(x => x.Name)
                            .ToListAsync();

        public async Task<string> GetCompanyName(int companyId)
            => await this.data.Companies
                            .Where(x => x.Id == companyId)
                            .Select(x => x.Name)
                            .FirstOrDefaultAsync();


        public bool UserIsCompany(string id)
        {
            if (!data.Companies.Any(x => x.UserId == id))
            {
                return false;
            }

            return true;
        }

        public int GetCurrentCompanyId(string userId)
            => data.Companies
                    .Where(x => x.UserId == userId)
                    .Select(x => x.Id)
                    .FirstOrDefault();

        public string GetCompanyPhoneNumber(string userId)
            => data.Companies
                    .Where(x => x.UserId == userId)
                    .Select(x => x.PhoneNumber)
                    .FirstOrDefault();
    }
}
