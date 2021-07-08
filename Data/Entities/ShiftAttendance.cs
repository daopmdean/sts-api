using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class ShiftAttendance : BaseEntity
    {
        [Key]
        public int ShiftAssignmentId { get; set; }

        public ShiftAssignment ShiftAssignment { get; set; }

        public DateTime TimeCheckIn { get; set; }

        public DateTime TimeCheckOut { get; set; }
    }
}
