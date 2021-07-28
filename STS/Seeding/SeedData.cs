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

        public static async Task SeedUsersIfNeeded(
            DataContext context,
            IAuthService authService,
            IUserRepository userRepository,
            IStoreStaffService storeStaffService)
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
                await authService.Register(registerRequest);
            }

            var admin = await userRepository.GetUserByUsernameAsync("admin");
            admin.RoleId = 1;

            var passioStoreManager = await userRepository
                .GetUserByUsernameAsync("passiostore");
            passioStoreManager.RoleId = 3;

            var coffeeHouseStoreManager = await userRepository
                .GetUserByUsernameAsync("coffeehousestore");
            coffeeHouseStoreManager.RoleId = 3;

            userRepository.Update(admin);
            userRepository.Update(coffeeHouseStoreManager);
            userRepository.Update(passioStoreManager);

            await SeedPassioStaff(context, authService, storeStaffService);
            await SeedCoffeeHouseStaff(context, authService, storeStaffService);

            context.SaveChanges();
        }

        private static async Task SeedPassioStaff(
            DataContext context,
            IAuthService service,
            IStoreStaffService storeStaffService)
        {
            var passioStaffRequests = new List<RegisterRequest>
            {
                new RegisterRequest
                {
                    Username = "mystaff01",
                    Password = "123456",
                    FirstName = "Asc",
                    LastName = "Qws"
                },
                new RegisterRequest
                {
                    Username = "mystaff02",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                },
                new RegisterRequest
                {
                    Username = "mystaff03",
                    Password = "123456",
                    FirstName = "Asc",
                    LastName = "Qws"
                },
                new RegisterRequest
                {
                    Username = "mystaff04",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                },
                new RegisterRequest
                {
                    Username = "mystaff05",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                },
                new RegisterRequest
                {
                    Username = "mystaff06",
                    Password = "123456",
                    FirstName = "Asc",
                    LastName = "Qws"
                },
                new RegisterRequest
                {
                    Username = "mystaff07",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                },
                new RegisterRequest
                {
                    Username = "mystaff08",
                    Password = "123456",
                    FirstName = "Asc",
                    LastName = "Qws"
                },
                new RegisterRequest
                {
                    Username = "mystaff09",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                },
                new RegisterRequest
                {
                    Username = "mystaff10",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                }
            };
            foreach (var request in passioStaffRequests)
            {
                await service.RegisterWithRole(1, 4, request);
            }

            context.SaveChanges();

            var storeStaffs = new List<StoreStaffCreate>
            {
                new StoreStaffCreate
                {
                    StoreId = 1,
                    Username = "mystaff01",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 1,
                    Username = "mystaff02",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 1,
                    Username = "mystaff03",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 1,
                    Username = "mystaff04",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 1,
                    Username = "mystaff05",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 1,
                    Username = "mystaff06",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 2,
                    Username = "mystaff07",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 2,
                    Username = "mystaff08",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 2,
                    Username = "mystaff09",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 2,
                    Username = "mystaff10",
                    IsManager = false,
                    IsPrimaryStore = true
                }
            };
            foreach (var storeStaff in storeStaffs)
            {
                await storeStaffService.CreateStoreStaff(storeStaff);
            }

            context.SaveChanges();
        }

        private static async Task SeedCoffeeHouseStaff(
            DataContext context,
            IAuthService service,
            IStoreStaffService storeStaffService)
        {
            var coffeeHouseStaffRequests = new List<RegisterRequest>
            {
                new RegisterRequest
                {
                    Username = "coffee01",
                    Password = "123456",
                    FirstName = "Asc",
                    LastName = "Qws"
                },
                new RegisterRequest
                {
                    Username = "coffee02",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                },
                new RegisterRequest
                {
                    Username = "coffee03",
                    Password = "123456",
                    FirstName = "Asc",
                    LastName = "Qws"
                },
                new RegisterRequest
                {
                    Username = "coffee04",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                },
                new RegisterRequest
                {
                    Username = "coffee05",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                },
                new RegisterRequest
                {
                    Username = "coffee06",
                    Password = "123456",
                    FirstName = "Asc",
                    LastName = "Qws"
                },
                new RegisterRequest
                {
                    Username = "coffee07",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                },
                new RegisterRequest
                {
                    Username = "coffee08",
                    Password = "123456",
                    FirstName = "Asc",
                    LastName = "Qws"
                },
                new RegisterRequest
                {
                    Username = "coffee09",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                },
                new RegisterRequest
                {
                    Username = "coffee10",
                    Password = "123456",
                    FirstName = "Iuj",
                    LastName = "Scd"
                }
            };

            foreach (var request in coffeeHouseStaffRequests)
            {
                await service.RegisterWithRole(2, 4, request);
            }

            context.SaveChanges();

            var storeStaffs = new List<StoreStaffCreate>
            {
                new StoreStaffCreate
                {
                    StoreId = 3,
                    Username = "coffee01",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 3,
                    Username = "coffee02",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 3,
                    Username = "coffee03",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 3,
                    Username = "coffee04",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 3,
                    Username = "coffee05",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 4,
                    Username = "coffee06",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 4,
                    Username = "coffee07",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 4,
                    Username = "coffee08",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 4,
                    Username = "coffee09",
                    IsManager = false,
                    IsPrimaryStore = true
                },
                new StoreStaffCreate
                {
                    StoreId = 4,
                    Username = "coffee10",
                    IsManager = false,
                    IsPrimaryStore = true
                }
            };
            foreach (var storeStaff in storeStaffs)
            {
                await storeStaffService.CreateStoreStaff(storeStaff);
            }

            context.SaveChanges();
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
