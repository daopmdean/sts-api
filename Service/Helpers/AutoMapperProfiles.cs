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
            CreateMap<StaffUpdateRequest, User>();
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<Role, RoleResponse>();

            CreateMap<Brand, BrandOverview>();
            CreateMap<BrandCreate, Brand>();
            CreateMap<BrandUpdate, Brand>();

            CreateMap<Store, StoreOverview>();
            CreateMap<StoreCreate, Store>();
            CreateMap<StoreUpdate, Store>();

            CreateMap<Post, PostOverview>();
            CreateMap<PostCreate, Post>();
            CreateMap<PostUpdate, Post>();

            CreateMap<StoreStaff, StoreStaffOverview>()
                .ForMember(dest => dest.Email,
                    obj => obj.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.FirstName,
                    obj => obj.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName,
                    obj => obj.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Address,
                    obj => obj.MapFrom(src => src.User.Address));
            CreateMap<StoreStaffCreate, StoreStaff>();
            CreateMap<StoreStaffUpdate, StoreStaff>();
            CreateMap<StoreAssign, StoreStaffCreate>();

            CreateMap<StaffSkill, StaffSkillOverview>();
            CreateMap<StaffSkillCreate, StaffSkill>();
            CreateMap<StaffSkillUpdate, StaffSkill>();

            CreateMap<Skill, SkillOverview>();
            CreateMap<SkillCreate, Skill>();
            CreateMap<SkillUpdate, Skill>();

            CreateMap<WeekSchedule, WeekScheduleOverview>();
            CreateMap<WeekScheduleCreate, WeekSchedule>();
            CreateMap<WeekScheduleUpdate, WeekSchedule>();

            CreateMap<WeekScheduleDetail, WeekScheduleDetailOverview>();
            CreateMap<WeekScheduleDetailCreate, WeekScheduleDetail>();
            CreateMap<WeekScheduleDetailUpdate, WeekScheduleDetail>();

            CreateMap<StaffScheduleDetail, StaffScheduleDetailOverview>();
            CreateMap<StaffScheduleDetailCreate, StaffScheduleDetail>();
            CreateMap<StaffScheduleDetailUpdate, StaffScheduleDetail>();

            CreateMap<StoreScheduleDetailCreate, StoreScheduleDetail>();
            CreateMap<StoreScheduleDetailUpdate, StoreScheduleDetail>();

            CreateMap<ShiftRegister, ShiftRegisterOverview>();
            CreateMap<ShiftRegistersCreate, ShiftRegister>();
            CreateMap<ShiftRegisterUpdate, ShiftRegister>();

            CreateMap<ShiftAssignment, ShiftAssignmentOverview>();
            CreateMap<ShiftAssignmentInfo, ShiftAssignment>();
            CreateMap<ShiftAssignmentCreate, ShiftAssignment>();
            CreateMap<ShiftAssignmentUpdate, ShiftAssignment>();

            CreateMap<AttendanceCreate, Attendance>();
            CreateMap<AttendanceManualCreate, Attendance>();
            CreateMap<AttendanceUpdate, Attendance>();

            CreateMap<SkillOverview, SkillRequest>()
                .ForMember(dest => dest.Id,
                    obj => obj.MapFrom(src => src.Id));
            CreateMap<StoreScheduleDetail, ConstraintSpecific>();
            CreateMap<StaffSkillOverview, SkillStaff>()
                .ForMember(dest => dest.SkillId,
                    obj => obj.MapFrom(src => src.SkillId));

            CreateMap<ShiftScheduleResult, ScheduleResponse>();
            CreateMap<ScheduleResponse, ShiftScheduleResult>();
            CreateMap<ShiftScheduleDetailResult, ShiftAssignmentResponse>();
            CreateMap<ShiftAssignmentResponse, ShiftScheduleDetailResult>();
        }
    }
}
