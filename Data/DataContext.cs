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
        public DbSet<Store> Stores { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public DbSet<WeekSchedule> WeekSchedules { get; set; }
        public DbSet<StaffScheduleDetail> StaffScheduleDetails { get; set; }
        public DbSet<WeekScheduleDetail> WeekScheduleDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Store>()
                .HasOne(s => s.Brand)
                .WithMany(b => b.Stores)
                .HasForeignKey(s => s.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WeekSchedule>()
                .HasOne(ws => ws.Store)
                .WithMany(s => s.WeekSchedules)
                .HasForeignKey(ws => ws.StoreId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WeekScheduleDetail>()
                .HasOne(wsd => wsd.WeekSchedule)
                .WithMany(ws => ws.WeekScheduleDetails)
                .HasForeignKey(wsd => wsd.WeekScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StaffScheduleDetail>()
                .HasOne(ssd => ssd.WeekSchedule)
                .WithMany(ws => ws.StaffScheduleDetails)
                .HasForeignKey(ssd => ssd.WeekScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StoreStaff>()
                .HasKey(ss => new { ss.Username, ss.StoreId });

            modelBuilder.Entity<StaffSkill>()
                .HasKey(ss => new { ss.Username, ss.SkillId });

        }
    }
}
