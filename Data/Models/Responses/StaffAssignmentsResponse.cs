using System.Collections.Generic;

namespace Data.Models.Responses
{
    public class StaffAssignmentsResponse
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<ShiftAssignmentOverview> Assignments { get; set; }
    }
}
