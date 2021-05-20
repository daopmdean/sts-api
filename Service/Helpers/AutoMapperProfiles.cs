using System;
using AutoMapper;
using Data.Entities;
using Service.Models.Responses;

namespace Service.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserOverview>();
            CreateMap<Role, RoleResponse>();
        }
    }
}
