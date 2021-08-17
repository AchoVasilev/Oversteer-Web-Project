namespace Oversteer.Web.MappingProfile
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
        }
    }
}