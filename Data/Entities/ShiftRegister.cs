using System;
using Data.Enums;

namespace Data.Entities
{
    public class ShiftRegister
    {
        public int Id { get; set; }

        public int StaffId { get; set; }

        public Staff Staff { get; set; }

        public int WeekScheduleId { get; set; }

        public WeekSchedule WeekSchedule { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public string PreferSkill { get; set; }

        public Status Status { get; set; }

    }
}
