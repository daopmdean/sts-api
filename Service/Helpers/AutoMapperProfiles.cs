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
            CreateMap<User, UserInfoResponse>();
            CreateMap<UserUpdate, User>();
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<Role, RoleResponse>();

            CreateMap<Brand, BrandOverview>();
            CreateMap<BrandCreate, Brand>();
            CreateMap<BrandUpdate, Brand>();
        }
    }
}
