using System.Collections.Generic;

namespace Data.Entities
{
    public class Skill : BaseEntity
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<StaffSkill> UserSkills { get; set; }
    }
}
