using System;
using System.Threading.Tasks;
using Data;
using Data.Repositories.Interfaces;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Interfaces;

namespace STS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>();
                var userRepo = services.GetRequiredService<IUserRepository>();
                var authService = services.GetRequiredService<IAuthService>();
                var storeStaffService = services
                    .GetRequiredService<IStoreStaffService>();
                var staffSkillService = services
                    .GetRequiredService<IStaffSkillService>();

                await context.Database.MigrateAsync();
                Seeding.SeedData.SeedDataIfNeeded(context);
                await Seeding.SeedData.SeedUsersIfNeeded(context,
                    authService, userRepo, storeStaffService, staffSkillService);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Error occured during migration");
            }

            InitializeFirebase();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void InitializeFirebase()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential
                    .FromFile("./sts-manager-firebase-adminsdk.json")
                //Credential = GoogleCredential.GetApplicationDefault()
            });
        }
    }
}
