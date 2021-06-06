using System.Collections.Generic;

namespace Data.Entities
{
    public class Brand : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoImg { get; set; }

        public string Hotline { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Store> Stores { get; set; }

        public ICollection<Skill> Skills { get; set; }
    }
}
