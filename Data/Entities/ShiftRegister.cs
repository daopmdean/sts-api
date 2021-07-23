using System;

namespace Data.Entities
{
    public class ShiftRegister : BaseEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public User User { get; set; }

        public int WeekScheduleId { get; set; }

        public WeekSchedule WeekSchedule { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }
    }
}
