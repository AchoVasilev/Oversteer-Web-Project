namespace Oversteer.Web.MappingProfile
{
    using System.Linq;

    using AutoMapper;

    using Oversteer.Data.Models.Rentals;
    using Oversteer.Web.ViewModels.Rents;

    public class RentingProfile : Profile
    {
        public RentingProfile()
        {
            this.CreateMap<Rental, RentsDto>()
                .ForMember(x => x.StartDate, opt =>
                    opt.MapFrom(x => x.StartDate.ToString("g")))
                .ForMember(x => x.ReturnDate, opt =>
                    opt.MapFrom(x => x.ReturnDate.ToString("g")));

            this.CreateMap<RentsDto, MyRentsViewModel>()
                .ReverseMap();

            this.CreateMap<Rental, RentDetailsModel>()
                .ForMember(x => x.Url, opt =>
                    opt.MapFrom(x => x.Car.CarImages.FirstOrDefault().RemoteImageUrl ??
                          x.Car.CarImages.FirstOrDefault().Url ??
                          "/images/cars/" + x.Car.CarImages.FirstOrDefault().Id + "." + x.Car.CarImages.FirstOrDefault().Extension))
                .ForMember(x => x.StartDate, opt =>
                    opt.MapFrom(x => x.StartDate.ToString("g")))
                .ForMember(x => x.ReturnDate, opt =>
                    opt.MapFrom(x => x.ReturnDate.ToString("g")));

            this.CreateMap<RentsDto, EditRentFormModel>();
        }
    }
}
