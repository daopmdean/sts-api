using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Data.Models.Requests;
using Data.Repositories.Interfaces;
using Service.Interfaces;

namespace STS.Seeding
{
    public static class SeedData
    {
        public static void SeedRolesIfNeeded(DataContext context)
        {
            if (context.Roles.Any())
                return;

            var roles = new List<Role>
            {
                new Role{ Name = "admin"},
                new Role{ Name = "brand manager"},
                new Role{ Name = "store manager"},
                new Role{ Name = "staff"},
            };

            foreach (var role in roles)
            {
                context.Add(role);
            }

            context.SaveChanges();
        }

        public static async Task SeedUsersIfNeeded(DataContext context,
            IAuthService service, IUserRepository userRepository)
        {
            if (context.Users.Any())
                return;

            var registerRequests = new List<RegisterRequest>
            {
                new RegisterRequest
                {
                    Username = "admin",
                    Password = "123456",
                    FirstName = "admin",
                    LastName = "admin"
                },
                new RegisterRequest
                {
                    Username = "daopham",
                    Password = "123456",
                    FirstName = "Dao",
                    LastName = "Pham"
                },
                new RegisterRequest
                {
                    Username = "staff",
                    Password = "123456",
                    FirstName = "Staff",
                    LastName = "Me"
                },
                new RegisterRequest
                {
                    Username = "quanly",
                    Password = "123456",
                    FirstName = "admin",
                    LastName = "admin"
                }
            };

            foreach (var registerRequest in registerRequests)
            {
                await service.Register(registerRequest);
            }

            var admin = await userRepository.GetUserByUsernameAsync("admin");
            admin.RoleId = 1;

            var staff = await userRepository.GetUserByUsernameAsync("staff");
            staff.RoleId = 4;

            userRepository.Update(admin);
            userRepository.Update(staff);

            context.SaveChanges();
        }
    }
}
