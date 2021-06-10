using System;
namespace Data.Models.Requests
{
    public class ShiftAttendanceCreate
    {
        public int ShiftAssignmentId { get; set; }

        public DateTime TimeCheckIn { get; set; }

        public DateTime TimeCheckOut { get; set; }
    }
}
