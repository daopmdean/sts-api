using System;
namespace Data.Models.Requests
{
    public class ShiftAttendanceUpdate
    {
        public DateTime TimeCheckIn { get; set; }

        public DateTime TimeCheckOut { get; set; }
    }
}
