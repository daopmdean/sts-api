namespace Data.Entities
{
    public class ShiftRegister
    {
        public int Id { get; set; }

        public int StaffId { get; set; }

        public Staff Staff { get; set; }

    }
}
