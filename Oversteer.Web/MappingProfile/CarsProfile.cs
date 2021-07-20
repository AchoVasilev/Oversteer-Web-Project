namespace Oversteer.Web.MappingProfile
{
    using AutoMapper;

    using Oversteer.Models.Cars;
    using Oversteer.Web.Models.Cars;

    public class CarsProfile : Profile
    {
        public CarsProfile()
        {
            this.CreateMap<CarBrand, CarBrandFormModel>();
        }
    }
}
