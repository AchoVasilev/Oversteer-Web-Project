﻿namespace Oversteer.Web.Services.Companies
{
    using System.Linq;

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

        public void CreateCompany(CreateCompanyFormModel companyFormModel, string userId)
        {
            var company = new Company()
            {
                Name = companyFormModel.CompanyName,
                PhoneNumber = companyFormModel.PhoneNumber,
                UserId = userId
            };

            this.data.Companies.Add(company);
            this.data.SaveChanges();
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
