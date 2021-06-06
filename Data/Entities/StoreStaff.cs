using System;

namespace Data.Entities
{
    public class StoreStaff : BaseEntity
    {
        public string StoreId { get; set; }

        public Store Store { get; set; }

        public string Username { get; set; }

        public User User { get; set; }

        public bool IsPrimaryStaff { get; set; }

        public DateTime DateStart { get; set; }
    }
}
