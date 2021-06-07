using System;
namespace Data.Models.Requests
{
    public class WeekScheduleDetailUpdate
    {
        public int SkillId { get; set; }

        public int Quantity { get; set; }

        public DateTime WorkStart { get; set; }

        public DateTime WorkEnd { get; set; }
    }
}
