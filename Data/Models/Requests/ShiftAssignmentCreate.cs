using System;
namespace Data.Models.Requests
{
    public class ShiftAssignmentCreate
    {
        public string Username { get; set; }

        public int StoreId { get; set; }

        public int SkillId { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public DateTime MealStart { get; set; }

        public DateTime MealEnd { get; set; }
    }
}
