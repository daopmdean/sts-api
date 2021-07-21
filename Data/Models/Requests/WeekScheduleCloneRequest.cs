using System;
using System.Collections.Generic;
using Data.Models.Responses;

namespace Data.Models.Requests
{
    public class WeekScheduleCloneRequest
    {
        public int WeekScheduleId { get; set; }
        public IEnumerable<ShiftAssignmentResponse> ShiftAssignments { get; set; }
    }
}
