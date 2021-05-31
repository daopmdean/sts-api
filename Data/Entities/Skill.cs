﻿using System;
using System.Collections.Generic;
using Data.Enums;

namespace Data.Entities
{
    public class Skill
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public ICollection<StaffSkill> StaffSkills { get; set; }
    }
}
