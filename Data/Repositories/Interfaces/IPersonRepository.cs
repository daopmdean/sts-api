using System;
using System.Collections.Generic;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        IEnumerable<Person> GetByName(string name);
    }
}
