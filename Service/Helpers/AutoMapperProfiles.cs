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

            CreateMap<Store, StoreOverview>();
            CreateMap<StoreCreate, Store>();
            CreateMap<StoreUpdate, Store>();

            CreateMap<StoreStaff, StoreStaffOverview>();
            CreateMap<StoreStaffCreate, StoreStaff>();
            CreateMap<StoreStaffUpdate, StoreStaff>();

            CreateMap<StaffSkill, StaffSkillOverview>();
            CreateMap<StaffSkillCreate, StaffSkill>();
            CreateMap<StaffSkillUpdate, StaffSkill>();

            CreateMap<Skill, SkillOverview>();
            CreateMap<SkillCreate, Skill>();
            CreateMap<SkillUpdate, Skill>();

            CreateMap<WeekSchedule, WeekScheduleOverview>();
            CreateMap<WeekScheduleCreate, WeekSchedule>();

            CreateMap<WeekScheduleDetail, WeekScheduleDetailOverview>();
            CreateMap<WeekScheduleDetailCreate, WeekScheduleDetail>();
            CreateMap<WeekScheduleDetailUpdate, WeekScheduleDetail>();

            CreateMap<StaffScheduleDetail, StaffScheduleDetailOverview>();
            CreateMap<StaffScheduleDetailCreate, StaffScheduleDetail>();
            CreateMap<StaffScheduleDetailUpdate, StaffScheduleDetail>();

            CreateMap<ShiftRegister, ShiftRegisterOverview>();
            CreateMap<ShiftRegisterCreate, ShiftRegister>();
            CreateMap<ShiftRegisterUpdate, ShiftRegister>();

            CreateMap<ShiftAssignment, ShiftAssignmentOverview>();
            CreateMap<ShiftAssignmentCreate, ShiftAssignment>();
            CreateMap<ShiftAssignmentUpdate, ShiftAssignment>();
        }
    }
}
