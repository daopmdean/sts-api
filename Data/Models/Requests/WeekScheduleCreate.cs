using System;
namespace Data.Models.Requests
{
    public class WeekScheduleCreate
    {
        public int StoreId { get; set; }

        public DateTime DateStart { get; set; }
    }
}
