namespace Oversteer.Web.MappingProfile
{
    using AutoMapper;

    using Oversteer.Models.Cars;
    using Oversteer.Web.Data.Cars;
    using Oversteer.Web.Models.Cars;

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
          //  this.CreateMap<Car, CarDetailsFormModel>()
          //      .ForMember(x => x.Url, opt =>
          //          opt.MapFrom(x => x.CarImages.FirstOrDefault().RemoteImageUrl ??
          //                "/images/cars/" + x.CarImages.FirstOrDefault().Id + "." + x.CarImages.FirstOrDefault().Extension));
        }
    }
}
