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
            SeedSkillsIfNeeded(context);
            SeedStoresIfNeeded(context);
        }

        public static async Task SeedUsersIfNeeded(
            DataContext context,
            IAuthService authService,
            IUserRepository userRepository,
            IStoreStaffService storeStaffService,
            IStaffSkillService staffSkillService)
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
                    FirstName = "quan",
                    LastName = "ly"
                }
            };

            foreach (var registerRequest in registerRequests)
            {
                await authService.Register(registerRequest);
            }

            var admin = await userRepository.GetUserByUsernameAsync("admin");
            admin.RoleId = 1;

            userRepository.Update(admin);

            context.SaveChanges();

            await SeedPassioStoreManager(context, authService, storeStaffService);
            await SeedCoffeeHouseStoreManager(context, authService, storeStaffService);

            await SeedPassioStaff(
                context, authService, storeStaffService, staffSkillService);
            await SeedCoffeeHouseStaff(
                context, authService, storeStaffService, staffSkillService);
        }

        private static async Task SeedPassioStoreManager(
            DataContext context,
            IAuthService authService,
            IStoreStaffService storeStaffService)
        {
            var firstManager = new RegisterRequest
            {
                Username = "passiostore1",
                Password = "123456",
                FirstName = "Cuong",
                LastName = "Ly"
            };
            var secondManager = new RegisterRequest
            {
                Username = "passiostore2",
                Password = "123456",
                FirstName = "Mai",
                LastName = "Vu"
            };

            await authService.RegisterWithRole(1, 3, firstManager);
            await authService.RegisterWithRole(1, 3, secondManager);
            context.SaveChanges();

            var passioS1ManagerAssign = new StoreStaffCreate
            {
                StoreId = 1,
                Username = "passiostore1",
                IsManager = true,
                IsPrimaryStore = true
            };
            var passioS2ManagerAssign = new StoreStaffCreate
            {
                StoreId = 1,
                Username = "passiostore2",
                IsManager = true,
                IsPrimaryStore = true
            };

            await storeStaffService.CreateStoreStaff(passioS1ManagerAssign);
            await storeStaffService.CreateStoreStaff(passioS2ManagerAssign);
            context.SaveChanges();
        }

        private static async Task SeedCoffeeHouseStoreManager(
            DataContext context,
            IAuthService authService,
            IStoreStaffService storeStaffService)
        {
            var firstManager = new RegisterRequest
            {
                Username = "coffeehousestore1",
                Password = "123456",
                FirstName = "Cuong",
                LastName = "Ly"
            };
            var secondManager = new RegisterRequest
            {
                Username = "coffeehousestore2",
                Password = "123456",
                FirstName = "Mai",
                LastName = "Vu"
            };

            await authService.RegisterWithRole(2, 3, firstManager);
            await authService.RegisterWithRole(2, 3, secondManager);
            context.SaveChanges();

            var coffeeHouseS1ManagerAssign = new StoreStaffCreate
            {
                StoreId = 3,
                Username = "coffeehousestore1",
                IsManager = true,
                IsPrimaryStore = true
            };
            var coffeeHouseS2ManagerAssign = new StoreStaffCreate
            {
                StoreId = 4,
                Username = "coffeehousestore2",
                IsManager = true,
                IsPrimaryStore = true
            };

            await storeStaffService.CreateStoreStaff(coffeeHouseS1ManagerAssign);
            await storeStaffService.CreateStoreStaff(coffeeHouseS2ManagerAssign);
            context.SaveChanges();
        }

        private static async Task SeedPassioStaff(
            DataContext context,
            IAuthService authService,
            IStoreStaffService storeStaffService,
            IStaffSkillService staffSkillService)
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
                await authService.RegisterWithRole(1, 4, request);
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

            var staffSkills = new List<StaffSkillCreate>
            {
                new StaffSkillCreate
                {
                    Username = "mystaff01",
                    SkillId = 1,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff01",
                    SkillId = 3,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff02",
                    SkillId = 2,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff02",
                    SkillId = 1,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff03",
                    SkillId = 3,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff03",
                    SkillId = 4,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff04",
                    SkillId = 4,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff04",
                    SkillId = 5,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff05",
                    SkillId = 5,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff05",
                    SkillId = 3,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff06",
                    SkillId = 1,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff06",
                    SkillId = 2,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff07",
                    SkillId = 2,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff07",
                    SkillId = 3,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff08",
                    SkillId = 3,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff08",
                    SkillId = 4,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff09",
                    SkillId = 4,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff09",
                    SkillId = 5,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff10",
                    SkillId = 5,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "mystaff10",
                    SkillId = 1,
                    Level = 1,
                },
            };
            foreach (var staffSkill in staffSkills)
            {
                await staffSkillService.CreateStaffSkill(staffSkill);
            }
        }

        private static async Task SeedCoffeeHouseStaff(
            DataContext context,
            IAuthService authService,
            IStoreStaffService storeStaffService,
            IStaffSkillService staffSkillService)
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
                await authService.RegisterWithRole(2, 4, request);
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

            var staffSkills = new List<StaffSkillCreate>
            {
                new StaffSkillCreate
                {
                    Username = "coffee01",
                    SkillId = 6,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee01",
                    SkillId = 8,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee02",
                    SkillId = 7,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee02",
                    SkillId = 6,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee03",
                    SkillId = 8,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee03",
                    SkillId = 9,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee04",
                    SkillId = 9,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee04",
                    SkillId = 10,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee05",
                    SkillId = 10,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee05",
                    SkillId = 8,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee06",
                    SkillId = 6,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee06",
                    SkillId = 7,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee07",
                    SkillId = 7,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee07",
                    SkillId = 8,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee08",
                    SkillId = 8,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee08",
                    SkillId = 9,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee09",
                    SkillId = 9,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee09",
                    SkillId = 10,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee10",
                    SkillId = 10,
                    Level = 1,
                },
                new StaffSkillCreate
                {
                    Username = "coffee10",
                    SkillId = 6,
                    Level = 1,
                },
            };
            foreach (var staffSkill in staffSkills)
            {
                await staffSkillService.CreateStaffSkill(staffSkill);
            }
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
                    Name = "The Coffee House",
                    Address = "Some where",
                    Hotline = "0989898978"
                },
                new Brand
                {
                    Name = "Passio",
                    Address = "Some where",
                    Hotline = "0989898989"
                },
            };

            foreach (var brand in brands)
            {
                context.Add(brand);
            }

            context.SaveChanges();
        }

        private static void SeedSkillsIfNeeded(DataContext context)
        {
            if (context.Skills.Any())
                return;

            var skills = new List<Skill>
            {
                new Skill
                {
                    BrandId = 1,
                    Name = "Bartender",
                    Description = "Make drinks"
                },
                new Skill
                {
                    BrandId = 1,
                    Name = "Security",
                    Description = "Secure the store"
                },
                new Skill
                {
                    BrandId = 1,
                    Name = "Labor",
                    Description = "Clean the store"
                },
                new Skill
                {
                    BrandId = 1,
                    Name = "Waiter",
                    Description = "Serve the customer"
                },
                new Skill
                {
                    BrandId = 1,
                    Name = "Cashier",
                    Description = "Collect money"
                },
                new Skill
                {
                    BrandId = 2,
                    Name = "Bartender",
                    Description = "Make drinks"
                },
                new Skill
                {
                    BrandId = 2,
                    Name = "Security",
                    Description = "Secure the store"
                },
                new Skill
                {
                    BrandId = 2,
                    Name = "Labor",
                    Description = "Clean the store"
                },
                new Skill
                {
                    BrandId = 2,
                    Name = "Waiter",
                    Description = "Serve the customer"
                },
                new Skill
                {
                    BrandId = 2,
                    Name = "Cashier",
                    Description = "Collect money"
                }
            };

            foreach (var skill in skills)
            {
                context.Add(skill);
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
                    BrandId = 2,
                    Name = "CoffeeHouse S2",
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
                    BrandId = 1,
                    Name = "Passio S2",
                    Address = "Some where",
                    Phone = "0123412346"
                },
                new Store
                {
                    BrandId = 1,
                    Name = "Passio S1",
                    Address = "Some where",
                    Phone = "0123412345"
                },
            };

            foreach (var store in stores)
            {
                context.Add(store);
            }

            context.SaveChanges();
        }
    }
}
