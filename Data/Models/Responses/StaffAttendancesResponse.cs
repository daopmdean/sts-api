using System.Collections.Generic;
using Data.Entities;

namespace Data.Models.Responses
{
    public class StaffAttendancesResponse
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<ShiftAttendance> Attendances { get; set; }
    }
}
