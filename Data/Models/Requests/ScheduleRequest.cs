using System;
using System.Collections.Generic;

namespace Data.Models.Requests
{
    public class ScheduleRequest
    {
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
        public DemandSkillRequest[] DemandBySkills { get; set; }
    }

    public class DemandSkillRequest
    {
        public int SkillId { set; get; }
        public DemandRequest[] Demands { get; set; }
    }

    public class DemandRequest
    {
        public int Quantity { set; get; }
        public int Level { set; get; }
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
        public int Id { get; set; }
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
        public ConstraintGeneral GeneralConstraints { get; set; }
        public ConstraintSpecific FulltimeConstraints { get; set; }
        public ConstraintSpecific ParttimeConstraints { get; set; }
    }

    public class ConstraintGeneral
    {
        public int MinDistanceBetweenSession { get; set; }
    }

    public class ConstraintSpecific
    {
        public int MinDayOff { get; set; }
        public int MaxDayOff { get; set; }
        public int MiWorkingTimeOnWeek { get; set; }
        public int MaxWorkingTimeOnWeek { get; set; }
        public int MinSessionDuration { get; set; }
        public int MaxSessionDuration { get; set; }
        public int MinWorkingTimeInDay { get; set; }
        public int MaxWorkingTimeInDay { get; set; }
        public int MaxNormalHour { get; set; }
        public int MaxShiftInDay { get; set; }
    }
}
