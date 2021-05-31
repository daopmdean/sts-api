using System.Collections.Generic;
using Data.Enums;

namespace Data.Entities
{
    public class Staff
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string PhotoUrl { get; set; }

        public StaffType Type { get; set; }

        public Status Status { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public int PositionId { get; set; }

        public Position Position { get; set; }

        public ICollection<StaffSkill> StaffSkills { get; set; }

        public ICollection<StoreStaff> StoreStaffs { get; set; }

        public ICollection<ShiftRegister> ShiftRegisters { get; set; }

        public ICollection<ShiftAssignment> ShiftAssignments { get; set; }

        public ICollection<StaffScheduleDetail> StaffScheduleDetails { get; set; }
    }
}
