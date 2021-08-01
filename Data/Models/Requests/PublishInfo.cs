using System;
using System.Collections.Generic;

namespace Data.Models.Requests
{
    public class PublishInfo
    {
        public int WeekScheduleId { get; set; }
        public IEnumerable<ShiftAssignmentInfo> ShiftAssignments { get; set; }
    }

    public class ShiftAssignmentInfo
    {
        public string Username { get; set; }

        public int StoreId { get; set; }

        public int SkillId { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }
    }

}
