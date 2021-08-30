namespace Oversteer.Web.MappingProfile
{
    using System.Linq;

    using AutoMapper;

    using Oversteer.Data.Models.Cars;
    using Oversteer.Data.Models.Others;
    using Oversteer.Data.Models.Rentals;
    using Oversteer.Data.Models.Users;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;
    using Oversteer.Web.ViewModels.Clients;
    using Oversteer.Web.ViewModels.Companies;
    using Oversteer.Web.ViewModels.Countries;
    using Oversteer.Web.ViewModels.Feedbacks;
    using Oversteer.Web.ViewModels.Home;
    using Oversteer.Web.ViewModels.Locations;
    using Oversteer.Web.ViewModels.Rents;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Cars
            this.CreateMap<CarBrand, CarBrandFormModel>();

            this.CreateMap<CarModel, CarModelFormModel>();

            this.CreateMap<Color, ColorFormModel>();

            this.CreateMap<Fuel, FuelTypeFormModel>();

            this.CreateMap<Transmission, TransmissionTypeFormModel>();

            this.CreateMap<CarType, CarTypeFormModel>();

            this.CreateMap<Car, CarDetailsFormModel>()
                .ForMember(x => x.Url, opt =>
                    opt.MapFrom(x => x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().RemoteImageUrl ??
                          x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Url ??
                          "/images/cars/" + x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Id + "." + x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Extension));

            this.CreateMap<Car, CarIndexViewModel>()
                .ForMember(x => x.Url, opt =>
                    opt.MapFrom(x => x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().RemoteImageUrl ??
                          x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Url ??
                          "/images/cars/" + x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Id + "." + x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Extension));

            this.CreateMap<Car, ListCarFormModel>()
                .ForMember(x => x.Url, opt =>
                    opt.MapFrom(x => x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().RemoteImageUrl ??
                          x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Url ??
                          "/images/cars/" + x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Id + "." + x.CarImages.Where(x => !x.IsDeleted).FirstOrDefault().Extension));

            this.CreateMap<CarFeature, CarFeatureFormModel>();

            this.CreateMap<Car, CarDto>();

            //Clients
            this.CreateMap<ApplicationUser, ClientFormModel>()
                .ForMember(x => x.UserEmail, opt =>
                       opt.MapFrom(x => x.Email));

            //Companies
            this.CreateMap<Company, CompanyDetailsFormModel>()
                .ForMember(x => x.CompanyServices, opt => opt.MapFrom(x => x.CompanyServices))
                .ForMember(x => x.Locations, opt => opt.MapFrom(x => x.Locations));

            this.CreateMap<CompanyService, CompanyServiceFormModel>();

            //Locations
            this.CreateMap<Location, CompanyLocationFormModel>();

            this.CreateMap<Location, LocationFormModel>();

            this.CreateMap<Location, LocationDto>();

            //Feedbacks
            this.CreateMap<Feedback, FeedbackViewModel>();

            this.CreateMap<Feedback, FeedbackDto>();

            this.CreateMap<Feedback, ListFeedbackModel>();

            //Countries
            this.CreateMap<Country, CountryViewModel>();

            //Renting
            this.CreateMap<Rental, RentsDto>()
                .ForMember(x => x.StartDate, opt =>
                    opt.MapFrom(x => x.StartDate.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(x => x.ReturnDate, opt =>
                    opt.MapFrom(x => x.ReturnDate.ToString("dd/MM/yyyy HH:mm")));

            this.CreateMap<RentsDto, MyRentsViewModel>()
                .ReverseMap();

            this.CreateMap<Rental, RentDetailsModel>()
                .ForMember(x => x.Url, opt =>
                    opt.MapFrom(x => x.Car.CarImages.FirstOrDefault().RemoteImageUrl ??
                          x.Car.CarImages.FirstOrDefault().Url ??
                          "/images/cars/" + x.Car.CarImages.FirstOrDefault().Id + "." + x.Car.CarImages.FirstOrDefault().Extension))
                .ForMember(x => x.StartDate, opt =>
                    opt.MapFrom(x => x.StartDate.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(x => x.ReturnDate, opt =>
                    opt.MapFrom(x => x.ReturnDate.ToString("dd/MM/yyyy HH:mm")));

            this.CreateMap<RentsDto, EditRentFormModel>();
        }
    }
}