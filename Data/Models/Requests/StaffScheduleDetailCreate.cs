namespace Data.Models.Requests
{
    public class StaffScheduleDetailCreate
    {
        public int WeekScheduleId { get; set; }

        public string Username { get; set; }

        public int MinHours { get; set; }

        public int MaxHours { get; set; }
    }
}
