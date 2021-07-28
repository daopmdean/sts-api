using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class WeekSchedule : BaseEntity
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateStart { get; set; }

        public ICollection<ShiftScheduleResult> ShiftScheduleResults { get; set; }

        public ICollection<StoreScheduleDetail> StoreScheduleDetails { get; set; }

        public ICollection<WeekScheduleDetail> WeekScheduleDetails { get; set; }

        //public ICollection<StaffScheduleDetail> StaffScheduleDetails { get; set; }

        public ICollection<ShiftRegister> ShiftRegisters { get; set; }

        public ICollection<ShiftAssignment> ShiftAssignments { get; set; }

        public WeekSchedule ShallowClone()
        {
            var cloneWeekSchedule = (WeekSchedule)MemberwiseClone();
            cloneWeekSchedule.Id = 0;
            cloneWeekSchedule.Status = Enums.Status.Unpublished;
            cloneWeekSchedule.DateCreated = DateTime.Now;

            return cloneWeekSchedule;
        }
    }
}
