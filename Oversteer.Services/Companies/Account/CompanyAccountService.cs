namespace Oversteer.Services.Companies.Account
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;

    public class CompanyAccountService : ICompanyAccountService
    {
        private readonly ApplicationDbContext data;

        public CompanyAccountService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> SetCompanyName(string userId, string name)
        {
            var company = await this.data.Companies
                                  .Where(x => x.UserId == userId)
                                  .FirstOrDefaultAsync();

            if (company == null)
            {
                return false;
            }

            company.Name = name;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetCompanyDescription(string userId, string description)
        {
            var company = await this.data.Companies
                                  .Where(x => x.UserId == userId)
                                  .FirstOrDefaultAsync();

            if (company == null)
            {
                return false;
            }

            company.Description = description;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetPhoneNumberAsync(int companyId, string phoneNumber)
        {
            var company = await this.data.Companies
                        .Where(x => x.Id == companyId)
                        .FirstOrDefaultAsync();

            if (company is null)
            {
                return false;
            }

            company.PhoneNumber = phoneNumber;

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
