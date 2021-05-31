using System.Collections.Generic;
using Data.Enums;

namespace Data.Entities
{
    public class Brand
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string LogoImg { get; set; }

        public string Hotline { get; set; }

        public Status Status { get; set; }

        public ICollection<Position> Positions { get; set; }

        public ICollection<Staff> Staffs { get; set; }

        public ICollection<Store> Stores { get; set; }

        public ICollection<Skill> Skills { get; set; }
    }
}
