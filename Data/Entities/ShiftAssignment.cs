﻿using System;
using Data.Enums;

namespace Data.Entities
{
    public class ShiftAssignment : BaseEntity
    {
        public int Id { get; set; }

        public int Username { get; set; }

        public User User { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int SkillId { get; set; }

        public Skill Skill { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public DateTime MealStart { get; set; }

        public DateTime MealEnd { get; set; }

        public int ReferenceId { get; set; }

        public ShiftAssignment ShiftReference { get; set; }
    }
}
