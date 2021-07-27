using System.Collections.Generic;
using Data.Entities;

namespace Data.Models.Responses
{
    public class StaffAssignmentsResponse
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<ShiftAssignment> Assignments { get; set; }
    }
}
