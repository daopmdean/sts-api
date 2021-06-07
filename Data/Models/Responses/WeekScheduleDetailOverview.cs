using System;
namespace Data.Models.Responses
{
    public class WeekScheduleDetailOverview
    {
        public int Id { get; set; }

        public int SkillId { get; set; }

        public int Quantity { get; set; }

        public DateTime WorkStart { get; set; }

        public DateTime WorkEnd { get; set; }
    }
}
