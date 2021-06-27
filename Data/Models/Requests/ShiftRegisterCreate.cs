using System;
using System.Collections.Generic;

namespace Data.Models.Requests
{
    public class ShiftRegistersCreate
    {
        public string Username { get; set; }

        public int WeekScheduleId { get; set; }

        public ICollection<TimeWork> TimeWorks { get; set; }
    }

    public class TimeWork
    {
        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }
    }
}
