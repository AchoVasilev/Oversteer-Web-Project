namespace Oversteer.Web.MappingProfile
{
    using AutoMapper;

    using Oversteer.Data.Models.Users;
    using Oversteer.Web.ViewModels.Clients;

    public class ClientsProfile : Profile
    {
        public ClientsProfile()
        {
            this.CreateMap<ApplicationUser, ClientFormModel>()
                .ForMember(x => x.UserEmail, opt =>
                       opt.MapFrom(x => x.Email));
        }
    }
}
