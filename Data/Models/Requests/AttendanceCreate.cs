using System;
namespace Data.Models.Requests
{
    public class AttendanceCreate
    {
        public string Username { get; set; }
        public int StoreId { get; set; }
        public DateTime TimeCheck { get; set; }
        public int CheckType { get; set; }
        public string ImageUrl { get; set; }
        public float RecognizePercentage { get; set; }
        public string DeviceCode { get; set; }
    }
}
