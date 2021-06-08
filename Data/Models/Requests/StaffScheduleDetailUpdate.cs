namespace Data.Models.Requests
{
    public class StaffScheduleDetailUpdate
    {
        public int Username { get; set; }

        public int MinHours { get; set; }

        public int MaxHours { get; set; }
    }
}
