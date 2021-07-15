using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public DbSet<Post> Posts { get; set; }
        public DbSet<StoreStaff> StoreStaffs { get; set; }
        public DbSet<StaffSkill> StaffSkills { get; set; }

        public DbSet<WeekSchedule> WeekSchedules { get; set; }
        public DbSet<StaffScheduleDetail> StaffScheduleDetails { get; set; }
        public DbSet<StoreScheduleDetail> StoreScheduleDetails { get; set; }
        public DbSet<WeekScheduleDetail> WeekScheduleDetails { get; set; }

        public DbSet<ShiftRegister> ShiftRegisters { get; set; }
        public DbSet<ShiftAssignment> ShiftAssignments { get; set; }
        public DbSet<ShiftAttendance> ShiftAttendances { get; set; }
        public DbSet<ShiftLog> ShiftLogs { get; set; }

        public DbSet<ShiftScheduleResult> ShiftScheduleResults { get; set; }
        public DbSet<ShiftScheduleDetailResult> ShiftScheduleDetailResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // user - role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // user - brand
            modelBuilder.Entity<User>()
                .HasOne(u => u.Brand)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.BrandId);

            // shift assignment - user
            modelBuilder.Entity<ShiftAssignment>()
                .HasOne(sa => sa.User)
                .WithMany(u => u.ShiftAssignments)
                .HasForeignKey(sr => sr.Username)
                .OnDelete(DeleteBehavior.NoAction);

            // shift attendance - shift assignment
            modelBuilder.Entity<ShiftAssignment>()
                .HasOne(sa => sa.ShiftAttendance)
                .WithOne(sa => sa.ShiftAssignment);

            // shift assignment - shift log
            modelBuilder.Entity<ShiftLog>()
                .HasOne(sl => sl.ShiftAssignment)
                .WithMany(sa => sa.ShiftLogs)
                .HasForeignKey(sl => sl.ShiftAssignmentId)
                .OnDelete(DeleteBehavior.NoAction);

            // shift register - user
            modelBuilder.Entity<ShiftRegister>()
                .HasOne(sr => sr.User)
                .WithMany(u => u.ShiftRegisters)
                .HasForeignKey(sr => sr.Username)
                .OnDelete(DeleteBehavior.NoAction);

            // shift register - week schedule
            modelBuilder.Entity<ShiftRegister>()
                .HasOne(sr => sr.WeekSchedule)
                .WithMany(ws => ws.ShiftRegisters)
                .HasForeignKey(sr => sr.WeekScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            // brand - store
            modelBuilder.Entity<Store>()
                .HasOne(s => s.Brand)
                .WithMany(b => b.Stores)
                .HasForeignKey(s => s.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            // storestaff compound key
            modelBuilder.Entity<StoreStaff>()
                .HasKey(ss => new { ss.Username, ss.StoreId });

            // store staff - store
            modelBuilder.Entity<StoreStaff>()
                .HasOne(ss => ss.Store)
                .WithMany(s => s.StoreStaffs)
                .HasForeignKey(ss => ss.StoreId)
                .OnDelete(DeleteBehavior.NoAction);

            // store staff - user
            modelBuilder.Entity<StoreStaff>()
                .HasOne(ss => ss.User)
                .WithMany(s => s.StoreStaffs)
                .HasForeignKey(ss => ss.Username)
                .OnDelete(DeleteBehavior.NoAction);

            // staffskill compound key
            modelBuilder.Entity<StaffSkill>()
                .HasKey(ss => new { ss.Username, ss.SkillId });

            // staffskill - skill
            modelBuilder.Entity<StaffSkill>()
                .HasOne(ss => ss.Skill)
                .WithMany(s => s.StaffSkills)
                .HasForeignKey(ss => ss.SkillId)
                .OnDelete(DeleteBehavior.NoAction);

            // staffskill - user
            modelBuilder.Entity<StaffSkill>()
                .HasOne(ss => ss.User)
                .WithMany(s => s.StaffSkills)
                .HasForeignKey(ss => ss.Username)
                .OnDelete(DeleteBehavior.NoAction);

            // brand - skill
            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Brand)
                .WithMany(b => b.Skills)
                .HasForeignKey(s => s.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            // week schedule - store
            modelBuilder.Entity<WeekSchedule>()
                .HasOne(ws => ws.Store)
                .WithMany(s => s.WeekSchedules)
                .HasForeignKey(ws => ws.StoreId)
                .OnDelete(DeleteBehavior.NoAction);

            // week schedule detail - week schedule
            modelBuilder.Entity<WeekScheduleDetail>()
                .HasOne(wsd => wsd.WeekSchedule)
                .WithMany(ws => ws.WeekScheduleDetails)
                .HasForeignKey(wsd => wsd.WeekScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            // staff schedule detail - week schedule
            //modelBuilder.Entity<StaffScheduleDetail>()
            //    .HasOne(ssd => ssd.WeekSchedule)
            //    .WithMany(ws => ws.StaffScheduleDetails)
            //    .HasForeignKey(ssd => ssd.WeekScheduleId)
            //    .OnDelete(DeleteBehavior.NoAction);

            // shift schedule detail - shift schedule
            modelBuilder.Entity<ShiftScheduleDetailResult>()
                .HasOne(rd => rd.ShiftScheduleResult)
                .WithMany(r => r.ShiftScheduleDetailResults)
                .HasForeignKey(rd => rd.ShiftScheduleResultId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            try
            { 
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exception = HandleDbUpdateException(ex);
                throw exception;
            }
            catch (DbUpdateException ex)
            {
                var exception = HandleDbUpdateException(ex);
                throw exception;
            }
        }

        private Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder(
                "A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", 
                        result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, dbu);
        }
    }
}
