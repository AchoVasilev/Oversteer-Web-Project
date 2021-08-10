namespace Oversteer.Services.Companies
{
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
            var cityExists = citiesService.CityIsInCountry(model.CountryId, model.CityName);

            var countryFormModel = new CountryFormModel()
            {
                Id = model.CountryId,
                City = new CityFormModel()
                {
                    Name = model.CityName,
                    ZipCode = new ZipCodeFormModel()
                    {
                        Code = model.ZipCode
                    }
                }
            };

            var cityId = cityExists ? citiesService.GetCityIdByCountry(model.CountryId, model.CityName)
                                     : await citiesService.CreateAsync(countryFormModel.City, model.CountryId);

            await countriesService.AddCitiesToCountry(countryFormModel);

            var addressFormModel = new AddressFormModel()
            {
                Name = model.Address,
                CityId = cityId
            };

            var countryName = countriesService.GetCountryName(model.CountryId);

            var addressId = await citiesService.AddAddress(addressFormModel);

            var locationName = string.Join(", ", countryName, model.CityName, model.Address, model.ZipCode);

            var location = new Location()
            {
                Name = locationName,
                CityId = cityId,
                AddressId = addressId,
                CountryId = model.CountryId,
                CompanyId = companyId
            };

            await data.Locations.AddAsync(location);
            await data.SaveChangesAsync();
        }

        public ICollection<string> GetAllLocationNames()
        {
            return this.data.Locations
                .Select(x => x.Name)
                .ToList();
        }

        public IEnumerable<LocationFormModel> GetCompanyLocations(int companyId)
            => data.Locations
                    .Where(x => x.CompanyId == companyId)
                    .ProjectTo<LocationFormModel>(this.mapper.ConfigurationProvider)
                    .ToList();

        public async Task<int> GetLocationIdByNameAsync(string name)
            => await this.data.Locations
                        .Where(x => x.Name.Contains(name))
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();
    }
}
