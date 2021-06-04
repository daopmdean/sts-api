using System;

namespace Data.Entities
{
    public class StoreStaff : BaseEntity
    {
        public string StoreId { get; set; }

        public Store Store { get; set; }

        public string StaffId { get; set; }

        public Staff Staff { get; set; }

        public bool IsPrimaryStore { get; set; }

        public DateTime DateStart { get; set; }
    }
}
