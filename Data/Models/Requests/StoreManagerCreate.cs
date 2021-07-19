namespace Data.Models.Requests
{
    public class StoreManagerCreate
    {
        public RegisterRequest GeneralInfo { get; set; }

        public StoreStaffCreate JobInformation { get; set; }
    }
}
