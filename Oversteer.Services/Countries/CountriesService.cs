namespace Oversteer.Services.Countries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;
    using Oversteer.Data.Models.Users;
    using Oversteer.Services.Cities;
    using Oversteer.Web.ViewModels.Countries;

    public class CountriesService : ICountriesService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;
        private readonly ICitiesService citiesService;

        public CountriesService(ApplicationDbContext data, IMapper mapper, ICitiesService citiesService)
        {
            this.data = data;
            this.mapper = mapper;
            this.citiesService = citiesService;
        }

        public IEnumerable<CountryViewModel> GetCountries()
        {
            var countries = this.data.Countries
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToList();

            var countriesFromModel = this.mapper.Map<IEnumerable<CountryViewModel>>(countries);

            return countriesFromModel;
        }

        public async Task AddCitiesToCountry(CountryFormModel model)
        {
            var country = this.data.Countries
                .Where(x => x.Id == model.Id && !x.IsDeleted)
                .FirstOrDefault();

            if (country == null)
            {
                return;
            }

            var cityName = await this.citiesService.CreateAsync(model.City, model.Id);

            this.data.Update(country);
            await this.data.SaveChangesAsync();
        }

        public int GetCountryId(string countryName)
            => this.data.Countries
                    .Where(x => x.Name == countryName)
                    .Select(x => x.Id)
                    .FirstOrDefault();

        public string GetCountryName(int countryId)
            => this.data.Countries
                .Where(x => x.Id == countryId)
                .Select(x => x.Name)
                .FirstOrDefault();

        public async Task<Country> GetCountry(int countryId)
            => await this.data.Countries
                        .Where(x => x.Id == countryId && !x.IsDeleted)
                        .FirstOrDefaultAsync();

        public async Task AddCountry(CountryViewModel model)
        {
            var country = new Country()
            {
                Name = model.Name
            };

            await this.data.Countries.AddAsync(country);

            await this.data.SaveChangesAsync();
        }

        public async Task<bool> EditCountry(int id, string name)
        {
            var country = await this.GetCountry(id);

            if (country == null)
            {
                return false;
            }

            country.Name = name;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCountry(int id)
        {
            var country = await this.GetCountry(id);

            if (country == null)
            {
                return false;
            }

            country.IsDeleted = true;
            country.DeletedOn = DateTime.UtcNow;

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
