using System;

namespace Data.Models.Responses
{
    public class StoreStaffOverview
    {
        public int StoreId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public bool IsManager { get; set; }

        public bool IsPrimaryStore { get; set; }

        public DateTime DateStart { get; set; }
    }
}
