using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace STS.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequiredManagers", policy
                    => policy
                    .RequireRole("admin", "brand manager", "store manager"));

                opt.AddPolicy("RequiredAdmin", policy
                    => policy.RequireRole("admin"));

                opt.AddPolicy("RequiredBrandManager", policy
                    => policy.RequireRole("brand manager"));

                opt.AddPolicy("RequiredStoreManager", policy
                    => policy.RequireRole("store manager"));

                opt.AddPolicy("RequiredStaff", policy
                    => policy.RequireRole("staff"));
            });

            return services;
        }
    }
}
