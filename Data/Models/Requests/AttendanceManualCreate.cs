using System;
namespace Data.Models.Requests
{
    public class AttendanceManualCreate
    {
        public string Username { get; set; }
        public int StoreId { get; set; }
        public DateTime TimeCheck { get; set; }
        public string Note { get; set; }
    }
}
