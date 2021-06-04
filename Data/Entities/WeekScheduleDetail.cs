using System;
namespace Data.Entities
{
    public class WeekScheduleDetail : BaseEntity
    {
        public int Id { get; set; }

        public int WeekScheduleId { get; set; }

        public WeekSchedule WeekSchedule { get; set; }

        public int SkillId { get; set; }

        public Skill Skill { get; set; }

        public int Quantity { get; set; }

        public DateTime WorkStart { get; set; }

        public DateTime WorkEnd { get; set; }
    }
}
