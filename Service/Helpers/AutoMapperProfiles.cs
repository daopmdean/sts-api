using AutoMapper;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;

namespace Service.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserOverview>();
            CreateMap<Role, RoleResponse>();
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
