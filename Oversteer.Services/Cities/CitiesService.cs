namespace Oversteer.Services.Cities
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

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
                Name = model.Name,
            };

            await this.data.Cities.AddAsync(city);
            await this.data.SaveChangesAsync();

            return city.Id;
        }

        public async Task<bool> CityIsInCountryAsync(int countryId, string cityName) 
            => await this.data.Cities
                    .AnyAsync(x => x.CountryId == countryId && x.Name == cityName);

        public async Task<int> GetCityIdByCountryAsync(int countryId, string cityName)
            => await this.data.Cities
                            .Where(x => x.CountryId == countryId && x.Name == cityName)
                            .Select(x => x.Id)
                            .FirstOrDefaultAsync();

        public async Task<int> AddAddressAsync(AddressFormModel model)
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

        public async Task<City> GetCityByIdAsync(int cityId)
            => await this.data.Cities
                                .Where(x => x.Id == cityId)
                                .FirstOrDefaultAsync();

        public async Task<bool> EditCityAsync(int cityId, string name)
        {
            var city = await this.GetCityByIdAsync(cityId);

            if (city == null)
            {
                return false;
            }

            city.Name = name;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
            => await this.data.Addresses
                                .Where(x => x.Id == addressId)
                                .FirstOrDefaultAsync();

        public async Task<bool> EditAddressAsync(int addressId, string name)
        {
            var address = await this.GetAddressByIdAsync(addressId);

            if (address == null)
            {
                return false;
            }

            address.Name = name;

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
