using System;
using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Data.Entities
{
    public class Attendance : BaseEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string Username { get; set; }
        public User User { get; set; }
        public DateTime TimeCheck { get; set; }
        public string CreatedBy { get; set; }
        public CheckType CheckType { get; set; }
        public string ImageUrl { get; set; }
        [StringLength(200)]
        public string Note { get; set; }
        public float RecognizePercentage { get; set; }
        [StringLength(100)]
        public string DeviceCode { get; set; }
    }
}
