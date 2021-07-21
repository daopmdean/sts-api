using Data.Enums;

namespace Data.Entities
{
    public class BaseEntity
    {
        public Status Status { get; set; } = Status.Active;
    }
}
