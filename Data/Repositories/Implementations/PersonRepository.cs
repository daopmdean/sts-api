using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;

        public PersonRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> Get()
        {
            return _context.People.ToList();
        }

        public Person GetById(int id)
        {
            return _context.People.First(x => x.PersonId == id);
        }

        public void Create(Person person)
        {
            _context.People.Add(person);
        }

        public void Update(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
        }

        public void Delete(Person person)
        {
            _context.Entry(person).State = EntityState.Deleted;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
