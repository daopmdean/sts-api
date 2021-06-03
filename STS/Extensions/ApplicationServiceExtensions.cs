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

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBrandService, BrandService>();

            return services;
        }
    }
}
