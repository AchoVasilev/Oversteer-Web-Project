namespace Oversteer.Tests.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Data.Models.Users;
    using Oversteer.Services.Companies;
    using Oversteer.Tests.Mocks;

    using Xunit;

    public class CompanyServiceTest
    {
        [Fact]
        public void UserIsCompanyShouldReturnTrueIfUserIsCompany()
        {
            using var data = DatabaseMock.Instance;

            data.Companies.Add(new Company { UserId = "TestUserId" } );

            data.SaveChanges();

            var companyService = new CompaniesService(data, null);

            var result = companyService.UserIsCompany("TestUserId");

            Assert.True(result);
        }

        [Fact]
        public void UserIsCompanyShouldReturnFalseIfUserIsNotCompany()
        {
            using var data = DatabaseMock.Instance;

            data.Companies.Add(new Company { UserId = "TestUserId" });

            data.SaveChanges();

            var companyService = new CompaniesService(data, null);

            var result = companyService.UserIsCompany("Penko");

            Assert.False(result);
        }

        [Fact]
        public void GetCurrentCompanyIdShouldReturnCorrectId()
        {
            using var data = DatabaseMock.Instance;

            data.Companies.Add(new Company { Id = 5, UserId = "TestUserId" });

            data.SaveChanges();

            var companyService = new CompaniesService(data, null);

            var result = companyService.GetCurrentCompanyId("TestUserId");

            Assert.Equal(5, result);
        }

        [Fact]
        public void GetCompanyPhoneNumberShouldReturnCorrectNumber()
        {
            using var data = DatabaseMock.Instance;

            data.Companies.Add(new Company { PhoneNumber = "0988878263", UserId = "TestUserId" });

            data.SaveChanges();

            var companyService = new CompaniesService(data, null);

            var result = companyService.GetCompanyPhoneNumber("TestUserId");

            Assert.Equal("0988878263", result);
        }

        [Fact]
        public async Task GetCompanyNameShouldReturnCorrectName()
        {
            using var data = DatabaseMock.Instance;

            data.Companies.Add(new Company { Id = 5, Name = "Gosho", UserId = "TestUserId" });

            data.SaveChanges();

            var companyService = new CompaniesService(data, null);

            var result = await companyService.GetCompanyName(5);

            Assert.Equal("Gosho", result);
        }

        [Fact]
        public async Task GetCompanyLocationsShouldReturnCorrectCount()
        {
            using var data = DatabaseMock.Instance;

            data.Companies.Add(new Company { Id = 5, Locations = new List<Location> { new Location { Name = "Kaspichan" }, new Location { Name = "Sofia" } } });

            data.SaveChanges();

            var companyService = new CompaniesService(data, null);

            var result = await companyService.GetCompanyLocations(5);

            Assert.Equal(2, result.Count);
        }
    }
}
