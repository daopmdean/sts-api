using System;
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

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IWeekScheduleRepository, WeekScheduleRepository>();
            services.AddScoped<IWeekScheduleDetailRepository, WeekScheduleDetailRepository>();
            services.AddScoped<IStaffScheduleDetailRepository, StaffScheduleDetailRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IWeekScheduleService, WeekScheduleService>();
            services.AddScoped<IWeekScheduleDetailService, WeekScheduleDetailService>();
            services.AddScoped<IStaffScheduleDetailService, StaffScheduleDetailService>();

            return services;
        }
    }
}
