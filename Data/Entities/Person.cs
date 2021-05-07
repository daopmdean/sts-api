using System;

namespace Data.Entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public Person()
        {
        }
    }
}
