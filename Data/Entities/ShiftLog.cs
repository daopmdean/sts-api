using System;
namespace Data.Entities
{
    public class ShiftLog
    {
        public int Id { get; set; }

        public int ShiftAssignmentId { get; set; }

        public ShiftAssignment ShiftAssignment { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Content { get; set; }
    }
}
