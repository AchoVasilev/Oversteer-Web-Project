namespace Oversteer.Tests.Mock
{
    using AutoMapper;

    using Oversteer.Web.MappingProfile;

    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<MapperProfile>();
                });

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
