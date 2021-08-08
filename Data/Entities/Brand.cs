using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Brand : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string LogoImg { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Hotline { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Store> Stores { get; set; }

        public ICollection<Skill> Skills { get; set; }

        //public ICollection<Post> Posts { get; set; }
    }
}
