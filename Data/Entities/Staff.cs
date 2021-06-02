using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Data.Entities
{
    public class Staff
    {
        [Key]
        public string Username { get; set; }

        public User User { get; set; }

        public StaffType Type { get; set; }

        public Status Status { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public ICollection<StaffSkill> StaffSkills { get; set; }

        public ICollection<StoreStaff> StoreStaffs { get; set; }

        public ICollection<ShiftRegister> ShiftRegisters { get; set; }

        public ICollection<ShiftAssignment> ShiftAssignments { get; set; }

        public ICollection<StaffScheduleDetail> StaffScheduleDetails { get; set; }
    }
}
