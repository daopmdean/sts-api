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
        public static void SeedDataIfNeeded(DataContext context)
        {
            SeedRolesIfNeeded(context);
            SeedBrandsIfNeeded(context);
            SeedStoresIfNeeded(context);
        }

        private static void SeedRolesIfNeeded(DataContext context)
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
                    FirstName = "Trung",
                    LastName = "Do"
                },
                new RegisterRequest
                {
                    Username = "quanly",
                    Password = "123456",
                    FirstName = "admin",
                    LastName = "admin"
                },
                new RegisterRequest
                {
                    Username = "coffeehousestore",
                    Password = "123456",
                    FirstName = "Cuong",
                    LastName = "Ly"
                },
                new RegisterRequest
                {
                    Username = "passiostore",
                    Password = "123456",
                    FirstName = "Mai",
                    LastName = "Vu"
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

            var store1 = await userRepository.GetUserByUsernameAsync(
                "coffeehousestore");
            store1.RoleId = 3;

            var store2 = await userRepository.GetUserByUsernameAsync(
                "passiostore");
            store2.RoleId = 3;

            userRepository.Update(admin);
            userRepository.Update(staff);
            userRepository.Update(store1);
            userRepository.Update(store2);

            context.SaveChanges();
        }

        private static void SeedBrandsIfNeeded(DataContext context)
        {
            if (context.Brands.Any())
                return;

            var brands = new List<Brand>
            {
                new Brand
                {
                    Name = "Passio",
                    Address = "Some where",
                    Hotline = "0989898989"
                },
                new Brand
                {
                    Name = "The Coffee House",
                    Address = "Some where",
                    Hotline = "0989898978"
                }
            };

            foreach (var brand in brands)
            {
                context.Add(brand);
            }

            context.SaveChanges();
        }

        private static void SeedStoresIfNeeded(DataContext context)
        {
            if (context.Stores.Any())
                return;

            var stores = new List<Store>
            {
                new Store
                {
                    BrandId = 1,
                    Name = "Passio S1",
                    Address = "Some where",
                    Phone = "0123412345"
                },
                new Store
                {
                    BrandId = 1,
                    Name = "Passio S2",
                    Address = "Some where",
                    Phone = "0123412346"
                },
                new Store
                {
                    BrandId = 2,
                    Name = "CoffeeHouse S1",
                    Address = "Some where",
                    Phone = "0123412345"
                },
                new Store
                {
                    BrandId = 2,
                    Name = "CoffeeHouse S2",
                    Address = "Some where",
                    Phone = "0123412346"
                }
            };

            foreach (var store in stores)
            {
                context.Add(store);
            }

            context.SaveChanges();
        }
    }
}
