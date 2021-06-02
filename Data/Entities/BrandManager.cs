using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class BrandManager
    {
        [Key]
        public string Username { get; set; }

        public User User { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }
    }
}
