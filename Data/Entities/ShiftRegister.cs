using System;
using Data.Enums;

namespace Data.Entities
{
    public class ShiftRegister : BaseEntity
    {
        public int Id { get; set; }

        public int Username { get; set; }

        public User User { get; set; }

        public int WeekScheduleId { get; set; }

        public WeekSchedule WeekSchedule { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public string PreferSkill { get; set; }
    }
}
