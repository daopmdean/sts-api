using System.Collections.Generic;

namespace Data.Entities
{
    public class ShiftScheduleResult : BaseEntity
    {
        public int Id { get; set; }
        public bool IsComplete { get; set; } = false;
        public long Conflicts { get; set; }
        public long Branches { get; set; }
        public double WallTime { get; set; }
        public ICollection<ShiftScheduleDetailResult> ShiftScheduleDetailResults { get; set; }

    }
}
