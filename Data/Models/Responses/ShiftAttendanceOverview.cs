using System;
namespace Data.Models.Responses
{
    public class ShiftAttendanceOverview
    {
        public int ShiftAssignmentId { get; set; }

        public DateTime TimeCheckIn { get; set; }

        public DateTime TimeCheckOut { get; set; }
    }
}
