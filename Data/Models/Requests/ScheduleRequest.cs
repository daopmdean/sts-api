using System;
using System.Collections.Generic;

namespace Data.Models.Requests
{
    public class ScheduleRequest
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public DateTime DateStart { get; set; }
        public List<StaffRequestData> Staffs { get; set; }

        public List<SkillRequest> Skills { get; set; }

        public List<DemandDayRequest> Demands { get; set; }

        public ConstraintData Constraints { get; set; }
    }

    public class SkillRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DemandDayRequest
    {
        public int Day { set; get; }
        public List<DemandSkillRequest> DemandBySkills { get; set; }
    }

    public class DemandSkillRequest
    {
        public int SkillId { set; get; }
        public List<DemandRequest> Demands { get; set; }
    }

    public class DemandRequest
    {
        public int Quantity { set; get; }
        public int Level { set; get; } = 1;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class StaffRequestData
    {
        public string Username { get; set; }
        public List<SkillStaff> Skills { get; set; }
        public int TypeStaff { get; set; }
        public List<AvalailableDayRequest> AvalailableDays { get; set; }
    }

    public class SkillStaff
    {
        public int SkillId { get; set; }
        public int Level { get; set; }
    }

    public class AvalailableDayRequest
    {
        public int Day { get; set; }
        public List<AvailableTimeRequest> AvailableTimes { get; set; }
    }

    public class AvailableTimeRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class ConstraintData
    {
        public int MinDistanceBetweenSession { get; set; }
        public ConstraintSpecific FulltimeConstraints { get; set; }
        public ConstraintSpecific ParttimeConstraints { get; set; }
    }

    public class ConstraintSpecific
    {
        public int MinDayOff { get; set; }
        public int MaxDayOff { get; set; }
        public float MinHoursPerWeek { get; set; }
        public float MaxHoursPerWeek { get; set; }
        public float MinShiftDuration { get; set; }
        public float MaxShiftDuration { get; set; }
        public float MinHoursPerDay { get; set; }
        public float MaxHoursPerDay { get; set; }
        public int MaxNormalHour { get; set; }
        public int MaxShiftPerDay { get; set; }
    }
}
