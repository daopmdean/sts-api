namespace Data.Entities
{
    public class StaffScheduleDetail
    {
        public int Id { get; set; }

        public int WeekScheduleId { get; set; }

        public WeekSchedule WeekSchedule { get; set; }

        public int StaffId { get; set; }

        public Staff Staff { get; set; }

        public int MinHours { get; set; }

        public int MaxHours { get; set; }
    }
}
