using System.Collections.Generic;
using Data.Enums;

namespace Data.Entities
{
    public class Store
    {
        public string Id { get; set; }

        public string BrandId { get; set; }

        public Brand Brand { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public Status Status { get; set; }

        public ICollection<StoreStaff> StoreStaffs { get; set; }

        public ICollection<WeekSchedule> WeekSchedules { get; set; }
    }
}
