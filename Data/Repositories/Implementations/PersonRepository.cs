using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implementations
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(DataContext context) : base(context) { }

        public IEnumerable<Person> GetByName(string name)
        {
            return _context.People.Where(person => person.Name.Contains(name));
        }
    }
}
