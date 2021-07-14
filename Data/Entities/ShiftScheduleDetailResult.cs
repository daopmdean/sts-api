using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class ShiftScheduleDetailResult : BaseEntity
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public int StoreId { get; set; }

        public int SkillId { get; set; }

        public long ShiftScheduleResultId { get; set; }

        public ShiftScheduleResult ShiftScheduleResult { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public DateTime MealStart { get; set; }

        public DateTime MealEnd { get; set; }
    }
}
