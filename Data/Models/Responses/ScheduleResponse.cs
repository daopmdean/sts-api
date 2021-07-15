using System.Collections.Generic;

namespace Data.Models.Responses
{
    public class ScheduleResponse
    {
        public int ShiftScheduleResultId { get; set; }

        public long Conflicts { get; set; }

        public long Branches { get; set; }

        public double WallTime { get; set; }

        public IEnumerable<ShiftAssignmentResponse> ShiftAssignments { get; set; }
    }
}
