using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using STS.Extensions;
using System;

namespace STS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            //services.AddDbContext<DataContext>(options =>
            //{
            //    string connectionString = Configuration.GetConnectionString("DevelopmentConnection");
            //    options.UseSqlServer(connectionString, b =>
            //    {
            //        b.MigrationsAssembly("STS");
            //        b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            //    });
            //});

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            //services.AddDbContext<DataContext>(options =>
            //{
            //    string connectionString = Configuration.GetConnectionString("ProductionConnection");
            //    options.UseSqlServer(connectionString, b =>
            //    {
            //        b.MigrationsAssembly("STS");
            //        b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            //    });
            //});

            ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbConnectionServices(Configuration);
            services.AddApplicationServices(Configuration);
            services.AddIdentityServices(Configuration);
            services.AddRabbitMQService(Configuration);
            services.AddControllers().AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling
                    = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "STS", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "STS v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(cors => cors
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
