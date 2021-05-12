using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
