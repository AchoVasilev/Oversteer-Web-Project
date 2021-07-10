namespace Oversteer.Web.MappingProfile
{
    using System.Linq;
    using AutoMapper;
    using Oversteer.Models.Cars;
    using Oversteer.Web.Dto;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<CarBrandDto, CarBrand>()
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Name))
                .ForMember(x => x.CarModels.Select(x => x.Name), x => x.MapFrom(y => y.Models.Select(x => x.Title)));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            var mapper = config.CreateMapper();
        }
    }
}
