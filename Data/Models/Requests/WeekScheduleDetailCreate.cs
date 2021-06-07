using System;
namespace Data.Models.Requests
{
    public class WeekScheduleDetailCreate
    {
        public int WeekScheduleId { get; set; }

        public int SkillId { get; set; }

        public int Quantity { get; set; }

        public DateTime WorkStart { get; set; }

        public DateTime WorkEnd { get; set; }
    }
}
