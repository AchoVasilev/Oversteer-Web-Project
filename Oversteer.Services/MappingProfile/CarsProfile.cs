namespace Oversteer.Services.MappingProfile
{
    using System.Linq;

    using AutoMapper;

    using Oversteer.Data.Models.Cars;
    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Cars.CarItems;
    using Oversteer.Web.ViewModels.Home;

    public class CarsProfile : Profile
    {
        public CarsProfile()
        {
            this.CreateMap<CarBrand, CarBrandFormModel>();

            this.CreateMap<CarModel, CarModelFormModel>();

            this.CreateMap<Color, ColorFormModel>();

            this.CreateMap<Fuel, FuelTypeFormModel>();

            this.CreateMap<Transmission, TransmissionTypeFormModel>();

            this.CreateMap<CarType, CarTypeFormModel>();

            this.CreateMap<Car, CarDetailsFormModel>()
                .ForMember(x => x.Url, opt =>
                    opt.MapFrom(x => x.CarImages.FirstOrDefault().RemoteImageUrl));

            this.CreateMap<Car, CarIndexViewModel>()
                .ForMember(x => x.Url, opt =>
                    opt.MapFrom(x => x.CarImages.FirstOrDefault().RemoteImageUrl ??
                          "/images/cars/" + x.CarImages.FirstOrDefault().Id + "." + x.CarImages.FirstOrDefault().Extension));

            this.CreateMap<Car, ListCarFormModel>()
                .ForMember(x => x.Url, opt =>
                    opt.MapFrom(x => x.CarImages.FirstOrDefault().RemoteImageUrl ??
                          "/images/cars/" + x.CarImages.FirstOrDefault().Id + "." + x.CarImages.FirstOrDefault().Extension));
        }
    }
}