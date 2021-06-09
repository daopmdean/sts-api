using System;

namespace Data.Models.Requests
{
    public class ShiftRegisterCreate
    {
        public string Username { get; set; }

        public int WeekScheduleId { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }
    }
}
