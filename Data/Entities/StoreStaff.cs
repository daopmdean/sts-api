using System;

namespace Data.Entities
{
    public class StoreStaff : BaseEntity
    {
        public int StoreId { get; set; }

        public Store Store { get; set; }

        public string Username { get; set; }

        public User User { get; set; }

        public bool IsPrimaryStore { get; set; }

        public bool IsManager { get; set; }

        public DateTime DateStart { get; set; } = DateTime.Now;
    }
}
