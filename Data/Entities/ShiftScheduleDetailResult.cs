using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class ShiftScheduleDetailResult : BaseEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int StoreId { get; set; }

        public int SkillId { get; set; }

        public int ShiftScheduleResultId { get; set; }

        public ShiftScheduleResult ShiftScheduleResult { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }
    }
}
