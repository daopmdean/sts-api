using System;
using System.Collections.Generic;
using Data.Enums;

namespace Data.Entities
{
    public class StoreStaff
    {
        public string StoreId { get; set; }

        public Store Store { get; set; }

        public string StaffId { get; set; }

        public Staff Staff { get; set; }

        public bool IsPrimaryStore { get; set; }

        public DateTime DateStart { get; set; }

        public Status Status { get; set; }
    }
}
