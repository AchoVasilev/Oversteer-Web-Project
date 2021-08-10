namespace Oversteer.Services.Countries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

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
                .OrderBy(x => x.Name)
                .ToList();
            var countriesFromModel = this.mapper.Map<IEnumerable<CountryViewModel>>(countries);

            return countriesFromModel;
        }

        public async Task AddCitiesToCountry(CountryFormModel model)
        {
            var country = this.data.Countries
                .Where(x => x.Id == model.Id)
                .FirstOrDefault();

            if (country == null)
            {
                return;
            }

            var cityName = await this.citiesService.CreateAsync(model.City, model.Id);

            foreach (var countryCity in country.Cities)
            {
                countryCity.ZipCodes.Add(new ZipCode()
                {
                    Code = model.City.ZipCode.Code
                });
            }

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
    }
}
