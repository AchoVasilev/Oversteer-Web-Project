namespace Oversteer.Services.Companies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;
    using Oversteer.Data.Models.Rentals;
    using Oversteer.Services.Cities;
    using Oversteer.Web.ViewModels.Locations;
    using Oversteer.Web.ViewModels.Cities;
    using Oversteer.Web.ViewModels.Countries;
    using Oversteer.Services.Countries;

    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext data;
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countriesService;
        private readonly IMapper mapper;

        public LocationService(
            ApplicationDbContext data,
            ICitiesService citiesService,
            ICountriesService countriesService,
            IMapper mapper)
        {
            this.data = data;
            this.citiesService = citiesService;
            this.countriesService = countriesService;
            this.mapper = mapper;
        }

        public async Task AddLocationAsync(int companyId, CreateLocationFormModel model)
        {
            var cityExists = await citiesService.CityIsInCountry(model.CountryId, model.CityName);

            var countryFormModel = new CountryFormModel()
            {
                Id = model.CountryId,
                City = new CityFormModel()
                {
                    Name = model.CityName,
                }
            };

            var cityId = cityExists ? await this.citiesService.GetCityIdByCountry(model.CountryId, model.CityName)
                                     : await this.citiesService.CreateAsync(countryFormModel.City, model.CountryId);

            await this.countriesService.AddCityToCountryAsync(countryFormModel);

            var addressFormModel = new AddressFormModel()
            {
                Name = model.Address,
                CityId = cityId
            };

            var countryName = await this.countriesService.GetCountryName(model.CountryId);

            var addressId = await this.citiesService.AddAddressAsync(addressFormModel);

            var locationName = string.Join(", ", countryName, model.CityName, model.Address);

            var location = new Location()
            {
                Name = locationName,
                CityId = cityId,
                AddressId = addressId,
                CountryId = model.CountryId,
                CompanyId = companyId
            };

            await this.data.Locations.AddAsync(location);
            await this.data.SaveChangesAsync();
        }

        public ICollection<string> GetAllLocationNames()
        {
            return this.data.Locations
                .Where(x => !x.IsDeleted)
                .Select(x => x.Name)
                .ToList();
        }

        public IEnumerable<LocationFormModel> GetCompanyLocations(int companyId)
            => this.data.Locations
                    .Where(x => x.CompanyId == companyId && !x.IsDeleted)
                    .ProjectTo<LocationFormModel>(this.mapper.ConfigurationProvider)
                    .ToList();

        public async Task<int> GetLocationIdByNameAsync(string name)
            => await this.data.Locations
                        .Where(x => x.Name.Contains(name) && !x.IsDeleted)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();

        public async Task DeleteLocationAsync(int locationId)
        {
            var location = await this.data.Locations
                                        .Where(x => x.Id == locationId && !x.IsDeleted)
                                        .FirstOrDefaultAsync();

            location.IsDeleted = true;
            location.DeletedOn = DateTime.UtcNow;

            await this.data.SaveChangesAsync();
        }

        public async Task<LocationFormModel> GetCurrentLocationAsync(int locationId)
            => await this.data.Locations
                        .Where(x => x.Id == locationId && !x.IsDeleted)
                        .ProjectTo<LocationFormModel>(this.mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

        public async Task<bool> EditLocationAsync(int locationId, int countryId, string cityName, string addressName)
        {
            var location = await this.data.Locations
                                    .Where(x => x.Id == locationId && !x.IsDeleted)
                                    .FirstOrDefaultAsync();

            if (location is null)
            {
                return false;
            }

            location.CountryId = countryId;
            location.City.Name = cityName;
            location.Address.Name = addressName;

            await this.data.SaveChangesAsync();

            return true;
        }

        public ICollection<LocationFormModel> GetAllLocations()
            => this.data.Locations
                        .Where(x => !x.IsDeleted)
                        .ProjectTo<LocationFormModel>(this.mapper.ConfigurationProvider)
                        .ToListAsync()
                        .GetAwaiter()
                        .GetResult();

        public async Task<bool> LocationIsFromCompanyAsync(int companyId, int locationId)
            => await this.data.Locations
                        .AnyAsync(x => x.Id == locationId && x.CompanyId == companyId);

    }
}
