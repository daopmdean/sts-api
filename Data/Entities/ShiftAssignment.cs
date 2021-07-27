using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class ShiftAssignment : BaseEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int WeekScheduleId { get; set; }

        public WeekSchedule WeekSchedule { get; set; }

        public User User { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int SkillId { get; set; }

        public Skill Skill { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public int ReferenceId { get; set; }

        public ShiftAttendance ShiftAttendance { get; set; }

        public ICollection<ShiftLog> ShiftLogs { get; set; }
    }
}
