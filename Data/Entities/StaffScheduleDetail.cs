namespace Data.Entities
{
    public class StaffScheduleDetail : BaseEntity
    {
        public int Id { get; set; }

        public int WeekScheduleId { get; set; }

        public WeekSchedule WeekSchedule { get; set; }

        public string Username { get; set; }

        public User User { get; set; }

        public int MinHours { get; set; }

        public int MaxHours { get; set; }
    }
}
