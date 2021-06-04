using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class StoreManager : BaseEntity
    {
        [Key]
        public string Username { get; set; }

        public User User { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }
    }
}
