namespace Oversteer.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Data.Models.Users;
    using Oversteer.Services.Companies;
    using Oversteer.Tests.Mock;
    using Oversteer.Web.ViewModels.Locations;

    using Xunit;

    public class LocationServiceTest
    {
        [Fact]
        public async Task GetLocationIdByNameShouldReturnCorrectResult()
        {
            using var data = DatabaseMock.Instance;

            data.Locations.Add(new Location { Id = 5, Name = "Kaspichan" });

            data.SaveChanges();

            var locationService = new LocationService(data, null, null, null);

            var result = await locationService.GetLocationIdByNameAsync("Kaspichan");

            Assert.Equal(5, result);
        }

        [Fact]
        public void GetAllLocationNamesShouldReturnCorrectCount()
        {
            using var data = DatabaseMock.Instance;

            data.Locations.Add(new Location { Id = 5, Name = "Kaspichan" });
            data.Locations.Add(new Location { Id = 6, Name = "Sofia" });
            data.Locations.Add(new Location { Id = 7, Name = "Varna" });

            data.SaveChanges();

            var locationService = new LocationService(data, null, null, null);

            var result = locationService.GetAllLocationNames();

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetCompanyLocationsShouldReturnCorrectCount()
        {
            var location = new Location
            {
                Id = 1,
                Name = "Bulgaria, Sofia, Mladost 4",
                CountryId = 1,
                CityId = 1,
                AddressId = 1,
                CompanyId = 1,
            };

            var company = new Company
            {
                Id = 1,
                Name = "PeshoCo"
            };

            var country = new Country
            {
                Id = 1,
                Name = "Bulgaria"
            };

            var city = new City
            {
                Id = 1,
                Name = "Sofia",
                CountryId = 1
            };

            var address = new Address
            {
                Id = 1,
                CityId = 1,
                Name = "Mladost 4"
            };

            var data = DatabaseMock.Instance;
            data.Countries.Add(country);
            data.Companies.Add(company);
            data.Cities.Add(city);
            data.Addresses.Add(address);
            data.Locations.Add(location);

            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var service = new LocationService(data, null, null, mapper);

            var count = service.GetCompanyLocations(1).Count();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task DeleteShouldSetIsDeletedToTrue()
        {
            var location = new Location
            {
                Id = 1,
                Name = "Bulgaria, Sofia, Mladost 4",
                CountryId = 1,
                CityId = 1,
                AddressId = 1,
                CompanyId = 1,
            };

            var data = DatabaseMock.Instance;

            data.Locations.Add(location);
            data.SaveChanges();

            var service = new LocationService(data, null, null, null);

            await service.DeleteLocationAsync(1);

            var deleted = data.Locations.Find(1);

            Assert.True(deleted.IsDeleted);
        }

        [Fact]
        public async Task GetCurrentLocationShouldReturnCorrectLocation()
        {
            var location = new Location
            {
                Id = 1,
                Name = "Bulgaria, Sofia, Mladost 4",
                CountryId = 1,
                CityId = 1,
                AddressId = 1,
                CompanyId = 1,
            };

            var company = new Company
            {
                Id = 1,
                Name = "PeshoCo"
            };

            var country = new Country
            {
                Id = 1,
                Name = "Bulgaria"
            };

            var city = new City
            {
                Id = 1,
                Name = "Sofia",
                CountryId = 1
            };

            var address = new Address
            {
                Id = 1,
                CityId = 1,
                Name = "Mladost 4"
            };

            var data = DatabaseMock.Instance;
            data.Countries.Add(country);
            data.Companies.Add(company);
            data.Cities.Add(city);
            data.Addresses.Add(address);
            data.Locations.Add(location);

            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var service = new LocationService(data, null, null, mapper);

            var model = await service.GetCurrentLocationAsync(1);

            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task EditLocationShouldReturnTrueWhenSuccessful()
        {
            var location = new Location
            {
                Id = 1,
                Name = "Bulgaria, Sofia, Mladost 4",
                CountryId = 1,
                CityId = 1,
                AddressId = 1,
                CompanyId = 1,
            };

            var company = new Company
            {
                Id = 1,
                Name = "PeshoCo"
            };

            var country = new Country
            {
                Id = 1,
                Name = "Bulgaria"
            };

            var city = new City
            {
                Id = 1,
                Name = "Sofia",
                CountryId = 1
            };

            var address = new Address
            {
                Id = 1,
                CityId = 1,
                Name = "Mladost 4"
            };

            var data = DatabaseMock.Instance;
            data.Countries.Add(country);
            data.Companies.Add(company);
            data.Cities.Add(city);
            data.Addresses.Add(address);
            data.Locations.Add(location);

            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var service = new LocationService(data, null, null, mapper);

            var result = await service.EditLocationAsync(1, 1, "Kaspichan", "Varbitsa 5");

            Assert.True(result);

            var edited = data.Locations.Find(1);

            Assert.Equal("Kaspichan", edited.City.Name);
        }

        [Fact]
        public async Task EditShouldReturnFalseIfLocationIsNull()
        {
            var data = DatabaseMock.Instance;

            var service = new LocationService(data, null, null, null);

            var result = await service.EditLocationAsync(5, 3, "asd", "pasd");

            Assert.False(result);
        }

        [Fact]
        public void GetAllLocationsShouldReturnCorrectCountAndModel()
        {
            var location = new Location
            {
                Id = 1,
                Name = "Bulgaria, Sofia, Mladost 4",
                CountryId = 1,
                CityId = 1,
                AddressId = 1,
                CompanyId = 1,
            };

            var company = new Company
            {
                Id = 1,
                Name = "PeshoCo"
            };

            var country = new Country
            {
                Id = 1,
                Name = "Bulgaria"
            };

            var city = new City
            {
                Id = 1,
                Name = "Sofia",
                CountryId = 1
            };

            var address = new Address
            {
                Id = 1,
                CityId = 1,
                Name = "Mladost 4"
            };

            var data = DatabaseMock.Instance;
            data.Countries.Add(country);
            data.Companies.Add(company);
            data.Cities.Add(city);
            data.Addresses.Add(address);
            data.Locations.Add(location);

            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var service = new LocationService(data, null, null, mapper);

            var model = service.GetAllLocations();

            Assert.Equal(1, model.Count);

            Assert.IsType<List<LocationFormModel>>(model);
        }

        [Fact]
        public async Task LocationIsFromCompanyShouldReturnTrueIfLocationIsFromCompany()
        {
            var location = new Location
            {
                Id = 1,
                Name = "Bulgaria, Sofia, Mladost 4",
                CountryId = 1,
                CityId = 1,
                AddressId = 1,
                CompanyId = 1,
            };

            var company = new Company
            {
                Id = 1,
                Name = "PeshoCo"
            };

            var country = new Country
            {
                Id = 1,
                Name = "Bulgaria"
            };

            var city = new City
            {
                Id = 1,
                Name = "Sofia",
                CountryId = 1
            };

            var address = new Address
            {
                Id = 1,
                CityId = 1,
                Name = "Mladost 4"
            };

            var data = DatabaseMock.Instance;
            data.Countries.Add(country);
            data.Companies.Add(company);
            data.Cities.Add(city);
            data.Addresses.Add(address);
            data.Locations.Add(location);

            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var service = new LocationService(data, null, null, mapper);

            var model = await service.LocationIsFromCompanyAsync(1, 1);

            Assert.True(model);
        }
    }
}
