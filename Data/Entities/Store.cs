using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Store : BaseEntity
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public ICollection<StoreStaff> StoreStaffs { get; set; }

        public ICollection<WeekSchedule> WeekSchedules { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}
