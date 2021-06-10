using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Service.Helpers;
using Service.Implementations;
using Service.Interfaces;

namespace STS.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            // repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();

            services.AddScoped<IStoreStaffRepository, StoreStaffRepository>();
            services.AddScoped<IStaffSkillRepository, StaffSkillRepository>();

            services.AddScoped<IWeekScheduleRepository, WeekScheduleRepository>();
            services.AddScoped<IWeekScheduleDetailRepository, WeekScheduleDetailRepository>();
            services.AddScoped<IStaffScheduleDetailRepository, StaffScheduleDetailRepository>();

            services.AddScoped<IShiftRegisterRepository, ShiftRegisterRepository>();
            services.AddScoped<IShiftAssignmentRepository, ShiftAssignmentRepository>();
            services.AddScoped<IShiftAttendanceRepository, ShiftAttendanceRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ISkillService, SkillService>();

            services.AddScoped<IStoreStaffService, StoreStaffService>();
            services.AddScoped<IStaffSkillService, StaffSkillService>();

            services.AddScoped<IWeekScheduleService, WeekScheduleService>();
            services.AddScoped<IWeekScheduleDetailService,
                WeekScheduleDetailService>();
            services.AddScoped<IStaffScheduleDetailService,
                StaffScheduleDetailService>();
            services.AddScoped<IShiftRegisterService, ShiftRegisterService>();
            services.AddScoped<IShiftAssignmentService, ShiftAssignmentService>();
            services.AddScoped<IShiftAttendanceService, ShiftAttendanceService>();

            return services;
        }
    }
}
