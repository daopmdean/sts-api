using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<PagedList<T>> GetAsync(PaginationParams @params);
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T person);
        void Update(T person);
        void Delete(T person);
        Task<bool> SaveChangesAsync();
    }
}
