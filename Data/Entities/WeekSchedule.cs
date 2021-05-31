﻿using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class WeekSchedule
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public DateTime DateStart { get; set; }

        public ICollection<WeekScheduleDetail> WeekScheduleDetails { get; set; }

        public ICollection<StaffScheduleDetail> StaffScheduleDetails { get; set; }

        public ICollection<ShiftRegister> ShiftRegisters { get; set; }
    }
}
