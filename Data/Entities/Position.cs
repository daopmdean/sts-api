using Data.Enums;

namespace Data.Entities
{
    public class Position
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public string Mode { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }
    }
}
