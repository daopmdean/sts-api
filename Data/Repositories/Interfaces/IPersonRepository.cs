using System;
using System.Collections.Generic;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<Person> Get();
        Person GetById(int id);
        void Create(Person person);
        void Update(Person person);
        void Delete(Person person);
        bool SaveChanges();
    }
}
