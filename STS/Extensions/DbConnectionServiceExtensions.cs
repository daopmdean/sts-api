using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace STS.Extensions
{
    public static class DbConnectionServiceExtensions
    {
        public static IServiceCollection AddDbConnectionServices(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<DataContext>(options =>
            {
                //var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                var connUrl = config.GetConnectionString("PostGresConnection");

                connUrl = connUrl.Replace("postgres://", string.Empty);
                var pgUserPass = connUrl.Split("@")[0];
                var pgHostPortDb = connUrl.Split("@")[1];
                var pgHostPort = pgHostPortDb.Split("/")[0];
                var pgDb = pgHostPortDb.Split("/")[1];
                var pgUser = pgUserPass.Split(":")[0];
                var pgPass = pgUserPass.Split(":")[1];
                var pgHost = pgHostPort.Split(":")[0];
                var pgPort = pgHostPort.Split(":")[1];

                var connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Prefer;TrustServerCertificate=True;";

                options.UseNpgsql(connStr,
                    b => b.MigrationsAssembly("STS"));
            });

            return services;
        }
    }
}
