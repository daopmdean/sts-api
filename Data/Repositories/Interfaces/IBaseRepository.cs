using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<PagedList<T>> Get(PaginationParams @params);
        Task<T> GetById(int id);
        Task Create(T person);
        void Update(T person);
        void Delete(T person);
        Task<bool> SaveChangesAsync();
    }
}
