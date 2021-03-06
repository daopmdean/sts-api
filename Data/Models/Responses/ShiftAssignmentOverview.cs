using System;

namespace Data.Models.Responses
{
    public class ShiftAssignmentOverview
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int StoreId { get; set; }

        public int SkillId { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public DateTime TimeCheckIn { get; set; }

        public DateTime TimeCheckOut { get; set; }

        public int ReferenceId { get; set; }
    }
}
