using System;
namespace Data.Entities
{
    public class ShiftAttendance : BaseEntity
    {
        public int Id { get; set; }

        public int ShiftAssignmentId { get; set; }

        public ShiftAssignment ShiftAssignment { get; set; }

        public DateTime TimeCheckIn { get; set; }

        public DateTime TimeCheckOut { get; set; }
    }
}
