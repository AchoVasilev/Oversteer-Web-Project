namespace Oversteer.Web.MappingProfile
{
    using AutoMapper;

    using Oversteer.Data.Models.Others;
    using Oversteer.Data.Models.Rentals;
    using Oversteer.Data.Models.Users;
    using Oversteer.Web.ViewModels.Companies;
    using Oversteer.Web.ViewModels.Feedbacks;
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

            this.CreateMap<Location, LocationDto>();

            this.CreateMap<Feedback, FeedbackViewModel>();

            this.CreateMap<Feedback, FeedbackDto>();

            this.CreateMap<Feedback, ListFeedbackModel>();
        }
    }
}
