using System;
namespace Data.Models.Requests
{
    public class ShiftAttendanceCreate
    {
        public string Username { get; set; }
        public DateTime TimeRequest { get; set; }
        public string Type { get; set; }
    }
}
