using System;
using Data.Enums;

namespace Data.Models.Requests
{
    public class WeekScheduleCreate
    {
        public int StoreId { get; set; }

        public DateTime DateStart { get; set; }

        public string CreatedBy { get; set; }

        public Status Status { get; set; }
    }
}
