namespace Oversteer.Web.Services.Companies
{
    using System.Linq;
    using System.Threading.Tasks;

    using Oversteer.Models.Users;
    using Oversteer.Web.Data;
    using Oversteer.Web.Models.Companies;
    using Oversteer.Web.Services.Contracts;

    public class CompaniesService : ICompaniesService
    {
        private readonly ApplicationDbContext data;

        public CompaniesService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task CreateCompanyAsync(CreateCompanyFormModel companyFormModel, string userId)
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

            await this.data.Companies.AddAsync(company);
            await this.data.SaveChangesAsync();
        }

        public bool UserIsCompany(string id)
        {
            if (!this.data.Companies.Any(x => x.UserId == id))
            {
                return false;
            }

            return true;
        }

        public int GetCurrentCompanyId(string userId)
            => this.data.Companies
                    .Where(x => x.UserId == userId)
                    .Select(x => x.Id)
                    .FirstOrDefault();

        public string GetCompanyPhoneNumber(string userId)
            => this.data.Companies
                    .Where(x => x.UserId == userId)
                    .Select(x => x.PhoneNumber)
                    .FirstOrDefault();
    }
}
