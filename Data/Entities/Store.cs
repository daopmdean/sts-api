using System.Collections.Generic;
using Data.Enums;

namespace Data.Entities
{
    public class Store : BaseEntity
    {
        public int Id { get; set; }

        public string BrandId { get; set; }

        public Brand Brand { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public ICollection<StoreStaff> StoreStaffs { get; set; }

        public ICollection<WeekSchedule> WeekSchedules { get; set; }
    }
}
