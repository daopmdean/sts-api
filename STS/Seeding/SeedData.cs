using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Entities;

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
    }
}
