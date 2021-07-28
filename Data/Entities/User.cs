using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Data.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public string Username { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public string Phone { get; set; }

        public Gender? Gender { get; set; }

        public StaffType? Type { get; set; }

        public string Address { get; set; }

        public string PhotoUrl { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int? BrandId { get; set; }

        public Brand Brand { get; set; }

        public ICollection<StaffSkill> StaffSkills { get; set; }

        public ICollection<StoreStaff> StoreStaffs { get; set; }

        public ICollection<ShiftRegister> ShiftRegisters { get; set; }

        public ICollection<ShiftAssignment> ShiftAssignments { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        public ICollection<StaffScheduleDetail> StaffScheduleDetails { get; set; }
    }
}
