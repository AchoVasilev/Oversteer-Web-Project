namespace Oversteer.Services.MappingProfile
{
    using AutoMapper;

    using Oversteer.Data.Models.Users;
    using Oversteer.Web.ViewModels.Countries;

    public class CountriesProfile : Profile
    {
        public CountriesProfile()
        {
            this.CreateMap<Country, CountryViewModel>();
        }
    }
}
