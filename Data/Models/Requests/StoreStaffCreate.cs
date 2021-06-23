namespace Data.Models.Requests
{
    public class StoreStaffCreate
    {
        public int StoreId { get; set; }

        public string Username { get; set; }

        public bool IsPrimaryStore { get; set; }

        public bool IsManager { get; set; }
    }
}
