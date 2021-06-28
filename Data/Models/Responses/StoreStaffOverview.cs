using System;
namespace Data.Models.Responses
{
    public class StoreStaffOverview
    {
        public int StoreId { get; set; }

        public string Username { get; set; }

        public bool IsManager { get; set; }

        public bool IsPrimaryStore { get; set; }

        public DateTime DateStart { get; set; }
    }
}
