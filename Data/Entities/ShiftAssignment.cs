namespace Data.Entities
{
    public class ShiftAssignment
    {
        public int Id { get; set; }

        public int StaffId { get; set; }

        public Staff Staff { get; set; }
    }
}
