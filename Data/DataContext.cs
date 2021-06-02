using System;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandManager> BrandManagers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreManager> StoreManagers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<BrandManager>()
                .HasOne(bm => bm.Brand)
                .WithMany(b => b.BrandManagers)
                .HasForeignKey(b => b.BrandId);

            modelBuilder.Entity<StoreStaff>()
                .HasKey(ss => new { ss.StaffId, ss.StoreId });

            modelBuilder.Entity<StaffSkill>()
                .HasKey(ss => new { ss.StaffId, ss.SkillId });

        }
    }
}
