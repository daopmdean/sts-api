using System;

namespace Data.Entities
{
    public class WeekSchedule
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public DateTime DateStart { get; set; }
    }
}
