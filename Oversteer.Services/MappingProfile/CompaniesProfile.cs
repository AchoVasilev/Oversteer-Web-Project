namespace Oversteer.Services.MappingProfile
{
    using AutoMapper;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Data.Models.Users;
    using Oversteer.Web.ViewModels.Companies;
    using Oversteer.Web.ViewModels.Locations;

    public class CompaniesProfile : Profile
    {
        public CompaniesProfile()
        {
            this.CreateMap<Company, CompanyDetailsFormModel>()
                .ForMember(x => x.CompanyServices, opt => opt.MapFrom(x => x.CompanyServices))
                .ForMember(x => x.Locations, opt => opt.MapFrom(x => x.Locations));

            this.CreateMap<CompanyService, CompanyServiceFormModel>();
            this.CreateMap<Location, CompanyLocationFormModel>();
            this.CreateMap<Location, LocationFormModel>();
        }
    }
}
