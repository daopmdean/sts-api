﻿namespace Data.Entities
{
    public class StoreScheduleDetail
    {
        public int Id { get; set; }

        public int WeekScheduleId { get; set; }

        public WeekSchedule WeekSchedule { get; set; }

        public int MinDayOff { get; set; }

        public int MaxDayOff { get; set; }

        public float MinHoursPerWeek { get; set; }

        public float MaxHoursPerWeek { get; set; }

        public float MinHoursPerDay { get; set; }

        public float MaxHoursPerDay { get; set; }

        public float MinShiftDuration { get; set; }

        public float MaxShiftDuration { get; set; }

        public int MaxShiftPerDay { get; set; }
    }
}