namespace Oversteer.Services.Cities
{
    using System.Linq;
    using System.Threading.Tasks;

    using Oversteer.Data;
    using Oversteer.Data.Models.Users;
    using Oversteer.Web.ViewModels.Cities;

    public class CitiesService : ICitiesService
    {
        private readonly ApplicationDbContext data;

        public CitiesService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<int> CreateAsync(CityFormModel model, int countryId)
        {
            var city = new City
            {
                CountryId = countryId,
                Name = model.Name
            };

            await this.data.Cities.AddAsync(city);
            await this.data.SaveChangesAsync();

            return city.Id;
        }

        public bool CityIsInCountry(int countryId, string cityName)
        {
            var exists = this.data.Cities
                .Any(x => x.CountryId == countryId && x.Name == cityName);

            if (!exists)
            {
                return false;
            }

            return true;
        }

        public int GetCityIdByCountry(int countryId, string cityName)
            => this.data.Cities
                    .Where(x => x.CountryId == countryId && x.Name == cityName)
                    .Select(x => x.Id)
                    .FirstOrDefault();

        public bool CityHasUser(string userId)
        {
            var cities = this.data.Cities.ToList();

            foreach (var city in cities)
            {
                var userHasCity = city.Users.Any(x => x.Id == userId);

                if (userHasCity == true)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<int> AddAddress(AddressFormModel model)
        {
            var address = new Address()
            {
                Name = model.Name,
                CityId = model.CityId
            };

            await this.data.Addresses.AddAsync(address);
            await this.data.SaveChangesAsync();

            return address.Id;
        }
    }
}
