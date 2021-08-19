namespace Oversteer.Tests.Services
{
    using System.Threading.Tasks;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Services.Companies;
    using Oversteer.Tests.Mocks;

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
    }
}
