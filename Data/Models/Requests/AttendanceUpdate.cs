using System;

namespace Data.Models.Requests
{
    public class AttendanceUpdate
    {
        public DateTime TimeCheck { get; set; }
        public int CheckType { get; set; }
        public string ImageUrl { get; set; }
        public float RecognizePercentage { get; set; }
    }
}
