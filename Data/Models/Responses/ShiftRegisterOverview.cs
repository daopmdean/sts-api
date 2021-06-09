using System;
namespace Data.Models.Responses
{
    public class ShiftRegisterOverview
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int WeekScheduleId { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }
    }
}
