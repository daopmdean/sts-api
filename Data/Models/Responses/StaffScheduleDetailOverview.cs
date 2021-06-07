using System;
namespace Data.Models.Responses
{
    public class StaffScheduleDetailOverview
    {
        public int Id { get; set; }

        public int WeekScheduleId { get; set; }

        public int Username { get; set; }

        public int MinHours { get; set; }

        public int MaxHours { get; set; }
    }
}
